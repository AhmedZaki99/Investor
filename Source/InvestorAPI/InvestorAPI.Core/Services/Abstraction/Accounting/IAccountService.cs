using InvestorData;

namespace InvestorAPI.Core
{
    /// <summary>
    /// Provides an abstraction for a service responsible for handling and processing <see cref="Account"/> models.
    /// </summary>
    public interface IAccountService : IBusinessEntityService<Account, AccountOutputDto, AccountInputDto, AccountInputDto>
    {

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
