using System.ComponentModel.DataAnnotations;

namespace InvestorAPI.Core
{

    public class ProductCreateInputDto : ProductUpdateInputDto
    {

        [Required(ErrorMessage = "{0} is required and cannot be empty.")] // IMPORTANT: Move fixed messages to constant references.
        public string? BusinessId { get; set; }

        public bool IsService { get; set; } = false;

    }

    public class ProductUpdateInputDto 
    {

        [Required(ErrorMessage = "{0} is required and cannot be empty.")]
        [StringLength(200, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string? Name { get; set; }

        [StringLength(60, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string? Code { get; set; }


        [AllowOne(nameof(Category), ErrorMessage = "Category object and id shouldn't be provided together, either provide one or none.")]
        public string? CategoryId { get; set; }

        [AllowOne(nameof(CategoryId), ErrorMessage = "Category object and id shouldn't be provided together, either provide one or none.")]
        public CategoryUpdateInputDto? Category { get; set; }


        public TradingInfoInputDto? SalesInformation { get; set; }
        public TradingInfoInputDto? PurchasingInformation { get; set; }

        public InventoryInfoInputDto? InventoryDetails { get; set; }

    }
}
