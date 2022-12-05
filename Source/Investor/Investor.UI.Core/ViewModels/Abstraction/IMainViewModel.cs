using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Investor.UI.Core.ViewModels
{
    /// <summary>
    /// Provides an abstraction for the bottom-level view model for application main view.
    /// </summary>
    public interface IMainViewModel : IViewModel
    {

        #region Observable Properties

        ObservableCollection<ICategoryViewModel> Categories { get; set; }
        ObservableCollection<IAccountViewModel> Accounts { get; set; }

        ObservableCollection<IProductViewModel> Products { get; set; }
        IProductViewModel? SelectedProduct { get; set; }

        IEnumerable<string?> InputErrors { get; }

        bool AddingNewProduct { get; set; }
        string LocalStatus { get; set; }

        #endregion

        #region Commands

        ICommand CloseApplicationCommand { get; }

        ICommand ToggleAddProductCommand { get; }

        ICommand GetProductsCommand { get; }
        IRelayCommand AddProductCommand { get; }
        IRelayCommand SaveProductCommand { get; }
        IRelayCommand DeleteProductCommand { get; }

        #endregion


        #region Methods

        Task FetchData();

        #endregion

    }
}
