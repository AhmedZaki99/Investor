using AutoMapper;
using InvestorAPI.Core;
using InvestorData;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace InvestorAPI.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {

        #region Dependencies

        private readonly IAccountService _accountService;

        private readonly IAccountRepository _accountRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly IBusinessTypeRepository _businessTypeRepository;

        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public AccountController(IAccountService accountService,
                                 IAccountRepository accountRepository,
                                 IBusinessRepository businessRepository,
                                 IBusinessTypeRepository businessTypeRepository,
                                 IMapper mapper)
        {
            _accountService = accountService;

            _accountRepository = accountRepository;
            _businessRepository = businessRepository;
            _businessTypeRepository = businessTypeRepository;
            _mapper = mapper;
        }

        #endregion


        #region Controller Actions

        /// <summary>
        /// Get the set of accounts related to the specified business, parent, or type.
        /// </summary>
        [HttpGet]
        public IActionResult GetAccountsAsync([FromQuery(Name = "business")] string? businessId = null,
                                              [FromQuery(Name = "parent")] string? parentId = null,
                                              [FromQuery] AccountType? type = null)
        {
            if (parentId is not null && type is not null)
            {
                ModelState.AddModelError("query parameters", "Accounts can't be filtered based on both parent and type, either provide only one of them or none.");

                return ValidationProblem(ModelState);
            }

            IAsyncEnumerable<Account> accounts;
            if (businessId is null)
            {
                Expression<Func<Account, bool>>? condition;
                condition = parentId is null ?
                            type is null ?
                            null :
                            a => a.AccountType == type :
                            a => a.ParentAccountId == parentId;

                accounts = _accountRepository.GetEntitiesAsync(condition);
            }
            else
            {
                accounts = parentId is null ?
                           type is AccountType aType ?
                           _accountRepository.FilterByType(businessId, aType) :
                           _accountRepository.GetEntitiesAsync(businessId) :
                           _accountRepository.FilterByParent(businessId, parentId);
            }

            return Ok(accounts.Select(_mapper.Map<AccountOutputDto>));
        }

        /// <summary>
        /// Get account by id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountOutputDto>> GetAccountAsync([FromRoute] string id)
        {
            var account = await _accountRepository.GetMinimalDataAsync(id);
            if (account is null)
            {
                return NotFound();
            }
            return _mapper.Map<AccountOutputDto>(account);
        }

        /// <summary>
        /// Create new account.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<AccountOutputDto>> CreateAccountAsync([FromBody] AccountInputDto accountDto)
        {
            var errors = await _accountService.ValidateAccountAsync(accountDto);
            if (errors.Count > 0)
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
                return ValidationProblem(ModelState);
            }
            
            var account = await _accountRepository.CreateAsync(_mapper.Map<Account>(accountDto));

            return CreatedAtAction(nameof(GetAccountAsync), new { id = account.Id }, _mapper.Map<AccountOutputDto>(account));
        }

        /// <summary>
        /// Update account by id.
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<ActionResult<AccountOutputDto>> UpdateAccountAsync([FromRoute] string id, [FromBody] JsonPatchDocument<AccountInputDto> patchDoc)
        {
            var account = await _accountRepository.FindAsync(id);
            if (account is null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<AccountInputDto>(account);
            patchDoc.TryApplyTo(dto, ModelState);

            var errors = await _accountService.ValidateAccountAsync(dto, account);
            foreach (var error in errors)
            {
                ModelState.AddModelError(error.Key, error.Value);
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            account = _mapper.Map(dto, account);

            await _accountRepository.UpdateAsync(account);

            return _mapper.Map<AccountOutputDto>(account);
        }

        /// <summary>
        /// Delete account by id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountAsync([FromRoute] string id)
        {
            var deleteResult = await _accountRepository.DeleteAsync(id);

            if (deleteResult == DeleteResult.EntityNotFound)
            {
                return NotFound();
            }
            else if (deleteResult == DeleteResult.Failed)
            {
                return Problem(statusCode: 500, title: "Server error.", detail: "Failed to delete the account.");
            }

            return NoContent();
        }

        #endregion

    }
}
