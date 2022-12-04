using AutoMapper;
using InvestorData;
using Microsoft.Extensions.Options;

namespace Investor.Core
{
    /// <summary>
    /// An endpoint client service that manages <see cref="Business"/> models.
    /// </summary>
    internal class BusinessClient : EntityClient<Business, BusinessOutputDto, BusinessCreateInputDto, BusinessUpdateInputDto>, IBusinessClient
    {

        #region Constructor

        public BusinessClient(HttpClient httpClient, IOptions<ApiOptions> optionsAccessor, IMapper mapper) : base(httpClient, optionsAccessor, mapper, "business")
        {
            
        }

        #endregion

    }
}
