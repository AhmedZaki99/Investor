using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestorData
{
    public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder
                .Property(i => i.Amount)
                .HasComputedColumnSql($"[{nameof(InvoiceItem.Quantity)}] * [{nameof(InvoiceItem.Price)}]");
        }
    }
}
