namespace Investor.UI.WPF
{
    internal interface IWpfService
    {

        /// <summary>
        /// Runs the Wpf application, blocking the thread until app termination.
        /// </summary>
        /// <returns>Application exit code.</returns>
        int RunApplication();

    }
}
