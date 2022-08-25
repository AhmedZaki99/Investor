using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InvestorAPI.Data
{
    public class Business : DatedEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;


        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = null!;


        [NotNull]
        public BusinessType? BusinessType { get; set; }
        public string? BusinessTypeId { get; set; }


        [MaxLength(32)]
        public string? Country { get; set; }

        [MaxLength(32)]
        public string? Currency { get; set; }

        // TODO: Add support for pre-defined countries and currencies. (using enums or database entities).


        #region Navigation Properties

        public List<Account> Accounts { get; set; } = new();

        public List<ScaleUnit> ScaleUnits { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<Product> Products { get; set; } = new();

        public List<Customer> Customers { get; set; } = new();
        public List<Vendor> Vendors { get; set; } = new();

        public List<Invoice> Invoices { get; set; } = new();
        public List<Bill> Bills { get; set; } = new();

        #endregion

    }
}
