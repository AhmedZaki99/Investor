using Investor.UI.Core.ViewModels;
using Microsoft.Extensions.Hosting;

namespace Investor.UI.Core
{
    /// <summary>
    /// Provides core application functionality.
    /// </summary>
    public class ApplicationCore : IApplicationCore
    {

        #region Private Fields

        private bool _applicationStartedup = false;

        #endregion

        #region Private Properties

        private MainViewModel? _mainViewModel;
        private MainViewModel MainViewModel
        {
            get => _mainViewModel ?? throw new InvalidOperationException("Application hasn't started up yet.");
            set => _mainViewModel = value;
        }

        #endregion


        #region Dependencies

        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly IUIService _uIService;

        #endregion

        #region Constructors

        public ApplicationCore(IHostApplicationLifetime applicationLifetime, IUIService uIService)
        {
            _applicationLifetime = applicationLifetime;
            _uIService = uIService;
        }

        #endregion

        #region Application Startup

        /// <inheritdoc/>
        public void StartupApplication()
        {
            if (_applicationStartedup)
            {
                throw new InvalidOperationException("Application has already started up.");
            }

            // Create the main viewmodel instance.
            MainViewModel = new MainViewModel(_applicationLifetime);

            // Initialize UI with the main viewmodel.
            _uIService.InitializeUI(MainViewModel);

            // Show UI at startup.
            _uIService.ShowUI();

            _applicationStartedup = true;
        }
        
        #endregion

    }
}
