using Investor.Core;
using System.ComponentModel.DataAnnotations;

namespace Investor.UI.Core.ViewModels
{
    public class BrandViewModel : ViewModelBase<BrandModel>, IBrandViewModel
    {


        #region Observable Properties

        [Required(ErrorMessage = "Brand name is required.")]
        [MaxLength(100, ErrorMessage = "Brand name should not be more than {1} characters long.")]
        public string Name
        {
            get => Model.Name;
            set => SetProperty(Model.Name, value, Model, (model, val) => model.Name = val, true);
        }

        [MaxLength(100, ErrorMessage = "Brand scale unit should not be more than {1} characters long.")]
        public string ScaleUnit
        {
            get => Model.ScaleUnit;
            set => SetProperty(Model.ScaleUnit, value, Model, (model, val) => model.ScaleUnit = val, true);
        }


        [MaxLength(1000, ErrorMessage = "Brand description should not exceed {1} characters in length.")]
        public string? Description
        {
            get => Model.Description;
            set => SetProperty(Model.Description, value, Model, (model, val) => model.Description = val, true);
        }


        public decimal? BuyPrice
        {
            get => Model.BuyPrice;
            set => SetProperty(Model.BuyPrice, value, Model, (model, val) => model.BuyPrice = val, true);
        }

        public decimal? SellPrice
        {
            get => Model.SellPrice;
            set => SetProperty(Model.SellPrice, value, Model, (model, val) => model.SellPrice = val, true);
        }

        #endregion


        #region Constructor

        public BrandViewModel(BrandModel model) : base(model)
        {

        }

        #endregion

    }
}
