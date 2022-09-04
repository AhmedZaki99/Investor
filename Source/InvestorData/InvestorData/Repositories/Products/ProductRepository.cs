using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InvestorData
{

    /// <summary>
    /// A data store that manages <see cref="Product"/> entities.
    /// </summary>
    internal class ProductRepository<TContext> : Repository<Product>, IProductRepository where TContext : InvestorDbContext
    {

        #region Constructor

        public ProductRepository(TContext dbContext) : base(dbContext, dbContext.Products)
        {

        }

        #endregion


        #region Search

        /// <inheritdoc/>
        public IAsyncEnumerable<Product> SearchByCode(Business business, string code)
        {
            return DbSet.Where(p => p.BusinessId == business.Id && p.Code != null && p.Code.StartsWith(code))
                        .AsNoTracking()
                        .AsAsyncEnumerable();
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<Product> SearchByName(Business business, string name)
        {
            return DbSet.Where(p => p.BusinessId == business.Id && p.Name.Contains(name))
                        .AsNoTracking()
                        .AsAsyncEnumerable();
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<Product> SearchByCodeThenName(Business business, string codeOrName)
        {
            return DbSet.Where(p => p.BusinessId == business.Id)
                        .Where(p => p.Code != null && p.Code.StartsWith(codeOrName) || p.Name.Contains(codeOrName))
                        .AsNoTracking()
                        .AsAsyncEnumerable();
        }

        #endregion

        #region Filter

        /// <inheritdoc/>
        public IAsyncEnumerable<Product> FilterByType(Business business, bool isService)
        {
            return DbSet.Where(p => p.BusinessId == business.Id && p.IsService == isService)
                        .AsNoTracking()
                        .AsAsyncEnumerable();
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<Product> FilterByCategory(Business business, Category category)
        {
            return DbSet.Where(p => p.BusinessId == business.Id && p.CategoryId == category.Id)
                        .AsNoTracking()
                        .AsAsyncEnumerable();
        }

        #endregion

        #region Pagination

        /// <inheritdoc/>
        public virtual IAsyncEnumerable<Product> PaginateProductsAsync(Business business, Product? lastProduct = null, int productsPerPage = 70)
        {
            if (productsPerPage <= 0)
            {
                throw new ArgumentException($"{nameof(productsPerPage)} argument cannot be less than 1.", nameof(productsPerPage));
            }

            IQueryable<Product> query = DbSet.Where(p => p.BusinessId == business.Id)
                                             .Include(p => p.Category)
                                             .OrderBy(p => p);
            if (lastProduct is not null)
            {
                query = query.Where(p => p.CompareTo(lastProduct) > 1);
            }

            return query.Take(productsPerPage)
                        .AsNoTracking()
                        .AsAsyncEnumerable();
        }

        #endregion

        #region Abstract Implementation

        protected override Expression<Func<Product, string>> KeyProperty()
        {
            return p => p.Id;
        }
        
        protected override Expression<Func<Product, bool>> HasKey(string key)
        {
            return p => p.Id == key;
        }

        #endregion

    }

}
