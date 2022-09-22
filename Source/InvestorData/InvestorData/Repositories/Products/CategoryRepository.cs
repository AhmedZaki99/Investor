using Microsoft.EntityFrameworkCore;

namespace InvestorData
{

    /// <summary>
    /// A repository that manages <see cref="Category"/> entities.
    /// </summary>
    internal class CategoryRepository<TContext> : Repository<Category>, ICategoryRepository where TContext : InvestorDbContext
    {

        #region Constructor

        public CategoryRepository(TContext dbContext) : base(dbContext, dbContext.Categories)
        {

        }

        #endregion


        #region Read

        /// <inheritdoc/>
        public IAsyncEnumerable<Category> GetEntitiesAsync(string businessId)
        {
            return QueryIncludingMinimalData()
                .Where(c => c.BusinessId == null || c.BusinessId == businessId)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        #endregion


        #region Abstract Implementation

        protected override IQueryable<Category> QueryIncludingMinimalData()
        {
            return DbSet;
        }

        protected override IQueryable<Category> QueryIncludingFullData()
        {
            return QueryIncludingMinimalData();
        }

        #endregion

    }

}
