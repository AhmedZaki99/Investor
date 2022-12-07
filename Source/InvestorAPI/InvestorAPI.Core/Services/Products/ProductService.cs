using AutoMapper;
using AutoMapper.QueryableExtensions;
using InvestorAPI.Data;
using InvestorData;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InvestorAPI.Core
{
    /// <summary>
    /// A service responsible for handling and processing <see cref="Product"/> models.
    /// </summary>
    internal class ProductService : EntityService<Product, ProductOutputDto, ProductCreateInputDto, ProductUpdateInputDto>, IProductService
    {

        // TODO: You might just want to add events triggered on each of the CRUD operations (protected events - methods - for the moment).
        // IMPORTANT: Note that services should not act like repositories, so, inter-services injection should be possible for shared effects.
        //            And remember, the Client is the User, while the Server is the Business.

        #region Constructor

        public ProductService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, dbContext.Products, mapper)
        {

        }

        #endregion


        #region Read

        /// <inheritdoc/>
        public override IAsyncEnumerable<ProductOutputDto> GetEntitiesAsync(params Expression<Func<Product, bool>>[] conditions)
        {
            var query = QueryIncludingInfo();
            foreach (var condition in conditions)
            {
                query = query.Where(condition);
            }

            return query
                .ProjectTo<ProductOutputDto>(Mapper.ConfigurationProvider)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }

        /// <inheritdoc/>
        public override Task<ProductOutputDto?> FindEntityAsync(string id)
        {
            return QueryIncludingInfo()
                .ProjectTo<ProductOutputDto>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<ProductOutputDto> GetEntitiesAsync(string businessId)
        {
            return GetEntitiesAsync(p => p.BusinessId == businessId);
        }

        #endregion

        #region Search

        /// <inheritdoc/>
        public IAsyncEnumerable<ProductOutputDto> SearchByCode(string businessId, string code)
        {
            return GetEntitiesAsync(p => p.BusinessId == businessId && p.Code != null && p.Code.StartsWith(code));
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<ProductOutputDto> SearchByName(string businessId, string name)
        {
            return GetEntitiesAsync(p => p.BusinessId == businessId && p.Name.Contains(name));
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<ProductOutputDto> SearchByCodeThenName(string businessId, string codeOrName)
        {
            return GetEntitiesAsync(p => p.BusinessId == businessId,
                                    p => p.Code != null && p.Code.StartsWith(codeOrName) 
                                      || p.Name.Contains(codeOrName));
        }

        #endregion

        #region Filter

        /// <inheritdoc/>
        public IAsyncEnumerable<ProductOutputDto> FilterByType(string businessId, bool isService)
        {
            return GetEntitiesAsync(p => p.BusinessId == businessId && p.IsService == isService);
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<ProductOutputDto> FilterByCategory(string businessId, string categoryId)
        {
            return GetEntitiesAsync(p => p.BusinessId == businessId && p.CategoryId == categoryId);
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<ProductOutputDto> FilterByTypeAndCategory(string businessId, bool isServie, string categoryId)
        {
            return GetEntitiesAsync(p => p.BusinessId == businessId 
                                      && p.CategoryId == categoryId
                                      && p.IsService == isServie);
        }

        #endregion


        #region Mapping

        protected override Product MapForCreate(ProductCreateInputDto dto)
        {
            var product = base.MapForCreate(dto);
            if (product.Category is not null)
            {
                product.Category.BusinessId = dto.BusinessId;
            }
            return product;
        }

        protected override Product MapForUpdate(ProductUpdateInputDto dto, Product original)
        {
            var product = base.MapForUpdate(dto, original);
            if (dto.Category is not null && product.Category is not null)
            {
                product.Category.BusinessId = original.BusinessId;
            }
            return product;
        }

        #endregion

        #region Validation

        /// <inheritdoc/>
        public override Task<Dictionary<string, string>?> ValidateCreateInputAsync(ProductCreateInputDto dto)
        {
            return ValidateAccountAsync(dto);
        }

        /// <inheritdoc/>
        public override Task<Dictionary<string, string>?> ValidateUpdateInputAsync(ProductUpdateInputDto dto, Product original)
        {
            return ValidateAccountAsync(dto, original);
        }

        private async Task<Dictionary<string, string>?> ValidateAccountAsync(ProductUpdateInputDto dto, Product? original = null)
        {
            if (dto.Code != original?.Code && await EntityDbSet.AnyAsync(p => p.Code == dto.Code))
            {
                return OneErrorDictionary(nameof(dto.Code), $"Product code already exists.");
            }

            var errors = await ValidateName(dto.Name!, original?.Name);

            errors ??= await ValidateId(AppDbContext.Categories, dto.CategoryId, original?.CategoryId);

            errors ??= dto.SalesInformation is not null
                ? await ValidateId(AppDbContext.Accounts, dto.SalesInformation.AccountId, original?.SalesInformation?.AccountId)
                : null;
            errors ??= dto.PurchasingInformation is not null
                ? await ValidateId(AppDbContext.Accounts, dto.PurchasingInformation.AccountId, original?.PurchasingInformation?.AccountId)
                : null;

            errors ??= dto.InventoryDetails is not null
                ? await ValidateId(AppDbContext.Accounts, dto.InventoryDetails.InventoryAccountId, original?.InventoryDetails?.InventoryAccountId)
                : null;
            errors ??= dto.InventoryDetails is not null
                ? await ValidateId(AppDbContext.ScaleUnits, dto.InventoryDetails.ScaleUnitId, original?.InventoryDetails?.ScaleUnitId)
                : null;

            errors ??= dto is ProductCreateInputDto cDto 
                ? await ValidateId(AppDbContext.Businesses, cDto.BusinessId) 
                : null;

            errors ??= dto.Category is not null
                ? await ValidateName(AppDbContext.Categories, dto.Category.Name!, original?.Category?.Name)
                : null;

            return errors;
        }

        #endregion


        #region Helper Methods

        private IQueryable<Product> QueryIncludingInfo()
        {
            return EntityDbSet
                .Include(p => p.SalesInformation)
                    .ThenInclude(i => i!.Account)
                .Include(p => p.PurchasingInformation)
                    .ThenInclude(i => i!.Account)
                .Include(p => p.InventoryDetails)
                    .ThenInclude(i => i!.InventoryAccount)
                .Include(p => p.InventoryDetails)
                    .ThenInclude(i => i!.ScaleUnit)
                .AsSplitQuery();
        }

        #endregion

    }
}
