using InvestorData;

namespace InvestorAPI.Core
{
    /// <summary>
    /// Provides an abstraction for a service responsible for handling and processing <see cref="Account"/> models.
    /// </summary>
    public interface IAccountService
    {

        #region Validation

        /// <summary>
        /// Check the data given in the <see cref="AccountInputDto"/> for any validation errors
        /// violating constraints of an <see cref="Account"/> model.
        /// </summary>
        /// <param name="dto">The <see cref="AccountInputDto"/> to validate its data.</param>
        /// <param name="original">The original state of the <see cref="Account"/> model, to check only updated properties.</param>
        /// <returns>A dictionary with the set of validation errors, if any found.</returns>
        Task<IDictionary<string, string>> ValidateAccountAsync(AccountInputDto dto, Account? original = null);

        #endregion

    }
}
