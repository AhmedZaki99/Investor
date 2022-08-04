using Investor.Core;
using Microsoft.Extensions.Hosting;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Investor.UI.Core.ViewModels
{
    /// <summary>
    /// The bottom-level view model for application main view.
    /// </summary>
    public class MainViewModel : ViewModelBase, IMainViewModel
    {

        #region Private Fields



        #endregion


        #region Unchanged Properties



        #endregion

        #region Observable Properties

        private string _brandName = string.Empty;
        public string BrandName
        {
            get => _brandName;
            set => SetProperty(ref _brandName, value);
        }


        #endregion

        #region Commands

        public ICommand CloseApplicationCommand { get; private set; }
        public ICommand ShowBrandCommand { get; private set; }

        #endregion


        #region Dependencies

        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly IBrandEndpoint _brandEndpoint;
        private readonly ILogger<MainViewModel> _logger;

        #endregion

        #region Constructors

        public MainViewModel(IHostApplicationLifetime applicationLifetime, IBrandEndpoint brandEndpoint, ILogger<MainViewModel> logger)
        {
            // Dependencies.
            _applicationLifetime = applicationLifetime;
            _brandEndpoint = brandEndpoint;
            _logger = logger;

            InitializeCommands();
        }

        #endregion

        #region Initialization

        [MemberNotNull(nameof(CloseApplicationCommand))]
        [MemberNotNull(nameof(ShowBrandCommand))]
        private void InitializeCommands()
        {
            CloseApplicationCommand = new RelayCommand(CloseApplication);
            ShowBrandCommand = new AsyncRelayCommand(ShowBrandAsync);
        }

        #endregion


        #region Commands Actions

        private void CloseApplication()
        {
            _applicationLifetime.StopApplication();
        }

        private async Task ShowBrandAsync()
        {
            BrandName = "Getting Brand...";
            try
            {
                var brands = await _brandEndpoint.PaginateAsync();

                BrandName = brands?.FirstOrDefault()?.Name ?? "No result.";
            }
            catch (ApiConnectionException ex)
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                    throw;
                }

                _logger.LogWarning(
                    "Failed to connect to the Api server, failure type: {failureType}, status code: {statusCode}, error message: {errMsg}",
                    ex.FailureType, ex.StatusCode is null ? "N/A" : ex.StatusCode, ex.Message);

                BrandName = "Failed to get the brand, please check your internet connection and try again.";
            }
        }

        #endregion


        #region Helper Methods



        #endregion

    }
}
