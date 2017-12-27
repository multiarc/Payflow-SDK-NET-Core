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
using PFProSDK.Common.Exceptions;
using PFProSDK.Common.Utility;
using PFProSDK.DataObjects;

#endregion

namespace PFProSDK.Transactions
{
    /// <summary>
    ///     This class is used to perform a capture transaction.
    /// </summary>
    /// <remarks>
    ///     Capture transaction needs to be performed on a successful
    ///     authorization transaction in order to capture the amount. Therefore, a
    ///     capture transaction always takes the PNRef of a authorization transaction.
    /// </remarks>
    /// <example>
    ///     <code lang="C#" escaped="false">
    /// 	...............
    /// 	// Populate data objects
    /// 	...............
    /// 	
    /// 	// Create a new Capture Transaction.
    /// 	CaptureTransaction Trans = new CaptureTransaction("PNRef of Authorization transaction",
    /// 		User, Connection, PayflowUtility.RequestId);
    /// 	
    /// 	// Submit the transaction.
    /// 	Response Resp = Trans.SubmitTransaction();
    /// 	
    /// 	if (Resp != null)
    /// 	{
    /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
    /// 		if (TrxnResponse != null)
    /// 		{
    /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
    /// 			Console.WriteLine("PNREF = " + TrxnResponse.Pnref);
    /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
    /// 			Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode);
    /// 			Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr);
    /// 			Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip);
    /// 		}
    /// 	}
    /// 	
    /// 	// Get the Context and check for any contained SDK specific errors.
    /// 	Context Ctx = Resp.TransactionContext;
    /// 	if (Ctx != null &amp;&amp; Ctx.getErrorCount() > 0)
    /// 	{
    /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());
    /// 	}
    /// 	
    /// 	</code>
    ///     <code lang="Visual Basic" escaped="false">
    /// 	' Create a new Capture Transaction.
    /// 	Dim Trans As CaptureTransaction = New CaptureTransaction("PNRef of Authorization transaction", User, 
    /// 									Connection, PayflowUtility.RequestId)
    /// 	' Submit the transaction.
    /// 	Dim Resp As Response = Trans.SubmitTransaction()
    /// 	If Not Resp Is Nothing Then
    /// 	    Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
    /// 	    If Not TrxnResponse Is Nothing Then
    /// 	        Console.WriteLine("RESULT = " + TrxnResponse.Result)
    /// 	        Console.WriteLine("PNREF = " + TrxnResponse.Pnref)
    /// 	        Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
    /// 	        Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode)
    /// 	        Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr)
    /// 	        Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip)
    /// 	    End If
    /// 	End If
    /// 	' Get the Context and check for any contained SDK specific errors.
    /// 	Dim Ctx As Context = Resp.TransactionContext
    /// 	If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
    /// 	    Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
    /// 	End If
    /// 	
    /// 	</code>
    /// </example>
    public sealed class CaptureTransaction : ReferenceTransaction
    {
        #region "Member variables"

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets, Sets  CaptureComplete.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         UK Only: Used with Delay Capture transaction
        ///         to indicate this is the last capture you intend
        ///         to make.
        ///         Values are : Y (default) N
        ///         If CaptureComplete is Y, any remaining amount of the
        ///         original reauthorized transaction is voided.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CAPTURECOMPLETE</code>
        /// </remarks>
        public string CaptureComplete { get; set; }

        #endregion

        #region "Core functions"

        internal override void GenerateRequest()
        {
            try
            {
                base.GenerateRequest();
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamCapturecomplete,
                    CaptureComplete));
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                ex = new TransactionException(ex);
                throw ex;
            }
        }

        #endregion

        #region "Constructors"

        /// <summary>
        ///     Private Constructor. This prevents
        ///     creation of an empty Transaction object.
        /// </summary>
        private CaptureTransaction()
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="origId">Original Transaction Id</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     Capture transaction needs to be performed on a successful
        ///     authorization transaction in order to capture the amount. Therefore, a
        ///     capture transaction always takes the PNRef of a authorization transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	
        /// 	// Create a new Capture Transaction.
        /// 	CaptureTransaction Trans = new CaptureTransaction("PNRef of Authorization transaction",
        /// 		User, Connection, Inv, PayflowUtility.RequestId);
        /// 	
        /// 	// Submit the transaction.
        /// 	Response Resp = Trans.SubmitTransaction();
        /// 	
        /// 	if (Resp != null)
        /// 	{
        /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
        /// 		if (TrxnResponse != null)
        /// 		{
        /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
        /// 			Console.WriteLine("PNREF = " + TrxnResponse.Pnref);
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 			Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode);
        /// 			Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr);
        /// 			Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip);
        /// 		}
        /// 	}
        /// 	
        /// 	// Get the Context and check for any contained SDK specific errors.
        /// 	Context Ctx = Resp.TransactionContext;
        /// 	if (Ctx != null &amp;&amp; Ctx.getErrorCount() > 0)
        /// 	{
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());
        /// 	}
        /// 	
        /// 	</code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	...............
        /// 	' Populate data objects
        /// 	...............
        /// 	' Create a new Capture Transaction.
        /// 	Dim Trans As CaptureTransaction = New CaptureTransaction("PNRef of Authorization transaction",
        /// 										 User, Connection, Inv, PayflowUtility.RequestId)
        /// 	' Submit the transaction.
        /// 	Dim Resp As Response = Trans.SubmitTransaction()
        /// 	If Not Resp Is Nothing Then
        /// 	    Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	    If Not TrxnResponse Is Nothing Then
        /// 	        Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	        Console.WriteLine("PNREF = " + TrxnResponse.Pnref)
        /// 	        Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 	        Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode)
        /// 	        Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr)
        /// 	        Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip)
        /// 	    End If
        /// 	End If
        /// 	' Get the Context and check for any contained SDK specific errors.
        /// 	Dim Ctx As Context = Resp.TransactionContext
        /// 	If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 	    Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	End If
        /// 	
        /// 	</code>
        /// </example>
        public CaptureTransaction(string origId, UserInfo userInfo, PayflowConnectionData payflowConnectionData,
            Invoice invoice, string requestId) : base(PayflowConstants.TrxtypeCapture, origId, userInfo,
            payflowConnectionData, invoice, requestId)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="origId">Original Transaction Id</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     Capture transaction needs to be performed on a successful
        ///     authorization transaction in order to capture the amount. Therefore, a
        ///     capture transaction always takes the PNRef of a authorization transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	
        /// 	// Create a new Capture Transaction.
        /// 	CaptureTransaction Trans = new CaptureTransaction("PNRef of Authorization transaction",
        /// 		User, Inv, PayflowUtility.RequestId);
        /// 	
        /// 	// Submit the transaction.
        /// 	Response Resp = Trans.SubmitTransaction();
        /// 	
        /// 	if (Resp != null)
        /// 	{
        /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
        /// 		if (TrxnResponse != null)
        /// 		{
        /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
        /// 			Console.WriteLine("PNREF = " + TrxnResponse.Pnref);
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 			Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode);
        /// 			Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr);
        /// 			Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip);
        /// 		}
        /// 	}
        /// 	
        /// 	// Get the Context and check for any contained SDK specific errors.
        /// 	Context Ctx = Resp.TransactionContext;
        /// 	if (Ctx != null &amp;&amp; Ctx.getErrorCount() > 0)
        /// 	{
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());
        /// 	}
        /// 	
        /// 	</code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	...............
        /// 	' Populate data objects
        /// 	...............
        /// 	' Create a new Capture Transaction.
        /// 	Dim Trans As CaptureTransaction = New CaptureTransaction("PNRef of Authorization transaction", 
        /// 										User, Inv, PayflowUtility.RequestId)
        /// 	' Submit the transaction.
        /// 	Dim Resp As Response = Trans.SubmitTransaction()
        /// 	If Not Resp Is Nothing Then
        /// 	    Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	    If Not TrxnResponse Is Nothing Then
        /// 	        Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	        Console.WriteLine("PNREF = " + TrxnResponse.Pnref)
        /// 	        Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 	        Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode)
        /// 	        Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr)
        /// 	        Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip)
        /// 	    End If
        /// 	End If
        /// 	' Get the Context and check for any contained SDK specific errors.
        /// 	Dim Ctx As Context = Resp.TransactionContext
        /// 	If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 	    Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	End If
        /// 	
        /// 	</code>
        /// </example>
        public CaptureTransaction(string origId, UserInfo userInfo, Invoice invoice, string requestId) : this(origId,
            userInfo, null, invoice, requestId)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="origId">Original Transaction Id</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="tender">Tender object.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     Capture transaction needs to be performed on a successful
        ///     authorization transaction in order to capture the amount. Therefore, a
        ///     capture transaction always takes the PNRef of a authorization transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	
        /// 	// Create a new Capture Transaction.
        /// 	CaptureTransaction Trans = new CaptureTransaction("PNRef of Authorization transaction",
        /// 		User, Connection, Inv, Tender, PayflowUtility.RequestId);
        /// 	
        /// 	// Submit the transaction.
        /// 	Response Resp = Trans.SubmitTransaction();
        /// 	
        /// 	if (Resp != null)
        /// 	{
        /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
        /// 		if (TrxnResponse != null)
        /// 		{
        /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
        /// 			Console.WriteLine("PNREF = " + TrxnResponse.Pnref);
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 			Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode);
        /// 			Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr);
        /// 			Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip);
        /// 		}
        /// 	}
        /// 	
        /// 	// Get the Context and check for any contained SDK specific errors.
        /// 	Context Ctx = Resp.TransactionContext;
        /// 	if (Ctx != null &amp;&amp; Ctx.getErrorCount() > 0)
        /// 	{
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());
        /// 	}
        /// 	
        /// 	</code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	...............
        /// 	' Populate data objects
        /// 	...............
        /// 	' Create a new Capture Transaction.
        /// 	Dim Trans As CaptureTransaction = New CaptureTransaction("PNRef of Authorization transaction", User, Connection,
        /// 										 Inv, Tender, PayflowUtility.RequestId)
        /// 	' Submit the transaction.
        /// 	Dim Resp As Response = Trans.SubmitTransaction()
        /// 	If Not Resp Is Nothing Then
        /// 	    Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	    If Not TrxnResponse Is Nothing Then
        /// 	        Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	        Console.WriteLine("PNREF = " + TrxnResponse.Pnref)
        /// 	        Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 	        Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode)
        /// 	        Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr)
        /// 	        Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip)
        /// 	    End If
        /// 	End If
        /// 	' Get the Context and check for any contained SDK specific errors.
        /// 	Dim Ctx As Context = Resp.TransactionContext
        /// 	If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 	    Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	End If
        /// 	
        /// 	</code>
        /// </example>
        public CaptureTransaction(string origId, UserInfo userInfo, PayflowConnectionData payflowConnectionData,
            Invoice invoice, BaseTender tender, string requestId) : base(PayflowConstants.TrxtypeCapture, origId,
            userInfo, payflowConnectionData, invoice, tender, requestId)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="origId">Original Transaction Id</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="tender">Tender object.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     Capture transaction needs to be performed on a successful
        ///     authorization transaction in order to capture the amount. Therefore, a
        ///     capture transaction always takes the PNRef of a authorization transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	
        /// 	// Create a new Capture Transaction.
        /// 	CaptureTransaction Trans = new CaptureTransaction("PNRef of Authorization transaction",
        /// 		User,  Inv, Tender, PayflowUtility.RequestId);
        /// 	
        /// 	// Submit the transaction.
        /// 	Response Resp = Trans.SubmitTransaction();
        /// 	
        /// 	if (Resp != null)
        /// 	{
        /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
        /// 		if (TrxnResponse != null)
        /// 		{
        /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
        /// 			Console.WriteLine("PNREF = " + TrxnResponse.Pnref);
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 			Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode);
        /// 			Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr);
        /// 			Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip);
        /// 		}
        /// 	}
        /// 	
        /// 	// Get the Context and check for any contained SDK specific errors.
        /// 	Context Ctx = Resp.TransactionContext;
        /// 	if (Ctx != null &amp;&amp; Ctx.getErrorCount() > 0)
        /// 	{
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());
        /// 	}
        /// 	
        /// 	</code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	...............
        /// 	' Populate data objects
        /// 	...............
        /// 	' Create a new Capture Transaction.
        /// 	Dim Trans As CaptureTransaction = New CaptureTransaction("PNRef of Authorization transaction", User, 
        /// 									 Inv, Tender, PayflowUtility.RequestId)
        /// 	' Submit the transaction.
        /// 	Dim Resp As Response = Trans.SubmitTransaction()
        /// 	If Not Resp Is Nothing Then
        /// 	    Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	    If Not TrxnResponse Is Nothing Then
        /// 	        Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	        Console.WriteLine("PNREF = " + TrxnResponse.Pnref)
        /// 	        Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 	        Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode)
        /// 	        Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr)
        /// 	        Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip)
        /// 	    End If
        /// 	End If
        /// 	' Get the Context and check for any contained SDK specific errors.
        /// 	Dim Ctx As Context = Resp.TransactionContext
        /// 	If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 	    Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	End If
        /// 	
        /// 	</code>
        /// </example>
        public CaptureTransaction(string origId, UserInfo userInfo, Invoice invoice, BaseTender tender,
            string requestId) : this(origId, userInfo, null, invoice, tender, requestId)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="origId">Original Transaction Id</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     Capture transaction needs to be performed on a successful
        ///     authorization transaction in order to capture the amount. Therefore, a
        ///     capture transaction always takes the PNRef of a authorization transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	
        /// 	// Create a new Capture Transaction.
        /// 	CaptureTransaction Trans = new CaptureTransaction("PNRef of Authorization transaction",
        /// 		User, Connection, PayflowUtility.RequestId);
        /// 	
        /// 	// Submit the transaction.
        /// 	Response Resp = Trans.SubmitTransaction();
        /// 	
        /// 	if (Resp != null)
        /// 	{
        /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
        /// 		if (TrxnResponse != null)
        /// 		{
        /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
        /// 			Console.WriteLine("PNREF = " + TrxnResponse.Pnref);
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 			Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode);
        /// 			Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr);
        /// 			Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip);
        /// 		}
        /// 	}
        /// 	
        /// 	// Get the Context and check for any contained SDK specific errors.
        /// 	Context Ctx = Resp.TransactionContext;
        /// 	if (Ctx != null &amp;&amp; Ctx.getErrorCount() > 0)
        /// 	{
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());
        /// 	}
        /// 	
        /// 	</code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	...............
        /// 	' Populate data objects
        /// 	...............
        /// 	' Create a new Capture Transaction.
        /// 	Dim Trans As CaptureTransaction = New CaptureTransaction("PNRef of Authorization transaction", User, 
        /// 							Connection, PayflowUtility.RequestId)
        /// 	' Submit the transaction.
        /// 	Dim Resp As Response = Trans.SubmitTransaction()
        /// 	If Not Resp Is Nothing Then
        /// 	    Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	    If Not TrxnResponse Is Nothing Then
        /// 	        Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	        Console.WriteLine("PNREF = " + TrxnResponse.Pnref)
        /// 	        Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 	        Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode)
        /// 	        Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr)
        /// 	        Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip)
        /// 	    End If
        /// 	End If
        /// 	' Get the Context and check for any contained SDK specific errors.
        /// 	Dim Ctx As Context = Resp.TransactionContext
        /// 	If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 	    Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	End If
        /// 	
        /// 	</code>
        /// </example>
        public CaptureTransaction(string origId, UserInfo userInfo, PayflowConnectionData payflowConnectionData,
            string requestId) : base(PayflowConstants.TrxtypeCapture, origId, userInfo, payflowConnectionData,
            requestId)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="origId">Original Transaction Id</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     Capture transaction needs to be performed on a successful
        ///     authorization transaction in order to capture the amount. Therefore, a
        ///     capture transaction always takes the PNRef of a authorization transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	
        /// 	// Create a new Capture Transaction.
        /// 	CaptureTransaction Trans = new CaptureTransaction("PNRef of Authorization transaction",
        /// 		User, Connection, PayflowUtility.RequestId);
        /// 	
        /// 	// Submit the transaction.
        /// 	Response Resp = Trans.SubmitTransaction();
        /// 	
        /// 	if (Resp != null)
        /// 	{
        /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
        /// 		if (TrxnResponse != null)
        /// 		{
        /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
        /// 			Console.WriteLine("PNREF = " + TrxnResponse.Pnref);
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 			Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode);
        /// 			Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr);
        /// 			Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip);
        /// 		}
        /// 	}
        /// 	
        /// 	// Get the Context and check for any contained SDK specific errors.
        /// 	Context Ctx = Resp.TransactionContext;
        /// 	if (Ctx != null &amp;&amp; Ctx.getErrorCount() > 0)
        /// 	{
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());
        /// 	}
        /// 	
        /// 	</code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	...............
        /// 	' Populate data objects
        /// 	...............
        /// 	' Create a new Capture Transaction.
        /// 	Dim Trans As CaptureTransaction = New CaptureTransaction("PNRef of Authorization transaction", User, 
        /// 									Connection, PayflowUtility.RequestId)
        /// 	' Submit the transaction.
        /// 	Dim Resp As Response = Trans.SubmitTransaction()
        /// 	If Not Resp Is Nothing Then
        /// 	    Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	    If Not TrxnResponse Is Nothing Then
        /// 	        Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	        Console.WriteLine("PNREF = " + TrxnResponse.Pnref)
        /// 	        Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 	        Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode)
        /// 	        Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr)
        /// 	        Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip)
        /// 	    End If
        /// 	End If
        /// 	' Get the Context and check for any contained SDK specific errors.
        /// 	Dim Ctx As Context = Resp.TransactionContext
        /// 	If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 	    Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	End If
        /// 	
        /// 	</code>
        /// </example>
        public CaptureTransaction(string origId,
            UserInfo userInfo,
            string requestId) : base(PayflowConstants.TrxtypeCapture, origId, userInfo, requestId)
        {
        }

        #endregion
    }
}