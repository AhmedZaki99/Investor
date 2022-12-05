using AutoMapper;
using InvestorData;
using Microsoft.Extensions.Options;

namespace Investor.Core
{
    /// <summary>
    /// An endpoint client service that manages <see cref="Category"/> models.
    /// </summary>
    internal class CategoryClient : EntityClient<Category, CategoryOutputDto, CategoryCreateInputDto, CategoryUpdateInputDto>, ICategoryClient
    {

        #region Constructor

        public CategoryClient(HttpClient httpClient, IOptions<ApiOptions> optionsAccessor, IMapper mapper) : base(httpClient, optionsAccessor, mapper, "products/categories")
        {
            
        }

        #endregion


        #region Read

        /// <inheritdoc/>
        public Task<IEnumerable<Category>> GetAllAsync(string businessId)
        {
            string query = $"businessId={businessId}";

            return GetAllInternalAsync(query);
        }

        #endregion

    }
}
