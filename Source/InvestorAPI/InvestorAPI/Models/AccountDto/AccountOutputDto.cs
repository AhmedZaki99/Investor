using InvestorData;

namespace InvestorAPI.Models
{
    public class AccountOutputDto
    {

        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public AccountType AccountType { get; set; }
        public decimal? Balance { get; set; }

        public string? ParentAccountId { get; set; }

        public AccountScope Scope { get; set; }
        public string? BusinessId { get; set; }


        public enum AccountScope
        {
            None = 0,
            Local = 1,
            Global = 2,
            BusinessTypeSpecific = 3,
        }
    }
}
