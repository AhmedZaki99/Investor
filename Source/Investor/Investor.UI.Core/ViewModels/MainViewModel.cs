using CommunityToolkit.Mvvm.Input;
using Investor.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

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

        private ObservableCollection<IBrandViewModel> _brands;
        public ObservableCollection<IBrandViewModel> Brands
        {
            get => _brands;
            set => SetProperty(ref _brands, value);
        }

        private string _localStatus;
        public string LocalStatus
        {
            get => _localStatus;
            set => SetProperty(ref _localStatus, value);
        }

        #endregion

        #region Commands

        public ICommand CloseApplicationCommand { get; private set; }
        public ICommand GetBrandsCommand { get; private set; }

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

            // Properites
            _brands = new();
            _localStatus = "Ready.";

            InitializeCommands();
        }

        #endregion

        #region Initialization

        [MemberNotNull(nameof(CloseApplicationCommand))]
        [MemberNotNull(nameof(GetBrandsCommand))]
        private void InitializeCommands()
        {
            CloseApplicationCommand = new RelayCommand(CloseApplication);
            GetBrandsCommand = new AsyncRelayCommand(GetBrandsAsync);
        }

        #endregion


        #region Commands Actions

        private void CloseApplication()
        {
            _applicationLifetime.StopApplication();
        }

        private async Task GetBrandsAsync()
        {
            LocalStatus = "Getting brands...";
            try
            {
                var brands = await _brandEndpoint.PaginateAsync();

                Brands = new(brands.Select(b => new BrandViewModel(b)));
                LocalStatus = "Ready.";
            }
            catch (ApiConnectionException ex)
            {
                //if (Debugger.IsAttached)
                //{
                //    Debugger.Break();
                //    throw;
                //}

                _logger.LogWarning(
                    "Failed to connect to the Api server, failure type: {failureType}, status code: {statusCode}, error message: {errMsg}",
                    ex.FailureType, ex.StatusCode is null ? "N/A" : ex.StatusCode, ex.Message);

                LocalStatus = "Failed to get brands, please check your internet connection and try again.";
            }
        }

        #endregion


        #region Helper Methods



        #endregion

    }
}
