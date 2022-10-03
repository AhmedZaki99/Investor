using AutoMapper;
using InvestorAPI.Data;
using InvestorData;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InvestorAPI.Core
{
    /// <summary>
    /// A service responsible for handling and processing <see cref="Account"/> models.
    /// </summary>
    internal class AccountService : EntityService<Account, AccountOutputDto, AccountInputDto, AccountInputDto>, IAccountService
    {

        #region Constructor

        public AccountService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, dbContext.Accounts, mapper)
        {

        }

        #endregion


        #region Read

        /// <inheritdoc/>
        public IAsyncEnumerable<AccountOutputDto> GetEntitiesAsync(string businessId)
        {
            ArgumentNullException.ThrowIfNull(businessId, nameof(businessId));

            return GetEntitiesAsync(IncludedInBusiness(businessId));
        }

        #endregion

        #region Filter

        /// <inheritdoc/>
        public IAsyncEnumerable<AccountOutputDto> FilterByTypeAsync(string? businessId, AccountType accountType)
        {
            return businessId is null
                   ? GetEntitiesAsync(a => a.AccountType == accountType)
                   : GetEntitiesAsync(a => a.AccountType == accountType, IncludedInBusiness(businessId));
        }

        #endregion


        #region Validation

        /// <inheritdoc/>
        public override Task<Dictionary<string, string>?> ValidateCreateInputAsync(AccountInputDto dto)
        {
            return ValidateAccountAsync(dto);
        }

        /// <inheritdoc/>
        public override Task<Dictionary<string, string>?> ValidateUpdateInputAsync(AccountInputDto dto, Account original)
        {
            return ValidateAccountAsync(dto, original);
        }

        private async Task<Dictionary<string, string>?> ValidateAccountAsync(AccountInputDto dto, Account? original = null)
        {
            if (dto.Name != original?.Name && await EntityDbSet.AnyAsync(a => a.Name == dto.Name))
            {
                return OneErrorDictionary(nameof(dto.Name), "Account name already exists.");
            }

            var errors = await ValidateId(AppDbContext.Businesses, dto.BusinessId, original?.BusinessId);
            if (errors is not null)
            {
                return errors;
            }
            errors = await ValidateId(AppDbContext.BusinessTypes, dto.BusinessTypeId, original?.BusinessTypeId);
            if (errors is not null)
            {
                return errors;
            }

            return null;
        }

        #endregion


        #region Filter Expressions

        private static Expression<Func<Account, bool>> IncludedInBusiness(string businessId)
        {
            return a => a.BusinessId == businessId || a.BusinessId == null && (a.BusinessTypeId == null || a.BusinessTypeId == a.Business!.BusinessTypeId);
        }

        #endregion

    }
}
