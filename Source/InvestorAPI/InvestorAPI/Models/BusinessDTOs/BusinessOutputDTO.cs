namespace InvestorAPI.Models
{
    public class BusinessOutputDTO
    {

        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? BusinessTypeId { get; set; }
        public BusinessTypeOutputDTO? BusinessType { get; set; }

        public string? Country { get; set; }
        public string? Currency { get; set; }

    }
}
