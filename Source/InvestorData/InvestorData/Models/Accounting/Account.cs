using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorData
{
    public class Account : DatedEntity, IStringId
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;

        public Business? Business { get; set; }
        public string? BusinessId { get; set; }
        

        public BusinessType? BusinessType { get; set; }
        public string? BusinessTypeId { get; set; }


        [Required]
        public AccountType AccountType { get; set; }


        public Account? ParentAccount { get; set; }
        public string? ParentAccountId { get; set; }
        public bool IsSubAccount => ParentAccount != null;


        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = null!;

        [MaxLength(1024)]
        public string? Description { get; set; }


        public decimal? Balance { get; set; }

    }

    public enum AccountType
    {
        AssetsAccount,
        LiabilitiesAccount,
        IncomeAccount,
        ExpenseAccount,
        EquityAccount
    }

}
