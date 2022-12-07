using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvestorData
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category : OptionalBusinessEntity, IUniqueName
    {

        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = null!;

        [MaxLength(1024)]
        public string? Description { get; set; }


        public ICollection<Product> Products { get; set; } = null!;

    }
}
