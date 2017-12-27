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
using System.Text;
using System.Threading.Tasks;
using PFProSDK.Common;
using PFProSDK.Common.Exceptions;
using PFProSDK.Common.Logging;
using PFProSDK.Common.Utility;
using PFProSDK.Communication;
using PFProSDK.DataObjects;

#endregion

namespace PFProSDK.Transactions
{
    /// <summary>
    ///     This class is the base class for all transaction objects. It has methods for generating the transaction request,
    ///     sending it to the server and obtaining the response.
    ///     For an usage of this class, please see the examples in SamplesCS + SamplesVB folders namely DOSale_Base.cs
    ///     and DOSale_Base.vb.
    /// </summary>
    /// <remarks>This class can be extended to create a new transaction type.</remarks>
    /// <example>
    ///     This example shows how to create and perform an Sale transaction using a Basetransaction Object.
    ///     <code lang="C#" escaped="false">
    /// 		..........
    /// 		..........
    /// 		//Populate required data objects.
    /// 		..........
    /// 		..........
    /// 		
    ///  //Create a new Base Transaction.
    /// 	BaseTransaction Trans = new BaseTransaction("S",
    /// 		User, Connection, Inv, Card, PayflowUtility.RequestId);
    /// 	//Submit the transaction.
    /// 	Trans.SubmitTransaction();
    /// 	// Get the Response
    /// 	Response Resp = Trans.Response;
    /// 
    /// 	// Display the transaction response parameters.
    /// 	if (Resp != null)
    /// 	{
    /// 		// Get the Transaction Response parameters.
    /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
    /// 		if (TrxnResponse != null)
    /// 		{
    /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
    /// 			Console.WriteLine("PNREF = " + TrxnResponse.Pnref);
    /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
    /// 			Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode);
    /// 			Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr);
    /// 			Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip);
    /// 			Console.WriteLine("IAVS = " + TrxnResponse.IAVS);
    /// 		}
    /// 		// Get the Fraud Response parameters.
    /// 		FraudResponse FraudResp =  Resp.FraudResponse;
    /// 		if (FraudResp != null)
    /// 		{
    /// 			Console.WriteLine("PREFPSMSG = " + FraudResp.PreFpsMsg);
    /// 			Console.WriteLine("POSTFPSMSG = " + FraudResp.PostFpsMsg);
    /// 		}
    /// 		// Get the Transaction Context and check for any contained SDK specific errors (optional code).
    /// 		Context TransCtx = Resp.TransactionContext;
    /// 		if (TransCtx != null &amp;&amp; TransCtx.getErrorCount() > 0)
    /// 		{
    /// 			Console.WriteLine(Environment.NewLine + "Transaction Errors = " + TransCtx.ToString());
    /// 		}
    /// 	}
    /// </code>
    ///     <code lang="Visual Basic" escaped="false">
    /// 		..........
    /// 		..........
    /// 		'Populate required data objects.
    /// 		..........
    /// 		..........
    /// 		
    /// 		' Create a new Base Transaction.
    /// 		Dim Trans As BaseTransaction = New BaseTransaction("S", User, Connection, Inv, Card, PayflowUtility.RequestId)
    /// 		' Submit the transaction.
    /// 		Trans.SubmitTransaction()
    /// 		' Get the Response
    /// 		Dim Resp As Response = Trans.Response
    /// 		If Not Resp Is Nothing Then
    /// 		' Get the Transaction Response parameters.
    /// 		Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
    /// 		If Not TrxnResponse Is Nothing Then
    /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result)
    /// 			Console.WriteLine("PNREF = " + TrxnResponse.Pnref)
    /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
    /// 			Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode)
    /// 			Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr)
    /// 			Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip)
    /// 			Console.WriteLine("IAVS = " + TrxnResponse.IAVS)
    /// 		End If
    /// 		' Get the Fraud Response parameters.
    /// 		Dim FraudResp As FraudResponse = Resp.FraudResponse
    /// 		If Not FraudResp Is Nothing Then
    /// 			Console.WriteLine("PREFPSMSG = " + FraudResp.PreFpsMsg)
    /// 			Console.WriteLine("POSTFPSMSG = " + FraudResp.PostFpsMsg)
    /// 		End If
    /// 
    /// 		' Display the response.
    /// 		Console.WriteLine(Environment.NewLine + PayflowUtility.GetStatus(Resp))
    /// 		' Get the Transaction Context and check for any contained SDK specific errors (optional code).
    /// 		Dim TransCtx As Context = Resp.TransactionContext
    /// 		If (Not TransCtx Is Nothing) And (TransCtx.getErrorCount() > 0) Then
    /// 			Console.WriteLine(Environment.NewLine + "Transaction Errors = " + TransCtx.ToString())
    /// 		End If
    /// 	End If
    ///  </code>
    /// </example>
    public class BaseTransaction
    {
        #region "Member Variables"

        /// <summary>
        ///     Arraylist of Extend Data objects. The arraylist contains objects of type ExtendData.
        ///     ExtendData has a parameter name and value and is used for sending any additional parameter currently not
        ///     supported by the SDK.
        ///     <seealso cref="ExtendData" />
        /// </summary>
        private ArrayList _mExtData;

        /// <summary>
        ///     Connection parameters to connect to the PayPal Payment Server.
        ///     <seealso cref="PayflowConnectionData" />
        /// </summary>
        private readonly PayflowConnectionData _mPayflowConnectionData;

        /// <summary>
        ///     Transaction request in Name-Value Pair format.
        ///     <example>
        ///         <code>
        /// TRXTYPE[1]=S&amp;ACCT[16]=5105105105105100&amp;EXPDATE[4]=0115&amp;TENDER[1]=C&amp;INVNUM[8]=INV12345&amp;AMT[5]=25.12
        /// &amp;PONUM[7]=PO12345&amp;STREET[23]=123 Main St.&amp;ZIP[5]=12345&amp;
        /// USER=user&amp;VENDOR=vendor&amp;PARTNER=partner&amp;PWD=password
        /// </code>
        ///     </example>
        /// </summary>
        private string _mRequest;

        /// <summary>
        ///     Transaction invoice object. Has parameters like Amt, InvNum, BillTo, ShipTo etc.
        ///     <seealso cref="Invoice" />
        /// </summary>
        private readonly Invoice _mInvoice;

        /// <summary>
        ///     Response object for the Transaction. Has objects like Transaction Response, Fraud Response,
        ///     Recurring Response etc.
        ///     <seealso cref="Response" />
        /// </summary>
        private Response _mResponse;

        /// <summary>
        ///     Payflow user credentials. Has parameters like User, Vendor, Partner, Password etc.
        ///     <seealso cref="UserInfo" />
        /// </summary>
        private readonly UserInfo _mUserInfo;

        /// <summary>
        ///     Holds  USER1 to USER10 fields.
        ///     <seealso cref="UserItem" />
        /// </summary>
        private UserInfo _mUserItem;

        /// <summary>
        ///     Request Buffer. This is used to build the request string in Name-Value pair format from Data Objects.
        /// </summary>
        private StringBuilder _mRequestBuffer;

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets the StringBuilder object for RequestBuffer.
        /// </summary>
        internal virtual StringBuilder RequestBuffer => _mRequestBuffer;


        /// <summary>
        ///     Type of transaction to perform, indicated by a single character.
        ///     Credit payments require an ORIGID referring to an earlier Debit/Sale payment,
        ///     and the AMT must be empty or the exact amount of the original Debit/Sale payment.
        ///     Allowed TrxType values are:
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Transaction Type</term>
        ///             <description>Transaction Name</description>
        ///         </listheader>
        ///         <item>
        ///             <term>S</term>
        ///             <description>Sale/Debit</description>
        ///         </item>
        ///         <item>
        ///             <term>A</term>
        ///             <description>Voice Authorization/Force</description>
        ///         </item>
        ///         <item>
        ///             <term>C</term>
        ///             <description>Credit</description>
        ///         </item>
        ///         <item>
        ///             <term>V</term>
        ///             <description>Void</description>
        ///         </item>
        ///         <item>
        ///             <term>D</term>
        ///             <description>Delayed Capture</description>
        ///         </item>
        ///         <item>
        ///             <term>F</term>
        ///             <description>Force/Voice Authorization</description>
        ///         </item>
        ///         <item>
        ///             <term>I</term>
        ///             <description>Inquiry</description>
        ///         </item>
        ///         <item>
        ///             <term>R</term>
        ///             <description>Recurring billing</description>
        ///         </item>
        ///     </list>
        ///     <para>Maps to Payflow Parameter: - <code>TRXTYPE</code></para>
        /// </summary>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		............
        /// 		//Trans is the transaction object.
        /// 		............
        /// 		Console.WriteLine("Transaction Type = " + Trans.TrxType);
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		............
        /// 		'Trans is the transaction object.
        /// 		............
        /// 		Console.WriteLine("Transaction Type = " + Trans.TrxType);
        ///  </code>
        /// </example>
        public virtual string TrxType { get; }

        /// <summary>
        ///     Value (LOW, MEDIUM or HIGH) that controls the detail level and format of transaction results.
        ///     LOW (default) returns normalized values. MEDIUM or HIGH return the processor's raw response values.
        ///     <para>Maps to Payflow Parameter: - <code>VERBOSITY</code></para>
        /// </summary>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		............
        /// 		//Trans is the transaction object.
        /// 		............
        /// 		Trans.Verbosity = "HIGH";
        /// 		Console.WriteLine("Transaction Type = " + Trans.TrxType);
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		............
        /// 		'Trans is the transaction object.
        /// 		............
        /// 		Trans.Verbosity = "HIGH"
        /// 		Console.WriteLine("Transaction Type = " + Trans.TrxType)
        ///  </code>
        /// </example>
        public virtual string Verbosity { get; set; }

        /// <summary>
        ///     Gets/sets the context object
        ///     of the current transaction.
        /// </summary>
        internal virtual Context Context { get; set; }

        /// <summary>
        ///     Gets the transaction response object.
        /// </summary>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		............
        /// 		//Trans is the transaction object.
        /// 		............
        /// 		
        /// 		//Submit the transaction.
        /// 		Trans.SubmitTransaction();
        /// 		
        /// 		// Get the Response.
        /// 		Response Resp = Trans.Response;
        /// 		if (Resp != null)
        /// 		{
        /// 			// Get the Transaction Response parameters.
        /// 			TransactionResponse TrxnResponse =  Resp.TransactionResponse;
        /// 			if (TrxnResponse != null)
        /// 			{
        /// 				Console.WriteLine("RESULT = " + TrxnResponse.Result);
        /// 				Console.WriteLine("PNREF = " + TrxnResponse.Pnref);
        /// 				Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 				Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode);
        /// 				Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr);
        /// 				Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip);
        /// 				Console.WriteLine("IAVS = " + TrxnResponse.IAVS);
        /// 			}
        /// 			// Get the Fraud Response parameters.
        /// 			FraudResponse FraudResp =  Resp.FraudResponse;
        /// 			if (FraudResp != null)
        /// 			{
        /// 				Console.WriteLine("PREFPSMSG = " + FraudResp.PreFpsMsg);
        /// 				Console.WriteLine("POSTFPSMSG = " + FraudResp.PostFpsMsg);
        /// 			}
        /// 		}
        /// 		// Get the Context and check for any contained SDK specific errors.
        /// 		Context Ctx = Resp.TransactionContext;
        /// 		if (Ctx != null ++ Ctx.getErrorCount() > 0)
        /// 		{
        /// 			Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());
        /// 		}	
        /// </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		............
        /// 		'Trans is the transaction object.
        /// 		............
        /// 		' Submit the transaction.
        /// 		Trans.SubmitTransaction()
        /// 		' Get the Response.
        /// 		Dim Resp As Response = Trans.Response
        /// 		
        /// 		If Not Resp Is Nothing Then
        /// 		' Get the Transaction Response parameters.
        /// 		
        /// 			Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 			
        /// 			If Not TrxnResponse Is Nothing Then
        /// 				Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 				Console.WriteLine("PNREF = " + TrxnResponse.Pnref)
        /// 				Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 				Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode)
        /// 				Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr)
        /// 				Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip)
        /// 				Console.WriteLine("IAVS = " + TrxnResponse.IAVS)
        /// 			End If
        /// 			' Get the Fraud Response parameters.
        /// 			Dim FraudResp As FraudResponse = Resp.FraudResponse
        /// 			If Not FraudResp Is Nothing Then
        /// 				Console.WriteLine("PREFPSMSG = " + FraudResp.PreFpsMsg)
        /// 				Console.WriteLine("POSTFPSMSG = " + FraudResp.PostFpsMsg)
        /// 			End If
        /// 		End If
        /// 		' Get the Context and check for any contained SDK specific errors.
        /// 		Dim Ctx As Context = Resp.TransactionContext
        /// 		
        /// 		If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 			Console.WriteLine(Constants.vbLf + "Errors = " + Ctx.ToString())
        /// 		End If												
        ///  </code>
        /// </example>
        public virtual Response Response => _mResponse;

        /// <summary>
        ///     Gets the extend data list.
        /// </summary>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		............
        /// 		//Trans is the transaction object.
        /// 		............
        /// 		
        /// 		ArrayList ExtDataList = Trans.ExtendData;
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		............
        /// 		'Trans is the transaction object.
        /// 		............
        /// 		Dim ExtDataList as ArrayList = Trans.ExtendData
        ///  </code>
        /// </example>
        public virtual ArrayList ExtendData => _mExtData;

        /// <summary>
        ///     Gets the transaction request.
        /// </summary>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		............
        /// 		//Trans is the transaction object.
        /// 		............
        /// 		
        /// 		Console.WriteLine("Transaction Request = " + Trans.Request);
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		............
        /// 		'Trans is the transaction object.
        /// 		............
        /// 		Console.WriteLine("Transaction Request = " + Trans.Request)
        ///  </code>
        /// </example>
        public virtual string Request => _mRequest;

        /// <summary>
        ///     Gets the Tender Object.
        /// </summary>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		............
        /// 		//Trans is the transaction object.
        /// 		............
        /// 		
        /// 		BaseTender Tender = Trans.Tender;
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		............
        /// 		'Trans is the transaction object.
        /// 		............
        /// 		Dim Tender as BaseTender = Trans.Tender
        ///  </code>
        /// </example>
        public virtual BaseTender Tender { get; }

        /// <summary>
        ///     Gets,Sets the RequestId for
        ///     the transaction.
        /// </summary>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		............
        /// 		//Trans is the transaction object.
        /// 		//A unique RequestId can be generated
        /// 		//using the 
        /// 		<see cref="PayflowUtility.RequestId">PayflowUtility.RequestId</see>
        /// 		//property. 
        /// 		............
        /// 		Trans.RequestId = PayflowUtility.RequestId;
        /// 		Console.WriteLine("Transaction RequestId = " + Trans.RequestId);
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		............
        /// 		'Trans is the transaction object.
        /// 		'A unique RequestId can be generated
        /// 		'using the 
        /// 		<see cref="PayflowUtility.RequestId">PayflowUtility.RequestId</see>
        /// 		'property. 
        /// 		............
        /// 		Trans.RequestId = PayflowUtility.RequestId
        /// 		Console.WriteLine("Transaction RequestId = " + Trans.RequestId)
        ///  </code>
        /// </example>
        public virtual string RequestId { get; set; }

        /// <summary>
        ///     Gets , sets Client Information object.
        /// </summary>
        public ClientInfo ClientInfo { get; set; }

        /// <summary>
        ///     Gets, sets BuyerAuthStatus object.
        /// </summary>
        public BuyerAuthStatus BuyerAuthStatus { get; set; }

        #endregion

        #region "Constructors"

        /// <summary>
        ///     protected Constructor. This prevents
        ///     creation of an empty Transaction object.
        /// </summary>
        protected BaseTransaction()
        {
            _mRequestBuffer = new StringBuilder();
            Context = new Context
            {
                LoadLoggerErrs = true
            };
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="trxType">Transaction type.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="requestId">Request Id</param>
        public BaseTransaction(string trxType,
            UserInfo userInfo,
            PayflowConnectionData payflowConnectionData,
            string requestId) : this()
        {
            TrxType = trxType;
            _mUserInfo = userInfo;
            _mPayflowConnectionData = payflowConnectionData;
            RequestId = requestId;
            if (_mUserInfo != null) _mUserInfo.Context = Context;
            if (_mPayflowConnectionData != null)
                if (_mPayflowConnectionData.Context != null && _mPayflowConnectionData.Context.IsErrorContained())
                    Context.AddErrors(_mPayflowConnectionData.Context.GetErrors());
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="trxType">Transaction type.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="requestId">Request Id</param>
        public BaseTransaction(string trxType,
            UserInfo userInfo,
            string requestId) : this()
        {
            TrxType = trxType;
            _mUserInfo = userInfo;
            RequestId = requestId;
            if (_mUserInfo != null) _mUserInfo.Context = Context;
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="trxType">Transaction type.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="requestId">Request Id</param>
        public BaseTransaction(string trxType,
            UserInfo userInfo,
            PayflowConnectionData payflowConnectionData,
            Invoice invoice,
            string requestId) : this(trxType, userInfo, payflowConnectionData, requestId)
        {
            _mInvoice = invoice;
            if (_mInvoice != null) _mInvoice.Context = Context;
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="trxType">Transaction type.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="requestId">Request Id</param>
        public BaseTransaction(string trxType,
            UserInfo userInfo,
            Invoice invoice,
            string requestId) : this(trxType, userInfo, null, invoice, requestId)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="trxType">Transaction type.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="tender">Tender object such as  Card Tender.</param>
        /// <param name="requestId">Request Id</param>
        public BaseTransaction(string trxType, UserInfo userInfo,
            PayflowConnectionData payflowConnectionData, Invoice invoice,
            BaseTender tender, string requestId) : this(trxType, userInfo, payflowConnectionData, invoice, requestId)
        {
            Tender = tender;
            if (Tender != null)
            {
                Tender.Context = Context;
                Tender.RequestBuffer = _mRequestBuffer;
            }
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="trxType">Transaction type.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="tender">Tender object such as  Card Tender.</param>
        /// <param name="requestId">Request Id</param>
        public BaseTransaction(string trxType, UserInfo userInfo,
            Invoice invoice,
            BaseTender tender, string requestId) : this(trxType, userInfo, null, invoice, tender, requestId)
        {
        }

        #endregion

        #region "Core functions"

        /// <summary>
        ///     This method submits the transaction
        ///     to the PayPal Payment Gateway.
        ///     The response is obtained from the gateway
        ///     and response object is populated with the
        ///     response values along with the sdk specific
        ///     errors in context, if any.
        /// </summary>
        /// <returns>Returns response object for Strong assembly transactions</returns>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		............
        /// 		//Trans is the transaction object.
        /// 		............
        /// 		
        /// 		//Submit the transaction.
        /// 		Trans.SubmitTransaction();
        /// 		
        /// 		// Get the Response.
        /// 		Response Resp = Trans.Response;
        /// 		if (Resp != null)
        /// 		{
        /// 			// Get the Transaction Response parameters.
        /// 			TransactionResponse TrxnResponse =  Resp.TransactionResponse;
        /// 			if (TrxnResponse != null)
        /// 			{
        /// 				Console.WriteLine("RESULT = " + TrxnResponse.Result);
        /// 				Console.WriteLine("PNREF = " + TrxnResponse.Pnref);
        /// 				Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 				Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode);
        /// 				Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr);
        /// 				Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip);
        /// 				Console.WriteLine("IAVS = " + TrxnResponse.IAVS);
        /// 			}
        /// 			// Get the Fraud Response parameters.
        /// 			FraudResponse FraudResp =  Resp.FraudResponse;
        /// 			if (FraudResp != null)
        /// 			{
        /// 				Console.WriteLine("PREFPSMSG = " + FraudResp.PreFpsMsg);
        /// 				Console.WriteLine("POSTFPSMSG = " + FraudResp.PostFpsMsg);
        /// 			}
        /// 		}
        /// 		// Get the Context and check for any contained SDK specific errors.
        /// 		Context Ctx = Resp.TransactionContext;
        /// 		if (Ctx != null &amp;&amp; Ctx.getErrorCount() > 0)
        /// 		{
        /// 			Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());
        /// 		}	
        /// </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		............
        /// 		'Trans is the transaction object.
        /// 		............
        /// 		' Submit the transaction.
        /// 		Trans.SubmitTransaction()
        /// 		' Get the Response.
        /// 		Dim Resp As Response = Trans.Response
        /// 		
        /// 		If Not Resp Is Nothing Then
        /// 		' Get the Transaction Response parameters.
        /// 		
        /// 			Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 			
        /// 			If Not TrxnResponse Is Nothing Then
        /// 				Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 				Console.WriteLine("PNREF = " + TrxnResponse.Pnref)
        /// 				Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 				Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode)
        /// 				Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr)
        /// 				Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip)
        /// 				Console.WriteLine("IAVS = " + TrxnResponse.IAVS)
        /// 			End If
        /// 			' Get the Fraud Response parameters.
        /// 			Dim FraudResp As FraudResponse = Resp.FraudResponse
        /// 			If Not FraudResp Is Nothing Then
        /// 				Console.WriteLine("PREFPSMSG = " + FraudResp.PreFpsMsg)
        /// 				Console.WriteLine("POSTFPSMSG = " + FraudResp.PostFpsMsg)
        /// 			End If
        /// 		End If
        /// 		' Get the Context and check for any contained SDK specific errors.
        /// 		Dim Ctx As Context = Resp.TransactionContext
        /// 		
        /// 		If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 			Console.WriteLine(Constants.vbLf + "Errors = " + Ctx.ToString())
        /// 		End If												
        ///  </code>
        /// </example>
        public virtual async Task<Response> SubmitTransactionAsync()
        {
            PayflowNetapi pfProNetApi = null;
            string responseValue = null;
            var fatal = false;
            Logger.Instance.Log("##### BEGIN TRANSACTION ##### -- " + RequestId, PayflowConstants.SeverityInfo);
            Logger.Instance.Log("PayPal.Payments.Transactions.BaseTransaction.SubmitTransaction(): Entered",
                PayflowConstants.SeverityDebug);
            try
            {
                if (ClientInfo == null) ClientInfo = new ClientInfo();
                //Check for the errors in the context now.
                var errors = PayflowUtility.AlignContext(Context, false);
                Context.LoadLoggerErrs = false;
                Context.ClearErrors();
                Context.AddErrors(errors);

                if (Context.HighestErrorLvl
                    == PayflowConstants.SeverityFatal)
                {
                    Logger.Instance.Log("PayPal.Payments.Transactions.BaseTransaction.SubmitTransaction(): Exiting",
                        PayflowConstants.SeverityDebug);
                    fatal = true;
                }

                if (!fatal)
                {
                    GenerateRequest();

                    _mRequest = RequestBuffer.ToString();


                    //Remove the trailing PayflowConstants.DELIMITER_NVP;
                    var parmListLen = _mRequest.Length;
                    if (parmListLen > 0 && _mRequest[parmListLen - 1] == '&')
                        _mRequest = _mRequest.Substring(0, parmListLen - 1);


                    //Call the api from here and submit transaction

                    if (_mPayflowConnectionData != null)
                        pfProNetApi = new PayflowNetapi(_mPayflowConnectionData.HostAddress,
                            _mPayflowConnectionData.HostPort,
                            _mPayflowConnectionData.TimeOut,
                            _mPayflowConnectionData.ProxyAddress,
                            _mPayflowConnectionData.ProxyPort,
                            _mPayflowConnectionData.ProxyLogon,
                            _mPayflowConnectionData.ProxyPassword);
                    else
                        pfProNetApi = new PayflowNetapi();

                    pfProNetApi.IsStrongAssemblyTransaction = true;
                    pfProNetApi.ClientInfo = ClientInfo;
                    responseValue = await pfProNetApi.SubmitTransactionAsync(_mRequest, RequestId);

                    Logger.Instance.Log("PayPal.Payments.Transactions.BaseTransaction.SubmitTransaction(): Exiting",
                        PayflowConstants.SeverityDebug);
                    Logger.Instance.Log("##### END TRANSACTION ##### -- " + RequestId, PayflowConstants.SeverityInfo);
                }
            }
            catch (BaseException baseEx)
            {
                var error = baseEx.GetFirstErrorInExceptionContext();
                //ErrorObject Error = PayflowUtility.PopulateCommError(PayflowConstants.E_UNKNOWN_STATE,BaseEx,PayflowConstants.SEVERITY_FATAL,false, null);
                Context.AddError(error);
            }
            catch (Exception ex)
            {
                var transEx = new TransactionException(ex);
                var error = PayflowUtility.PopulateCommError(PayflowConstants.EUnknownState, transEx,
                    PayflowConstants.SeverityFatal, false, null);
                Context.AddError(error);
            }
            finally
            {
                if (pfProNetApi != null)
                {
                    _mRequest = pfProNetApi.TransactionRequest;
                    Context.AddErrors(pfProNetApi.TransactionContext.GetErrors());
                    RequestId = pfProNetApi.RequestId;
                    ClientInfo = pfProNetApi.ClientInfo;
                }
                else
                {
                    //There is some error due to which the return
                    //is called even before pfpronetapi object is
                    //created.
                    //Check the first fatal error in context and
                    //put its response value to string.
                    if (_mRequest != null && _mRequest.Length > 0)
                        _mRequest = PayflowUtility.MaskSensitiveFields(_mRequest);
                    var errorList = Context.GetErrors(PayflowConstants.SeverityFatal);
                    var firstFatalError = (ErrorObject) errorList[0];
                    responseValue = firstFatalError.ToString();
                }

                _mResponse = new Response(RequestId, Context);
                _mResponse.SetRequestString(_mRequest);
                _mResponse.SetParams(responseValue);


                //Log the context
                if (Context.IsErrorContained()) Context.LogErrors();
                pfProNetApi = null;
            }

            return _mResponse;
        }

        /// <summary>
        ///     Generates the transaction request.
        /// </summary>
        internal virtual void GenerateRequest()
        {
            Logger.Instance.Log("PayPal.Payments.Transactions.BaseTransaction.GenerateRequest(): Entered",
                PayflowConstants.SeverityDebug);
            try
            {
                _mRequestBuffer = new StringBuilder();
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamTrxtype, TrxType));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamVerbosity, Verbosity));
                if (_mExtData != null && _mExtData.Count > 0)
                    foreach (ExtendData ed in _mExtData)
                        if (ed != null)
                        {
                            ed.RequestBuffer = _mRequestBuffer;
                            ed.GenerateRequest();
                        }

                if (Tender != null)
                {
                    Tender.RequestBuffer = _mRequestBuffer;
                    Tender.GenerateRequest();
                }

                if (_mInvoice != null)
                {
                    _mInvoice.RequestBuffer = _mRequestBuffer;
                    _mInvoice.GenerateRequest();
                }

                if (_mUserInfo != null)
                {
                    _mUserInfo.RequestBuffer = _mRequestBuffer;
                    _mUserInfo.GenerateRequest();
                }

                if (_mUserItem != null)
                {
                    _mUserItem.RequestBuffer = _mRequestBuffer;
                    _mUserItem.GenerateRequest();
                }

                if (BuyerAuthStatus != null)
                {
                    BuyerAuthStatus.RequestBuffer = _mRequestBuffer;
                    BuyerAuthStatus.GenerateRequest();
                }

                Logger.Instance.Log("PayPal.Payments.Transactions.BaseTransaction.GenerateRequest(): Exiting",
                    PayflowConstants.SeverityDebug);
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var te = new TransactionException(ex);
                throw te;
            }
        }

        #endregion

        #region "Extend Data Related Methods"

        /// <summary>
        ///     Adds an Extend Data object to
        ///     the extend data list held by transaction
        ///     object.
        /// </summary>
        /// <param name="extData">Extend Data object to be added.</param>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		............
        /// 		//Trans is the transaction object.
        /// 		............
        /// 		
        /// 		//Create an object of <see cref="DataObjects.ExtendData">ExtendData</see>
        /// 		ExtendData ExtData = new ExtendData("PFPRO_PARAM_NAME","Param Value");
        /// 		
        /// 		//Add to Transaction Extend Data list.
        /// 		Trans.AddToExtendData(ExtData);
        /// 		
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		............
        /// 		'Trans is the transaction object.
        /// 		............
        /// 		
        /// 		'Create an object of <see cref="DataObjects.ExtendData">ExtendData</see>
        /// 		Dim ExtData as ExtendData = new ExtendData("PFPRO_PARAM_NAME","Param Value")
        /// 		
        /// 		'Add to Transaction Extend Data list.
        /// 		Trans.AddToExtendData(ExtData)
        /// 		
        ///  </code>
        /// </example>
        public virtual void AddToExtendData(ExtendData extData)
        {
            if (_mExtData == null) _mExtData = new ArrayList();
            if (extData != null)
            {
                extData.Context = Context;
                extData.RequestBuffer = _mRequestBuffer;
                _mExtData.Add(extData);
            }
        }

        /// <summary>
        ///     Clears the Extend Data list held by
        ///     transaction object.
        /// </summary>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		............
        /// 		//Trans is the transaction object.
        /// 		............
        /// 		
        /// 		//Clear Transaction Extend Data list.
        /// 		Trans.ClearExtendData();
        /// 		
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		............
        /// 		'Trans is the transaction object.
        /// 		............
        /// 		
        /// 		'Clear Transaction Extend Data list.
        /// 		Trans.ClearExtendData()
        /// 		
        ///  </code>
        /// </example>
        public virtual void ClearExtendData()
        {
            if (_mExtData != null) _mExtData.Clear();
        }

        #endregion


        #region "Client Header related methods"

        /// <summary>
        ///     Adds a transaction header
        /// </summary>
        /// <param name="headerName">Header name</param>
        /// <param name="headerValue">Header value</param>
        public void AddTransHeader(string headerName, string headerValue)
        {
            AddHeader(headerName, headerValue);
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

        #endregion
    }
}