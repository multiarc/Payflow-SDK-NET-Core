#region "Imports"

#endregion

namespace PFProSDK.DataObjects
{
    /// <summary>
    ///     This class is used to store the complete information
    ///     about a client information header.
    /// </summary>
    internal sealed class ClientInfoHeader : BaseRequestDataObject
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="headerName">Header name</param>
        /// <param name="headerValue">Header value</param>
        internal ClientInfoHeader(string headerName, object headerValue)
        {
            HeaderName = headerName;
            HeaderValue = headerValue;
        }

        /// <summary>
        ///     Gets header name
        /// </summary>
        internal string HeaderName { get; }

        /// <summary>
        ///     Gets header value
        /// </summary>
        internal object HeaderValue { get; }
    }
}