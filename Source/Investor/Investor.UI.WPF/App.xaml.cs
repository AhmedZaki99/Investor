using Investor.UI.Core;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Investor.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        #region Private Fields

        private bool _appShuttingDown = false;

        #endregion


        #region Dependencies

        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly IApplicationCore _applicationCore;

        #endregion

        #region Constructors

        public App(IHostApplicationLifetime applicationLifetime, IApplicationCore applicationCore)
        {
            _applicationLifetime = applicationLifetime;
            _applicationCore = applicationCore;

            applicationLifetime.ApplicationStopping.Register(HostStopping);

            InitializeComponent();
        }

        #endregion


        #region Host Event Handlers

        private void HostStopping()
        {
            // Check if application is already shutting down.
            if (!_appShuttingDown)
            {
                // Shutdown the application when host stops.
                this.Shutdown();
            }
        }

        #endregion

        #region Application Event Handlers

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Startup Application.
            _applicationCore.StartupApplication();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Set a flag to announce that application is shutting down.
            _appShuttingDown = true;

            base.OnExit(e);

            // Check if the host is already stopping.
            if (!_applicationLifetime.ApplicationStopping.IsCancellationRequested)
            {
                // In case the application itself is exiting first; nevertheless,
                // the host should remain the source of lifetime management.
                _applicationLifetime.StopApplication();
            }
        }

        #endregion

    }
}
