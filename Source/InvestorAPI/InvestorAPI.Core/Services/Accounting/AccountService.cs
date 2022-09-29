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
    internal class AccountService : EntityService<Account, AccountOutputDto, AccountCreateInputDto, AccountUpdateInputDto>, IAccountService
    {

        #region Constructor

        public AccountService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, dbContext.Accounts, mapper)
        {

        }

        #endregion


        #region Read

        /// <inheritdoc/>
        public override IAsyncEnumerable<AccountOutputDto> GetEntitiesAsync(Expression<Func<Account, bool>>? condition = null)
        {
            var query = condition is not null ? EntityDbSet.Where(condition) : EntityDbSet;

            return query
                .Where(a => a.ParentAccountId == null)
                .Include(a => a.ChildAccounts)
                .AsSplitQuery()
                .AsNoTracking()
                .AsAsyncEnumerable()
                .Select(Mapper.Map<AccountOutputDto>);
        }

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
        public IAsyncEnumerable<ChildAccountOutputDto> FilterByParentAsync(string parentId)
        {
            ArgumentNullException.ThrowIfNull(parentId, nameof(parentId));

            return EntityDbSet
                .Where(a => a.ParentAccountId == parentId)
                .AsNoTracking()
                .AsAsyncEnumerable()
                .Select(Mapper.Map<ChildAccountOutputDto>);
        }

        #endregion


        #region Validation

        /// <inheritdoc/>
        public override Task<Dictionary<string, string>?> ValidateCreateInputAsync(AccountCreateInputDto dto)
        {
            return ValidateAccountAsync(dto);
        }

        /// <inheritdoc/>
        public override Task<Dictionary<string, string>?> ValidateUpdateInputAsync(AccountUpdateInputDto dto, Account original)
        {
            return ValidateAccountAsync(dto, original);
        }

        private async Task<Dictionary<string, string>?> ValidateAccountAsync(AccountUpdateInputDto dto, Account? original = null)
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

            if (dto is AccountCreateInputDto cDto && cDto.ParentAccountId is not null)
            {
                var parentAccount = await EntityDbSet.FindAsync(cDto.ParentAccountId);
                if (parentAccount is null)
                {
                    return OneErrorDictionary(nameof(cDto.ParentAccountId), "There's no Account found with the Id provided.");
                }
                if (parentAccount.IsSubAccount)
                {
                    return OneErrorDictionary(nameof(cDto.ParentAccountId), "The account provided has a parent account on its own, thus it can't be assigned as a parent account.");
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
                .Include(a => a.ChildAccounts)
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
            return a => a.ParentAccountId == null && (a.BusinessId == businessId || a.BusinessId == null && (a.BusinessTypeId == null || a.BusinessTypeId == a.Business!.BusinessTypeId));
        }

        #endregion

    }
}
