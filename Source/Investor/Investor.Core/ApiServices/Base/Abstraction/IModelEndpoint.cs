namespace Investor.Core
{

    /// <summary>
    /// Provides an abstraction for an endpoint client service, which communitcates with the api to
    /// manage generic data models, with a default entity key type of string.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface IModelEndpoint<TModel> : IModelEndpoint<TModel, string> where TModel : class
    {

    }


    /// <summary>
    /// Provides an abstraction for an endpoint client service, which communitcates with the api to
    /// manage generic data models.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TKey">The type of the model key.</typeparam>
    public interface IModelEndpoint<TModel, TKey> where TModel : class
    {

        #region Read

        /// <summary>
        /// Get Model by key, if exists.
        /// </summary>
        Task<TModel?> GetAsync(TKey key);

        #endregion

        #region Paginate

        /// <summary>
        /// Returns a page of models starting after the last model fetched.
        /// </summary>
        /// <param name="lastModel">
        ///     <para>
        ///     The last model fetched in previous page.
        ///     </para>
        ///     <para>
        ///     If not provided, the first page is returned.
        ///     </para>
        /// </param>
        Task<IEnumerable<TModel>> PaginateAsync(TModel? lastModel = null);

        /// <summary>
        /// Returns a page of models starting after the last model fetched, specifying the number of
        /// models to return per page.
        /// </summary>
        /// <param name="count">The count of models to return per page.</param>
        /// <param name="lastModel">
        ///     <para>
        ///     The last model fetched in previous page.
        ///     </para>
        ///     <para>
        ///     If not provided, the first page is returned.
        ///     </para>
        /// </param>
        Task<IEnumerable<TModel>> PaginateAsync(int count, TModel? lastModel = null);

        #endregion

        #region Create, Update & Delete

        /// <summary>
        /// Creates a new model.
        /// </summary>
        /// <returns>The created model as stored in server (including key).</returns>
        Task<TModel> CreateAsync(TModel model);

        /// <summary>
        /// Save changes made to the model in the server.
        /// </summary>
        /// <returns>True if changes were saved successfully, otherwise false.</returns>
        Task<bool> SaveChangesAsync(TModel model);

        /// <summary>
        /// Delete model by key.
        /// </summary>
        /// <returns>True if the model has been deleted successfully, otherwise false.</returns>
        Task<bool> DeleteAsync(TKey key);

        #endregion

    }

}
