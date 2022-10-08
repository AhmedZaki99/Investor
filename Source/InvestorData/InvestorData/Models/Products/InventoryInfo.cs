using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InvestorData
{
    public class InventoryInfo : EntityBase
    {

        [MaxLength(128)]
        public string? SKU { get; set; }

        public double Quantity { get; set; }
        public double? ReorderPoint { get; set; } 

        public string? ScaleUnitId { get; set; }
        public ScaleUnit? ScaleUnit { get; set; }

        [NotNull]
        public string? InventoryAccountId { get; set; }
        public Account? InventoryAccount { get; set; }

    }
}
