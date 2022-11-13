using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestorData
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .OwnsOne(p => p.SalesInformation, owned => owned.ToTable("SalesInfos"));

            builder
                .OwnsOne(p => p.PurchasingInformation, owned => owned.ToTable("PurchasingInfos"));
        }
    }
}
