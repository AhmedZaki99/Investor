using InvestorAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InvestorAPI.Core
{
    /// <summary>
    /// Extension methods for setting up essential Core services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class CoreServiceCollectionExtensions
    {

        #region Extensions

        /// <summary>
        /// Adds the application core services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <returns>A <see cref="CoreBuilder"/> that can be used to further configure the Core services.</returns>
        public static CoreBuilder AddCoreServices(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));


            // Add Auto Mapper.
            services.AddAutoMapper(typeof(CoreServiceCollectionExtensions));


            // Add core services..

            services.AddScoped<IBusinessService, BusinessService>();
            services.AddScoped<IBusinessTypeService, BusinessTypeService>();

            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            

            return new CoreBuilder(services);
        }


        /// <summary>
        /// Adds Sql Server Database services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <returns>The <see cref="CoreBuilder"/>.</returns>
        public static CoreBuilder AddSqlServerDb(this CoreBuilder builder, string connectionString)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));
            ArgumentNullException.ThrowIfNull(connectionString, nameof(connectionString));

            // Add Database Context.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.CommandTimeout(60)));

            return builder;
        }

        #endregion

    }
}
