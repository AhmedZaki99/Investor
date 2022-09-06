using System.Linq.Expressions;

namespace InvestorData
{

    /// <summary>
    /// Provides an abstraction for a repository which manages <see cref="Account"/> entities.
    /// </summary>
    public interface IAccountRepository : IRepository<Account>
    {

        #region Read

        /// <summary>
        /// Returns all entities for a given business.
        /// </summary>
        /// <param name="businessId">Business to get entities for.</param>
        IAsyncEnumerable<Account> GetEntitiesAsync(string businessId);

        #endregion

        #region Filter

        /// <summary>
        /// Filter accounts by type.
        /// </summary>
        /// <param name="businessId">Business to get accounts from.</param>
        /// <param name="accountType">Type of the account.</param>
        /// <returns>Filtered accounts.</returns>
        IAsyncEnumerable<Account> FilterByType(string businessId, AccountType accountType);

        /// <summary>
        /// Filter accounts by parent.
        /// </summary>
        /// <param name="businessId">Business to get products from.</param>
        /// <param name="parentId">The parent account to filter on.</param>
        /// <returns>Filterd accounts.</returns>
        IAsyncEnumerable<Account> FilterByParent(string businessId, string parentId);

        #endregion

    }
}
