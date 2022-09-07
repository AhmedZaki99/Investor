using InvestorAPI.Models;
using InvestorData;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InvestorAPI.Controllers
{
    [ApiController]
    [Route("api/business/types")]
    public class BusinessTypeController : ControllerBase
    {

        #region Dependencies

        private readonly IBusinessTypeRepository _businessTypeRepository;

        #endregion

        #region Constructor

        public BusinessTypeController(IBusinessTypeRepository businessTypeRepository)
        {
            _businessTypeRepository = businessTypeRepository;
        }

        #endregion

        #region Controller Actions

        /// <summary>
        /// Get the set of available business types.
        /// </summary>
        [HttpGet]
        public IAsyncEnumerable<BusinessTypeOutputDTO> GetBusinessTypesAsync()
        {
            return _businessTypeRepository
                .GetEntitiesAsync()
                .Select(t => new BusinessTypeOutputDTO(t));
        }

        /// <summary>
        /// Get business by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessTypeOutputDTO>> GetBusinessTypeAsync([FromRoute] string id)
        {
            var businessType = await _businessTypeRepository.GetFullDataAsync(id);
            if (businessType is null)
            {
                return NotFound();
            }
            return new BusinessTypeOutputDTO(businessType);
        }

        /// <summary>
        /// Create new business type.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BusinessTypeOutputDTO>> CreateBusinessTypeAsync([FromBody] BusinessTypeCreateInputDTO businessTypeDTO)
        {
            var businessType = await _businessTypeRepository.CreateAsync(businessTypeDTO.Map());

            return CreatedAtAction(nameof(GetBusinessTypeAsync), new { id = businessType.Id }, new BusinessTypeOutputDTO(businessType));
        }

        /// <summary>
        /// Update business type by id.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<ActionResult<BusinessTypeOutputDTO>> UpdateBusinessTypeAsync([FromRoute] string id, [FromBody] JsonPatchDocument<BusinessType> patchDoc)
        {
            var businessType = await _businessTypeRepository.FindAsync(id);
            if (businessType is null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(businessType, ModelState);

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            await _businessTypeRepository.UpdateAsync(businessType);

            return new BusinessTypeOutputDTO(businessType);
        }

        /// <summary>
        /// Delete business type by id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusinessTypeAsync([FromRoute] string id)
        {
            var deleteResult = await _businessTypeRepository.DeleteAsync(id);

            if (deleteResult == DeleteResult.EntityNotFound)
            {
                return NotFound();
            }
            else if (deleteResult == DeleteResult.Failed)
            {
                return Problem(statusCode: 500, title: "Server error.", detail: "Failed to delete the business type.");
            }

            return NoContent();
        }

        #endregion

    }
}
