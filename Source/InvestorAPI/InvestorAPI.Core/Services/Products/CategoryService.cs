using AutoMapper;
using InvestorAPI.Data;
using InvestorData;
using Microsoft.EntityFrameworkCore;

namespace InvestorAPI.Core
{
    /// <summary>
    /// A service responsible for handling and processing <see cref="Category"/> data.
    /// </summary>
    internal class CategoryService : EntityService<Category, CategoryOutputDto, CategortCreateInputDto, CategoryUpdateInputDto>, ICategoryService
    {

        #region Constructor

        public CategoryService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, dbContext.Categories, mapper)
        {

        }

        #endregion


        #region Validation

        /// <inheritdoc/>
        public override Task<Dictionary<string, string>?> ValidateCreateInputAsync(CategortCreateInputDto dto)
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
            // TODO: Add a generalized logic to check for unique names.

            if (dto.Name != original?.Name && await EntityDbSet.AnyAsync(c => c.Name == dto.Name))
            {
                return OneErrorDictionary(nameof(dto.Name), "Category name already exists.");
            }

            return dto is CategortCreateInputDto cDto ? await ValidateId(AppDbContext.Businesses, cDto.BusinessId) : null;
        }

        #endregion

    }
}
