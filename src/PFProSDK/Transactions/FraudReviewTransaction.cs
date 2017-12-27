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
    ///     This class is used to perform a fraud review transaction.
    /// </summary>
    /// <remarks>
    ///     Fraud Review can be used as alternative to manually
    ///     approving transactions under fraud on PayPal manager.
    /// </remarks>
    /// <example>
    ///     <code lang="C#" escaped="false">
    /// 	...............
    /// 	// Populate data objects
    /// 	...............
    /// 	// Ensure that Purchase price ceiling filter is set to $50.
    /// 	// Create a new Sale Transaction with purchase price ceiling amount filter set to $50.
    /// 	// Submit the sale transaction and get the PNRef number from this.
    /// 	FraudReviewTransaction Trans = new FraudReviewTransaction("PNRef of Fraud Sale", "RMS_APPROVE",
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
    /// 		}
    /// 	}
    /// 	// Get the Context and check for any contained SDK specific errors (optional code).
    /// 	Context Ctx = Resp.TransactionContext;
    /// 	if (Ctx != null &amp;&amp; Ctx.getErrorCount() > 0)
    /// 	{
    /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());
    /// 	}
    /// 	</code>
    ///     <code lang="Visual Basic" escaped="false">
    /// 	...............
    /// 	' Populate data objects
    /// 	...............
    /// 	' Ensure that Purchase price ceiling filter is set to $50.
    /// 	' Create a new Sale Transaction with purchase price ceiling amount filter set to $50.
    /// 	' Submit the sale transaction and get the PNRef number from this.
    /// 	Dim Trans As FraudReviewTransaction = New FraudReviewTransaction("PNRef of Fraud Sale", "RMS_APPROVE",
    /// 							User, Connection, PayflowUtility.RequestId)
    /// 	' Submit the transaction.
    /// 	Dim Resp As Response = Trans.SubmitTransaction()
    /// 	If Not Resp Is Nothing Then
    /// 	    ' Get the Transaction Response parameters.
    /// 	    Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
    /// 	    If Not TrxnResponse Is Nothing Then
    /// 	        Console.WriteLine("RESULT = " + TrxnResponse.Result)
    /// 	    End If
    /// 	End If
    /// 	' Get the Context and check for any contained SDK specific errors (optional code).
    /// 	Dim Ctx As Context = Resp.TransactionContext
    /// 	If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
    /// 	    Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
    /// 	End If
    /// 
    /// 	</code>
    /// </example>
    public sealed class FraudReviewTransaction : ReferenceTransaction
    {
        #region "Member Variables"

        /// <summary>
        ///     Holds the update action. Mandatory for this transaction.
        /// </summary>
        private readonly string _mUpdateAction;

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
                //Add UPDATEACTION
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamUpdateaction,
                    _mUpdateAction));
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
        private FraudReviewTransaction()
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="origId">Original Transaction Id</param>
        /// <param name="updateAction">Update Action RMS_APPROVE or RMS_MERCHANT_DECLINE</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     Fraud Review can be used as alternative to manually
        ///     approving transactions under fraud on PayPal manager.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	// Ensure that Purchase price ceiling filter is set to $50.
        /// 	// Create a new Sale Transaction with purchase price ceiling amount filter set to $50.
        /// 	// Submit the sale transaction and get the PNRef number from this.
        /// 	FraudReviewTransaction Trans = new FraudReviewTransaction("PNRef of Fraud Sale", "RMS_APPROVE",
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
        /// 		}
        /// 	}
        /// 	// Get the Context and check for any contained SDK specific errors (optional code).
        /// 	Context Ctx = Resp.TransactionContext;
        /// 	if (Ctx != null &amp;&amp; Ctx.getErrorCount() > 0)
        /// 	{
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());
        /// 	}
        /// 	</code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	...............
        /// 	' Populate data objects
        /// 	...............
        /// 	' Ensure that Purchase price ceiling filter is set to $50.
        /// 	' Create a new Sale Transaction with purchase price ceiling amount filter set to $50.
        /// 	' Submit the sale transaction and get the PNRef number from this.
        /// 	Dim Trans As FraudReviewTransaction = New FraudReviewTransaction("PNRef of Fraud Sale", "RMS_APPROVE",
        /// 							User, Connection, PayflowUtility.RequestId)
        /// 	' Submit the transaction.
        /// 	Dim Resp As Response = Trans.SubmitTransaction()
        /// 	If Not Resp Is Nothing Then
        /// 	    ' Get the Transaction Response parameters.
        /// 	    Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	    If Not TrxnResponse Is Nothing Then
        /// 	        Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	    End If
        /// 	End If
        /// 	' Get the Context and check for any contained SDK specific errors (optional code).
        /// 	Dim Ctx As Context = Resp.TransactionContext
        /// 	If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 	    Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	End If
        /// 
        /// 	</code>
        /// </example>
        public FraudReviewTransaction(
            string origId,
            string updateAction,
            UserInfo userInfo,
            PayflowConnectionData payflowConnectionData,
            string requestId)
            : base(PayflowConstants.TrxtypeFraudapprove, origId, userInfo, payflowConnectionData, requestId)
        {
            _mUpdateAction = updateAction;
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="origId">Original Transaction Id</param>
        /// <param name="updateAction">Update Action RMS_APPROVE or RMS_MERCHANT_DECLINE</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     Fraud Review can be used as alternative to manually
        ///     approving transactions under fraud on PayPal manager.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	// Ensure that Purchase price ceiling filter is set to $50.
        /// 	// Create a new Sale Transaction with purchase price ceiling amount filter set to $50.
        /// 	// Submit the sale transaction and get the PNRef number from this.
        /// 	FraudReviewTransaction Trans = new FraudReviewTransaction("PNRef of Fraud Sale", "RMS_APPROVE",
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
        /// 		}
        /// 	}
        /// 	// Get the Context and check for any contained SDK specific errors (optional code).
        /// 	Context Ctx = Resp.TransactionContext;
        /// 	if (Ctx != null &amp;&amp; Ctx.getErrorCount() > 0)
        /// 	{
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());
        /// 	}
        /// 	</code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	...............
        /// 	' Populate data objects
        /// 	...............
        /// 	' Ensure that Purchase price ceiling filter is set to $50.
        /// 	' Create a new Sale Transaction with purchase price ceiling amount filter set to $50.
        /// 	' Submit the sale transaction and get the PNRef number from this.
        /// 	Dim Trans As FraudReviewTransaction = New FraudReviewTransaction("PNRef of Fraud Sale", "RMS_APPROVE",
        /// 							User, PayflowUtility.RequestId)
        /// 	' Submit the transaction.
        /// 	Dim Resp As Response = Trans.SubmitTransaction()
        /// 	If Not Resp Is Nothing Then
        /// 	    ' Get the Transaction Response parameters.
        /// 	    Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
        /// 	    If Not TrxnResponse Is Nothing Then
        /// 	        Console.WriteLine("RESULT = " + TrxnResponse.Result)
        /// 	    End If
        /// 	End If
        /// 	' Get the Context and check for any contained SDK specific errors (optional code).
        /// 	Dim Ctx As Context = Resp.TransactionContext
        /// 	If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then
        /// 	    Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())
        /// 	End If
        /// 
        /// 	</code>
        /// </example>
        public FraudReviewTransaction(
            string origId,
            string updateAction,
            UserInfo userInfo,
            string requestId)
            : this(origId, updateAction, userInfo, null, requestId)
        {
        }

        #endregion
    }
}