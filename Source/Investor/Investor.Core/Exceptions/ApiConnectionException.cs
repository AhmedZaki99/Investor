using Polly.Timeout;
using System.Net;

namespace Investor.Core
{
    /// <summary>
    /// Exception thrown when an attempt to connect to an Api fails.
    /// </summary>
    public class ApiConnectionException : Exception
    {

        #region Public Properties

        /// <summary>
        /// Gets the type of the failure occured.
        /// </summary>
        public ApiConnectionFailure FailureType { get; }

        /// <summary>
        /// Gets the Http status code for server response, when the failure type is <see cref="ApiConnectionFailure.ServerError"/>.
        /// </summary>
        /// <returns>
        /// An Http status code if the exception represents a non-successful result, otherwise null.
        /// </returns>
        public HttpStatusCode? StatusCode { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiConnectionException"/> class
        /// with an <see cref="HttpRequestException"/> of the connection error.
        /// </summary>
        /// <param name="requestException">The <see cref="HttpRequestException"/> of the connection error.</param>
        /// <param name="message">A message that describes the current exception.</param>
        public ApiConnectionException(HttpRequestException requestException, string message = "") : base(message, requestException)
        {
            StatusCode = requestException.StatusCode;

            FailureType = StatusCode is not null ? ApiConnectionFailure.ServerError : ApiConnectionFailure.ConnectionError;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiConnectionException"/> class
        /// with a <see cref="TimeoutRejectedException"/> of the timeout occured.
        /// </summary>
        /// <param name="timeoutException">The <see cref="TimeoutRejectedException"/> of the timeout occured.</param>
        /// <param name="message">A message that describes the current exception.</param>
        public ApiConnectionException(TimeoutRejectedException timeoutException, string message = "") : base(message, timeoutException)
        {
            FailureType = ApiConnectionFailure.Timeout;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiConnectionException"/> class
        /// with an Http status code of the server response.
        /// </summary>
        /// <param name="statusCode">The Http status code.</param>
        /// <param name="message">A message that describes the current exception.</param>
        public ApiConnectionException(HttpStatusCode statusCode, string message = "") : base(message)
        {
            StatusCode = statusCode;
            FailureType = ApiConnectionFailure.ServerError;
        }

        #endregion

    }

    /// <summary>
    /// Specifies the type of failure occured on an Api connection attempt.
    /// </summary>
    public enum ApiConnectionFailure
    {
        /// <summary>
        /// Connection timed out.
        /// </summary>
        Timeout,
        /// <summary>
        /// Internal server error.
        /// </summary>
        ServerError,
        /// <summary>
        /// Internet connection error.
        /// </summary>
        ConnectionError
    }

}
