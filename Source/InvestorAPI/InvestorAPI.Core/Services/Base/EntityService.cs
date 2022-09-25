using AutoMapper;
using InvestorAPI.Data;
using InvestorData;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvestorAPI.Core
{
    internal abstract class EntityService<TEntity, TOutputDto, TCreateDto, TUpdateDto> 
        where TEntity : class 
        where TOutputDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {

        #region Protected Properties

        protected ApplicationDbContext AppDbContext { get; }
        protected DbSet<TEntity> EntityDbSet { get; }

        protected IMapper Mapper { get; }

        #endregion


        #region Constructor

        public EntityService(ApplicationDbContext dbContext, DbSet<TEntity> dbSet, IMapper mapper)
        {
            AppDbContext = dbContext;
            EntityDbSet = dbSet;
            Mapper = mapper;
        }

        #endregion


        #region Data Read

        protected virtual IAsyncEnumerable<TOutputDto> GetEntitiesAsync()
        {
            return EntityDbSet
                .AsNoTracking()
                .AsAsyncEnumerable()
                .Select(Mapper.Map<TOutputDto>);
        }


        protected virtual async Task<TOutputDto?> FindEntityAsync(string id)
        {
            var entity = await EntityDbSet.FindAsync(id);

            return entity is null ? null : Mapper.Map<TOutputDto>(entity);
        }

        #endregion

        #region Create

        protected virtual async Task<OperationResult<TOutputDto>> CreateEntityAsync(TCreateDto dto, bool validateDtoProperties = false)
        {
            var errors = validateDtoProperties ? ValidateObject(dto) : null;

            errors ??= await ValidateCreateInputAsync(dto);
            if (errors is not null)
            {
                return new(errors, OperationError.ValidationError);
            }

            var entity = Mapper.Map<TEntity>(dto);

            EntityDbSet.Add(entity);

            errors = await TrySaveChangesAsync();
            if (errors is not null)
            {
                return new(errors, OperationError.DatabaseError);
            }
            return new(Mapper.Map<TOutputDto>(entity));
        }

        #endregion

        #region Update

        protected virtual Task<OperationResult<TOutputDto>> UpdateEntityAsync(string id, TUpdateDto dto, bool validateDtoProperties = false)
        {
            var errors = validateDtoProperties ? ValidateObject(dto) : null;
            if (errors is not null)
            {
                return Task.FromResult(new OperationResult<TOutputDto>(errors, OperationError.ValidationError));
            }

            return UpdateEntityAsync(id, updateDto =>
            {
                updateDto = dto;
                return true;
            });
        }

        protected virtual async Task<OperationResult<TOutputDto>> UpdateEntityAsync(string id, Func<TUpdateDto, bool> updateCallback, bool validateDtoProperties = false)
        {
            var entity = await EntityDbSet.FindAsync(id);
            if (entity is null)
            {
                return new(OperationError.DataNotFound);
            }
            var dto = Mapper.Map<TUpdateDto>(entity);

            if (!updateCallback.Invoke(dto))
            {
                return new(OperationError.ExternalError);
            }

            var errors = validateDtoProperties ? ValidateObject(dto) : null;

            errors ??= await ValidateUpdateInputAsync(dto, entity);
            if (errors is not null)
            {
                return new(errors, OperationError.ValidationError);
            }
            entity = Mapper.Map(dto, entity);


            var entry = AppDbContext.Entry(entity);
            if (entry.State == EntityState.Unchanged)
            {
                entry.State = EntityState.Modified;
            }

            errors = await TrySaveChangesAsync();
            if (errors is not null)
            {
                return new(errors, OperationError.DatabaseError);
            }
            return new(Mapper.Map<TOutputDto>(entity));
        }

        #endregion

        #region Delete

        protected virtual async Task<DeleteResult> DeleteEntityAsync(string id)
        {
            var entity = await EntityDbSet.FindAsync(id);
            if (entity is null)
            {
                return DeleteResult.EntityNotFound;
            }
            EntityDbSet.Remove(entity);

            return await AppDbContext.SaveChangesAsync() > 0 ? DeleteResult.Success : DeleteResult.Failed;
        }

        #endregion


        #region Abstract Methods

        protected abstract Task<Dictionary<string, string>?> ValidateCreateInputAsync(TCreateDto dto);

        protected abstract Task<Dictionary<string, string>?> ValidateUpdateInputAsync(TUpdateDto dto, TEntity original);

        #endregion


        #region Helper Methods

        private async Task<Dictionary<string, string>?> TrySaveChangesAsync()
        {
            if (await AppDbContext.SaveChangesAsync() > 0)
            {
                return null;
            }

            return new Dictionary<string, string>
            {
                ["Server Error"] = "Failed to save data."
            };
        }

        private static Dictionary<string, string>? ValidateObject<T>(T objectToValidate) where T : class
        {
            List<ValidationResult> results = new();
            ValidationContext context = new(objectToValidate);

            Validator.TryValidateObject(objectToValidate, context, results, true);

            if (results.Count > 0)
            {
                var pairs = results.Select(e => new KeyValuePair<string, string>(e.MemberNames.First(), e.ErrorMessage ?? "Invalid value."));
                return new(pairs);
            }
            return null;
        }

        #endregion

    }
}
