using InvestorData;

namespace InvestorAPI.Core
{
    /// <summary>
    /// Provides an abstraction for a service responsible for handling and processing <see cref="Business"/> data.
    /// </summary>
    public interface IBusinessService
    {

        #region Data Read

        /// <summary>
        /// Get the set of available businesses.
        /// </summary>
        /// <returns>An <see cref="IAsyncEnumerable{T}"/> of businesses mapped to <see cref="BusinessOutputDto"/>.</returns>
        IAsyncEnumerable<BusinessOutputDto> GetBusinessesAsync();

        /// <summary>
        /// Find business by id.
        /// </summary>
        /// <param name="id">Business id to search for.</param>
        /// <returns>The found business if any, mapped to a <see cref="BusinessOutputDto"/>.</returns>
        Task<BusinessOutputDto?> FindBusinessAsync(string id);

        #endregion

        #region Create

        /// <summary>
        /// Create a new <see cref="Business"/> after validating data provided by <see cref="BusinessCreateInputDto"/>.
        /// </summary>
        /// <param name="dto">The object containing data to create the business of.</param>
        /// <param name="validateDtoProperties">
        /// If <see langword="true"/>, also validate the DTO properties using their associated 
        /// <see cref="System.ComponentModel.DataAnnotations.ValidationAttribute"/> attributes.
        /// </param>
        /// <returns>
        /// An <see cref="OperationResult{TOutput}"/> wrapping the created business data,
        /// and providing a dictionary of errors occured in the process, if any.
        /// </returns>
        Task<OperationResult<BusinessOutputDto>> CreateBusinessAsync(BusinessCreateInputDto dto, bool validateDtoProperties = false);

        #endregion

        #region Update

        /// <summary>
        /// Update the underlying <see cref="Business"/> with data provided by <see cref="BusinessUpdateInputDto"/>,
        /// after validating it.
        /// </summary>
        /// <param name="id">The id of the <see cref="Business"/> to update.</param>
        /// <param name="dto">The object containing data to update the underlying business.</param>
        /// <param name="validateDtoProperties">
        /// If <see langword="true"/>, also validate the DTO properties using their associated 
        /// <see cref="System.ComponentModel.DataAnnotations.ValidationAttribute"/> attributes.
        /// </param>
        /// <returns>
        /// An <see cref="OperationResult{TOutput}"/> wrapping the updated business data,
        /// and providing a dictionary of errors occured in the process, if any.        
        /// </returns>
        Task<OperationResult<BusinessOutputDto>> UpdateBusinessAsync(string id, BusinessUpdateInputDto dto, bool validateDtoProperties = false);

        /// <summary>
        /// Update the underlying <see cref="Business"/> with the callback provided,
        /// which is applied to a <see cref="BusinessUpdateInputDto"/>, and then validate the result.
        /// </summary>
        /// <param name="id">The id of the <see cref="Business"/> to update.</param>
        /// <param name="updateCallback">
        /// A callback used to update the underlying <see cref="Business"/>,
        /// which provides a <see cref="BusinessUpdateInputDto"/> to make necessary changes,
        /// and returns a <see cref="bool"/> to state whether the changes were made successfully.
        /// </param>
        /// <param name="validateDtoProperties">
        /// If <see langword="true"/>, also validate the DTO properties using their associated 
        /// <see cref="System.ComponentModel.DataAnnotations.ValidationAttribute"/> attributes.
        /// </param>
        /// <returns>
        /// An <see cref="OperationResult{TOutput}"/> wrapping the updated business data,
        /// and providing a dictionary of errors occured in the process, if any.        
        /// </returns>
        Task<OperationResult<BusinessOutputDto>> UpdateBusinessAsync(string id, Func<BusinessUpdateInputDto, bool> updateCallback, bool validateDtoProperties = false);

        #endregion

        #region Delete

        /// <summary>
        /// Delete business by id.
        /// </summary>
        /// <param name="id">The id of the <see cref="Business"/> to delete.</param>
        /// <returns>A <see cref="DeleteResult"/>.</returns>
        Task<DeleteResult> DeleteBusinessAsync(string id);

        #endregion


        #region Validation

        /// <summary>
        /// Check the data given with an Input DTO for any validation errors violating constraints of a <see cref="Business"/> model.
        /// </summary>
        /// <remarks>
        /// Consider providing the original state of the <see cref="Business"/> model if validation is made
        /// for update operation.
        /// </remarks>
        /// <param name="dto">The input object to validate its data.</param>
        /// <param name="original">
        /// The original state of the <see cref="Business"/> model, to check only updated properties.
        /// This parameter should be provided when updating the model.
        /// </param>
        /// <returns>A dictionary with the set of validation errors, if any found.</returns>
        Task<Dictionary<string, string>?> ValidateInputAsync(BusinessUpdateInputDto dto, Business? original = null);

        #endregion

    }
}
