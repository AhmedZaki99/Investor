using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvestorData
{
    [Index(nameof(TraderType), nameof(BusinessId))]
    [Index(nameof(Name), IsUnique = true)]
    public class Trader : BusinessEntity, IUniqueName
    {

        public TraderType TraderType { get; set; }


        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = null!;

        [MaxLength(1024)]
        public string? Notes { get; set; }


        public Contact? Contact { get; set; }
        public Address? Address { get; set; }


        public ICollection<Invoice> Invoices { get; set; } = null!;
        public ICollection<Payment> Payments { get; set; } = null!;

    }


    public enum TraderType
    {
        Customer = 0,
        Vendor = 1
    }

}
