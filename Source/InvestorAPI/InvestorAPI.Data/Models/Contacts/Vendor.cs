using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorAPI.Data
{
    public class Vendor : DatedEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;

        [Required]
        public string BusinessId { get; set; } = null!;
        public Business Business { get; set; } = null!;


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
