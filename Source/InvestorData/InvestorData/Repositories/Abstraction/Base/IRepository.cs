using System.Linq.Expressions;

namespace InvestorData
{

    /// <summary>
    /// Provides an abstraction for a repository which manages database entities at a basic level,
    /// with a default entity key type of string.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, string> where TEntity : class, IStringId
    { 

    }

    /// <summary>
    /// Provides an abstraction for a repository which manages database entities at a basic level.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the entity key.</typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : class
    {

        #region Read

        /// <summary>
        /// Get Entity by key, if exists.
        /// </summary>
        ValueTask<TEntity?> FindAsync(TKey key);


        /// <summary>
        /// Get Entity by key, including minimal navigational data required.
        /// </summary>
        Task<TEntity?> GetMinimalDataAsync(TKey key);

        /// <summary>
        /// Get Entity by key, including full navigational data.
        /// </summary>
        Task<TEntity?> GetFullDataAsync(TKey key);


        /// <summary>
        /// Returns all entities in the database, based on a condition.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <remarks>
        /// This method is not preferred to use in case of big data handling.
        /// </remarks>
        IAsyncEnumerable<TEntity> GetEntitiesAsync(Expression<Func<TEntity, bool>>? condition = null);

        #endregion

        #region Create, Update, Delete, etc..

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <returns>The created entity as stored in the database (including key).</returns>
        Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// Update entity data.
        /// </summary>
        /// <returns>True if the update is successful, otherwise false.</returns>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// Delete entity by key.
        /// </summary>
        /// <returns>Wheather deletion was successful, failed, or entity is not found.</returns>
        Task<DeleteResult> DeleteAsync(TKey key);

        #endregion

        #region Helpers
        
        /// <summary>
        /// Determines wheather the entity exists.
        /// </summary>
        bool EntityExists(TKey key);

        #endregion

    }
}
