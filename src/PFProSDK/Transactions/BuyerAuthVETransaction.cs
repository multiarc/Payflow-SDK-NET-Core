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
    ///     This class is used to create and perform
    ///     a Verify Enrollment transaction.
    ///     Verify Enrollment is the first step of Buyer authentication process.
    /// </summary>
    /// <remarks>
    ///     After a successful Verify Enrollment Transaction,
    ///     you should redirect the user's browser to his/her browser to the
    ///     secure authentication server which will authinticate the user.
    ///     While redirecting to this secure authentication server,
    ///     you must pass the parameter PaReq obtained in the response of this transaction.
    ///     For more information, please refer to the Payflow Developers' Guide.
    /// </remarks>
    /// <example>
    ///     This example shows how to create and perform a Verify Eknrollment transaction.
    ///     <code lang="C#" escaped="false">
    /// 		..........
    /// 		..........
    /// 		//Populate required data objects.
    /// 		
    /// 		//Create the Card object.
    /// 		CreditCard Card = new CreditCard("XXXXXXXXXXXXXXXX","XXXX");
    /// 		
    /// 		//Create the currency object.
    /// 		Currency Amt = new Currency(new decimal(1.00),"US");
    /// 		..........
    /// 		..........
    /// 		
    /// 		//Create a new Verify Enrollment Transaction.
    /// 		 BuyerAuthVETransaction Trans = new BuyerAuthVETransaction(
    /// 												UserInfo,
    /// 												PayflowConnectionData,
    /// 												Card,
    /// 												Amt, 
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
    /// 				Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
    /// 			}
    /// 			
    /// 		
    /// 			// Get the Buyer auth Response parameters.
    /// 			BuyerAuthResponse BAResponse = Resp.BuyerAuthResponse;
    /// 			if (BAResponse != null)
    /// 			{
    /// 				Console.WriteLine("AUTHENTICATION_STATUS = " + BAResponse.Authentication_Status);
    /// 				Console.WriteLine("AUTHENTICATION_ID = " + BAResponse.Authentication_Id);
    /// 				Console.WriteLine("ACSURL = " + BAResponse.AcsUrl);
    /// 				Console.WriteLine("PAREQ = " + BAResponse.PaReq);
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
    /// 		
    /// 		//Create the Card object.
    /// 		Dim Card As CreditCard = new CreditCard("XXXXXXXXXXXXXXXX","XXXX")
    /// 		
    /// 		//Create the currency object.
    /// 		Dim Amt As Currency = new Currency(new decimal(1.00),"US")
    /// 		..........
    /// 		..........
    /// 		
    /// 		'Create a new Authorization Transaction.
    /// 		Dim Trans as New BuyerAuthVETransaction(
    /// 												UserInfo,
    /// 												PayflowConnectionData,
    /// 												Card,
    /// 												Amt, 
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
    /// 				Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
    /// 			End If
    /// 			' Get the Buyer auth Response parameters.
    /// 			Dim BAResponse As BuyerAuthResponse = Resp.BuyerAuthResponse;
    /// 			If Not BAResponse Is Nothing Then
    /// 				Console.WriteLine("AUTHENTICATION_STATUS = " + BAResponse.Authentication_Status)
    /// 				Console.WriteLine("AUTHENTICATION_ID = " + BAResponse.Authentication_Id)
    /// 				Console.WriteLine("ACSURL = " + BAResponse.AcsUrl)
    /// 				Console.WriteLine("PAREQ = " + BAResponse.PaReq)
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
    public sealed class BuyerAuthVeTransaction : BuyerAuthTransaction
    {
        #region "Properties"

        /// <summary>
        ///     Gets, Sets Purchase description.
        ///     <para>Maps to Payflow Parameter: - <code>PUR_DESC</code></para>
        /// </summary>
        public string PurDesc { get; set; }

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
                if (_mCreditcard != null)
                {
                    _mCreditcard.RequestBuffer = RequestBuffer;
                    _mCreditcard.GenerateRequest();
                }

                if (_mCurrency != null)
                {
                    RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamCurrency,
                        _mCurrency.CurrencyCode));
                    RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamAmt, _mCurrency));
                }

                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamPurDesc, PurDesc));
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
        ///     Holds the currency value, mandatory for VE.
        /// </summary>
        private readonly Currency _mCurrency;

        private readonly CreditCard _mCreditcard;

        #endregion

        #region "Constructors"

        /// <summary>
        ///     Private Constructor. This prevents
        ///     creation of an empty Transaction object.
        /// </summary>
        private BuyerAuthVeTransaction()
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="creditCard">Credit card information for the user.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="currency">Currency value</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     After a successful Verify Enrollment Transaction,
        ///     you should redirect the user's browser to his/her browser to the
        ///     secure authentication server which will authinticate the user.
        ///     While redirecting to this secure authentication server,
        ///     you must pass the parameter PaReq obtained in the response of this transaction.
        /// </remarks>
        /// <example>
        ///     This example shows how to create and perform a Verify Eknrollment transaction.
        ///     <code lang="C#" escaped="false">
        /// 		..........
        /// 		..........
        /// 		//Populate required data objects.
        /// 		
        /// 		//Create the Card object.
        /// 		CreditCard Card = new CreditCard("XXXXXXXXXXXXXXXX","XXXX");
        /// 		
        /// 		//Create the currency object.
        /// 		Currency Amt = new Currency(new decimal(1.00),"US");
        /// 		..........
        /// 		..........
        /// 		
        /// 		//Create a new Verify Enrollment Transaction.
        /// 		 BuyerAuthVETransaction Trans = new BuyerAuthVETransaction(
        /// 												UserInfo,
        /// 												PayflowConnectionData,
        /// 												Card,
        /// 												Amt, 
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
        /// 				Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 			}
        /// 			
        /// 		
        /// 			// Get the Buyer auth Response parameters.
        /// 			BuyerAuthResponse BAResponse = Resp.BuyerAuthResponse;
        /// 			if (BAResponse != null)
        /// 			{
        /// 				Console.WriteLine("AUTHENTICATION_STATUS = " + BAResponse.Authentication_Status);
        /// 				Console.WriteLine("AUTHENTICATION_ID = " + BAResponse.Authentication_Id);
        /// 				Console.WriteLine("ACSURL = " + BAResponse.AcsUrl);
        /// 				Console.WriteLine("PAREQ = " + BAResponse.PaReq);
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
        /// 		
        /// 		//Create the Card object.
        /// 		Dim Card As CreditCard = new CreditCard("XXXXXXXXXXXXXXXX","XXXX")
        /// 		
        /// 		//Create the currency object.
        /// 		Dim Amt As Currency = new Currency(new decimal(1.00),"US")
        /// 		..........
        /// 		..........
        /// 		
        /// 		'Create a new Authorization Transaction.
        /// 		Dim Trans as New BuyerAuthVETransaction(
        /// 												UserInfo,
        /// 												PayflowConnectionData,
        /// 												Card,
        /// 												Amt, 
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
        /// 				Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 			End If
        /// 			' Get the Buyer auth Response parameters.
        /// 			Dim BAResponse As BuyerAuthResponse = Resp.BuyerAuthResponse;
        /// 			If Not BAResponse Is Nothing Then
        /// 				Console.WriteLine("AUTHENTICATION_STATUS = " + BAResponse.Authentication_Status)
        /// 				Console.WriteLine("AUTHENTICATION_ID = " + BAResponse.Authentication_Id)
        /// 				Console.WriteLine("ACSURL = " + BAResponse.AcsUrl)
        /// 				Console.WriteLine("PAREQ = " + BAResponse.PaReq)
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
        public BuyerAuthVeTransaction(UserInfo userInfo, CreditCard creditCard,
            PayflowConnectionData payflowConnectionData, Currency currency, string requestId)
            : base(PayflowConstants.TrxtypeBuyerauthVe, userInfo, payflowConnectionData, requestId)
        {
            _mCurrency = currency;
            PurDesc = PurDesc;
            _mCreditcard = creditCard;
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="creditCard">Credit card information for the user.</param>
        /// <param name="currency">Currency value</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     After a successful Verify Enrollment Transaction,
        ///     you should redirect the user's browser to his/her browser to the
        ///     secure authentication server which will authinticate the user.
        ///     While redirecting to this secure authentication server,
        ///     you must pass the parameter PaReq obtained in the response of this transaction.
        /// </remarks>
        /// <example>
        ///     This example shows how to create and perform a Verify Eknrollment transaction.
        ///     <code lang="C#" escaped="false">
        /// 		..........
        /// 		..........
        /// 		//Populate required data objects.
        /// 		
        /// 		//Create the Card object.
        /// 		CreditCard Card = new CreditCard("XXXXXXXXXXXXXXXX","XXXX");
        /// 		
        /// 		//Create the currency object.
        /// 		Currency Amt = new Currency(new decimal(1.00),"US");
        /// 		..........
        /// 		..........
        /// 		
        /// 		//Create a new Verify Enrollment Transaction.
        /// 		 BuyerAuthVETransaction Trans = new BuyerAuthVETransaction(
        /// 												UserInfo,
        /// 												PayflowConnectionData,
        /// 												Card,
        /// 												Amt, 
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
        /// 				Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
        /// 			}
        /// 			
        /// 		
        /// 			// Get the Buyer auth Response parameters.
        /// 			BuyerAuthResponse BAResponse = Resp.BuyerAuthResponse;
        /// 			if (BAResponse != null)
        /// 			{
        /// 				Console.WriteLine("AUTHENTICATION_STATUS = " + BAResponse.Authentication_Status);
        /// 				Console.WriteLine("AUTHENTICATION_ID = " + BAResponse.Authentication_Id);
        /// 				Console.WriteLine("ACSURL = " + BAResponse.AcsUrl);
        /// 				Console.WriteLine("PAREQ = " + BAResponse.PaReq);
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
        /// 		
        /// 		//Create the Card object.
        /// 		Dim Card As CreditCard = new CreditCard("XXXXXXXXXXXXXXXX","XXXX")
        /// 		
        /// 		//Create the currency object.
        /// 		Dim Amt As Currency = new Currency(new decimal(1.00),"US")
        /// 		..........
        /// 		..........
        /// 		
        /// 		'Create a new Verify Enrollment Transaction.
        /// 		Dim Trans as New BuyerAuthVETransaction(
        /// 												UserInfo,
        /// 												PayflowConnectionData,
        /// 												Card,
        /// 												Amt, 
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
        /// 				Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
        /// 			End If
        /// 			' Get the Buyer auth Response parameters.
        /// 			Dim BAResponse As BuyerAuthResponse = Resp.BuyerAuthResponse;
        /// 			If Not BAResponse Is Nothing Then
        /// 				Console.WriteLine("AUTHENTICATION_STATUS = " + BAResponse.Authentication_Status)
        /// 				Console.WriteLine("AUTHENTICATION_ID = " + BAResponse.Authentication_Id)
        /// 				Console.WriteLine("ACSURL = " + BAResponse.AcsUrl)
        /// 				Console.WriteLine("PAREQ = " + BAResponse.PaReq)
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
        public BuyerAuthVeTransaction(UserInfo userInfo, CreditCard creditCard, Currency currency, string requestId)
            : this(userInfo, creditCard, null, currency, requestId)
        {
        }

        #endregion
    }
}