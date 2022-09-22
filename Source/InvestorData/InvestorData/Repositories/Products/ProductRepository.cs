using Microsoft.EntityFrameworkCore;

namespace InvestorData
{

    /// <summary>
    /// A repository that manages <see cref="Product"/> entities.
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
        public IAsyncEnumerable<Product> SearchByCode(string businessId, string code)
        {
            return DbSet.Where(p => p.BusinessId == businessId && p.Code != null && p.Code.StartsWith(code))
                        .AsNoTracking()
                        .AsAsyncEnumerable();
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<Product> SearchByName(string businessId, string name)
        {
            return DbSet.Where(p => p.BusinessId == businessId && p.Name.Contains(name))
                        .AsNoTracking()
                        .AsAsyncEnumerable();
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<Product> SearchByCodeThenName(string businessId, string codeOrName)
        {
            return DbSet.Where(p => p.BusinessId == businessId)
                        .Where(p => p.Code != null && p.Code.StartsWith(codeOrName) || p.Name.Contains(codeOrName))
                        .AsNoTracking()
                        .AsAsyncEnumerable();
        }

        #endregion

        #region Filter

        /// <inheritdoc/>
        public IAsyncEnumerable<Product> FilterByType(string businessId, bool isService)
        {
            return QueryIncludingMinimalData()
                .Where(p => p.BusinessId == businessId && p.IsService == isService)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<Product> FilterByCategory(string businessId, string categoryId)
        {
            return QueryIncludingMinimalData()
                .Where(p => p.BusinessId == businessId && p.CategoryId == categoryId)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        #endregion

        #region Pagination

        /// <inheritdoc/>
        public virtual async Task<IAsyncEnumerable<Product>> PaginateProductsAsync(string businessId, string? lastProductId = null, int productsPerPage = 70)
        {
            if (productsPerPage <= 0)
            {
                throw new ArgumentException($"{nameof(productsPerPage)} argument cannot be less than 1.", nameof(productsPerPage));
            }

            IQueryable<Product> query = QueryIncludingFullData().Where(p => p.BusinessId == businessId)
                                                                .OrderBy(p => p);

            Product? lastProduct = await DbSet.Include(p => p.Category)
                                              .FirstOrDefaultAsync(p => p.Id == lastProductId);

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

        protected override IQueryable<Product> QueryIncludingMinimalData()
        {
            return DbSet.Include(p => p.Category)
                        .Include(p => p.ScaleUnit);
        }

        protected override IQueryable<Product> QueryIncludingFullData()
        {
            return QueryIncludingMinimalData()
                .Include(p => p.IncomeAccount)
                .Include(p => p.ExpenseAccount)
                .Include(p => p.InventoryAccount);
        }

        #endregion

    }

}
