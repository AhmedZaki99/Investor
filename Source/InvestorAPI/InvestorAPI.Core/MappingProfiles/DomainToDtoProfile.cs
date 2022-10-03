using AutoMapper;
using InvestorData;
using static InvestorAPI.Core.AccountOutputDto;

namespace InvestorAPI.Core
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
                .ForMember(dto => dto.Scope, config => config.MapFrom(model => 
                    model.BusinessId == null
                    ? model.BusinessTypeId == null 
                    ? AccountScope.Global 
                    : AccountScope.BusinessTypeSpecific 
                    : AccountScope.Local));

            CreateMap<AccountInputDto, Account>().ReverseMap();


            // Product.
        }

    }
}
