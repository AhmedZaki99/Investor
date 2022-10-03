using System.ComponentModel.DataAnnotations;

namespace InvestorData
{
    public abstract class CustomerVendorBase : BusinessEntity, IUniqueName
    {

        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = null!;

        [MaxLength(1024)]
        public string? Notes { get; set; }


        public string? ContactId { get; set; }
        public Contact? Contact { get; set; }

        public string? AddressId { get; set; }
        public Address? Address { get; set; }

    }
}
