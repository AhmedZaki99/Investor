using InvestorData;

namespace InvestorAPI.Core
{
    /// <summary>
    /// Provides an abstraction for a service responsible for handling and processing <see cref="Business"/> data.
    /// </summary>
    public interface IBusinessService : IEntityService<Business, BusinessOutputDto, BusinessCreateInputDto, BusinessUpdateInputDto>
    {



    }
}
