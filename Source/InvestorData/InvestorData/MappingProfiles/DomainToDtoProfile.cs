using AutoMapper;

namespace InvestorData
{
    public class DomainToDtoProfile : Profile
    {

        public DomainToDtoProfile()
        {
            // BusinessType.
            CreateMap<BusinessType, BusinessTypeOutputDto>().ReverseMap();
            CreateMap<BusinessTypeInputDto, BusinessType>().ReverseMap();

            // Business.
            CreateMap<Business, BusinessOutputDto>().ReverseMap();
            CreateMap<BusinessCreateInputDto, Business>();
            CreateMap<BusinessUpdateInputDto, Business>().ReverseMap();

            // Account.
            CreateMap<Account, AccountOutputDto>().ReverseMap();
            CreateMap<AccountInputDto, Account>().ReverseMap();


            // Category.
            CreateMap<Category, CategoryOutputDto>().ReverseMap();
            CreateMap<CategoryCreateInputDto, Category>();
            CreateMap<CategoryUpdateInputDto, Category>().ReverseMap();

            // Trading Info.
            CreateMap<TradingInfo, TradingInfoOutputDto>().ReverseMap();
            CreateMap<TradingInfoInputDto, TradingInfo>().ReverseMap();

            // Inventory Info.
            CreateMap<InventoryInfo, InventoryInfoOutputDto>().ReverseMap();
            CreateMap<InventoryInfoInputDto, InventoryInfo>().ReverseMap();

            // Product.
            CreateMap<Product, ProductOutputDto>().ReverseMap();
            CreateMap<ProductCreateInputDto, Product>();
            CreateMap<ProductUpdateInputDto, Product>().ReverseMap();
        }

    }
}
