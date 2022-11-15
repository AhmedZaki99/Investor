using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorData
{
    [Owned]
    [Table("InventoryInfos")]
    public class InventoryInfo
    {

        [MaxLength(128)]
        public string? SKU { get; set; }

        public double Quantity { get; set; }
        public double? ReorderPoint { get; set; } 

        public string? ScaleUnitId { get; set; }
        public ScaleUnit? ScaleUnit { get; set; }

        public string? InventoryAccountId { get; set; }
        public Account? InventoryAccount { get; set; }

    }
}
