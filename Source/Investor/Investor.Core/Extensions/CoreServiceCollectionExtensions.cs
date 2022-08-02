using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Fallback;
using Polly.Retry;
using Polly.Timeout;

namespace Investor.Core
{
    /// <summary>
    /// Extension methods for setting up essential Core services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class CoreServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the minimum essential Core services to the specified <see cref="IServiceCollection" />. Additional services,
        /// such as Endpoint servcies, must be added separately using the <see cref="CoreBuilder"/> returned from this method.
        /// </summary>
        /// <returns>A <see cref="CoreBuilder"/> that can be used to further configure the Core services.</returns>
        public static CoreBuilder AddCoreServices(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            // TODO: Add Core Services.
            

            return new CoreBuilder(services);
        }


        /// <summary>
        /// Adds Api Endpoint services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <returns>The <see cref="CoreBuilder"/>.</returns>
        public static CoreBuilder AddApiEndpoints(this CoreBuilder builder, string apiServerAddress)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            // Initialize Api policy handlers.
            var policies = GetApiPolicyHandlers();

            // Add Api Endpoints.
            builder.Services.AddHttpClient<IBrandEndpoint, BrandEndpoint>().AddApiPolicyHandlers(policies);


            // Configure Api Options.
            builder.Services.Configure<ApiOptions>(options => options.ApiServerAddress = apiServerAddress);

            return builder;
        }


        #region Helper Methods

        private static IHttpClientBuilder AddApiPolicyHandlers(this IHttpClientBuilder clientBuilder, params IAsyncPolicy<HttpResponseMessage?>[] policies)
        {
            foreach (var policy in policies)
            {
                clientBuilder.AddPolicyHandler(policy);
            }
            return clientBuilder;
        }

        private static IAsyncPolicy<HttpResponseMessage?>[] GetApiPolicyHandlers()
        {
            var fallbackPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutRejectedException>()
                .FallbackAsync(fallbackValue: null, onFallbackAsync: ThrowApiException);

            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(i));

            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage?>(1);


            return new IAsyncPolicy<HttpResponseMessage?>[]
            {
                fallbackPolicy,
                retryPolicy,
                timeoutPolicy
            };
        }

        private static Task ThrowApiException(DelegateResult<HttpResponseMessage?> func)
        {
            if (func.Result is not null)
            {
                throw new ApiConnectionException(func.Result.StatusCode, $"Failed to communicate to the api due to server error, status code: {func.Result.StatusCode}.");
            }
            else if (func.Exception is HttpRequestException requestException)
            {
                throw new ApiConnectionException(requestException, "Failed to communicate to the api due to connection error, see the inner request exception.");
            }
            else if (func.Exception is TimeoutRejectedException timeoutException)
            {
                throw new ApiConnectionException(timeoutException, "Failed to communicate to the api due to connection timeout, see the inner timeout exception.");
            }
            return Task.CompletedTask;
        }

        #endregion

    }
}
