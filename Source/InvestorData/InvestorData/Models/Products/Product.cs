using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvestorData
{
    [Index(nameof(IsService), nameof(BusinessId))]
    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(Code), IsUnique = true)]
    [EntityTypeConfiguration(typeof(ProductConfiguration))] // UNDONE: Use generic attribute after .net 7 migration.
    public class Product : BusinessEntity, IUniqueName
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

    }
}
