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
        public IAsyncEnumerable<BusinessOutputDto> GetBusinessesAsync()
        {
            return _businessRepository
                .GetEntitiesAsync()
                .Select(_mapper.Map<BusinessOutputDto>);
        }

        /// <summary>
        /// Get business by id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessOutputDto>> GetBusinessAsync([FromRoute] string id)
        {
            var business = await _businessRepository.GetFullDataAsync(id);
            if (business is null)
            {
                return NotFound();
            }
            return _mapper.Map<BusinessOutputDto>(business);
        }

        /// <summary>
        /// Create new business.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BusinessOutputDto>> CreateBusinessAsync([FromBody] BusinessCreateInputDto businessDto)
        {
            if (businessDto.BusinessTypeId is not null && !_businessTypeRepository.EntityExists(businessDto.BusinessTypeId))
            {
                ModelState.AddModelError(nameof(businessDto.BusinessTypeId), "There's no BusinessType found with the Id provided.");
            }
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            
            var business = await _businessRepository.CreateAsync(_mapper.Map<Business>(businessDto));

            return CreatedAtAction(nameof(GetBusinessAsync), new { id = business.Id }, _mapper.Map<BusinessOutputDto>(business));
        }

        /// <summary>
        /// Update business by id.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<ActionResult<BusinessOutputDto>> UpdateBusinessAsync([FromRoute] string id, [FromBody] JsonPatchDocument<BusinessUpdateInputDto> patchDoc)
        {
            var business = await _businessRepository.FindAsync(id);
            if (business is null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<BusinessUpdateInputDto>(business);
            patchDoc.TryApplyTo(dto, ModelState);

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            business = _mapper.Map(dto, business);

            await _businessRepository.UpdateAsync(business);

            return _mapper.Map<BusinessOutputDto>(business);
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
