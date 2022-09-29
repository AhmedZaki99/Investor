using InvestorData;

namespace InvestorAPI.Core
{
    /// <summary>
    /// Provides an abstraction for a service responsible for handling and processing <see cref="BusinessType"/> data.
    /// </summary>
    public interface IBusinessTypeService : IEntityService<BusinessType, BusinessTypeOutputDto, BusinessTypeInputDto, BusinessTypeInputDto>
    {



    }
}
