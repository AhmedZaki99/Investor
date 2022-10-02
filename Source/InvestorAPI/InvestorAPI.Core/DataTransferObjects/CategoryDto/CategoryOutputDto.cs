using InvestorData;

namespace InvestorAPI.Core
{
    public class CategoryOutputDto : IStringId
    {

        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public string BusinessId { get; set; } = null!;

    }
}
