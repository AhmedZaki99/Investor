using InvestorData;

namespace InvestorAPI.Core
{
    /// <summary>
    /// Provides an abstraction for a service responsible for handling and processing <see cref="Product"/> models.
    /// </summary>
    public interface IProductService : IBusinessEntityService<Product, ProductOutputDto, ProductCreateInputDto, ProductUpdateInputDto>
    {

        #region Search

        /// <summary>
        /// Search for products by thier code.
        /// </summary>
        /// <param name="businessId">Business to get products from.</param>
        /// <param name="code">The code to search for.</param>
        /// <returns>Products fully or partially matching the code.</returns>
        IAsyncEnumerable<ProductOutputDto> SearchByCode(string businessId, string code);

        /// <summary>
        /// Search for products by thier name.
        /// </summary>
        /// <param name="businessId">Business to get products from.</param>
        /// <param name="name">The name to search for.</param>
        /// <returns>Products fully or partially matching the name.</returns>
        IAsyncEnumerable<ProductOutputDto> SearchByName(string businessId, string name);

        /// <summary>
        /// Search for products matching a given code or name.
        /// </summary>
        /// <param name="businessId">Business to get products from.</param>
        /// <param name="codeOrName">The code or name to search for.</param>
        /// <returns>Products with code or name that's fully or partially matching the input.</returns>
        IAsyncEnumerable<ProductOutputDto> SearchByCodeThenName(string businessId, string codeOrName);

        #endregion

        #region Filter

        /// <summary>
        /// Filter products by type.
        /// </summary>
        /// <param name="businessId">Business to get products from.</param>
        /// <param name="isService">Wheather product is a service.</param>
        /// <returns>Filtered products.</returns>
        IAsyncEnumerable<ProductOutputDto> FilterByType(string businessId, bool isService);

        /// <summary>
        /// Filter products by category.
        /// </summary>
        /// <param name="businessId">Business to get products from.</param>
        /// <param name="categoryId">The category to filter on.</param>
        /// <returns>Filterd products.</returns>
        IAsyncEnumerable<ProductOutputDto> FilterByCategory(string businessId, string categoryId);

        /// <summary>
        /// Filter products by type and category.
        /// </summary>
        /// <param name="businessId">Business to get products from.</param>
        /// <param name="isService">Wheather product is a service.</param>
        /// <param name="categoryId">The category to filter on.</param>
        /// <returns>Filterd products.</returns>
        IAsyncEnumerable<ProductOutputDto> FilterByTypeAndCategory(string businessId, bool isService,string categoryId);

        #endregion

    }
}
