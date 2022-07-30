using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InvestorAPI.Data
{

    /// <summary>
    /// A data store that manages <see cref="Brand"/> entities.
    /// </summary>
    internal class BrandStore : EntityStore<Brand>, IBrandStore
    {

        #region Constructor

        public BrandStore(ApplicationDbContext dbContext) : base(dbContext, dbContext.Brands)
        {
            
        }

        #endregion


        #region Interface Implementation

        /// <inheritdoc/>
        public Task<List<Brand>> PaginateEntitiesAsync(DateTime lastEntityDate, int entitiesPerPage = 30)
        {
            entitiesPerPage = entitiesPerPage > MaxEntitiesPerPage ? MaxEntitiesPerPage : entitiesPerPage;

            return GetOrderedQuery().Where(b => b.DateCreated < lastEntityDate).Take(entitiesPerPage)
                                    .AsNoTracking()
                                    .ToListAsync();
        }

        #endregion

        #region Abstract Implementation

        protected override Expression<Func<Brand, string>> KeyProperty()
        {
            return b => b.BrandId;
        }
        
        protected override Expression<Func<Brand, bool>> HasKey(string key)
        {
            return b => b.BrandId == key;
        }


        protected override IOrderedQueryable<Brand> GetOrderedQuery()
        {
            return DbSet.OrderByDescending(b => b.DateCreated);
        }

        protected override Expression<Func<Brand, bool>> OrderedAfterEntity(Brand entity)
        {
            return b => b.DateCreated < entity.DateCreated;
        }


        protected override IQueryable<Brand> GetNavigationQuery()
        {
            throw new NotImplementedException("The model does not contain any navigation properties yet.");
        }

        #endregion

    }

}
