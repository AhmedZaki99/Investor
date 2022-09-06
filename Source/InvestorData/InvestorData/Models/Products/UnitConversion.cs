using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestorData
{
    public class UnitConversion : IStringId
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


        [MaxLength(1024)]
        public string? Description { get; set; }

    }
}
