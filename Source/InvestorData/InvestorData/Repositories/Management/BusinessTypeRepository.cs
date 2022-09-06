namespace InvestorData
{

    /// <summary>
    /// A data store that manages <see cref="BusinessType"/> entities.
    /// </summary>
    internal class BusinessTypeRepository<TContext> : Repository<BusinessType>, IBusinessTypeRepository where TContext : InvestorDbContext
    {

        #region Constructor

        public BusinessTypeRepository(TContext dbContext) : base(dbContext, dbContext.BusinessTypes)
        {

        }

        #endregion


        #region Abstract Implementation

        protected override IQueryable<BusinessType> QueryIncludingMinimalData()
        {
            return DbSet;
        }

        protected override IQueryable<BusinessType> QueryIncludingFullData()
        {
            return QueryIncludingMinimalData();
        }

        #endregion

    }

}
