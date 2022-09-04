using InvestorAPI.Models;
using InvestorData;
using Microsoft.AspNetCore.Mvc;

namespace InvestorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        #region Dependencies

        private readonly IProductRepository _productRepository;

        #endregion

        #region Constructor

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        #endregion


        #region Controller Actions

        /// <summary>
        /// Get a page of products, based on the business id and the last fetched product id.
        /// </summary>
        [HttpGet("{businessId}")]
        public async IAsyncEnumerable<ProductOutputDTO> PaginateBrandsAsync([FromRoute] string businessId, [FromQuery] string? lastProductId = null, [FromQuery] int perPage = 70)
        {
            IAsyncEnumerable<Product> products;
            if (lastProductId is null)
            {
                products = await _productRepository.PaginateProductsAsync(businessId, productsPerPage: perPage);
            }
            else products = await _productRepository.PaginateProductsAsync(businessId, lastProductId, perPage);

            await foreach (var product in products)
            {
                yield return new ProductOutputDTO(product);
            }
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductOutputDTO>> GetProductAsync([FromRoute] string id)
        {
            var product = await _productRepository.FindAsync(id);
            if (product is null)
            {
                return NotFound();
            }
            return new ProductOutputDTO(product);
        }

        #endregion

    }
}
