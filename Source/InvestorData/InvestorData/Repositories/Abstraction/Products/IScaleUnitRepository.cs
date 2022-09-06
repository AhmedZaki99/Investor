namespace InvestorData
{

    /// <summary>
    /// Provides an abstraction for a repository which manages <see cref="ScaleUnit"/> entities.
    /// </summary>
    public interface IScaleUnitRepository : IRepository<ScaleUnit>
    {


        #region Read

        /// <summary>
        /// Returns all entities for a given business.
        /// </summary>
        /// <param name="businessId">Business to get entities for.</param>
        IAsyncEnumerable<ScaleUnit> GetEntitiesAsync(string businessId);

        #endregion

    }
}
