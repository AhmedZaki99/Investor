using InvestorData;

namespace Investor.Core
{
    /// <summary>
    /// Provides an abstraction for a client service, which communitcates with the api to
    /// manage business-related data models.
    /// </summary>
    public interface IBusinessEntityClient<TEntity, TOutputDto, TCreateDto, TUpdateDto> : IEntityClient<TEntity, TOutputDto, TCreateDto, TUpdateDto>
        where TEntity : BusinessEntity
        where TOutputDto : OutputDtoBase
        where TCreateDto : class
        where TUpdateDto : class
    {

        #region Read

        /// <summary>
        /// Get all entities for the given business.
        /// </summary>
        Task<IEnumerable<TEntity>> GetAllAsync(string businessId);

        #endregion

    }
}
