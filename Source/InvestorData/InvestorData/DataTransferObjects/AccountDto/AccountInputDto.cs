using System.ComponentModel.DataAnnotations;

namespace InvestorData
{
    public class AccountInputDto
    {

        [Required(ErrorMessage = "{0} is required and cannot be empty.")]
        [StringLength(200, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string? Name { get; set; }

        [StringLength(1000, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string? Description { get; set; }


        [Required(ErrorMessage = "{0} is required and should be provided.")]
        [EnumDataType(typeof(AccountType), ErrorMessage = "Invalid data for {0}")]
        public AccountType? AccountType { get; set; }
        public decimal? Balance { get; set; }


        [AllowOne(nameof(BusinessId), ErrorMessage = "Accounts can't be assigned for both Business and BusinessType, try providing only one of them.")]
        public string? BusinessId { get; set; }

        [AllowOne(nameof(BusinessId), ErrorMessage = "Accounts can't be assigned for both Business and BusinessType, try providing only one of them.")]
        public string? BusinessTypeId { get; set; }

    }
}
