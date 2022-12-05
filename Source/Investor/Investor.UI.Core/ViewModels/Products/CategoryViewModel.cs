using InvestorData;
using System.ComponentModel.DataAnnotations;

namespace Investor.UI.Core.ViewModels
{
    public class CategoryViewModel : ViewModelBase<Category>, ICategoryViewModel
    {

        #region Observable Properties

        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(200, ErrorMessage = "{0} length shouldn't exceed {1} characters.")]
        public string Name
        {
            get => Model.Name;
            set => SetProperty(Model.Name, value, Model, (model, val) => model.Name = val, true);
        }

        [MaxLength(1000, ErrorMessage = "{0} length shouldn't exceed {1} characters.")]
        public string? Description
        {
            get => Model.Description;
            set => SetProperty(Model.Description, value, Model, (model, val) => model.Description = val, true);
        }

        #endregion


        #region Constructor

        public CategoryViewModel(Category model) : base(model)
        {

        }

        #endregion

    }
}
