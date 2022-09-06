namespace InvestorData
{

    /// <summary>
    /// Provides an abstraction for a repository which manages <see cref="Category"/> entities.
    /// </summary>
    public interface ICategoryRepository : IRepository<Category>
    {

        #region Read

        /// <summary>
        /// Returns all entities for a given business.
        /// </summary>
        /// <param name="businessId">Business to get entities for.</param>
        IAsyncEnumerable<Category> GetEntitiesAsync(string businessId);

        #endregion

    }
}
