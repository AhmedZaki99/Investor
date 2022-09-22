namespace InvestorAPI.Core
{
    public class BusinessTypeOutputDto
    {

        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public bool DisableServices { get; set; }
        public bool DisableProducts { get; set; }
        public bool NoInventory { get; set; }
        public bool SalesOnly { get; set; }

    }
}
