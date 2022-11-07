using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestorData
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasOne(p => p.SalesInformation)
                .WithOne();

            builder
                .HasOne(p => p.PurchasingInformation)
                .WithOne();

            builder
                .HasOne(p => p.InventoryDetails)
                .WithOne();
        }
    }
}
