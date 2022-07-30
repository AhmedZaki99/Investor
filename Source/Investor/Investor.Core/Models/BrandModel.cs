using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Investor.Core
{
    public class BrandModel
    {

        #region Json Properties

        [JsonPropertyName("BrandId")]
        public string Id { get; }


        [Required]
        [StringLength(255, ErrorMessage = "Brand name should not be more than 255 characters.")]
        public string Name { get; set; }

        [Display(Name = "Scale Unit")]
        [StringLength(255, ErrorMessage = "Scale unit should not be more than 255 characters.")]
        public string ScaleUnit { get; set; }


        [DataType(DataType.MultilineText)]
        [StringLength(1023, ErrorMessage = "The maximum of 1023 characters has been exceeded for the description.")]
        public string? Description { get; set; }


        [Display(Name = "Buy Price")]
        [DataType(DataType.Currency)]
        public decimal? BuyPrice { get; set; }

        [Display(Name = "Sell Price")]
        [DataType(DataType.Currency)]
        public decimal? SellPrice { get; set; }


        [Display(Name = "Date Created")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateCreated { get; }

        #endregion

        #region Constructors

        [JsonConstructor]
        public BrandModel(string id, string name, string scaleUnit, string? description, decimal? buyPrice, decimal? sellPrice, DateTime dateCreated)
        {
            Id = id;
            Name = name;
            ScaleUnit = scaleUnit;
            Description = description;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            DateCreated = dateCreated;
        }

        public BrandModel(string id, string name, string scaleUnit, string? description, decimal? buyPrice, decimal? sellPrice)
            : this(id, name, scaleUnit, description, buyPrice, sellPrice, DateTime.UnixEpoch) { }

        public BrandModel(string name, string scaleUnit, string? description, decimal? buyPrice, decimal? sellPrice)
            : this(string.Empty, name, scaleUnit, description, buyPrice, sellPrice) { }

        #endregion

    }
}
