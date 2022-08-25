using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InvestorAPI.Data
{
    public class Product : DatedEntity
    {

        #region Common Data

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ProductId { get; set; } = null!;

        [Required]
        public string BusinessId { get; set; } = null!;
        public Business Business { get; set; } = null!;


        [Required]
        public bool IsService { get; set; }


        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        [MaxLength(63)]
        public string? Code { get; set; }


        public Category? Category { get; set; }
        public string? CategoryId { get; set; }


        public decimal? SalesPrice { get; set; }

        [MaxLength(1023)]
        public string? SalesDescription { get; set; }

        [NotNull]
        public Account? IncomeAccount { get; set; }
        public string? IncomeAccountId { get; set; }


        public decimal? Cost { get; set; }

        [MaxLength(1023)]
        public string? PurchaseDescription { get; set; }

        [NotNull]
        public Account? ExpenseAccount { get; set; }
        public string? ExpenseAccountId { get; set; }

        #endregion

        #region Non-Service Data

        [MaxLength(255)]
        public string? SKU { get; set; }


        public double? Quantity { get; set; }

        public ScaleUnit? ScaleUnit { get; set; }
        public string? ScaleUnitId { get; set; }

        public int? ReorderPoint { get; set; }

        [NotNull]
        public Account? InventoryAccount { get; set; }
        public string? InventoryAccountId { get; set; } 

        #endregion

    }
}
