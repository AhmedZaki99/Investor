using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorAPI.Data
{
    public class Customer : DatedEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;

        [Required]
        public string BusinessId { get; set; } = null!;
        public Business Business { get; set; } = null!;


        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = null!;

        [MaxLength(1024)]
        public string? Notes { get; set; }


        public Contact? PrimaryContact { get; set; }
        public string? PrimaryContactId { get; set; }

        // FEATURE: Add a list of additional contacts.


        public Address? BillingAddress { get; set; }
        public string? BillingAddressId { get; set; }


        public List<Invoice> Invoices { get; set; } = new();

    }
}
