using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestorData
{
    public class UnitConversionConfiguration : IEntityTypeConfiguration<UnitConversion>
    {
        public void Configure(EntityTypeBuilder<UnitConversion> builder)
        {
            builder
                .HasOne(c => c.SourceUnit)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .HasOne(c => c.TargetUnit)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
