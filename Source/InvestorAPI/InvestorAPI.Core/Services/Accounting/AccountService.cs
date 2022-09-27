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

            return FilterAccounts(businessId);
        }

        #endregion

        #region Filter

        /// <inheritdoc/>
        public IAsyncEnumerable<AccountOutputDto> FilterByTypeAsync(string? businessId, AccountType accountType)
        {
            return businessId is null
                   ? GetEntitiesAsync(a => a.AccountType == accountType)
                   : FilterAccounts(businessId, condition: a => a.AccountType == accountType);
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<AccountOutputDto> FilterByParentAsync(string? businessId, string parentId)
        {
            ArgumentNullException.ThrowIfNull(parentId, nameof(parentId));

            return businessId is null
                   ? GetEntitiesAsync(a => a.ParentAccountId == parentId)
                   : FilterAccounts(businessId, condition: a => a.ParentAccountId == parentId);
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
            if (dto.BusinessId is not null && dto.BusinessTypeId is not null)
            {
                return OneErrorDictionary("BusinessId / BusinessTypeId", "Accounts can't be assigned for both Business and BusinessType, try providing only one of them.");
            }

            if (dto.ParentAccountId is not null && dto.ParentAccountId != original?.ParentAccountId)
            {
                var parentAccount = await EntityDbSet.FindAsync(dto.ParentAccountId);
                if (parentAccount is null)
                {
                    return OneErrorDictionary(nameof(dto.ParentAccountId), "There's no Account found with the Id provided.");
                }
                if (parentAccount.IsSubAccount)
                {
                    return OneErrorDictionary(nameof(dto.ParentAccountId), "The account provided has a parent account on its own, thus it can't be assigned as a parent account.");
                }
            }
            return null;
        }

        #endregion


        #region Helper Methods

        private IAsyncEnumerable<AccountOutputDto> FilterAccounts(string businessId, Expression<Func<Account, bool>>? condition = null)
        {
            ArgumentNullException.ThrowIfNull(businessId, nameof(businessId));

            var query = EntityDbSet
                .Include(a => a.Business)
                .Where(IncludedInBusiness(businessId));

            if (condition is not null)
            {
                query = query.Where(condition);
            }

            return query
                .AsSplitQuery()
                .AsNoTracking()
                .AsAsyncEnumerable()
                .Select(Mapper.Map<AccountOutputDto>);
        }

        private static Expression<Func<Account, bool>> IncludedInBusiness(string businessId)
        {
            return a => a.BusinessId == businessId || a.BusinessId == null && (a.BusinessTypeId == null || a.BusinessTypeId == a.Business!.BusinessTypeId);
        }

        #endregion

    }
}
