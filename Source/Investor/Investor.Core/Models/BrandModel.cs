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
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string ScaleUnit { get; set; }


        [DataType(DataType.MultilineText)]
        [StringLength(1023)]
        public string? Description { get; set; }


        [DataType(DataType.Currency)]
        public decimal? BuyPrice { get; set; }

        [DataType(DataType.Currency)]
        public decimal? SellPrice { get; set; }


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
