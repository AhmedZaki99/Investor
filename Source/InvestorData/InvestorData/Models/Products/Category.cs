using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorData
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category : DatedEntity, IStringId, IComparable<Category>
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;

        public string? BusinessId { get; set; }
        public Business? Business { get; set; }


        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = null!;

        [MaxLength(1024)]
        public string? Description { get; set; }


        public List<Product> Products { get; set; } = new();


        #region Comparison

        public int CompareTo(Category? other)
        {
            return Name.CompareTo(other?.Name);
        } 

        #endregion

    }
}
