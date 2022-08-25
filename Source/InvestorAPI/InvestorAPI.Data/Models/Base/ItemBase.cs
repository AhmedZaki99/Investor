using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorAPI.Data
{
    public class ItemBase
    {

        [Required]
        public string ProductId { get; set; } = null!;
        public Product Product { get; set; } = null!;


        [MaxLength(1023)]
        public string? Description { get; set; }


        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal Amount { get; set; }

    }

}
