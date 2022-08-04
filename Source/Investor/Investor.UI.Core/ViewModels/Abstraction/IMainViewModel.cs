using System.Windows.Input;

namespace Investor.UI.Core.ViewModels
{
    /// <summary>
    /// Provides an abstraction for the bottom-level view model for application main view.
    /// </summary>
    public interface IMainViewModel : IViewModel
    {

        #region Observable Properties

        string BrandName { get; set; }

        #endregion

        #region Commands

        ICommand CloseApplicationCommand { get; }
        ICommand ShowBrandCommand { get; }

        #endregion

    }
}
