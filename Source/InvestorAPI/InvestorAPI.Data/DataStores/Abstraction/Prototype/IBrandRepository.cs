using InvestorData;

namespace InvestorAPI.Data
{

    /// <summary>
    /// Provides an abstraction for a repository which manages <see cref="Brand"/> entities.
    /// </summary>
    public interface IBrandRepository : IRepository<Brand>
    {

        #region Pagination

        /// <summary>
        /// Returns a page of brands starting after the last brand fetched.
        /// </summary>
        /// <param name="lastBrandName">
        ///     <para>
        ///     Name of the last brand fetched in previous page.</param>
        ///     </para>
        ///     <para>
        ///     If not provided, the first page is returned.
        ///     </para>
        /// <param name="brandsPerPage">Number of brands per page; default is 30.</param>
        IAsyncEnumerable<Brand> PaginateBrandsAsync(string? lastBrandName = null, int brandsPerPage = 30);

        #endregion

    }
}
