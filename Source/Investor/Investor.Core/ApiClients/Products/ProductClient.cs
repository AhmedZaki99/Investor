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


        #region Interface Implementation

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
            return GetAllInternalAsync(relativePath: $"filter-in-business/{businessId}", query: new
            {
                categoryId
            });
        }

        public Task<IEnumerable<Product>> GetAllByTypeAsync(string businessId, bool isService)
        {
            return GetAllInternalAsync(relativePath: $"filter-in-business/{businessId}", query: new
            {
                isService
            });
        }

        public Task<IEnumerable<Product>> GetAllByCategoryAndTypeAsync(string businessId, string categoryId, bool isService)
        {
            return GetAllInternalAsync(relativePath: $"filter-in-business/{businessId}", query: new
            {
                categoryId,
                isService
            });
        }

        #endregion

        #region Search

        public Task<IEnumerable<Product>> SearchAsync(string businessId, string keyword)
        {
            return GetAllInternalAsync(relativePath: $"search-in-business/{businessId}", query: new
            {
                keyword
            });
        }

        #endregion

        #endregion


        #region Mapping

        protected override Product MapOutput(ProductOutputDto dto)
        {
            var product = base.MapOutput(dto);

            //if (dto.CategoryId is not null)
            //{
            //    product.Category = new Category
            //    {
            //        Id = dto.CategoryId,
            //        Name = dto.CategoryName!
            //    };
            //}
            //if (product.SalesInformation is not null)
            //{
            //    product.SalesInformation.Account = new Account
            //    {
            //        Id = dto.SalesInformation!.AccountId,
            //        Name = dto.SalesInformation.AccountName!
            //    };
            //}
            //if (product.PurchasingInformation is not null)
            //{
            //    product.PurchasingInformation.Account = new Account
            //    {
            //        Id = dto.PurchasingInformation!.AccountId,
            //        Name = dto.PurchasingInformation.AccountName!
            //    };
            //}

            return product;
        }

        #endregion

    }
}
