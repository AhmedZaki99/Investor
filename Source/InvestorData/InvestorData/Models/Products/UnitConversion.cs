using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvestorData
{
    [EntityTypeConfiguration(typeof(UnitConversionConfiguration))]
    public class UnitConversion : EntityBase
    {

        [Required]
        public string SourceUnitId { get; set; } = null!;
        public ScaleUnit? SourceUnit { get; set; }

        [Required]
        public string TargetUnitId { get; set; } = null!;
        public ScaleUnit? TargetUnit { get; set; }


        public double ConversionValue { get; set; }


        [MaxLength(1024)]
        public string? Description { get; set; }

    }
}
