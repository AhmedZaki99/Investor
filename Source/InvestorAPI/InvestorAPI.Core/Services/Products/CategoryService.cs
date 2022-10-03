using AutoMapper;
using InvestorAPI.Data;
using InvestorData;

namespace InvestorAPI.Core
{
    /// <summary>
    /// A service responsible for handling and processing <see cref="Category"/> data.
    /// </summary>
    internal class CategoryService : EntityService<Category, CategoryOutputDto, CategoryCreateInputDto, CategoryUpdateInputDto>, ICategoryService
    {

        #region Constructor

        public CategoryService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, dbContext.Categories, mapper)
        {

        }

        #endregion


        #region Read

        /// <inheritdoc/>
        public IAsyncEnumerable<CategoryOutputDto> GetEntitiesAsync(string businessId)
        {
            ArgumentNullException.ThrowIfNull(businessId, nameof(businessId));

            return GetEntitiesAsync(c => c.BusinessId == null || c.BusinessId == businessId);
        }

        #endregion


        #region Validation

        /// <inheritdoc/>
        public override Task<Dictionary<string, string>?> ValidateCreateInputAsync(CategoryCreateInputDto dto)
        {
            return ValidateInputAsync(dto);
        }

        /// <inheritdoc/>
        public override Task<Dictionary<string, string>?> ValidateUpdateInputAsync(CategoryUpdateInputDto dto, Category original)
        {
            return ValidateInputAsync(dto, original);
        }

        private async Task<Dictionary<string, string>?> ValidateInputAsync(CategoryUpdateInputDto dto, Category? original = null)
        {
            var errors = await ValidateName(dto.Name!, original?.Name);

            errors ??= dto is CategoryCreateInputDto cDto 
                ? await ValidateId(AppDbContext.Businesses, cDto.BusinessId) 
                : null;

            return errors;
        }

        #endregion

    }
}
