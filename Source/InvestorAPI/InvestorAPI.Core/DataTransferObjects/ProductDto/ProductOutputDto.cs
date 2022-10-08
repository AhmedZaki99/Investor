namespace InvestorAPI.Core
{
    public class ProductOutputDto : OutputDtoBase
    {

        public string BusinessId { get; set; } = null!;

        public bool IsService { get; set; }

        public string Name { get; set; } = null!;
        public string? Code { get; set; }

        public string? CategoryId { get; set; }
        public string? CategoryName { get; set; }


        public TradingInfoOutputDto? SalesInformation { get; set; }
        public TradingInfoOutputDto? PurchasingInformation { get; set; }

        public InventoryInfoOutputDto? InventoryDetails { get; set; }

    }
}
