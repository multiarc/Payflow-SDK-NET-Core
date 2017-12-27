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

using System.Collections;
using PFProSDK.Common.Utility;

#endregion

namespace PFProSDK.DataObjects
{
    /// <summary>
    ///     Used for transaction response.
    /// </summary>
    /// <remarks>
    ///     TransactionResponse object is contained in the main response
    ///     object Response of the transaction.
    /// </remarks>
    /// <example>
    ///     Following is the example of how to get the transaction response
    ///     after the transaction.
    ///     <code lang="C#" escaped="false">
    ///   ...................
    ///   //Trans is the transaction object.
    ///   ...................
    ///  
    ///  // Submit the transaction.
    /// 	Response Resp = Trans.SubmitTransaction();
    /// 			
    /// 	if (Resp != null)
    ///  {
    /// 		// Get the Transaction Response parameters.
    /// 		TransactionResponse TrxnResponse =  Resp.TransactionResponse;
    /// 		if (TrxnResponse != null)
    /// 		{
    /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result);
    /// 			Console.WriteLine("PNREF = " + TrxnResponse.Pnref);
    /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
    /// 			Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode);
    /// 			Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr);
    /// 			Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip);
    /// 			Console.WriteLine("IAVS = " + TrxnResponse.IAVS);
    /// 		}
    /// 	}
    /// 	 ................
    ///  </code>
    ///     <code lang="Visual Basic" escaped="false">
    /// 	.........................
    /// 	' Trans is the transaction object.
    /// 	.........................
    /// 	' Submit the transaction.
    /// 	Dim Resp As Response = Trans.SubmitTransaction()
    /// 	
    /// 	If Not Resp Is Nothing Then
    /// 		' Get the Transaction Response parameters.
    /// 		Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
    /// 	
    /// 		If Not TrxnResponse Is Nothing Then
    /// 			Console.WriteLine("RESULT = " + TrxnResponse.Result)
    /// 			Console.WriteLine("PNREF = " + TrxnResponse.Pnref)
    /// 			Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
    /// 			Console.WriteLine("AUTHCODE = " + TrxnResponse.AuthCode)
    /// 			Console.WriteLine("AVSADDR = " + TrxnResponse.AVSAddr)
    /// 			Console.WriteLine("AVSZIP = " + TrxnResponse.AVSZip)
    /// 			Console.WriteLine("IAVS = " + TrxnResponse.IAVS)
    /// 		End If
    /// 	End If
    /// 	.........................
    ///  </code>
    /// </example>
    public sealed class TransactionResponse : BaseResponseDataObject
    {
        #region "Constructors"

        /// <summary>
        ///     Constructor for Transaction response.
        /// </summary>
        internal TransactionResponse()
        {
        }

        #endregion

        #region "Functions"

        /// <summary>
        ///     Sets response params
        /// </summary>
        /// <param name="responseHashTable">Response Hash table by ref</param>
        internal void SetParams(ref Hashtable responseHashTable)
        {
            //			mResponse = (String) ResponseHashTable[PayflowConstants.INTL_PARAM_FULLRESPONSE];
            Result = int.Parse((string) responseHashTable[PayflowConstants.ParamResult]);
            Pnref = (string) responseHashTable[PayflowConstants.ParamPnref];
            RespMsg = (string) responseHashTable[PayflowConstants.ParamRespmsg];
            AuthCode = (string) responseHashTable[PayflowConstants.ParamAuthcode];
            AvsAddr = (string) responseHashTable[PayflowConstants.ParamAvsaddr];
            AvsZip = (string) responseHashTable[PayflowConstants.ParamAvszip];
            CardSecure = (string) responseHashTable[PayflowConstants.ParamCardsecure];
            Cvv2Match = (string) responseHashTable[PayflowConstants.ParamCvv2Match];
            Iavs = (string) responseHashTable[PayflowConstants.ParamIavs];
            OrigResult = (string) responseHashTable[PayflowConstants.ParamOrigresult];
            TransState = (string) responseHashTable[PayflowConstants.ParamTransstate];
            CustRef = (string) responseHashTable[PayflowConstants.ParamCustref];
            StartTime = (string) responseHashTable[PayflowConstants.ParamStarttime];
            EndTime = (string) responseHashTable[PayflowConstants.ParamEndtime];
            Duplicate = (string) responseHashTable[PayflowConstants.ParamDuplicate];
            DateToSettle = (string) responseHashTable[PayflowConstants.ParamDateToSettle];
            BatchId = (string) responseHashTable[PayflowConstants.ParamBatchid];
            AddlMsgs = (string) responseHashTable[PayflowConstants.ParamAddlmsgs];
            RespText = (string) responseHashTable[PayflowConstants.ParamResptext];
            ProcAvs = (string) responseHashTable[PayflowConstants.ParamProcavs];
            ProcCardSecure = (string) responseHashTable[PayflowConstants.ParamProccardsecure];
            ProcCvv2 = (string) responseHashTable[PayflowConstants.ParamProccvv2];
            HostCode = (string) responseHashTable[PayflowConstants.ParamHostcode];
            //Added as SETTLE_DATE param is also available when VERBOSITY= MEDIUM
            //2005-12-10
            SettleDate = (string) responseHashTable[PayflowConstants.ParamSettleDate];
            OrigPnref = (string) responseHashTable[PayflowConstants.ParamOrigpnref];
            PPref = (string) responseHashTable[PayflowConstants.ParamPpref];
            CorrelationId = (string) responseHashTable[PayflowConstants.ParamCorrelationid];
            FeeAmt = (string) responseHashTable[PayflowConstants.ParamFeeamt];
            PendingReason = (string) responseHashTable[PayflowConstants.ParamPendingreason];
            PaymentType = (string) responseHashTable[PayflowConstants.ParamPaymenttype];
            // Added STATUS when VERBOSITY=MEDIUM
            // 2006-09-18 TS
            Status = (string) responseHashTable[PayflowConstants.ParamStatus];
            // Added BALAMT & AMEXID 2007-06-05 tsieber
            BalAmt = (string) responseHashTable[PayflowConstants.ParamBalamt];
            AmexId = (string) responseHashTable[PayflowConstants.ParamAmexid];
            // Added AMEXPOSDATA 2007-11-05 tsieber
            AmexPosData = (string) responseHashTable[PayflowConstants.ParamAmexposdata];
            Acct = (string) responseHashTable[PayflowConstants.ParamAcct];
            LastName = (string) responseHashTable[PayflowConstants.ParamLastname];
            FirstName = (string) responseHashTable[PayflowConstants.ParamFirstname];
            Amt = (string) responseHashTable[PayflowConstants.ParamAmt];
            ExpDate = (string) responseHashTable[PayflowConstants.ParamExpdate];
            TransTime = (string) responseHashTable[PayflowConstants.ParamTranstime];
            CardType = (string) responseHashTable[PayflowConstants.ParamCardtype];
            OrigAmt = (string) responseHashTable[PayflowConstants.ParamOrigamt];
            EmailMatch = (string) responseHashTable[PayflowConstants.ParamEmailmatch];
            PhoneMatch = (string) responseHashTable[PayflowConstants.ParamPhonematch];
            ExtRspMsg = (string) responseHashTable[PayflowConstants.ParamExtrspmsg];
            SecureToken = (string) responseHashTable[PayflowConstants.ParamSecuretoken];
            SecureTokenId = (string) responseHashTable[PayflowConstants.ParamSecuretokenid];

            // Remove the used response params from hash table.
            // ResponseHashTable.Remove(PayflowConstants.INTL_PARAM_FULLRESPONSE);
            responseHashTable.Remove(PayflowConstants.ParamResult);
            responseHashTable.Remove(PayflowConstants.ParamPnref);
            responseHashTable.Remove(PayflowConstants.ParamRespmsg);
            responseHashTable.Remove(PayflowConstants.ParamAuthcode);
            responseHashTable.Remove(PayflowConstants.ParamAvsaddr);
            responseHashTable.Remove(PayflowConstants.ParamAvszip);
            responseHashTable.Remove(PayflowConstants.ParamCardsecure);
            responseHashTable.Remove(PayflowConstants.ParamCvv2Match);
            responseHashTable.Remove(PayflowConstants.ParamIavs);
            responseHashTable.Remove(PayflowConstants.ParamOrigresult);
            responseHashTable.Remove(PayflowConstants.ParamTransstate);
            responseHashTable.Remove(PayflowConstants.ParamCustref);
            responseHashTable.Remove(PayflowConstants.ParamStarttime);
            responseHashTable.Remove(PayflowConstants.ParamEndtime);
            responseHashTable.Remove(PayflowConstants.ParamDuplicate);
            responseHashTable.Remove(PayflowConstants.ParamDateToSettle);
            responseHashTable.Remove(PayflowConstants.ParamBatchid);
            responseHashTable.Remove(PayflowConstants.ParamAddlmsgs);
            responseHashTable.Remove(PayflowConstants.ParamResptext);
            responseHashTable.Remove(PayflowConstants.ParamProcavs);
            responseHashTable.Remove(PayflowConstants.ParamProccardsecure);
            responseHashTable.Remove(PayflowConstants.ParamProccvv2);
            responseHashTable.Remove(PayflowConstants.ParamHostcode);
            responseHashTable.Remove(PayflowConstants.ParamSettleDate);
            responseHashTable.Remove(PayflowConstants.ParamOrigpnref);
            responseHashTable.Remove(PayflowConstants.ParamPpref);
            responseHashTable.Remove(PayflowConstants.ParamCorrelationid);
            responseHashTable.Remove(PayflowConstants.ParamFeeamt);
            responseHashTable.Remove(PayflowConstants.ParamPendingreason);
            responseHashTable.Remove(PayflowConstants.ParamPaymenttype);
            responseHashTable.Remove(PayflowConstants.ParamBalamt);
            responseHashTable.Remove(PayflowConstants.ParamAmexid);
            responseHashTable.Remove(PayflowConstants.ParamAmexposdata);
            responseHashTable.Remove(PayflowConstants.ParamAcct);
            responseHashTable.Remove(PayflowConstants.ParamLastname);
            responseHashTable.Remove(PayflowConstants.ParamFirstname);
            responseHashTable.Remove(PayflowConstants.ParamAmt);
            responseHashTable.Remove(PayflowConstants.ParamExpdate);
            responseHashTable.Remove(PayflowConstants.ParamTranstime);
            responseHashTable.Remove(PayflowConstants.ParamCardtype);
            responseHashTable.Remove(PayflowConstants.ParamOrigamt);
            responseHashTable.Remove(PayflowConstants.ParamEmailmatch);
            responseHashTable.Remove(PayflowConstants.ParamPhonematch);
            responseHashTable.Remove(PayflowConstants.ParamExtrspmsg);
            responseHashTable.Remove(PayflowConstants.ParamSecuretoken);
            responseHashTable.Remove(PayflowConstants.ParamSecuretokenid);
            // Commented Line below to reserve Status for Recurring Inquiry 2007-06-05 tsieber
            // ResponseHashTable.Remove(PayflowConstants.PARAM_STATUS);
        }

        #endregion

        #region "Member Variables"

        //Expose OrigPnref param 28-12-2005

        //Added a SETTLE_DATE param is also available when VERBOSITY= MEDIUM
        //2005-12-10

        // Added STATUS param is also available when VERBOSITY = MEDIUM

        // Added BALAMT param, AMEX CAPN 05/31/07

        // Added AMEXID param, AMEX CAPN 05/31/07 VERBOSITY = MEDIUM

        // Added AMEXPOSDATA param, AMEX CAPN 11/07/07 VERBOSITY = MEDIUM

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets Result
        /// </summary>
        /// <remarks>
        ///     The outcome of the attempted transaction. A
        ///     result of 0 (zero) indicates the transaction was
        ///     approved. Any other number indicates a
        ///     decline or error.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>RESULT</code>
        /// </remarks>
        public int Result { get; private set; }

        /// <summary>
        ///     Gets Pnref
        /// </summary>
        /// <remarks>
        ///     PayPal Reference ID, a unique number that
        ///     identifies the transaction.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PNREF</code>
        /// </remarks>
        public string Pnref { get; private set; }

        /// <summary>
        ///     Gets RespMsg
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The response message returned with the
        ///         transaction result. Exact wording varies.
        ///         Sometimes a colon appears after the initial
        ///         RESPMSG followed by more detailed
        ///         information.
        ///     </para>
        ///     <code>APPROVED</code>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>RESPMSG</code>
        /// </remarks>
        public string RespMsg { get; private set; }

        /// <summary>
        ///     Gets AuthCode
        /// </summary>
        /// <remarks>
        ///     Returned for Sale, Authorization, and Voice
        ///     Authorization transactions. AUTHCODE is the
        ///     approval code obtained over the phone from
        ///     the processing network.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>AUTHCODE</code>
        /// </remarks>
        public string AuthCode { get; private set; }

        /// <summary>
        ///     Gets AVSAddr
        /// </summary>
        /// <remarks>
        ///     AVS address responses are for advice only.
        ///     This process does not affect the outcome of the
        ///     authorization.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>AVSADDR</code>
        /// </remarks>
        public string AvsAddr { get; private set; }

        /// <summary>
        ///     Gets AVSZip
        /// </summary>
        /// <remarks>
        ///     AVS ZIP code responses are for advice only.
        ///     This process does not affect the outcome of the
        ///     authorization.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>AVSZIP</code>
        /// </remarks>
        public string AvsZip { get; private set; }

        /// <summary>
        ///     Gets CardSecure
        /// </summary>
        /// <remarks>
        ///     Obtained for Visa cards.
        ///     CAVV validity.
        ///     Y=valid, N=Not valid, X=cannot determine
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CARDSECURE</code>
        /// </remarks>
        public string CardSecure { get; private set; }

        /// <summary>
        ///     Gets CVV2Match
        /// </summary>
        /// <remarks>
        ///     Result of the card security code (CVV2) check.
        ///     This value does not affect the outcome of the
        ///     transaction.
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Value</term>
        ///             <description>Description</description>
        ///         </listheader>
        ///         <item>
        ///             <term>Y</term>
        ///             <description>The submitted value matches the data on file for the card.</description>
        ///         </item>
        ///         <item>
        ///             <term>N</term>
        ///             <description>The submitted value does not match the data on file for the card.</description>
        ///         </item>
        ///         <item>
        ///             <term>X</term>
        ///             <description>The cardholder’s bank does not support this service.</description>
        ///         </item>
        ///     </list>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CVV2MATCH</code>
        /// </remarks>
        public string Cvv2Match { get; private set; }

        /// <summary>
        ///     Gets EMailMatch
        /// </summary>
        /// <remarks>
        ///     Result of the e-mail check.
        ///     This value does not affect the outcome of the
        ///     transaction.
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Value</term>
        ///             <description>Description</description>
        ///         </listheader>
        ///         <item>
        ///             <term>Y</term>
        ///             <description>The submitted value matches the data on file for the card holder.</description>
        ///         </item>
        ///         <item>
        ///             <term>N</term>
        ///             <description>The submitted value does not match the data on file for the card holder./description>
        ///         </item>
        ///         <item>
        ///             <term>X</term>
        ///             <description>The cardholder’s bank does not support this service.</description>
        ///         </item>
        ///     </list>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>EMAILMATCH</code>
        /// </remarks>
        public string EmailMatch { get; private set; }

        /// <summary>
        ///     Gets PhoneMatch
        /// </summary>
        /// <remarks>
        ///     Result of the phone check.
        ///     This value does not affect the outcome of the
        ///     transaction.
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Value</term>
        ///             <description>Description</description>
        ///         </listheader>
        ///         <item>
        ///             <term>Y</term>
        ///             <description>The submitted value matches the data on file for the card holder.</description>
        ///         </item>
        ///         <item>
        ///             <term>N</term>
        ///             <description>The submitted value does not match the data on file for the card holder.</description>
        ///         </item>
        ///         <item>
        ///             <term>X</term>
        ///             <description>The cardholder’s bank does not support this service.</description>
        ///         </item>
        ///     </list>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PHONEMATCH</code>
        /// </remarks>
        public string PhoneMatch { get; private set; }

        /// <summary>
        ///     Gets IAVS
        /// </summary>
        /// <remarks>
        ///     International AVS address responses are for
        ///     advice only. This value does not affect the
        ///     outcome of the transaction.
        ///     Indicates whether AVS response is
        ///     international (Y), US (N), or cannot be
        ///     determined (X).
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>IAVS</code>
        /// </remarks>
        public string Iavs { get; private set; }


        /// <summary>
        ///     Gets inquiry OrigResult
        /// </summary>
        /// <remarks>
        ///     Gets the Original transaction result for which
        ///     inquiry transaction is performed.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ORIGRESULT</code>
        /// </remarks>
        public string OrigResult { get; private set; }

        //Expose OrigPnref param 28-12-2005
        /// <summary>
        ///     Gets inquiry OrigPnref
        /// </summary>
        /// <remarks>
        ///     Gets the Original PNREF for which
        ///     inquiry transaction is performed.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ORIGPNREF</code>
        /// </remarks>
        public string OrigPnref { get; private set; }

        /// <summary>
        ///     Gets inquiry TransState
        /// </summary>
        /// <remarks>
        ///     Gets the Transaction state of the transaction for
        ///     which inquiry transaction is performed.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>TRANSSTATE</code>
        /// </remarks>
        public string TransState { get; private set; }

        /// <summary>
        ///     Gets inquiry Custref
        /// </summary>
        /// <remarks>
        ///     Merchant-defined identifier for reporting and
        ///     auditing purposes. For example, you can set
        ///     CUSTREF to the invoice number.
        ///     You can use CUSTREF when performing Inquiry
        ///     transactions. To ensure that you can always
        ///     access the correct transaction when performing
        ///     an Inquiry, you must provide a unique CUSTREF																																						   when submitting any
        ///     transaction, including retries.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CUSTREF</code>
        /// </remarks>
        public string CustRef { get; private set; }

        /// <summary>
        ///     Gets inquiry StartTime
        /// </summary>
        /// <remarks>
        ///     Gets the Start time of the transaction for
        ///     which inquiry transaction is performed.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>STARTTIME</code>
        /// </remarks>
        public string StartTime { get; private set; }

        /// <summary>
        ///     Gets inquiry EndTime
        /// </summary>
        /// <remarks>
        ///     Gets the End time of the transaction for
        ///     which inquiry transaction is performed.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ENDTIME</code>
        /// </remarks>
        public string EndTime { get; private set; }

        /// <summary>
        ///     Gets Duplicate
        /// </summary>
        /// <remarks>
        ///     Indicates transactions sent with duplicate identifier.
        ///     If a transaction is performed with the request id that has
        ///     been previously used for another transaction, Duplicate is
        ///     returned as 1.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>DUPLICATE</code>
        /// </remarks>
        public string Duplicate { get; private set; }

        /// <summary>
        ///     Gets inquiry DateToSettle
        /// </summary>
        /// <remarks>
        ///     Gets the settle date of the transaction for which
        ///     inquiry transaction is performed.
        ///     Value available only before settlement has started
        ///     Value obtained when Payflow Verbosity paramter = MEDIUM
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>DATE_TO_SETTLE</code>
        /// </remarks>
        public string DateToSettle { get; private set; }

        /// <summary>
        ///     Gets inquiry BatchId
        /// </summary>
        /// <remarks>
        ///     Gets the batch id of the transaction for which the
        ///     inquiry transaction is performed.
        ///     Value available only after settlement has assigned a BatchId
        ///     Value obtained when Payflow Verbosity paramter = MEDIUM
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>BATCHID</code>
        /// </remarks>
        public string BatchId { get; private set; }

        /// <summary>
        ///     Gets, Sets  AddlMsgs
        /// </summary>
        /// <remarks>
        ///     Additional error message that indicates that the
        ///     merchant used a feature that is disabled.
        ///     Value obtained when Payflow Verbosity paramter = MEDIUM
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ADDLMSGS</code>
        /// </remarks>
        public string AddlMsgs { get; private set; }

        /// <summary>
        ///     Gets, Sets  HostCode
        /// </summary>
        /// <remarks>
        ///     Response code returned by the processor. This
        ///     value is not normalized by PayPal.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>HOSTCODE</code>
        /// </remarks>
        public string HostCode { get; private set; }

        /// <summary>
        ///     Gets, Sets  ProcAVS
        /// </summary>
        /// <remarks>
        ///     AVS (Address Verification Service) response
        ///     from the processor.
        ///     Value obtained when Payflow Verbosity paramter = MEDIUM
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PROCAVS</code>
        /// </remarks>
        public string ProcAvs { get; private set; }

        /// <summary>
        ///     Gets, Sets  ProcCardSecure
        /// </summary>
        /// <remarks>
        ///     VPAS/SPA response from the processor.
        ///     Value obtained when Payflow Verbosity paramter = MEDIUM
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PROCCARDSECURE</code>
        /// </remarks>
        public string ProcCardSecure { get; private set; }

        /// <summary>
        ///     Gets, Sets  ProcCVV2
        /// </summary>
        /// <remarks>
        ///     CVV2 (buyer authentication) response from the processor.
        ///     Its a 3- or 4-digit code that is printed (not imprinted) on
        ///     the back of a credit card. Used as partial assurance
        ///     that the card is in the buyer’s possession.
        ///     Value obtained when Payflow Verbosity paramter = MEDIUM
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PROCCVV2</code>
        /// </remarks>
        public string ProcCvv2 { get; private set; }

        /// <summary>
        ///     Gets, Sets  RespText
        /// </summary>
        /// <remarks>
        ///     Text corresponding to the response code
        ///     returned by the processor. This text is not
        ///     normalized by Gateway server.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>RESPTEXT</code>
        /// </remarks>
        public string RespText { get; private set; }

        //Added as SETTLE_DATE param is also available when VERBOSITY= MEDIUM
        //2005-12-10
        /// <summary>
        ///     Gets inquiry SettleDate
        /// </summary>
        /// <remarks>
        ///     Date when the settlement is completed
        ///     Value obtained when Payflow Verbosity paramter = MEDIUM
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SETTLE_DATE</code>
        /// </remarks>
        public string SettleDate { get; private set; }

        /// <summary>
        ///     Gets the PPref parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PPREF</code>
        /// </remarks>
        public string PPref { get; private set; }

        /// <summary>
        ///     Gets the CorrelationId parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CORRELATIONID</code>
        /// </remarks>
        public string CorrelationId { get; private set; }

        /// <summary>
        ///     Gets the feeamt parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>FEEAMT</code>
        /// </remarks>
        public string FeeAmt { get; private set; }

        /// <summary>
        ///     Gets the PendingReason parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PENDINGREASON</code>
        /// </remarks>
        public string PendingReason { get; private set; }

        /// <summary>
        ///     Gets the PaymentType parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PAYMENTTYPE</code>
        /// </remarks>
        public string PaymentType { get; private set; }

        //Added as STATUS param is also available when VERBOSITY= MEDIUM
        //2006-09-18
        /// <summary>
        ///     Gets inquiry Status
        /// </summary>
        /// <remarks>
        ///     Status of transaction
        ///     Value obtained when Payflow Verbosity paramter = MEDIUM
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>STATUS</code>
        /// </remarks>
        public string Status { get; private set; }

        // Added BALAMT param 2007-05-31
        /// <summary>
        ///     Gets the BalAmt parameter
        /// </summary>
        /// <remarks>
        ///     American Express CAPN transactions only:
        ///     Balance on a pre-paid store value card. The value includes a decimal and
        ///     the exact amount to the cent (42.00, not 42).
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>BALAMT</code>
        /// </remarks>
        public string BalAmt { get; private set; }

        // Added AMEXID param 2007-05-31
        /// <summary>
        ///     Gets the AmexID parameter
        /// </summary>
        /// <remarks>
        ///     American Express CAPN transactions only:
        ///     Unique transaction ID returned when VERBOSITY = MEDIUM.
        ///     Used to track American Express CAPN transactions.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>AMEXID</code>
        /// </remarks>
        public string AmexId { get; private set; }

        // Added AMEXPOSDATA param 2007-11-05
        /// <summary>
        ///     Gets the AmexPosData parameter
        /// </summary>
        /// <remarks>
        ///     American Express CAPN transactions only:
        ///     Unique field returned when VERBOSITY = MEDIUM.
        ///     Used by merchants who authorize transactions through
        ///     the payflow gateway but settle through a third-party solution.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>AMEXPOSDATA</code>
        /// </remarks>
        public string AmexPosData { get; private set; }

        /// <summary>
        ///     Gets the TransTime parameter
        /// </summary>
        /// <remarks>
        ///     Returns the transaction time in the format of:
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>TRANSTIME</code>
        /// </remarks>
        public string TransTime { get; private set; }

        /// <summary>
        ///     Gets the CardType parameter
        /// </summary>
        /// <remarks>
        ///     Returns a value which represents the card type used.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CARDTYPE</code>
        /// </remarks>
        public string CardType { get; private set; }

        /// <summary>
        ///     Gets the OrigAmt parameter
        /// </summary>
        /// <remarks>
        ///     Returns the original amount sent for processing.  Used
        ///     with PARTIALAUTH parameter.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ORIGAMT</code>
        /// </remarks>
        public string OrigAmt { get; private set; }

        /// <summary>
        ///     Gets the Acct parameter
        /// </summary>
        /// <remarks>
        ///     Returns the last 4-digits of the credit card number used.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ACCT</code>
        /// </remarks>
        public string Acct { get; private set; }

        /// <summary>
        ///     Gets the LastName parameter
        /// </summary>
        /// <remarks>
        ///     Returns the last name.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>LASTNAME</code>
        /// </remarks>
        public string LastName { get; private set; }

        /// <summary>
        ///     Gets the FirstName parameter
        /// </summary>
        /// <remarks>
        ///     Returns the first name.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>FIRSTNAME</code>
        /// </remarks>
        public string FirstName { get; private set; }

        /// <summary>
        ///     Gets the amt parameter
        /// </summary>
        /// <remarks>
        ///     Returns the amount of the transaction that was
        ///     authorized.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>AMT</code>
        /// </remarks>
        public string Amt { get; private set; }

        /// <summary>
        ///     Gets the ExpDate parameter
        /// </summary>
        /// <remarks>
        ///     Returns the expiration date of the credit card used.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>EXPDATE</code>
        /// </remarks>
        public string ExpDate { get; private set; }

        /// <summary>
        ///     Gets the ExtRspMsg parameter
        /// </summary>
        /// <remarks>
        ///     Returns additional (extra) response messages from processor.
        ///     Not supported by all processors.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>EXTRSPMSG</code>
        /// </remarks>
        public string ExtRspMsg { get; private set; }

        /// <summary>
        ///     Gets the SecureToken parameter
        /// </summary>
        /// <remarks>
        ///     Returns the secure token that was sent in the original transaction.
        ///     Used with secure token id to call the hosted payment pages.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SECURETOKEN</code>
        /// </remarks>
        public string SecureToken { get; private set; }

        /// <summary>
        ///     Gets the SecureTokenId parameter
        /// </summary>
        /// <remarks>
        ///     Returns the secure token id that was sent in the original transaction.
        ///     Used with secure token to call the hosted payment pages.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SECURETOKENID</code>
        /// </remarks>
        public string SecureTokenId { get; private set; }

        #endregion
    }
}