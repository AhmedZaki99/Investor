using AutoMapper;
using InvestorData;
using Microsoft.Extensions.Options;

namespace Investor.Core
{
    /// <summary>
    /// An endpoint client service that manages <see cref="BusinessType"/> models.
    /// </summary>
    internal class BusinessTypeClient : EntityClient<BusinessType, BusinessTypeOutputDto, BusinessTypeInputDto, BusinessTypeInputDto>, IBusinessTypeClient
    {

        #region Constructor

        public BusinessTypeClient(HttpClient httpClient, IOptions<ApiOptions> optionsAccessor, IMapper mapper) : base(httpClient, optionsAccessor, mapper, "business/types")
        {
            
        }

        #endregion
    }
}
