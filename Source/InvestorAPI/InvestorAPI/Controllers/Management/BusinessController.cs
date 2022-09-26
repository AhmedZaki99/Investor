using InvestorAPI.Core;
using InvestorData;
using Microsoft.AspNetCore.Mvc;

namespace InvestorAPI.Controllers
{
    [Route("api/business")]
    public class BusinessController : EntityController<Business, BusinessOutputDto, BusinessCreateInputDto, BusinessUpdateInputDto>
    {

        #region Dependencies

        private readonly IBusinessService _businessService;

        #endregion

        #region Constructor

        public BusinessController(IBusinessService businessService) : base(businessService)
        {
            _businessService = businessService;
        }

        #endregion


        #region Controller Actions



        #endregion

    }
}
