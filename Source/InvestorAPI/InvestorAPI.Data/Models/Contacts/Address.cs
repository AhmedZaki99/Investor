using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorAPI.Data
{
    public class Address
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;


        [Required]
        [MaxLength(127)]
        public string Country { get; set; } = null!;

        [MaxLength(127)]
        public string? Province { get; set; }


        [MaxLength(255)]
        public string? AddressLine1 { get; set; }

        [MaxLength(255)]
        public string? AddressLine2 { get; set; }
        

        [MaxLength(127)]
        public string? City { get; set; }

        [MaxLength(63)]
        public string? PostalCode { get; set; }

    }
}
