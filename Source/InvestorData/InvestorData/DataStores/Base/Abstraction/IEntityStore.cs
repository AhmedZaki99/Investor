namespace InvestorData
{

    /// <summary>
    /// Provides an abstraction for a store which manages database entities at a basic level,
    /// with a default entity key type of string.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IEntityStore<TEntity> : IEntityStore<TEntity, string> where TEntity : class
    { 

    }

    /// <summary>
    /// Provides an abstraction for a store which manages database entities at a basic level.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the entity key.</typeparam>
    public interface IEntityStore<TEntity, TKey> where TEntity : class
    {

        #region Read

        /// <summary>
        /// Get Entity by key, if exists.
        /// </summary>
        ValueTask<TEntity?> FindAsync(TKey key);

        /// <summary>
        /// Get Entity by key, including all relational data; if any exists.
        /// </summary>
        /// <param name="key">Entity key</param>
        /// <param name="track">True if the returned entity should be tracked for changes, otherwise false; default is false.</param>
        Task<TEntity?> FindAndNavigateAsync(TKey key, bool track = false);


        /// <summary>
        /// Returns a list of entities by page and count.
        /// </summary>
        /// <param name="page">The page to return.</param>
        /// <param name="entitiesPerPage">Number of entities per page; default is 30.</param>
        /// <remarks>
        ///     <para>
        ///     This method is not preferred to use outside the scope of random access pagination,
        ///     since it may be less efficient approach than <see cref="PaginateEntitiesAsync(TEntity?, int)"/>
        ///     for next/previous pagination.
        ///     </para>
        ///     <para>
        ///     And to keep up with a better performance, carefully consider if random access pagination
        ///     really is required for your use case, or if next/previous pagination is enough.
        ///     </para>
        /// </remarks>
        IAsyncEnumerable<TEntity> ListEntitiesAsync(int page = 1, int entitiesPerPage = 30);

        /// <summary>
        /// Returns a page of entities starting after the last entity fetched.
        /// </summary>
        /// <param name="lastEntity">
        ///     <para>
        ///     The last entity fetched in previous page.
        ///     </para>
        ///     <para>
        ///     If not provided, the first page is returned.
        ///     </para>
        /// </param>
        /// <param name="entitiesPerPage">Number of entities per page; default is 30.</param>
        IAsyncEnumerable<TEntity> PaginateEntitiesAsync(TEntity? lastEntity = null, int entitiesPerPage = 30);

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



        #region Benchmarking

        Task<List<TEntity>> OldPaginateEntitiesAsync(TEntity? lastEntity = null, int entitiesPerPage = 30);

        #endregion

    }
}
