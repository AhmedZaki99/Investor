using InvestorData;
using Microsoft.Extensions.DependencyInjection;

namespace Investor.Core
{
    /// <summary>
    /// Extension methods for setting up essential Core services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class CoreServiceCollectionExtensions
    {

        #region Extensions

        /// <summary>
        /// Adds the minimum essential Core services to the specified <see cref="IServiceCollection" />. Additional services,
        /// such as Endpoint servcies, must be added separately using the <see cref="CoreBuilder"/> returned from this method.
        /// </summary>
        /// <returns>A <see cref="CoreBuilder"/> that can be used to further configure the Core services.</returns>
        public static CoreBuilder AddCoreServices(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            // Add Data Mapper.
            services.AddDataMapper();


            return new CoreBuilder(services);
        }


        /// <summary>
        /// Adds Api Endpoint services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <returns>The <see cref="CoreBuilder"/>.</returns>
        public static CoreBuilder AddApiEndpoints(this CoreBuilder builder, string apiServerAddress)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            // Add Api Clients.

            builder.Services.AddHttpClient<IBusinessTypeClient, BusinessTypeClient>().AddApiPolicyHandlers<BusinessTypeClient>();
            builder.Services.AddHttpClient<IBusinessClient, BusinessClient>().AddApiPolicyHandlers<BusinessClient>();

            builder.Services.AddHttpClient<IAccountClient, AccountClient>().AddApiPolicyHandlers<AccountClient>();
            
            builder.Services.AddHttpClient<ICategoryClient, CategoryClient>().AddApiPolicyHandlers<CategoryClient>();
            builder.Services.AddHttpClient<IProductClient, ProductClient>().AddApiPolicyHandlers<ProductClient>();


            // Configure Api Options.
            builder.Services.Configure<ApiOptions>(options => options.ApiServerAddress = apiServerAddress);

            return builder;
        }

        #endregion

    }
}
