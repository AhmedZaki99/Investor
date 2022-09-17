using InvestorData;
using System.ComponentModel.DataAnnotations;

namespace InvestorAPI.Models
{
    public class BusinessInputDTO
    {

        #region Public Properties

        [Required(ErrorMessage = "{0} is required to create a new business.")]
        [StringLength(200, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string? Name { get; set; }


        public string? BusinessTypeId { get; set; }


        [StringLength(30, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string? Country { get; set; }

        [StringLength(30, ErrorMessage = "{0} should not be more than {1} characters.")]
        public string? Currency { get; set; }

        #endregion

        #region Helper Methods

        public Business Map() => new()
        {
            Name = Name ?? throw new NullReferenceException("Business name must be provided."),
            BusinessTypeId = BusinessTypeId,
            Country = Country,
            Currency = Currency
        };

        #endregion
    }
}
