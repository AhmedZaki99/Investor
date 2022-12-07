using InvestorData;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvestorAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {

        #region Entity Sets

        public DbSet<Business> Businesses => Set<Business>();
        public DbSet<BusinessType> BusinessTypes => Set<BusinessType>();

        public DbSet<Account> Accounts => Set<Account>();

        public DbSet<ScaleUnit> ScaleUnits => Set<ScaleUnit>();
        public DbSet<UnitConversion> UnitConversions => Set<UnitConversion>();

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();

        public DbSet<Trader> Traders => Set<Trader>();
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
