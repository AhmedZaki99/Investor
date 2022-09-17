using System.Windows;

namespace Investor.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        #region Properties

        public bool AppShuttingDown { get; private set; }

        #endregion


        #region Constructors

        public App()
        {
            AppShuttingDown = false;

            InitializeComponent();
        }

        #endregion


        #region Application Event Handlers

        protected override void OnExit(ExitEventArgs e)
        {
            // Set a flag to announce that application is shutting down.
            AppShuttingDown = true;

            base.OnExit(e);
        }

        #endregion

    }
}
