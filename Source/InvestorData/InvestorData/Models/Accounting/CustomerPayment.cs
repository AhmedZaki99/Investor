using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InvestorData
{
    public class CustomerPayment : PaymentBase, IStringId
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;

        [Required]
        public string BusinessId { get; set; } = null!;
        public Business? Business { get; set; }


        [NotNull]
        public string? CustomerId { get; set; }
        public Customer? Customer { get; set; } 


        [Required]
        public string PaymentMethodId { get; set; } = null!;
        public PaymentMethod? PaymentMethod { get; set; }

    }
}
