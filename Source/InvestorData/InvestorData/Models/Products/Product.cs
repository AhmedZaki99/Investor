using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InvestorData
{
    [Index(nameof(IsService))]
    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(Code), IsUnique = true)]
    public class Product : BusinessEntity, IUniqueName, IComparable<Product>
    {

        #region Common Data

        [Required]
        public bool IsService { get; set; }


        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = null!;

        [MaxLength(64)]
        public string? Code { get; set; }


        public string? CategoryId { get; set; }
        public Category? Category { get; set; }


        public decimal? SalesPrice { get; set; }

        [MaxLength(1024)]
        public string? SalesDescription { get; set; }

        public string? IncomeAccountId { get; set; }
        public Account? IncomeAccount { get; set; }


        public decimal? Cost { get; set; }

        [MaxLength(1024)]
        public string? PurchaseDescription { get; set; }

        public string? ExpenseAccountId { get; set; }
        public Account? ExpenseAccount { get; set; }

        #endregion

        #region Non-Service Data

        [MaxLength(128)]
        public string? SKU { get; set; }


        public double? Quantity { get; set; }

        public string? ScaleUnitId { get; set; }
        public ScaleUnit? ScaleUnit { get; set; }

        public int? ReorderPoint { get; set; }

        [NotNull]
        public string? InventoryAccountId { get; set; }
        public Account? InventoryAccount { get; set; }

        #endregion


        #region Coparision

        public int CompareTo(Product? other)
        {
            if (other is null)
            {
                return 1;
            }
            int compareByCategory = Category?.CompareTo(other.Category) ?? (other.Category is null ? 0 : -1);
            if (compareByCategory == 0)
            {
                return Name.CompareTo(other.Name);
            }
            return compareByCategory;
        } 
        
        public int PlainCompareTo(Product? other)
        {
            if (other == null)
            {
                return 1;
            }

            int compareByCategory;
            if (Category == null)
            {
                if (other.Category != null)
                {
                    return -1;
                }
                compareByCategory = 0;
            }
            else
            {
                compareByCategory = Category.CompareTo(other.Category);
            }

            if (compareByCategory == 0)
            {
                return Name.CompareTo(other.Name);
            }
            return compareByCategory;
        }

        #endregion

    }
}
