using AutoMapper;
using InvestorAPI.Data;
using InvestorData;
using Microsoft.EntityFrameworkCore;

namespace InvestorAPI.Core
{
    /// <summary>
    /// A service responsible for handling and processing <see cref="BusinessType"/> data.
    /// </summary>
    internal class BusinessTypeService : EntityService<BusinessType, BusinessTypeOutputDto, BusinessTypeInputDto, BusinessTypeInputDto>, IBusinessTypeService
    {

        #region Constructor

        public BusinessTypeService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, dbContext.BusinessTypes, mapper)
        {

        }

        #endregion


        #region Data Read

        /// <inheritdoc/>
        public IAsyncEnumerable<BusinessTypeOutputDto> GetBusinessTypesAsync()
        {
            return GetEntitiesAsync();
        }


        /// <inheritdoc/>
        public Task<BusinessTypeOutputDto?> FindBusinessTypeAsync(string id)
        {
            return FindEntityAsync(id);
        }

        #endregion

        #region Create

        /// <inheritdoc/>
        public Task<OperationResult<BusinessTypeOutputDto>> CreateBusinessTypeAsync(BusinessTypeInputDto dto, bool validateDtoProperties = false)
        {
            return CreateEntityAsync(dto, validateDtoProperties);
        }

        #endregion

        #region Update

        /// <inheritdoc/>
        public Task<OperationResult<BusinessTypeOutputDto>> UpdateBusinessTypeAsync(string id, BusinessTypeInputDto dto, bool validateDtoProperties = false)
        {
            return UpdateEntityAsync(id, dto, validateDtoProperties);
        }

        /// <inheritdoc/>
        public Task<OperationResult<BusinessTypeOutputDto>> UpdateBusinessTypeAsync(string id, Func<BusinessTypeInputDto, bool> updateCallback, bool validateDtoProperties = false)
        {
            return UpdateEntityAsync(id, updateCallback, validateDtoProperties);
        }

        #endregion

        #region Delete

        /// <inheritdoc/>
        public Task<DeleteResult> DeleteBusinessTypeAsync(string id)
        {
            return DeleteEntityAsync(id);
        }

        #endregion


        #region Validation

        /// <inheritdoc/>
        public async Task<Dictionary<string, string>?> ValidateInputAsync(BusinessTypeInputDto dto, BusinessType? original = null)
        {
            if (dto.Name != original?.Name && await EntityDbSet.AnyAsync(b => b.Name == dto.Name))
            {
                return new Dictionary<string, string>
                {
                    [nameof(dto.Name)] = "BusinessType name already exists."
                };
            }
            return null;
        }

        protected override Task<Dictionary<string, string>?> ValidateCreateInputAsync(BusinessTypeInputDto dto)
        {
            return ValidateInputAsync(dto);
        }

        protected override Task<Dictionary<string, string>?> ValidateUpdateInputAsync(BusinessTypeInputDto dto, BusinessType original)
        {
            return ValidateInputAsync(dto, original);
        }

        #endregion

    }
}
