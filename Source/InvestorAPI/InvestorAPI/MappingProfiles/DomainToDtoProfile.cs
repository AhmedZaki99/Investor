using AutoMapper;
using InvestorAPI.Models;
using InvestorData;

namespace InvestorAPI
{
    public class DomainToDtoProfile : Profile
    {

        public DomainToDtoProfile()
        {
            // BusinessType.
            CreateMap<BusinessType, BusinessTypeOutputDTO>();
            CreateMap<BusinessTypeCreateInputDTO, BusinessType>();

            // Business.
            CreateMap<Business, BusinessOutputDTO>();
            CreateMap<BusinessCreateInputDTO, Business>();
        }

    }
}
