using Microsoft.Extensions.DependencyInjection;

namespace Investor.UI.Core
{
    /// <summary>
    /// A builder for configuring Application Serviecs.
    /// </summary>
    public class ApplicationBuilder
    {

        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> where application services are configured.
        /// </summary>
        public IServiceCollection Services { get; }


        public ApplicationBuilder(IServiceCollection services)
        {
            Services = services;
        }

    }
}
