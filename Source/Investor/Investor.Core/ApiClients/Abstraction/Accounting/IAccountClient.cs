using InvestorData;

namespace Investor.Core
{
    /// <summary>
    /// Provides an abstraction for an endpoint client service, which manages <see cref="Account"/> models.
    /// </summary>
    public interface IAccountClient : IEntityClient<Account, AccountOutputDto, AccountInputDto, AccountInputDto>
    {

        #region Read

        /// <summary>
        /// Get all entities for the given business.
        /// </summary>
        Task<IEnumerable<Account>> GetAllAsync(string businessId);

        /// <summary>
        /// Get all accounts for the given business, filtering them by type.
        /// </summary>
        Task<IEnumerable<Account>> GetAllByTypeAsync(string businessId, AccountType accountType);

        #endregion

    }
}
