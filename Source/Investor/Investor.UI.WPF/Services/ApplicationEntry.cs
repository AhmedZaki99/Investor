using Investor.UI.Core;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace Investor.UI.WPF
{
    /// <summary>
    /// Provides application entry and lifetime management functionality.
    /// </summary>
    internal sealed class ApplicationEntry : IApplicationEntry
    {

        #region Private Properites

        private App? _app;
        private App App
        {
            get => _app ?? throw new InvalidOperationException("Application has not started yet.");
            set => _app = value;
        }

        #endregion


        #region Dependencies

        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly IApplicationCore _applicationCore;

        #endregion

        #region Constructor

        public ApplicationEntry(IHostApplicationLifetime applicationLifetime, IApplicationCore applicationCore)
        {
            // Dependencies..
            _applicationLifetime = applicationLifetime;
            _applicationCore = applicationCore;

            applicationLifetime.ApplicationStopping.Register(HostStopping);
        }

        #endregion


        #region Host Event Handlers

        private void HostStopping()
        {
            // Check if application is already shutting down.
            if (!App.AppShuttingDown)
            {
                // Shutdown the application when host stops.
                App.Shutdown();
            }
        }

        #endregion

        #region App Event Handlers

        private async void OnAppStartup(object sender, StartupEventArgs e)
        {
            // Startup Application.
            await _applicationCore.StartupApplication();
        }

        private void OnAppExit(object sender, ExitEventArgs e)
        {
            // Check if the host is already stopping.
            if (!_applicationLifetime.ApplicationStopping.IsCancellationRequested)
            {
                // In case the application itself is exiting first; nevertheless,
                // the host should remain the source of lifetime management.
                _applicationLifetime.StopApplication();
            }
        }

        #endregion


        #region Public Methods

        /// <inheritdoc/>
        public int RunApplication()
        {
            // Initialize application.
            App = new App();

            App.Startup += OnAppStartup;
            App.Exit += OnAppExit;

            // Run the application and block until termination.
            return App.Run();
        }

        #endregion

    }
}
