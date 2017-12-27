#region "Copyright"

//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

#endregion

#region "Imports"

using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Xml;
using PFProSDK.Common;
using PFProSDK.Common.Logging;
using PFProSDK.Common.Utility;
using PFProSDK.DataObjects;

#endregion

namespace PFProSDK.Communication
{
    /// <summary>
    ///     PayflowNETAPI is used to submit a Name-value pair or XMLPay request to
    ///     PayPal payment gateway for online payment processing. The response
    ///     returned is the string value of the response from the PayPal payment
    ///     gateway.
    /// </summary>
    /// <remarks>
    ///     Instance of PayflowNETAPI initialized with the information related
    ///     to connection to the PayPal payment gateway.
    ///     If the empty constructor of this class is used to create the object or
    ///     passed values are empty, then The following values (if empty)
    ///     are looked for as follows:
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
    /// <example>
    ///     <code lang="C#" escaped="false">
    /// 	/// ..........
    ///  	// Sample Request. 
    ///  	// Please replace user, vendor, password &amp; partner with your merchant information.
    ///  	String Request = "TRXTYPE=S&amp;ACCT=5105105105105100&amp;EXPDATE=0115&amp;TENDER=C&amp;INVNUM=INV12345&amp;AMT=25.12&amp;PONUM=PO12345&amp;STREET=123 Main St.&amp;ZIP=12345&amp;USER=user&amp;VENDOR=vendor&amp;PARTNER=partner&amp;PWD=password";
    ///  	// Create an instance of PayflowNETAPI.
    ///  	PayflowNETAPI PayflowNetApi = new PayflowNETAPI();
    ///  	// RequestId is a unique string that is required for each &amp; every transaction. 
    ///  	// The merchant can use her/his own algorithm to generate this unique request id or 
    ///  	// use the SDK provided API to generate this as shown below (PayflowUtility.RequestId).
    ///  	String Response = PayflowNetApi.SubmitTransaction(Request, PayflowUtility.RequestId);
    ///  	// To write the Response on to the console.
    ///  	Console.WriteLine(Environment.NewLine + "Request = " + PayflowNetApi.TransactionRequest);
    ///  	Console.WriteLine(Environment.NewLine + "Response = " + Response);
    ///  	// Following lines of code are optional. 
    ///  	// Begin optional code for displaying SDK errors ...
    ///  	// It is used to read any errors that might have occurred in the SDK.
    /// 	    String TransErrors = PayflowNetApi.TransactionContext.ToString();
    /// 	    if (TransErrors != null &amp;&amp; TransErrors.Length > 0)	
    /// 	    {
    /// 	    	Console.WriteLine(Environment.NewLine + "Transaction Errors from SDK = " + TransErrors);
    /// 	    }
    /// 	   
    ///  </code>
    ///     <code lang="Visual Basic" escaped="false">
    /// 		' Sample Request. 
    /// 		' Please replace user, vendor, password &amp; partner with your merchant information.
    /// 		Dim Request As String = "TRXTYPE=S&amp;ACCT=5105105105105100&amp;EXPDATE=0115&amp;TENDER=C&amp;INVNUM=INV12345&amp;AMT=25.12&amp;PONUM=PO12345&amp;STREET=123 Main St.&amp;ZIP=12345&amp;USER=user&amp;VENDOR=vendor&amp;PARTNER=partner&amp;PWD=password"
    ///  
    /// 		' Create an instance of PayflowNETAPI.
    /// 		Dim PayflowNetApi As PayflowNETAPI = new PayflowNETAPI
    ///  
    /// 		' RequestId is a unique string that is required for each &amp; every transaction. 
    /// 		' The merchant can use her/his own algorithm to generate this unique request id or 
    /// 		' use the SDK provided API to generate this as shown below (PayflowUtility.GetRequestId()).
    /// 		Dim Response As String = PayflowNetApi.SubmitTransaction(Request, PayflowUtility.RequestId)
    ///  
    /// 		' To write the Response on to the console.
    /// 		Console.WriteLine(Environment.NewLine + "Request = " + PayflowNetApi.TransactionRequest)
    /// 		Console.WriteLine(Environment.NewLine + "Response = " + Response)
    ///  
    /// 		' Following lines of code are optional. 
    /// 		' Begin optional code for displaying SDK errors ...
    /// 		' It is used to read any errors that might have occurred in the SDK.
    ///  
    /// 		Dim TransErrors As String = PayflowNetApi.TransactionContext.ToString()
    /// 		If (Not TransErrors Is Nothing And TransErrors.Length > 0) Then
    /// 			Console.WriteLine(Environment.NewLine + "Transaction Errors from SDK = " + TransErrors)
    /// 		End If
    ///  </code>
    /// </example>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public sealed class PayflowNetapi
    {
        #region "Member Variables"

        /// <summary>
        ///     PaymentStateMachine object.
        /// </summary>
        private PaymentStateMachine _mPaymentStateMachine;

        /// <summary>
        ///     Host Address
        /// </summary>
        private string _mHostAddress;

        /// <summary>
        ///     Host Port
        /// </summary>
        private int _mHostPort;

        /// <summary>
        ///     TimeOut
        /// </summary>
        private int _mTimeOut;

        /// <summary>
        ///     Proxy Address
        /// </summary>
        private string _mProxyAddress;

        /// <summary>
        ///     Proxy Port
        /// </summary>
        private int _mProxyPort;

        /// <summary>
        ///     Proxy Logon
        /// </summary>
        private string _mProxyLogon;

        /// <summary>
        ///     Proxy Password
        /// </summary>
        private string _mProxyPassword;

        /// <summary>
        ///     Transaction Context
        /// </summary>
        /// p
        private Context _mTransactionContext;

        /// <summary>
        ///     Transaction request withought masking
        /// </summary>
        private string _mTransRqst;

        #endregion

        #region "Constructors"

        /// <summary>
        ///     PayflowNETAPI Constructor
        /// </summary>
        /// <summary>
        ///     PayflowNETAPI is used to submit a Name-value pair or XMLPay request to
        ///     PayPal payment gateway for online payment processing. The response
        ///     returned is the string value of the response from the PayPal payment
        ///     gateway.
        /// </summary>
        /// <remarks>
        ///     Instance of PayflowNETAPI initialized with the information related
        ///     to connection to the PayPal payment gateway.
        ///     If the empty constructor of this class is used to create the object or
        ///     passed values are empty, then The following values (if empty)
        ///     are looked for as follows:
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
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	/// ..........
        ///  	// Sample Request. 
        ///  	// Please replace user, vendor, password &amp; partner with your merchant information.
        ///  	String Request = "TRXTYPE=S&amp;ACCT=5105105105105100&amp;EXPDATE=0115&amp;TENDER=C&amp;INVNUM=INV12345&amp;AMT=25.12&amp;PONUM=PO12345&amp;STREET=123 Main St.&amp;ZIP=12345&amp;USER=user&amp;VENDOR=vendor&amp;PARTNER=partner&amp;PWD=password";
        ///  	// Create an instance of PayflowNETAPI.
        ///  	PayflowNETAPI PfProNetApi = new PayflowNETAPI();
        ///  	// RequestId is a unique string that is required for each &amp; every transaction. 
        ///  	// The merchant can use her/his own algorithm to generate this unique request id or 
        ///  	// use the SDK provided API to generate this as shown below (PayflowUtility.RequestId).
        ///  	String Response = PfProNetApi.SubmitTransaction(Request, PayflowUtility.RequestId);
        ///  	// To write the Response on to the console.
        ///  	Console.WriteLine(Environment.NewLine + "Request = " + PfProNetApi.TransactionRequest);
        ///  	Console.WriteLine(Environment.NewLine + "Response = " + Response);
        ///  	// Following lines of code are optional. 
        ///  	// Begin optional code for displaying SDK errors ...
        ///  	// It is used to read any errors that might have occurred in the SDK.
        /// 	   String TransErrors = PfProNetApi.TransactionContext.ToString();
        /// 	   if (TransErrors != null &amp;&amp; TransErrors.Length > 0)	
        /// 	   {
        /// 	    	Console.WriteLine(Environment.NewLine + "Transaction Errors from SDK = " + TransErrors);
        /// 	   }
        /// 	   
        ///     // End optional code for displaying SDK errors.
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        ///  ' Sample Request. 
        ///  ' Please replace user, vendor, password &amp; partner with your merchant information.
        ///  Dim Request As String = "TRXTYPE=S&amp;ACCT=5105105105105100&amp;EXPDATE=0115&amp;TENDER=C&amp;INVNUM=INV12345&amp;AMT=25.12&amp;PONUM=PO12345&amp;STREET=123 Main St.&amp;ZIP=12345&amp;USER=user&amp;VENDOR=vendor&amp;PARTNER=partner&amp;PWD=password"
        ///  
        ///  ' Create an instance of PayflowNETAPI.
        ///  Dim PfProNetApi As PayflowNETAPI = new PayflowNETAPI
        ///  
        ///  ' RequestId is a unique string that is required for each &amp; every transaction. 
        ///  ' The merchant can use her/his own algorithm to generate this unique request id or 
        ///  ' use the SDK provided API to generate this as shown below (PayflowUtility.GetRequestId()).
        ///  Dim Response As String = PfProNetApi.SubmitTransaction(Request, PayflowUtility.RequestId)
        ///  
        ///  ' To write the Response on to the console.
        ///  Console.WriteLine(Environment.NewLine + "Request = " + PfProNetApi.TransactionRequest)
        ///  Console.WriteLine(Environment.NewLine + "Response = " + Response)
        ///  
        ///  ' Following lines of code are optional. 
        ///  ' Begin optional code for displaying SDK errors ...
        ///  ' It is used to read any errors that might have occurred in the SDK.
        ///  
        ///  Dim TransErrors As String = PfProNetApi.TransactionContext.ToString()
        ///  If (Not TransErrors Is Nothing And TransErrors.Length > 0) Then
        ///    Console.WriteLine(Environment.NewLine + "Transaction Errors from SDK = " + TransErrors)
        ///  End If
        ///  
        ///  'End optional code for displaying SDK errors.
        ///  </code>
        /// </example>
        public PayflowNetapi()
            : this(null, 0, 0, null, 0, null, null)
        {
        }

        /// <summary>
        ///     PayflowNETAPI Constructor
        /// </summary>
        /// <param name="hostAddress">Payflow Host Address.</param>
        /// <param name="hostPort">Payflow Host Port.</param>
        /// <param name="timeOut">Transaction Timeout.</param>
        /// <param name="proxyAddress">Proxy Address.</param>
        /// <param name="proxyPort">Proxy Port.</param>
        /// <param name="proxyLogon">Proxy Logon Id.</param>
        /// <param name="proxyPassword">Proxy Password.</param>
        /// <summary>
        ///     PayflowNETAPI is used to submit a Name-value pair or XMLPay request to
        ///     PayPal payment gateway for online payment processing. The response
        ///     returned is the string value of the response from the PayPal payment
        ///     gateway.
        /// </summary>
        /// <remarks>
        ///     Instance of PayflowNETAPI initialized with the information related
        ///     to connection to the PayPal payment gateway.
        ///     If the empty constructor of this class is used to create the object or
        ///     passed values are empty, then The following values (if empty)
        ///     are looked for as follows:
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
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	/// ..........
        ///  	// Sample Request. 
        ///  	// Please replace user, vendor, password &amp; partner with your merchant information.
        ///  	String Request = "TRXTYPE=S&amp;ACCT=5105105105105100&amp;EXPDATE=0115&amp;TENDER=C&amp;INVNUM=INV12345&amp;AMT=25.12&amp;PONUM=PO12345&amp;STREET=123 Main St.&amp;ZIP=12345&amp;USER=user&amp;VENDOR=vendor&amp;PARTNER=partner&amp;PWD=password";
        ///  	// Create an instance of PayflowNETAPI.
        ///  	PayflowNETAPI PfProNetApi = new PayflowNETAPI();
        ///  	// RequestId is a unique string that is required for each &amp; every transaction. 
        ///  	// The merchant can use her/his own algorithm to generate this unique request id or 
        ///  	// use the SDK provided API to generate this as shown below (PayflowUtility.RequestId).
        ///  	String Response = PfProNetApi.SubmitTransaction(Request, PayflowUtility.RequestId);
        ///  	// To write the Response on to the console.
        ///  	Console.WriteLine(Environment.NewLine + "Request = " + PfProNetApi.TransactionRequest);
        ///  	Console.WriteLine(Environment.NewLine + "Response = " + Response);
        ///  	// Following lines of code are optional. 
        ///  	// Begin optional code for displaying SDK errors ...
        ///  	// It is used to read any errors that might have occurred in the SDK.
        /// 	    String TransErrors = PfProNetApi.TransactionContext.ToString();
        /// 	    if (TransErrors != null &amp;&amp; TransErrors.Length > 0)	
        /// 	    {
        /// 	    	Console.WriteLine(Environment.NewLine + "Transaction Errors from SDK = " + TransErrors);
        /// 	    }
        /// 	   
        ///      // End optional code for displaying SDK errors.
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        ///  ' Sample Request. 
        ///  ' Please replace user, vendor, password &amp; partner with your merchant information.
        ///  Dim Request As String = "TRXTYPE=S&amp;ACCT=5105105105105100&amp;EXPDATE=0115&amp;TENDER=C&amp;INVNUM=INV12345&amp;AMT=25.12&amp;PONUM=PO12345&amp;STREET=123 Main St.&amp;ZIP=12345&amp;USER=user&amp;VENDOR=vendor&amp;PARTNER=partner&amp;PWD=password"
        ///  
        ///  ' Create an instance of PayflowNETAPI.
        ///  Dim PfProNetApi As PayflowNETAPI = new PayflowNETAPI
        ///  
        ///  ' RequestId is a unique string that is required for each &amp; every transaction. 
        ///  ' The merchant can use her/his own algorithm to generate this unique request id or 
        ///  ' use the SDK provided API to generate this as shown below (PayflowUtility.GetRequestId()).
        ///  Dim Response As String = PfProNetApi.SubmitTransaction(Request, PayflowUtility.RequestId)
        ///  
        ///  ' To write the Response on to the console.
        ///  Console.WriteLine(Environment.NewLine + "Request = " + PfProNetApi.TransactionRequest)
        ///  Console.WriteLine(Environment.NewLine + "Response = " + Response)
        ///  
        ///  ' Following lines of code are optional. 
        ///  ' Begin optional code for displaying SDK errors ...
        ///  ' It is used to read any errors that might have occurred in the SDK.
        ///  
        ///  Dim TransErrors As String = PfProNetApi.TransactionContext.ToString()
        ///  If (Not TransErrors Is Nothing And TransErrors.Length > 0) Then
        ///    Console.WriteLine(Environment.NewLine + "Transaction Errors from SDK = " + TransErrors)
        ///  End If
        ///  
        ///  'End optional code for displaying SDK errors.
        ///  </code>
        /// </example>
        public PayflowNetapi(string hostAddress, int hostPort, int timeOut, string proxyAddress, int proxyPort,
            string proxyLogon, string proxyPassword)
        {
            _mTransactionContext = new Context();
            SetParameters(hostAddress, hostPort, timeOut, proxyAddress, proxyPort, proxyLogon, proxyPassword, null,
                null, null, null, false);
        }

        /// <summary>
        ///     PayflowNETAPI Constructor
        /// </summary>
        /// <param name="hostAddress">Payflow Host Address.</param>
        /// <param name="hostPort">Payflow Host Port.</param>
        /// <summary>
        ///     PayflowNETAPI is used to submit a Name-value pair or XMLPay request to
        ///     PayPal payment gateway for online payment processing. The response
        ///     returned is the string value of the response from the PayPal payment
        ///     gateway.
        /// </summary>
        /// <remarks>
        ///     Instance of PayflowNETAPI initialized with the information related
        ///     to connection to the PayPal payment gateway.
        ///     If the empty constructor of this class is used to create the object or
        ///     passed values are empty, then The following values (if empty)
        ///     are looked for as follows:
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
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	/// ..........
        ///  	// Sample Request. 
        ///  	// Please replace user, vendor, password &amp; partner with your merchant information.
        ///  	String Request = "TRXTYPE=S&amp;ACCT=5105105105105100&amp;EXPDATE=0115&amp;TENDER=C&amp;INVNUM=INV12345&amp;AMT=25.12&amp;PONUM=PO12345&amp;STREET=123 Main St.&amp;ZIP=12345&amp;USER=user&amp;VENDOR=vendor&amp;PARTNER=partner&amp;PWD=password";
        ///  	// Create an instance of PayflowNETAPI.
        ///  	PayflowNETAPI PfProNetApi = new PayflowNETAPI();
        ///  	// RequestId is a unique string that is required for each &amp; every transaction. 
        ///  	// The merchant can use her/his own algorithm to generate this unique request id or 
        ///  	// use the SDK provided API to generate this as shown below (PayflowUtility.RequestId).
        ///  	String Response = PfProNetApi.SubmitTransaction(Request, PayflowUtility.RequestId);
        ///  	// To write the Response on to the console.
        ///  	Console.WriteLine(Environment.NewLine + "Request = " + PfProNetApi.TransactionRequest);
        ///  	Console.WriteLine(Environment.NewLine + "Response = " + Response);
        ///  	// Following lines of code are optional. 
        ///  	// Begin optional code for displaying SDK errors ...
        ///  	// It is used to read any errors that might have occurred in the SDK.
        /// 	    String TransErrors = PfProNetApi.TransactionContext.ToString();
        /// 	    if (TransErrors != null &amp;&amp; TransErrors.Length > 0)	
        /// 	    {
        /// 	    	Console.WriteLine(Environment.NewLine + "Transaction Errors from SDK = " + TransErrors);
        /// 	    }
        /// 	   
        ///      // End optional code for displaying SDK errors.
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        ///  ' Sample Request. 
        ///  ' Please replace user, vendor, password &amp; partner with your merchant information.
        ///  Dim Request As String = "TRXTYPE=S&amp;ACCT=5105105105105100&amp;EXPDATE=0115&amp;TENDER=C&amp;INVNUM=INV12345&amp;AMT=25.12&amp;PONUM=PO12345&amp;STREET=123 Main St.&amp;ZIP=12345&amp;USER=user&amp;VENDOR=vendor&amp;PARTNER=partner&amp;PWD=password"
        ///  
        ///  ' Create an instance of PayflowNETAPI.
        ///  Dim PfProNetApi As PayflowNETAPI = new PayflowNETAPI
        ///  
        ///  ' RequestId is a unique string that is required for each &amp; every transaction. 
        ///  ' The merchant can use her/his own algorithm to generate this unique request id or 
        ///  ' use the SDK provided API to generate this as shown below (PayflowUtility.GetRequestId()).
        ///  Dim Response As String = PfProNetApi.SubmitTransaction(Request, PayflowUtility.RequestId)
        ///  
        ///  ' To write the Response on to the console.
        ///  Console.WriteLine(Environment.NewLine + "Request = " + PfProNetApi.TransactionRequest)
        ///  Console.WriteLine(Environment.NewLine + "Response = " + Response)
        ///  
        ///  ' Following lines of code are optional. 
        ///  ' Begin optional code for displaying SDK errors ...
        ///  ' It is used to read any errors that might have occurred in the SDK.
        ///  
        ///  Dim TransErrors As String = PfProNetApi.TransactionContext.ToString()
        ///  If (Not TransErrors Is Nothing And TransErrors.Length > 0) Then
        ///    Console.WriteLine(Environment.NewLine + "Transaction Errors from SDK = " + TransErrors)
        ///  End If
        ///  
        ///  'End optional code for displaying SDK errors.
        ///  </code>
        /// </example>
        public PayflowNetapi(string hostAddress, int hostPort)
            : this(hostAddress, hostPort, 0, null, 0, null, null)
        {
        }

        /// <summary>
        ///     PayflowNETAPI Constructor
        ///     <param name="hostAddress">Payflow Host Address.</param>
        ///     <param name="hostPort">Payflow Host Port.</param>
        ///     <param name="proxyAddress">Proxy Address.</param>
        ///     <param name="proxyPort">Proxy Port.</param>
        ///     <param name="proxyLogon">Proxy Logon Id.</param>
        ///     <param name="proxyPassword">Proxy Password.</param>
        /// </summary>
        /// <summary>
        ///     PayflowNETAPI is used to submit a Name-value pair or XMLPay request to
        ///     PayPal payment gateway for online payment processing. The response
        ///     returned is the string value of the response from the PayPal payment
        ///     gateway.
        /// </summary>
        /// <remarks>
        ///     Instance of PayflowNETAPI initialized with the information related
        ///     to connection to the PayPal payment gateway.
        ///     If the empty constructor of this class is used to create the object or
        ///     passed values are empty, then The following values (if empty)
        ///     are looked for as follows:
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
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	/// ..........
        ///  	// Sample Request. 
        ///  	// Please replace user, vendor, password &amp; partner with your merchant information.
        ///  	String Request = "TRXTYPE=S&amp;ACCT=5105105105105100&amp;EXPDATE=0115&amp;TENDER=C&amp;INVNUM=INV12345&amp;AMT=25.12&amp;PONUM=PO12345&amp;STREET=123 Main St.&amp;ZIP=12345&amp;USER=user&amp;VENDOR=vendor&amp;PARTNER=partner&amp;PWD=password";
        ///  	// Create an instance of PayflowNETAPI.
        ///  	PayflowNETAPI PfProNetApi = new PayflowNETAPI();
        ///  	// RequestId is a unique string that is required for each &amp; every transaction. 
        ///  	// The merchant can use her/his own algorithm to generate this unique request id or 
        ///  	// use the SDK provided API to generate this as shown below (PayflowUtility.RequestId).
        ///  	String Response = PfProNetApi.SubmitTransaction(Request, PayflowUtility.RequestId);
        ///  	// To write the Response on to the console.
        ///  	Console.WriteLine(Environment.NewLine + "Request = " + PfProNetApi.TransactionRequest);
        ///  	Console.WriteLine(Environment.NewLine + "Response = " + Response);
        ///  	// Following lines of code are optional. 
        ///  	// Begin optional code for displaying SDK errors ...
        ///  	// It is used to read any errors that might have occurred in the SDK.
        /// 	    String TransErrors = PfProNetApi.TransactionContext.ToString();
        /// 	    if (TransErrors != null &amp;&amp; TransErrors.Length > 0)	
        /// 	    {
        /// 	    	Console.WriteLine(Environment.NewLine + "Transaction Errors from SDK = " + TransErrors);
        /// 	    }
        /// 	   
        ///      // End optional code for displaying SDK errors.
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        ///  ' Sample Request. 
        ///  ' Please replace user, vendor, password &amp; partner with your merchant information.
        ///  Dim Request As String = "TRXTYPE=S&amp;ACCT=5105105105105100&amp;EXPDATE=0115&amp;TENDER=C&amp;INVNUM=INV12345&amp;AMT=25.12&amp;PONUM=PO12345&amp;STREET=123 Main St.&amp;ZIP=12345&amp;USER=user&amp;VENDOR=vendor&amp;PARTNER=partner&amp;PWD=password"
        ///  
        ///  ' Create an instance of PayflowNETAPI.
        ///  Dim PfProNetApi As PayflowNETAPI = new PayflowNETAPI
        ///  
        ///  ' RequestId is a unique string that is required for each &amp; every transaction. 
        ///  ' The merchant can use her/his own algorithm to generate this unique request id or 
        ///  ' use the SDK provided API to generate this as shown below (PayflowUtility.GetRequestId()).
        ///  Dim Response As String = PfProNetApi.SubmitTransaction(Request, PayflowUtility.RequestId)
        ///  
        ///  ' To write the Response on to the console.
        ///  Console.WriteLine(Environment.NewLine + "Request = " + PfProNetApi.TransactionRequest)
        ///  Console.WriteLine(Environment.NewLine + "Response = " + Response)
        ///  
        ///  ' Following lines of code are optional. 
        ///  ' Begin optional code for displaying SDK errors ...
        ///  ' It is used to read any errors that might have occurred in the SDK.
        ///  
        ///  Dim TransErrors As String = PfProNetApi.TransactionContext.ToString()
        ///  If (Not TransErrors Is Nothing And TransErrors.Length > 0) Then
        ///    Console.WriteLine(Environment.NewLine + "Transaction Errors from SDK = " + TransErrors)
        ///  End If
        ///  
        ///  'End optional code for displaying SDK errors.
        ///  </code>
        /// </example>
        public PayflowNetapi(string hostAddress, int hostPort, string proxyAddress, int proxyPort, string proxyLogon,
            string proxyPassword)
            : this(hostAddress, hostPort, 0, proxyAddress, proxyPort, proxyLogon, proxyPassword)
        {
        }

        /// <summary>
        ///     PayflowNETAPI Constructor
        /// </summary>
        /// <param name="hostAddress">Payflow Host Address.</param>
        /// <param name="hostPort">Payflow Host Port.</param>
        /// <param name="timeOut">Transaction Timeout.</param>
        /// <summary>
        ///     PayflowNETAPI is used to submit a Name-value pair or XMLPay request to
        ///     PayPal payment gateway for online payment processing. The response
        ///     returned is the string value of the response from the PayPal payment
        ///     gateway.
        /// </summary>
        /// <remarks>
        ///     Instance of PayflowNETAPI initialized with the information related
        ///     to connection to the PayPal payment gateway.
        ///     If the empty constructor of this class is used to create the object or
        ///     passed values are empty, then The following values (if empty)
        ///     are looked for as follows:
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
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	/// ..........
        ///  	// Sample Request. 
        ///  	// Please replace user, vendor, password &amp; partner with your merchant information.
        ///  	String Request = "TRXTYPE=S&amp;ACCT=5105105105105100&amp;EXPDATE=0115&amp;TENDER=C&amp;INVNUM=INV12345&amp;AMT=25.12&amp;PONUM=PO12345&amp;STREET=123 Main St.&amp;ZIP=12345&amp;USER=user&amp;VENDOR=vendor&amp;PARTNER=partner&amp;PWD=password";
        ///  	// Create an instance of PayflowNETAPI.
        ///  	PayflowNETAPI PfProNetApi = new PayflowNETAPI();
        ///  	// RequestId is a unique string that is required for each &amp; every transaction. 
        ///  	// The merchant can use her/his own algorithm to generate this unique request id or 
        ///  	// use the SDK provided API to generate this as shown below (PayflowUtility.RequestId).
        ///  	String Response = PfProNetApi.SubmitTransaction(Request, PayflowUtility.RequestId);
        ///  	// To write the Response on to the console.
        ///  	Console.WriteLine(Environment.NewLine + "Request = " + PfProNetApi.TransactionRequest);
        ///  	Console.WriteLine(Environment.NewLine + "Response = " + Response);
        ///  	// Following lines of code are optional. 
        ///  	// Begin optional code for displaying SDK errors ...
        ///  	// It is used to read any errors that might have occurred in the SDK.
        /// 	    String TransErrors = PfProNetApi.TransactionContext.ToString();
        /// 	    if (TransErrors != null &amp;&amp; TransErrors.Length > 0)	
        /// 	    {
        /// 	    	Console.WriteLine(Environment.NewLine + "Transaction Errors from SDK = " + TransErrors);
        /// 	    }
        /// 	   
        ///      // End optional code for displaying SDK errors.
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        ///  ' Sample Request. 
        ///  ' Please replace user, vendor, password &amp; partner with your merchant information.
        ///  Dim Request As String = "TRXTYPE=S&amp;ACCT=5105105105105100&amp;EXPDATE=0115&amp;TENDER=C&amp;INVNUM=INV12345&amp;AMT=25.12&amp;PONUM=PO12345&amp;STREET=123 Main St.&amp;ZIP=12345&amp;USER=user&amp;VENDOR=vendor&amp;PARTNER=partner&amp;PWD=password"
        ///  
        ///  ' Create an instance of PayflowNETAPI.
        ///  Dim PfProNetApi As PayflowNETAPI = new PayflowNETAPI
        ///  
        ///  ' RequestId is a unique string that is required for each &amp; every transaction. 
        ///  ' The merchant can use her/his own algorithm to generate this unique request id or 
        ///  ' use the SDK provided API to generate this as shown below (PayflowUtility.GetRequestId()).
        ///  Dim Response As String = PfProNetApi.SubmitTransaction(Request, PayflowUtility.RequestId)
        ///  
        ///  ' To write the Response on to the console.
        ///  Console.WriteLine(Environment.NewLine + "Request = " + PfProNetApi.TransactionRequest)
        ///  Console.WriteLine(Environment.NewLine + "Response = " + Response)
        ///  
        ///  ' Following lines of code are optional. 
        ///  ' Begin optional code for displaying SDK errors ...
        ///  ' It is used to read any errors that might have occurred in the SDK.
        ///  
        ///  Dim TransErrors As String = PfProNetApi.TransactionContext.ToString()
        ///  If (Not TransErrors Is Nothing And TransErrors.Length > 0) Then
        ///    Console.WriteLine(Environment.NewLine + "Transaction Errors from SDK = " + TransErrors)
        ///  End If
        ///  
        ///  'End optional code for displaying SDK errors.
        ///  </code>
        /// </example>
        public PayflowNetapi(string hostAddress, int hostPort, int timeOut)
            : this(hostAddress, hostPort, timeOut, null, 0, null, null)
        {
        }

        /// <summary>
        ///     PayflowNETAPI Constructor
        /// </summary>
        /// <param name="hostAddress">Payflow Host Address.</param>
        /// <summary>
        ///     PayflowNETAPI is used to submit a Name-value pair or XMLPay request to
        ///     PayPal payment gateway for online payment processing. The response
        ///     returned is the string value of the response from the PayPal payment
        ///     gateway.
        /// </summary>
        /// <remarks>
        ///     Instance of PayflowNETAPI initialized with the information related
        ///     to connection to the PayPal payment gateway.
        ///     If the empty constructor of this class is used to create the object or
        ///     passed values are empty, then The following values (if empty)
        ///     are looked for as follows:
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
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	/// ..........
        ///  	// Sample Request. 
        ///  	// Please replace user, vendor, password &amp; partner with your merchant information.
        ///  	String Request = "TRXTYPE=S&amp;ACCT=5105105105105100&amp;EXPDATE=0115&amp;TENDER=C&amp;INVNUM=INV12345&amp;AMT=25.12&amp;PONUM=PO12345&amp;STREET=123 Main St.&amp;ZIP=12345&amp;USER=user&amp;VENDOR=vendor&amp;PARTNER=partner&amp;PWD=password";
        ///  	// Create an instance of PayflowNETAPI.
        ///  	PayflowNETAPI PfProNetApi = new PayflowNETAPI();
        ///  	// RequestId is a unique string that is required for each &amp; every transaction. 
        ///  	// The merchant can use her/his own algorithm to generate this unique request id or 
        ///  	// use the SDK provided API to generate this as shown below (PayflowUtility.RequestId).
        ///  	String Response = PfProNetApi.SubmitTransaction(Request, PayflowUtility.RequestId);
        ///  	// To write the Response on to the console.
        ///  	Console.WriteLine(Environment.NewLine + "Request = " + PfProNetApi.TransactionRequest);
        ///  	Console.WriteLine(Environment.NewLine + "Response = " + Response);
        ///  	// Following lines of code are optional. 
        ///  	// Begin optional code for displaying SDK errors ...
        ///  	// It is used to read any errors that might have occurred in the SDK.
        /// 	    String TransErrors = PfProNetApi.TransactionContext.ToString();
        /// 	    if (TransErrors != null &amp;&amp; TransErrors.Length > 0)	
        /// 	    {
        /// 	    	Console.WriteLine(Environment.NewLine + "Transaction Errors from SDK = " + TransErrors);
        /// 	    }
        /// 	   
        ///      // End optional code for displaying SDK errors.
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        ///  ' Sample Request. 
        ///  ' Please replace user, vendor, password &amp; partner with your merchant information.
        ///  Dim Request As String = "TRXTYPE=S&amp;ACCT=5105105105105100&amp;EXPDATE=0115&amp;TENDER=C&amp;INVNUM=INV12345&amp;AMT=25.12&amp;PONUM=PO12345&amp;STREET=123 Main St.&amp;ZIP=12345&amp;USER=user&amp;VENDOR=vendor&amp;PARTNER=partner&amp;PWD=password"
        ///  
        ///  ' Create an instance of PayflowNETAPI.
        ///  Dim PfProNetApi As PayflowNETAPI = new PayflowNETAPI
        ///  
        ///  ' RequestId is a unique string that is required for each &amp; every transaction. 
        ///  ' The merchant can use her/his own algorithm to generate this unique request id or 
        ///  ' use the SDK provided API to generate this as shown below (PayflowUtility.GetRequestId()).
        ///  Dim Response As String = PfProNetApi.SubmitTransaction(Request, PayflowUtility.RequestId)
        ///  
        ///  ' To write the Response on to the console.
        ///  Console.WriteLine(Environment.NewLine + "Request = " + PfProNetApi.TransactionRequest)
        ///  Console.WriteLine(Environment.NewLine + "Response = " + Response)
        ///  
        ///  ' Following lines of code are optional. 
        ///  ' Begin optional code for displaying SDK errors ...
        ///  ' It is used to read any errors that might have occurred in the SDK.
        ///  
        ///  Dim TransErrors As String = PfProNetApi.TransactionContext.ToString()
        ///  If (Not TransErrors Is Nothing And TransErrors.Length > 0) Then
        ///    Console.WriteLine(Environment.NewLine + "Transaction Errors from SDK = " + TransErrors)
        ///  End If
        ///  
        ///  'End optional code for displaying SDK errors.
        ///  </code>
        /// </example>
        public PayflowNetapi(string hostAddress)
            : this(hostAddress, 0, 0, null, 0, null, null)
        {
        }

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets the Request Id.

        public string RequestId { get; private set; }


        /// <summary>
        ///     Gets the Transaction Context object.
        /// </summary>

        public Context TransactionContext => _mTransactionContext;

        /// <summary>
        ///     Gets the Transaction response.
        /// </summary>

        public string TransactionResponse { get; private set; }

        /// <summary>
        ///     Gets the Transaction request.
        /// </summary>


        public string TransactionRequest { get; private set; }

        /// <summary>
        ///     Gets, Sets flag for Strong Assembly
        ///     Transaction.
        /// </summary>

        internal bool IsStrongAssemblyTransaction { get; set; }

        /// <summary>
        ///     Gets the PayflowNETAPI Client Version.
        /// </summary>

        public string Version => PayflowConstants.ClientType + PayflowConstants.ClientVersion;

        internal bool IsXmlPayRequest { get; set; }

        /// <summary>
        ///     Client information.
        /// </summary>
        [ComVisible(false)]
        public ClientInfo ClientInfo { get; set; }

        #endregion

        #region "Functions"

        /// <summary>
        ///     SetParameters will be used to initialize the different parameters passed by the user. This has been kept
        ///     as a public function since this needs to be called by the COM implementation. This is an undocumented
        ///     functionionality which the pure dotNET client are not suppose to use.
        /// </summary>
        public void SetParameters(string hostAddress,
            int hostPort,
            int timeOut,
            string proxyAddress,
            int proxyPort,
            string proxyLogon,
            string proxyPassword,
            string trace,
            string logLevel,
            string logFileName,
            string logFileSize,
            bool wrapperIsCom)
        {
            _mTransactionContext.ClearErrors();
            if (wrapperIsCom)
            {
                PayflowConstants.Trace = trace;
                PayflowUtility.TraceInitialized = true;
                Logger.SetInstance(logLevel, logFileName, logFileSize, wrapperIsCom);
            }

            if (hostAddress != null) _mHostAddress = hostAddress.Trim();

            _mHostPort = hostPort;
            _mTimeOut = timeOut;

            if (proxyAddress != null) _mProxyAddress = proxyAddress.Trim();

            _mProxyPort = proxyPort;

            if (proxyLogon != null) _mProxyLogon = proxyLogon.Trim();

            if (proxyPassword != null) _mProxyPassword = proxyPassword.Trim();

            InitDefaultValues();
        }

        /// <summary>
        ///     <para>Submits a transaction to Payflow Server.</para>
        ///     PayflowNETAPI is used to submit a Name-value pair or XMLPay request to
        ///     PayPal payment gateway for online payment processing. The response
        ///     returned is the string value of the response from the PayPal payment
        ///     gateway.
        /// </summary>
        /// <param name="paramList">Parameter list.</param>
        /// <param name="requestId">Request Id</param>
        /// <returns>String value of transaction response.</returns>
        /// <remarks>
        ///     Instance of PayflowNETAPI initialized with the information related
        ///     to connection to the PayPal payment gateway.
        ///     If the empty constructor of this class is used to create the object or
        ///     passed values are empty, then The following values (if empty)
        ///     are looked for as follows:
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
        ///             <term>Transaction TimeOut</term>
        ///             <description>45 seconds</description>
        ///             <description>NA</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	/// ..........
        ///  	// Sample Request. 
        ///  	// Please replace user, vendor, password &amp; partner with your merchant information.
        ///  	String Request = "TRXTYPE=S&amp;ACCT=5105105105105100&amp;EXPDATE=0115&amp;TENDER=C&amp;INVNUM=INV12345&amp;AMT=25.12&amp;PONUM=PO12345&amp;STREET=123 Main St.&amp;ZIP=12345&amp;USER=user&amp;VENDOR=vendor&amp;PARTNER=partner&amp;PWD=password";
        ///  	// Create an instance of PayflowNETAPI.
        ///  	PayflowNETAPI PayflowNetApi = new PayflowNETAPI();
        ///  	// RequestId is a unique string that is required for each &amp; every transaction. 
        ///  	// The merchant can use her/his own algorithm to generate this unique request id or 
        ///  	// use the SDK provided API to generate this as shown below (PayflowUtility.RequestId).
        ///  	String Response = PayflowNetApi.SubmitTransaction(Request, PayflowUtility.RequestId);
        ///  	// To write the Response on to the console.
        ///  	Console.WriteLine(Environment.NewLine + "Request = " + PayflowNetApi.TransactionRequest);
        ///  	Console.WriteLine(Environment.NewLine + "Response = " + Response);
        ///  	// Following lines of code are optional. 
        ///  	// Begin optional code for displaying SDK errors ...
        ///  	// It is used to read any errors that might have occurred in the SDK.
        /// 	    String TransErrors = PayflowNetApi.TransactionContext.ToString();
        /// 	    if (TransErrors != null &amp;&amp; TransErrors.Length > 0)	
        /// 	    {
        /// 	     	Console.WriteLine(Environment.NewLine + "Transaction Errors from SDK = " + TransErrors);
        /// 	    }
        /// 	   
        ///      // End optional code for displaying SDK errors.
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		' Sample Request. 
        /// 		' Please replace user, vendor, password &amp; partner with your merchant information.
        /// 		Dim Request As String = "TRXTYPE=S&amp;ACCT=5105105105105100&amp;EXPDATE=0115&amp;TENDER=C&amp;INVNUM=INV12345&amp;AMT=25.12&amp;PONUM=PO12345&amp;STREET=123 Main St.&amp;ZIP=12345&amp;USER=user&amp;VENDOR=vendor&amp;PARTNER=partner&amp;PWD=password"
        ///  
        /// 		' Create an instance of PayflowNETAPI.
        /// 		Dim PayflowNetApi As PayflowNETAPI = new PayflowNETAPI
        ///  
        /// 		' RequestId is a unique string that is required for each &amp; every transaction. 
        /// 		' The merchant can use her/his own algorithm to generate this unique request id or 
        /// 		' use the SDK provided API to generate this as shown below (PayflowUtility.GetRequestId()).
        /// 		Dim Response As String = PayflowNetApi.SubmitTransaction(Request, PayflowUtility.RequestId)
        ///  
        /// 		' To write the Response on to the console.
        /// 		Console.WriteLine(Environment.NewLine + "Request = " + PayflowNetApi.TransactionRequest)
        /// 		Console.WriteLine(Environment.NewLine + "Response = " + Response)
        ///  
        /// 		' Following lines of code are optional. 
        /// 		' Begin optional code for displaying SDK errors ...
        /// 		' It is used to read any errors that might have occurred in the SDK.
        ///  
        /// 		Dim TransErrors As String = PayflowNetApi.TransactionContext.ToString()
        /// 		If (Not TransErrors Is Nothing And TransErrors.Length > 0) Then
        /// 			Console.WriteLine(Environment.NewLine + "Transaction Errors from SDK = " + TransErrors)
        /// 		End If
        ///  
        /// 		'End optional code for displaying SDK errors.
        ///  </code>
        /// </example>
        public async Task<string> SubmitTransactionAsync(string paramList, string requestId)
        {
            Logger.Instance.Log("PayPal.Payments.Communication.PayflowNETAPI.SubmitTransaction(String): Entered.",
                PayflowConstants.SeverityDebug);

            string retVal;
            RequestId = requestId;
            GlobalClass.GlobalVar = RequestId;
            _mTransRqst = paramList;
            TransactionRequest = PayflowUtility.MaskSensitiveFields(paramList);

            if (ClientInfo == null) ClientInfo = new ClientInfo();

            ClientInfo.SetClientVersion(PayflowConstants.ClientVersion);
            ClientInfo.SetClientType(PayflowConstants.ClientType);

            if (IsStrongAssemblyTransaction)
                ClientInfo.RequestType = PayflowConstants.StrongAssembly;
            else
                ClientInfo.RequestType = PayflowConstants.WeakAssembly;
            try
            {
                CheckTransactionArgs(paramList, requestId);

                if (!IsStrongAssemblyTransaction)
                {
                    _mTransactionContext.LoadLoggerErrs = true;
                    var errors = PayflowUtility.AlignContext(_mTransactionContext, IsXmlPayRequest);
                    _mTransactionContext.LoadLoggerErrs = false;
                    _mTransactionContext.ClearErrors();
                    _mTransactionContext.AddErrors(errors);
                }

                if (_mTransactionContext.HighestErrorLvl == PayflowConstants.SeverityFatal)
                {
                    var errorList = _mTransactionContext.GetErrors(PayflowConstants.SeverityFatal);
                    var firstFatalError = (ErrorObject) errorList[0];
                    retVal = firstFatalError.ToString();
                    TransactionRequest = PayflowUtility.MaskSensitiveFields(paramList);
                    TransactionResponse = retVal;
                    Logger.Instance.Log(
                        "PayPal.Payments.Communication.PayflowNETAPI.SubmitTransaction(String): Exiting.",
                        PayflowConstants.SeverityDebug);
                    return retVal;
                }

                _mPaymentStateMachine = PaymentStateMachine.Instance;


                _mPaymentStateMachine.InitializeContext(_mHostAddress, _mHostPort, _mTimeOut, _mProxyAddress,
                    _mProxyPort, _mProxyLogon, _mProxyPassword, ClientInfo);

                //Initialize transaction
                _mPaymentStateMachine.InitTrans(paramList, requestId);

                //Begin Payflow Timeout Check Point 1
                long timeRemainingMsec;

                if (PayflowUtility.IsTimedOut(_mPaymentStateMachine.TimeOut, _mPaymentStateMachine.StartTime,
                    out timeRemainingMsec))
                {
                    var addlMessage = "Input timeout in millsec = " + _mPaymentStateMachine.TimeOut;

                    var err = PayflowUtility.PopulateCommError(PayflowConstants.ETimeoutWaitResp, null,
                        PayflowConstants.SeverityFatal, _mPaymentStateMachine.IsXmlPayRequest, addlMessage);

                    if (!_mPaymentStateMachine.PsmContext.IsCommunicationErrorContained(err))
                        _mPaymentStateMachine.PsmContext.AddError(err);
                }
                else
                {
                    _mPaymentStateMachine.TimeOut = timeRemainingMsec;
                }
                //End Payflow Timeout Check Point 1


                //Begin Toggle through states 
                while (_mPaymentStateMachine.InProgress) await _mPaymentStateMachine.ExecuteAsync();
                //End Toggle through states 


                TransactionResponse = _mPaymentStateMachine.Response;
                retVal = TransactionResponse;
                ClientInfo = _mPaymentStateMachine.ClientInfo;
                //Assign the context data 
                RequestId = _mPaymentStateMachine.RequestId;
                TransactionRequest = PayflowUtility.MaskSensitiveFields(paramList);
                _mTransactionContext.AddErrors(_mPaymentStateMachine.PsmContext.GetErrors());
                _mPaymentStateMachine = null;
                var errList = PayflowUtility.AlignContext(_mTransactionContext, IsXmlPayRequest);
                _mTransactionContext.LoadLoggerErrs = false;
                _mTransactionContext.ClearErrors();
                _mTransactionContext.AddErrors(errList);
            }
            catch (Exception ex)
            {
                retVal = ex.ToString();
            }
            finally
            {
            }

            // Logger.Instance.Log("PayPal.Payments.Communication.PayflowNETAPI.SubmitTransaction(String): RetVal = " + RetVal, PayflowConstants.SEVERITY_DEBUG);
            Logger.Instance.Log("PayPal.Payments.Communication.PayflowNETAPI.SubmitTransaction(String): Exiting.",
                PayflowConstants.SeverityDebug);
            return retVal;
        }


        /// <summary>
        ///     Initializes the default connection values
        /// </summary>
        private void InitDefaultValues()
        {
            Logger.Instance.Log("PayPal.Payments.Communication.PayflowNETAPI.InitDefaultValues(): Entered",
                PayflowConstants.SeverityDebug);
            //Check if the values held in the PayPal server connection related params if they are passed null or 0 (for int values) then initialize
            // them to appropriate default values.

            // if timeout value not passed, set the TimeOut to default value.
            if (_mTimeOut == 0)
                _mTimeOut = PayflowConstants.DefaultTimeout * 1000;
            else
                _mTimeOut = _mTimeOut * 1000;


            // Set the timeout value.
            /*if (mTimeOut == 0)
            {
                try
                {
                    // Obtain timeout value from .config file, if present.
                    mTimeOut = Convert.ToInt32(PayflowUtility.AppSettings(PayflowConstants.INTL_PARAM_PAYFLOW_TIMEOUT)) * 1000;
                }
                catch (Exception)
                {
                    // Set default if TIMEOUT is not in .config file.
                    mTimeOut = PayflowConstants.DEFAULT_TIMEOUT * 1000;
                    Logger.Instance.Log("PayPal.Payments.Communication.PayflowNETAPI.InitDefaultValues(): Timeout set to Default Value: " + mTimeOut.ToString(), PayflowConstants.SEVERITY_DEBUG);
                }
            }
            */
            if (_mHostPort == 0) _mHostPort = PayflowConstants.DefaultHostport;

            try
            {
                if (_mHostAddress == null || _mHostAddress.Length == 0)
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
                            _mTransactionContext.AddError(error);
                        }
                        else
                        {
                            _mHostAddress = hostAddress;
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
                        _mTransactionContext.AddError(error);
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
                _mTransactionContext.AddError(error);
            }

            Logger.Instance.Log("PayPal.Payments.Communication.PayflowNETAPI.InitDefaultValues(): Exiting",
                PayflowConstants.SeverityDebug);
        }

        /// <summary>
        ///     Checks the vital transaction arguments
        ///     for null or empty and populates context
        ///     accordingly.
        /// </summary>
        /// <param name="paramList">Param list</param>
        /// <param name="requestId">Request Id</param>
        private void CheckTransactionArgs(string paramList, string requestId)
        {
            Logger.Instance.Log(
                "PayPal.Payments.Communication.PayflowNETAPI.CheckTransactionArgs(String,String,String,bool): Entered",
                PayflowConstants.SeverityDebug);
            try
            {
                if (paramList == null || paramList.Trim().Length == 0)
                {
                    var respMessage = PayflowConstants.ParamResult
                                      + PayflowConstants.SeparatorNvp
                                      + (string) PayflowConstants.CommErrorCodes[PayflowConstants.EEmptyParamList]
                                      + PayflowConstants.DelimiterNvp
                                      + PayflowConstants.ParamRespmsg
                                      + PayflowConstants.SeparatorNvp
                                      + (string) PayflowConstants.CommErrorMessages[PayflowConstants.EEmptyParamList];

                    var error = new ErrorObject(PayflowConstants.SeverityFatal, "", respMessage);
                    _mTransactionContext.AddError(error);
                }
                else
                {
                    //Check for XmlPay 1.0
                    //We are not supporting Xml Pay 1.0
                    paramList = paramList.TrimStart(' ');
                    var index = paramList.TrimStart().IndexOf(PayflowConstants.XmlId);
                    if (index >= 0 && paramList.IndexOf("<") == 0)
                    {
                        var version = PayflowUtility.GetXmlAttribute(paramList, PayflowConstants.XmlParamVersion);
                        if (version != null && version.Trim().Length > 0)
                        {
                            IsXmlPayRequest = true;
                            if ("1.0".Equals(version))
                            {
                                var addlMessage = " ,Input XMLPay Request Version = " + version;
                                var errParams = new[]
                                {
                                    (string) PayflowConstants.CommErrorCodes["E_VERSION_NOT_SUPPORTED"],
                                    (string) PayflowConstants.CommErrorMessages["E_VERSION_NOT_SUPPORTED"] + addlMessage
                                };
                                var error = new ErrorObject(PayflowConstants.SeverityFatal,
                                    PayflowConstants.MsgCommunicationErrorXmlpayNoResponseId, errParams);
                                _mTransactionContext.AddError(error);
                            }
                        }
                    }
                    else
                    {
                        if (!IsStrongAssemblyTransaction)
                            ParameterListValidator.Validate(paramList, false, ref _mTransactionContext);
                    }
                }

                if (requestId == null || requestId.Trim().Length == 0)
                {
                    var respMessage = PayflowConstants.ParamResult
                                      + PayflowConstants.SeparatorNvp
                                      + (string) PayflowConstants.CommErrorCodes[PayflowConstants.EMissingRequestId]
                                      + PayflowConstants.DelimiterNvp
                                      + PayflowConstants.ParamRespmsg
                                      + PayflowConstants.SeparatorNvp
                                      + (string) PayflowConstants.CommErrorMessages[PayflowConstants.EMissingRequestId];

                    var error = new ErrorObject(PayflowConstants.SeverityFatal, "", respMessage);
                    _mTransactionContext.AddError(error);
                }
            }
            catch (Exception ex)
            {
                var addlMessage = PayflowConstants.EmptyString;
                if (ex is XmlException)
                {
                    IsXmlPayRequest = true;
                    addlMessage = "Error while parsing the xml request.";
                }
                else
                {
                    IsXmlPayRequest = false;
                }

                var error = PayflowUtility.PopulateCommError(PayflowConstants.EUnknownState, ex,
                    PayflowConstants.SeverityFatal, IsXmlPayRequest, addlMessage);
                _mTransactionContext.AddError(error);
            }

            Logger.Instance.Log(
                "PayPal.Payments.Communication.PayflowNETAPI.CheckTransactionArgs(String,String,String,bool): Exiting",
                PayflowConstants.SeverityDebug);
        }


        /*
         This function has been out in place to support generation of requestID from the COM Wrapper.This is
         because a static function cannot be called from COM Wrapper and PayflowUtility is a static class and has 
         a private constructor.
        */
        public string GenerateRequestId()
        {
            return PayflowUtility.RequestId;
        }

        #region "PAYFLOW-HEADERs related methods"

        /// <summary>
        ///     Adds a Transaction header
        /// </summary>
        /// <param name="headerName">Header Name</param>
        /// <param name="headerValue">Header Value</param>
        public void AddTransHeader(string headerName, string headerValue)
        {
            AddHeader(headerName, headerValue);
        }

        /// <summary>
        ///     Removes a Transaction header
        /// </summary>
        /// <param name="headerName">Header Name</param>
        public void RemoveTransHeader(string headerName)
        {
            RemoveHeader(headerName);
        }

        /// <summary>
        ///     Removes a header
        /// </summary>
        /// <param name="headerName">Header Name</param>
        private void RemoveHeader(string headerName)
        {
            if (ClientInfo != null)
                if (ClientInfo.ClientInfoHash.ContainsKey(headerName))
                    ClientInfo.ClientInfoHash.Remove(headerName);
        }

        /// <summary>
        ///     Adds a header
        /// </summary>
        /// <param name="headerName">Header name</param>
        /// <param name="headerValue">Header value</param>
        private void AddHeader(string headerName, string headerValue)
        {
            if (ClientInfo == null) ClientInfo = new ClientInfo();

            ClientInfo.AddHeaderToHash(headerName, headerValue);
        }

        #endregion

        #endregion

        #region "System.Object overides"

        /// <summary>
        ///     This function overides the System.Object.Equals function.
        /// </summary>
        /// <param name="obj">Object which needs to be compared.</param>
        /// <returns>
        ///     Returns the boolean value indicating if the Object passed is equal to the current object.
        /// </returns>
        [ComVisible(false)]
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        ///     This function overides the System.Object.GetHashCode function.
        /// </summary>
        /// <returns>
        ///     Returns the HashCode for the current instance.
        /// </returns>
        [ComVisible(false)]
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        ///     This function overides the System.Object.ToString function.
        /// </summary>
        /// <returns>
        ///     Returns the String representation of the current instance.
        /// </returns>
        [ComVisible(false)]
        public override string ToString()
        {
            return base.ToString();
        }

        #endregion
    }
}