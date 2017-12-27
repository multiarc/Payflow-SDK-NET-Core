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

using System;
using PFProSDK.Common.Exceptions;
using PFProSDK.Common.Utility;
using PFProSDK.DataObjects;

namespace PFProSDK.Transactions
{
    /// <summary>
    ///     This class is used to create and perform an
    ///     Credit Transaction.
    /// </summary>
    /// <remarks>
    ///     Reference credit transaction can be performed on successful
    ///     transactions in order to credit the amount. Therefore, a
    ///     reference credit transaction takes the PNRef of a previous transaction.
    /// </remarks>
    /// <example>
    ///     <code lang="C#" escaped="false">
    /// 	...............
    /// 	// Populate data objects
    /// 	...............
    /// 	// Create a new Credit Transaction.
    /// 	// Following is an example of a reference credit type of transaction.
    /// 	CreditTransaction Trans = new CreditTransaction("PNRef of a previous transaction.",
    /// 		User, Connection, Inv, PayflowUtility.RequestId);
    /// 	// Submit the transaction.
    /// 	Response Resp = Trans.SubmitTransaction();
    /// 	if (Resp != null)
    /// 	{
    /// 		// Get the Transaction Response parameters.
    /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
    /// 		if (TrxnResponse != null)
    /// 		{
    /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
    /// 			Console.WriteLine("PNREF = " + TrxnResponse.Pnref);
    /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
    /// 		}
    /// 	}
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
    /// 	' Create a new Credit Transaction.
    /// 	' Following is an example of a reference credit type of transaction.
    /// 	Dim Trans As CreditTransaction = New CreditTransaction("PNRef of a previous transaction.", User,
    /// 						Connection, Inv, PayflowUtility.RequestId)
    /// 		' Submit the transaction.
    /// 	Dim Resp As Response = Trans.SubmitTransaction()
    /// 	If Not Resp Is Nothing Then
    /// 	    ' Get the Transaction Response parameters.
    /// 	    Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
    /// 	    If Not TrxnResponse Is Nothing Then
    /// 	        Console.WriteLine("RESULT = " + TrxnResponse.Result)
    /// 	        Console.WriteLine("PNREF = " + TrxnResponse.Pnref)
    /// 	        Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
    /// 	    End If
    /// 	End If
    /// 	' Get the Context and check for any contained SDK specific errors.
    /// 	Dim Ctx As Context = Resp.TransactionContext
    /// 	If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
    /// 	    Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
    /// 	End If
    /// 	</code>
    /// </example>
    public sealed class CreditTransaction : BaseTransaction
    {
        #region "Member Variables"

        /// <summary>
        ///     Original transaction id.
        /// </summary>
        private readonly string _mOrigId;

        #endregion

        #region "Core functions"

        /// <summary>
        ///     Generates the transaction request.
        /// </summary>
        internal override void GenerateRequest()
        {
            try
            {
                base.GenerateRequest();
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamOrigid, _mOrigId));
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

            //catch
            //{
            //    throw new Exception();				
            //}
        }

        #endregion

        #region "Constructors"

        /// <summary>
        ///     Private Constructor. This prevents
        ///     creation of an empty Transaction object.
        /// </summary>
        private CreditTransaction()
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
        ///     Reference credit transaction can be performed on successful
        ///     transactions in order to credit the amount. Therefore, a
        ///     reference credit transaction takes the PNRef of a previous transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	// Create a new Credit Transaction.
        /// 	// Following is an example of a reference credit type of transaction.
        /// 	CreditTransaction Trans = new CreditTransaction("PNRef of a previous transaction.",
        /// 		User, Connection, Inv, PayflowUtility.RequestId);
        /// 	// Submit the transaction.
        /// 	Response Resp = Trans.SubmitTransaction();
        /// 	if (Resp != null)
        /// 	{
        /// 		// Get the Transaction Response parameters.
        /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
        /// 		if (TrxnResponse != null)
        /// 		{
        /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
        /// 			Console.WriteLine("PNREF = " + TrxnResponse.Pnref);
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 		}
        /// 	}
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
        /// 	' Create a new Credit Transaction.
        /// 	' Following is an example of a reference credit type of transaction.
        /// 	Dim Trans As CreditTransaction = New CreditTransaction("PNRef of a previous transaction.", User,
        /// 						Connection, Inv, PayflowUtility.RequestId)
        /// 		' Submit the transaction.
        /// 	Dim Resp As Response = Trans.SubmitTransaction()
        /// 	If Not Resp Is Nothing Then
        /// 	    ' Get the Transaction Response parameters.
        /// 	    Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	    If Not TrxnResponse Is Nothing Then
        /// 	        Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	        Console.WriteLine("PNREF = " + TrxnResponse.Pnref)
        /// 	        Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 	    End If
        /// 	End If
        /// 	' Get the Context and check for any contained SDK specific errors.
        /// 	Dim Ctx As Context = Resp.TransactionContext
        /// 	If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 	    Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	End If
        /// 	</code>
        /// </example>
        public CreditTransaction(string origId, UserInfo userInfo, PayflowConnectionData payflowConnectionData,
            Invoice invoice, string requestId) : base(PayflowConstants.TrxtypeCredit, userInfo, payflowConnectionData,
            invoice, requestId)
        {
            _mOrigId = origId;
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="origId">Original Transaction Id</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     Reference credit transaction can be performed on successful
        ///     transactions in order to credit the amount. Therefore, a
        ///     reference credit transaction takes the PNRef of a previous transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	// Create a new Credit Transaction.
        /// 	// Following is an example of a reference credit type of transaction.
        /// 	CreditTransaction Trans = new CreditTransaction("PNRef of a previous transaction.",
        /// 		User, Inv, PayflowUtility.RequestId);
        /// 	// Submit the transaction.
        /// 	Response Resp = Trans.SubmitTransaction();
        /// 	if (Resp != null)
        /// 	{
        /// 		// Get the Transaction Response parameters.
        /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
        /// 		if (TrxnResponse != null)
        /// 		{
        /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
        /// 			Console.WriteLine("PNREF = " + TrxnResponse.Pnref);
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 		}
        /// 	}
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
        /// 	' Create a new Credit Transaction.
        /// 	' Following is an example of a reference credit type of transaction.
        /// 	Dim Trans As CreditTransaction = New CreditTransaction("PNRef of a previous transaction.", User,
        /// 						 Inv, PayflowUtility.RequestId)
        /// 		' Submit the transaction.
        /// 	Dim Resp As Response = Trans.SubmitTransaction()
        /// 	If Not Resp Is Nothing Then
        /// 	    ' Get the Transaction Response parameters.
        /// 	    Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	    If Not TrxnResponse Is Nothing Then
        /// 	        Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	        Console.WriteLine("PNREF = " + TrxnResponse.Pnref)
        /// 	        Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 	    End If
        /// 	End If
        /// 	' Get the Context and check for any contained SDK specific errors.
        /// 	Dim Ctx As Context = Resp.TransactionContext
        /// 	If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 	    Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	End If
        /// 	</code>
        /// </example>
        public CreditTransaction(string origId, UserInfo userInfo, Invoice invoice, string requestId) : this(origId,
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
        ///     Reference credit transaction can be performed on successful
        ///     transactions in order to credit the amount. Therefore, a
        ///     reference credit transaction takes the PNRef of a previous transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	// Create a new Credit Transaction.
        /// 	// Following is an example of a reference credit type of transaction.
        /// 	CreditTransaction Trans = new CreditTransaction("PNRef of a previous transaction.",
        /// 		User, Connection, Inv, Tender, PayflowUtility.RequestId);
        /// 	// Submit the transaction.
        /// 	Response Resp = Trans.SubmitTransaction();
        /// 	if (Resp != null)
        /// 	{
        /// 		// Get the Transaction Response parameters.
        /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
        /// 		if (TrxnResponse != null)
        /// 		{
        /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
        /// 			Console.WriteLine("PNREF = " + TrxnResponse.Pnref);
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 		}
        /// 	}
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
        /// 	' Create a new Credit Transaction.
        /// 	' Following is an example of a reference credit type of transaction.
        /// 	Dim Trans As CreditTransaction = New CreditTransaction("PNRef of a previous transaction.", User,
        /// 						Connection, Inv, Tender, PayflowUtility.RequestId)
        /// 		' Submit the transaction.
        /// 	Dim Resp As Response = Trans.SubmitTransaction()
        /// 	If Not Resp Is Nothing Then
        /// 	    ' Get the Transaction Response parameters.
        /// 	    Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	    If Not TrxnResponse Is Nothing Then
        /// 	        Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	        Console.WriteLine("PNREF = " + TrxnResponse.Pnref)
        /// 	        Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 	    End If
        /// 	End If
        /// 	' Get the Context and check for any contained SDK specific errors.
        /// 	Dim Ctx As Context = Resp.TransactionContext
        /// 	If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 	    Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	End If
        /// 	</code>
        /// </example>
        public CreditTransaction(string origId, UserInfo userInfo, PayflowConnectionData payflowConnectionData,
            Invoice invoice, BaseTender tender, string requestId) : base(PayflowConstants.TrxtypeCredit, userInfo,
            payflowConnectionData, invoice, tender, requestId)
        {
            _mOrigId = origId;
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
        ///     Reference credit transaction can be performed on successful
        ///     transactions in order to credit the amount. Therefore, a
        ///     reference credit transaction takes the PNRef of a previous transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	// Create a new Credit Transaction.
        /// 	// Following is an example of a reference credit type of transaction.
        /// 	CreditTransaction Trans = new CreditTransaction("PNRef of a previous transaction.",
        /// 		User, Inv, Tender, PayflowUtility.RequestId);
        /// 	// Submit the transaction.
        /// 	Response Resp = Trans.SubmitTransaction();
        /// 	if (Resp != null)
        /// 	{
        /// 		// Get the Transaction Response parameters.
        /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
        /// 		if (TrxnResponse != null)
        /// 		{
        /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
        /// 			Console.WriteLine("PNREF = " + TrxnResponse.Pnref);
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 		}
        /// 	}
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
        /// 	' Create a new Credit Transaction.
        /// 	' Following is an example of a reference credit type of transaction.
        /// 	Dim Trans As CreditTransaction = New CreditTransaction("PNRef of a previous transaction.", User,
        /// 						 Inv, Tender, PayflowUtility.RequestId)
        /// 		' Submit the transaction.
        /// 	Dim Resp As Response = Trans.SubmitTransaction()
        /// 	If Not Resp Is Nothing Then
        /// 	    ' Get the Transaction Response parameters.
        /// 	    Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	    If Not TrxnResponse Is Nothing Then
        /// 	        Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	        Console.WriteLine("PNREF = " + TrxnResponse.Pnref)
        /// 	        Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 	    End If
        /// 	End If
        /// 	' Get the Context and check for any contained SDK specific errors.
        /// 	Dim Ctx As Context = Resp.TransactionContext
        /// 	If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 	    Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	End If
        /// 	</code>
        /// </example>
        public CreditTransaction(string origId, UserInfo userInfo, Invoice invoice, BaseTender tender, string requestId)
            : this(origId, userInfo, null, invoice, tender, requestId)
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
        ///     Reference credit transaction can be performed on successful
        ///     transactions in order to credit the amount. Therefore, a
        ///     reference credit transaction takes the PNRef of a previous transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	// Create a new Credit Transaction.
        /// 	// Following is an example of a reference credit type of transaction.
        /// 	CreditTransaction Trans = new CreditTransaction("PNRef of a previous transaction.",
        /// 		User, Connection, PayflowUtility.RequestId);
        /// 	// Submit the transaction.
        /// 	Response Resp = Trans.SubmitTransaction();
        /// 	if (Resp != null)
        /// 	{
        /// 		// Get the Transaction Response parameters.
        /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
        /// 		if (TrxnResponse != null)
        /// 		{
        /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
        /// 			Console.WriteLine("PNREF = " + TrxnResponse.Pnref);
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 		}
        /// 	}
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
        /// 	' Create a new Credit Transaction.
        /// 	' Following is an example of a reference credit type of transaction.
        /// 	Dim Trans As CreditTransaction = New CreditTransaction("PNRef of a previous transaction.", User,
        /// 						Connection, PayflowUtility.RequestId)
        /// 		' Submit the transaction.
        /// 	Dim Resp As Response = Trans.SubmitTransaction()
        /// 	If Not Resp Is Nothing Then
        /// 	    ' Get the Transaction Response parameters.
        /// 	    Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	    If Not TrxnResponse Is Nothing Then
        /// 	        Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	        Console.WriteLine("PNREF = " + TrxnResponse.Pnref)
        /// 	        Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 	    End If
        /// 	End If
        /// 	' Get the Context and check for any contained SDK specific errors.
        /// 	Dim Ctx As Context = Resp.TransactionContext
        /// 	If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 	    Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	End If
        /// 	</code>
        /// </example>
        public CreditTransaction(string origId, UserInfo userInfo, PayflowConnectionData payflowConnectionData,
            string requestId) : base(PayflowConstants.TrxtypeCredit, userInfo, payflowConnectionData, null, requestId)
        {
            _mOrigId = origId;
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="origId">Original Transaction Id</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     Reference credit transaction can be performed on successful
        ///     transactions in order to credit the amount. Therefore, a
        ///     reference credit transaction takes the PNRef of a previous transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	// Create a new Credit Transaction.
        /// 	// Following is an example of a reference credit type of transaction.
        /// 	CreditTransaction Trans = new CreditTransaction("PNRef of a previous transaction.", PayflowUtility.RequestId);
        /// 	// Submit the transaction.
        /// 	Response Resp = Trans.SubmitTransaction();
        /// 	if (Resp != null)
        /// 	{
        /// 		// Get the Transaction Response parameters.
        /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
        /// 		if (TrxnResponse != null)
        /// 		{
        /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
        /// 			Console.WriteLine("PNREF = " + TrxnResponse.Pnref);
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 		}
        /// 	}
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
        /// 	' Create a new Credit Transaction.
        /// 	' Following is an example of a reference credit type of transaction.
        /// 	Dim Trans As CreditTransaction = New CreditTransaction("PNRef of a previous transaction.", User, PayflowUtility.RequestId)
        /// 		' Submit the transaction.
        /// 	Dim Resp As Response = Trans.SubmitTransaction()
        /// 	If Not Resp Is Nothing Then
        /// 	    ' Get the Transaction Response parameters.
        /// 	    Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	    If Not TrxnResponse Is Nothing Then
        /// 	        Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	        Console.WriteLine("PNREF = " + TrxnResponse.Pnref)
        /// 	        Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 	    End If
        /// 	End If
        /// 	' Get the Context and check for any contained SDK specific errors.
        /// 	Dim Ctx As Context = Resp.TransactionContext
        /// 	If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 	    Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	End If
        /// 	</code>
        /// </example>
        public CreditTransaction(string origId, UserInfo userInfo, string requestId) : base(
            PayflowConstants.TrxtypeCredit, userInfo, requestId)
        {
            _mOrigId = origId;
        }


        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="tender">Tender object such as  Card Tender.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     This class is used for a stand alone credit transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	// Create a new Credit Transaction.
        /// 	// Following is an example of a stand alone credit type of transaction.
        /// 	CreditTransaction Trans = new CreditTransaction(User, Inv, Connection,
        /// 							Tender, PayflowUtility.RequestId);
        /// 	// Submit the transaction.
        /// 	Response Resp = Trans.SubmitTransaction();
        /// 	if (Resp != null)
        /// 	{
        /// 		// Get the Transaction Response parameters.
        /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
        /// 		if (TrxnResponse != null)
        /// 		{
        /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
        /// 			Console.WriteLine("PNREF = " + TrxnResponse.Pnref);
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 		}
        /// 	}
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
        /// 	' Create a new Credit Transaction.
        /// 	' Following is an example of a stand alone credit type of transaction.
        /// 	Dim Trans As CreditTransaction = New CreditTransaction(User, Connection,
        /// 						 Inv, Tender, PayflowUtility.RequestId)
        /// 		' Submit the transaction.
        /// 	Dim Resp As Response = Trans.SubmitTransaction()
        /// 	If Not Resp Is Nothing Then
        /// 	    ' Get the Transaction Response parameters.
        /// 	    Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	    If Not TrxnResponse Is Nothing Then
        /// 	        Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	        Console.WriteLine("PNREF = " + TrxnResponse.Pnref)
        /// 	        Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 	    End If
        /// 	End If
        /// 	' Get the Context and check for any contained SDK specific errors.
        /// 	Dim Ctx As Context = Resp.TransactionContext
        /// 	If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 	    Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	End If
        /// 	</code>
        /// </example>
        public CreditTransaction(UserInfo userInfo,
            PayflowConnectionData payflowConnectionData,
            Invoice invoice,
            BaseTender tender,
            string requestId)
            : base(PayflowConstants.TrxtypeCredit, userInfo,
                payflowConnectionData,
                invoice,
                tender,
                requestId)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="tender">Tender object such as  Card Tender.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     This class is used for a stand alone credit transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	// Create a new Credit Transaction.
        /// 	// Following is an example of a stand alone type of transaction.
        /// 	CreditTransaction Trans = new CreditTransaction(User, Inv, 
        /// 							Tender, PayflowUtility.RequestId);
        /// 	// Submit the transaction.
        /// 	Response Resp = Trans.SubmitTransaction();
        /// 	if (Resp != null)
        /// 	{
        /// 		// Get the Transaction Response parameters.
        /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
        /// 		if (TrxnResponse != null)
        /// 		{
        /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
        /// 			Console.WriteLine("PNREF = " + TrxnResponse.Pnref);
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 		}
        /// 	}
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
        /// 	' Create a new Credit Transaction.
        /// 	' Following is an example of a stand alone credit type of transaction.
        /// 	Dim Trans As CreditTransaction = New CreditTransaction(User,
        /// 						 Inv, Tender, PayflowUtility.RequestId)
        /// 		' Submit the transaction.
        /// 	Dim Resp As Response = Trans.SubmitTransaction()
        /// 	If Not Resp Is Nothing Then
        /// 	    ' Get the Transaction Response parameters.
        /// 	    Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	    If Not TrxnResponse Is Nothing Then
        /// 	        Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	        Console.WriteLine("PNREF = " + TrxnResponse.Pnref)
        /// 	        Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 	    End If
        /// 	End If
        /// 	' Get the Context and check for any contained SDK specific errors.
        /// 	Dim Ctx As Context = Resp.TransactionContext
        /// 	If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 	    Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	End If
        /// 	</code>
        /// </example>
        public CreditTransaction(UserInfo userInfo,
            Invoice invoice,
            BaseTender tender,
            string requestId)
            : this(userInfo,
                null,
                invoice,
                tender,
                requestId)
        {
        }

        #endregion
    }
}