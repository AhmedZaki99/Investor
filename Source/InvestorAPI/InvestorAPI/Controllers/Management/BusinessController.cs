using AutoMapper;
using InvestorAPI.Models;
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

        private readonly IBusinessRepository _businessRepository;
        private readonly IBusinessTypeRepository _businessTypeRepository;

        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public BusinessController(IBusinessRepository businessRepository, IBusinessTypeRepository businessTypeRepository, IMapper mapper)
        {
            _businessRepository = businessRepository;
            _businessTypeRepository = businessTypeRepository;
            _mapper = mapper;
        }

        #endregion


        #region Controller Actions

        /// <summary>
        /// Get the set of available businesses.
        /// </summary>
        [HttpGet]
        public IAsyncEnumerable<BusinessOutputDTO> GetBusinessesAsync()
        {
            return _businessRepository
                .GetEntitiesAsync()
                .Select(_mapper.Map<BusinessOutputDTO>);
        }

        /// <summary>
        /// Get business by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessOutputDTO>> GetBusinessAsync([FromRoute] string id)
        {
            var business = await _businessRepository.GetFullDataAsync(id);
            if (business is null)
            {
                return NotFound();
            }
            return _mapper.Map<BusinessOutputDTO>(business);
        }

        /// <summary>
        /// Create new business.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BusinessOutputDTO>> CreateBusinessAsync([FromBody] BusinessCreateInputDTO businessDTO)
        {
            if (businessDTO.BusinessTypeId is not null && !_businessTypeRepository.EntityExists(businessDTO.BusinessTypeId))
            {
                ModelState.AddModelError(nameof(businessDTO.BusinessTypeId), "There's no BusinessType found with the Id provided.");
            }
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            
            var business = await _businessRepository.CreateAsync(_mapper.Map<Business>(businessDTO));

            return CreatedAtAction(nameof(GetBusinessAsync), new { id = business.Id }, _mapper.Map<BusinessOutputDTO>(business));
        }

        /// <summary>
        /// Update business by id.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<ActionResult<BusinessOutputDTO>> UpdateBusinessAsync([FromRoute] string id, [FromBody] JsonPatchDocument<Business> patchDoc)
        {
            var business = await _businessRepository.FindAsync(id);
            if (business is null)
            {
                return NotFound();
            }

            // IMPORTANT: Add the ability to validate Patch Document operations based on a DTO, to prevent both overposting and invalid values.

            patchDoc.TryApplyTo(business, ModelState, new string[]
            {
                nameof(Business.Id),
                nameof(Business.BusinessTypeId)
            });

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            await _businessRepository.UpdateAsync(business);

            return _mapper.Map<BusinessOutputDTO>(business);
        }

        /// <summary>
        /// Delete business by id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusinessAsync([FromRoute] string id)
        {
            var deleteResult = await _businessRepository.DeleteAsync(id);

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
