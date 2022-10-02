using System.ComponentModel.DataAnnotations;

namespace InvestorAPI.Core
{

    public class CategortCreateInputDto : CategoryUpdateInputDto
    {

        public string? BusinessId { get; set; }

    }


    public class CategoryUpdateInputDto
    {

        [Required(ErrorMessage = "{0} is required and cannot be empty.")]
        [StringLength(200, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string? Name { get; set; }

        [StringLength(1000, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string? Description { get; set; }

    }

}
