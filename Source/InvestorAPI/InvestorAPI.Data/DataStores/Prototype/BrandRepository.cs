using InvestorData;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InvestorAPI.Data
{

    /// <summary>
    /// A repository that manages <see cref="Brand"/> entities.
    /// </summary>
    internal class BrandRepository : Repository<Brand, string>, IBrandRepository
    {

        #region Constructor

        public BrandRepository(ApplicationDbContext dbContext) : base(dbContext, dbContext.Brands)
        {
            
        }

        #endregion


        #region Pagination

        /// <inheritdoc/>
        public IAsyncEnumerable<Brand> PaginateBrandsAsync(string? lastbrandName = null, int brandsPerPage = 30)
        {
            if (brandsPerPage <= 0)
            {
                throw new ArgumentException($"{nameof(brandsPerPage)} argument cannot be less than 1.", nameof(brandsPerPage));
            }

            IQueryable<Brand> query = DbSet.OrderBy(b => b.Name);
            if (lastbrandName is not null)
            {
                query = query.Where(b => b.Name.CompareTo(lastbrandName) > 0);
            }

            return query.Take(brandsPerPage)
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


        protected override IQueryable<Brand> QueryIncludingMinimalData()
        {
            return DbSet;
        }

        protected override IQueryable<Brand> QueryIncludingFullData()
        {
            return DbSet;
        }

        #endregion


    }

}
