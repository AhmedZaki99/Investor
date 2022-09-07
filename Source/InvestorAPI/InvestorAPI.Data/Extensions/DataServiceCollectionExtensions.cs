using InvestorData;
using Microsoft.Extensions.DependencyInjection;

namespace InvestorAPI.Data
{
    /// <summary>
    /// Extension methods for setting up data access services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class DataServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the data access repositories to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            // Add base repositories.
            services.AddInvestorRepositories<ApplicationDbContext>();

            // Add local repositories.
            services.AddScoped<IBrandRepository, BrandRepository>();

            return services;
        }

    }
}
