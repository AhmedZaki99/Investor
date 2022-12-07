using InvestorData;

namespace Investor.Core
{
    /// <summary>
    /// Provides an abstraction for an endpoint client service, which manages <see cref="Product"/> models.
    /// </summary>
    public interface IProductClient : IBusinessEntityClient<Product, ProductOutputDto, ProductCreateInputDto, ProductUpdateInputDto>
    {

        #region Read

        /// <summary>
        /// Get all products for the given business, filtering them by category.
        /// </summary>
        Task<IEnumerable<Product>> GetAllByCategoryAsync(string businessId, string categoryId);

        /// <summary>
        /// Get all products for the given business, filtering them by type.
        /// </summary>
        Task<IEnumerable<Product>> GetAllByTypeAsync(string businessId, bool isService);

        /// <summary>
        /// Get all products for the given business, filtering them by category and type.
        /// </summary>
        Task<IEnumerable<Product>> GetAllByCategoryAndTypeAsync(string businessId, string categoryId, bool isService);

        #endregion

        #region Search

        /// <summary>
        /// Search for products using a keyword.
        /// </summary>
        Task<IEnumerable<Product>> SearchAsync(string businessId, string keyword);

        #endregion

    }
}
