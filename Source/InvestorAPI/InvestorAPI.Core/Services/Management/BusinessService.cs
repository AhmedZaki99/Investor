using AutoMapper;
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
        public override async Task<BusinessOutputDto?> FindEntityAsync(string id)
        {
            var business = await EntityDbSet
                .Include(b => b.BusinessType)
                .FirstOrDefaultAsync(b => b.Id == id);

            return business is null ? null : Mapper.Map<BusinessOutputDto>(business);
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
            var errors = new Dictionary<string, string>();

            if (dto.Name != original?.Name && await EntityDbSet.AnyAsync(b => b.Name == dto.Name))
            {
                errors.Add(nameof(dto.Name), "Business name already exists.");
            }
            if (dto is BusinessCreateInputDto cDto && !await EntityDbSet.AnyAsync(b => b.Id == cDto.BusinessTypeId))
            {
                errors.Add(nameof(cDto.BusinessTypeId), "There's no BusinessType found with the Id provided.");
            }

            return errors.Count > 0 ? errors : null;
        }

        #endregion

    }
}
