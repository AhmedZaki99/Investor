namespace InvestorData
{
    public class TradingInfoOutputDto
    {
        public string AccountId { get; set; } = null!;
        public string? AccountName { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }
    }
}
