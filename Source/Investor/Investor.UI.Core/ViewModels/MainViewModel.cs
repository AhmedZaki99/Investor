using Investor.Core;
using Microsoft.Extensions.Hosting;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using System.Diagnostics;

namespace Investor.UI.Core.ViewModels
{
    /// <summary>
    /// The bottom-level view model for application main window.
    /// </summary>
    public class MainViewModel : BaseViewModel
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

        #endregion

        #region Constructors

        #region Design Constructor

        /// <summary>
        /// A private parameterless constructor exposed to be inherited by design instances.
        /// </summary>
#nullable disable
        private protected MainViewModel()
#nullable enable
        { }

        #endregion

        public MainViewModel(IHostApplicationLifetime applicationLifetime, IBrandEndpoint brandEndpoint)
        {
            // Dependencies.
            _applicationLifetime = applicationLifetime;
            _brandEndpoint = brandEndpoint;

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
                BrandName = ex.Message;
            }
        }

        #endregion


        #region Helper Methods



        #endregion

    }
}
