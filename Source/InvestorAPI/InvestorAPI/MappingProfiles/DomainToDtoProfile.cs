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
            CreateMap<BusinessType, BusinessTypeOutputDto>();
            CreateMap<BusinessTypeInputDto, BusinessType>().ReverseMap();

            // Business.
            CreateMap<Business, BusinessOutputDto>();
            CreateMap<BusinessCreateInputDto, Business>();
            CreateMap<BusinessUpdateInputDto, Business>().ReverseMap();
        }

    }
}
