using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InvestorData
{
    public class Invoice : InvoiceBillBase, IStringId
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;

        [Required]
        public string BusinessId { get; set; } = null!;
        [MaybeNull]
        public Business Business { get; set; } = null!;


        [NotNull]
        public string? CustomerId { get; set; }
        public Customer? Customer { get; set; }


        public List<InvoiceItem> Items { get; set; } = new();

    }

}
