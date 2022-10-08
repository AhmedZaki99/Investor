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


        #region Validation

        /// <inheritdoc/>
        public override Task<Dictionary<string, string>?> ValidateCreateInputAsync(BusinessTypeInputDto dto)
        {
            return ValidateInputAsync(dto);
        }

        /// <inheritdoc/>
        public override Task<Dictionary<string, string>?> ValidateUpdateInputAsync(BusinessTypeInputDto dto, BusinessType original)
        {
            return ValidateInputAsync(dto, original);
        }

        private Task<Dictionary<string, string>?> ValidateInputAsync(BusinessTypeInputDto dto, BusinessType? original = null)
        {
            return ValidateName(dto.Name!, original?.Name);
        }

        #endregion

    }
}
