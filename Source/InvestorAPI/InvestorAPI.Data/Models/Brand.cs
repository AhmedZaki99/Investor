using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorAPI.Data
{
    public class Brand
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string BrandId { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string ScaleUnit { get; set; } = null!;


        [MaxLength(1023)]
        public string? Description { get; set; }


        public decimal? BuyPrice { get; set; }

        public decimal? SellPrice { get; set; }


        [Required]
        [Precision(3)]
        public DateTime DateCreated { get; set; }

    }
}
