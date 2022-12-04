using AutoMapper;

namespace InvestorData
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
            CreateMap<Account, AccountOutputDto>();
            CreateMap<AccountInputDto, Account>().ReverseMap();


            // Category.
            CreateMap<Category, CategoryOutputDto>();
            CreateMap<CategoryCreateInputDto, Category>();
            CreateMap<CategoryUpdateInputDto, Category>().ReverseMap();

            // Trading Info.
            CreateMap<TradingInfo, TradingInfoOutputDto>();
            CreateMap<TradingInfoInputDto, TradingInfo>().ReverseMap();

            // Inventory Info.
            CreateMap<InventoryInfo, InventoryInfoOutputDto>();
            CreateMap<InventoryInfoInputDto, InventoryInfo>().ReverseMap();

            // Product.
            CreateMap<Product, ProductOutputDto>();
            CreateMap<ProductCreateInputDto, Product>();
            CreateMap<ProductUpdateInputDto, Product>().ReverseMap();
        }

    }
}
