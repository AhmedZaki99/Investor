using System.ComponentModel.DataAnnotations;

namespace InvestorAPI.Core
{

    public class ProductCreateInputDto : ProductUpdateInputDto
    {

        [Required(ErrorMessage = "{0} is required and cannot be empty.")]
        public string? BusinessId { get; set; }


        [Required(ErrorMessage = "{0} is required and cannot be empty.")] // IMPORTANT: Move fixed messages to constant references.
        public bool IsService { get; set; }

    }

    public class ProductUpdateInputDto 
    {

        [Required(ErrorMessage = "{0} is required and cannot be empty.")]
        [StringLength(200, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string? Name { get; set; }

        [StringLength(60, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string? Code { get; set; }

        public string? CategoryId { get; set; }

        public TradingInfoInputDto? SalesInformation { get; set; }
        public TradingInfoInputDto? PurchasingInformation { get; set; }

        public InventoryInfoInputDto? InventoryDetails { get; set; }

    }
}
