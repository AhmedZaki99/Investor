namespace Investor.UI.Core
{
    /// <summary>
    /// Provides an abstraction for core application functionality.
    /// </summary>
    public interface IApplicationCore
    {

        /// <summary>
        /// Starting up the application.
        /// </summary>
        /// <remarks>
        /// This method should be only called once.
        /// </remarks>
        /// <exception cref="InvalidOperationException"/>
        void StartupApplication();

    }
}
