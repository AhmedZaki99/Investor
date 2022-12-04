using AutoMapper;
using InvestorData;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;

namespace Investor.Core
{

    /// <inheritdoc cref="IEntityClient{TEntity, TOutputDto, TCreateDto, TUpdateDto}"/>
    internal abstract class EntityClient<TEntity, TOutputDto, TCreateDto, TUpdateDto> : IEntityClient<TEntity, TOutputDto, TCreateDto, TUpdateDto>
        where TEntity : EntityBase
        where TOutputDto : OutputDtoBase
        where TCreateDto : class
        where TUpdateDto : class
    {

        #region Protected Dependencies

        protected HttpClient HttpClient { get; }
        protected ApiOptions Options { get; }

        protected IMapper Mapper { get; }

        #endregion

        #region Constructor

        public EntityClient(HttpClient httpClient, IOptions<ApiOptions> optionsAccessor, IMapper mapper, string endpointPath)
        {
            Options = optionsAccessor?.Value ??
                throw new InvalidOperationException("Api options must be configured in order to connect with Api Endpoints.");

            string baseUrl = $"{Options.ApiServerAddress}/{Options.ApiRelativePath}/{endpointPath}/";

            HttpClient = httpClient;

            HttpClient.BaseAddress = new Uri(baseUrl);
            HttpClient.DefaultRequestHeaders.Add("accept", "application/json");
            HttpClient.DefaultRequestHeaders.Add("User-Agent", "Investor-Client");

            Mapper = mapper;
        }

        #endregion


        #region Interface Implementation

        #region Read

        /// <inheritdoc/>
        public async Task<TEntity?> GetAsync(string id)
        {
            try
            {
                var dto = await HttpClient.GetFromJsonAsync<TOutputDto>(id) ?? 
                    throw new NullReferenceException("Api resonse data deserialization returned null.");

                return Mapper.Map<TEntity>(dto);
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

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            // TODO: Check the possibility of using IAsyncEnumerable and recent bug fixes.
            try
            {
                var dtos = await HttpClient.GetFromJsonAsync<IEnumerable<TOutputDto>>(string.Empty) ??
                    throw new NullReferenceException("Api resonse data deserialization returned null.");

                return Mapper.Map<IEnumerable<TEntity>>(dtos);
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return Enumerable.Empty<TEntity>();
                }
                throw;
            }
        }

        #endregion

        #region Create, Update & Delete

        /// <inheritdoc/>
        public async Task<TEntity> CreateAsync(TCreateDto dto)
        {
            using var response = await HttpClient.PostAsJsonAsync(string.Empty, dto);

            response.EnsureSuccessStatusCode();

            var responseDto = await response.Content.ReadFromJsonAsync<TOutputDto>() ??
                throw new NullReferenceException("Api resonse data deserialization returned null.");

            return Mapper.Map<TEntity>(responseDto);
        }

        /// <inheritdoc/>
        public async Task<bool> SaveChangesAsync(string id, TUpdateDto dto)
        {
            using var response = await HttpClient.PutAsJsonAsync(id, dto);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            response.EnsureSuccessStatusCode();

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(string id)
        {
            using var response = await HttpClient.DeleteAsync(id);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            response.EnsureSuccessStatusCode();

            return true;
        }

        #endregion

        #endregion

    }
}
