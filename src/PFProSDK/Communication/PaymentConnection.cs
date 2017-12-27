#region "Copyright"

//PayPal Payflow Pro .NET SDK
//Copyright (C) 2014  PayPal, Inc.
//
//This file is part of the Payflow Pro .NET SDK
//
//The Payflow .NET SDK is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.
//
//The Payflow .NET SDK is is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with the Payflow .NET SDK.  If not, see <http://www.gnu.org/licenses/>.

#endregion

#region "Imports"

using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PFProSDK.Common;
using PFProSDK.Common.Logging;
using PFProSDK.Common.Utility;
using PFProSDK.DataObjects;

#endregion

namespace PFProSDK.Communication
{
    /// <summary>
    ///     This is the Connection Class.
    /// </summary>
    internal class PaymentConnection
    {
        #region "Constructor"

        /// <summary>
        ///     Constructor for PaymentConnection.
        /// </summary>
        /// <param name="psmContext">Context object by reference.</param>
        public PaymentConnection(ref Context psmContext)
        {
            ConnContext = psmContext;
        }

        #endregion

        #region "Member Variables"

        /// <summary>
        ///     Payflow  Host Address
        /// </summary>
        private string _mHostAddress;

        /// <summary>
        ///     Payflow  Host Port
        /// </summary>
        private int _mHostPort;

        /// <summary>
        ///     Payflow  Server Uri object.
        /// </summary>
        private Uri _mServerUri;

        /// <summary>
        ///     Connection object.
        /// </summary>
        private HttpWebRequest _mServerConnection;

        /// <summary>
        ///     Proxy Address.
        /// </summary>
        private string _mProxyAddress;

        /// <summary>
        ///     Proxy Port
        /// </summary>
        private int _mProxyPort;

        /// <summary>
        ///     Proxy Logon Id
        /// </summary>
        private string _mProxyLogon;

        /// <summary>
        ///     Proxy Password
        /// </summary>
        private string _mProxyPassword;

        /// <summary>
        ///     Transaction start time.
        /// </summary>
        private long _mStartTime;

        /// <summary>
        ///     Proxy Object.
        /// </summary>
        private WebProxy _mProxyInfo;

        /// <summary>
        ///     Status of proxy connection.
        ///     False if proxy host address is  not parsed successfully.
        /// </summary>
        private bool _mProxyStatus = true;

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets whether transaction
        ///     is with or without proxy.
        /// </summary>
        public bool IsProxy { get; private set; }

        /// <summary>
        ///     Gets, Sets the param list
        ///     content type.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        ///     Gets, Sets Request Id.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        ///     Gets the StartTime of the
        ///     transaction.
        /// </summary>
        public long StartTime
        {
            get
            {
                if (_mStartTime == 0) InitTransactionStartTime();
                return _mStartTime;
            }
        }

        /// <summary>
        ///     Gets, Sets the timeout
        ///     value of transaction.
        /// </summary>
        public long TimeOut { get; set; }

        /// <summary>
        ///     Gets the Connection
        ///     context object.
        /// </summary>
        internal Context ConnContext { get; }

        /// <summary>
        ///     Gets, Sets XmlPay Request type flag.
        /// </summary>
        public bool IsXmlPayRequest { get; set; }

        /// <summary>
        ///     Client information.
        /// </summary>
        public ClientInfo ClientInfo { get; set; }

        #endregion

        #region "Init functions"

        private void InitTransactionStartTime()
        {
            //Start time in mill seconds.
            _mStartTime = DateTime.Now.Ticks / 10000;
        }

        /// <summary>
        ///     Initializes Connection Host Attributes.
        /// </summary>
        /// <param name="hostAddress">String Value of Host Address.</param>
        /// <param name="hostPort">Host port as positive integer.</param>
        /// <param name="timeOut">Connection Timeout in Seconds.</param>
        private void InitializeHost(string hostAddress, int hostPort, int timeOut)
        {
            Logger.Instance.Log(
                "PayPal.Payments.Communication.PaymentConnection.InitializeHost(String,int,int): Entered.",
                PayflowConstants.SeverityDebug);

            if (hostAddress != null && hostAddress.Length > 0)
            {
                _mHostAddress = hostAddress;
                Logger.Instance.Log(
                    "PayPal.Payments.Communication.PaymentConnection.InitializeHost(String,int,int): HostAddress = " +
                    _mHostAddress,
                    PayflowConstants.SeverityInfo);
            }
            else
            {
                var nullHostError = PayflowUtility.PopulateCommError(PayflowConstants.ENullHostString, null,
                    PayflowConstants.SeverityFatal, IsXmlPayRequest,
                    null);
                if (!ConnContext.IsCommunicationErrorContained(nullHostError)) ConnContext.AddError(nullHostError);
            }

            _mHostPort = hostPort;
            Logger.Instance.Log(
                "PayPal.Payments.Communication.PaymentConnection.InitializeHost(String,int,int): HostPort = " +
                _mHostPort,
                PayflowConstants.SeverityInfo);
            TimeOut = timeOut;
            Logger.Instance.Log(
                "PayPal.Payments.Communication.PaymentConnection.InitializeHost(String,int,int): Exiting.",
                PayflowConstants.SeverityDebug);
        }

        /// <summary>
        ///     Initializes proxy Attributes
        /// </summary>
        /// <param name="proxyAddress">String Value of Proxy Address.</param>
        /// <param name="proxyPort">Proxy port as positive integer.</param>
        /// <param name="proxyLogon">String Value of Proxy User Id.</param>
        /// <param name="proxyPassword">String Value of Proxy Password.</param>
        private void InitializeProxy(string proxyAddress, int proxyPort, string proxyLogon, string proxyPassword)
        {
            Logger.Instance.Log(
                "PayPal.Payments.Communication.PaymentConnection.InitializeProxy(String,int,String, String): Entered.",
                PayflowConstants.SeverityDebug);

            _mProxyAddress = proxyAddress;
            _mProxyPort = proxyPort;
            _mProxyLogon = proxyLogon;
            _mProxyPassword = proxyPassword;

            if (_mProxyAddress != null && _mProxyAddress.Length > 0 && _mProxyPort > 0) IsProxy = true;

            Logger.Instance.Log(
                "PayPal.Payments.Communication.PaymentConnection.InitializeProxy(String,int,String, String): Exiting.",
                PayflowConstants.SeverityDebug);
        }

        /// <summary>
        ///     Initializes Connection from Connection Attributes.
        /// </summary>
        /// <param name="hostAddress">String Value of Host Address.</param>
        /// <param name="hostPort">Host port as positive integer.</param>
        /// <param name="timeOut">Connection Timeout in Seconds.</param>
        /// <param name="proxyAddress">String Value of Proxy Address. Pass null if not applicable.</param>
        /// <param name="proxyPort">Proxy port as positive integer.Pass 0 if not applicable.</param>
        /// <param name="proxyLogon">String Value of Proxy User Id.Pass null if not applicable.</param>
        /// <param name="proxyPassword">String Value of Proxy Password.Pass null if not applicable.</param>
        public void InitializeConnection(string hostAddress, int hostPort,
            int timeOut, string proxyAddress, int proxyPort,
            string proxyLogon, string proxyPassword)
        {
            Logger.Instance.Log(
                "PayPal.Payments.Communication.PaymentConnection.InitializeConnection(String,int,int,String,int,String,String): Entered.",
                PayflowConstants.SeverityDebug);
            InitializeHost(hostAddress, hostPort, timeOut);
            InitializeProxy(proxyAddress, proxyPort, proxyLogon, proxyPassword);
            Logger.Instance.Log(
                "PayPal.Payments.Communication.PaymentConnection.InitializeConnection(String,int,int,String,int,String,String): Exiting.",
                PayflowConstants.SeverityDebug);
        }


        /// <summary>
        ///     Initialized the Server Uri object from
        ///     available connection attributes.
        /// </summary>
        private void InitServerUri()
        {
            Logger.Instance.Log("PayPal.Payments.Communication.PaymentConnection.InitServerUri(String): Entered.",
                PayflowConstants.SeverityDebug);

            var serverUriBuilder = new UriBuilder();

            try
            {
                serverUriBuilder.Host = _mHostAddress;
                serverUriBuilder.Scheme = "https";
                serverUriBuilder.Port = _mHostPort;
                _mServerUri = serverUriBuilder.Uri;
            }
            catch (Exception ex)
            {
                var addlMessage = "Input Server Uri = " + serverUriBuilder.Scheme + "://" + serverUriBuilder.Host +
                                  ":" + serverUriBuilder.Port;
                if (ex is UriFormatException && IsProxy)
                {
                    addlMessage += " Input Proxy info = http://" + _mProxyAddress;
                    _mProxyStatus = false;
                }
                else if (IsProxy)
                {
                    addlMessage += " Input Proxy info = " + _mProxyInfo.Address;
                }

                var initError = PayflowUtility.PopulateCommError(PayflowConstants.ESokConnFailed, ex,
                    PayflowConstants.SeverityError, IsXmlPayRequest,
                    addlMessage);
                if (!ConnContext.IsCommunicationErrorContained(initError)) ConnContext.AddError(initError);
            }
            finally
            {
                Logger.Instance.Log("PayPal.Payments.Communication.PaymentConnection.InitServerUri(String): Exiting.",
                    PayflowConstants.SeverityDebug);
            }
        }

        /// <summary>
        ///     Initializes Proxy Object from
        ///     available proxy information.
        /// </summary>
        private void InitProxyInfo()
        {
            Logger.Instance.Log("PayPal.Payments.Communication.PaymentConnection.InitProxyInfo(): Entered.",
                PayflowConstants.SeverityDebug);
            if (IsProxy)
            {
                _mProxyInfo = new WebProxy();
                var proxyUriBuilder = new UriBuilder
                {
                    Host = _mProxyAddress,
                    Port = _mProxyPort,
                    Scheme = "http"
                };
                var proxyUri = proxyUriBuilder.Uri;
                var proxyCredential = new NetworkCredential(_mProxyLogon, _mProxyPassword);
                var proxyCredentialCache = new CredentialCache();
                proxyCredentialCache.Add(proxyUri, "Basic", proxyCredential);
                //mProxyInfo = new WebProxy();
                _mProxyInfo.Address = proxyUri;
                _mProxyInfo.Credentials = proxyCredentialCache;
                //mProxyInfo.Credentials = new NetworkCredential(mProxyLogon, mProxyPassword);
                _mProxyInfo.BypassProxyOnLocal = true;
                _mProxyInfo.UseDefaultCredentials = true;
                _mServerConnection.Proxy = _mProxyInfo;
            }

            Logger.Instance.Log("PayPal.Payments.Communication.PaymentConnection.InitProxyInfo(): Exiting.",
                PayflowConstants.SeverityDebug);
        }

        /// <summary>
        ///     Initializes all the connection attributes and creates the connection.
        /// </summary>
        private void CreateConnection()
        {
            Logger.Instance.Log("PayPal.Payments.Communication.PaymentConnection.CreateConnection(): Entered.",
                PayflowConstants.SeverityDebug);

            try
            {
                //Create Connection Object.
                _mServerConnection = (HttpWebRequest) WebRequest.Create(_mServerUri);
                // Create a new request to the above mentioned URL.    
                // WebRequest mServerConnection = WebRequest.Create(mServerUri);

                //Set Connection Properties
                _mServerConnection.Method = "POST";
                _mServerConnection.KeepAlive = false;
                _mServerConnection.UserAgent = PayflowConstants.UserAgent;
                _mServerConnection.ContentType = ContentType;
                _mServerConnection.Timeout = (int) TimeOut;
                // Add request id in the header.
                _mServerConnection.Headers.Add(PayflowConstants.PayflowheaderRequestId, RequestId);
                var timeOut = TimeOut / 1000;
                _mServerConnection.Headers.Add(PayflowConstants.PayflowheaderTimeout, timeOut.ToString());

                if (IsProxy)
                {
                    if (_mProxyInfo == null) InitProxyInfo();

                    //mServerConnection.Proxy = mProxyInfo;
                }
                else
                {
                    _mServerConnection.Proxy = WebRequest.DefaultWebProxy;
                }

                //Add VIT Headers
                if (ClientInfo != null)
                {
                    //Get the Hash map.
                    var clientInfoHash = ClientInfo.ClientInfoHash;
                    if (clientInfoHash != null && clientInfoHash.Count > 0)
                        foreach (DictionaryEntry headerKeyValue in clientInfoHash)
                        {
                            var valueObj = headerKeyValue.Value;
                            if (valueObj != null)
                            {
                                var currHeader = (ClientInfoHeader) valueObj;
                                var hdrName = currHeader.HeaderName;
                                var hdrValueObj = currHeader.HeaderValue;
                                string hdrValueStr = null;
                                //Check if Header name is non-null, non-empty string.
                                var validHeaderName = hdrName != null && hdrName.Length > 0;
                                var validHeaderValue = hdrValueObj != null;
                                //Check if Header value object is non-null, object.
                                if (validHeaderValue)
                                {
                                    hdrValueStr = currHeader.HeaderValue.ToString();
                                    //Check if the header value is non-null, non-empty.
                                    validHeaderValue = hdrValueStr != null && hdrValueStr.Length > 0;
                                }

                                //If all conditions are satisfied, then add header to request.
                                if (validHeaderName && validHeaderValue)
                                    try
                                    {
                                        _mServerConnection.Headers.Add(hdrName, hdrValueStr);
                                    }
                                    catch (Exception ex)
                                    {
                                        var addlMessage = "Invalid Client(Wrapper) Header: " + hdrName;
                                        var headerError = PayflowUtility.PopulateCommError(
                                            PayflowConstants.MessageWrapperheaderError, ex,
                                            PayflowConstants.SeverityWarn, IsXmlPayRequest,
                                            addlMessage);
                                        if (!ConnContext.IsCommunicationErrorContained(headerError))
                                            ConnContext.AddError(headerError);
                                    }
                            }
                        }
                }

                //Added VIT Headers to the http request.
            }
            catch (Exception ex)
            {
                var addlMessage = "Input Server Uri = " + _mServerUri.AbsoluteUri;

                if (ex is UriFormatException && IsProxy)
                {
                    addlMessage += " Input Proxy info = http://" + _mProxyAddress;
                    _mProxyStatus = false;
                }
                else if (IsProxy)
                {
                    addlMessage += " Input Proxy info = " + _mProxyInfo.Address;
                }

                var initError = PayflowUtility.PopulateCommError(PayflowConstants.ESokConnFailed, ex,
                    PayflowConstants.SeverityError, IsXmlPayRequest,
                    addlMessage);
                if (!ConnContext.IsCommunicationErrorContained(initError)) ConnContext.AddError(initError);
            }
            finally
            {
                Logger.Instance.Log("PayPal.Payments.Communication.PaymentConnection.CreateConnection(): Exiting.",
                    PayflowConstants.SeverityDebug);
            }
        }

        #endregion

        #region "Connection Functions"

        /// <summary>
        ///     Initializes the Server Connection.
        /// </summary>
        /// <returns>True if success, False otherwise.</returns>
        public bool ConnectToServer()
        {
            Logger.Instance.Log("PayPal.Payments.Communication.PaymentConnection.ConnectToServer(String): Entered.",
                PayflowConstants.SeverityDebug);
            var retVal = false;

            try
            {
                //Initialize Server Uri.
                Logger.Instance.Log(
                    "PayPal.Payments.Communication.PaymentConnection.ConnectToServer(String): Initializing Server Uri.",
                    PayflowConstants.SeverityInfo);
                InitServerUri();
                Logger.Instance.Log(
                    "PayPal.Payments.Communication.PaymentConnection.ConnectToServer(String): Initialized Server Uri = " +
                    _mServerUri.AbsoluteUri,
                    PayflowConstants.SeverityInfo);

                //Create Connection object & Set Connection Attributes
                Logger.Instance.Log(
                    "PayPal.Payments.Communication.PaymentConnection.ConnectToServer(String): Initializing Connection Attributes.",
                    PayflowConstants.SeverityInfo);
                CreateConnection();
                if (_mServerConnection != null)
                {
                    if (_mProxyStatus)
                    {
                        retVal = true;
                        Logger.Instance.Log(
                            "PayPal.Payments.Communication.PaymentConnection.ConnectToServer(String): Connection Created.",
                            PayflowConstants.SeverityInfo);
                    }
                    else
                    {
                        retVal = false;
                        Logger.Instance.Log(
                            "PayPal.Payments.Communication.PaymentConnection.ConnectToServer(String): Connection Creation Failure: Incorrect Proxy Details.",
                            PayflowConstants.SeverityInfo);
                    }
                }
                else
                {
                    retVal = false;
                    Logger.Instance.Log(
                        "PayPal.Payments.Communication.PaymentConnection.ConnectToServer(String): Connection Creation Failure.",
                        PayflowConstants.SeverityInfo);
                }
            }
            catch (Exception ex)
            {
                var addlMessage = "Input Server Uri = " + _mServerUri.AbsoluteUri;
                if (IsProxy) addlMessage += " Input Proxy info = " + _mProxyInfo.Address;
                var initError = PayflowUtility.PopulateCommError(PayflowConstants.ESokConnFailed, ex,
                    PayflowConstants.SeverityError, IsXmlPayRequest,
                    addlMessage);
                if (!ConnContext.IsCommunicationErrorContained(initError)) ConnContext.AddError(initError);
            }
            finally
            {
                Logger.Instance.Log("PayPal.Payments.Communication.PaymentConnection.ConnectToServer(String): Exiting.",
                    PayflowConstants.SeverityDebug);
            }

            return retVal;
        }

        /// <summary>
        ///     Sends the request to the server.
        /// </summary>
        /// <param name="request">String Value of request.</param>
        /// <returns>True if success, False otherwise.</returns>
        public async Task<bool> SendToServer(string request)
        {
            Logger.Instance.Log("PayPal.Payments.Communication.PaymentConnection.SendToServer(String): Entered.",
                PayflowConstants.SeverityDebug);
            var retVal = false;

            // Uncomment this line to test this SDK in QA.  This will override the certificate check where the
            // host URL does not match the server certificate causing the exception:
            //
            // "The underlying connection was closed: Could not establish trust relationship with remote server."
            //
            // See notes in LocalPolicy.cs in the Utility directory.
            //
            //System.Net.ServicePointManager.CertificatePolicy = new LocalPolicy(ref mContext,IsXmlPayRequest);

            try
            {
                var encoding = new ASCIIEncoding();
                if (request != null)
                {
                    var paramListBytes = encoding.GetBytes(request);
                    _mServerConnection.ContentLength = paramListBytes.Length;
                    var reqStram = _mServerConnection.GetRequestStream();
                    await reqStram.WriteAsync(paramListBytes, 0, paramListBytes.Length);
                    reqStram.Close();
                    retVal = true;

                    var headerKeys = _mServerConnection.Headers.AllKeys;
                    if (headerKeys != null)
                    {
                        var keysLen = headerKeys.GetLength(0);
                        int index;
                        Logger.Instance.Log("++++++++++++++++++++++", PayflowConstants.SeverityDebug);
                        for (index = 0; index < keysLen; index++)
                            //if (!(HeaderKeys[index].Equals(PayflowConstants.PAYFLOWHEADER_CLIENT_VERSION)))
                            //	 || HeaderKeys[index].Equals(PayflowConstants.PAYFLOWHEADER_CLIENT_TYPE)))
                            //{
                            Logger.Instance.Log(
                                "HTTP Header: " + headerKeys[index] + ": " +
                                _mServerConnection.Headers.Get(headerKeys[index]), PayflowConstants.SeverityDebug);
                        //}
                        Logger.Instance.Log("++++++++++++++++++++++", PayflowConstants.SeverityDebug);
                    }
                }
                else
                {
                    var initError = PayflowUtility.PopulateCommError(PayflowConstants.EEmptyParamList, null,
                        PayflowConstants.SeverityError, IsXmlPayRequest,
                        null);
                    if (!ConnContext.IsCommunicationErrorContained(initError)) ConnContext.AddError(initError);
                }
            }
            catch (Exception ex)
            {
                var addlMessage = "Input Server Uri = " + _mServerUri.AbsoluteUri;
                if (IsProxy) addlMessage += " Input Proxy info = " + _mProxyInfo.Address;
                var initError = PayflowUtility.PopulateCommError(PayflowConstants.ESokConnFailed, ex,
                    PayflowConstants.SeverityError, IsXmlPayRequest,
                    addlMessage);
                if (!ConnContext.IsCommunicationErrorContained(initError)) ConnContext.AddError(initError);
                Logger.Instance.Log("PayPal.Payments.Communication.PaymentConnection.SendToServer(String): InitError: ",
                    PayflowConstants.SeverityDebug);
            }
            finally
            {
                Logger.Instance.Log("PayPal.Payments.Communication.PaymentConnection.SendToServer(String): Exiting.",
                    PayflowConstants.SeverityDebug);
            }

            return retVal;
        }

        /// <summary>
        ///     Receives the transaction response from
        ///     the server.
        /// </summary>
        /// <returns>String Value of Response.</returns>
        public async Task<string> ReceiveResponseAsync()
        {
            Logger.Instance.Log("PayPal.Payments.Communication.PaymentConnection.ReceiveResponse(): Entered.",
                PayflowConstants.SeverityDebug);
            var response = PayflowConstants.EmptyString;

            try
            {
                var serverResponse = await _mServerConnection.GetResponseAsync();
                var responseStream = serverResponse.GetResponseStream();
                RequestId = serverResponse.Headers.Get(PayflowConstants.PayflowheaderRequestId);
                Logger.Instance.Log(
                    "PayPal.Payments.Communication.PaymentConnection.ReceiveResponse(): Obtained RequestId = " +
                    RequestId,
                    PayflowConstants.SeverityInfo);

                // v4.3.1 - changed stream handling due to issue with code below not returning a complete
                // stream.//read the stream in bytes
                //long RespLen = ServerResponse.ContentLength ;
                //Byte[] RespByteBuffer = new byte[RespLen];
                //ResponseStream.Read (RespByteBuffer,0,Convert.ToInt32 (RespLen));
                //UTF8Encoding UtfEncoding = new UTF8Encoding ();
                //Response = UtfEncoding.GetString(RespByteBuffer);

                response = await new StreamReader(serverResponse.GetResponseStream()).ReadToEndAsync();

                Logger.Instance.Log(
                    "PayPal.Payments.Communication.PaymentConnection.ReceiveResponse(): Obtained Response.",
                    PayflowConstants.SeverityInfo);
                responseStream.Close();
                _mServerConnection = null;
            }
            catch (Exception ex)
            {
                var addlMessage = "Input Server Uri = " + _mServerUri.AbsoluteUri;
                if (IsProxy) addlMessage += " Input Proxy info = " + _mProxyInfo.Address;
                var initError = PayflowUtility.PopulateCommError(PayflowConstants.ETimeoutWaitResp, ex,
                    PayflowConstants.SeverityError, IsXmlPayRequest,
                    addlMessage);
                if (!ConnContext.IsCommunicationErrorContained(initError)) ConnContext.AddError(initError);
            }
            finally
            {
                Logger.Instance.Log("PayPal.Payments.Communication.PaymentConnection.ReceiveResponse(): Exiting.",
                    PayflowConstants.SeverityDebug);
            }

            return response;
        }

        /// <summary>
        ///     Disconnects from the server.
        /// </summary>
        public void Disconnect()
        {
            Logger.Instance.Log("PayPal.Payments.Communication.PaymentConnection.Disconnect(): Entered.",
                PayflowConstants.SeverityDebug);
            _mServerConnection = null;
            Logger.Instance.Log("PayPal.Payments.Communication.PaymentConnection.Disconnect(): Exiting.",
                PayflowConstants.SeverityDebug);
        }

        #endregion
    }
}