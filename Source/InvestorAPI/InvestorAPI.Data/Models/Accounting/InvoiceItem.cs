using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorAPI.Data
{
    public class InvoiceItem : ItemBase
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string InvoiceItemId { get; set; } = null!;


        [Required]
        public Invoice Invoice { get; set; } = null!;
        public string InvoiceId { get; set; } = null!; 

    }

}
