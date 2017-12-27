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
using PFProSDK.Common;
using PFProSDK.Common.Utility;

#endregion

namespace PFProSDK.DataObjects
{
    /// <summary>
    ///     Used for Payflow Host related information.
    /// </summary>
    /// <remarks>
    ///     This class stores the information related to connection to the
    ///     PayPal payment gateway. If the empty constructor of this class
    ///     is used to create the object, or
    ///     passed values are empty, then The following values (if empty) are looked for
    ///     as follows:
    ///     <list type="table">
    ///         <listheader>
    ///             <term>Property</term>
    ///             <description>From Internal Default</description>
    ///             <description>From App.config key</description>
    ///         </listheader>
    ///         <item>
    ///             <term>Payflow Host</term>
    ///             <description>NA</description>
    ///             <description>PAYFLOW_HOST</description>
    ///         </item>
    ///         <item>
    ///             <term>Payflow Port</term>
    ///             <description>443</description>
    ///             <description>NA</description>
    ///         </item>
    ///         <item>
    ///             <term>Transaction timeout</term>
    ///             <description>45 seconds</description>
    ///             <description>NA</description>
    ///         </item>
    ///     </list>
    /// </remarks>
    public sealed class PayflowConnectionData : BaseRequestDataObject
    {
        /// <summary>
        ///     Initializes the default values
        /// </summary>
        private void InitDefaultValues()
        {
            //Check if the values held
            //in the PayPal server
            //connection related params
            //if they are passed null or
            //0 (for int values) then
            //intialize them to appropriate
            //default values.

            //set the timeout to default timeout.
            if (TimeOut == 0) TimeOut = PayflowConstants.DefaultTimeout;

            /**
              // ToInt32 can throw FormatException or OverflowException.
              try
              {
                  mTimeOut = Convert.ToInt32(PayflowUtility.AppSettings(PayflowConstants.INTL_PARAM_PAYFLOW_TIMEOUT));
              }
              catch (FormatException Ex)
              {
                  String StackTrace = PayflowConstants.EMPTY_STRING;
                  PayflowUtility.InitStackTraceOn();
                  if (PayflowConstants.TRACE_ON.Equals(PayflowConstants.TRACE))
                  {
                      StackTrace = ": " + Ex.Message + Ex.StackTrace;
                  }
                  String RespMessage = PayflowConstants.PARAM_RESULT
                      + PayflowConstants.SEPARATOR_NVP
                      + (String)PayflowConstants.CommErrorCodes[PayflowConstants.E_CONFIG_ERROR]
                      + PayflowConstants.DELIMITER_NVP
                      + PayflowConstants.PARAM_RESPMSG
                      + PayflowConstants.SEPARATOR_NVP
                      + (String)PayflowConstants.CommErrorMessages[PayflowConstants.E_CONFIG_ERROR]
                      + "Tag "
                      + PayflowConstants.INTL_PARAM_PAYFLOW_TIMEOUT +
                      " is not valid, using default value."
                      + StackTrace;
                  ErrorObject Error = new ErrorObject(PayflowConstants.SEVERITY_FATAL, "", RespMessage);
                  Context.AddError(Error);
              }
              catch (OverflowException Ex)
              {
                  String StackTrace = PayflowConstants.EMPTY_STRING;
                  PayflowUtility.InitStackTraceOn();
                  if (PayflowConstants.TRACE_ON.Equals(PayflowConstants.TRACE))
                  {
                      StackTrace = ": " + Ex.Message + Ex.StackTrace;
                  }
                  String RespMessage = PayflowConstants.PARAM_RESULT
                      + PayflowConstants.SEPARATOR_NVP
                      + (String)PayflowConstants.CommErrorCodes[PayflowConstants.E_CONFIG_ERROR]
                      + PayflowConstants.DELIMITER_NVP
                      + PayflowConstants.PARAM_RESPMSG
                      + PayflowConstants.SEPARATOR_NVP
                      + (String)PayflowConstants.CommErrorMessages[PayflowConstants.E_CONFIG_ERROR]
                      + "Tag "
                      + PayflowConstants.INTL_PARAM_PAYFLOW_TIMEOUT +
                      " is not valid, using default value."
                      + StackTrace;
                  ErrorObject Error = new ErrorObject(PayflowConstants.SEVERITY_FATAL, "", RespMessage);
                  Context.AddError(Error);
              }
                
            }
                mHostPort = PayflowConstants.DEFAULT_HOSTPORT;
            }*/

            try
            {
                if (HostAddress == null || HostAddress.Length == 0)
                {
                    var hostAddress = PayflowUtility.AppSettings(PayflowConstants.IntlParamPayflowHost);
                    if (hostAddress != null && hostAddress.Length > 0)
                    {
                        hostAddress = hostAddress.TrimStart().TrimEnd();
                        if (hostAddress.Length == 0)
                        {
                            var respMessage = PayflowConstants.ParamResult
                                              + PayflowConstants.SeparatorNvp
                                              + (string) PayflowConstants.CommErrorCodes[PayflowConstants.EConfigError]
                                              + PayflowConstants.DelimiterNvp
                                              + PayflowConstants.ParamRespmsg
                                              + PayflowConstants.SeparatorNvp
                                              + (string) PayflowConstants.CommErrorMessages[
                                                  PayflowConstants.EConfigError]
                                              + "Tag "
                                              + PayflowConstants.IntlParamPayflowHost +
                                              " is not present in the config file or config file is missing.";
                            var error = new ErrorObject(PayflowConstants.SeverityFatal, "", respMessage);
                            Context.AddError(error);
                        }
                        else
                        {
                            HostAddress = hostAddress;
                        }
                    }
                    else
                    {
                        var respMessage = PayflowConstants.ParamResult
                                          + PayflowConstants.SeparatorNvp
                                          + (string) PayflowConstants.CommErrorCodes[PayflowConstants.EConfigError]
                                          + PayflowConstants.DelimiterNvp
                                          + PayflowConstants.ParamRespmsg
                                          + PayflowConstants.SeparatorNvp
                                          + (string) PayflowConstants.CommErrorMessages[PayflowConstants.EConfigError]
                                          + "Tag "
                                          + PayflowConstants.IntlParamPayflowHost +
                                          " is not present in the config file or config file is missing.";
                        var error = new ErrorObject(PayflowConstants.SeverityFatal, "", respMessage);
                        Context.AddError(error);
                    }
                }
            }
            catch (Exception ex)
            {
                var stackTrace = PayflowConstants.EmptyString;
                PayflowUtility.InitStackTraceOn();
                if (PayflowConstants.TraceOn.Equals(PayflowConstants.Trace))
                    stackTrace = ": " + ex.Message + ex.StackTrace;
                var respMessage = PayflowConstants.ParamResult
                                  + PayflowConstants.SeparatorNvp
                                  + (string) PayflowConstants.CommErrorCodes[PayflowConstants.EConfigError]
                                  + PayflowConstants.DelimiterNvp
                                  + PayflowConstants.ParamRespmsg
                                  + PayflowConstants.SeparatorNvp
                                  + (string) PayflowConstants.CommErrorMessages[PayflowConstants.EConfigError]
                                  + "Tag "
                                  + PayflowConstants.IntlParamPayflowHost +
                                  " is not present in the config file or config file is missing."
                                  + stackTrace;
                var error = new ErrorObject(PayflowConstants.SeverityFatal, "", respMessage);
                Context.AddError(error);
            }
        }

        #region "Member Variables"

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets HostAddress. It is PayPal's HostName
        /// </summary>
        /// <remarks>Read-only property.</remarks>
        public string HostAddress { get; private set; }

        /// <summary>
        ///     Gets HostPort. Use port 443
        /// </summary>
        /// <remarks>Read-only property.</remarks>
        public int HostPort { get; }

        /// <summary>
        ///     Gets Time-out period for the transaction. The minimum recommended
        ///     time-out value is 30 seconds. The client begins tracking
        ///     from the time that it sends the transaction request to the server.
        /// </summary>
        /// <remarks>Read-only property.</remarks>
        public int TimeOut { get; private set; }

        /// <summary>
        ///     Gets Proxy server address. Use the PROXY parameters for servers
        ///     behind a firewall. Your network administrator can provide the
        ///     values.
        /// </summary>
        /// <remarks>Read-only property.</remarks>
        public string ProxyAddress { get; }

        /// <summary>
        ///     Gets ProxyPort
        /// </summary>
        /// <remarks>Read-only property.</remarks>
        public int ProxyPort { get; }

        /// <summary>
        ///     Gets ProxyLogon
        /// </summary>
        /// <remarks>Read-only property.</remarks>
        public string ProxyLogon { get; }

        /// <summary>
        ///     Gets ProxyPassword
        /// </summary>
        /// <remarks>Read-only property.</remarks>
        public string ProxyPassword { get; }

        #endregion

        #region "Constructors"

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <remarks>
        ///     The following values (if empty) are looked for
        ///     as follows:
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Property</term>
        ///             <description>From Internal Default</description>
        ///             <description>From App.config key</description>
        ///         </listheader>
        ///         <item>
        ///             <term>Payflow Host</term>
        ///             <description>NA</description>
        ///             <description>Payflow_HOST</description>
        ///         </item>
        ///         <item>
        ///             <term>Payflow Port</term>
        ///             <description>443</description>
        ///             <description>NA</description>
        ///         </item>
        ///         <item>
        ///             <term>Transaction timeout</term>
        ///             <description>45 seconds</description>
        ///             <description>NA</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public PayflowConnectionData() : this(null, 0, 0, null, 0, null, null)
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="hostAddress">Payflow Host Address</param>
        /// <remarks>
        ///     The following values (if empty) are looked for
        ///     as follows:
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Property</term>
        ///             <description>From Internal Default</description>
        ///             <description>From App.config key</description>
        ///         </listheader>
        ///         <item>
        ///             <term>Payflow Host</term>
        ///             <description>NA</description>
        ///             <description>Payflow_HOST</description>
        ///         </item>
        ///         <item>
        ///             <term>Payflow Port</term>
        ///             <description>443</description>
        ///             <description>NA</description>
        ///         </item>
        ///         <item>
        ///             <term>Transaction timeout</term>
        ///             <description>45 seconds</description>
        ///             <description>NA</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public PayflowConnectionData(string hostAddress) : this(hostAddress, 0, 0, null, 0, null, null)
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="hostAddress">Payflow Host Address</param>
        /// <param name="hostPort">Payflow Host port</param>
        /// <param name="timeOut">Transaction time out</param>
        /// <remarks>
        ///     The following values (if empty) are looked for
        ///     as follows:
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Property</term>
        ///             <description>From Internal Default</description>
        ///             <description>From App.config key</description>
        ///         </listheader>
        ///         <item>
        ///             <term>Payflow Host</term>
        ///             <description>NA</description>
        ///             <description>Payflow_HOST</description>
        ///         </item>
        ///         <item>
        ///             <term>Payflow Port</term>
        ///             <description>443</description>
        ///             <description>NA</description>
        ///         </item>
        ///         <item>
        ///             <term>Transaction timeout</term>
        ///             <description>45 seconds</description>
        ///             <description>NA</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public PayflowConnectionData(string hostAddress, int hostPort, int timeOut) : this(hostAddress, hostPort,
            timeOut, null, 0, null, null)
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="hostAddress">Payflow Host Address</param>
        /// <param name="hostPort">>Payflow Host port</param>
        /// <param name="timeOut">Transaction timeout</param>
        /// <param name="proxyAddress">Proxy Address</param>
        /// <param name="proxyPort">Proxy Port</param>
        /// <param name="proxyLogon">Proxy Logon Id </param>
        /// <param name="proxyPassword">ProxyPwd</param>
        /// <remarks>
        ///     The following values (if empty) are looked for
        ///     as follows:
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Property</term>
        ///             <description>From Internal Default</description>
        ///             <description>From App.config key</description>
        ///         </listheader>
        ///         <item>
        ///             <term>Payflow Host</term>
        ///             <description>NA</description>
        ///             <description>Payflow_HOST</description>
        ///         </item>
        ///         <item>
        ///             <term>Payflow Port</term>
        ///             <description>443</description>
        ///             <description>NA</description>
        ///         </item>
        ///         <item>
        ///             <term>Transaction timeout</term>
        ///             <description>45 seconds</description>
        ///             <description>NA</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public PayflowConnectionData(string hostAddress, int hostPort, int timeOut, string proxyAddress, int proxyPort,
            string proxyLogon, string proxyPassword)
        {
            if (Context == null) Context = new Context();

            HostAddress = hostAddress;
            HostPort = hostPort;
            TimeOut = timeOut;
            ProxyAddress = proxyAddress;
            ProxyPort = proxyPort;
            ProxyLogon = proxyLogon;
            ProxyPassword = proxyPassword;
            InitDefaultValues();
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="hostAddress">Payflow Host Address</param>
        /// <param name="hostPort">>Payflow Host port</param>
        /// <remarks>
        ///     The following values (if empty) are looked for
        ///     as follows:
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Property</term>
        ///             <description>From Internal Default</description>
        ///             <description>From App.config key</description>
        ///         </listheader>
        ///         <item>
        ///             <term>Payflow Host</term>
        ///             <description>NA</description>
        ///             <description>Payflow_HOST</description>
        ///         </item>
        ///         <item>
        ///             <term>Payflow Port</term>
        ///             <description>443</description>
        ///             <description>NA</description>
        ///         </item>
        ///         <item>
        ///             <term>Transaction timeout</term>
        ///             <description>45 seconds</description>
        ///             <description>NA</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public PayflowConnectionData(string hostAddress, int hostPort) : this(hostAddress, hostPort, 0, null, 0, null,
            null)
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="hostAddress">Payflow Host Address</param>
        /// <param name="hostPort">>Payflow Host port</param>
        /// <param name="proxyAddress">Proxy Address</param>
        /// <param name="proxyPort">Proxy Port</param>
        /// <param name="proxyLogon">Proxy Logon Id </param>
        /// <param name="proxyPassword">ProxyPwd</param>
        /// <remarks>
        ///     The following values (if empty) are looked for
        ///     as follows:
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Property</term>
        ///             <description>From Internal Default</description>
        ///             <description>From App.config key</description>
        ///         </listheader>
        ///         <item>
        ///             <term>Payflow Host</term>
        ///             <description>NA</description>
        ///             <description>Payflow_HOST</description>
        ///         </item>
        ///         <item>
        ///             <term>Payflow Port</term>
        ///             <description>443</description>
        ///             <description>NA</description>
        ///         </item>
        ///         <item>
        ///             <term>Transaction timeout</term>
        ///             <description>45 seconds</description>
        ///             <description>NA</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public PayflowConnectionData(string hostAddress, int hostPort, string proxyAddress, int proxyPort,
            string proxyLogon, string proxyPassword) : this(hostAddress, hostPort, 0, proxyAddress, proxyPort,
            proxyLogon, proxyPassword)
        {
        }

        #endregion
    }
}