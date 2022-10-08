using AutoMapper;
using AutoMapper.QueryableExtensions;
using InvestorAPI.Data;
using InvestorData;
using Microsoft.EntityFrameworkCore;

namespace InvestorAPI.Core
{
    /// <summary>
    /// A service responsible for handling and processing <see cref="Business"/> data.
    /// </summary>
    internal class BusinessService : EntityService<Business, BusinessOutputDto, BusinessCreateInputDto, BusinessUpdateInputDto>, IBusinessService
    {

        #region Constructor

        public BusinessService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, dbContext.Businesses, mapper)
        {

        }

        #endregion


        #region Read

        /// <inheritdoc/>
        public override Task<BusinessOutputDto?> FindEntityAsync(string id)
        {
            return EntityDbSet
                .Include(b => b.BusinessType)
                .AsSplitQuery()
                .ProjectTo<BusinessOutputDto>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        #endregion


        #region Validation
        
        /// <inheritdoc/>
        public override Task<Dictionary<string, string>?> ValidateCreateInputAsync(BusinessCreateInputDto dto)
        {
            return ValidateInputAsync(dto);
        }

        /// <inheritdoc/>
        public override Task<Dictionary<string, string>?> ValidateUpdateInputAsync(BusinessUpdateInputDto dto, Business original)
        {
            return ValidateInputAsync(dto, original);
        }

        private async Task<Dictionary<string, string>?> ValidateInputAsync(BusinessUpdateInputDto dto, Business? original = null)
        {
            var errors = await ValidateName(dto.Name!, original?.Name);

            errors ??= dto is BusinessCreateInputDto cDto 
                ? await ValidateId(AppDbContext.BusinessTypes, cDto.BusinessTypeId) 
                : null;

            return errors;
        }

        #endregion

    }
}
