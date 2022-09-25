using InvestorAPI.Core;
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

        private readonly IBusinessTypeService _businessTypeService;

        #endregion

        #region Constructor

        public BusinessTypeController(IBusinessTypeService businessTypeService)
        {
            _businessTypeService = businessTypeService;
        }

        #endregion

        #region Controller Actions

        /// <summary>
        /// Get the set of available business types.
        /// </summary>
        [HttpGet]
        public IAsyncEnumerable<BusinessTypeOutputDto> GetBusinessTypesAsync()
        {
            return _businessTypeService.GetBusinessTypesAsync();
        }

        /// <summary>
        /// Get business type by id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessTypeOutputDto>> GetBusinessTypeAsync([FromRoute] string id)
        {
            var businessType = await _businessTypeService.FindBusinessTypeAsync(id);
            if (businessType is null)
            {
                return NotFound();
            }
            return businessType;
        }

        /// <summary>
        /// Create new business type.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BusinessTypeOutputDto>> CreateBusinessTypeAsync([FromBody] BusinessTypeInputDto businessTypeDto)
        {
            var result = await _businessTypeService.CreateBusinessTypeAsync(businessTypeDto);
            if (result.Output is not BusinessTypeOutputDto dto)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
                return ValidationProblem(ModelState);
            }
            return CreatedAtAction(nameof(GetBusinessTypeAsync), new { id = dto.Id }, dto);
        }

        /// <summary>
        /// Update business type by id.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<ActionResult<BusinessTypeOutputDto>> UpdateBusinessTypeAsync([FromRoute] string id, [FromBody] JsonPatchDocument<BusinessTypeInputDto> patchDoc)
        {
            var result = await _businessTypeService.UpdateBusinessTypeAsync(id, inputDto => 
                patchDoc.TryApplyTo(inputDto, ModelState));

            if (result.Output is BusinessTypeOutputDto dto)
            {
                return dto;
            }
            if (result.ErrorType == OperationError.DataNotFound)
            {
                return NotFound();
            }
            if (result.ErrorType != OperationError.ExternalError)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
            }
            return ValidationProblem(ModelState);
        }

        /// <summary>
        /// Delete business type by id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusinessTypeAsync([FromRoute] string id)
        {
            var deleteResult = await _businessTypeService.DeleteBusinessTypeAsync(id);

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
