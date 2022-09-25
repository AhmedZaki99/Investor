﻿using AutoMapper;
using InvestorAPI.Data;
using InvestorData;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvestorAPI.Core
{
    /// <summary>
    /// A service responsible for handling and processing <see cref="BusinessType"/> data.
    /// </summary>
    internal class BusinessTypeService : IBusinessTypeService
    {

        #region Dependencies

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public BusinessTypeService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #endregion


        #region Data Read

        /// <inheritdoc/>
        public IAsyncEnumerable<BusinessTypeOutputDto> GetBusinessTypesAsync()
        {
            return _dbContext.BusinessTypes
                .AsNoTracking()
                .AsAsyncEnumerable()
                .Select(_mapper.Map<BusinessTypeOutputDto>);
        }


        /// <inheritdoc/>
        public async Task<BusinessTypeOutputDto?> FindBusinessTypeAsync(string id)
        {
            var businessType = await _dbContext.BusinessTypes.FindAsync(id);
            
            return businessType is null ? null : _mapper.Map<BusinessTypeOutputDto>(businessType);
        }

        #endregion

        #region Create

        /// <inheritdoc/>
        public async Task<OperationResult<BusinessTypeOutputDto>> CreateBusinessTypeAsync(BusinessTypeInputDto dto, bool validateDtoProperties = false)
        {
            var errors = validateDtoProperties ? ValidateObject(dto) : null;

            errors ??= await ValidateInputAsync(dto);
            if (errors is not null)
            {
                return new(errors , OperationError.ValidationError);
            }

            var businessType = _mapper.Map<BusinessType>(dto);

            _dbContext.BusinessTypes.Add(businessType);

            errors = await TrySaveChangesAsync();
            if (errors is not null)
            {
                return new(errors, OperationError.DatabaseError);
            }
            return new(_mapper.Map<BusinessTypeOutputDto>(businessType));
        }

        #endregion

        #region Update

        /// <inheritdoc/>
        public Task<OperationResult<BusinessTypeOutputDto>> UpdateBusinessTypeAsync(string id, BusinessTypeInputDto dto, bool validateDtoProperties = false)
        {
            var errors = validateDtoProperties ? ValidateObject(dto) : null;
            if (errors is not null)
            {
                return Task.FromResult(new OperationResult<BusinessTypeOutputDto>(errors, OperationError.ValidationError));
            }

            return UpdateBusinessTypeAsync(id, updateDto =>
            {
                updateDto = dto;
                return true;
            });
        }

        /// <inheritdoc/>
        public async Task<OperationResult<BusinessTypeOutputDto>> UpdateBusinessTypeAsync(string id, Func<BusinessTypeInputDto, bool> updateCallback, bool validateDtoProperties = false)
        {
            var businessType = await _dbContext.BusinessTypes.FindAsync(id);
            if (businessType is null)
            {
                return new(OperationError.DataNotFound);
            }
            var dto = _mapper.Map<BusinessTypeInputDto>(businessType);

            if (!updateCallback.Invoke(dto))
            {
                return new(OperationError.ExternalError);
            }

            var errors = validateDtoProperties ? ValidateObject(dto) : null;

            errors ??= await ValidateInputAsync(dto, businessType);
            if (errors is not null)
            {
                return new(errors, OperationError.ValidationError);
            }
            businessType = _mapper.Map(dto, businessType);


            var entry = _dbContext.Entry(businessType);
            if (entry.State == EntityState.Unchanged)
            {
                entry.State = EntityState.Modified;
            }

            errors = await TrySaveChangesAsync();
            if (errors is not null)
            {
                return new(errors, OperationError.DatabaseError);
            }
            return new(_mapper.Map<BusinessTypeOutputDto>(businessType));
        }

        #endregion

        #region Delete

        /// <inheritdoc/>
        public async Task<DeleteResult> DeleteBusinessTypeAsync(string id)
        {
            var businessType = await _dbContext.BusinessTypes.FindAsync(id);
            if (businessType is null)
            {
                return DeleteResult.EntityNotFound;
            }
            _dbContext.BusinessTypes.Remove(businessType);

            return await _dbContext.SaveChangesAsync() > 0 ? DeleteResult.Success : DeleteResult.Failed;
        }

        #endregion
        

        #region Validation

        /// <inheritdoc/>
        public async Task<Dictionary<string, string>?> ValidateInputAsync(BusinessTypeInputDto dto, BusinessType? original = null)
        {
            if (dto.Name != original?.Name && await _dbContext.BusinessTypes.AnyAsync(b => b.Name == dto.Name))
            {
                return new Dictionary<string, string>
                {
                    [nameof(dto.Name)] = "BusinessType name already exists."
                };
            }
            return null;
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
                ["Server Error"] = "Failed to save businessType data."
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
