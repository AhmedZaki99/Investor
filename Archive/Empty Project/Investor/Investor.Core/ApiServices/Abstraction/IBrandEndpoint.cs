namespace Investor.Core
{
    /// <summary>
    /// Provides an abstraction for an endpoint client service, which manages <see cref="BrandModel"/> models.
    /// </summary>
    public interface IBrandEndpoint : IModelEndpoint<BrandModel>
    {

        #region Pagination

        /// <summary>
        /// Returns a page of brands starting after the last brand fetched.
        /// </summary>
        /// <param name="lastBrandDate">Creation date of the last brand fetched in previous page.</param>
        Task<IEnumerable<BrandModel>> PaginateAsync(DateTime lastBrandDate);

        /// <summary>
        /// Returns a page of brands starting after the last brand fetched.
        /// </summary>
        /// <param name="count">The count of brands to return per page.</param>
        /// <param name="lastBrandDate">Creation date of the last brand fetched in previous page.</param>
        Task<IEnumerable<BrandModel>> PaginateAsync(int count, DateTime lastBrandDate);

        #endregion

    }
}
