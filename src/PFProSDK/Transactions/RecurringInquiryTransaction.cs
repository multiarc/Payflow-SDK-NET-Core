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

using PFProSDK.Common.Utility;
using PFProSDK.DataObjects;

#endregion

namespace PFProSDK.Transactions
{
    /// <summary>
    ///     This class is used to perform a recurring transaction with
    ///     inquiry action.
    /// </summary>
    /// <remarks>
    ///     RecurringInquiryTransaction is used to request two different sets of information:
    ///     To view the full set of payment information for a profile, include the
    ///     PAYMENTHISTORY=Y name/value pair with the Inquiry action.
    ///     To view the status of a customer’s profile, submit an Inquiry action that does
    ///     not include the PAYMENTHISTORY parameter (alternatively, submit
    ///     PAYMENTHISTORY=N).
    /// </remarks>
    /// <example>
    ///     <code lang="C#" escaped="false">
    /// 	...............
    /// 	// Populate data objects
    /// 	...............
    /// 	//Set the Recurring related information.
    /// 	RecurringInfo RecurInfo = new RecurringInfo();
    /// 	RecurInfo.OrigProfileId = "RT0000001350";
    /// 	///////////////////////////////////////////////////////////////////
    /// 	// Create a new Recurring Inquiry Transaction.
    /// 	RecurringInquiryTransaction Trans = new RecurringInquiryTransaction(
    /// 		User, Connection, RecurInfo, PayflowUtility.RequestId);
    /// 	// Submit the transaction.
    /// 	Response Resp = Trans.SubmitTransaction();
    /// 	if (Resp != null)
    /// 	{
    /// 		// Get the Transaction Response parameters.
    /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
    /// 		if (TrxnResponse != null)
    /// 		{
    /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
    /// 		}
    /// 		// Get the Recurring Response parameters.
    /// 		RecurringResponse RecurResponse = Resp.RecurringResponse;
    /// 		if (RecurResponse != null)
    /// 		{
    /// 			Console.WriteLine("RPREF = " + RecurResponse.RPRef);
    /// 			Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId);
    /// 			Console.WriteLine("STATUS = " + RecurResponse.Status);
    /// 			Console.WriteLine("PROFILENAME = " + RecurResponse.ProfileName);
    /// 			Console.WriteLine("START = " + RecurResponse.Start);
    /// 			Console.WriteLine("TERM = " + RecurResponse.Term);
    /// 			Console.WriteLine("NEXTPAYMENT = " + RecurResponse.NextPayment);
    /// 			Console.WriteLine("PAYPERIOD = " + RecurResponse.PayPeriod);
    /// 			Console.WriteLine("NEXTPAYMENT = " + RecurResponse.NextPayment);
    /// 			Console.WriteLine("TENDER = " + RecurResponse.Tender);
    /// 			Console.WriteLine("AMT = " + RecurResponse.Amt);
    /// 			Console.WriteLine("ACCT = " + RecurResponse.Acct);
    /// 			Console.WriteLine("EXPDATE = " + RecurResponse.ExpDate);
    /// 			Console.WriteLine("AGGREGATEAMT = " + RecurResponse.AggregateAmt);
    /// 			Console.WriteLine("AGGREGATEOPTIONALAMT = " + RecurResponse.AggregateOptionalAmt);
    /// 			Console.WriteLine("MAXFAILPAYMENTS = " + RecurResponse.MaxFailPayments);
    /// 			Console.WriteLine("NUMFAILPAYMENTS = " + RecurResponse.NumFailPayments);
    /// 			Console.WriteLine("RETRYNUMDAYS = " + RecurResponse.RetryNumDays);
    /// 			Console.WriteLine("STREET = " + RecurResponse.Street);
    /// 			Console.WriteLine("ZIP = " + RecurResponse.Zip);
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
    /// 	'Set the Recurring related information.
    /// 	 Dim RecurInfo As RecurringInfo = New RecurringInfo
    /// 	 RecurInfo.OrigProfileId = "RT0000001350"
    /// 	 '/////////////////////////////////////////////////////////////////
    /// 	 ' Create a new Recurring Inquiry Transaction.
    /// 	 Dim Trans As RecurringInquiryTransaction = New RecurringInquiryTransaction(User,
    /// 					Connection, RecurInfo, PayflowUtility.RequestId)
    /// 	 ' Submit the transaction.
    /// 	 Dim Resp As Response = Trans.SubmitTransaction()
    /// 	 If Not Resp Is Nothing Then
    /// 	     ' Get the Transaction Response parameters.
    /// 	     Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
    /// 	     If Not TrxnResponse Is Nothing Then
    /// 	         Console.WriteLine("RESULT = " + TrxnResponse.Result)
    /// 	     End If
    /// 	     ' Get the Recurring Response parameters.
    /// 	     Dim RecurResponse As RecurringResponse = Resp.RecurringResponse
    /// 	     If Not RecurResponse Is Nothing Then
    /// 	         Console.WriteLine("RPREF = " + RecurResponse.RPRef)
    /// 	         Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId)
    /// 	         Console.WriteLine("STATUS = " + RecurResponse.Status)
    /// 	         Console.WriteLine("PROFILENAME = " + RecurResponse.ProfileName)
    /// 	         Console.WriteLine("START = " + RecurResponse.Start)
    /// 	         Console.WriteLine("TERM = " + RecurResponse.Term)
    /// 	         Console.WriteLine("NEXTPAYMENT = " + RecurResponse.NextPayment)
    /// 	         Console.WriteLine("PAYPERIOD = " + RecurResponse.PayPeriod)
    /// 	         Console.WriteLine("NEXTPAYMENT = " + RecurResponse.NextPayment)
    /// 	         Console.WriteLine("TENDER = " + RecurResponse.Tender)
    /// 	         Console.WriteLine("AMT = " + RecurResponse.Amt)
    /// 	         Console.WriteLine("ACCT = " + RecurResponse.Acct)
    /// 	         Console.WriteLine("EXPDATE = " + RecurResponse.ExpDate)
    /// 	         Console.WriteLine("AGGREGATEAMT = " + RecurResponse.AggregateAmt)
    /// 	         Console.WriteLine("AGGREGATEOPTIONALAMT = " + RecurResponse.AggregateOptionalAmt)
    /// 	         Console.WriteLine("MAXFAILPAYMENTS = " + RecurResponse.MaxFailPayments)
    /// 	         Console.WriteLine("NUMFAILPAYMENTS = " + RecurResponse.NumFailPayments)
    /// 	         Console.WriteLine("RETRYNUMDAYS = " + RecurResponse.RetryNumDays)
    /// 	         Console.WriteLine("STREET = " + RecurResponse.Street)
    /// 	         Console.WriteLine("ZIP = " + RecurResponse.Zip)
    /// 	     End If
    /// 	 End If
    /// 	 ' Get the Context and check for any contained SDK specific errors.
    /// 	 Dim Ctx As Context = Resp.TransactionContext
    /// 	 If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
    /// 	     Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
    /// 	 End If
    /// 
    /// 	</code>
    /// </example>
    public class RecurringInquiryTransaction : RecurringTransaction
    {
        #region "Constructor"

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="recurringInfo">RecurringInfo object.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     RecurringInquiryTransaction is used to request two different sets of information:
        ///     To view the full set of payment information for a profile, include the
        ///     PAYMENTHISTORY=Y name/value pair with the Inquiry action.
        ///     To view the status of a customer’s profile, submit an Inquiry action that does
        ///     not include the PAYMENTHISTORY parameter (alternatively, submit
        ///     PAYMENTHISTORY=N).
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	//Set the Recurring related information.
        /// 	RecurringInfo RecurInfo = new RecurringInfo();
        /// 	RecurInfo.OrigProfileId = "RT0000001350";
        /// 	///////////////////////////////////////////////////////////////////
        /// 	// Create a new Recurring Inquiry Transaction.
        /// 	RecurringInquiryTransaction Trans = new RecurringInquiryTransaction(
        /// 		User, Connection, RecurInfo, PayflowUtility.RequestId);
        /// 	// Submit the transaction.
        /// 	Response Resp = Trans.SubmitTransaction();
        /// 	if (Resp != null)
        /// 	{
        /// 		// Get the Transaction Response parameters.
        /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
        /// 		if (TrxnResponse != null)
        /// 		{
        /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
        /// 		}
        /// 		// Get the Recurring Response parameters.
        /// 		RecurringResponse RecurResponse = Resp.RecurringResponse;
        /// 		if (RecurResponse != null)
        /// 		{
        /// 			Console.WriteLine("RPREF = " + RecurResponse.RPRef);
        /// 			Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId);
        /// 			Console.WriteLine("STATUS = " + RecurResponse.Status);
        /// 			Console.WriteLine("PROFILENAME = " + RecurResponse.ProfileName);
        /// 			Console.WriteLine("START = " + RecurResponse.Start);
        /// 			Console.WriteLine("TERM = " + RecurResponse.Term);
        /// 			Console.WriteLine("NEXTPAYMENT = " + RecurResponse.NextPayment);
        /// 			Console.WriteLine("PAYPERIOD = " + RecurResponse.PayPeriod);
        /// 			Console.WriteLine("NEXTPAYMENT = " + RecurResponse.NextPayment);
        /// 			Console.WriteLine("TENDER = " + RecurResponse.Tender);
        /// 			Console.WriteLine("AMT = " + RecurResponse.Amt);
        /// 			Console.WriteLine("ACCT = " + RecurResponse.Acct);
        /// 			Console.WriteLine("EXPDATE = " + RecurResponse.ExpDate);
        /// 			Console.WriteLine("AGGREGATEAMT = " + RecurResponse.AggregateAmt);
        /// 			Console.WriteLine("AGGREGATEOPTIONALAMT = " + RecurResponse.AggregateOptionalAmt);
        /// 			Console.WriteLine("MAXFAILPAYMENTS = " + RecurResponse.MaxFailPayments);
        /// 			Console.WriteLine("NUMFAILPAYMENTS = " + RecurResponse.NumFailPayments);
        /// 			Console.WriteLine("RETRYNUMDAYS = " + RecurResponse.RetryNumDays);
        /// 			Console.WriteLine("STREET = " + RecurResponse.Street);
        /// 			Console.WriteLine("ZIP = " + RecurResponse.Zip);
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
        /// 	'Set the Recurring related information.
        /// 	 Dim RecurInfo As RecurringInfo = New RecurringInfo
        /// 	 RecurInfo.OrigProfileId = "RT0000001350"
        /// 	 '/////////////////////////////////////////////////////////////////
        /// 	 ' Create a new Recurring Inquiry Transaction.
        /// 	 Dim Trans As RecurringInquiryTransaction = New RecurringInquiryTransaction(User,
        /// 					Connection, RecurInfo, PayflowUtility.RequestId)
        /// 	 ' Submit the transaction.
        /// 	 Dim Resp As Response = Trans.SubmitTransaction()
        /// 	 If Not Resp Is Nothing Then
        /// 	     ' Get the Transaction Response parameters.
        /// 	     Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	     If Not TrxnResponse Is Nothing Then
        /// 	         Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	     End If
        /// 	     ' Get the Recurring Response parameters.
        /// 	     Dim RecurResponse As RecurringResponse = Resp.RecurringResponse
        /// 	     If Not RecurResponse Is Nothing Then
        /// 	         Console.WriteLine("RPREF = " + RecurResponse.RPRef)
        /// 	         Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId)
        /// 	         Console.WriteLine("STATUS = " + RecurResponse.Status)
        /// 	         Console.WriteLine("PROFILENAME = " + RecurResponse.ProfileName)
        /// 	         Console.WriteLine("START = " + RecurResponse.Start)
        /// 	         Console.WriteLine("TERM = " + RecurResponse.Term)
        /// 	         Console.WriteLine("NEXTPAYMENT = " + RecurResponse.NextPayment)
        /// 	         Console.WriteLine("PAYPERIOD = " + RecurResponse.PayPeriod)
        /// 	         Console.WriteLine("NEXTPAYMENT = " + RecurResponse.NextPayment)
        /// 	         Console.WriteLine("TENDER = " + RecurResponse.Tender)
        /// 	         Console.WriteLine("AMT = " + RecurResponse.Amt)
        /// 	         Console.WriteLine("ACCT = " + RecurResponse.Acct)
        /// 	         Console.WriteLine("EXPDATE = " + RecurResponse.ExpDate)
        /// 	         Console.WriteLine("AGGREGATEAMT = " + RecurResponse.AggregateAmt)
        /// 	         Console.WriteLine("AGGREGATEOPTIONALAMT = " + RecurResponse.AggregateOptionalAmt)
        /// 	         Console.WriteLine("MAXFAILPAYMENTS = " + RecurResponse.MaxFailPayments)
        /// 	         Console.WriteLine("NUMFAILPAYMENTS = " + RecurResponse.NumFailPayments)
        /// 	         Console.WriteLine("RETRYNUMDAYS = " + RecurResponse.RetryNumDays)
        /// 	         Console.WriteLine("STREET = " + RecurResponse.Street)
        /// 	         Console.WriteLine("ZIP = " + RecurResponse.Zip)
        /// 	     End If
        /// 	 End If
        /// 	 ' Get the Context and check for any contained SDK specific errors.
        /// 	 Dim Ctx As Context = Resp.TransactionContext
        /// 	 If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 	     Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	 End If
        /// 
        /// 	</code>
        /// </example>
        public RecurringInquiryTransaction(
            UserInfo userInfo,
            PayflowConnectionData payflowConnectionData,
            RecurringInfo recurringInfo, string requestId)
            : base(PayflowConstants.RecurringActionInquiry,
                recurringInfo,
                userInfo, payflowConnectionData, requestId)
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="recurringInfo">RecurringInfo object.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     RecurringInquiryTransaction is used to request two different sets of information:
        ///     To view the full set of payment information for a profile, include the
        ///     PAYMENTHISTORY=Y name/value pair with the Inquiry action.
        ///     To view the status of a customer’s profile, submit an Inquiry action that does
        ///     not include the PAYMENTHISTORY parameter (alternatively, submit
        ///     PAYMENTHISTORY=N).
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	//Set the Recurring related information.
        /// 	RecurringInfo RecurInfo = new RecurringInfo();
        /// 	RecurInfo.OrigProfileId = "RT0000001350";
        /// 	///////////////////////////////////////////////////////////////////
        /// 	// Create a new Recurring Inquiry Transaction.
        /// 	RecurringInquiryTransaction Trans = new RecurringInquiryTransaction(
        /// 		User,  RecurInfo, PayflowUtility.RequestId);
        /// 	// Submit the transaction.
        /// 	Response Resp = Trans.SubmitTransaction();
        /// 	if (Resp != null)
        /// 	{
        /// 		// Get the Transaction Response parameters.
        /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
        /// 		if (TrxnResponse != null)
        /// 		{
        /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
        /// 		}
        /// 		// Get the Recurring Response parameters.
        /// 		RecurringResponse RecurResponse = Resp.RecurringResponse;
        /// 		if (RecurResponse != null)
        /// 		{
        /// 			Console.WriteLine("RPREF = " + RecurResponse.RPRef);
        /// 			Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId);
        /// 			Console.WriteLine("STATUS = " + RecurResponse.Status);
        /// 			Console.WriteLine("PROFILENAME = " + RecurResponse.ProfileName);
        /// 			Console.WriteLine("START = " + RecurResponse.Start);
        /// 			Console.WriteLine("TERM = " + RecurResponse.Term);
        /// 			Console.WriteLine("NEXTPAYMENT = " + RecurResponse.NextPayment);
        /// 			Console.WriteLine("PAYPERIOD = " + RecurResponse.PayPeriod);
        /// 			Console.WriteLine("NEXTPAYMENT = " + RecurResponse.NextPayment);
        /// 			Console.WriteLine("TENDER = " + RecurResponse.Tender);
        /// 			Console.WriteLine("AMT = " + RecurResponse.Amt);
        /// 			Console.WriteLine("ACCT = " + RecurResponse.Acct);
        /// 			Console.WriteLine("EXPDATE = " + RecurResponse.ExpDate);
        /// 			Console.WriteLine("AGGREGATEAMT = " + RecurResponse.AggregateAmt);
        /// 			Console.WriteLine("AGGREGATEOPTIONALAMT = " + RecurResponse.AggregateOptionalAmt);
        /// 			Console.WriteLine("MAXFAILPAYMENTS = " + RecurResponse.MaxFailPayments);
        /// 			Console.WriteLine("NUMFAILPAYMENTS = " + RecurResponse.NumFailPayments);
        /// 			Console.WriteLine("RETRYNUMDAYS = " + RecurResponse.RetryNumDays);
        /// 			Console.WriteLine("STREET = " + RecurResponse.Street);
        /// 			Console.WriteLine("ZIP = " + RecurResponse.Zip);
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
        /// 	'Set the Recurring related information.
        /// 	 Dim RecurInfo As RecurringInfo = New RecurringInfo
        /// 	 RecurInfo.OrigProfileId = "RT0000001350"
        /// 	 '/////////////////////////////////////////////////////////////////
        /// 	 ' Create a new Recurring Inquiry Transaction.
        /// 	 Dim Trans As RecurringInquiryTransaction = New RecurringInquiryTransaction(User,
        /// 					 RecurInfo, PayflowUtility.RequestId)
        /// 	 ' Submit the transaction.
        /// 	 Dim Resp As Response = Trans.SubmitTransaction()
        /// 	 If Not Resp Is Nothing Then
        /// 	     ' Get the Transaction Response parameters.
        /// 	     Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	     If Not TrxnResponse Is Nothing Then
        /// 	         Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	     End If
        /// 	     ' Get the Recurring Response parameters.
        /// 	     Dim RecurResponse As RecurringResponse = Resp.RecurringResponse
        /// 	     If Not RecurResponse Is Nothing Then
        /// 	         Console.WriteLine("RPREF = " + RecurResponse.RPRef)
        /// 	         Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId)
        /// 	         Console.WriteLine("STATUS = " + RecurResponse.Status)
        /// 	         Console.WriteLine("PROFILENAME = " + RecurResponse.ProfileName)
        /// 	         Console.WriteLine("START = " + RecurResponse.Start)
        /// 	         Console.WriteLine("TERM = " + RecurResponse.Term)
        /// 	         Console.WriteLine("NEXTPAYMENT = " + RecurResponse.NextPayment)
        /// 	         Console.WriteLine("PAYPERIOD = " + RecurResponse.PayPeriod)
        /// 	         Console.WriteLine("NEXTPAYMENT = " + RecurResponse.NextPayment)
        /// 	         Console.WriteLine("TENDER = " + RecurResponse.Tender)
        /// 	         Console.WriteLine("AMT = " + RecurResponse.Amt)
        /// 	         Console.WriteLine("ACCT = " + RecurResponse.Acct)
        /// 	         Console.WriteLine("EXPDATE = " + RecurResponse.ExpDate)
        /// 	         Console.WriteLine("AGGREGATEAMT = " + RecurResponse.AggregateAmt)
        /// 	         Console.WriteLine("AGGREGATEOPTIONALAMT = " + RecurResponse.AggregateOptionalAmt)
        /// 	         Console.WriteLine("MAXFAILPAYMENTS = " + RecurResponse.MaxFailPayments)
        /// 	         Console.WriteLine("NUMFAILPAYMENTS = " + RecurResponse.NumFailPayments)
        /// 	         Console.WriteLine("RETRYNUMDAYS = " + RecurResponse.RetryNumDays)
        /// 	         Console.WriteLine("STREET = " + RecurResponse.Street)
        /// 	         Console.WriteLine("ZIP = " + RecurResponse.Zip)
        /// 	     End If
        /// 	 End If
        /// 	 ' Get the Context and check for any contained SDK specific errors.
        /// 	 Dim Ctx As Context = Resp.TransactionContext
        /// 	 If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 	     Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	 End If
        /// 
        /// 	</code>
        /// </example>
        public RecurringInquiryTransaction(
            UserInfo userInfo,
            RecurringInfo recurringInfo, string requestId)
            : this(userInfo, null, recurringInfo, requestId)
        {
        }

        #endregion
    }
}