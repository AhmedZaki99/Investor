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

        ObservableCollection<IBrandViewModel> Brands { get; set; }
        IBrandViewModel? SelectedBrand { get; set; }

        IEnumerable<string?> InputErrors { get; }

        bool AddingNewBrand { get; set; }
        string LocalStatus { get; set; }

        #endregion

        #region Commands

        ICommand CloseApplicationCommand { get; }

        ICommand ToggleAddBrandCommand { get; }

        ICommand GetBrandsCommand { get; }
        IRelayCommand AddBrandCommand { get; }
        IRelayCommand SaveBrandCommand { get; }
        IRelayCommand DeleteBrandCommand { get; }

        #endregion

    }
}
