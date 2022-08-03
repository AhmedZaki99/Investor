namespace Investor.UI.Core.ViewModels
{
    /// <summary>
    /// Provides an abstraction for a base class that provides basic functionality for page view models.
    /// </summary>
    public interface IPageViewModel : IBaseViewModel
    {

        #region Unchanged Properties

        IBaseViewModel Parent { get; }

        #endregion

        #region Observable Properties

        string Title { get; set; }

        bool IsSelected { get; set; }

        #endregion

    }
}
