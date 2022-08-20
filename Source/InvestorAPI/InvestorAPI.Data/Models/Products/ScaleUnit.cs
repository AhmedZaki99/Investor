using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorAPI.Data
{
    public class ScaleUnit : DatedEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ScaleUnitId { get; set; } = null!;


        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        [MaxLength(1023)]
        public string? Description { get; set; }

    }
}
