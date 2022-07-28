using Investor.UI.Core;
using Investor.UI.Core.ViewModels;
using System;
using System.Windows;

namespace Investor.UI.WPF
{
    /// <summary>
    /// Provides UI-related functionality.
    /// </summary>
    internal class UIService : IUIService
    {

        #region Private Properties

        private Window? _mainWindow;
        private Window MainWindow
        {
            get => _mainWindow ?? throw new InvalidOperationException("UI hasn't been initialized.");
            set => _mainWindow = value;
        }

        #endregion

        #region Public Properties

        /// <inheritdoc/>
        public bool IsUIVisible => MainWindow.IsVisible;

        #endregion


        #region Constructor



        #endregion


        #region Methods

        /// <inheritdoc/>
        public void InitializeUI(MainViewModel mainViewModel)
        {
            MainWindow = new MainView
            {
                DataContext = mainViewModel
            };
        }


        /// <inheritdoc/>
        public void ShowUI() => MainWindow.Dispatcher.Invoke(MainWindow.Show);

        /// <inheritdoc/>
        public void HideUI() => MainWindow.Dispatcher.Invoke(MainWindow.Hide);

        #endregion
    }
}
