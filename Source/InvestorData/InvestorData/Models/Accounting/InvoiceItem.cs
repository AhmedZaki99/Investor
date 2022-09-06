using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InvestorData
{
    public class InvoiceItem : ItemBase, IStringId
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;


        [Required]
        public string InvoiceId { get; set; } = null!;
        [MaybeNull]
        public Invoice Invoice { get; set; } = null!;

    }

}
