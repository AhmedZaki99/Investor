using Microsoft.Extensions.DependencyInjection;

namespace InvestorAPI.Data
{
    /// <summary>
    /// Extension methods for setting up data access services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class DataServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the data access stores to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddInvestorDataStores(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            // Add Data Stores.
            services.AddScoped<IBrandStore, BrandStore>();

            return services;
        }

    }
}
