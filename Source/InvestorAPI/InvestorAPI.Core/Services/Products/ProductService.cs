using AutoMapper;
using AutoMapper.QueryableExtensions;
using InvestorAPI.Data;
using InvestorData;
using Microsoft.EntityFrameworkCore;

namespace InvestorAPI.Core
{
    /// <summary>
    /// A service responsible for handling and processing <see cref="Product"/> models.
    /// </summary>
    internal class ProductService : EntityService<Product, ProductOutputDto, ProductCreateInputDto, ProductUpdateInputDto>, IProductService
    {

        #region Constructor

        public ProductService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, dbContext.Products, mapper)
        {

        }

        #endregion


        #region Read

        /// <inheritdoc/>
        public IAsyncEnumerable<ProductOutputDto> GetEntitiesAsync(string businessId)
        {
            return GetEntitiesAsync(p => p.BusinessId == businessId);
        }

        #endregion

        #region Search

        /// <inheritdoc/>
        public IAsyncEnumerable<ProductOutputDto> SearchByCode(string businessId, string code)
        {
            return GetEntitiesAsync(p => p.BusinessId == businessId && p.Code != null && p.Code.StartsWith(code));
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<ProductOutputDto> SearchByName(string businessId, string name)
        {
            return GetEntitiesAsync(p => p.BusinessId == businessId && p.Name.Contains(name));
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<ProductOutputDto> SearchByCodeThenName(string businessId, string codeOrName)
        {
            return GetEntitiesAsync(p => p.BusinessId == businessId,
                                    p => p.Code != null && p.Code.StartsWith(codeOrName) 
                                      || p.Name.Contains(codeOrName));
        }

        #endregion

        #region Filter

        /// <inheritdoc/>
        public IAsyncEnumerable<ProductOutputDto> FilterByType(string businessId, bool isService)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<ProductOutputDto> FilterByCategory(string businessId, string categoryId)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Validation

        /// <inheritdoc/>
        public override Task<Dictionary<string, string>?> ValidateCreateInputAsync(ProductCreateInputDto dto)
        {
            return ValidateAccountAsync(dto);
        }

        /// <inheritdoc/>
        public override Task<Dictionary<string, string>?> ValidateUpdateInputAsync(ProductUpdateInputDto dto, Product original)
        {
            return ValidateAccountAsync(dto, original);
        }

        private async Task<Dictionary<string, string>?> ValidateAccountAsync(ProductUpdateInputDto dto, Product? original = null)
        {
            if (dto.Name != original?.Name && await EntityDbSet.AnyAsync(a => a.Name == dto.Name))
            {
                return OneErrorDictionary(nameof(dto.Name), "Product name already exists.");
            }

            var errors = dto is ProductCreateInputDto cDto ? await ValidateId(AppDbContext.Businesses, cDto.BusinessId) : null;
            if (errors is not null)
            {
                return errors;
            }

            return null;
        }

        #endregion

    }
}
