using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvestorData
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category : OptionalBusinessEntity, IUniqueName, IComparable<Category>
    {

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
