using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InvestorData
{
    public class CreditPayment : PaymentBase, IStringId
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;

        [Required]
        public string BusinessId { get; set; } = null!;
        [MaybeNull]
        public Business Business { get; set; } = null!;


        [NotNull]
        public string? VendorId { get; set; } 
        public Vendor? Vendor { get; set; }


        [Required]
        public string PaymentMethodId { get; set; } = null!;
        [MaybeNull]
        public PaymentMethod PaymentMethod { get; set; } = null!;

    }
}
