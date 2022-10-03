using InvestorData;

namespace InvestorAPI.Core
{
    public class AccountOutputDto : OutputDtoBase
    {

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public AccountScope AccountScope { get; set; }
        public AccountType AccountType { get; set; }

        public decimal? Balance { get; set; }
        
        public string? BusinessId { get; set; }

    }

}
