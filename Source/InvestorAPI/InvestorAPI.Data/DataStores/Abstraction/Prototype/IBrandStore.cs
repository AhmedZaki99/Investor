namespace InvestorAPI.Data
{

    /// <summary>
    /// Provides an abstraction for a store which manages <see cref="Brand"/> entities.
    /// </summary>
    public interface IBrandStore : IEntityStore<Brand>
    {

        #region Pagination

        /// <summary>
        /// Returns a page of entities starting after the last entity fetched.
        /// </summary>
        /// <param name="lastEntityName">Name of the last brand fetched in previous page.</param>
        /// <param name="entitiesPerPage">Number of entities per page; default is 30.</param>
        IAsyncEnumerable<Brand> PaginateEntitiesAsync(string lastEntityName, int entitiesPerPage = 30);

        #endregion



        #region Benchmarking

        Task<List<Brand>> OldPaginateEntitiesAsync(string lastEntityId, int entitiesPerPage = 30);

        /// <summary>
        /// Create a ton of brands.
        /// </summary>
        Task<Brand> CreateATonAsync(IEnumerable<Brand> brands);

        #endregion

    }
}
