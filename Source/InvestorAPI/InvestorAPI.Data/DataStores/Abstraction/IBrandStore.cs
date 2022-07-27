namespace InvestorAPI.Data
{

    /// <summary>
    /// Provides an abstraction for a store which manages <see cref="Brand"/> entities.
    /// </summary>
    public interface IBrandStore : IEntityStore<Brand>
    {

        #region Pagination

        /// <summary>
        /// Returns a page of entities starting from the last entity fetched.
        /// </summary>
        /// <param name="lastEntityDate">Creation date of the last brand fetched in previous page.</param>
        /// <param name="entitiesPerPage">Number of entities per page; default is 30.</param>
        Task<List<Brand>> PaginateEntitiesAsync(DateTime lastEntityDate, int entitiesPerPage = 30);

        #endregion

    }
}
