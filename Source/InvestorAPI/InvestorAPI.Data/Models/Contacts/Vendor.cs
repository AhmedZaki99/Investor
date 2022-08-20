using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorAPI.Data
{
    public class Vendor : DatedEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string VendorId { get; set; } = null!;


        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        [MaxLength(1023)]
        public string? Notes { get; set; }


        public Contact? Contact { get; set; }
        public string? ContactId { get; set; }

        public Address? Address { get; set; }
        public string? AddressId { get; set; }


        public List<Bill> Bills { get; set; } = new();

    }
}
