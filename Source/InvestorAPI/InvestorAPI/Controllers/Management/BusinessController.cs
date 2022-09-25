using InvestorAPI.Core;
using InvestorData;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InvestorAPI.Controllers
{
    [ApiController]
    [Route("api/business")]
    public class BusinessController : ControllerBase
    {

        #region Dependencies

        private readonly IBusinessService _businessService;

        #endregion

        #region Constructor

        public BusinessController(IBusinessService businessService)
        {
            _businessService = businessService;
        }

        #endregion


        #region Controller Actions

        /// <summary>
        /// Get the set of available businesses.
        /// </summary>
        [HttpGet]
        public IAsyncEnumerable<BusinessOutputDto> GetBusinessesAsync()
        {
            return _businessService.GetBusinessesAsync();
        }

        /// <summary>
        /// Get business by id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessOutputDto>> GetBusinessAsync([FromRoute] string id)
        {
            var business = await _businessService.FindBusinessAsync(id);
            if (business is null)
            {
                return NotFound();
            }
            return business;
        }

        /// <summary>
        /// Create new business.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BusinessOutputDto>> CreateBusinessAsync([FromBody] BusinessCreateInputDto businessDto)
        {
            var result = await _businessService.CreateBusinessAsync(businessDto);
            if (result.Output is BusinessOutputDto dto)
            {
                return CreatedAtAction(nameof(GetBusinessAsync), new { id = dto.Id }, dto);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Key, error.Value);
            }

            return result.ErrorType == OperationError.UnprocessableEntity ? UnprocessableEntity(ModelState) : ValidationProblem(ModelState);
        }

        /// <summary>
        /// Update business by id.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<ActionResult<BusinessOutputDto>> UpdateBusinessAsync([FromRoute] string id, [FromBody] JsonPatchDocument<BusinessUpdateInputDto> patchDoc)
        {
            var result = await _businessService.UpdateBusinessAsync(id, inputDto =>
               patchDoc.TryApplyTo(inputDto, ModelState));

            if (result.Output is BusinessOutputDto dto)
            {
                return dto;
            }
            if (result.ErrorType == OperationError.EntityNotFound)
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

            return result.ErrorType == OperationError.UnprocessableEntity ? UnprocessableEntity(ModelState) : ValidationProblem(ModelState);
        }

        /// <summary>
        /// Delete business by id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusinessAsync([FromRoute] string id)
        {
            var deleteResult = await _businessService.DeleteBusinessAsync(id);

            if (deleteResult == DeleteResult.EntityNotFound)
            {
                return NotFound();
            }
            else if (deleteResult == DeleteResult.Failed)
            {
                return Problem(statusCode: 500, title: "Server error.", detail: "Failed to delete the business.");
            }

            return NoContent();
        }

        #endregion

    }
}
