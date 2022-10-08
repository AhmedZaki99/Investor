namespace InvestorAPI.Core
{
    public class CategoryOutputDto : OutputDtoBase
    {

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public string BusinessId { get; set; } = null!;

    }
}
