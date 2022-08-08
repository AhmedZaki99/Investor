namespace Investor.UI.Core.ViewModels
{
    /// <summary>
    /// Provides an abstraction for generic page view models.
    /// </summary>
    public interface IPageViewModel : IViewModel
    {

        #region Unchanged Properties

        IViewModel Parent { get; }

        #endregion

        #region Observable Properties

        string Title { get; set; }

        bool IsSelected { get; set; }

        #endregion

    }
}
