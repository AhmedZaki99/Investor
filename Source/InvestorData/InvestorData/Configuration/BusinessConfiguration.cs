using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestorData
{
    public class BusinessConfiguration : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {
            // UNDONE: Use data annotations for delete behavior instead of fluent API.

            #region Client-Cascade dependent items on delete

            builder
                .HasMany(b => b.Accounts)
                .WithOne(a => a.Business)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .HasMany(b => b.Products)
                .WithOne(p => p.Business)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .HasMany(b => b.Categories)
                .WithOne(c => c.Business)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .HasMany(b => b.ScaleUnits)
                .WithOne(u => u.Business)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .HasMany(b => b.Invoices)
                .WithOne(i => i.Business)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .HasMany(b => b.Payments)
                .WithOne(c => c.Business)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .HasMany(b => b.Traders)
                .WithOne(c => c.Business)
                .OnDelete(DeleteBehavior.ClientCascade);

            #endregion

        }
    }
}
