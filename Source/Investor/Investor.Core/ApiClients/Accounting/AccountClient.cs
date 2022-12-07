using AutoMapper;
using InvestorData;
using Microsoft.Extensions.Options;

namespace Investor.Core
{
    /// <summary>
    /// An endpoint client service that manages <see cref="Account"/> models.
    /// </summary>
    internal class AccountClient : EntityClient<Account, AccountOutputDto, AccountInputDto, AccountInputDto>, IAccountClient
    {

        #region Constructor

        public AccountClient(HttpClient httpClient, IOptions<ApiOptions> optionsAccessor, IMapper mapper) : base(httpClient, optionsAccessor, mapper, "accounts")
        {
            
        }

        #endregion


        #region Read

        /// <inheritdoc/>
        public Task<IEnumerable<Account>> GetAllAsync(string businessId)
        {
            return GetAllInternalAsync(query: new { businessId });
        }

        /// <inheritdoc/>
        public Task<IEnumerable<Account>> GetAllByTypeAsync(string businessId, AccountType accountType)
        {
            return GetAllInternalAsync(query: new
            {
                businessId,
                accountType
            });
        }

        #endregion

    }
}
