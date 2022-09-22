using System.ComponentModel.DataAnnotations;

namespace InvestorAPI.Core
{
    public class BusinessUpdateInputDto
    {

        [Required(ErrorMessage = "{0} is required and cannot be empty.")]
        [StringLength(200, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string Name { get; set; } = null!;


        [StringLength(30, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string? Country { get; set; }

        [StringLength(30, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string? Currency { get; set; }

    }
}
