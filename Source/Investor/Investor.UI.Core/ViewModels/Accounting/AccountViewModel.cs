using InvestorData;
using System.ComponentModel.DataAnnotations;

namespace Investor.UI.Core.ViewModels
{
    public class AccountViewModel : ViewModelBase<Account>, IAccountViewModel
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

        public AccountType AccountType
        {
            get => Model.AccountType;
            set => SetProperty(Model.AccountType, value, Model, (model, val) => model.AccountType = val, true);
        }

        public decimal? Balance
        {
            get => Model.Balance;
            set => SetProperty(Model.Balance, value, Model, (model, val) => model.Balance = val, true);
        }

        #endregion


        #region Constructor

        public AccountViewModel(Account model) : base(model)
        {

        }

        #endregion

    }
}
