using System.ComponentModel.DataAnnotations;

namespace InvestorAPI.Models
{
    public class BusinessTypeInputDto
    {

        [Required(ErrorMessage = "{0} is required and cannot be empty.")]
        [StringLength(200, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string Name { get; set; } = null!;

        [StringLength(1000, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string? Description { get; set; }

        public bool DisableServices { get; set; } = false;
        public bool DisableProducts { get; set; } = false;
        public bool NoInventory { get; set; } = false;
        public bool SalesOnly { get; set; } = false;

    }
}
