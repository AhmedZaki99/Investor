using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorAPI.Data
{
    public class UnitConversion
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string UnitConversionId { get; set; } = null!;


        [Required]
        public ScaleUnit SourceUnit { get; set; } = null!;
        public string SourceUnitId { get; set; } = null!;

        [Required]
        public ScaleUnit TargetUnit { get; set; } = null!;
        public string TargetUnitId { get; set; } = null!;


        [Required]
        public double ConversionValue { get; set; }


        [MaxLength(1023)]
        public string? Description { get; set; }

    }
}
