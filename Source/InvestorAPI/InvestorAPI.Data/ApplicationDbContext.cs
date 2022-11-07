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

        public DbSet<TradingInfo> TradingInfos => Set<TradingInfo>();
        public DbSet<InventoryInfo> InventoryInfos => Set<InventoryInfo>();

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();

        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<Trader> Traders => Set<Trader>();

        public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
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

            // Set DatedEntity types creation date on database side.
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (GetAllBaseTypes(entity.ClrType).Any(t => t == typeof(DatedEntity)))
                {
                    entity.GetProperty(nameof(DatedEntity.DateCreated)).SetDefaultValueSql("GETUTCDATE()");
                }
            }
        }
        
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
