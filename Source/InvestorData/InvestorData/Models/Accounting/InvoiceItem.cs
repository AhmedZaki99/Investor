using System.ComponentModel.DataAnnotations;

namespace InvestorData
{
    public class InvoiceItem : ItemBase
    {

        [Required]
        public string InvoiceId { get; set; } = null!;
        public Invoice? Invoice { get; set; }

    }

}
