using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorAPI.Data
{
    public class Customer : DatedEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CustomerId { get; set; } = null!;

        [Required]
        public string BusinessId { get; set; } = null!;
        public Business Business { get; set; } = null!;


        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        [MaxLength(1023)]
        public string? Notes { get; set; }


        public Contact? PrimaryContact { get; set; }
        public string? PrimaryContactId { get; set; }

        // FEATURE: Add a list of additional contacts.


        public Address? BillingAddress { get; set; }
        public string? BillingAddressId { get; set; }


        public List<Invoice> Invoices { get; set; } = new();

    }
}
