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
    ///     This is the base class of all different recurring action transactions.
    /// </summary>
    /// <remarks>
    ///     Each derived class of RecurringTransaction specifies a unique action
    ///     transaction. This class can also be directly used to perform a recurring
    ///     transaction. Alternatively, a new class can be extended from this to
    ///     create a specific recurring action transaction.
    /// </remarks>
    /// <example>
    ///     <code lang="C#" escaped="false">
    /// 	...............
    /// 	// Populate data objects
    /// 	...............
    /// 	//Set the Recurring related information.
    /// 	RecurringInfo RecurInfo = new RecurringInfo();
    /// 	// The date that the first payment will be processed.
    /// 	// This will be of the format mmddyyyy.
    /// 	RecurInfo.Start = "01012009";
    /// 	RecurInfo.ProfileName = "PayPal";
    /// 	// Specifies how often the payment occurs. All PAYPERIOD values must use
    /// 	// capital letters and can be any of WEEK / BIWK / SMMO / FRWK / MONT /
    /// 	// QTER / SMYR / YEAR
    /// 	RecurInfo.PayPeriod = "WEEK";
    /// 	///////////////////////////////////////////////////////////////////
    /// 	// Create a new Recurring Transaction.
    /// 	RecurringTransaction Trans = new RecurringTransaction("A", RecurInfo,
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
    /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
    /// 		}
    /// 		// Get the Recurring Response parameters.
    /// 		RecurringResponse RecurResponse = Resp.RecurringResponse;
    /// 		if (RecurResponse != null)
    /// 		{
    /// 			Console.WriteLine("RPREF = " + RecurResponse.RPRef);
    /// 			Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId);
    /// 		}
    /// 	}
    /// 	// Get the Context and check for any contained SDK specific errors.
    /// 	Context Ctx = Resp.TransactionContext;
    /// 	if (Ctx != null &amp;&amp; Ctx.getErrorCount() > 0)
    /// 	{
    /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());
    /// 	}
    /// 
    ///  </code>
    ///     <code lang="Visual Basic" escaped="false">
    /// 	...............
    /// 	' Populate data objects
    /// 	...............
    /// 	'Set the Recurring related information.
    /// 	  Dim RecurInfo As RecurringInfo = New RecurringInfo
    /// 	  ' The date that the first payment will be processed.
    /// 	  ' This will be of the format mmddyyyy.
    /// 	  RecurInfo.Start = "01012009"
    /// 	  RecurInfo.ProfileName = "PayPal"
    /// 	  ' Specifies how often the payment occurs. All PAYPERIOD values must use
    /// 	  ' capital letters and can be any of WEEK / BIWK / SMMO / FRWK / MONT /
    /// 	  ' QTER / SMYR / YEAR
    /// 	  RecurInfo.PayPeriod = "WEEK"
    /// 	  '/////////////////////////////////////////////////////////////////
    /// 	  ' Create a new Recurring Transaction.
    /// 	  Dim Trans As RecurringTransaction = New RecurringTransaction("A", RecurInfo,
    /// 	 	User, Connection, Inv, Tender, PayflowUtility.RequestId)
    /// 	  ' Submit the transaction.
    /// 	  Dim Resp As Response = Trans.SubmitTransaction()
    /// 	  If Not Resp Is Nothing Then
    /// 	      ' Get the Transaction Response parameters.
    /// 	      Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
    /// 	      If Not TrxnResponse Is Nothing Then
    /// 	          Console.WriteLine("RESULT = " + TrxnResponse.Result)
    /// 	          Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
    /// 	      End If
    /// 	      ' Get the Recurring Response parameters.
    /// 	      Dim RecurResponse As RecurringResponse = Resp.RecurringResponse
    /// 	      If Not RecurResponse Is Nothing Then
    /// 	          Console.WriteLine("RPREF = " + RecurResponse.RPRef)
    /// 	          Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId)
    /// 	      End If
    /// 	  End If
    /// 	  ' Get the Context and check for any contained SDK specific errors.
    /// 	  Dim Ctx As Context = Resp.TransactionContext
    /// 	  If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
    /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
    /// 	  End If
    /// 
    /// 	</code>
    /// </example>
    public class RecurringTransaction : BaseTransaction
    {
        #region "Core functions"

        /// <summary>
        ///     Generates the transaction request.
        /// </summary>
        internal override void GenerateRequest()
        {
            try
            {
                base.GenerateRequest();
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamAction, _mAction));
                if (_mRecurringInfo != null)
                {
                    _mRecurringInfo.RequestBuffer = RequestBuffer;
                    _mRecurringInfo.GenerateRequest();
                }
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

        #region "Member Variables"

        private readonly string _mAction;

        /// <summary>
        ///     Holds the Recurring Info object
        /// </summary>
        private readonly RecurringInfo _mRecurringInfo;

        #endregion

        #region "Constructors"

        /// <summary>
        ///     private Constructor. This prevents
        ///     creation of an empty Transaction object.
        /// </summary>
        private RecurringTransaction()
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="action">Action, type of recurring transaction</param>
        /// <param name="recurringInfo">Recurring Info object.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     Each derived class of RecurringTransaction specifies a unique action
        ///     transaction. This class can also be directly used to perform a recurring
        ///     transaction. Alternatively, a new class can be extended from this to
        ///     create a specific recurring action transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	//Set the Recurring related information.
        /// 	RecurringInfo RecurInfo = new RecurringInfo();
        /// 	// The date that the first payment will be processed.
        /// 	// This will be of the format mmddyyyy.
        /// 	RecurInfo.Start = "01012009";
        /// 	RecurInfo.ProfileName = "PayPal";
        /// 	// Specifies how often the payment occurs. All PAYPERIOD values must use
        /// 	// capital letters and can be any of WEEK / BIWK / SMMO / FRWK / MONT /
        /// 	// QTER / SMYR / YEAR
        /// 	RecurInfo.PayPeriod = "WEEK";
        /// 	///////////////////////////////////////////////////////////////////
        /// 	// Create a new Recurring Transaction.
        /// 	RecurringTransaction Trans = new RecurringTransaction("A", RecurInfo,
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
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 		}
        /// 		// Get the Recurring Response parameters.
        /// 		RecurringResponse RecurResponse = Resp.RecurringResponse;
        /// 		if (RecurResponse != null)
        /// 		{
        /// 			Console.WriteLine("RPREF = " + RecurResponse.RPRef);
        /// 			Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId);
        /// 		}
        /// 	}
        /// 	// Get the Context and check for any contained SDK specific errors.
        /// 	Context Ctx = Resp.TransactionContext;
        /// 	if (Ctx != null &amp;&amp; Ctx.getErrorCount() > 0)
        /// 	{
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());
        /// 	}
        /// 
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	...............
        /// 	' Populate data objects
        /// 	...............
        /// 	'Set the Recurring related information.
        /// 	  Dim RecurInfo As RecurringInfo = New RecurringInfo
        /// 	  ' The date that the first payment will be processed.
        /// 	  ' This will be of the format mmddyyyy.
        /// 	  RecurInfo.Start = "01012009"
        /// 	  RecurInfo.ProfileName = "PayPal"
        /// 	  ' Specifies how often the payment occurs. All PAYPERIOD values must use
        /// 	  ' capital letters and can be any of WEEK / BIWK / SMMO / FRWK / MONT /
        /// 	  ' QTER / SMYR / YEAR
        /// 	  RecurInfo.PayPeriod = "WEEK"
        /// 	  '/////////////////////////////////////////////////////////////////
        /// 	  ' Create a new Recurring Transaction.
        /// 	  Dim Trans As RecurringTransaction = New RecurringTransaction("A", RecurInfo,
        /// 	 	User, Connection, PayflowUtility.RequestId)
        /// 	  ' Submit the transaction.
        /// 	  Dim Resp As Response = Trans.SubmitTransaction()
        /// 	  If Not Resp Is Nothing Then
        /// 	      ' Get the Transaction Response parameters.
        /// 	      Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	      If Not TrxnResponse Is Nothing Then
        /// 	          Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	          Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 	      End If
        /// 	      ' Get the Recurring Response parameters.
        /// 	      Dim RecurResponse As RecurringResponse = Resp.RecurringResponse
        /// 	      If Not RecurResponse Is Nothing Then
        /// 	          Console.WriteLine("RPREF = " + RecurResponse.RPRef)
        /// 	          Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId)
        /// 	      End If
        /// 	  End If
        /// 	  ' Get the Context and check for any contained SDK specific errors.
        /// 	  Dim Ctx As Context = Resp.TransactionContext
        /// 	  If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	  End If
        /// 
        /// 	</code>
        /// </example>
        public RecurringTransaction(
            string action,
            RecurringInfo recurringInfo,
            UserInfo userInfo,
            PayflowConnectionData payflowConnectionData, string requestId)
            : base(PayflowConstants.TrxtypeRecurring,
                userInfo, payflowConnectionData, requestId)
        {
            if (recurringInfo != null)
            {
                _mRecurringInfo = recurringInfo;
                _mRecurringInfo.Context = Context;
            }

            _mAction = action;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="action">Action, type of recurring transaction</param>
        /// <param name="recurringInfo">Recurring Info object.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     Each derived class of RecurringTransaction specifies a unique action
        ///     transaction. This class can also be directly used to perform a recurring
        ///     transaction. Alternatively, a new class can be extended from this to
        ///     create a specific recurring action transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	//Set the Recurring related information.
        /// 	RecurringInfo RecurInfo = new RecurringInfo();
        /// 	// The date that the first payment will be processed.
        /// 	// This will be of the format mmddyyyy.
        /// 	RecurInfo.Start = "01012009";
        /// 	RecurInfo.ProfileName = "PayPal";
        /// 	// Specifies how often the payment occurs. All PAYPERIOD values must use
        /// 	// capital letters and can be any of WEEK / BIWK / SMMO / FRWK / MONT /
        /// 	// QTER / SMYR / YEAR
        /// 	RecurInfo.PayPeriod = "WEEK";
        /// 	///////////////////////////////////////////////////////////////////
        /// 	// Create a new Recurring Transaction.
        /// 	RecurringTransaction Trans = new RecurringTransaction("A", RecurInfo,
        /// 		User, PayflowUtility.RequestId);
        /// 	// Submit the transaction.
        /// 	Response Resp = Trans.SubmitTransaction();
        /// 	if (Resp != null)
        /// 	{
        /// 		// Get the Transaction Response parameters.
        /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
        /// 		if (TrxnResponse != null)
        /// 		{
        /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 		}
        /// 		// Get the Recurring Response parameters.
        /// 		RecurringResponse RecurResponse = Resp.RecurringResponse;
        /// 		if (RecurResponse != null)
        /// 		{
        /// 			Console.WriteLine("RPREF = " + RecurResponse.RPRef);
        /// 			Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId);
        /// 		}
        /// 	}
        /// 	// Get the Context and check for any contained SDK specific errors.
        /// 	Context Ctx = Resp.TransactionContext;
        /// 	if (Ctx != null &amp;&amp; Ctx.getErrorCount() > 0)
        /// 	{
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());
        /// 	}
        /// 
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	...............
        /// 	' Populate data objects
        /// 	...............
        /// 	'Set the Recurring related information.
        /// 	  Dim RecurInfo As RecurringInfo = New RecurringInfo
        /// 	  ' The date that the first payment will be processed.
        /// 	  ' This will be of the format mmddyyyy.
        /// 	  RecurInfo.Start = "01012009"
        /// 	  RecurInfo.ProfileName = "PayPal"
        /// 	  ' Specifies how often the payment occurs. All PAYPERIOD values must use
        /// 	  ' capital letters and can be any of WEEK / BIWK / SMMO / FRWK / MONT /
        /// 	  ' QTER / SMYR / YEAR
        /// 	  RecurInfo.PayPeriod = "WEEK"
        /// 	  '/////////////////////////////////////////////////////////////////
        /// 	  ' Create a new Recurring Transaction.
        /// 	  Dim Trans As RecurringTransaction = New RecurringTransaction("A", RecurInfo,
        /// 	 	User, PayflowUtility.RequestId)
        /// 	  ' Submit the transaction.
        /// 	  Dim Resp As Response = Trans.SubmitTransaction()
        /// 	  If Not Resp Is Nothing Then
        /// 	      ' Get the Transaction Response parameters.
        /// 	      Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	      If Not TrxnResponse Is Nothing Then
        /// 	          Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	          Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 	      End If
        /// 	      ' Get the Recurring Response parameters.
        /// 	      Dim RecurResponse As RecurringResponse = Resp.RecurringResponse
        /// 	      If Not RecurResponse Is Nothing Then
        /// 	          Console.WriteLine("RPREF = " + RecurResponse.RPRef)
        /// 	          Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId)
        /// 	      End If
        /// 	  End If
        /// 	  ' Get the Context and check for any contained SDK specific errors.
        /// 	  Dim Ctx As Context = Resp.TransactionContext
        /// 	  If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	  End If
        /// 
        /// 	</code>
        /// </example>
        public RecurringTransaction(
            string action,
            RecurringInfo recurringInfo,
            UserInfo userInfo,
            string requestId)
            : base(PayflowConstants.TrxtypeRecurring,
                userInfo, requestId)
        {
            if (recurringInfo != null)
            {
                _mRecurringInfo = recurringInfo;
                _mRecurringInfo.Context = Context;
            }

            _mAction = action;
        }


        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="action">Action, type of recurring transaction</param>
        /// <param name="recurringInfo">Recurring Info object.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="invoice">Invoice Object</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     Each derived class of RecurringTransaction specifies a unique action
        ///     transaction. This class can also be directly used to perform a recurring
        ///     transaction. Alternatively, a new class can be extended from this to
        ///     create a specific recurring action transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	//Set the Recurring related information.
        /// 	RecurringInfo RecurInfo = new RecurringInfo();
        /// 	// The date that the first payment will be processed.
        /// 	// This will be of the format mmddyyyy.
        /// 	RecurInfo.Start = "01012009";
        /// 	RecurInfo.ProfileName = "PayPal";
        /// 	// Specifies how often the payment occurs. All PAYPERIOD values must use
        /// 	// capital letters and can be any of WEEK / BIWK / SMMO / FRWK / MONT /
        /// 	// QTER / SMYR / YEAR
        /// 	RecurInfo.PayPeriod = "WEEK";
        /// 	///////////////////////////////////////////////////////////////////
        /// 	// Create a new Recurring Transaction.
        /// 	RecurringTransaction Trans = new RecurringTransaction("A", RecurInfo,
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
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 		}
        /// 		// Get the Recurring Response parameters.
        /// 		RecurringResponse RecurResponse = Resp.RecurringResponse;
        /// 		if (RecurResponse != null)
        /// 		{
        /// 			Console.WriteLine("RPREF = " + RecurResponse.RPRef);
        /// 			Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId);
        /// 		}
        /// 	}
        /// 	// Get the Context and check for any contained SDK specific errors.
        /// 	Context Ctx = Resp.TransactionContext;
        /// 	if (Ctx != null &amp;&amp; Ctx.getErrorCount() > 0)
        /// 	{
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());
        /// 	}
        /// 
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	...............
        /// 	' Populate data objects
        /// 	...............
        /// 	'Set the Recurring related information.
        /// 	  Dim RecurInfo As RecurringInfo = New RecurringInfo
        /// 	  ' The date that the first payment will be processed.
        /// 	  ' This will be of the format mmddyyyy.
        /// 	  RecurInfo.Start = "01012009"
        /// 	  RecurInfo.ProfileName = "PayPal"
        /// 	  ' Specifies how often the payment occurs. All PAYPERIOD values must use
        /// 	  ' capital letters and can be any of WEEK / BIWK / SMMO / FRWK / MONT /
        /// 	  ' QTER / SMYR / YEAR
        /// 	  RecurInfo.PayPeriod = "WEEK"
        /// 	  '/////////////////////////////////////////////////////////////////
        /// 	  ' Create a new Recurring Transaction.
        /// 	  Dim Trans As RecurringTransaction = New RecurringTransaction("A", RecurInfo,
        /// 	 	User, Connection, Inv, PayflowUtility.RequestId)
        /// 	  ' Submit the transaction.
        /// 	  Dim Resp As Response = Trans.SubmitTransaction()
        /// 	  If Not Resp Is Nothing Then
        /// 	      ' Get the Transaction Response parameters.
        /// 	      Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	      If Not TrxnResponse Is Nothing Then
        /// 	          Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	          Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 	      End If
        /// 	      ' Get the Recurring Response parameters.
        /// 	      Dim RecurResponse As RecurringResponse = Resp.RecurringResponse
        /// 	      If Not RecurResponse Is Nothing Then
        /// 	          Console.WriteLine("RPREF = " + RecurResponse.RPRef)
        /// 	          Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId)
        /// 	      End If
        /// 	  End If
        /// 	  ' Get the Context and check for any contained SDK specific errors.
        /// 	  Dim Ctx As Context = Resp.TransactionContext
        /// 	  If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	  End If
        /// 
        /// 	</code>
        /// </example>
        public RecurringTransaction(
            string action,
            RecurringInfo recurringInfo,
            UserInfo userInfo,
            PayflowConnectionData payflowConnectionData,
            Invoice invoice, string requestId)
            : base(PayflowConstants.TrxtypeRecurring,
                userInfo, payflowConnectionData, invoice, requestId)
        {
            if (recurringInfo != null)
            {
                _mRecurringInfo = recurringInfo;
                _mRecurringInfo.Context = Context;
            }

            _mAction = action;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="action">Action, type of recurring transaction</param>
        /// <param name="recurringInfo">Recurring Info object.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="invoice">Invoice Object</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     Each derived class of RecurringTransaction specifies a unique action
        ///     transaction. This class can also be directly used to perform a recurring
        ///     transaction. Alternatively, a new class can be extended from this to
        ///     create a specific recurring action transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	//Set the Recurring related information.
        /// 	RecurringInfo RecurInfo = new RecurringInfo();
        /// 	// The date that the first payment will be processed.
        /// 	// This will be of the format mmddyyyy.
        /// 	RecurInfo.Start = "01012009";
        /// 	RecurInfo.ProfileName = "PayPal";
        /// 	// Specifies how often the payment occurs. All PAYPERIOD values must use
        /// 	// capital letters and can be any of WEEK / BIWK / SMMO / FRWK / MONT /
        /// 	// QTER / SMYR / YEAR
        /// 	RecurInfo.PayPeriod = "WEEK";
        /// 	///////////////////////////////////////////////////////////////////
        /// 	// Create a new Recurring Transaction.
        /// 	RecurringTransaction Trans = new RecurringTransaction("A", RecurInfo,
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
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 		}
        /// 		// Get the Recurring Response parameters.
        /// 		RecurringResponse RecurResponse = Resp.RecurringResponse;
        /// 		if (RecurResponse != null)
        /// 		{
        /// 			Console.WriteLine("RPREF = " + RecurResponse.RPRef);
        /// 			Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId);
        /// 		}
        /// 	}
        /// 	// Get the Context and check for any contained SDK specific errors.
        /// 	Context Ctx = Resp.TransactionContext;
        /// 	if (Ctx != null &amp;&amp; Ctx.getErrorCount() > 0)
        /// 	{
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());
        /// 	}
        /// 
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	...............
        /// 	' Populate data objects
        /// 	...............
        /// 	'Set the Recurring related information.
        /// 	  Dim RecurInfo As RecurringInfo = New RecurringInfo
        /// 	  ' The date that the first payment will be processed.
        /// 	  ' This will be of the format mmddyyyy.
        /// 	  RecurInfo.Start = "01012009"
        /// 	  RecurInfo.ProfileName = "PayPal"
        /// 	  ' Specifies how often the payment occurs. All PAYPERIOD values must use
        /// 	  ' capital letters and can be any of WEEK / BIWK / SMMO / FRWK / MONT /
        /// 	  ' QTER / SMYR / YEAR
        /// 	  RecurInfo.PayPeriod = "WEEK"
        /// 	  '/////////////////////////////////////////////////////////////////
        /// 	  ' Create a new Recurring Transaction.
        /// 	  Dim Trans As RecurringTransaction = New RecurringTransaction("A", RecurInfo,
        /// 	 	User, Inv, PayflowUtility.RequestId)
        /// 	  ' Submit the transaction.
        /// 	  Dim Resp As Response = Trans.SubmitTransaction()
        /// 	  If Not Resp Is Nothing Then
        /// 	      ' Get the Transaction Response parameters.
        /// 	      Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	      If Not TrxnResponse Is Nothing Then
        /// 	          Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	          Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 	      End If
        /// 	      ' Get the Recurring Response parameters.
        /// 	      Dim RecurResponse As RecurringResponse = Resp.RecurringResponse
        /// 	      If Not RecurResponse Is Nothing Then
        /// 	          Console.WriteLine("RPREF = " + RecurResponse.RPRef)
        /// 	          Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId)
        /// 	      End If
        /// 	  End If
        /// 	  ' Get the Context and check for any contained SDK specific errors.
        /// 	  Dim Ctx As Context = Resp.TransactionContext
        /// 	  If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	  End If
        /// 
        /// 	</code>
        /// </example>
        public RecurringTransaction(
            string action,
            RecurringInfo recurringInfo,
            UserInfo userInfo,
            Invoice invoice, string requestId)
            : this(action, recurringInfo, userInfo, null, invoice, requestId)
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="action">Action, type of recurring transaction</param>
        /// <param name="recurringInfo">Recurring Info object.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="tender">Tender</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     Each derived class of RecurringTransaction specifies a unique action
        ///     transaction. This class can also be directly used to perform a recurring
        ///     transaction. Alternatively, a new class can be extended from this to
        ///     create a specific recurring action transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	//Set the Recurring related information.
        /// 	RecurringInfo RecurInfo = new RecurringInfo();
        /// 	// The date that the first payment will be processed.
        /// 	// This will be of the format mmddyyyy.
        /// 	RecurInfo.Start = "01012009";
        /// 	RecurInfo.ProfileName = "PayPal";
        /// 	// Specifies how often the payment occurs. All PAYPERIOD values must use
        /// 	// capital letters and can be any of WEEK / BIWK / SMMO / FRWK / MONT /
        /// 	// QTER / SMYR / YEAR
        /// 	RecurInfo.PayPeriod = "WEEK";
        /// 	///////////////////////////////////////////////////////////////////
        /// 	// Create a new Recurring Transaction.
        /// 	RecurringTransaction Trans = new RecurringTransaction("A", RecurInfo,
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
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 		}
        /// 		// Get the Recurring Response parameters.
        /// 		RecurringResponse RecurResponse = Resp.RecurringResponse;
        /// 		if (RecurResponse != null)
        /// 		{
        /// 			Console.WriteLine("RPREF = " + RecurResponse.RPRef);
        /// 			Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId);
        /// 		}
        /// 	}
        /// 	// Get the Context and check for any contained SDK specific errors.
        /// 	Context Ctx = Resp.TransactionContext;
        /// 	if (Ctx != null &amp;&amp; Ctx.getErrorCount() > 0)
        /// 	{
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());
        /// 	}
        /// 
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	...............
        /// 	' Populate data objects
        /// 	...............
        /// 	'Set the Recurring related information.
        /// 	  Dim RecurInfo As RecurringInfo = New RecurringInfo
        /// 	  ' The date that the first payment will be processed.
        /// 	  ' This will be of the format mmddyyyy.
        /// 	  RecurInfo.Start = "01012009"
        /// 	  RecurInfo.ProfileName = "PayPal"
        /// 	  ' Specifies how often the payment occurs. All PAYPERIOD values must use
        /// 	  ' capital letters and can be any of WEEK / BIWK / SMMO / FRWK / MONT /
        /// 	  ' QTER / SMYR / YEAR
        /// 	  RecurInfo.PayPeriod = "WEEK"
        /// 	  '/////////////////////////////////////////////////////////////////
        /// 	  ' Create a new Recurring Transaction.
        /// 	  Dim Trans As RecurringTransaction = New RecurringTransaction("A", RecurInfo,
        /// 	 	User, Connection, Inv, Tender, PayflowUtility.RequestId)
        /// 	  ' Submit the transaction.
        /// 	  Dim Resp As Response = Trans.SubmitTransaction()
        /// 	  If Not Resp Is Nothing Then
        /// 	      ' Get the Transaction Response parameters.
        /// 	      Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	      If Not TrxnResponse Is Nothing Then
        /// 	          Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	          Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 	      End If
        /// 	      ' Get the Recurring Response parameters.
        /// 	      Dim RecurResponse As RecurringResponse = Resp.RecurringResponse
        /// 	      If Not RecurResponse Is Nothing Then
        /// 	          Console.WriteLine("RPREF = " + RecurResponse.RPRef)
        /// 	          Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId)
        /// 	      End If
        /// 	  End If
        /// 	  ' Get the Context and check for any contained SDK specific errors.
        /// 	  Dim Ctx As Context = Resp.TransactionContext
        /// 	  If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	  End If
        /// 
        /// 	</code>
        /// </example>
        public RecurringTransaction(
            string action,
            RecurringInfo recurringInfo,
            UserInfo userInfo,
            PayflowConnectionData payflowConnectionData,
            Invoice invoice,
            BaseTender tender, string requestId)
            : base(PayflowConstants.TrxtypeRecurring,
                userInfo, payflowConnectionData, invoice,
                tender, requestId)
        {
            if (recurringInfo != null)
            {
                _mRecurringInfo = recurringInfo;
                _mRecurringInfo.Context = Context;
            }

            _mAction = action;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="action">Action, type of recurring transaction</param>
        /// <param name="recurringInfo">Recurring Info object.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="tender">Tender</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     Each derived class of RecurringTransaction specifies a unique action
        ///     transaction. This class can also be directly used to perform a recurring
        ///     transaction. Alternatively, a new class can be extended from this to
        ///     create a specific recurring action transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	//Set the Recurring related information.
        /// 	RecurringInfo RecurInfo = new RecurringInfo();
        /// 	// The date that the first payment will be processed.
        /// 	// This will be of the format mmddyyyy.
        /// 	RecurInfo.Start = "01012009";
        /// 	RecurInfo.ProfileName = "PayPal";
        /// 	// Specifies how often the payment occurs. All PAYPERIOD values must use
        /// 	// capital letters and can be any of WEEK / BIWK / SMMO / FRWK / MONT /
        /// 	// QTER / SMYR / YEAR
        /// 	RecurInfo.PayPeriod = "WEEK";
        /// 	///////////////////////////////////////////////////////////////////
        /// 	// Create a new Recurring Transaction.
        /// 	RecurringTransaction Trans = new RecurringTransaction("A", RecurInfo,
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
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 		}
        /// 		// Get the Recurring Response parameters.
        /// 		RecurringResponse RecurResponse = Resp.RecurringResponse;
        /// 		if (RecurResponse != null)
        /// 		{
        /// 			Console.WriteLine("RPREF = " + RecurResponse.RPRef);
        /// 			Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId);
        /// 		}
        /// 	}
        /// 	// Get the Context and check for any contained SDK specific errors.
        /// 	Context Ctx = Resp.TransactionContext;
        /// 	if (Ctx != null &amp;&amp; Ctx.getErrorCount() > 0)
        /// 	{
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());
        /// 	}
        /// 
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	...............
        /// 	' Populate data objects
        /// 	...............
        /// 	'Set the Recurring related information.
        /// 	  Dim RecurInfo As RecurringInfo = New RecurringInfo
        /// 	  ' The date that the first payment will be processed.
        /// 	  ' This will be of the format mmddyyyy.
        /// 	  RecurInfo.Start = "01012009"
        /// 	  RecurInfo.ProfileName = "PayPal"
        /// 	  ' Specifies how often the payment occurs. All PAYPERIOD values must use
        /// 	  ' capital letters and can be any of WEEK / BIWK / SMMO / FRWK / MONT /
        /// 	  ' QTER / SMYR / YEAR
        /// 	  RecurInfo.PayPeriod = "WEEK"
        /// 	  '/////////////////////////////////////////////////////////////////
        /// 	  ' Create a new Recurring Transaction.
        /// 	  Dim Trans As RecurringTransaction = New RecurringTransaction("A", RecurInfo,
        /// 	 	User, Inv, Tender, PayflowUtility.RequestId)
        /// 	  ' Submit the transaction.
        /// 	  Dim Resp As Response = Trans.SubmitTransaction()
        /// 	  If Not Resp Is Nothing Then
        /// 	      ' Get the Transaction Response parameters.
        /// 	      Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	      If Not TrxnResponse Is Nothing Then
        /// 	          Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	          Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 	      End If
        /// 	      ' Get the Recurring Response parameters.
        /// 	      Dim RecurResponse As RecurringResponse = Resp.RecurringResponse
        /// 	      If Not RecurResponse Is Nothing Then
        /// 	          Console.WriteLine("RPREF = " + RecurResponse.RPRef)
        /// 	          Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId)
        /// 	      End If
        /// 	  End If
        /// 	  ' Get the Context and check for any contained SDK specific errors.
        /// 	  Dim Ctx As Context = Resp.TransactionContext
        /// 	  If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	  End If
        /// 
        /// 	</code>
        /// </example>
        public RecurringTransaction(
            string action,
            RecurringInfo recurringInfo,
            UserInfo userInfo,
            Invoice invoice,
            BaseTender tender, string requestId)
            : this(action, recurringInfo,
                userInfo, null, invoice,
                tender, requestId)
        {
        }

        #endregion
    }
}