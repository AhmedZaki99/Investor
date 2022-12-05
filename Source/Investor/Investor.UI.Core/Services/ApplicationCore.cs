using Investor.UI.Core.ViewModels;

namespace Investor.UI.Core
{
    /// <summary>
    /// Provides core application functionality.
    /// </summary>
    internal class ApplicationCore : IApplicationCore
    {

        #region Private Fields

        private bool _applicationStartedup = false;

        #endregion


        #region Dependencies

        private readonly IMainViewModel _mainViewModel;
        private readonly IUIService _uIService;

        #endregion

        #region Constructors

        public ApplicationCore(IMainViewModel mainViewModel, IUIService uIService)
        {
            _mainViewModel = mainViewModel;
            _uIService = uIService;
        }

        #endregion

        #region Application Startup

        /// <inheritdoc/>
        public async Task StartupApplication()
        {
            if (_applicationStartedup)
            {
                throw new InvalidOperationException("Application has already started up.");
            }

            // Initialize UI with the main viewmodel.
            _uIService.InitializeUI(_mainViewModel);

            // Show UI at startup.
            _uIService.ShowUI();

            // Load viewmodel data.
            await _mainViewModel.FetchData();

            _applicationStartedup = true;
        }
        
        #endregion

    }
}
