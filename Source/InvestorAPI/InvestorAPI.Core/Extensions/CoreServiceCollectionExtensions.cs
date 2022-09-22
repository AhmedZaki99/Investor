using Microsoft.Extensions.DependencyInjection;

namespace InvestorAPI.Core
{
    /// <summary>
    /// Extension methods for setting up essential Core services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class CoreServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the application core services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            // Add auto mapper.
            services.AddAutoMapper(typeof(CoreServiceCollectionExtensions));


            // Add core services..

            services.AddScoped<IAccountService, AccountService>();


            return services;
        }

    }
}
