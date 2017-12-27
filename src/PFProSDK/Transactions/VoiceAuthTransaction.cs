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
    ///     This class is used to perform a voice authorization transaction.
    /// </summary>
    /// <remarks>
    ///     Some transactions cannot be authorized over the Internet (for example, high dollar
    ///     amounts)—processing networks generate Referral (Result Code 13) transactions.
    ///     In these situations, contact the customer service department of the
    ///     merchant bank and provide the payment information as requested.
    ///     If the transaction is approved, the bank provides a voice authorization
    ///     code (AUTHCODE) for the transaction. This must be included as AUTHCODE
    ///     as part of a Voice Authorization transaction.
    /// </remarks>
    /// <example>
    ///     <code lang="C#" escaped="false">
    /// 	...............
    /// 	// Populate data objects
    /// 	...............
    /// 	// Create a new Voice Auth Transaction.                                                           
    /// 	VoiceAuthTransaction Trans = new VoiceAuthTransaction("123PNI",                    
    /// 		User, Connection, Inv, Card, PayflowUtility.RequestId);                                
    /// 	                                                                                                                    
    /// 	// Submit the transaction.                                                                                
    /// 	Response Resp = Trans.SubmitTransaction();                                                   
    /// 	                                                                                                                    
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
    ///  </code>
    ///     <code lang="Visual Basic" escaped="false">
    /// 	...............
    /// 	' Populate data objects
    /// 	...............
    /// 	 ' Create a new Voice Auth Transaction.                                                                
    /// 	 Dim Trans As VoiceAuthTransaction = New VoiceAuthTransaction("123PNI", User,    
    /// 		Connection, Inv, Card, PayflowUtility.RequestId)                                          
    /// 	                                                                                                                         
    /// 	 ' Submit the transaction.                                                                                     
    /// 	 Dim Resp As Response = Trans.SubmitTransaction()                                             
    /// 	                                                                                                                         
    /// 	 If Not Resp Is Nothing Then                                                                                 
    /// 	     ' Get the Transaction Response parameters.                                                     
    /// 	     Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse           
    /// 	     If Not TrxnResponse Is Nothing Then                                                               
    /// 	         Console.WriteLine("RESULT = " + TrxnResponse.Result)                                
    /// 	         Console.WriteLine("PNREF = " + TrxnResponse.Pnref)                                   
    /// 	         Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)                         
    /// 	         Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode)                      
    /// 	     End If                                                                                                            
    /// 	 End If                                                                                                                
    /// 	                                                                                                                         
    /// 	 ' Get the Context and check for any contained SDK specific errors.                         
    /// 	 Dim Ctx As Context = Resp.TransactionContext                                                    
    /// 	 If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then                                  
    /// 	     Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())             
    /// 	 End If                                                                                                                
    /// 	</code>
    /// </example>
    public sealed class VoiceAuthTransaction : BaseTransaction
    {
        #region "Member variables"

        /// <summary>
        ///     Holds Authcode , mandatory param for Voice Auth transaction.
        /// </summary>
        private readonly string _mAuthCode;

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
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamAuthcode, _mAuthCode));
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
        private VoiceAuthTransaction()
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="authCode">Authcode , mandatory for Voice auth transaction.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="tender">Tender object such as  Card Tender.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     Some transactions cannot be authorized over the Internet (for example, high dollar
        ///     amounts)—processing networks generate Referral (Result Code 13) transactions.
        ///     In these situations, contact the customer service department of the
        ///     merchant bank and provide the payment information as requested.
        ///     If the transaction is approved, the bank provides a voice authorization
        ///     code (AUTHCODE) for the transaction. This must be included as AUTHCODE
        ///     as part of a Voice Authorization transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	// Create a new Voice Auth Transaction.                                                           
        /// 	VoiceAuthTransaction Trans = new VoiceAuthTransaction("123PNI",                    
        /// 		User, Connection, Inv, Card, PayflowUtility.RequestId);                                
        /// 	                                                                                                                    
        /// 	// Submit the transaction.                                                                                
        /// 	Response Resp = Trans.SubmitTransaction();                                                   
        /// 	                                                                                                                    
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
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	...............
        /// 	' Populate data objects
        /// 	...............
        /// 	 ' Create a new Voice Auth Transaction.                                                                
        /// 	 Dim Trans As VoiceAuthTransaction = New VoiceAuthTransaction("123PNI", User,    
        /// 		Connection, Inv, Card, PayflowUtility.RequestId)                                          
        /// 	                                                                                                                         
        /// 	 ' Submit the transaction.                                                                                     
        /// 	 Dim Resp As Response = Trans.SubmitTransaction()                                             
        /// 	                                                                                                                         
        /// 	 If Not Resp Is Nothing Then                                                                                 
        /// 	     ' Get the Transaction Response parameters.                                                     
        /// 	     Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse           
        /// 	     If Not TrxnResponse Is Nothing Then                                                               
        /// 	         Console.WriteLine("RESULT = " + TrxnResponse.Result)                                
        /// 	         Console.WriteLine("PNREF = " + TrxnResponse.Pnref)                                   
        /// 	         Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)                         
        /// 	         Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode)                      
        /// 	     End If                                                                                                            
        /// 	 End If                                                                                                                
        /// 	                                                                                                                         
        /// 	 ' Get the Context and check for any contained SDK specific errors.                         
        /// 	 Dim Ctx As Context = Resp.TransactionContext                                                    
        /// 	 If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then                                  
        /// 	     Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())             
        /// 	 End If                                                                                                                
        /// 	</code>
        /// </example>
        public VoiceAuthTransaction(string authCode, UserInfo userInfo,
            PayflowConnectionData payflowConnectionData, Invoice invoice,
            BaseTender tender, string requestId)
            : base(PayflowConstants.TrxtypeVoiceauth, userInfo,
                payflowConnectionData, invoice,
                tender, requestId)
        {
            _mAuthCode = authCode;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="authCode">Authcode , mandatory for Voice auth transaction.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="tender">Tender object such as  Card Tender.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     Some transactions cannot be authorized over the Internet (for example, high dollar
        ///     amounts)—processing networks generate Referral (Result Code 13) transactions.
        ///     In these situations, contact the customer service department of the
        ///     merchant bank and provide the payment information as requested.
        ///     If the transaction is approved, the bank provides a voice authorization
        ///     code (AUTHCODE) for the transaction. This must be included as AUTHCODE
        ///     as part of a Voice Authorization transaction.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	// Create a new Voice Auth Transaction.                                                           
        /// 	VoiceAuthTransaction Trans = new VoiceAuthTransaction("123PNI",                    
        /// 		User, Inv, Card, PayflowUtility.RequestId);                                
        /// 	                                                                                                                    
        /// 	// Submit the transaction.                                                                                
        /// 	Response Resp = Trans.SubmitTransaction();                                                   
        /// 	                                                                                                                    
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
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	...............
        /// 	' Populate data objects
        /// 	...............
        /// 	 ' Create a new Voice Auth Transaction.                                                                
        /// 	 Dim Trans As VoiceAuthTransaction = New VoiceAuthTransaction("123PNI", User,    
        /// 		Inv, Card, PayflowUtility.RequestId)                                          
        /// 	                                                                                                                         
        /// 	 ' Submit the transaction.                                                                                     
        /// 	 Dim Resp As Response = Trans.SubmitTransaction()                                             
        /// 	                                                                                                                         
        /// 	 If Not Resp Is Nothing Then                                                                                 
        /// 	     ' Get the Transaction Response parameters.                                                     
        /// 	     Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse           
        /// 	     If Not TrxnResponse Is Nothing Then                                                               
        /// 	         Console.WriteLine("RESULT = " + TrxnResponse.Result)                                
        /// 	         Console.WriteLine("PNREF = " + TrxnResponse.Pnref)                                   
        /// 	         Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)                         
        /// 	         Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode)                      
        /// 	     End If                                                                                                            
        /// 	 End If                                                                                                                
        /// 	                                                                                                                         
        /// 	 ' Get the Context and check for any contained SDK specific errors.                         
        /// 	 Dim Ctx As Context = Resp.TransactionContext                                                    
        /// 	 If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then                                  
        /// 	     Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString())             
        /// 	 End If                                                                                                                
        /// 	</code>
        /// </example>
        public VoiceAuthTransaction(string authCode, UserInfo userInfo,
            Invoice invoice,
            BaseTender tender, string requestId)
            : this(authCode, userInfo,
                null, invoice,
                tender, requestId)
        {
        }

        #endregion
    }
}