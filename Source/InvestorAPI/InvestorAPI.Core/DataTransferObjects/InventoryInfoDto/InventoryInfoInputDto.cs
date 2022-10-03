using System.ComponentModel.DataAnnotations;

namespace InvestorAPI.Core
{
    public class InventoryInfoInputDto
    {

        [StringLength(100, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string? SKU { get; set; }


        [Required(ErrorMessage = "{0} is required and cannot be empty.")]
        public double Quantity { get; set; }
        public double? ReorderPoint { get; set; }


        [Required(ErrorMessage = "{0} is required and cannot be empty.")]
        public string InventoryAccountId { get; set; } = null!; 
        public string? ScaleUnitId { get; set; }

    }
}
