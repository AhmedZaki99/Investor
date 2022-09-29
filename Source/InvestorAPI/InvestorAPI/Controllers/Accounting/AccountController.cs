using InvestorAPI.Core;
using InvestorData;
using Microsoft.AspNetCore.Mvc;

namespace InvestorAPI.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : EntityController<Account, AccountOutputDto, AccountInputDto, AccountInputDto>
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
        /// Get the set of accounts related to the specified business or type.
        /// </summary>
        [HttpGet]
        public IActionResult GetAccountsAsync([FromQuery] string? businessId = null, [FromQuery] AccountType? type = null)
        {
            var accounts = type is null
                           ? businessId is null
                           ? _accountService.GetEntitiesAsync()
                           : _accountService.GetEntitiesAsync(businessId)
                           : _accountService.FilterByTypeAsync(businessId, type.Value);

            return Ok(accounts);
        }

        #endregion

    }
}
