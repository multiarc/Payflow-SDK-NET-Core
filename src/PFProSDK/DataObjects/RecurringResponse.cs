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
using PFProSDK.Common.Exceptions;
using PFProSDK.Common.Utility;

#endregion

namespace PFProSDK.DataObjects
{
    /// <summary>
    ///     Container class for all the messages related to
    ///     recurring transactions.
    /// </summary>
    /// <remarks>
    ///     This class contains response messages specific to
    ///     the recurring transactions.
    /// </remarks>
    /// <example>
    ///     Following example shows how to obtain and use the recurring
    ///     response.
    ///     <code lang="C#" escaped="false">
    /// 		...................
    /// 		// Trans is the recurring transaction.
    /// 		...................
    /// 		// Submit the transaction.
    /// 		Response Resp = Trans.SubmitTransaction();
    /// 		
    /// 		if (Resp != null)
    /// 			{
    /// 			// Get the Transaction Response parameters.
    /// 			TransactionResponse TrxnResponse =  Resp.TransactionResponse;
    /// 			if (TrxnResponse != null)
    /// 			{
    /// 				Console.WriteLine("RESULT = " + TrxnResponse.Result);
    /// 				Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg);
    /// 			}
    /// 			// Get the Recurring Response parameters.
    /// 			RecurringResponse RecurResponse = Resp.RecurringResponse;
    /// 			if (RecurResponse != null)
    /// 			{
    /// 				Console.WriteLine("RPREF = " + RecurResponse.RPRef);
    /// 				Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId);
    /// 			}
    /// 		}
    /// 		
    /// 		...................
    ///  </code>
    ///     <code lang="Visual Basic" escaped="false">
    /// 	            ..........................
    /// 	            ' Trans is the transaction object
    /// 	            ..........................
    /// 	            ' Submit the transaction.
    /// 	            Dim Resp As Response = Trans.SubmitTransaction()
    /// 	            If Not Resp Is Nothing Then
    /// 	                ' Get the Transaction Response parameters.
    /// 	                Dim TrxnResponse As TransactionResponse = Resp.TransactionResponse
    /// 	                If Not TrxnResponse Is Nothing Then
    /// 	                    Console.WriteLine("RESULT = " + TrxnResponse.Result)
    /// 	                    Console.WriteLine("RESPMSG = " + TrxnResponse.RespMsg)
    /// 	                End If
    /// 	                ' Get the Recurring Response parameters.
    /// 	                Dim RecurResponse As RecurringResponse = Resp.RecurringResponse
    /// 	                If Not RecurResponse Is Nothing Then
    /// 	                    Console.WriteLine("RPREF = " + RecurResponse.RPRef)
    /// 	                    Console.WriteLine("PROFILEID = " + RecurResponse.ProfileId)
    /// 	                End If
    /// 	            End If
    /// 	            ..........................
    ///  </code>
    /// </example>
    public sealed class RecurringResponse : BaseResponseDataObject
    {
        #region "Constructors"

        /// <summary>
        ///     Constructor for RecurringResponse
        /// </summary>
        internal RecurringResponse()
        {
            InquiryParams = new Hashtable();
        }

        #endregion

        #region "Methods"

        /// <summary>
        ///     Sets Response params
        /// </summary>
        /// <param name="responseHashTable">Response Hash table by ref</param>
        internal void SetParams(ref Hashtable responseHashTable)
        {
            try
            {
                ProfileId = (string) responseHashTable[PayflowConstants.ParamProfileid];
                RpRef = (string) responseHashTable[PayflowConstants.ParamRpref];
                TrxPnRef = (string) responseHashTable[PayflowConstants.ParamTrxpnref];
                TrxResult = (string) responseHashTable[PayflowConstants.ParamTrxresult];
                TrxRespMsg = (string) responseHashTable[PayflowConstants.ParamTrxrespmsg];

                //Additional fields for Inquiry transaction
                ProfileName = (string) responseHashTable[PayflowConstants.ParamProfilename];
                Start = (string) responseHashTable[PayflowConstants.ParamStart];
                Term = (string) responseHashTable[PayflowConstants.ParamTerm];
                PayPeriod = (string) responseHashTable[PayflowConstants.ParamPayperiod];
                Status = (string) responseHashTable[PayflowConstants.ParamStatus];
                Tender = (string) responseHashTable[PayflowConstants.ParamTender];
                PaymentsLeft = (string) responseHashTable[PayflowConstants.ParamPaymentsleft];
                NextPayment = (string) responseHashTable[PayflowConstants.ParamNextpayment];
                End = (string) responseHashTable[PayflowConstants.ParamEnd];
                AggregateAmt = (string) responseHashTable[PayflowConstants.ParamAggregateamt];
                AggregateOptionalAmt = (string) responseHashTable[PayflowConstants.ParamAggregateoptionalamt];
                Amt = (string) responseHashTable[PayflowConstants.ParamAmt];
                Acct = (string) responseHashTable[PayflowConstants.ParamAcct];
                ExpDate = (string) responseHashTable[PayflowConstants.ParamExpdate];
                MaxFailPayments = (string) responseHashTable[PayflowConstants.ParamMaxfailpayments];
                NumFailPayments = (string) responseHashTable[PayflowConstants.ParamNumfailpayments];
                RetryNumDays = (string) responseHashTable[PayflowConstants.ParamRetrynumdays];
                Email = (string) responseHashTable[PayflowConstants.ParamEmail];
                CompanyName = (string) responseHashTable[PayflowConstants.ParamCompanyname];
                Name = (string) responseHashTable[PayflowConstants.ParamName];
                FirstName = (string) responseHashTable[PayflowConstants.ParamFirstname];
                MiddleName = (string) responseHashTable[PayflowConstants.ParamMiddlename];
                LastName = (string) responseHashTable[PayflowConstants.ParamLastname];
                Street = (string) responseHashTable[PayflowConstants.ParamStreet];
                City = (string) responseHashTable[PayflowConstants.ParamCity];
                State = (string) responseHashTable[PayflowConstants.ParamState];
                Zip = (string) responseHashTable[PayflowConstants.ParamZip];
                Country = (string) responseHashTable[PayflowConstants.ParamCountry];
                PhoneNum = (string) responseHashTable[PayflowConstants.ParamPhonenum];
                ShipToFirstName = (string) responseHashTable[PayflowConstants.ParamShiptofirstname];
                ShipToMiddleName = (string) responseHashTable[PayflowConstants.ParamShiptomiddlename];
                ShipToLastName = (string) responseHashTable[PayflowConstants.ParamShiptolastname];
                ShipToStreet = (string) responseHashTable[PayflowConstants.ParamShiptostreet];
                ShipToCity = (string) responseHashTable[PayflowConstants.ParamShiptocity];
                ShipToState = (string) responseHashTable[PayflowConstants.ParamShiptostate];
                ShipToZip = (string) responseHashTable[PayflowConstants.ParamShiptozip];
                ShipToCountry = (string) responseHashTable[PayflowConstants.ParamShiptocountry];


                responseHashTable.Remove(PayflowConstants.ParamProfileid);
                responseHashTable.Remove(PayflowConstants.ParamRpref);
                responseHashTable.Remove(PayflowConstants.ParamTrxpnref);
                responseHashTable.Remove(PayflowConstants.ParamTrxresult);
                responseHashTable.Remove(PayflowConstants.ParamTrxrespmsg);
                responseHashTable.Remove(PayflowConstants.ParamProfilename);
                responseHashTable.Remove(PayflowConstants.ParamStart);
                responseHashTable.Remove(PayflowConstants.ParamTerm);
                responseHashTable.Remove(PayflowConstants.ParamPayperiod);
                responseHashTable.Remove(PayflowConstants.ParamStatus);
                responseHashTable.Remove(PayflowConstants.ParamTender);
                responseHashTable.Remove(PayflowConstants.ParamPaymentsleft);
                responseHashTable.Remove(PayflowConstants.ParamNextpayment);
                responseHashTable.Remove(PayflowConstants.ParamEnd);
                responseHashTable.Remove(PayflowConstants.ParamAggregateamt);
                responseHashTable.Remove(PayflowConstants.ParamAggregateoptionalamt);
                responseHashTable.Remove(PayflowConstants.ParamAmt);
                responseHashTable.Remove(PayflowConstants.ParamAcct);
                responseHashTable.Remove(PayflowConstants.ParamExpdate);
                responseHashTable.Remove(PayflowConstants.ParamMaxfailpayments);
                responseHashTable.Remove(PayflowConstants.ParamNumfailpayments);
                responseHashTable.Remove(PayflowConstants.ParamRetrynumdays);
                responseHashTable.Remove(PayflowConstants.ParamEmail);
                responseHashTable.Remove(PayflowConstants.ParamCompanyname);
                responseHashTable.Remove(PayflowConstants.ParamName);
                responseHashTable.Remove(PayflowConstants.ParamFirstname);
                responseHashTable.Remove(PayflowConstants.ParamMiddlename);
                responseHashTable.Remove(PayflowConstants.ParamLastname);
                responseHashTable.Remove(PayflowConstants.ParamStreet);
                responseHashTable.Remove(PayflowConstants.ParamCity);
                responseHashTable.Remove(PayflowConstants.ParamState);
                responseHashTable.Remove(PayflowConstants.ParamZip);
                responseHashTable.Remove(PayflowConstants.ParamCountry);
                responseHashTable.Remove(PayflowConstants.ParamPhonenum);
                responseHashTable.Remove(PayflowConstants.ParamShiptofirstname);
                responseHashTable.Remove(PayflowConstants.ParamShiptomiddlename);
                responseHashTable.Remove(PayflowConstants.ParamShiptolastname);
                responseHashTable.Remove(PayflowConstants.ParamShiptostreet);
                responseHashTable.Remove(PayflowConstants.ParamShiptocity);
                responseHashTable.Remove(PayflowConstants.ParamShiptostate);
                responseHashTable.Remove(PayflowConstants.ParamShiptozip);
                responseHashTable.Remove(PayflowConstants.ParamShiptocountry);
                responseHashTable.Remove(PayflowConstants.ParamPResulTn);
                responseHashTable.Remove(PayflowConstants.ParamPPnreFn);
                responseHashTable.Remove(PayflowConstants.ParamPTranstatEn);
                responseHashTable.Remove(PayflowConstants.ParamPTendeRn);
                responseHashTable.Remove(PayflowConstants.ParamPTranstimEn);
                responseHashTable.Remove(PayflowConstants.ParamPAmTn);
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

        #endregion

        #region "Member variables"

        //Additional fields for Inquiry transaction

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets ProfileId
        /// </summary>
        /// <remarks>
        ///     The Profile ID of the original profile.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PROFILEID</code>
        /// </remarks>
        public string ProfileId { get; private set; }

        /// <summary>
        ///     Gets RPRef
        /// </summary>
        /// <remarks>
        ///     Reference number to this particular action request.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>RPREF</code>
        /// </remarks>
        public string RpRef { get; private set; }

        /// <summary>
        ///     Gets TrxPNRef
        /// </summary>
        /// <remarks>
        ///     PNREF of the optional transaction.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>TRXPNREF</code>
        /// </remarks>
        public string TrxPnRef { get; private set; }

        /// <summary>
        ///     Gets TrxResult
        /// </summary>
        /// <remarks>
        ///     RESULT of the optional transaction.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>TRXRESULT</code>
        /// </remarks>
        public string TrxResult { get; private set; }

        /// <summary>
        ///     Gets TrxRespMsg
        /// </summary>
        /// <remarks>
        ///     RESPMSG of the optional transaction
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>TRXRESPMSG</code>
        /// </remarks>
        public string TrxRespMsg { get; private set; }

        //Additional fields for Inquiry transaction
        /// <summary>
        ///     Gets ProfileName
        /// </summary>
        /// <remarks>
        ///     Name for the profile.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PROFILENAME</code>
        /// </remarks>
        public string ProfileName { get; private set; }

        /// <summary>
        ///     Gets Start
        /// </summary>
        /// <remarks>
        ///     Beginning date for the recurring billing cycle.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>START</code>
        /// </remarks>
        public string Start { get; private set; }

        /// <summary>
        ///     Gets Term
        /// </summary>
        /// <remarks>
        ///     Number of payments to be made over the life of the agreement.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>TERM</code>
        /// </remarks>
        public string Term { get; private set; }

        /// <summary>
        ///     Gets PayPeriod
        /// </summary>
        /// <remarks>
        ///     Specifies how often the payment occurs.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PAYPERIOD</code>
        /// </remarks>
        public string PayPeriod { get; private set; }

        /// <summary>
        ///     Gets Status
        /// </summary>
        /// <remarks>
        ///     Current status of the profile.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>STATUS</code>
        /// </remarks>
        public string Status { get; private set; }

        /// <summary>
        ///     Gets TenderType
        /// </summary>
        /// <remarks>
        ///     Tender Type
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>TENDER</code>
        /// </remarks>
        public string Tender { get; private set; }

        /// <summary>
        ///     Gets PaymentsLeft
        /// </summary>
        /// <remarks>
        ///     Number of payments left to be billed.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PAYMENTSLEFT</code>
        /// </remarks>
        public string PaymentsLeft { get; private set; }

        /// <summary>
        ///     Gets NxtPayment
        /// </summary>
        /// <remarks>
        ///     Date that the next payment is due.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>NEXTPAYMENT</code>
        /// </remarks>
        public string NextPayment { get; private set; }

        /// <summary>
        ///     Gets End
        /// </summary>
        /// <remarks>
        ///     Date that the last payment is due. Present only if this is
        ///     not an unlimited-term subscription.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>END</code>
        /// </remarks>
        public string End { get; private set; }

        /// <summary>
        ///     Gets AggregateAmt
        /// </summary>
        /// <remarks>
        ///     Amount collected so far for scheduled payments.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>AGGREGATEAMT</code>
        /// </remarks>
        public string AggregateAmt { get; private set; }

        /// <summary>
        ///     Gets AggregateOptAmt
        /// </summary>
        /// <remarks>
        ///     Amount collected through sending optional transactions.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>AGGREGATEOPTIONALAMT</code>
        /// </remarks>
        public string AggregateOptionalAmt { get; private set; }

        /// <summary>
        ///     Gets Amt
        /// </summary>
        /// <remarks>
        ///     Base dollar amount to be billed.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>AMT</code>
        /// </remarks>
        public string Amt { get; private set; }

        /// <summary>
        ///     Gets Acct
        /// </summary>
        /// <remarks>
        ///     Masked credit card number.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ACCT</code>
        /// </remarks>
        public string Acct { get; private set; }

        /// <summary>
        ///     Gets ExpDate
        /// </summary>
        /// <remarks>
        ///     Expiration date of the credit card account.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>EXPDATE</code>
        /// </remarks>
        public string ExpDate { get; private set; }

        /// <summary>
        ///     Gets MaxFailPayments
        /// </summary>
        /// <remarks>
        ///     The number of payment periods (specified by
        ///     PAYPERIOD) for which the transaction is allowed to fail
        ///     before PayPal cancels a profile.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>MAXFAILPAYMENTS</code>
        /// </remarks>
        public string MaxFailPayments { get; private set; }

        /// <summary>
        ///     Gets NumFailPayments
        /// </summary>
        /// <remarks>
        ///     Number of payments that failed.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>NUMFAILPAYMENTS</code>
        /// </remarks>
        public string NumFailPayments { get; private set; }

        /// <summary>
        ///     Gets RetryNumDays
        /// </summary>
        /// <remarks>
        ///     The number of consecutive days that PayPal should
        ///     attempt to process a failed transaction until Approved
        ///     status is received.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>RETRYNUMDAYS</code>
        /// </remarks>
        public string RetryNumDays { get; private set; }

        /// <summary>
        ///     Gets Email
        /// </summary>
        /// <remarks>
        ///     Customer e-mail address.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>EMAIL</code>
        /// </remarks>
        public string Email { get; private set; }

        /// <summary>
        ///     Gets CompanyName
        /// </summary>
        /// <remarks>
        ///     Recurring Profile Company Name.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>COMPANYNAME</code>
        /// </remarks>
        public string CompanyName { get; private set; }

        /// <summary>
        ///     Gets Name
        /// </summary>
        /// <remarks>
        ///     Name of account holder
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>NAME</code>
        /// </remarks>
        public string Name { get; private set; }

        /// <summary>
        ///     Gets FirstName
        /// </summary>
        /// <remarks>
        ///     First name of card holder.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>FIRSTNAME</code>
        /// </remarks>
        public string FirstName { get; private set; }

        /// <summary>
        ///     Gets MiddleName
        /// </summary>
        /// <remarks>
        ///     Middle name of card holder
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>MIDDLENAME</code>
        /// </remarks>
        public string MiddleName { get; private set; }

        /// <summary>
        ///     Gets Lastname
        /// </summary>
        /// <remarks>
        ///     Last name of card holder
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>LASTNAME</code>
        /// </remarks>
        public string LastName { get; private set; }

        /// <summary>
        ///     Gets Street
        /// </summary>
        /// <remarks>
        ///     Billing address
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>STREET</code>
        /// </remarks>
        public string Street { get; private set; }

        /// <summary>
        ///     Gets City
        /// </summary>
        /// <remarks>
        ///     Billing city
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CITY</code>
        /// </remarks>
        public string City { get; private set; }

        /// <summary>
        ///     Gets State
        /// </summary>
        /// <remarks>
        ///     Billing state
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>STATE</code>
        /// </remarks>
        public string State { get; private set; }

        /// <summary>
        ///     Gets Zip
        /// </summary>
        /// <remarks>
        ///     Billing zip
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ZIP</code>
        /// </remarks>
        public string Zip { get; private set; }

        /// <summary>
        ///     Gets Country
        /// </summary>
        /// <remarks>
        ///     Billing country
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>COUNTRY</code>
        /// </remarks>
        public string Country { get; private set; }

        /// <summary>
        ///     Gets PhoneNum
        /// </summary>
        /// <remarks>
        ///     Billing phonenum
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PHONENUM</code>
        /// </remarks>
        public string PhoneNum { get; private set; }

        /// <summary>
        ///     Gets ShipToFirstName
        /// </summary>
        /// <remarks>
        ///     First name of the ship-to person
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPTOFIRSTNAME</code>
        /// </remarks>
        public string ShipToFirstName { get; private set; }

        /// <summary>
        ///     Gets ShipToMiddleName
        /// </summary>
        /// <remarks>
        ///     Middle name of the ship-to person
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPTOMIDDLENAME</code>
        /// </remarks>
        public string ShipToMiddleName { get; private set; }

        /// <summary>
        ///     Gets ShipToLastName
        /// </summary>
        /// <remarks>
        ///     Last name of the ship-to person
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPTOLASTNAME</code>
        /// </remarks>
        public string ShipToLastName { get; private set; }

        /// <summary>
        ///     Gets ShipToStreet
        /// </summary>
        /// <remarks>
        ///     Shipping street
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPTOSTREET</code>
        /// </remarks>
        public string ShipToStreet { get; private set; }

        /// <summary>
        ///     Gets ShipToCity
        /// </summary>
        /// <remarks>
        ///     Shipping city
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPTOCITY</code>
        /// </remarks>
        public string ShipToCity { get; private set; }

        /// <summary>
        ///     Gets ShipToState
        /// </summary>
        /// <remarks>
        ///     Shipping state
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPTOSTATE</code>
        /// </remarks>
        public string ShipToState { get; private set; }

        /// <summary>
        ///     Gets ShipToZip
        /// </summary>
        /// <remarks>
        ///     Shipping zip
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPTOZIP</code>
        /// </remarks>
        public string ShipToZip { get; private set; }

        /// <summary>
        ///     Gets ShipToCountry
        /// </summary>
        /// <remarks>
        ///     Shipping country
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPTOCOUNTRY</code>
        /// </remarks>
        public string ShipToCountry { get; private set; }


        /// <summary>
        ///     Gets recurring inquiry
        ///     param hash table
        /// </summary>
        /// <remarks>
        ///     This hash table contains the response messages
        ///     when the recurring transaction is with
        ///     PAYMENTHISTORY=Y
        ///     <para>Maps to following Payflow Parameters:</para>
        ///     <code>
        /// <list type="table">
        ///             <listheader>
        ///                 <term>Payflow param</term>
        ///                 <description>Description</description>
        ///             </listheader>
        ///             <item>
        ///                 <term>P_RESULTn</term>
        ///                 <description>Result code of the financial transaction.</description>
        ///             </item>
        ///             <item>
        ///                 <term>P_PNREFn</term>
        ///                 <description>PNREF of the particular payment.</description>
        ///             </item>
        ///             <item>
        ///                 <term>P_TRANSTATEn</term>
        ///                 <description>TRANS_STATE of the particular payment.</description>
        ///             </item>
        ///             <item>
        ///                 <term>P_TENDERn</term>
        ///                 <description>Tender type</description>
        ///             </item>
        ///             <item>
        ///                 <term>P_TRANSTIMEn</term>
        ///                 <description>The timestamp for the transaction in the dd-mmm-yy hh:mm AM/PM format.</description>
        ///             </item>
        ///             <item>
        ///                 <term>P_AMTn</term>
        ///                 <description>
        ///                     Dollar amount (US dollars) that was billed. Specifies dollars and cents using a decimal
        ///                     point.
        ///                 </description>
        ///             </item>
        ///         </list>
        /// </code>
        /// </remarks>
        public Hashtable InquiryParams { get; }

        #endregion
    }
}