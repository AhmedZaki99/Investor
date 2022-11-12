using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvestorData
{
    [Index(nameof(Name), IsUnique = true)]
    public class Business : DatedEntity, IUniqueName
    {

        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = null!;


        // IMPORTANT: Study more proper designs for data structure with required relations that shouldn't cascade on delete.

        public string? BusinessTypeId { get; set; }
        public BusinessType? BusinessType { get; set; }


        [MaxLength(32)]
        public string? Country { get; set; }

        [MaxLength(32)]
        public string? Currency { get; set; }

        // TODO: Add support for pre-defined countries and currencies. (using enums or database entities).


        #region Navigation Properties

        // TODO: Study the difference bet. assigning nav. properties as Lists vs IEnumerables..

        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public List<Account> Accounts { get; set; } = new();


        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public List<ScaleUnit> ScaleUnits { get; set; } = new();

        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public List<Category> Categories { get; set; } = new();

        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public List<Product> Products { get; set; } = new();


        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public List<Trader> Traders { get; set; } = new();

        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public List<Invoice> Invoices { get; set; } = new();

        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public List<Payment> Payments { get; set; } = new();

        #endregion

    }
}
