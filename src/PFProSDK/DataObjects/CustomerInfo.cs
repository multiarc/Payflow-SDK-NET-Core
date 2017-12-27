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

#endregion

namespace PFProSDK.DataObjects
{
    /// <summary>
    ///     Used for Customer related information.
    /// </summary>
    /// <remarks>
    ///     Use this class to set the customer related
    ///     information.
    /// </remarks>
    /// <example>
    ///     <code lang="C#" escaped="false">
    /// 	.................
    /// 	// Inv is the Invoice object
    /// 	.................
    /// 	// Set the Customer Info details.
    /// 	CustomerInfo Cust = New CustomerInfo();
    /// 	Cust.CustCode = "CustXXXXX";
    /// 	Cust.CustIP = "255.255.255.255";
    /// 	Inv.CustomerInfo = Cust;
    /// 	.................
    /// 	</code>
    ///     <code lang="Visual Basic" escaped="false">
    /// 	.................
    /// 	' Inv is the Invoice object
    /// 	.................
    /// 	' Set the Customer Info details.
    /// 	Dim Cust As CustomerInfo = New CustomerInfo
    /// 	Cust.CustCode = "CustXXXXX"
    /// 	Cust.CustIP = "255.255.255.255"
    /// 	Inv.CustomerInfo = Cust
    /// 	.................
    /// 	</code>
    /// </example>
    public sealed class CustomerInfo : BaseRequestDataObject
    {
        #region "Core functions"

        /// <summary>
        ///     Generates the transaction request.
        /// </summary>
        internal override void GenerateRequest()
        {
            try
            {
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamReqname, ReqName));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamCustcode, CustCode));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamCustip, _mCustIp));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamCustvatregnum,
                    _mCustVatRegNum));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamDob, _mDob));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamCustid, _mCustId));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamCusthostname,
                    _mCustHostName));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamCustbrowser, _mCustBrowser));

                // 04/07/07 Moved CompanyName to BillTo class.
                // Removed 04/07/07
                // RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.PARAM_CORPNAME, mCorpName));
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

        #region "Member Variables"

        /// <summary>
        ///     Holds customer host name
        /// </summary>
        private string _mCustHostName;

        /// <summary>
        ///     Holds customer browser
        /// </summary>
        private string _mCustBrowser;

        /// <summary>
        ///     Holds customer IP
        /// </summary>
        private string _mCustIp;

        /// <summary>
        ///     Holds Customer Vat registration number
        /// </summary>
        private string _mCustVatRegNum;

        /// <summary>
        ///     Holds Customer's date of birth
        /// </summary>
        private string _mDob;

        /// <summary>
        ///     Holds customer id
        /// </summary>
        private string _mCustId;

        // <summary>
        // Holds corporate name
        // </summary>
        // Removed 04/07/07 
        // private String mCorpName;

        /// <summary>
        ///     Holds MerchantName
        /// </summary>
        private string _mMerchantName;

        /// <summary>
        ///     Holds MerchantStreet
        /// </summary>
        private string _mMerchantStreet;

        /// <summary>
        ///     Holds MerchantCity
        /// </summary>
        private string _mMerchantCity;

        /// <summary>
        ///     Holds MerchantState
        /// </summary>
        private string _mMerchantState;

        /// <summary>
        ///     Holds MerchantCountryCode
        /// </summary>
        private string _mMerchantCountryCode;

        /// <summary>
        ///     Holds MerchantZip
        /// </summary>
        private string _mMerchantZip;

        #endregion

        #region "Constructor"

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets, Sets ReqName.
        /// </summary>
        /// <remarks>
        ///     <para>Requester Name.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>REQNAME</code>
        /// </remarks>
        public string ReqName { get; set; }

        /// <summary>
        ///     Gets, Sets  CustCode.
        /// </summary>
        /// <remarks>
        ///     <para>Customer code/customer reference ID.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CUSTCODE</code>
        /// </remarks>
        public string CustCode { get; set; }

        /// <summary>
        ///     Gets, Sets  CustIP.
        /// </summary>
        /// <remarks>
        ///     <para>Customer's IP address.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CUSTIP</code>
        /// </remarks>
        public string CustIp
        {
            get => _mCustIp;
            set => _mCustIp = value;
        }

        /// <summary>
        ///     Gets, Sets  CustHostName.
        /// </summary>
        /// <remarks>
        ///     <para>Customer's name of server that the account holder is connected to.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CUSTHOSTNAME</code>
        /// </remarks>
        public string CustHostName
        {
            get => _mCustHostName;
            set => _mCustHostName = value;
        }

        /// <summary>
        ///     Gets, Sets  CustBrowser.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Account holder’s HTTP browser type. Example:
        ///         MOZILLA/4.0~(COMPATIBLE;~MSIE~5.0;~WINDOWS~95)
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CUSTBROWSER</code>
        /// </remarks>
        public string CustBrowser
        {
            get => _mCustBrowser;
            set => _mCustBrowser = value;
        }

        /// <summary>
        ///     Gets, Sets  CustVatRegNum.
        /// </summary>
        /// <remarks>
        ///     <para>Customer's VAT registrations number.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CUSTVATREGNUM</code>
        /// </remarks>
        public string CustVatRegNum
        {
            get => _mCustVatRegNum;
            set => _mCustVatRegNum = value;
        }

        /// <summary>
        ///     Gets, Sets  DOB.
        /// </summary>
        /// <remarks>
        ///     <para>Account holder’s date of birth.</para>
        ///     <para>Format: mmddyyyy.</para>
        ///     <para>mm - Month, dd - Day, yy - Year.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>DOB</code>
        /// </remarks>
        public string Dob
        {
            get => _mDob;
            set => _mDob = value;
        }


        /// <summary>
        ///     Gets, Sets  CustId.
        /// </summary>
        /// <remarks>
        ///     <para>Customer's Id.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CUSTID</code>
        /// </remarks>
        public string CustId
        {
            get => _mCustId;
            set => _mCustId = value;
        }

        /// <summary>
        ///     Gets, Sets  MerchantName.
        /// </summary>
        /// <remarks>
        ///     <para>Name of Merchant</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>MERCHANTNAME</code>
        /// </remarks>
        public string MerchantName
        {
            get => _mMerchantName;
            set => _mMerchantName = value;
        }

        /// <summary>
        ///     Gets, Sets  MerchantStreet.
        /// </summary>
        /// <remarks>
        ///     <para>Merchant's Stree Address (Number and Street Name)</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>MERCHANTSTREET</code>
        /// </remarks>
        public string MerchantStreet
        {
            get => _mMerchantStreet;
            set => _mMerchantStreet = value;
        }

        /// <summary>
        ///     Gets, Sets  MerchantCity.
        /// </summary>
        /// <remarks>
        ///     <para>Merchant's City</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>MERCHANTCITY</code>
        /// </remarks>
        public string MerchantCity
        {
            get => _mMerchantCity;
            set => _mMerchantCity = value;
        }

        /// <summary>
        ///     Gets, Sets  MerchantState.
        /// </summary>
        /// <remarks>
        ///     <para>Merchant's State</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>MERCHANTSTATE</code>
        /// </remarks>
        public string MerchantState
        {
            get => _mMerchantState;
            set => _mMerchantState = value;
        }

        /// <summary>
        ///     Gets, Sets  MerchantCountryCode.
        /// </summary>
        /// <remarks>
        ///     <para>Merchant's Numeric Country Code.  Example: USA = 840</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>MERCHANTCOUNTRYCODE</code>
        /// </remarks>
        public string MerchantCountryCode
        {
            get => _mMerchantCountryCode;
            set => _mMerchantCountryCode = value;
        }

        /// <summary>
        ///     Gets, Sets  MerchantZip.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Merchant's 5- to 9-digit ZIP (postal) code excluding
        ///         spaces, dashes, and non-numeric characters.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>MERCHANTZIP</code>
        /// </remarks>
        public string MerchantZip
        {
            get => _mMerchantZip;
            set => _mMerchantZip = value;
        }

        // <summary>
        // Gets, Sets  CorpName.
        // </summary>
        // <remarks>
        // <para>Corporation name.</para>
        // <para>Maps to Payflow Parameter:</para>
        // <code>CORPNAME</code>
        // </remarks>
        //public String CorpName
        //{
        //	get { return mCorpName; }
        //	set { mCorpName = value; }
        //}

        // 04/07/07 - Moved Company Name to BillTo class.

        // 04/07/07 - Moved MerchDescr and MerchSvc to Invoice class.

        #endregion
    }
}