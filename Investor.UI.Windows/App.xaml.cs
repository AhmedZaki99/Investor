using Investor.UI.Core;
using System.Windows;

namespace Investor.UI.Windows
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Setup IoC container.
            IoCContainer.Setup();

            // Initialize the main window.
            Current.MainWindow = new MainWindow
            {
                DataContext = IoCContainer.Get<MainViewModel>()
            };
            Current.MainWindow.Show();
        }

    }
}
