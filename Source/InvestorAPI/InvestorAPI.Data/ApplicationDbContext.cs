using Microsoft.EntityFrameworkCore;

namespace InvestorAPI.Data
{
    public class ApplicationDbContext : DbContext
    {

        #region Entity Sets

        public DbSet<Brand> Brands => Set<Brand>();
        
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


            modelBuilder.Entity<Brand>()
                .Property(b => b.ScaleUnit)
                .HasDefaultValue("Unit");
           
            modelBuilder.Entity<Brand>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");
        }

        #endregion

    }
}
