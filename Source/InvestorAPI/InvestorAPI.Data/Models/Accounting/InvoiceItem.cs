using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorAPI.Data
{
    public class InvoiceItem : ItemBase
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;


        [Required]
        public string InvoiceId { get; set; } = null!; 
        public Invoice Invoice { get; set; } = null!;

    }

}
