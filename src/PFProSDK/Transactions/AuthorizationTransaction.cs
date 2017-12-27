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
    ///     This class is used to create and perform an
    ///     Authorization Transaction.
    /// </summary>
    /// <remarks>A successful authorization needs to be captured using a capture transaction.</remarks>
    /// <example>
    ///     This example shows how to create and perform a authorization transaction.
    ///     <code lang="C#" escaped="false">
    /// 		..........
    /// 		..........
    /// 		//Populate required data objects.
    /// 		..........
    /// 		..........
    /// 		
    /// 		//Create a new Authorization Transaction.
    /// 		 AuthorizationTransaction Trans = new AuthorizationTransaction(
    /// 												UserInfo,
    /// 												PayflowConnectionData,
    /// 												Invoice,
    /// 												Tender, 
    /// 												RequestId);
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
    /// 		..........
    /// 		..........
    /// 		'Populate required data objects.
    /// 		..........
    /// 		..........
    /// 		
    /// 		'Create a new Authorization Transaction.
    /// 		Dim Trans as New AuthorizationTransaction(
    /// 												UserInfo,
    /// 												PayflowConnectionData,
    /// 												Invoice,
    /// 												Tender, 
    /// 												RequestId)
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
    public class AuthorizationTransaction : BaseTransaction
    {
        #region "Properties"

        /// <summary>
        ///     Gets, Sets OrigId. This property is used to perform a reference Authorization Transaction.
        /// </summary>
        /// <remarks>
        ///     A reference Authorization transaction is an authorization transaction which copies the transaction data,
        ///     except the Account Number, Expiration Date and Swipe data from a previous trasnaction.
        ///     PNRef of this previous trasnaction needs to be set in this OrigId property.
        /// </remarks>
        /// <remarks>A successful authorization needs to be captured using a capture transaction.</remarks>
        /// <example>
        ///     This example shows how to create and perform a reference authorization transaction.
        ///     <code lang="C#" escaped="false">
        /// 		..........
        /// 		..........
        /// 		//Populate required data objects.
        /// 		..........
        /// 		..........
        /// 		
        /// 		//Create a new Authorization Transaction.
        /// 		 AuthorizationTransaction Trans = new AuthorizationTransaction(
        /// 												UserInfo,
        /// 												PayflowConnectionData,
        /// 												Invoice,
        /// 												Tender, 
        /// 												RequestId);
        /// 		// Set the OrigId to refer to 
        /// 		// a previous trasncation.
        /// 		Trans.OrigId = "V64A0A07BD24";
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
        /// 		..........
        /// 		..........
        /// 		'Populate required data objects.
        /// 		..........
        /// 		..........
        /// 		
        /// 		'Create a new Authorization Transaction.
        /// 		Dim Trans as New AuthorizationTransaction(
        /// 												UserInfo,
        /// 												PayflowConnectionData,
        /// 												Invoice,
        /// 												Tender, 
        /// 												RequestId)
        /// 		' Set the OrigId to refer to 
        /// 		' a previous trasncation.
        /// 		Trans.OrigId = "V64A0A07BD24"
        /// 		
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
        public string OrigId
        {
            get => _mOrigId;
            set => _mOrigId = value;
        }

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets, Sets PartialAuth. This property is used to notify banks that a partial authorization can be performed for a
        ///     pre-paid debit/gift card.
        /// </summary>
        /// <remarks>
        ///     Partial Approval is supported for Visa, MasterCard, American Express and Discover (JCB (US Domestic only),
        ///     and Diners) Prepaid card products such as gift, Flexible Spending Account (FSA) or Healthcare Reimbursement Account
        ///     (HRA) cards. In addition Discover (JCB (US Domestic only), and Diners) supports partial Approval on their consumer
        ///     credit card. It is often difficult for the consumer to spend the exact amount available on the prepaid account, as
        ///     the purchase can be for amounts greater than the value available. This can result in unnecessary declines. Visa,
        ///     MasterCard, American Express and Discover (JCB (US Domestic only), and Diners) recognize that the prepaid products
        ///     represent unique opportunities for both merchants and consumers. With Partial Approval issuers may approve a
        ///     portion
        ///     of the amount requested. This will enable the residual transaction amount to be paid by other means. The
        ///     introduction
        ///     of the partial approval capability will reduce decline frequency and enhance the consumer and merchant experience
        ///     at
        ///     the point of sale. Merchants will now have the ability to accept partial approval rather than having the sale
        ///     declined.
        /// </remarks>
        /// <example>
        ///     This example shows how to submit the Partial Authorization flag.
        ///     <code lang="C#" escaped="false">
        /// 		..........
        /// 		..........
        /// 		//Populate required data objects.
        /// 		..........
        /// 		..........
        /// 		
        /// 		//Create a new Authorization Transaction.
        /// 		 AuthorizationTransaction Trans = new AuthorizationTransaction(
        /// 												UserInfo,
        /// 												PayflowConnectionData,
        /// 												Invoice,
        /// 												Tender, 
        /// 												RequestId);
        /// 		// Set the flag to allow partial authorizations.
        /// 		Trans.PartialAuth = "Y";
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
        /// 		..........
        /// 		..........
        /// 		'Populate required data objects.
        /// 		..........
        /// 		..........
        /// 		
        /// 		'Create a new Authorization Transaction.
        /// 		Dim Trans as New AuthorizationTransaction(
        /// 												UserInfo,
        /// 												PayflowConnectionData,
        /// 												Invoice,
        /// 												Tender, 
        /// 												RequestId)
        /// 		'Set the flag to allow partial authorizations.
        /// 		Trans.PartialAuth = "Y"
        /// 		
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
        public string PartialAuth { get; set; }

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
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamPartialauth, PartialAuth));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamCreatesecuretoken,
                    _mCreateSecureToken));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamSecuretokenid,
                    _mSecureTokenId));
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

        /// <summary>
        ///     Original transaction id.
        ///     The ORIGID is the PNREF no. from a previous transaction.
        ///     OrigId is used to create a new Authorization transaction using the details of a previous
        ///     transaction.
        /// </summary>
        private string _mOrigId;

        /// <summary>
        ///     Secure token request.
        ///     Used to store sensitive data prior to making a call to the hosted page.
        /// </summary>
        private string _mCreateSecureToken;

        /// <summary>
        ///     Secure token id.
        ///     Id used to generate a secure token.  Must be sent with the token when calling the hosted pages.
        ///     This can be any random GUID but must be unique.
        /// </summary>
        private string _mSecureTokenId;

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets, Sets CreateSecureToken. This property is used to create a SecureToken.
        /// </summary>
        /// <remarks>
        ///     Use a secure token to send non-credit card transaction data to the Payflow server for storage in
        ///     a way that can’t be intercepted and manipulated maliciously.The secure token must be used with the hosted
        ///     checkout pages. The token is good for a one-time transaction and is valid for 30 minutes.
        ///     NOTE: Without using a secure token, Payflow Pro merchants can host their own payment page and Payflow Link
        ///     merchants
        ///     can use a form post to send transaction data to the hosted checkout pages. However, by not using the secure token,
        ///     these Payflow gateway users are responsible for the secure handling of data.  To obtain a secure token, pass a
        ///     unique,
        ///     36-character token ID and set CREATESECURETOKEN=Y in a request to the Payflow server. The Payflow server associates
        ///     your
        ///     ID with a secure token and returns the token as a string of up to 32 alphanumeric characters.  To pass the
        ///     transaction
        ///     data to the hosted checkout page, you pass the secure token and token ID in an HTTP form post. The token and ID
        ///     trigger
        ///     the Payflow server to retrieve your data and display it for buyer approval.
        ///     See the DOSecureTokenAuth sample for more information.
        ///     <code lang="C#" escaped="false">
        /// 		..........
        /// 		..........
        /// 		//Populate required data objects.
        /// 		..........
        /// 		..........
        /// 		
        ///  	// Since we are using the hosted payment pages, you will not be sending the credit card data with the 
        ///      // Secure Token Request.  You just send all other 'sensitive' data within this request and when you
        ///      // call the hosted payment pages, you'll only need to pass the SECURETOKEN; which is generated and returned
        ///      // and the SECURETOKENID that was created and used in the request.
        ///      // Create a new Secure Token Authorization Transaction.  Even though this example is performing
        ///      // an authorization, you can create a secure token using SaleTransction too.  Only Authorization and Sale
        ///      // type transactions are permitted.
        /// 		//Create a new Authorization Transaction.
        /// 		 AuthorizationTransaction Trans = new AuthorizationTransaction(
        /// 												UserInfo,
        /// 												PayflowConnectionData,
        /// 												Invoice,
        /// 												Tender, 
        /// 												RequestId);
        /// 		// Set the flag to create a Secure Token.
        /// 		Trans.CreateSecureToken = "Y";
        /// 		// The Secure Token Id must be a unique id up to 36 characters.  Using the RequestID object to generate 
        /// 		// a random id, but any means to create an id can be used.
        /// 		Trans.SecureTokenId = PayflowUtility.RequestId;
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
        /// 		{
        /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
        /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 			Console.WriteLine("SECURETOKEN = " + TrxnResponse.SecureToken);
        /// 			Console.WriteLine("SECURETOKENID = " + TrxnResponse.SecureTokenId);
        /// 			// If value is true, then the Request ID has not been changed and the original response
        /// 			// of the original transction is returned. 
        /// 			Console.WriteLine("DUPLICATE = " + TrxnResponse.Duplicate);
        /// 		}
        /// 		}
        /// 		// Get the Context and check for any contained SDK specific errors.
        /// 		Context Ctx = Resp.TransactionContext;
        /// 		if (Ctx != null ++ Ctx.getErrorCount() > 0)
        /// 		{
        /// 			Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());
        /// 		}	
        /// </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		..........
        /// 		..........
        /// 		'Populate required data objects.
        /// 		..........
        /// 		..........
        /// 		
        ///      ' Since we are using the hosted payment pages, you will not be sending the credit card data with the 
        ///      ' Secure Token Request.  You just send all other 'sensitive' data within this request and when you
        ///      ' call the hosted payment pages, you'll only need to pass the SECURETOKEN; which is generated and returned
        ///      ' and the SECURETOKENID that was created and used in the request.
        ///      
        ///      ' Create a new Secure Token Authorization Transaction.  Even though this example is performing
        ///      ' an authorization, you can create a secure token using SaleTransction too.  Only Authorization and Sale
        ///      ' type transactions are permitted.
        /// 		'Create a new Authorization Transaction.
        /// 		Dim Trans as New AuthorizationTransaction(UserInfo, PayflowConnectionData, Invoice, Tender, RequestId)
        /// 		' See the CreateSecureToken parameter to yes "Y", to flag this transaction request to create a secure token.
        /// 		Trans.CreateSecureToken = "Y"
        /// 		
        ///      ' The Secure Token Id must be a unique id up to 36 characters.  Using the RequestID object to 
        ///      ' generate a random id, but any means to create an id can be used.
        ///      Trans.SecureTokenId = PayflowUtility.RequestId
        /// 		
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
        ///             Console.WriteLine("RESULT = " + TrxnResponse.Result.ToString)
        ///             Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        ///             Console.WriteLine("SECURETOKEN = " + TrxnResponse.SecureToken)
        ///             Console.WriteLine("SECURETOKENID = " + TrxnResponse.SecureTokenId)
        ///             ' If value is true, then the Request ID has not been changed and the original response
        ///             ' of the original transction is returned. 
        ///             Console.WriteLine("DUPLICATE = " + TrxnResponse.Duplicate)
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
        public string CreateSecureToken
        {
            get => _mCreateSecureToken;
            set => _mCreateSecureToken = value;
        }

        public string SecureTokenId
        {
            get => _mSecureTokenId;
            set => _mSecureTokenId = value;
        }

        #endregion

        #region "Constructors"

        /// <summary>
        ///     Private Constructor. This prevents
        ///     creation of an empty Transaction object.
        /// </summary>
        private AuthorizationTransaction()
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="tender">Tender object </param>
        /// <param name="requestId">Request Id</param>
        /// <example>
        ///     This example shows how to create and perform a authorization transaction.
        ///     <code lang="C#" escaped="false">
        /// 		..........
        /// 		..........
        /// 		//Populate required data objects.
        /// 		..........
        /// 		..........
        /// 		
        /// 		//Create a new Authorization Transaction.
        /// 		 AuthorizationTransaction Trans = new AuthorizationTransaction(
        /// 												UserInfo,
        /// 												PayflowConnectionData,
        /// 												Invoice,
        /// 												Tender, 
        /// 												RequestId);
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
        /// 		..........
        /// 		..........
        /// 		'Populate required data objects.
        /// 		..........
        /// 		..........
        /// 		
        /// 		'Create a new Authorization Transaction.
        /// 		Dim Trans as New AuthorizationTransaction(
        /// 												UserInfo,
        /// 												PayflowConnectionData,
        /// 												Invoice,
        /// 												Tender, 
        /// 												RequestId)
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
        public AuthorizationTransaction(UserInfo userInfo, PayflowConnectionData payflowConnectionData,
            Invoice invoice,
            BaseTender tender, string requestId)
            : base(PayflowConstants.TrxtypeAuth, userInfo, payflowConnectionData,
                invoice,
                //PaymentDevice ,
                tender, requestId)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="requestId">Request Id</param>
        /// <example>
        ///     This example shows how to create and perform a authorization transaction.
        ///     <code lang="C#" escaped="false">
        /// 		..........
        /// 		..........
        /// 		//Populate required data objects.
        /// 		..........
        /// 		..........
        /// 		
        /// 		//Create a new Authorization Transaction.
        /// 		 AuthorizationTransaction Trans = new AuthorizationTransaction(
        /// 												UserInfo,
        /// 												PayflowConnectionData,
        /// 												Invoice,
        /// 												RequestId);
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
        /// 		..........
        /// 		..........
        /// 		'Populate required data objects.
        /// 		..........
        /// 		..........
        /// 		
        /// 		'Create a new Authorization Transaction.
        /// 		Dim Trans as New AuthorizationTransaction(
        /// 												UserInfo,
        /// 												PayflowConnectionData,
        /// 												Invoice,
        /// 												RequestId)
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
        public AuthorizationTransaction(UserInfo userInfo, PayflowConnectionData payflowConnectionData,
            Invoice invoice, string requestId)
            : base(PayflowConstants.TrxtypeAuth, userInfo, payflowConnectionData,
                invoice, requestId)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="tender">Tender object </param>
        /// <param name="requestId">Request Id</param>
        /// <example>
        ///     This example shows how to create and perform
        ///     a authorization transaction.
        ///     <code lang="C#" escaped="false">
        /// 		..........
        /// 		..........
        /// 		//Populate required data objects.
        /// 		..........
        /// 		..........
        /// 		
        /// 		// Create a new Authorization Transaction.
        /// 		AuthorizationTransaction Trans = new AuthorizationTransaction(
        /// 												UserInfo,
        /// 												PayflowConnectionData,
        /// 												Invoice,
        /// 												Tender, 
        /// 												RequestId);
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
        /// 		..........
        /// 		..........
        /// 		'Populate required data objects.
        /// 		..........
        /// 		..........
        /// 		
        /// 		'Create a new Authorization Transaction.
        /// 		Dim Trans as New AuthorizationTransaction(
        /// 												UserInfo,
        /// 												PayflowConnectionData,
        /// 												Invoice,
        /// 												Tender, 
        /// 												RequestId)
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
        public AuthorizationTransaction(UserInfo userInfo, Invoice invoice,
            BaseTender tender, string requestId)
            : this(userInfo, null, invoice, tender, requestId)
        {
        }

        //constructor to be used in case of basic and order auth
        /// <summary>
        /// </summary>
        /// <param name="trxType"></param>
        /// <param name="userInfo"></param>
        /// <param name="payflowConnectionData"></param>
        /// <param name="invoice"></param>
        /// <param name="tender"></param>
        /// <param name="requestId"></param>
        internal AuthorizationTransaction(string trxType, UserInfo userInfo,
            PayflowConnectionData payflowConnectionData,
            Invoice invoice,
            BaseTender tender, string requestId)
            : base(trxType, userInfo, payflowConnectionData,
                invoice,
                //PaymentDevice ,
                tender, requestId)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="trxType"></param>
        /// <param name="userInfo"></param>
        /// <param name="invoice"></param>
        /// <param name="tender"></param>
        /// <param name="requestId"></param>
        internal AuthorizationTransaction(string trxType, UserInfo userInfo, Invoice invoice,
            BaseTender tender, string requestId)
            : this(trxType, userInfo, null, invoice, tender, requestId)
        {
        }

        #endregion
    }
}