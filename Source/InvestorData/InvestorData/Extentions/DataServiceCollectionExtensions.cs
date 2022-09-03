using Microsoft.Extensions.DependencyInjection;

namespace InvestorData
{
    /// <summary>
    /// Extension methods for setting up data access services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class DataServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the data access repositories to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <typeparam name="TContext">Type of the database context.</typeparam>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddInvestorRepositories<TContext>(this IServiceCollection services) where TContext : InvestorDbContext
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            // Add repositories.
            services.AddScoped<IProductRepository, ProductRepository<TContext>>();

            return services;
        }

    }
}
