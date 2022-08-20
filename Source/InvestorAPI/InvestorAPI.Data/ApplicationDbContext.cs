using Microsoft.EntityFrameworkCore;

namespace InvestorAPI.Data
{
    public class ApplicationDbContext : DbContext
    {

        #region Entity Sets

        public DbSet<Brand> Brands => Set<Brand>();


        public DbSet<Account> Accounts => Set<Account>();
        
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Service> Services => Set<Service>();
        
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


            // Brand Model..
            modelBuilder.Entity<Brand>()
                .Property(b => b.ScaleUnit)
                .HasDefaultValue("Unit");
           
            modelBuilder.Entity<Brand>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");


            // Dated Entities..
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
