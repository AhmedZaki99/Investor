using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvestorData
{
    [Index(nameof(IsService), nameof(BusinessId))]
    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(Code), IsUnique = true)]
    public class Product : BusinessEntity, IUniqueName, IComparable<Product>
    {

        public bool IsService { get; set; }


        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = null!;

        [MaxLength(64)]
        public string? Code { get; set; }


        public string? CategoryId { get; set; }
        public Category? Category { get; set; }


        public string? SalesInformationId { get; set; }
        public TradingInfo? SalesInformation { get; set; }

        public string? PurchasingInformationId { get; set; }
        public TradingInfo? PurchasingInformation { get; set; }


        public string? InventoryDetailsId { get; set; }
        public InventoryInfo? InventoryDetails { get; set; }


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
