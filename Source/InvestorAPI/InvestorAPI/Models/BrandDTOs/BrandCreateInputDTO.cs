using InvestorAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace InvestorAPI.Models
{
    public class BrandCreateInputDTO
    {

        #region Public Properties

        [Required(ErrorMessage = "Brand name is required to create a new brand.")]
        [StringLength(255, ErrorMessage = "Brand name should not be more than 255 characters.")]
        public string? Name { get; set; }

        [StringLength(255, ErrorMessage = "Scale unit should not be more than 255 characters.")]
        public string? ScaleUnit { get; set; }


        [DataType(DataType.MultilineText)]
        [StringLength(1023, ErrorMessage = "The maximum of 1023 characters has been exceeded for the description.")]
        public string? Description { get; set; }


        [DataType(DataType.Currency)]
        public decimal? BuyPrice { get; set; }

        [DataType(DataType.Currency)]
        public decimal? SellPrice { get; set; }

        #endregion

        #region Helper Methods

        public Brand Map()
        {
            Brand instance = new()
            {
                Name = Name ?? throw new NullReferenceException("Brand name must be provided."),
                Description = Description,
                BuyPrice = BuyPrice,
                SellPrice = SellPrice
            };
            if (ScaleUnit is not null)
            {
                instance.ScaleUnit = ScaleUnit;
            }

            return instance;
        }

        #endregion
    }
}
