namespace Investor.UI.WPF
{
    /// <summary>
    /// Provides an abstraction for application entry and lifetime management functionality.
    /// </summary>
    internal interface IApplicationEntry
    {

        /// <summary>
        /// Runs the Wpf application, blocking the thread until app termination.
        /// </summary>
        /// <returns>Application exit code.</returns>
        int RunApplication();

    }
}
