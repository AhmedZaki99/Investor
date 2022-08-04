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

        string LocalStatus { get; set; }

        #endregion

        #region Commands

        ICommand CloseApplicationCommand { get; }
        ICommand GetBrandsCommand { get; }

        #endregion

    }
}
