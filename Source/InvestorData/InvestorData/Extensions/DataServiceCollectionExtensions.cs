using Microsoft.Extensions.DependencyInjection;

namespace InvestorData
{
    /// <summary>
    /// Extension methods for setting up data related services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class DataServiceCollectionExtensions
    {

        #region Extensions

        /// <summary>
        /// Adds AutoMapper services to the specified <see cref="IServiceCollection" /> and configures if for Dtos.
        /// </summary>
        /// <returns>The <see cref="IServiceCollection" />.</returns>
        public static IServiceCollection AddDataMapper(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            // Add Auto Mapper.
            services.AddAutoMapper(typeof(DataServiceCollectionExtensions));

            return services;
        }

        #endregion

    }
}
