using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvestorData
{
    [Index(nameof(Name), IsUnique = true)]
    public class Business : DatedEntity, IUniqueName
    {

        [Required]
        public string AppUserId { get; set; } = null!;


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

        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public ICollection<Account> Accounts { get; set; } = null!;


        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public ICollection<ScaleUnit> ScaleUnits { get; set; } = null!;

        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public ICollection<Category> Categories { get; set; } = null!;

        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public ICollection<Product> Products { get; set; } = null!;


        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public ICollection<Trader> Traders { get; set; } = null!;

        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public ICollection<Invoice> Invoices { get; set; } = null!;

        [DeleteBehavior(DeleteBehavior.ClientCascade)]
        public ICollection<Payment> Payments { get; set; } = null!;

        #endregion

    }
}
