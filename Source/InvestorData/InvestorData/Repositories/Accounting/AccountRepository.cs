using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InvestorData
{

    /// <summary>
    /// A data store that manages <see cref="Account"/> entities.
    /// </summary>
    internal class AccountRepository<TContext> : Repository<Account>, IAccountRepository where TContext : InvestorDbContext
    {

        #region Constructor

        public AccountRepository(TContext dbContext) : base(dbContext, dbContext.Accounts)
        {

        }

        #endregion


        #region Read

        public IAsyncEnumerable<Account> GetEntitiesAsync(string businessId)
        {
            return QueryForBusiness(businessId)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        #endregion

        #region Filter

        /// <inheritdoc/>
        public IAsyncEnumerable<Account> FilterByType(string businessId, AccountType accountType)
        {
            return QueryForBusiness(businessId)
                .Where(a => a.AccountType == accountType)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<Account> FilterByParent(string businessId, string parentId)
        {
            return QueryForBusiness(businessId)
                .Where(a => a.ParentAccountId == parentId)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        #endregion


        #region Abstract Implementation

        protected override IQueryable<Account> QueryIncludingMinimalData()
        {
            return DbSet;
        }

        protected override IQueryable<Account> QueryIncludingFullData()
        {
            return QueryIncludingMinimalData().Include(a => a.ParentAccount);
        }

        #endregion


        #region Helper Methods

        private IQueryable<Account> QueryForBusiness(string businessId)
        {
            return QueryIncludingMinimalData()
                .Include(a => a.Business)
                .Where(IncludedInBusiness(businessId));
        }

        private static Expression<Func<Account, bool>> IncludedInBusiness(string businessId)
        {
            return a => a.BusinessId == businessId || a.BusinessId == null && (a.BusinessTypeId == null || a.BusinessTypeId == a.Business!.BusinessTypeId);
        }

        #endregion

    }

}
