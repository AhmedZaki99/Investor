using InvestorData;

namespace InvestorAPI.Core
{
    /// <summary>
    /// A base interface that provides an abstraction for services handling and processing
    /// business-related models of type <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity to process.</typeparam>
    /// <typeparam name="TOutputDto">Type of the output data transfer object.</typeparam>
    /// <typeparam name="TCreateDto">Type of the input data transfer object used for entity creation.</typeparam>
    /// <typeparam name="TUpdateDto">Type of the input data transfer object used for entity update.</typeparam>
    public interface IBusinessEntityService<TEntity, TOutputDto, TCreateDto, TUpdateDto> : IEntityService<TEntity, TOutputDto, TCreateDto, TUpdateDto>
        where TEntity : BusinessEntity
        where TOutputDto : OutputDtoBase
        where TCreateDto : class
        where TUpdateDto : class
    {

        #region Read

        /// <summary>
        /// Returns all entities for a given business.
        /// </summary>
        /// <param name="businessId">Business to get entities for.</param>
        IAsyncEnumerable<TOutputDto> GetEntitiesAsync(string businessId);

        #endregion

    }
}
