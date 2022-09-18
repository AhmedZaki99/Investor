using InvestorAPI.Data;

namespace InvestorAPI.Models
{
    public class BrandOutputDto
    {

        #region Public Properties

        public string BrandId { get; set; }

        public string Name { get; set; }
        public string ScaleUnit { get; set; }

        public string? Description { get; set; }

        public decimal? BuyPrice { get; set; }
        public decimal? SellPrice { get; set; }

        public DateTime DateCreated { get; set; }

        #endregion


        #region Constructors

        public BrandOutputDto(Brand brand)
        {
            BrandId = brand.BrandId;
            Name = brand.Name;
            ScaleUnit = brand.ScaleUnit;
            Description = brand.Description;
            BuyPrice = brand.BuyPrice;
            SellPrice = brand.SellPrice;
            DateCreated = brand.DateCreated;
        }

        #endregion


        #region Static Methods



        #endregion

    }
}
