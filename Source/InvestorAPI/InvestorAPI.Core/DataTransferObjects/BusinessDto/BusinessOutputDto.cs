using InvestorData;

namespace InvestorAPI.Core
{
    public class BusinessOutputDto : OutputDtoBase
    {

        public string Name { get; set; } = null!;

        public string? BusinessTypeId { get; set; }
        public BusinessTypeOutputDto? BusinessType { get; set; } // TODO: Consider flattening the businessType data.

        public string? Country { get; set; }
        public string? Currency { get; set; }

    }
}
