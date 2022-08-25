using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorAPI.Data
{
    public class UnitConversion
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;


        [Required]
        public string SourceUnitId { get; set; } = null!;
        public ScaleUnit SourceUnit { get; set; } = null!;

        [Required]
        public string TargetUnitId { get; set; } = null!;
        public ScaleUnit TargetUnit { get; set; } = null!;


        [Required]
        public double ConversionValue { get; set; }


        [MaxLength(1023)]
        public string? Description { get; set; }

    }
}
