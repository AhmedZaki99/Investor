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
            CreateMap<BusinessTypeInputDTO, BusinessType>().ReverseMap();

            // Business.
            CreateMap<Business, BusinessOutputDTO>();
            CreateMap<BusinessCreateInputDTO, Business>();
            CreateMap<BusinessUpdateInputDTO, Business>().ReverseMap();
        }

    }
}
