using InvestorData;

namespace InvestorAPI.Core
{
    /// <summary>
    /// Provides an abstraction for a service responsible for handling and processing <see cref="Account"/> models.
    /// </summary>
    public interface IAccountService : IEntityService<Account, AccountOutputDto, AccountInputDto, AccountInputDto>
    {

        #region Read

        /// <summary>
        /// Returns all entities for a given business.
        /// </summary>
        /// <param name="businessId">Business to get entities for.</param>
        IAsyncEnumerable<AccountOutputDto> GetEntitiesAsync(string businessId);

        #endregion

        #region Filter

        /// <summary>
        /// Filter accounts by type.
        /// </summary>
        /// <param name="businessId">Business to get accounts from.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <returns>Filtered accounts.</returns>
        IAsyncEnumerable<AccountOutputDto> FilterByTypeAsync(string? businessId, AccountType accountType);

        #endregion

    }
}
