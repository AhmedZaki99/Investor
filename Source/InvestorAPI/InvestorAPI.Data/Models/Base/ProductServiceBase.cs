using System.ComponentModel.DataAnnotations;

namespace InvestorAPI.Data
{
    public abstract class ProductServiceBase : DatedEntity
    {

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        public Category? Category { get; set; }
        public string? CategoryId { get; set; }


        public decimal? SalesPrice { get; set; }

        [MaxLength(1023)]
        public string? SalesDescription { get; set; }

        public Account? IncomeAccount { get; set; }
        public string? IncomeAccountId { get; set; }


        public decimal? Cost { get; set; }

        [MaxLength(1023)]
        public string? PurchaseDescription { get; set; }
        
        public Account? ExpenseAccount { get; set; }
        public string? ExpenseAccountId { get; set; }

    }
}
