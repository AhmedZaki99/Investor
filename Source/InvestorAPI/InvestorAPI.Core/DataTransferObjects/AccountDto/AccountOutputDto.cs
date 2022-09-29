using InvestorData;

namespace InvestorAPI.Core
{
    public class AccountOutputDto : ChildAccountOutputDto
    {

        public AccountType? AccountType { get; set; }


        public AccountScope Scope { get; set; }
        public string? BusinessId { get; set; }


        public List<ChildAccountOutputDto>? ChildAccounts { get; set; }


        public enum AccountScope
        {
            None = 0,
            SubAccount = 1,
            Local = 2,
            Global = 3,
            BusinessTypeSpecific = 4,
        }
    }

    public class ChildAccountOutputDto : IStringId
    {

        public string Id { get; set; } = null!;
        public string? ParentAccountId { get; set; }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public decimal? Balance { get; set; }

    }
}
