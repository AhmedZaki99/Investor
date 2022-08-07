using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace Investor.Core
{
    internal static class ApiClientServiceCollectionExtensions
    {


        #region Constansts

        private const int RetryCount = 3;

        private const int TimeoutSeconds = 15;

        #endregion


        #region Extensions

        public static IHttpClientBuilder AddApiPolicyHandlers<TClient>(this IHttpClientBuilder clientBuilder) where TClient : class
        {
            clientBuilder.AddPolicyHandler(GetFallbackPolicy<TClient>);
            clientBuilder.AddPolicyHandler(GetRetryPolicy<TClient>);
            clientBuilder.AddPolicyHandler(GetTimeoutPolicy<TClient>);

            return clientBuilder;
        }

        #endregion


        #region Policy Handlers


        #region Fallback

        private static IAsyncPolicy<HttpResponseMessage?> GetFallbackPolicy<TClient>(IServiceProvider serviceProvider, HttpRequestMessage request) where TClient : class
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutRejectedException>()
                .FallbackAsync(fallbackValue: null, onFallbackAsync: OnFallbackCallback<TClient>);
        }

        private static Task OnFallbackCallback<TClient>(DelegateResult<HttpResponseMessage?> reponse) where TClient : class
        {
            if (reponse.Result is not null)
            {
                throw new ApiConnectionException(reponse.Result.StatusCode, $"Failed to communicate to the api due to server error, status code: {reponse.Result.StatusCode}.");
            }
            else if (reponse.Exception is HttpRequestException requestException)
            {
                throw new ApiConnectionException(requestException, "Failed to communicate to the api due to connection error, see the inner request exception.");
            }
            else if (reponse.Exception is TimeoutRejectedException timeoutException)
            {
                throw new ApiConnectionException(timeoutException, "Failed to communicate to the api due to connection timeout, see the inner timeout exception.");
            }
            return Task.CompletedTask;
        }

        #endregion

        #region Retry

        private static IAsyncPolicy<HttpResponseMessage?> GetRetryPolicy<TClient>(IServiceProvider serviceProvider, HttpRequestMessage request) where TClient : class
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(RetryCount, i => TimeSpan.FromSeconds(i), onRetry: (response, time) =>
                    OnRetryCallback<TClient>(serviceProvider, response, time));
        }

        private static void OnRetryCallback<TClient>(IServiceProvider serviceProvider, DelegateResult<HttpResponseMessage?> response, TimeSpan time) where TClient : class
        {
            var logger = serviceProvider.GetRequiredService<ILogger<TClient>>();

            string failureType = "unknown error";
            if (response.Result is not null)
            {
                failureType = $"server error (status code: {(int)response.Result.StatusCode})";
            }
            else if (response.Exception is HttpRequestException)
            {
                failureType = "connection error";
            }
            else if (response.Exception is TimeoutRejectedException)
            {
                failureType = "connection timeout";
            }

            logger.LogWarning("Failed to connect to the Api due to {failureType}, retrying after {retryDuration} seconds.", failureType, time.TotalSeconds);
        }

        #endregion

        #region Timeout

        private static IAsyncPolicy<HttpResponseMessage?> GetTimeoutPolicy<TClient>(IServiceProvider serviceProvider, HttpRequestMessage request) where TClient : class
        {
            return Policy.TimeoutAsync<HttpResponseMessage?>(TimeoutSeconds);
        }

        #endregion


        #endregion


    }
}
