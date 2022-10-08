using System.ComponentModel.DataAnnotations;

namespace InvestorAPI.Core
{
    public class TradingInfoInputDto
    {

        [Required(ErrorMessage = "{0} is required and cannot be empty.")]
        public string AccountId { get; set; } = null!;

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [StringLength(1000, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string? Description { get; set; }

    }
}
