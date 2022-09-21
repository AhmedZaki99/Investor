using AutoMapper;
using InvestorAPI.Models;
using InvestorData;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Principal;

namespace InvestorAPI.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {

        #region Dependencies

        private readonly IAccountRepository _accountRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly IBusinessTypeRepository _businessTypeRepository;

        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public AccountController(IAccountRepository accountRepository,
                                 IBusinessRepository businessRepository,
                                 IBusinessTypeRepository businessTypeRepository,
                                 IMapper mapper)
        {
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
            // TODO: Try adding services dedicated for validation.

            if (!await ValidateAccountAsync(accountDto))
            {
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
            // IMPORTANT: Add validation for updated model.

            var account = await _accountRepository.FindAsync(id);
            if (account is null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<AccountInputDto>(account);
            patchDoc.TryApplyTo(dto, ModelState);

            if (!await ValidateAccountAsync(dto, account))
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

        #region Helper Methods

        private async Task<bool> ValidateAccountAsync(AccountInputDto dto, Account? original = null)
        {
            if (dto.BusinessId != original?.BusinessId && !ValidateId(_businessRepository, dto.BusinessId))
            {
                return false;
            }
            if (dto.BusinessTypeId != original?.BusinessTypeId && !ValidateId(_businessTypeRepository, dto.BusinessTypeId))
            {
                return false;
            }

            if (dto.BusinessId is not null && dto.BusinessTypeId is not null)
            {
                ModelState.AddModelError("BusinessId / BusinessTypeId", "Accounts can't be assigned for both Business and BusinessType, try providing only one of them.");
                return false;
            }
            if (dto.ParentAccountId != original?.ParentAccountId && dto.ParentAccountId is not null)
            {
                var parentAccount = await _accountRepository.GetMinimalDataAsync(dto.ParentAccountId);

                string? errorMsg = parentAccount is not null ?
                                   parentAccount.IsSubAccount ?
                                   "The account provided has a parent account on its own, thus it can't be assigned as a parent account." : null :
                                   "There's no Account found with the Id provided.";
                if (errorMsg is not null)
                {
                    ModelState.AddModelError(nameof(dto.ParentAccountId), errorMsg);
                    return false;
                }
            }

            return true;
        }

        private bool ValidateId<T>(IRepository<T> repository, string? id) where T : class, IStringId
        {
            if (id is not null && !repository.EntityExists(id))
            {
                ModelState.AddModelError($"{typeof(T).Name}Id", $"There's no {typeof(T).Name} found with the Id provided.");
                return false;
            }
            return true;
        }
        #endregion

    }
}
