using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorAPI.Data
{
    public class Product : ProductServiceBase
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ProductId { get; set; } = null!;


        [MaxLength(255)]
        public string? SKU { get; set; }


        [Required]
        public int Quantity { get; set; }

        public int? ReorderPoint { get; set; }

        public Account? InventoryAccount { get; set; }
        public string? InventoryAccountId { get; set; }

    }
}
