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
    ///     This class is used to perform a void transaction.
    /// </summary>
    /// <remarks>
    ///     The Void transaction prevents a transaction from being settled, but does
    ///     not release the authorization (hold on funds) on the cardholder’s account.
    ///     Delayed Capture, Sale, Credit, Authorization, and Voice
    ///     Authorization transactions can be voided. A Void transaction cannot be voided.
    ///     The Void must occur prior to settlement.
    /// </remarks>
    /// <example>
    ///     <code lang="C#" escaped="false">
    /// 	...............
    /// 	// Populate data objects
    /// 	...............
    /// 	// Create a new Void Transaction.                                                                     
    /// 	// The ORIGID is the PNREF no. for a previous transaction.                                 
    /// 	VoidTransaction Trans = new VoidTransaction("V63A0A07BE5A",                         
    /// 		User, Connection, PayflowUtility.RequestId);                                                 
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
    /// 		}                                                                                                            
    /// 	}                                                                                                                    
    /// 	                                                                                                                     
    /// 	// Get the Context and check for any contained SDK specific errors.                    
    /// 	Context Ctx = Resp.TransactionContext;                                                           
    /// 	if (Ctx != null  &amp;  &amp;  Ctx.getErrorCount() > 0)                                                        
    /// 	{                                                                                                                    
    /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());     
    /// 	}                                                                                                                    
    /// 
    ///  </code>
    ///     <code lang="Visual Basic" escaped="false">
    /// 	...............
    /// 	' Populate data objects
    /// 	...............
    /// 	  ' Create a new Void Transaction.                                                                    
    /// 	  ' The ORIGID is the PNREF no. for a previous transaction.                                 
    /// 		 Dim Trans As VoidTransaction = New VoidTransaction("V63A0A07BE5A",           
    /// 	 User, Connection, PayflowUtility.RequestId)                                                    
    /// 	                                                                                                                     
    /// 	  ' Submit the transaction.                                                                                
    /// 	  Dim Resp As Response = Trans.SubmitTransaction()                                        
    /// 	                                                                                                                     
    /// 	  If Not Resp Is Nothing Then                                                                            
    /// 	      ' Get the Transaction Response parameters.                                                
    /// 	      Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse      
    /// 	      If Not TrxnResponse Is Nothing Then                                                          
    /// 	          Console.WriteLine("RESULT = "  +  TrxnResponse.Result)                           
    /// 	          Console.WriteLine("PNREF = "  +  TrxnResponse.Pnref)                              
    /// 	          Console.WriteLine("RESPMSG = "  +  TrxnResponse.RespMsg)                    
    /// 	      End If                                                                                                       
    /// 	  End If                                                                                                           
    /// 	                                                                                                                     
    /// 	  ' Get the Context and check for any contained SDK specific errors.                    
    /// 	  Dim Ctx As Context = Resp.TransactionContext                                               
    /// 	  If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then                             
    /// 	      Console.WriteLine(Environment.NewLine  +  "Errors = "  +  Ctx.ToString())        
    /// 	  End If                                                                                                           
    /// 	</code>
    /// </example>
    public class VoidTransaction : ReferenceTransaction
    {
        #region "Constructors"

        /// <summary>
        ///     Private Constructor. This prevents
        ///     creation of an empty Transaction object.
        /// </summary>
        private VoidTransaction()
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="origId">Original Transaction Id.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     The Void transaction prevents a transaction from being settled, but does
        ///     not release the authorization (hold on funds) on the cardholder’s account.
        ///     Delayed Capture, Sale, Credit, Authorization, and Voice
        ///     Authorization transactions can be voided. A Void transaction cannot be voided.
        ///     The Void must occur prior to settlement.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	// Create a new Void Transaction.                                                                     
        /// 	// The ORIGID is the PNREF no. for a previous transaction.                                 
        /// 	VoidTransaction Trans = new VoidTransaction("V63A0A07BE5A",                         
        /// 		User, Connection, PayflowUtility.RequestId);                                                 
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
        /// 		}                                                                                                            
        /// 	}                                                                                                                    
        /// 	                                                                                                                     
        /// 	// Get the Context and check for any contained SDK specific errors.                    
        /// 	Context Ctx = Resp.TransactionContext;                                                           
        /// 	if (Ctx != null  &amp;  &amp;  Ctx.getErrorCount() > 0)                                                        
        /// 	{                                                                                                                    
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());     
        /// 	}                                                                                                                    
        /// 
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	...............
        /// 	' Populate data objects
        /// 	...............
        /// 	  ' Create a new Void Transaction.                                                                    
        /// 	  ' The ORIGID is the PNREF no. for a previous transaction.                                 
        /// 		 Dim Trans As VoidTransaction = New VoidTransaction("V63A0A07BE5A",           
        /// 	 User, Connection, PayflowUtility.RequestId)                                                    
        /// 	                                                                                                                     
        /// 	  ' Submit the transaction.                                                                                
        /// 	  Dim Resp As Response = Trans.SubmitTransaction()                                        
        /// 	                                                                                                                     
        /// 	  If Not Resp Is Nothing Then                                                                            
        /// 	      ' Get the Transaction Response parameters.                                                
        /// 	      Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse      
        /// 	      If Not TrxnResponse Is Nothing Then                                                          
        /// 	          Console.WriteLine("RESULT = "  +  TrxnResponse.Result)                           
        /// 	          Console.WriteLine("PNREF = "  +  TrxnResponse.Pnref)                              
        /// 	          Console.WriteLine("RESPMSG = "  +  TrxnResponse.RespMsg)                    
        /// 	      End If                                                                                                       
        /// 	  End If                                                                                                           
        /// 	                                                                                                                     
        /// 	  ' Get the Context and check for any contained SDK specific errors.                    
        /// 	  Dim Ctx As Context = Resp.TransactionContext                                               
        /// 	  If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then                             
        /// 	      Console.WriteLine(Environment.NewLine  +  "Errors = "  +  Ctx.ToString())        
        /// 	  End If                                                                                                           
        /// 	</code>
        /// </example>
        public VoidTransaction(string origId, UserInfo userInfo, PayflowConnectionData payflowConnectionData,
            string requestId) : base(PayflowConstants.TrxtypeVoid, origId, userInfo, payflowConnectionData, requestId)
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="origId">Original Transaction Id.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     The Void transaction prevents a transaction from being settled, but does
        ///     not release the authorization (hold on funds) on the cardholder’s account.
        ///     Delayed Capture, Sale, Credit, Authorization, and Voice
        ///     Authorization transactions can be voided. A Void transaction cannot be voided.
        ///     The Void must occur prior to settlement.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	// Create a new Void Transaction.                                                                     
        /// 	// The ORIGID is the PNREF no. for a previous transaction.                                 
        /// 	VoidTransaction Trans = new VoidTransaction("V63A0A07BE5A",                         
        /// 		User, PayflowUtility.RequestId);                                                 
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
        /// 		}                                                                                                            
        /// 	}                                                                                                                    
        /// 	                                                                                                                     
        /// 	// Get the Context and check for any contained SDK specific errors.                    
        /// 	Context Ctx = Resp.TransactionContext;                                                           
        /// 	if (Ctx != null  &amp;  &amp;  Ctx.getErrorCount() > 0)                                                        
        /// 	{                                                                                                                    
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());     
        /// 	}                                                                                                                    
        /// 
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	...............
        /// 	' Populate data objects
        /// 	...............
        /// 	  ' Create a new Void Transaction.                                                                    
        /// 	  ' The ORIGID is the PNREF no. for a previous transaction.                                 
        /// 		 Dim Trans As VoidTransaction = New VoidTransaction("V63A0A07BE5A",           
        /// 	 User, PayflowUtility.RequestId)                                                    
        /// 	                                                                                                                     
        /// 	  ' Submit the transaction.                                                                                
        /// 	  Dim Resp As Response = Trans.SubmitTransaction()                                        
        /// 	                                                                                                                     
        /// 	  If Not Resp Is Nothing Then                                                                            
        /// 	      ' Get the Transaction Response parameters.                                                
        /// 	      Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse      
        /// 	      If Not TrxnResponse Is Nothing Then                                                          
        /// 	          Console.WriteLine("RESULT = "  +  TrxnResponse.Result)                           
        /// 	          Console.WriteLine("PNREF = "  +  TrxnResponse.Pnref)                              
        /// 	          Console.WriteLine("RESPMSG = "  +  TrxnResponse.RespMsg)                    
        /// 	      End If                                                                                                       
        /// 	  End If                                                                                                           
        /// 	                                                                                                                     
        /// 	  ' Get the Context and check for any contained SDK specific errors.                    
        /// 	  Dim Ctx As Context = Resp.TransactionContext                                               
        /// 	  If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then                             
        /// 	      Console.WriteLine(Environment.NewLine  +  "Errors = "  +  Ctx.ToString())        
        /// 	  End If                                                                                                           
        /// 	</code>
        /// </example>
        public VoidTransaction(string origId, UserInfo userInfo, string requestId) : base(PayflowConstants.TrxtypeVoid,
            origId, userInfo, requestId)
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="origId">Original Transaction Id.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     The Void transaction prevents a transaction from being settled, but does
        ///     not release the authorization (hold on funds) on the cardholder’s account.
        ///     Delayed Capture, Sale, Credit, Authorization, and Voice
        ///     Authorization transactions can be voided. A Void transaction cannot be voided.
        ///     The Void must occur prior to settlement.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	// Create a new Void Transaction.                                                                     
        /// 	// The ORIGID is the PNREF no. for a previous transaction.                                 
        /// 	VoidTransaction Trans = new VoidTransaction("V63A0A07BE5A",                         
        /// 		User, Connection, PayflowUtility.RequestId);                                                 
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
        /// 		}                                                                                                            
        /// 	}                                                                                                                    
        /// 	                                                                                                                     
        /// 	// Get the Context and check for any contained SDK specific errors.                    
        /// 	Context Ctx = Resp.TransactionContext;                                                           
        /// 	if (Ctx != null  &amp;  &amp;  Ctx.getErrorCount() > 0)                                                        
        /// 	{                                                                                                                    
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());     
        /// 	}                                                                                                                    
        /// 
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	...............
        /// 	' Populate data objects
        /// 	...............
        /// 	  ' Create a new Void Transaction.                                                                    
        /// 	  ' The ORIGID is the PNREF no. for a previous transaction.                                 
        /// 		 Dim Trans As VoidTransaction = New VoidTransaction("V63A0A07BE5A",           
        /// 	 User, Connection, PayflowUtility.RequestId)                                                    
        /// 	                                                                                                                     
        /// 	  ' Submit the transaction.                                                                                
        /// 	  Dim Resp As Response = Trans.SubmitTransaction()                                        
        /// 	                                                                                                                     
        /// 	  If Not Resp Is Nothing Then                                                                            
        /// 	      ' Get the Transaction Response parameters.                                                
        /// 	      Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse      
        /// 	      If Not TrxnResponse Is Nothing Then                                                          
        /// 	          Console.WriteLine("RESULT = "  +  TrxnResponse.Result)                           
        /// 	          Console.WriteLine("PNREF = "  +  TrxnResponse.Pnref)                              
        /// 	          Console.WriteLine("RESPMSG = "  +  TrxnResponse.RespMsg)                    
        /// 	      End If                                                                                                       
        /// 	  End If                                                                                                           
        /// 	                                                                                                                     
        /// 	  ' Get the Context and check for any contained SDK specific errors.                    
        /// 	  Dim Ctx As Context = Resp.TransactionContext                                               
        /// 	  If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then                             
        /// 	      Console.WriteLine(Environment.NewLine  +  "Errors = "  +  Ctx.ToString())        
        /// 	  End If                                                                                                           
        /// 	</code>
        /// </example>
        public VoidTransaction(string origId, UserInfo userInfo, PayflowConnectionData payflowConnectionData,
            Invoice invoice, string requestId) : base(PayflowConstants.TrxtypeVoid, origId, userInfo,
            payflowConnectionData, invoice, requestId)
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="origId">Original Transaction Id.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     The Void transaction prevents a transaction from being settled, but does
        ///     not release the authorization (hold on funds) on the cardholder’s account.
        ///     Delayed Capture, Sale, Credit, Authorization, and Voice
        ///     Authorization transactions can be voided. A Void transaction cannot be voided.
        ///     The Void must occur prior to settlement.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	...............
        /// 	// Populate data objects
        /// 	...............
        /// 	// Create a new Void Transaction.                                                                     
        /// 	// The ORIGID is the PNREF no. for a previous transaction.                                 
        /// 	VoidTransaction Trans = new VoidTransaction("V63A0A07BE5A",                         
        /// 		User, Connection, Inv, PayflowUtility.RequestId);                                                 
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
        /// 		}                                                                                                            
        /// 	}                                                                                                                    
        /// 	                                                                                                                     
        /// 	// Get the Context and check for any contained SDK specific errors.                    
        /// 	Context Ctx = Resp.TransactionContext;                                                           
        /// 	if (Ctx != null  &amp;  &amp;  Ctx.getErrorCount() > 0)                                                        
        /// 	{                                                                                                                    
        /// 		Console.WriteLine(Environment.NewLine + "Errors = " + Ctx.ToString());     
        /// 	}                                                                                                                    
        /// 
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	...............
        /// 	' Populate data objects
        /// 	...............
        /// 	  ' Create a new Void Transaction.                                                                    
        /// 	  ' The ORIGID is the PNREF no. for a previous transaction.                                 
        /// 		 Dim Trans As VoidTransaction = New VoidTransaction("V63A0A07BE5A",           
        /// 	 User, Connection, Inv, PayflowUtility.RequestId)                                                    
        /// 	                                                                                                                     
        /// 	  ' Submit the transaction.                                                                                
        /// 	  Dim Resp As Response = Trans.SubmitTransaction()                                        
        /// 	                                                                                                                     
        /// 	  If Not Resp Is Nothing Then                                                                            
        /// 	      ' Get the Transaction Response parameters.                                                
        /// 	      Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse      
        /// 	      If Not TrxnResponse Is Nothing Then                                                          
        /// 	          Console.WriteLine("RESULT = "  +  TrxnResponse.Result)                           
        /// 	          Console.WriteLine("PNREF = "  +  TrxnResponse.Pnref)                              
        /// 	          Console.WriteLine("RESPMSG = "  +  TrxnResponse.RespMsg)                    
        /// 	      End If                                                                                                       
        /// 	  End If                                                                                                           
        /// 	                                                                                                                     
        /// 	  ' Get the Context and check for any contained SDK specific errors.                    
        /// 	  Dim Ctx As Context = Resp.TransactionContext                                               
        /// 	  If Not Ctx Is Nothing AndAlso Ctx.getErrorCount() > 0 Then                             
        /// 	      Console.WriteLine(Environment.NewLine  +  "Errors = "  +  Ctx.ToString())        
        /// 	  End If                                                                                                           
        /// 	</code>
        /// </example>
        public VoidTransaction(string origId, UserInfo userInfo, Invoice invoice, string requestId) : this(origId,
            userInfo, null, invoice, requestId)
        {
        }

        #endregion
    }
}