using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace Investor.Core
{
    /// <summary>
    /// Extension methods for setting up essential Core services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class CoreServiceCollectionExtensions
    {

        #region Public Extentions

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

            // Add Api Endpoints.
            builder.Services.AddHttpClient<IBrandEndpoint, BrandEndpoint>().AddApiPolicyHandlers<IBrandEndpoint>();


            // Configure Api Options.
            builder.Services.Configure<ApiOptions>(options => options.ApiServerAddress = apiServerAddress);

            return builder;
        }

        #endregion

        #region Private Extentions

        private static IHttpClientBuilder AddApiPolicyHandlers<TClient>(this IHttpClientBuilder clientBuilder) where TClient : class
        {
            clientBuilder.AddPolicyHandler(GetFallbackPolicy<TClient>);
            clientBuilder.AddPolicyHandler(GetRetryPolicy<TClient>);
            clientBuilder.AddPolicyHandler(GetTimeoutPolicy<TClient>);

            return clientBuilder;
        }

        #endregion


        #region Helper Methods

        private static IAsyncPolicy<HttpResponseMessage?> GetTimeoutPolicy<TClient>(IServiceProvider serviceProvider, HttpRequestMessage request) where TClient : class
        {
            return Policy.TimeoutAsync<HttpResponseMessage?>(10);
        }
        
        private static IAsyncPolicy<HttpResponseMessage?> GetRetryPolicy<TClient>(IServiceProvider serviceProvider, HttpRequestMessage request) where TClient : class
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(i), onRetry: (func, time) =>
                {
                    var logger = serviceProvider.GetRequiredService<ILogger<TClient>>();

                    string failureType = "unknown error";
                    if (func.Result is not null)
                    {
                        failureType = $"server error (status code: {(int)func.Result.StatusCode})";
                    }
                    else if (func.Exception is HttpRequestException)
                    {
                        failureType = "connection error";
                    }
                    else if (func.Exception is TimeoutRejectedException)
                    {
                        failureType = "connection timeout";
                    }

                    logger.LogWarning("Failed to connect to the Api due to {failureType}, retrying after {retryDuration} seconds.", failureType, time.TotalSeconds);
                });
        }

        private static IAsyncPolicy<HttpResponseMessage?> GetFallbackPolicy<TClient>(IServiceProvider serviceProvider, HttpRequestMessage request) where TClient : class
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutRejectedException>()
                .FallbackAsync(fallbackValue: null, onFallbackAsync: func =>
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
                });
        }

        #endregion

    }
}
