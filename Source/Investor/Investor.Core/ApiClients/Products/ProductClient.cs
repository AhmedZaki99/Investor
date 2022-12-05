using AutoMapper;
using InvestorData;
using Microsoft.Extensions.Options;

namespace Investor.Core
{
    /// <summary>
    /// An endpoint client service that manages <see cref="Product"/> models.
    /// </summary>
    internal class ProductClient : EntityClient<Product, ProductOutputDto, ProductCreateInputDto, ProductUpdateInputDto>, IProductClient
    {

        #region Constructor

        public ProductClient(HttpClient httpClient, IOptions<ApiOptions> optionsAccessor, IMapper mapper) : base(httpClient, optionsAccessor, mapper, "products")
        {
            
        }

        #endregion


        #region Read

        /// <inheritdoc/>
        public Task<IEnumerable<Product>> GetAllAsync(string businessId)
        {
            return GetAllInternalAsync(relativePath: $"filter-in-business/{businessId}");
        }

        #endregion

        #region Filter

        public Task<IEnumerable<Product>> GetAllByCategoryAsync(string businessId, string categoryId)
        {
            string query = $"categoryId={categoryId}";

            return GetAllInternalAsync(query: query, relativePath: $"filter-in-business/{businessId}");
        }

        public Task<IEnumerable<Product>> GetAllByTypeAsync(string businessId, bool isService)
        {
            string query = $"isService={isService}";

            return GetAllInternalAsync(query: query, relativePath: $"filter-in-business/{businessId}");
        }

        public Task<IEnumerable<Product>> GetAllByCategoryAndTypeAsync(string businessId, string categoryId, bool isService)
        {
            string query = $"categoryId={categoryId}&isService={isService}";

            return GetAllInternalAsync(query: query, relativePath: $"filter-in-business/{businessId}");
        }

        #endregion

        #region Search

        public Task<IEnumerable<Product>> SearchAsync(string businessId, string keyword)
        {
            string query = $"keyword={keyword}";

            return GetAllInternalAsync(query: query, relativePath: $"search-in-business/{businessId}");
        }

        #endregion

    }
}
