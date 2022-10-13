using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Investor.Core
{
    /// <summary>
    /// An endpoint client service that manages <see cref="BrandModel"/> models.
    /// </summary>
    internal class BrandEndpoint : ModelEndpoint<BrandModel>, IBrandEndpoint
    {


        #region Constructor

        public BrandEndpoint(HttpClient httpClient, IOptions<ApiOptions> optionsAccessor) : base(httpClient, optionsAccessor, "brands")
        {
            // IMPORTANT: Rename Endpoint suffix to Client.
        }

        #endregion


        #region Interface Implementation

        /// <inheritdoc/>
        public Task<IEnumerable<BrandModel>> PaginateAsync(DateTime lastBrandDate)
        {
            string queryParams = $"?lastBrandDate={lastBrandDate:s}";

            return PaginateAsync(queryParams);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<BrandModel>> PaginateAsync(int count, DateTime lastBrandDate)
        {
            string queryParams = $"?lastBrandDate={lastBrandDate:s}&perPage={count}";

            return PaginateAsync(queryParams);
        }

        #endregion

        #region Abstract Implementation

        /// <inheritdoc/>
        public override Task<IEnumerable<BrandModel>> PaginateAsync(BrandModel? lastModel = null)
        {
            if (lastModel is not null)
            {
                return PaginateAsync(lastModel.DateCreated);
            }
            return PaginateAsync(string.Empty);
        }

        /// <inheritdoc/>
        public override Task<IEnumerable<BrandModel>> PaginateAsync(int count, BrandModel? lastModel = null)
        {
            if (lastModel is not null)
            {
                return PaginateAsync(count, lastModel.DateCreated);
            }
            return PaginateAsync($"?perPage={count}");
        }

        #endregion

        #region Protected Abstract Implementation

        protected override string GetKey(BrandModel model) => model.Id;

        #endregion

        #region Helper Methods

        private async Task<IEnumerable<BrandModel>> PaginateAsync(string queryParams)
        {
            return await HttpClient.GetFromJsonAsync<IEnumerable<BrandModel>>(queryParams) ??
                throw new NullReferenceException("Api resonse data deserialization returned null.");
        }

        #endregion
    }
}
