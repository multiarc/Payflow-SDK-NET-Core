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
using PFProSDK.Common;
using PFProSDK.Common.Exceptions;
using PFProSDK.Common.Utility;

#endregion

namespace PFProSDK.DataObjects
{
    /// <summary>
    ///     Container class for response messages.
    /// </summary>
    /// <remarks>
    ///     This class enclosed response data objects specific to
    ///     following:
    ///     <list type="bullet">
    ///         <item>
    ///             Transaction response
    ///             --> Response messages common to all transactions.
    ///         </item>
    ///         <item>
    ///             Fraud response
    ///             --> Fraud Filters response messages.
    ///         </item>
    ///         <item>
    ///             Recurring response
    ///             --> Recurring transaction response messages.
    ///         </item>
    ///         <item>
    ///             Buyerauth response
    ///             --> Buyer auth response messages. (Not supported.)
    ///         </item>
    ///     </list>
    ///     <para>
    ///         Additionally the Response class also contains the
    ///         transaction context, full request response string values.
    ///     </para>
    ///     <seealso cref="FraudResponse" />
    ///     <seealso cref="TransactionResponse" />
    ///     <seealso cref="RecurringResponse" />
    ///     <seealso cref="BuyerAuthResponse" />
    ///     <seealso cref="Context" />
    /// </remarks>
    /// <example>
    ///     Following example shows, how to obtain response
    ///     of a transaction and how to use it.
    ///     <code lang="C#" escaped="false">
    /// 		..........
    /// 		// Trans is the transaction object.
    /// 		..........
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
    /// 		..........
    /// 		' Trans is the transaction object.
    /// 		..........
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
    public sealed class Response
    {
        #region "Member Variables"

        /// <summary>
        ///     Holds parsed response hash table
        /// </summary>
        private Hashtable _mResponseHashTable;

        /// <summary>
        ///     Response string
        /// </summary>
        private string _mResponseString;

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets FraudResult
        /// </summary>
        /// <remarks>
        ///     Gets the container object for all the fraud filters
        ///     related response messages.
        ///     <seealso cref="FraudResponse" />
        /// </remarks>
        public FraudResponse FraudResponse { get; private set; }

        /// <summary>
        ///     Gets BuyerAuthResult
        /// </summary>
        /// <remarks>
        ///     Gets the container object for all the buyer auth
        ///     related response messages.
        ///     <seealso cref="BuyerAuthResponse" />
        /// </remarks>
        public BuyerAuthResponse BuyerAuthResponse { get; private set; }

        /// <summary>
        ///     Gets RecurringResult
        /// </summary>
        /// <remarks>
        ///     Gets the container object for all the recurring
        ///     transaction related response messages.
        ///     <seealso cref="RecurringResponse" />
        /// </remarks>
        public RecurringResponse RecurringResponse { get; private set; }

        /// <summary>
        ///     Gets ExpressCheckout Response for GET action
        /// </summary>
        /// <remarks>
        ///     Gets the container object for all the express
        ///     checkout related response messages for GET.
        ///     <seealso cref="RecurringResponse" />
        /// </remarks>
        public EcGetResponse ExpressCheckoutGetResponse { get; private set; }

        /// <summary>
        ///     Gets ExpressCheckout Response for DO action
        /// </summary>
        /// <remarks>
        ///     Gets the container object for all the express
        ///     checkout related response messages for DO.
        ///     <seealso cref="RecurringResponse" />
        /// </remarks>
        public EcDoResponse ExpressCheckoutDoResponse { get; private set; }

        /// <summary>
        ///     Gets ExpressCheckout Response for Set action
        /// </summary>
        /// <remarks>
        ///     Gets the container object for all the express
        ///     checkout related response messages for SET.
        ///     <seealso cref="RecurringResponse" />
        /// </remarks>
        public ExpressCheckoutResponse ExpressCheckoutSetResponse { get; private set; }

        /// <summary>
        ///     Gets ExpressCheckout Response for Update action
        /// </summary>
        /// <remarks>
        ///     Gets the container object for all the express
        ///     checkout related response messages for UPDATE.
        ///     <seealso cref="RecurringResponse" />
        /// </remarks>
        public EcUpdateResponse ExpressCheckoutUpdateResponse { get; private set; }

        /// <summary>
        ///     Gets TransactionResult
        /// </summary>
        /// <remarks>
        ///     Gets the container object for response messages common to
        ///     all the transactions.
        ///     <seealso cref="TransactionResponse" />
        /// </remarks>
        public TransactionResponse TransactionResponse { get; private set; }

        /// <summary>
        ///     Gets transaction context
        /// </summary>
        /// <remarks>
        ///     Gets the transaction context
        ///     populated with errors, if any.
        ///     <seealso cref="Context" />
        /// </remarks>
        public Context TransactionContext { get; }

        /// <summary>
        ///     Gets extended response
        ///     list.
        /// </summary>
        /// <remarks>
        ///     This arraylist contains the extend data objects populated
        ///     with the response messages.
        ///     <seealso cref="ExtendData" />
        /// </remarks>
        public ArrayList ExtendDataList { get; private set; }

        // moved from transaction response to response obj
        /// <summary>
        ///     Gets Request
        /// </summary>
        /// <remarks>
        ///     This is the request string as sent to the
        ///     PayPal payment gateway.
        /// </remarks>
        public string RequestString { get; private set; }

        /// <summary>
        ///     Gets RequestId
        /// </summary>
        /// <remarks>
        ///     This is the request id set
        ///     for the transaction as sent to the PayPal payment
        ///     gateway.
        /// </remarks>
        public string RequestId { get; }

        #endregion

        #region "Constructors"

        /// <summary>
        ///     Constructor for Response
        /// </summary>
        public Response()
        {
        }

        /// <summary>
        ///     Constructor for Response
        /// </summary>
        /// <param name="requestId">Request Id</param>
        /// <param name="trxContext">Transaction Context object</param>
        public Response(string requestId, Context trxContext)
        {
            TransactionContext = trxContext;
            //mTransactionResponse = new TransactionResponse(RequestId, ResponseId);\
            RequestId = requestId;
        }

        #endregion

        #region "Core Functions"

        /// <summary>
        ///     Sets the response params
        /// </summary>
        /// <param name="response">Response string</param>
        internal void SetParams(string response)
        {
            try
            {
                _mResponseString = response;
                if (response != null)
                {
                    var resultIndex = response.IndexOf(PayflowConstants.ParamResult);
                    if (resultIndex >= 0)
                    {
                        if (resultIndex > 0) response = response.Substring(resultIndex);
                        ParseResponse(response);
                        SetResultParams(ref _mResponseHashTable);
                        SetFraudResultParams(ref _mResponseHashTable);
                        SetBuyerAuthResultParams(ref _mResponseHashTable);
                        var trxType = PayflowUtility.LocateValueForName(RequestString,
                            PayflowConstants.ParamTrxtype, false);
                        if (string.Equals(trxType, PayflowConstants.TrxtypeRecurring))
                        {
                            SetRecurringResultParams(ref _mResponseHashTable);
                        }
                        else
                        {
                            SetExpressCheckoutDoResultParams(ref _mResponseHashTable);
                            SetExpressCheckoutGetResultParams(ref _mResponseHashTable);
                            SetExpressCheckoutSetResultParams(ref _mResponseHashTable);
                            SetExpressCheckoutUpdateResultParams(ref _mResponseHashTable);
                        }

                        _mResponseHashTable.Remove(PayflowConstants.IntlParamFullresponse);
                        SetExtDataList();
                        _mResponseHashTable = null;
                    }
                    else
                    {
                        //Append the RESULT and RESPMSG for error code
                        //E_UNKNOWN_STATE and create a message.
                        //Call SetParams again on it.
                        var responseValue = PayflowConstants.ParamResult
                                            + PayflowConstants.SeparatorNvp
                                            + (string) PayflowConstants.CommErrorCodes[PayflowConstants.EUnknownState]
                                            + PayflowConstants.DelimiterNvp
                                            + PayflowConstants.ParamRespmsg
                                            + PayflowConstants.SeparatorNvp
                                            + (string) PayflowConstants.CommErrorMessages[
                                                PayflowConstants.EUnknownState]
                                            + ", " + _mResponseString;
                        SetParams(responseValue);
                    }
                }
                else
                {
                    var addlMessage = "Empty response";
                    var err = PayflowUtility.PopulateCommError(PayflowConstants.EEmptyParamList, null,
                        PayflowConstants.SeverityWarn, false, addlMessage);
                    TransactionContext.AddError(err);
                    err = TransactionContext.GetError(TransactionContext.GetErrorCount() - 1);
                    var responseValue = err.ToString();
                    SetParams(responseValue);
                }
            }
            catch (BaseException baseEx)
            {
                //ErrorObject Error = PayflowUtility.PopulateCommError(PayflowConstants.E_UNKNOWN_STATE,BaseEx,PayflowConstants.SEVERITY_ERROR,false, null);
                var error = baseEx.GetFirstErrorInExceptionContext();
                TransactionContext.AddError(error);
                var responseValue = error.ToString();
                SetParams(responseValue);
            }
            catch (Exception ex)
            {
                var dEx = new DataObjectException(ex);
                var error = PayflowUtility.PopulateCommError(PayflowConstants.EUnknownState, dEx,
                    PayflowConstants.SeverityError, false, null);
                TransactionContext.AddError(error);
                var responseValue = error.ToString();
                SetParams(responseValue);
            }

            //catch
            //{
            //    ErrorObject Error = PayflowUtility.PopulateCommError(PayflowConstants.E_UNKNOWN_STATE,null,PayflowConstants.SEVERITY_ERROR,false,null);
            //    mContext.AddError(Error);
            //    String ResponseValue = Error.ToString();
            //    this.SetParams(ResponseValue);
            //}
        }

        /// <summary>
        ///     Sets the transaction result params
        /// </summary>
        /// <param name="responseHashTable">Response Hash table by ref</param>
        private void SetResultParams(ref Hashtable responseHashTable)
        {
            try
            {
                TransactionResponse = new TransactionResponse();
                TransactionResponse.SetParams(ref responseHashTable);
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var dEx = new DataObjectException(ex);
                throw dEx;
            }

            //catch
            //{
            //    throw new Exception();				
            //}
        }

        /// <summary>
        ///     Sets fraud result params
        /// </summary>
        /// <param name="responseHashTable">Response Hash table by ref</param>
        private void SetFraudResultParams(ref Hashtable responseHashTable)
        {
            try
            {
                FraudResponse = new FraudResponse();
                FraudResponse.SetParams(ref responseHashTable);
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var dEx = new DataObjectException(ex);
                throw dEx;
            }

            //catch
            //{
            //    throw new Exception();				
            //}
        }

        /// <summary>
        ///     Sets recurring result params
        /// </summary>
        /// <param name="responseHashTable">Response Hash table by ref</param>
        private void SetRecurringResultParams(ref Hashtable responseHashTable)
        {
            try
            {
                RecurringResponse = new RecurringResponse();
                RecurringResponse.SetParams(ref responseHashTable);
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var dEx = new DataObjectException(ex);
                throw dEx;
            }

            //catch
            //{
            //    throw new Exception();				
            //}
        }

        /// <summary>
        ///     Sets the ExpressCheckout response for GET params
        /// </summary>
        /// <param name="responseHashTable">Response Hash table by ref</param>
        private void SetExpressCheckoutGetResultParams(ref Hashtable responseHashTable)
        {
            try
            {
                ExpressCheckoutGetResponse = new EcGetResponse();
                ExpressCheckoutGetResponse.SetParams(ref responseHashTable);
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var dEx = new DataObjectException(ex);
                throw dEx;
            }

            //catch
            //{
            //    throw new Exception();				
            //}
        }

        /// <summary>
        ///     Sets the ExpressCheckout response for DO params
        /// </summary>
        /// <param name="responseHashTable">Response Hash table by ref</param>
        private void SetExpressCheckoutDoResultParams(ref Hashtable responseHashTable)
        {
            try
            {
                ExpressCheckoutDoResponse = new EcDoResponse();
                ExpressCheckoutDoResponse.SetParams(ref responseHashTable);
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var dEx = new DataObjectException(ex);
                throw dEx;
            }

            //catch
            //{
            //    throw new Exception();				
            //}
        }

        /// <summary>
        ///     Sets the ExpressCheckout response for SET params
        /// </summary>
        /// <param name="responseHashTable">Response Hash table by ref</param>
        private void SetExpressCheckoutSetResultParams(ref Hashtable responseHashTable)
        {
            try
            {
                ExpressCheckoutSetResponse = new ExpressCheckoutResponse();
                ExpressCheckoutSetResponse.SetParams(ref responseHashTable);
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var dEx = new DataObjectException(ex);
                throw dEx;
            }

            //catch
            //{
            //    throw new Exception();				
            //}
        }

        /// <summary>
        ///     Sets the ExpressCheckout response for UPDATE params
        /// </summary>
        /// <param name="responseHashTable">Response Hash table by ref</param>
        private void SetExpressCheckoutUpdateResultParams(ref Hashtable responseHashTable)
        {
            try
            {
                ExpressCheckoutUpdateResponse = new EcUpdateResponse();
                ExpressCheckoutUpdateResponse.SetParams(ref responseHashTable);
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var dEx = new DataObjectException(ex);
                throw dEx;
            }

            //catch
            //{
            //    throw new Exception();				
            //}
        }

        /// <summary>
        ///     Sets buyer auth results params
        /// </summary>
        /// <param name="responseHashTable">Response Hash table by ref</param>
        private void SetBuyerAuthResultParams(ref Hashtable responseHashTable)
        {
            try
            {
                BuyerAuthResponse = new BuyerAuthResponse();
                BuyerAuthResponse.SetParams(ref responseHashTable);
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var dEx = new DataObjectException(ex);
                throw dEx;
            }

            //catch
            //{
            //    throw new Exception();				
            //}
        }

        /// <summary>
        ///     Parses response
        /// </summary>
        /// <param name="response">Response string</param>
        private void ParseResponse(string response)
        {
            //Pass a new context object to the validator. We do not mean to
            //validate request here. We just need to parse it and populate
            //the response hashtable.
            var respContext = new Context();
            //Check newly created context for fatal error. If the newly created
            //context has fatal error, it means that, there is a problem with the
            //message file. So we'll have manually assign the result,respmsg value.
            //Add following values in hashtable manually 
            //RESULT=<Code for E_UNKNOWN_STATE>
            //RESPMSG=<Message for E_UNKNOWN_STATE>, <Response String>
            if (respContext.HighestErrorLvl == PayflowConstants.SeverityFatal)
            {
                var result = PayflowUtility.LocateValueForName(response, PayflowConstants.ParamResult, false);
                var respMsg = PayflowUtility.LocateValueForName(response, PayflowConstants.ParamRespmsg, false);
                if (_mResponseHashTable == null) _mResponseHashTable = new Hashtable();

                _mResponseHashTable.Add(PayflowConstants.IntlParamFullresponse, _mResponseString);
                _mResponseHashTable.Add(PayflowConstants.ParamResult, result);
                _mResponseHashTable.Add(PayflowConstants.ParamRespmsg, respMsg);
            }
            else
            {
                _mResponseHashTable = ParameterListValidator.ParseNvpList(response, ref respContext, true);
                respContext = null;
                if (_mResponseHashTable != null)
                    _mResponseHashTable.Add(PayflowConstants.IntlParamFullresponse, response);
            }
        }

        /// <summary>
        ///     Populates extended response
        ///     array list
        /// </summary>
        private void SetExtDataList()
        {
            ExtendData extData = null;
            string name;
            string value;
            if (_mResponseHashTable == null || _mResponseHashTable.Count == 0)
            {
                ExtendDataList = null;
            }
            else
            {
                ExtendDataList = new ArrayList();
                foreach (DictionaryEntry extDataPair in _mResponseHashTable)
                {
                    name = (string) extDataPair.Key;
                    value = (string) extDataPair.Value;
                    //Separate the recurring inquiry response here
                    var duplicateKeyIndex = name.IndexOf(PayflowConstants.TagDuplicate);
                    if (duplicateKeyIndex > 0) name = name.Substring(0, duplicateKeyIndex - 1);
                    if (name.StartsWith(PayflowConstants.PrefixRecurringInquiryResp))
                    {
                        RecurringResponse.InquiryParams.Add(name, value);
                    }
                    else
                    {
                        extData = new ExtendData(name, value);
                        ExtendDataList.Add(extData);
                    }

                    name = null;
                    value = null;
                    extData = null;
                }
            }
        }

        internal void SetRequestString(string requestString)
        {
            RequestString = requestString;
        }

        #endregion
    }
}