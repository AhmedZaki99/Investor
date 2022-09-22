using Microsoft.EntityFrameworkCore;

namespace InvestorData
{

    /// <summary>
    /// A repository that manages <see cref="ScaleUnit"/> entities.
    /// </summary>
    internal class ScaleUnitRepository<TContext> : Repository<ScaleUnit>, IScaleUnitRepository where TContext : InvestorDbContext
    {

        #region Constructor

        public ScaleUnitRepository(TContext dbContext) : base(dbContext, dbContext.ScaleUnits)
        {

        }

        #endregion


        #region Read

        /// <inheritdoc/>
        public IAsyncEnumerable<ScaleUnit> GetEntitiesAsync(string businessId)
        {
            return QueryIncludingMinimalData()
                .Where(s => s.BusinessId == null || s.BusinessId == businessId)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        #endregion


        #region Abstract Implementation

        protected override IQueryable<ScaleUnit> QueryIncludingMinimalData()
        {
            return DbSet;
        }

        protected override IQueryable<ScaleUnit> QueryIncludingFullData()
        {
            return QueryIncludingMinimalData();
        }

        #endregion

    }

}
