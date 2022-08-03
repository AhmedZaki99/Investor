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
        /// Initializes the UI, binding it to the provided <see cref="IMainViewModel"/> implementation.
        /// </summary>
        /// <param name="mainViewModel">The <see cref="IMainViewModel"/> instance to bind to the UI.</param>
        void InitializeUI(IMainViewModel mainViewModel);


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
