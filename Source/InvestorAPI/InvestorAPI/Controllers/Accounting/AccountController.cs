using InvestorAPI.Core;
using InvestorData;
using Microsoft.AspNetCore.Mvc;

namespace InvestorAPI.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : EntityController<Account, AccountOutputDto, AccountCreateInputDto, AccountUpdateInputDto>
    {

        #region Dependencies

        private readonly IAccountService _accountService;

        #endregion

        #region Constructor

        public AccountController(IAccountService accountService) : base(accountService)
        {
            _accountService = accountService;
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

            var accounts = parentId is null
                           ? type is null
                           ? businessId is null
                           ? _accountService.GetEntitiesAsync()
                           : _accountService.GetEntitiesAsync(businessId)
                           : _accountService.FilterByTypeAsync(businessId, type.Value)
                           : _accountService.FilterByParentAsync(parentId);

            return Ok(accounts);
        }

        #endregion

    }
}
