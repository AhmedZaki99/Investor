using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InvestorData
{
    [Index(nameof(Name), IsUnique = true)]
    [EntityTypeConfiguration(typeof(BusinessConfiguration))]
    public class Business : DatedEntity, IUniqueName
    {

        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = null!;


        [NotNull]
        public string? BusinessTypeId { get; set; }
        public BusinessType? BusinessType { get; set; }


        [MaxLength(32)]
        public string? Country { get; set; }

        [MaxLength(32)]
        public string? Currency { get; set; }

        // TODO: Add support for pre-defined countries and currencies. (using enums or database entities).


        #region Navigation Properties

        // TODO: Study the difference bet. assigning nav. properties as Lists vs IEnumerables..

        public List<Account> Accounts { get; set; } = new();

        public List<ScaleUnit> ScaleUnits { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<Product> Products { get; set; } = new();

        public List<Trader> Traders { get; set; } = new();
        public List<Invoice> Invoices { get; set; } = new();
        public List<Payment> Payments { get; set; } = new();

        #endregion

    }
}
