using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorData
{
    [EntityTypeConfiguration(typeof(InvoiceItemConfiguration))]
    public class InvoiceItem : EntityBase
    {

        [Required]
        public string InvoiceId { get; set; } = null!;
        public Invoice? Invoice { get; set; }


        [Required]
        public string ProductId { get; set; } = null!;
        public Product? Product { get; set; }


        [MaxLength(1024)]
        public string? Description { get; set; }


        public double Quantity { get; set; }
        public decimal Price { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal Amount { get; set; }

    }

}
