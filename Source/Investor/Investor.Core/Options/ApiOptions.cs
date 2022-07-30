namespace Investor.Core
{
    internal class ApiOptions
    {
        /// <summary>
        /// Gets or sets the base Api server url.
        /// </summary>
        public string ApiServerAddress { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the relative path for Api endpoints.
        /// </summary>
        public string ApiRelativePath { get; set; } = "api";
    }
}
