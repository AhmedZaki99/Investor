using System.ComponentModel.DataAnnotations;

namespace InvestorData
{
    public class Address : EntityBase
    {

        [Required]
        [MaxLength(64)]
        public string Country { get; set; } = null!;

        [MaxLength(64)]
        public string? Province { get; set; }


        [MaxLength(256)]
        public string? AddressLine1 { get; set; }

        [MaxLength(256)]
        public string? AddressLine2 { get; set; }
        

        [MaxLength(64)]
        public string? City { get; set; }

        [MaxLength(32)]
        public string? PostalCode { get; set; }

    }
}
