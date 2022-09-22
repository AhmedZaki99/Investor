using Microsoft.EntityFrameworkCore;

namespace InvestorData
{

    /// <summary>
    /// A repository that manages <see cref="Business"/> entities.
    /// </summary>
    internal class BusinessRepository<TContext> : Repository<Business>, IBusinessRepository where TContext : InvestorDbContext
    {

        #region Constructor

        public BusinessRepository(TContext dbContext) : base(dbContext, dbContext.Businesses)
        {

        }

        #endregion


        #region Abstract Implementation

        protected override IQueryable<Business> QueryIncludingMinimalData()
        {
            return DbSet.Include(b => b.BusinessType);
        }

        protected override IQueryable<Business> QueryIncludingFullData()
        {
            return QueryIncludingMinimalData();
        }

        #endregion

    }

}
