using Investor.Core;
using Investor.UI.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Investor.UI.Core
{
    /// <summary>
    /// Extension methods for setting up essential Application services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class ApplicationServiceCollectionExtensions
    {

        /// <summary>
        /// Adds the minimum essential Application services to the specified <see cref="IServiceCollection" />. Additional services
        /// such as the UI servcie must be added separately using the <see cref="ApplicationBuilder"/> returned from this method.
        /// </summary>
        /// <returns>An <see cref="ApplicationBuilder"/> that can be used to further configure the Application services.</returns>
        public static ApplicationBuilder AddApplicationServices(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            // Add Application Services.
            services.AddSingleton<IApplicationCore, ApplicationCore>();


            return new ApplicationBuilder(services);
        }


        /// <summary>
        /// Adds necessary Api-related services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <returns>The <see cref="ApplicationBuilder"/>.</returns>
        public static ApplicationBuilder AddApiServer(this ApplicationBuilder builder, string apiServerAddress)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            // Add Core Services with Api Endpoints.
            builder.Services.AddCoreServices()
                .AddApiEndpoints(apiServerAddress);

            return builder;
        }

        /// <summary>
        /// Adds necessary UI-related services including view models, and an <see cref="IUIService"/>
        /// implementation to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TImplementation">The type of the <see cref="IUIService"/> implementation.</typeparam>
        /// <returns>The <see cref="ApplicationBuilder"/>.</returns>
        public static ApplicationBuilder AddUI<TImplementation>(this ApplicationBuilder builder) where TImplementation : class, IUIService
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));


            // Add main view model.
            builder.Services.AddSingleton<IMainViewModel, MainViewModel>();

            // Add page view models.
            // TODO: Add pages.


            // Add UI Service.
            builder.Services.AddSingleton<IUIService, TImplementation>();


            return builder;
        }

    }
}
