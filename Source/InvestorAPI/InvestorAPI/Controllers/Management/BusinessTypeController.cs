using AutoMapper;
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
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public BusinessTypeController(IBusinessTypeRepository businessTypeRepository, IMapper mapper)
        {
            _businessTypeRepository = businessTypeRepository;
            _mapper = mapper;
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
                .Select(_mapper.Map<BusinessTypeOutputDTO>);
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
            return _mapper.Map<BusinessTypeOutputDTO>(businessType);
        }

        /// <summary>
        /// Create new business type.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BusinessTypeOutputDTO>> CreateBusinessTypeAsync([FromBody] BusinessTypeInputDTO businessTypeDTO)
        {
            var businessType = await _businessTypeRepository.CreateAsync(_mapper.Map<BusinessType>(businessTypeDTO));

            return CreatedAtAction(nameof(GetBusinessTypeAsync), new { id = businessType.Id }, _mapper.Map<BusinessTypeOutputDTO>(businessType));
        }

        /// <summary>
        /// Update business type by id.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<ActionResult<BusinessTypeOutputDTO>> UpdateBusinessTypeAsync([FromRoute] string id, [FromBody] JsonPatchDocument<BusinessTypeInputDTO> patchDoc)
        {
            var businessType = await _businessTypeRepository.FindAsync(id);
            if (businessType is null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<BusinessTypeInputDTO>(businessType);
            patchDoc.TryApplyTo(dto, ModelState);

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            businessType = _mapper.Map(dto, businessType);

            await _businessTypeRepository.UpdateAsync(businessType);

            return _mapper.Map<BusinessTypeOutputDTO>(businessType);
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
