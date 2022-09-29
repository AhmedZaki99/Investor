using InvestorData;
using System.ComponentModel.DataAnnotations;

namespace InvestorAPI.Core
{
    public class AccountUpdateInputDto : IValidatableObject
    {

        #region Properties

        [Required(ErrorMessage = "{0} is required and cannot be empty.")]
        [StringLength(200, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string Name { get; set; } = null!;

        [StringLength(1000, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string? Description { get; set; }


        [EnumDataType(typeof(AccountType), ErrorMessage = "Invalid data for {0}")]
        public AccountType? AccountType { get; set; }


        public string? BusinessId { get; set; }
        public string? BusinessTypeId { get; set; }


        public bool IsSubAccount { get; protected set; }

        #endregion


        #region Constructors

        public AccountUpdateInputDto() : this(false)
        {

        }

        public AccountUpdateInputDto(bool isSubAccount)
        {
            IsSubAccount = isSubAccount;
        }

        #endregion


        #region Validation

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (BusinessId is not null && BusinessTypeId is not null)
            {
                yield return new ValidationResult("Accounts can't be assigned for both Business and BusinessType, try providing only one of them.",
                    new[] { nameof(BusinessId), nameof(BusinessTypeId) });
            }
            if (IsSubAccount)
            {
                static ValidationResult subAccountResult(string member) => new($"Sub-Accounts follow their parent's {member}, and can't be assigned to a different one.",
                    new[] { member });

                if (AccountType is not null)
                {
                    yield return subAccountResult(nameof(AccountType));
                }
                if (BusinessId is not null)
                {
                    yield return subAccountResult(nameof(BusinessId));
                }
                if (BusinessTypeId is not null)
                {
                    yield return subAccountResult(nameof(BusinessTypeId));
                }
            }
            else if (AccountType is null)
            {
                yield return new ValidationResult($"{nameof(AccountType)} is required for Accounts with no parent.", new[] { nameof(AccountType) });
            }
        }

        #endregion

    }
}
