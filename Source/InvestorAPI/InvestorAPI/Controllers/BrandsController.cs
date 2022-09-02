using InvestorAPI.Data;
using InvestorAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvestorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController : ControllerBase
    {

        #region Dependencies

        private readonly IBrandStore _brandStore;

        #endregion

        #region Constructor

        public BrandsController(IBrandStore brandStore)
        {
            _brandStore = brandStore;
        }

        #endregion


        #region Controller Actions

        /// <summary>
        /// Get a page of brands, based on the last fetched brand creation date.
        /// </summary>
        [HttpGet]
        public async IAsyncEnumerable<BrandOutputDTO> PaginateBrandsAsync([FromQuery] string? lastBrandId = null, [FromQuery] int perPage = 30)
        {
            IAsyncEnumerable<Brand> brands;
            if (lastBrandId is null)
            {
                brands = _brandStore.PaginateEntitiesAsync(entitiesPerPage: perPage);
            }
            else brands = _brandStore.PaginateEntitiesAsync(lastBrandId, perPage);

            await foreach (var brand in brands)
            {
                yield return new BrandOutputDTO(brand);
            }
        }

        /// <summary>
        /// Get brand by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<BrandOutputDTO>> GetBrandAsync([FromRoute] string id)
        {
            var brand = await _brandStore.FindAsync(id);
            if (brand is null)
            {
                return NotFound();
            }
            return new BrandOutputDTO(brand);
        }

        /// <summary>
        /// Create new brand.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BrandOutputDTO>> CreateBrandAsync([FromBody] BrandCreateInputDTO brandDTO)
        {
            var brand = await _brandStore.CreateAsync(brandDTO.Map());

            return CreatedAtAction(nameof(GetBrandAsync), new { id = brand.BrandId }, new BrandOutputDTO(brand));
        }
        
        /// <summary>
        /// Update brand by id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrandAsync([FromRoute] string id, [FromBody] BrandUpdateInputDTO brandDTO)
        {
            if (brandDTO.BrandId != id)
            {
                ModelState.AddModelError(nameof(brandDTO.BrandId), "Brand Id provided doesn't match with the Brand Id in route.");
            }
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var brand = await _brandStore.FindAsync(id);
            if (brand is null)
            {
                return NotFound();
            }

            await _brandStore.UpdateAsync(brandDTO.Update(brand));

            return NoContent();
        }

        /// <summary>
        /// Delete brand by id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrandAsync([FromRoute] string id)
        {
            var deleteResult = await _brandStore.DeleteAsync(id);

            if (deleteResult == DeleteResult.EntityNotFound)
            {
                return NotFound();
            }
            else if (deleteResult == DeleteResult.Failed)
            {
                return Problem(statusCode: 500, title: "Server error.", detail: "Failed to delete the brand.");
            }

            return NoContent();
        }

        #endregion



        #region Benchmarking

        [HttpGet("old")]
        public async Task<ActionResult<IEnumerable<BrandOutputDTO>>> OldPaginateBrandsAsync([FromQuery] string? lastBrandId = null, [FromQuery] int perPage = 30)
        {
            List<Brand> brands;
            if (lastBrandId is null)
            {
                brands = await _brandStore.OldPaginateEntitiesAsync(entitiesPerPage: perPage);
            }
            else brands = await _brandStore.OldPaginateEntitiesAsync(lastBrandId, perPage);

            return brands.Select(b => new BrandOutputDTO(b)).ToList();
        }

        /// <summary>
        /// Create 70,000 brands.
        /// </summary>
        [HttpPost("70k")]
        public async Task<ActionResult<BrandOutputDTO>> SpamBrandsAsync([FromBody] BrandCreateInputDTO brandDTO)
        {
            var tonOfBrands = Enumerable.Range(0, 70000).Select(i => new Brand
            {
                Name = $"{brandDTO.Name} #{i}",
                Description = brandDTO.Description,
                BuyPrice = brandDTO.BuyPrice,
                SellPrice = brandDTO.SellPrice,
                ScaleUnit = brandDTO.ScaleUnit ?? "Unit"
            });
            await _brandStore.CreateATonAsync(tonOfBrands);

            return NoContent();
        }

        #endregion


    }
}
