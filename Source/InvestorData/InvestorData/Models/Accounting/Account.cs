using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorData
{
    [Index(nameof(Name), IsUnique = true)]
    [EntityTypeConfiguration(typeof(AccountConfiguration))] // UNDONE: Use generic attribute after .net 7 migration.
    public class Account : OptionalBusinessEntity, IUniqueName
    {
        
        public string? BusinessTypeId { get; set; }
        public BusinessType? BusinessType { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public AccountScope AccountScope { get; set; }

        public AccountType AccountType { get; set; }


        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = null!;

        [MaxLength(1024)]
        public string? Description { get; set; }


        public decimal? Balance { get; set; }

    }

    public enum AccountScope
    {
        None = 0,
        Local = 1,
        Global = 2,
        BusinessTypeSpecific = 3,
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
