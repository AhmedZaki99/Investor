using InvestorData;

namespace InvestorAPI.Core
{
    /// <summary>
    /// Provides an abstraction for a service responsible for handling and processing <see cref="BusinessType"/> data.
    /// </summary>
    public interface IBusinessTypeService
    {

        #region Data Read

        /// <summary>
        /// Get the set of available business types.
        /// </summary>
        /// <returns>An <see cref="IAsyncEnumerable{T}"/> of business types mapped to <see cref="BusinessTypeOutputDto"/>.</returns>
        IAsyncEnumerable<BusinessTypeOutputDto> GetBusinessTypesAsync();

        /// <summary>
        /// Find business type by id.
        /// </summary>
        /// <param name="id">BusinessType id to search for.</param>
        /// <returns>The found business type if any, mapped to a <see cref="BusinessTypeOutputDto"/>.</returns>
        Task<BusinessTypeOutputDto?> FindBusinessTypeAsync(string id);

        #endregion

        #region Create

        /// <summary>
        /// Create a new <see cref="BusinessType"/> after validating data provided by <see cref="BusinessTypeInputDto"/>.
        /// </summary>
        /// <param name="dto">The object containing data to create the business type of.</param>
        /// <param name="validateDtoProperties">
        /// If <see langword="true"/>, also validate the DTO properties using their associated 
        /// <see cref="System.ComponentModel.DataAnnotations.ValidationAttribute"/> attributes.
        /// </param>
        /// <returns>
        /// An <see cref="OperationResult{TOutput}"/> wrapping the created business type data,
        /// and providing a dictionary of errors occured in the process, if any.
        /// </returns>
        Task<OperationResult<BusinessTypeOutputDto>> CreateBusinessTypeAsync(BusinessTypeInputDto dto, bool validateDtoProperties = false);

        #endregion

        #region Update

        /// <summary>
        /// Update the underlying <see cref="BusinessType"/> with data provided by <see cref="BusinessTypeInputDto"/>,
        /// after validating it.
        /// </summary>
        /// <param name="id">The id of the <see cref="BusinessType"/> to update.</param>
        /// <param name="dto">The object containing data to update the underlying business type.</param>
        /// <param name="validateDtoProperties">
        /// If <see langword="true"/>, also validate the DTO properties using their associated 
        /// <see cref="System.ComponentModel.DataAnnotations.ValidationAttribute"/> attributes.
        /// </param>
        /// <returns>
        /// An <see cref="OperationResult{TOutput}"/> wrapping the updated business type data,
        /// and providing a dictionary of errors occured in the process, if any.        
        /// </returns>
        Task<OperationResult<BusinessTypeOutputDto>> UpdateBusinessTypeAsync(string id, BusinessTypeInputDto dto, bool validateDtoProperties = false);

        /// <summary>
        /// Update the underlying <see cref="BusinessType"/> with the callback provided,
        /// which is applied to a <see cref="BusinessTypeInputDto"/>, and then validate the result.
        /// </summary>
        /// <param name="id">The id of the <see cref="BusinessType"/> to update.</param>
        /// <param name="updateCallback">
        /// A callback used to update the underlying <see cref="BusinessType"/>,
        /// which provides a <see cref="BusinessTypeInputDto"/> to make necessary changes,
        /// and returns a <see cref="bool"/> to state whether the changes were made successfully.
        /// </param>
        /// <param name="validateDtoProperties">
        /// If <see langword="true"/>, also validate the DTO properties using their associated 
        /// <see cref="System.ComponentModel.DataAnnotations.ValidationAttribute"/> attributes.
        /// </param>
        /// <returns>
        /// An <see cref="OperationResult{TOutput}"/> wrapping the updated business type data,
        /// and providing a dictionary of errors occured in the process, if any.        
        /// </returns>
        Task<OperationResult<BusinessTypeOutputDto>> UpdateBusinessTypeAsync(string id, Func<BusinessTypeInputDto, bool> updateCallback, bool validateDtoProperties = false);

        #endregion

        #region Delete

        /// <summary>
        /// Delete business type by id.
        /// </summary>
        /// <param name="id">The id of the business type to delete.</param>
        /// <returns>A <see cref="DeleteResult"/>.</returns>
        Task<DeleteResult> DeleteBusinessTypeAsync(string id);

        #endregion


        #region Validation

        /// <summary>
        /// Check the data given with an Input DTO for any validation errors violating constraints of a <see cref="BusinessType"/> model.
        /// </summary>
        /// <remarks>
        /// Consider providing the original state of the <see cref="BusinessType"/> model if validation is made
        /// for update operation.
        /// </remarks>
        /// <param name="dto">The input object to validate its data.</param>
        /// <param name="original">
        /// The original state of the <see cref="BusinessType"/> model, to check only updated properties.
        /// This parameter should be provided when updating the model.
        /// </param>
        /// <returns>A dictionary with the set of validation errors, if any found.</returns>
        Task<Dictionary<string, string>?> ValidateInputAsync(BusinessTypeInputDto dto, BusinessType? original = null);

        #endregion

    }
}
