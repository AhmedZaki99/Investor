using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorAPI.Data
{
    public class Account : DatedEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AccountId { get; set; } = null!;


        [Required]
        public AccountType AccountType { get; set; }


        public Account? ParentAccount { get; set; }
        public string? ParentAccountId { get; set; }
        public bool IsSubAccount => ParentAccount != null;


        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        [MaxLength(1023)]
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
