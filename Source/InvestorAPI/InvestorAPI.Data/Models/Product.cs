using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorAPI.Data
{
    public class Product : DatedEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ProductId { get; set; } = null!;


        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        public Category? Category { get; set; }
        public string? CategoryId { get; set; }


        [Required]
        public int Quantity { get; set; }

        public int? ReorderPoint { get; set; }

        [Required]
        public Account InventoryAccount { get; set; } = null!;
        public string InventoryAccountId { get; set; } = null!;


        public decimal? SalesPrice { get; set; }

        [MaxLength(1023)]
        public string? SalesDescription { get; set; }

        [Required]
        public Account IncomeAccount { get; set; } = null!;
        public string IncomeAccountId { get; set; } = null!;


        public decimal? Cost { get; set; }

        [MaxLength(1023)]
        public string? PurchaseDescription { get; set; }

        [Required]
        public Account ExpenseAccount { get; set; } = null!;
        public string ExpenseAccountId { get; set; } = null!;

    }
}
