using InvestorData;
using System.ComponentModel.DataAnnotations;

namespace InvestorAPI.Core
{
    public class AccountInputDto : IValidatableObject
    {

        #region Properties

        [Required(ErrorMessage = "{0} is required and cannot be empty.")]
        [StringLength(200, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string Name { get; set; } = null!;

        [StringLength(1000, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string? Description { get; set; }


        [Required(ErrorMessage = "{0} is required and should be provided.")]
        [EnumDataType(typeof(AccountType), ErrorMessage = "Invalid data for {0}")]
        public AccountType AccountType { get; set; }
        public decimal? Balance { get; set; }

        public string? BusinessId { get; set; }
        public string? BusinessTypeId { get; set; }

        #endregion

        #region Validation

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (BusinessId is not null && BusinessTypeId is not null)
            {
                yield return new ValidationResult("Accounts can't be assigned for both Business and BusinessType, try providing only one of them.",
                    new[] { nameof(BusinessId), nameof(BusinessTypeId) });
            }
        }

        #endregion

    }
}
