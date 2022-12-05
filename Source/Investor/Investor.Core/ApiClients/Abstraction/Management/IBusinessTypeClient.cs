using InvestorData;

namespace Investor.Core
{
    /// <summary>
    /// Provides an abstraction for an endpoint client service, which manages <see cref="BusinessType"/> models.
    /// </summary>
    public interface IBusinessTypeClient : IEntityClient<BusinessType, BusinessTypeOutputDto, BusinessTypeInputDto, BusinessTypeInputDto>
    {



    }
}
