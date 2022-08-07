using Investor.Core;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Investor.UI.Core.ViewModels
{
    public class BrandViewModel : ViewModelBase<BrandModel>, IBrandViewModel
    {

        #region Public Properties

        public bool IsModified { get; private set; }

        #endregion

        #region Observable Properties

        [Required(ErrorMessage = "Brand name is required.")]
        [MaxLength(100, ErrorMessage = "Name length shouldn't exceed {1} characters.")]
        public string Name
        {
            get => Model.Name;
            set => SetProperty(Model.Name, value, Model, (model, val) => model.Name = val, true);
        }

        [MaxLength(100, ErrorMessage = "Scale unit length shouldn't exceed {1} characters.")]
        public string ScaleUnit
        {
            get => Model.ScaleUnit;
            set => SetProperty(Model.ScaleUnit, value, Model, (model, val) => model.ScaleUnit = val, true);
        }


        [MaxLength(1000, ErrorMessage = "Description length shouldn't exceed {1} characters.")]
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
            IsModified = false;
        }

        #endregion


        #region Overridden Methods

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            IsModified = true;

            base.OnPropertyChanged(e);
        }

        #endregion

    }
}
