using Investor.UI.Core.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Investor.UI.WPF
{
    /// <summary>
    /// The <see cref="IBrandViewModel"/> design instance provider.
    /// </summary>
    public class BrandDesignModel : IBrandViewModel
    {

        #region Implementation

        public string Name { get; set; }
        public string ScaleUnit { get; set; }
        public string? Description { get; set; }
        public decimal? BuyPrice { get; set; }
        public decimal? SellPrice { get; set; }

        #endregion

        #region Constructor

        public BrandDesignModel()
        {
            Name = "Dell Inspiron 3580 Laptop";
            ScaleUnit = "Device";
            Description = "Features Intel Core i5 8th-Gen CPU, with 8 GB Memory, and a Radeon 520 HD GPU";
            BuyPrice = 550m;
            SellPrice = 699.99m;
        }

        public BrandDesignModel(string name, string scaleUnit, string? description, decimal? buyPrice, decimal? sellPrice)
        {
            Name = name;
            ScaleUnit = scaleUnit;
            Description = description;
            SellPrice = sellPrice;
            BuyPrice = buyPrice;
        }

        #endregion

    }
}
