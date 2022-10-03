using InvestorData;

namespace InvestorAPI.Core
{
    public class ProductOutputDto : IStringId
    {

        public string Id { get; set; } = null!;
        public string BusinessId { get; set; } = null!;

        public bool IsService { get; set; }

        public string Name { get; set; } = null!;
        public string? Code { get; set; }

        public string? CategoryId { get; set; }

        public TradingInfoDto? SalesInformation { get; set; }
        public TradingInfoDto? PurchasingInformation { get; set; }

        public InventoryInfoDto? InventoryDetails { get; set; }

    }
}
