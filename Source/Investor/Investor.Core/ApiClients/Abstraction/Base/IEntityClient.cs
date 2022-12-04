using InvestorData;

namespace Investor.Core
{

    /// <summary>
    /// Provides an abstraction for a client service, which communitcates with the api to
    /// manage generic data models.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TOutputDto">Type of the output data transfer object.</typeparam>
    /// <typeparam name="TCreateDto">Type of the input data transfer object used for entity creation.</typeparam>
    /// <typeparam name="TUpdateDto">Type of the input data transfer object used for entity update.</typeparam>
    public interface IEntityClient<TEntity, TOutputDto, TCreateDto, TUpdateDto> 
        where TEntity : EntityBase
        where TOutputDto : OutputDtoBase
        where TCreateDto : class
        where TUpdateDto : class
    {

        #region Read

        /// <summary>
        /// Get Entity by id, if exists.
        /// </summary>
        Task<TEntity?> GetAsync(string id);

        /// <summary>
        /// Get all entities.
        /// </summary>
        Task<IEnumerable<TEntity>> GetAllAsync();

        #endregion

        #region Create, Update & Delete

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <returns>The created entity as stored in server (including id).</returns>
        Task<TEntity> CreateAsync(TCreateDto dto);

        /// <summary>
        /// Save changes made to the entity in the server.
        /// </summary>
        /// <returns>The updated entity as stored in server.</returns>
        Task<TEntity> SaveChangesAsync(TEntity originalEntity, TUpdateDto dto);

        /// <summary>
        /// Delete entity by id.
        /// </summary>
        /// <returns>True if the entity has been deleted successfully, otherwise false.</returns>
        Task<bool> DeleteAsync(string id);

        #endregion

    }

}
