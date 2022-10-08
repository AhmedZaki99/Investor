using InvestorAPI.Core;
using InvestorData;
using Microsoft.AspNetCore.Mvc;

namespace InvestorAPI.Controllers
{
    [Route("api/products/categories")]
    public class CategoryController : EntityController<Category, CategoryOutputDto, CategoryCreateInputDto, CategoryUpdateInputDto>
    {

        #region Dependencies

        private readonly ICategoryService _categoryService;

        #endregion

        #region Constructor

        public CategoryController(ICategoryService categoryService) : base(categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion


        #region Controller Actions

        /// <summary>
        /// Get products' categories by business id.
        /// </summary>
        [HttpGet]
        public IAsyncEnumerable<CategoryOutputDto> GetCategoriesAsync([FromQuery] string? businessId = null)
        {
            return businessId is null
                ? _categoryService.GetEntitiesAsync()
                : _categoryService.GetEntitiesAsync(businessId);
        }

        #endregion

    }
}
