using InvestorData;

namespace Investor.Core
{
    /// <summary>
    /// Provides an abstraction for an endpoint client service, which manages <see cref="Business"/> models.
    /// </summary>
    public interface IBusinessClient : IEntityClient<Business, BusinessOutputDto, BusinessCreateInputDto, BusinessUpdateInputDto>
    {



    }
}
