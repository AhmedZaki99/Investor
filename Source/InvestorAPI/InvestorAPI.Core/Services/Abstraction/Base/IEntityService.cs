using InvestorData;
using System.Linq.Expressions;

namespace InvestorAPI.Core
{
    /// <summary>
    /// A base interface that provides an abstraction for services handling and processing <typeparamref name="TEntity"/> models data.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity to process.</typeparam>
    /// <typeparam name="TOutputDto">Type of the output data transfer object.</typeparam>
    /// <typeparam name="TCreateDto">Type of the input data transfer object used for entity creation.</typeparam>
    /// <typeparam name="TUpdateDto">Type of the input data transfer object used for entity update.</typeparam>
    public interface IEntityService<TEntity, TOutputDto, TCreateDto, TUpdateDto>
        where TEntity : class, IStringId
        where TOutputDto : class, IStringId
        where TCreateDto : class
        where TUpdateDto : class
    {

        #region Read

        /// <summary>
        /// Get all entities, based on a condition.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <remarks>
        /// This method is not preferred to use in case of big data handling.
        /// </remarks>
        /// <returns>An <see cref="IAsyncEnumerable{T}"/> of type <typeparamref name="TEntity"/> mapped to <typeparamref name="TOutputDto"/>.</returns>
        IAsyncEnumerable<TOutputDto> GetEntitiesAsync(params Expression<Func<TEntity, bool>>[] conditions);

        /// <summary>
        /// Find entity by id.
        /// </summary>
        /// <param name="id">Entity id to search for.</param>
        /// <returns>The found entity if any, mapped to a <typeparamref name="TOutputDto"/>.</returns>
        Task<TOutputDto?> FindEntityAsync(string id);

        #endregion


        #region Create

        /// <summary>
        /// Create a new entity after validating data provided by <typeparamref name="TCreateDto"/> object.
        /// </summary>
        /// <param name="dto">The object containing data to create the entity of.</param>
        /// <param name="validateDtoProperties">
        /// If <see langword="true"/>, also validate the DTO properties using their associated 
        /// <see cref="System.ComponentModel.DataAnnotations.ValidationAttribute"/> attributes.
        /// </param>
        /// <returns>
        /// An <see cref="OperationResult{TOutput}"/> wrapping the created entity data,
        /// and providing a dictionary of errors occured in the process, if any.
        /// </returns>
        Task<OperationResult<TOutputDto>> CreateEntityAsync(TCreateDto dto, bool validateDtoProperties = false);

        #endregion

        #region Update

        /// <summary>
        /// Update the underlying entity with data provided by <typeparamref name="TUpdateDto"/> object,
        /// after validating it.
        /// </summary>
        /// <param name="id">The id of the entity to update.</param>
        /// <param name="dto">The object containing data to update the underlying entity.</param>
        /// <param name="validateDtoProperties">
        /// If <see langword="true"/>, also validate the DTO properties using their associated 
        /// <see cref="System.ComponentModel.DataAnnotations.ValidationAttribute"/> attributes.
        /// </param>
        /// <returns>
        /// An <see cref="OperationResult{TOutput}"/> wrapping the updated entity data,
        /// and providing a dictionary of errors occured in the process, if any.        
        /// </returns>
        Task<OperationResult<TOutputDto>> UpdateEntityAsync(string id, TUpdateDto dto, bool validateDtoProperties = false);

        /// <summary>
        /// Update the underlying entity with the callback provided,
        /// which is applied to a <typeparamref name="TUpdateDto"/> object, and then validate the result.
        /// </summary>
        /// <param name="id">The id of the entity to update.</param>
        /// <param name="updateCallback">
        /// A callback used to update the underlying entity,
        /// which provides a <typeparamref name="TUpdateDto"/> object to make necessary changes,
        /// and returns a <see cref="bool"/> to state whether the changes were made successfully.
        /// </param>
        /// <param name="validateDtoProperties">
        /// If <see langword="true"/>, also validate the DTO properties using their associated 
        /// <see cref="System.ComponentModel.DataAnnotations.ValidationAttribute"/> attributes.
        /// </param>
        /// <returns>
        /// An <see cref="OperationResult{TOutput}"/> wrapping the updated entity data,
        /// and providing a dictionary of errors occured in the process, if any.        
        /// </returns>
        Task<OperationResult<TOutputDto>> UpdateEntityAsync(string id, Func<TUpdateDto, bool> updateCallback, bool validateDtoProperties = false);

        #endregion

        #region Delete

        /// <summary>
        /// Delete entity by id.
        /// </summary>
        /// <param name="id">The id of the entity to delete.</param>
        /// <returns>A <see cref="DeleteResult"/>.</returns>
        Task<DeleteResult> DeleteEntityAsync(string id);

        #endregion


        #region Validation

        /// <summary>
        /// Check the data given with a <typeparamref name="TCreateDto"/> for any validation errors violating constraints of a <typeparamref name="TEntity"/> model.
        /// </summary>
        /// <param name="dto">The input object to validate its data.</param>
        /// <returns>A dictionary with the set of validation errors, if any found.</returns>
        Task<Dictionary<string, string>?> ValidateCreateInputAsync(TCreateDto dto);

        /// <summary>
        /// Check the data given with a <typeparamref name="TUpdateDto"/> for any validation errors violating constraints of a <typeparamref name="TEntity"/> model,
        /// against the original state of the <typeparamref name="TEntity"/> model.
        /// </summary>
        /// <param name="dto">The input object to validate its data.</param>
        /// <param name="original">
        /// The original state of the <typeparamref name="TEntity"/> model, to check only updated properties.
        /// </param>
        /// <returns>A dictionary with the set of validation errors, if any found.</returns>
        Task<Dictionary<string, string>?> ValidateUpdateInputAsync(TUpdateDto dto, TEntity original);

        #endregion

    }
}
