using AutoMapper;
using InvestorAPI.Core;
using InvestorData;

using Scope = InvestorAPI.Core.AccountOutputDto.AccountScope;

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

            // Account.
            CreateMap<Account, AccountOutputDto>()
                .ForMember(dest => dest.Scope, src => src.MapFrom(src => 
                    src.BusinessId == null ? 
                    src.BusinessTypeId == null ? 
                    Scope.Global : Scope.BusinessTypeSpecific : Scope.Local));
            CreateMap<AccountInputDto, Account>().ReverseMap();
        }

    }
}
