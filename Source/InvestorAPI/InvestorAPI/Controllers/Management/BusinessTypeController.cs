using InvestorAPI.Core;
using InvestorData;
using Microsoft.AspNetCore.Mvc;

namespace InvestorAPI.Controllers
{
    [Route("api/business/types")]
    public class BusinessTypeController : EntityController<BusinessType, BusinessTypeOutputDto, BusinessTypeInputDto, BusinessTypeInputDto>
    {

        #region Dependencies

        private readonly IBusinessTypeService _businessTypeService;

        #endregion

        #region Constructor

        public BusinessTypeController(IBusinessTypeService businessTypeService) : base(businessTypeService)
        {
            _businessTypeService = businessTypeService;
        }

        #endregion

        #region Controller Actions



        #endregion

    }
}
