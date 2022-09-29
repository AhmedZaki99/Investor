using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorData
{
    public class Account : DatedEntity, IStringId
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;

        public string? BusinessId { get; set; }
        public Business? Business { get; set; }
        
        public string? BusinessTypeId { get; set; }
        public BusinessType? BusinessType { get; set; }

        public AccountType AccountType { get; set; }


        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = null!;

        [MaxLength(1024)]
        public string? Description { get; set; }


        public decimal? Balance { get; set; }

    }

    public enum AccountType
    {
        None = 0,
        AssetsAccount = 1,
        LiabilitiesAccount = 2,
        IncomeAccount = 3,
        ExpenseAccount = 4,
        EquityAccount= 5
    }

}
