using AutoMapper;
using AutoMapper.QueryableExtensions;
using InvestorAPI.Data;
using InvestorData;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace InvestorAPI.Core
{
    internal abstract class EntityService<TEntity, TOutputDto, TCreateDto, TUpdateDto> : IEntityService<TEntity, TOutputDto, TCreateDto, TUpdateDto>
        where TEntity : EntityBase
        where TOutputDto : OutputDtoBase
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

        public virtual IAsyncEnumerable<TOutputDto> GetEntitiesAsync(params Expression<Func<TEntity, bool>>[] conditions)
        {
            var query = EntityDbSet.AsQueryable();
            foreach (var condition in conditions)
            {
                query = query.Where(condition);
            }

            return query
                .ProjectTo<TOutputDto>(Mapper.ConfigurationProvider)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }


        public virtual Task<TOutputDto?> FindEntityAsync(string id)
        {
            return EntityDbSet
                .ProjectTo<TOutputDto>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        #endregion

        #region Create

        public virtual async Task<OperationResult<TOutputDto>> CreateEntityAsync(TCreateDto dto, bool validateDtoProperties = false)
        {
            var errors = validateDtoProperties ? ValidateObject(dto) : null;
            if (errors is not null)
            {
                return new(errors, OperationError.ValidationError);
            }

            errors = await ValidateCreateInputAsync(dto);
            if (errors is not null)
            {
                return new(errors, OperationError.UnprocessableEntity);
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

        public virtual Task<OperationResult<TOutputDto>> UpdateEntityAsync(string id, TUpdateDto dto, bool validateDtoProperties = false)
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

        public virtual async Task<OperationResult<TOutputDto>> UpdateEntityAsync(string id, Func<TUpdateDto, bool> updateCallback, bool validateDtoProperties = false)
        {
            var entity = await EntityDbSet.FindAsync(id);
            if (entity is null)
            {
                return new(OperationError.EntityNotFound);
            }
            var dto = Mapper.Map<TUpdateDto>(entity);

            if (!updateCallback.Invoke(dto))
            {
                return new(OperationError.ExternalError);
            }

            var errors = validateDtoProperties ? ValidateObject(dto) : null;
            if (errors is not null)
            {
                return new(errors, OperationError.ValidationError);
            }

            errors = await ValidateUpdateInputAsync(dto, entity);
            if (errors is not null)
            {
                return new(errors, OperationError.UnprocessableEntity);
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

        public virtual async Task<DeleteResult> DeleteEntityAsync(string id)
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

        public abstract Task<Dictionary<string, string>?> ValidateCreateInputAsync(TCreateDto dto);

        public abstract Task<Dictionary<string, string>?> ValidateUpdateInputAsync(TUpdateDto dto, TEntity original);

        #endregion

        #region Protected Methods

        protected static async Task<Dictionary<string, string>?> ValidateId<T>(DbSet<T> dbSet, string? id, string? originalId = null) where T : EntityBase
        {
            if (id is not null && id != originalId && !await dbSet.AnyAsync(e => e.Id == id))
            {
                return OneErrorDictionary($"{typeof(T).Name}Id", $"There's no {typeof(T).Name} found with the Id provided.");
            }
            return null;
        }

        protected static Dictionary<string, string> OneErrorDictionary(string key, string message) => new()
        {
            [key] = message
        };

        #endregion


        #region Helper Methods

        private async Task<Dictionary<string, string>?> TrySaveChangesAsync()
        {
            if (await AppDbContext.SaveChangesAsync() > 0)
            {
                return null;
            }
            return OneErrorDictionary("Server Error", "Failed to save data.");
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
