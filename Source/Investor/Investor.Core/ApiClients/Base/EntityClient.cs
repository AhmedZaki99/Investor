using AutoMapper;
using Flurl;
using InvestorData;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Investor.Core
{

    /// <inheritdoc cref="IEntityClient{TEntity, TOutputDto, TCreateDto, TUpdateDto}"/>
    internal abstract class EntityClient<TEntity, TOutputDto, TCreateDto, TUpdateDto> : IEntityClient<TEntity, TOutputDto, TCreateDto, TUpdateDto>
        where TEntity : EntityBase
        where TOutputDto : OutputDtoBase
        where TCreateDto : class
        where TUpdateDto : class
    {

        #region Protected Properties

        protected JsonSerializerOptions JsonOptions { get; }

        #endregion

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

            string baseUrl = Url.Combine(Options.ApiServerAddress, Options.ApiRelativePath, endpointPath);

            HttpClient = httpClient;

            HttpClient.BaseAddress = new Uri($"{baseUrl}/");
            HttpClient.DefaultRequestHeaders.Add("accept", "application/json");
            HttpClient.DefaultRequestHeaders.Add("User-Agent", "Investor-Client");

            Mapper = mapper;

            JsonOptions = new(JsonSerializerDefaults.Web);
            JsonOptions.Converters.Add(new JsonStringEnumConverter());
        }

        #endregion


        #region Interface Implementation

        #region Read

        /// <inheritdoc/>
        public async Task<TEntity?> GetAsync(string id)
        {
            try
            {
                var dto = await HttpClient.GetFromJsonAsync<TOutputDto>(id, options: JsonOptions) ?? 
                    throw new NullReferenceException("Api resonse data deserialization returned null.");

                return MapOutput(dto);
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
        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return GetAllInternalAsync();
        }

        #endregion

        #region Create, Update & Delete

        /// <inheritdoc/>
        public async Task<TEntity> CreateAsync(TCreateDto dto)
        {
            using var response = await HttpClient.PostAsJsonAsync(string.Empty, dto, options: JsonOptions);

            response.EnsureSuccessStatusCode();

            var responseDto = await response.Content.ReadFromJsonAsync<TOutputDto>(options: JsonOptions) ??
                throw new NullReferenceException("Api resonse data deserialization returned null.");

            return MapOutput(responseDto);
        }


        /// <inheritdoc/>
        public async Task<TEntity?> SaveChangesAsync(string id, TUpdateDto updatedDto)
        {
            var entity = await GetAsync(id);
            if (entity is null)
            {
                return null;
            }
            return await SaveChangesAsync(entity, updatedDto);
        }

        /// <inheritdoc/>
        public async Task<TEntity?> SaveChangesAsync(TEntity originalEntity, TUpdateDto updatedDto)
        {
            var originalDto = Mapper.Map<TUpdateDto>(originalEntity);

            var patchDoc = new JsonPatchDocument();
            patchDoc.Construct(originalDto, updatedDto);

            using var response = await HttpClient.PatchAsJsonAsync(originalEntity.Id, patchDoc, options: JsonOptions);

            response.EnsureSuccessStatusCode();

            var responseDto = await response.Content.ReadFromJsonAsync<TOutputDto>(options: JsonOptions) ??
                throw new NullReferenceException("Api resonse data deserialization returned null.");

            return MapOutput(responseDto);
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


        #region Protected Helper Methods

        protected async Task<IEnumerable<TEntity>> GetAllInternalAsync(string relativePath = "", object? query = null)
        {
            // TODO: Check the possibility of using IAsyncEnumerable and recent bug fixes.
            try
            {
                string path = HttpClient.BaseAddress
                    .AppendPathSegment(relativePath)
                    .SetQueryParams(query);

                var dtos = await HttpClient.GetFromJsonAsync<IEnumerable<TOutputDto>>(path, options: JsonOptions) ??
                    throw new NullReferenceException("Api resonse data deserialization returned null.");

                return dtos.Select(MapOutput);
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

        #region Mapping

        /// <summary>
        /// Maps the given Dto to an entity instance valid for binding.
        /// </summary>
        /// <remarks>
        /// Could be overridden to provide further adjustment on mapped element.
        /// </remarks>
        /// <param name="dto">The output object to map.</param>
        /// <returns>The mapped instance of the entity.</returns>
        protected virtual TEntity MapOutput(TOutputDto dto)
        {
            return Mapper.Map<TEntity>(dto);
        }

        #endregion

    }
}
