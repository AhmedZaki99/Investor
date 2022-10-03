using System.ComponentModel.DataAnnotations;

namespace InvestorAPI.Core
{

    public class TradingInfoDto
    {

        [Required(ErrorMessage = "{0} is required and cannot be empty.")]
        public string AccountId { get; set; } = null!; // Create an output dto with a flattened Account name.

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [StringLength(1000, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string? Description { get; set; }

    }


    public class InventoryInfoDto
    {

        [StringLength(100, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string? SKU { get; set; }


        [Required(ErrorMessage = "{0} is required and cannot be empty.")]
        public double Quantity { get; set; }
        public double? ReorderPoint { get; set; }


        [Required(ErrorMessage = "{0} is required and cannot be empty.")]
        public string InventoryAccountId { get; set; } = null!; 
        public string? ScaleUnitId { get; set; } // Create an output dto with a flattened scale unit name.

    }

}
