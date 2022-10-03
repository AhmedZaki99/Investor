using InvestorData;
using Microsoft.EntityFrameworkCore;

namespace InvestorAPI.Data
{
    public class ApplicationDbContext : DbContext
    {

        #region Entity Sets

        public DbSet<Business> Businesses => Set<Business>();
        public DbSet<BusinessType> BusinessTypes => Set<BusinessType>();

        public DbSet<Account> Accounts => Set<Account>();

        public DbSet<ScaleUnit> ScaleUnits => Set<ScaleUnit>();
        public DbSet<UnitConversion> UnitConversions => Set<UnitConversion>();

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();

        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<Trader> Traders => Set<Trader>();

        public DbSet<Item> Items => Set<Item>();
        public DbSet<Invoice> Invoices => Set<Invoice>();

        public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();
        public DbSet<Payment> Payments => Set<Payment>();


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

            // Business..
            BuildBusiness(modelBuilder);

            // Account..
            BuildAccount(modelBuilder);

            // Invoice & Bill..
            BuildInvoice(modelBuilder);

            // ScaleUnit..
            BuildScaleUnit(modelBuilder);

            // Dated Entities..
            BuildDatedEntities(modelBuilder);
        }

        #region Model Builders

        private static void BuildBusiness(ModelBuilder modelBuilder)
        {
            #region Client-Cascade dependent items on delete

            modelBuilder.Entity<Business>()
                .HasMany(b => b.Accounts)
                .WithOne(a => a.Business)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Business>()
                .HasMany(b => b.Products)
                .WithOne(p => p.Business)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Business>()
                .HasMany(b => b.Categories)
                .WithOne(c => c.Business)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Business>()
                .HasMany(b => b.ScaleUnits)
                .WithOne(u => u.Business)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Business>()
                .HasMany(b => b.Invoices)
                .WithOne(i => i.Business)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Business>()
                .HasMany(b => b.Payments)
                .WithOne(c => c.Business)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Business>()
                .HasMany(b => b.Traders)
                .WithOne(c => c.Business)
                .OnDelete(DeleteBehavior.ClientCascade);

            #endregion

        }

        private static void BuildAccount(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(i => i.AccountScope)
                .HasComputedColumnSql(
                    @$"
                        CASE
                            WHEN [{nameof(Account.BusinessId)}] IS NULL
                            THEN CASE
		                        WHEN [{nameof(Account.BusinessTypeId)}] IS NULL
		                        THEN CAST({AccountScope.Global:d} AS INT)
		                        ELSE CAST({AccountScope.BusinessTypeSpecific:d} AS INT)
	                        END
	                        ELSE CAST({AccountScope.Local:d} AS INT)
                        END
                    ");
        }

        private static void BuildInvoice(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .Property(i => i.Amount)
                .HasComputedColumnSql($"[{nameof(Item.Quantity)}] * [{nameof(Item.Price)}]");
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
