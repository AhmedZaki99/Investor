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


        public string? ContactId { get; set; }
        public Contact? Contact { get; set; }

        public string? AddressId { get; set; }
        public Address? Address { get; set; }


        public List<Invoice> Invoices { get; set; } = new();
        public List<Payment> Payments { get; set; } = new();

    }


    public enum TraderType
    {
        Customer = 0,
        Vendor = 1
    }

}
