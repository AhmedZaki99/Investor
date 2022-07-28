using Investor.UI.Core.ViewModels;

namespace Investor.UI.Core
{
    /// <summary>
    /// Provides an abstraction for UI-related functionality.
    /// </summary>
    public interface IUIService
    {

        #region Properties

        /// <summary>
        /// Gets a value that determines wheather the UI is visible.
        /// </summary>
        bool IsUIVisible { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the UI, binding it to the provided <see cref="MainViewModel"/> instance.
        /// </summary>
        /// <param name="mainViewModel">The <see cref="MainViewModel"/> instance to bind to the UI.</param>
        void InitializeUI(MainViewModel mainViewModel);


        /// <summary>
        /// Bring the UI into view.
        /// </summary>
        void ShowUI();

        /// <summary>
        /// Hide the UI from the view.
        /// </summary>
        void HideUI();

        #endregion

    }
}
