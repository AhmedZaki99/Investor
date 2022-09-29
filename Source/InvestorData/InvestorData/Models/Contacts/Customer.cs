using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorData
{
    public class Customer : DatedEntity, IStringId
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;

        [Required]
        public string BusinessId { get; set; } = null!;
        public Business? Business { get; set; }


        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = null!;

        [MaxLength(1024)]
        public string? Notes { get; set; }


        public string? PrimaryContactId { get; set; }
        public Contact? PrimaryContact { get; set; }

        // FEATURE: Add a list of additional contacts.


        public string? BillingAddressId { get; set; }
        public Address? BillingAddress { get; set; }


        public List<Invoice> Invoices { get; set; } = new();
        public List<CustomerPayment> Payments { get; set; } = new();

    }
}
