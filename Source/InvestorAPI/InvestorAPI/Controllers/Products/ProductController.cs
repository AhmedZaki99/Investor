using InvestorAPI.Core;
using InvestorData;
using Microsoft.AspNetCore.Mvc;

namespace InvestorAPI.Controllers
{
    [Route("api/products")]
    public class ProductController : EntityController<Product, ProductOutputDto, ProductCreateInputDto, ProductUpdateInputDto>
    {

        #region Dependencies

        private readonly IProductService _productService;

        #endregion

        #region Constructor

        public ProductController(IProductService productService) : base(productService)
        {
            _productService = productService;
        }

        #endregion


        #region Controller Actions

        /// <summary>
        /// Get all products.
        /// </summary>
        [HttpGet]
        public IAsyncEnumerable<ProductOutputDto> GetProductsAsync()
        {
            return _productService.GetEntitiesAsync();
        }

        /// <summary>
        /// Filter products by category and type.
        /// </summary>
        [HttpGet("filter-in-business/{businessId}")]
        public IAsyncEnumerable<ProductOutputDto> GetProductsAsync([FromRoute] string businessId,
                                                                   [FromQuery] string? categoryId = null,
                                                                   [FromQuery] bool? isService = null)
        {
            return categoryId is not null
                ? isService is not null
                ? _productService.FilterByTypeAndCategory(businessId, isService.Value, categoryId)
                : _productService.FilterByCategory(businessId, categoryId)
                : isService is not null 
                ? _productService.FilterByType(businessId, isService.Value)
                : _productService.GetEntitiesAsync(businessId);
        }
        
        /// <summary>
        /// Search for products by their code and name.
        /// </summary>
        [HttpGet("search-in-business/{businessId}")]
        public IActionResult SearchProductsAsync([FromRoute] string businessId, 
                                                 [FromQuery] string? keyword = null,
                                                 [FromQuery] bool codeOnly = false,
                                                 [FromQuery] bool nameOnly = false)
        {
            if (codeOnly && nameOnly)
            {
                ModelState.AddModelError("query parameters", $"Either {nameof(codeOnly)} or {nameof(nameOnly)} can be set to true individually, but not both.");
                return ValidationProblem(ModelState);
            }

            var products =
                keyword is not null
                ? codeOnly
                ? _productService.SearchByCode(businessId, keyword)
                : nameOnly
                ? _productService.SearchByName(businessId, keyword)
                : _productService.SearchByCodeThenName(businessId, keyword)
                : _productService.GetEntitiesAsync(businessId);

            return Ok(products);
        }

        #endregion

    }
}
