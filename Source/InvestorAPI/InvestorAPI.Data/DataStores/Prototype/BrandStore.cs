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
            MaxEntitiesPerPage = 210000;
        }

        #endregion


        #region Interface Implementation

        /// <inheritdoc/>
        public IAsyncEnumerable<Brand> PaginateEntitiesAsync(string lastEntityId, int entitiesPerPage = 30)
        {
            entitiesPerPage = entitiesPerPage > MaxEntitiesPerPage ? MaxEntitiesPerPage : entitiesPerPage;

            return GetOrderedQuery().Where(b => b.BrandId.CompareTo(lastEntityId) > 0)
                                    .Take(entitiesPerPage)
                                    .AsNoTracking()
                                    .AsAsyncEnumerable();
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
            return DbSet.OrderBy(b => b.BrandId);
        }

        protected override Expression<Func<Brand, bool>> OrderedAfterEntity(Brand entity)
        {
            return b => b.BrandId.CompareTo(entity.BrandId) > 0;
        }


        protected override IQueryable<Brand> GetNavigationQuery()
        {
            throw new NotImplementedException("The model does not contain any navigation properties yet.");
        }

        #endregion



        #region Benchmarking

        public Task<List<Brand>> OldPaginateEntitiesAsync(string lastEntityId, int entitiesPerPage = 30)
        {
            entitiesPerPage = entitiesPerPage > MaxEntitiesPerPage ? MaxEntitiesPerPage : entitiesPerPage;

            return GetOrderedQuery().Where(b => b.BrandId.CompareTo(lastEntityId) > 0)
                                    .Take(entitiesPerPage)
                                    .AsNoTracking()
                                    .ToListAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<Brand> CreateATonAsync(IEnumerable<Brand> brands)
        {
            await DbSet.AddRangeAsync(brands);
            await DbContext.SaveChangesAsync();

            return brands.First();
        }

        #endregion

    }

}
