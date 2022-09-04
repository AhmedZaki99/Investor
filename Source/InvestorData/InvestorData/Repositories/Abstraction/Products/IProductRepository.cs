namespace InvestorData
{

    /// <summary>
    /// Provides an abstraction for a repository which manages <see cref="Product"/> entities.
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {

        #region Search

        /// <summary>
        /// Search for products by thier code.
        /// </summary>
        /// <param name="businessId">Business to get products from.</param>
        /// <param name="code">The code to search for.</param>
        /// <returns>Products fully or partially matching the code.</returns>
        IAsyncEnumerable<Product> SearchByCode(string businessId, string code);

        /// <summary>
        /// Search for products by thier name.
        /// </summary>
        /// <param name="businessId">Business to get products from.</param>
        /// <param name="name">The name to search for.</param>
        /// <returns>Products fully or partially matching the name.</returns>
        IAsyncEnumerable<Product> SearchByName(string businessId, string name);

        /// <summary>
        /// Search for products matching a given code or name.
        /// </summary>
        /// <param name="businessId">Business to get products from.</param>
        /// <param name="codeOrName">The code or name to search for.</param>
        /// <returns>Products with code or name that's fully or partially matching the input.</returns>
        IAsyncEnumerable<Product> SearchByCodeThenName(string businessId, string codeOrName);

        #endregion

        #region Filter

        /// <summary>
        /// Filter products by type.
        /// </summary>
        /// <param name="businessId">Business to get products from.</param>
        /// <param name="isService">Wheather product is a service.</param>
        /// <returns>Filtered products.</returns>
        IAsyncEnumerable<Product> FilterByType(string businessId, bool isService);

        /// <summary>
        /// Filter products by category.
        /// </summary>
        /// <param name="businessId">Business to get products from.</param>
        /// <param name="category">The category to filter on.</param>
        /// <returns>Filterd products.</returns>
        IAsyncEnumerable<Product> FilterByCategory(string businessId, Category category);

        #endregion

        #region Pagination

        /// <summary>
        /// Returns a page of products starting after the last product fetched.
        /// </summary>
        /// <param name="businessId">Business to get products from.</param>
        /// <param name="lastProductId">
        ///     <para>
        ///     The last product fetched in previous page. 
        ///     </para>
        ///     <para>
        ///     If not provided, the first page is returned.
        ///     </para>
        /// </param>
        /// <param name="productsPerPage">Number of products per page; default is 70.</param>
        Task<IAsyncEnumerable<Product>> PaginateProductsAsync(string businessId, string? lastProductId = null, int productsPerPage = 70);

        #endregion

    }
}
