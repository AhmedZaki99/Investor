namespace InvestorAPI.Core
{
    public class InventoryInfoOutputDto
    {
        public string? SKU { get; set; }

        public double Quantity { get; set; }
        public double? ReorderPoint { get; set; }

        public string InventoryAccountId { get; set; } = null!;
        public string? InventoryAccountName { get; set; }

        public string? ScaleUnitId { get; set; }
        public string? ScaleUnitName { get; set; }
    }
}
