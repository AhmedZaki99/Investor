using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;

namespace Investor.Core
{

    /// <inheritdoc cref="IModelEndpoint{TModel}"/>
    internal abstract class ModelEndpoint<TModel> : ModelEndpoint<TModel, string>, IModelEndpoint<TModel> where TModel : class
    {
        public ModelEndpoint(HttpClient httpClient, IOptions<ApiOptions> optionsAccessor, string endpointPath)
            : base(httpClient, optionsAccessor, endpointPath) { }
    }


    /// <inheritdoc cref="IModelEndpoint{TModel, TKey}"/>
    internal abstract class ModelEndpoint<TModel, TKey> : IModelEndpoint<TModel, TKey> where TModel : class
    {

        #region Protected Dependencies

        protected HttpClient HttpClient { get; }

        protected ApiOptions Options { get; }

        #endregion

        #region Constructor

        public ModelEndpoint(HttpClient httpClient, IOptions<ApiOptions> optionsAccessor, string endpointPath)
        {
            Options = optionsAccessor?.Value ??
                throw new InvalidOperationException("Api options must be configured in order to connect with Api Endpoints.");

            string baseUrl = $"{Options.ApiServerAddress}/{Options.ApiRelativePath}/{endpointPath}/";

            HttpClient = httpClient;

            HttpClient.BaseAddress = new Uri(baseUrl);
            HttpClient.DefaultRequestHeaders.Add("accept", "application/json");
            HttpClient.DefaultRequestHeaders.Add("User-Agent", "Investor-Client");
        }

        #endregion


        #region Interface Implementation

        #region Read

        /// <inheritdoc/>
        public async Task<TModel?> GetAsync(TKey key)
        {
            try
            {
                return await HttpClient.GetFromJsonAsync<TModel>($"{key}") ?? 
                    throw new NullReferenceException("Api resonse data deserialization returned null.");
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                throw;
            }
        }

        #endregion

        #region Pagination

        /// <inheritdoc/>
        public abstract Task<IEnumerable<TModel>> PaginateAsync(TModel? lastModel = null);

        /// <inheritdoc/>
        public abstract Task<IEnumerable<TModel>> PaginateAsync(int count, TModel? lastModel = null);

        #endregion

        #region Create, Update & Delete

        /// <inheritdoc/>
        public async Task<TModel> CreateAsync(TModel model)
        {
            using var response = await HttpClient.PostAsJsonAsync(string.Empty, model);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TModel>() ??
                throw new NullReferenceException("Api resonse data deserialization returned null.");
        }

        /// <inheritdoc/>
        public async Task<bool> SaveChangesAsync(TModel model)
        {
            using var response = await HttpClient.PutAsJsonAsync($"{GetKey(model)}", model);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            response.EnsureSuccessStatusCode();

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(TKey key)
        {
            using var response = await HttpClient.DeleteAsync($"{key}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            response.EnsureSuccessStatusCode();

            return true;
        }

        #endregion

        #endregion

        #region Abstract Helper Methods

        protected abstract TKey GetKey(TModel model);

        #endregion

    }
}
