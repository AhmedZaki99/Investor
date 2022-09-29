using AutoMapper;
using InvestorData;

using Scope = InvestorAPI.Core.AccountOutputDto.AccountScope;

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
                .ForMember(dest => dest.Scope, src => src.MapFrom(src => 
                    src.ParentAccountId == null
                    ? src.BusinessId == null
                    ? src.BusinessTypeId == null 
                    ? Scope.Global 
                    : Scope.BusinessTypeSpecific 
                    : Scope.Local
                    : Scope.SubAccount));

            CreateMap<Account, ChildAccountOutputDto>();

            CreateMap<AccountCreateInputDto, Account>();
            CreateMap<AccountUpdateInputDto, Account>()
                .ReverseMap()
                .ConstructUsing(src => new AccountUpdateInputDto(src.ParentAccountId != null));
        }

    }
}
