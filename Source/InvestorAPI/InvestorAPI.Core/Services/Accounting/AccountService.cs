using AutoMapper;
using InvestorAPI.Data;
using InvestorData;
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

            var business = AppDbContext.Businesses.FirstOrDefault(b => b.Id == businessId);
            if (business is null)
            {
                return AsyncEnumerable.Empty<AccountOutputDto>();
            }
            return GetEntitiesAsync(IncludedInBusiness(business));
        }

        #endregion

        #region Filter

        /// <inheritdoc/>
        public IAsyncEnumerable<AccountOutputDto> FilterByTypeAsync(string? businessId, AccountType accountType)
        {
            if (businessId is null)
            {
                return GetEntitiesAsync(a => a.AccountType == accountType);
            }

            var business = AppDbContext.Businesses.FirstOrDefault(b => b.Id == businessId);
            if (business is null)
            {
                return AsyncEnumerable.Empty<AccountOutputDto>();
            }
            return GetEntitiesAsync(a => a.AccountType == accountType, IncludedInBusiness(business));
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
            var errors = await ValidateName(dto.Name!, original?.Name);
            errors ??= await ValidateId(AppDbContext.Businesses, dto.BusinessId, original?.BusinessId);
            errors ??= await ValidateId(AppDbContext.BusinessTypes, dto.BusinessTypeId, original?.BusinessTypeId);

            return errors;
        }

        #endregion


        #region Filter Expressions

        private static Expression<Func<Account, bool>> IncludedInBusiness(Business business)
        {
            return a => a.AccountScope == AccountScope.Global
                     || a.AccountScope == AccountScope.Local && a.BusinessId == business.Id
                     || a.AccountScope == AccountScope.BusinessTypeSpecific && a.BusinessTypeId == business.BusinessTypeId;
        }

        #endregion

    }
}
