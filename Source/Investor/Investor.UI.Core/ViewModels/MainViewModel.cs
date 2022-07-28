using Microsoft.Extensions.Hosting;
using Microsoft.Toolkit.Mvvm.Input;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

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



        #endregion

        #region Commands

        public ICommand CloseApplicationCommand { get; private set; }

        #endregion


        #region Dependencies

        private readonly IHostApplicationLifetime _applicationLifetime;

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

        public MainViewModel(IHostApplicationLifetime applicationLifetime)
        {
            // Dependencies.
            _applicationLifetime = applicationLifetime;

            InitializeCommands();
        }

        #endregion

        #region Initialization

        [MemberNotNull(nameof(CloseApplicationCommand))]
        private void InitializeCommands()
        {
            CloseApplicationCommand = new RelayCommand(CloseApplication);
        }

        #endregion


        #region Commands Actions

        private void CloseApplication()
        {
            _applicationLifetime.StopApplication();
        }

        #endregion


        #region Helper Methods



        #endregion

    }
}
