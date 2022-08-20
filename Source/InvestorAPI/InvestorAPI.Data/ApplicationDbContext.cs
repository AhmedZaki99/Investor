using Microsoft.EntityFrameworkCore;

namespace InvestorAPI.Data
{
    public class ApplicationDbContext : DbContext
    {

        #region Entity Sets

        public DbSet<Brand> Brands => Set<Brand>();


        public DbSet<Account> Accounts => Set<Account>();
        
        public DbSet<ScaleUnit> ScaleUnits => Set<ScaleUnit>();
        public DbSet<UnitConversion> UnitConversion => Set<UnitConversion>();

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Vendor> Vendors => Set<Vendor>();

        public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
        public DbSet<Invoice> Invoices => Set<Invoice>();

        public DbSet<BillItem> BillItems => Set<BillItem>();
        public DbSet<Bill> Bills => Set<Bill>();

        #endregion


        #region Constructors

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        #endregion


        #region Fluent API

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Prototype

            // Brand Model..
            modelBuilder.Entity<Brand>()
                .Property(b => b.ScaleUnit)
                .HasDefaultValue("Unit");

            modelBuilder.Entity<Brand>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");

            #endregion

            // Invoice & Bill..
            BuildInvoice(modelBuilder);

            // ScaleUnit..
            BuildScaleUnit(modelBuilder);

            // Dated Entities..
            BuildDatedEntities(modelBuilder);
        }

        #region Model Builders

        private static void BuildInvoice(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceItem>()
                .Property(i => i.Amount)
                .HasComputedColumnSql($"[{nameof(InvoiceItem.Quantity)}] * [{nameof(InvoiceItem.Price)}]");
        }

        private static void BuildScaleUnit(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UnitConversion>()
                .HasOne(c => c.SourceUnit)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<UnitConversion>()
                .HasOne(c => c.TargetUnit)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);
        }

        private static void BuildDatedEntities(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (GetAllBaseTypes(entity.ClrType).Any(t => t == typeof(DatedEntity)))
                {
                    entity.GetProperty(nameof(DatedEntity.DateCreated)).SetDefaultValueSql("GETUTCDATE()");
                }
            }
        }

        #endregion

        #endregion


        #region Convention

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<decimal>()
                .HavePrecision(19, 4);
        }

        #endregion


        #region Helper Methods

        private static IEnumerable<Type> GetAllBaseTypes(Type? type)
        {
            while ((type = type?.BaseType) is not null)
            {
                yield return type;
            }
        }

        #endregion

    }
}
