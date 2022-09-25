using AutoMapper;
using InvestorAPI.Data;
using InvestorData;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvestorAPI.Core
{
    /// <summary>
    /// A service responsible for handling and processing <see cref="Business"/> data.
    /// </summary>
    internal class BusinessService : IBusinessService
    {

        #region Dependencies

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public BusinessService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #endregion


        #region Data Read

        /// <inheritdoc/>
        public IAsyncEnumerable<BusinessOutputDto> GetBusinessesAsync()
        {
            return _dbContext.Businesses
                .AsNoTracking()
                .AsAsyncEnumerable()
                .Select(_mapper.Map<BusinessOutputDto>);
        }


        /// <inheritdoc/>
        public async Task<BusinessOutputDto?> FindBusinessAsync(string id)
        {
            var business = await _dbContext.Businesses
                .Include(b => b.BusinessType)
                .FirstOrDefaultAsync(b => b.Id == id);

            return business is null ? null : _mapper.Map<BusinessOutputDto>(business);
        }

        #endregion

        #region Create

        /// <inheritdoc/>
        public async Task<OperationResult<BusinessOutputDto>> CreateBusinessAsync(BusinessCreateInputDto dto, bool validateDtoProperties = false)
        {
            var errors = validateDtoProperties ? ValidateObject(dto) : null;

            errors ??= await ValidateInputAsync(dto);
            if (errors is not null)
            {
                return new(errors , OperationError.ValidationError);
            }

            var business = _mapper.Map<Business>(dto);

            _dbContext.Businesses.Add(business);

            errors = await TrySaveChangesAsync();
            if (errors is not null)
            {
                return new(errors, OperationError.DatabaseError);
            }
            return new(_mapper.Map<BusinessOutputDto>(business));
        }

        #endregion
        
        #region Update

        /// <inheritdoc/>
        public Task<OperationResult<BusinessOutputDto>> UpdateBusinessAsync(string id, BusinessUpdateInputDto dto, bool validateDtoProperties = false)
        {
            var errors = validateDtoProperties ? ValidateObject(dto) : null;
            if (errors is not null)
            {
                return Task.FromResult(new OperationResult<BusinessOutputDto>(errors, OperationError.ValidationError));
            }

            return UpdateBusinessAsync(id, updateDto =>
            {
                updateDto = dto;
                return true;
            });
        }

        /// <inheritdoc/>
        public async Task<OperationResult<BusinessOutputDto>> UpdateBusinessAsync(string id, Func<BusinessUpdateInputDto, bool> updateCallback, bool validateDtoProperties = false)
        {
            var business = await _dbContext.Businesses.FindAsync(id);
            if (business is null)
            {
                return new(OperationError.DataNotFound);
            }
            var dto = _mapper.Map<BusinessUpdateInputDto>(business);

            if (!updateCallback.Invoke(dto))
            {
                return new(OperationError.ExternalError);
            }

            var errors = validateDtoProperties ? ValidateObject(dto) : null;

            errors ??= await ValidateInputAsync(dto, business);
            if (errors is not null)
            {
                return new(errors, OperationError.ValidationError);
            }
            business = _mapper.Map(dto, business);


            var entry = _dbContext.Entry(business);
            if (entry.State == EntityState.Unchanged)
            {
                entry.State = EntityState.Modified;
            }

            errors = await TrySaveChangesAsync();
            if (errors is not null)
            {
                return new(errors, OperationError.DatabaseError);
            }
            return new(_mapper.Map<BusinessOutputDto>(business));
        }

        #endregion

        #region Delete

        /// <inheritdoc/>
        public async Task<DeleteResult> DeleteBusinessAsync(string id)
        {
            var business = await _dbContext.Businesses.FindAsync(id);
            if (business is null)
            {
                return DeleteResult.EntityNotFound;
            }
            _dbContext.Businesses.Remove(business);

            return await _dbContext.SaveChangesAsync() > 0 ? DeleteResult.Success : DeleteResult.Failed;
        }

        #endregion


        #region Validation

        /// <inheritdoc/>
        public async Task<Dictionary<string, string>?> ValidateInputAsync(BusinessUpdateInputDto dto, Business? original = null)
        {
            var errors = new Dictionary<string, string>();

            if (dto.Name != original?.Name && await _dbContext.Businesses.AnyAsync(b => b.Name == dto.Name))
            {
                errors.Add(nameof(dto.Name), "Business name already exists.");
            }
            if (dto is BusinessCreateInputDto cDto && !await _dbContext.BusinessTypes.AnyAsync(b => b.Id == cDto.BusinessTypeId))
            {
                errors.Add(nameof(cDto.BusinessTypeId), "There's no BusinessType found with the Id provided.");
            }

            return errors.Count > 0 ? errors : null;
        }

        #endregion


        #region Helper Methods

        private async Task<Dictionary<string, string>?> TrySaveChangesAsync()
        {
            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return null;
            }

            return new Dictionary<string, string>
            {
                ["Server Error"] = "Failed to save business data."
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
