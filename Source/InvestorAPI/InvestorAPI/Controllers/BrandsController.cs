using InvestorAPI.Data;
using InvestorAPI.Models;
using InvestorData;
using Microsoft.AspNetCore.Mvc;

namespace InvestorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController : ControllerBase
    {

        #region Dependencies

        private readonly IBrandRepository _brandRepository;

        #endregion

        #region Constructor

        public BrandsController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        #endregion


        #region Controller Actions

        /// <summary>
        /// Get a page of brands, based on the last fetched brand name.
        /// </summary>
        [HttpGet]
        public async IAsyncEnumerable<BrandOutputDto> PaginateBrandsAsync([FromQuery] string? lastBrandName = null, [FromQuery] int perPage = 30)
        {
            IAsyncEnumerable<Brand> brands;
            if (lastBrandName is null)
            {
                brands = _brandRepository.PaginateBrandsAsync(brandsPerPage: perPage);
            }
            else brands = _brandRepository.PaginateBrandsAsync(lastBrandName, perPage);

            await foreach (var brand in brands)
            {
                yield return new BrandOutputDto(brand);
            }
        }

        /// <summary>
        /// Get brand by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<BrandOutputDto>> GetBrandAsync([FromRoute] string id)
        {
            var brand = await _brandRepository.FindAsync(id);
            if (brand is null)
            {
                return NotFound();
            }
            return new BrandOutputDto(brand);
        }

        /// <summary>
        /// Create new brand.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BrandOutputDto>> CreateBrandAsync([FromBody] BrandCreateInputDto brandDto)
        {
            var brand = await _brandRepository.CreateAsync(brandDto.Map());

            return CreatedAtAction(nameof(GetBrandAsync), new { id = brand.BrandId }, new BrandOutputDto(brand));
        }
        
        /// <summary>
        /// Update brand by id.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrandAsync([FromRoute] string id, [FromBody] BrandUpdateInputDto brandDto)
        {
            if (brandDto.BrandId != id)
            {
                ModelState.AddModelError(nameof(brandDto.BrandId), "Brand Id provided doesn't match with the Brand Id in route.");
            }
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var brand = await _brandRepository.FindAsync(id);
            if (brand is null)
            {
                return NotFound();
            }

            await _brandRepository.UpdateAsync(brandDto.Update(brand));

            return NoContent();
        }

        /// <summary>
        /// Delete brand by id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrandAsync([FromRoute] string id)
        {
            var deleteResult = await _brandRepository.DeleteAsync(id);

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

    }
}
