#region "Imports"

using System.Collections;
using PFProSDK.Common.Utility;

#endregion

namespace PFProSDK.DataObjects
{
    /// <summary>
    ///     This class is used to store the
    ///     Payflow Client related properties
    /// </summary>
    public sealed class ClientInfo : BaseRequestDataObject
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public ClientInfo()
        {
            ClientInfoHash = new Hashtable();
        }

        #region "Properties"

        /// <summary>
        ///     Gets the client info hash table
        /// </summary>
        internal Hashtable ClientInfoHash { get; private set; }

        /// <summary>
        ///     Gets client version
        /// </summary>
        /// <remarks>
        ///     <para>Client version.</para>
        ///     <para>Maps to HTTP Header:</para>
        ///     <code>PAYFLOW-CLIENT-VERSION</code>
        /// </remarks>
        public string ClientVersion => (string) GetHeaderFromHash(PayflowConstants.PayflowheaderClientVersion);

        /// <summary>
        ///     Gets, sets OS architecture
        /// </summary>
        internal string OsArchitecture
        {
            get => (string) GetHeaderFromHash(PayflowConstants.PayflowheaderOsArchitecture);
            set => AddHeaderToHash(PayflowConstants.PayflowheaderOsArchitecture, value);
        }

        /// <summary>
        ///     Gets, sets OS version
        /// </summary>
        internal string OsVersion
        {
            get => (string) GetHeaderFromHash(PayflowConstants.PayflowheaderOsVersion);
            set => AddHeaderToHash(PayflowConstants.PayflowheaderOsVersion, value);
        }

        /// <summary>
        ///     Gets, sets OS Name
        /// </summary>
        internal string OsName
        {
            get => (string) GetHeaderFromHash(PayflowConstants.PayflowheaderOsName);
            set => AddHeaderToHash(PayflowConstants.PayflowheaderOsName, value);
        }

        /// <summary>
        ///     Gets, sets Proxy
        /// </summary>
        internal string Proxy
        {
            get => (string) GetHeaderFromHash(PayflowConstants.PayflowheaderProxy);
            set => AddHeaderToHash(PayflowConstants.PayflowheaderProxy, value);
        }

        /// <summary>
        ///     Gets, sets runtime version
        /// </summary>
        internal string RunTimeVersion
        {
            get => (string) GetHeaderFromHash(PayflowConstants.PayflowheaderRuntimeVersion);
            set => AddHeaderToHash(PayflowConstants.PayflowheaderRuntimeVersion, value);
        }

        /// <summary>
        ///     Sets integration product
        /// </summary>
        /// <remarks>
        ///     <para>Integration product.</para>
        ///     <para>Maps to HTTP Header:</para>
        ///     <code>PAYFLOW-INTEGRATION-PRODUCT</code>
        /// </remarks>
        public string IntegrationProduct
        {
            set => AddHeaderToHash(PayflowConstants.PayflowheaderIntegrationProduct, value);
        }

        /// <summary>
        ///     Sets integration version
        /// </summary>
        /// <remarks>
        ///     <para>Integration product.</para>
        ///     <para>Maps to HTTP Header:</para>
        ///     <code>PAYFLOW-INTEGRATION-VERSION</code>
        /// </remarks>
        public string IntegrationVersion
        {
            set => AddHeaderToHash(PayflowConstants.PayflowheaderIntegrationVersion, value);
        }

        /// <summary>
        ///     Gets Client Type
        /// </summary>
        /// <remarks>
        ///     <para>Client type.</para>
        ///     <para>Maps to HTTP Header:</para>
        ///     <code>PAYFLOW-CLIENT-TYPE</code>
        /// </remarks>
        public string ClientType => (string) GetHeaderFromHash(PayflowConstants.PayflowheaderClientType);

        /// <summary>
        ///     Sets Request Type
        /// </summary>
        /// <remarks>
        ///     <para>Request type.</para>
        ///     <para>Maps to HTTP Header:</para>
        ///     <code>PAYFLOW-ASSEMBLY</code>
        /// </remarks>
        internal string RequestType
        {
            set => AddHeaderToHash(PayflowConstants.PayflowheaderAssembly, value);
        }

        /// <summary>
        ///     Sets client version
        /// </summary>
        /// <param name="version">String value of client version</param>
        internal void SetClientVersion(string version)
        {
            AddHeaderToHash(PayflowConstants.PayflowheaderClientVersion, version);
        }

        /// <summary>
        ///     Gets integration product
        /// </summary>
        /// <returns>String value of integration product</returns>
        internal string GetIntegrationProduct()
        {
            return (string) GetHeaderFromHash(PayflowConstants.PayflowheaderIntegrationProduct);
        }

        /// <summary>
        ///     Gets integration version
        /// </summary>
        /// <returns>String value of integration version</returns>
        internal string GetIntegrationVersion()
        {
            return (string) GetHeaderFromHash(PayflowConstants.PayflowheaderIntegrationVersion);
        }

        /// <summary>
        ///     Sets client type
        /// </summary>
        /// <param name="clientType">String value of Client Type</param>
        internal void SetClientType(string clientType)
        {
            AddHeaderToHash(PayflowConstants.PayflowheaderClientType, clientType);
        }

        #endregion

        #region "Private and internal methods"

        /// <summary>
        ///     Adds a header to the header hash table
        /// </summary>
        /// <param name="headerName">Header name</param>
        /// <param name="headerValue">Header value</param>
        internal void AddHeaderToHash(string headerName, object headerValue)
        {
            // Null Header Names & Values are not allowed.
            // Empty Header Names are not allowed.
            if (headerName == null || headerName.Length == 0 || headerValue == null) return;

            var currHeader
                = new ClientInfoHeader(headerName, headerValue);

            if (ClientInfoHash == null) ClientInfoHash = new Hashtable();

            if (ClientInfoHash.ContainsKey(headerName)) ClientInfoHash.Remove(headerName);

            ClientInfoHash.Add(headerName, currHeader);
        }

        /// <summary>
        ///     Gets a header value from hash
        /// </summary>
        /// <param name="headerName">Header name</param>
        /// <returns>Header value object</returns>
        internal object GetHeaderFromHash(string headerName)
        {
            if (ClientInfoHash == null) return null;

            var currHeader = (ClientInfoHeader) ClientInfoHash[headerName];

            if (currHeader == null) return null;

            return currHeader.HeaderValue;
        }

        #endregion
    }
}