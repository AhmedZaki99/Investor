using InvestorData;
using Microsoft.EntityFrameworkCore;

namespace InvestorAPI.Data
{
    public class ApplicationDbContext : InvestorDbContext
    {

        #region Entity Sets

        #region Prototype

        public DbSet<Brand> Brands => Set<Brand>();

        #endregion


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

        }

        #endregion

    }
}
