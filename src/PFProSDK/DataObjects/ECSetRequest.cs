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

#endregion

namespace PFProSDK.DataObjects
{
    /// <summary>
    ///     Used for ExpressCheckout SET operation.
    /// </summary>
    /// <remarks>
    ///     <seealso cref="EcGetRequest" />
    ///     <seealso cref="EcDoRequest" />
    /// </remarks>
    public class EcSetRequest : ExpressCheckoutRequest
    {
        #region "Member variables"

        // Transaction PayLater object. Has parameters like PromoCode, ProductCategory, etc.
        private readonly PayLater _mPayLater;

        #endregion

        #region "Core functions"

        /// <summary>
        ///     Generates the transaction request.
        /// </summary>
        internal override void GenerateRequest()
        {
            // This function is not called. All the
            // address information is validated and generated
            // in its respective derived classes.
            base.GenerateRequest();

            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamReturnurl, ReturnUrl));
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamCancelurl, CancelUrl));
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamReqconfirmshipping,
                ReqConfirmShipping));
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamReqbillingaddress,
                ReqBillingAddress));
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamNoshipping, NoShipping));
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamAddroverride, AddrOverride));
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLocalecode, LocaleCode));
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamMaxamt, MaxAmt));
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamPagestyle, PageStyle));
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamHdrimg, HeaderImage));
            RequestBuffer.Append(
                PayflowUtility.AppendToRequest(PayflowConstants.ParamHdrbordercolor, HeaderBorderColor));
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamHdrbackcolor, HeaderBackColor));
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamPayflowcolor, PayFlowColor));
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamBillingtype, BillingType));
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamBaDesc, BaDesc));
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamPaymenttype, PaymentType));
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamBaCustom, BaCustom));
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamShiptoname, ShipToName));
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamAllownote, AllowNote));

            if (_mPayLater != null)
            {
                _mPayLater.RequestBuffer = RequestBuffer;
                _mPayLater.GenerateRequest();
                ;
            }
        }

        #endregion

        #region "Constructor"

        /// <summary>
        ///     Constructor for ECSetRequest
        /// </summary>
        /// <param name="returnUrl">String</param>
        /// <param name="cancelUrl">String</param>
        /// <remarks>
        ///     ECSetRequest is used to set the data required for a Express Checkout SET operation.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		
        /// 		//Create the ECSetrequest object
        /// 		ECSetRequest SetEC = new ECSetRequest("http://www.yourwebsitereturnurl.com","http://www.yourwebsitecancelurl.com");
        /// 		
        /// 		.............
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		.............
        /// 		
        /// 		//Create the ECSetrequest object
        /// 		Dim SetEC As ECSetRequest = new ECSetRequest("http://www.yourwebsitereturnurl.com","http://www.yourwebsitecancelurl.com")
        /// 		
        /// 		.............
        ///  </code>
        /// </example>
        public EcSetRequest(string returnUrl, string cancelUrl)
            : base(PayflowConstants.ActiontypeSet)
        {
            ReturnUrl = returnUrl;
            CancelUrl = cancelUrl;
        }

        /// <summary>
        ///     Constructor for ECSetRequest
        /// </summary>
        /// <param name="returnUrl">String</param>
        /// <param name="cancelUrl">String</param>
        /// <param name="payLater">String</param>
        /// <remarks>
        ///     ECSetRequest is used to set the data required for a Express Checkout SET operation.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		
        /// 		//Create the ECSetrequest object
        /// 		ECSetRequest SetEC = new ECSetRequest("http://www.yourwebsitereturnurl.com", "http://www.yourwebsitecancelurl.com", PayLater);
        /// 		
        /// 		.............
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		.............
        /// 		
        /// 		//Create the ECSetrequest object
        /// 		Dim SetEC As ECSetRequest = new ECSetRequest("http://www.yourwebsitereturnurl.com", "http://www.yourwebsitecancelurl.com", PayLater)
        /// 		
        /// 		.............
        ///  </code>
        /// </example>
        public EcSetRequest(string returnUrl, string cancelUrl, PayLater payLater)
            : base(PayflowConstants.ActiontypeSet)
        {
            ReturnUrl = returnUrl;
            CancelUrl = cancelUrl;
            _mPayLater = payLater;
        }

        /// <summary>
        ///     Constructor for ECSetRequest
        /// </summary>
        /// <param name="returnUrl">String</param>
        /// <param name="cancelUrl">String</param>
        /// <param name="billingType">String</param>
        /// <param name="baDesc">String</param>
        /// <param name="paymentType">String</param>
        /// <param name="baCustom">String</param>
        /// <remarks>
        ///     ECSetRequest is used to set the data required for a Express Checkout SET operation.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		
        /// 		//Create the ECSetRequest object.
        /// 		ECSetRequest SetEC = new ECSetRequest("http://www.yourwebsitereturnurl.com","http://www.yourwebsitecancelurl.com",
        /// 		"MerchantInitiatedBilling", "Test Transaction", "any", "Something");
        /// 		
        /// 		.............
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		.............
        /// 		
        /// 		//Create the ECSetRequest object for Reference Transaction with Purchase.
        /// 		Dim SetEC As ECSetRequest = new ECSetRequest("http://www.yourwebsitereturnurl.com","http://www.yourwebsitecancelurl.com"
        /// 		"MerchantInitiatedBilling", "Test Transaction", "any", "Something")
        /// 		
        /// 		.............
        ///  </code>
        /// </example>
        public EcSetRequest(string returnUrl, string cancelUrl, string billingType, string baDesc, string paymentType,
            string baCustom)
            : base(PayflowConstants.ActiontypeSet)
        {
            ReturnUrl = returnUrl;
            CancelUrl = cancelUrl;
            BillingType = billingType;
            BaDesc = baDesc;
            PaymentType = paymentType;
            BaCustom = baCustom;
        }

        protected EcSetRequest(string returnUrl, string cancelUrl, string billingType, string baDesc,
            string paymentType, string baCustom, string action)
            : base(PayflowConstants.ActiontypeSetba)
        {
            ReturnUrl = returnUrl;
            CancelUrl = cancelUrl;
            BillingType = billingType;
            BaDesc = baDesc;
            PaymentType = paymentType;
            BaCustom = baCustom;
        }

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets or Sets the returnurl.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>RETURNURL</code>
        /// </remarks>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     Gets or Sets the cancelurl.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CANCELURL</code>
        /// </remarks>
        public string CancelUrl { get; set; }

        /// <summary>
        ///     Gets or Sets the ReqConfirmShipping parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>REQCONFIRMSHIPPING</code>
        /// </remarks>
        public string ReqConfirmShipping { get; set; }

        /// <summary>
        ///     Gets or Sets the ReqBillingAddress parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>REQBILLINGADDRESS</code>
        /// </remarks>
        public string ReqBillingAddress { get; set; }

        /// <summary>
        ///     Gets or Sets the NoShipping parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>NOSHIPPING</code>
        /// </remarks>
        public string NoShipping { get; set; }

        /// <summary>
        ///     Gets or Sets the AddrOveride Parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ADDROVERRIDE</code>
        /// </remarks>
        public string AddrOverride { get; set; }

        /// <summary>
        ///     Gets or Sets the LocaleCode Parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>LOCALECODE</code>
        /// </remarks>
        public string LocaleCode { get; set; }

        /// <summary>
        ///     Gets or Sets the MaxAmt Parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>MAXAMT</code>
        /// </remarks>
        public Currency MaxAmt { get; set; }

        /// <summary>
        ///     Gets or Sets the PageStyle Parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PAGESTYLE</code>
        /// </remarks>
        public string PageStyle { get; set; }

        /// <summary>
        ///     Gets or Sets the HdrImg Parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>cpp-header-image</code>
        /// </remarks>
        public string HeaderImage { get; set; }

        /// <summary>
        ///     Gets or Sets the HdrBorderColor Parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>cpp-header-border-color</code>
        /// </remarks>
        public string HeaderBorderColor { get; set; }

        /// <summary>
        ///     Gets or Sets the HdrBackColor Parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>cpp-header-back-color</code>
        /// </remarks>
        public string HeaderBackColor { get; set; }

        /// <summary>
        ///     Gets or Sets the PayFlowColor Parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>cpp-paflow-color</code>
        /// </remarks>
        public string PayFlowColor { get; set; }

        /// <summary>
        ///     Gets or Sets the Billing Type Parameter.
        /// </summary>
        /// <remarks>
        ///     Sets up automated recurring billing for the customer.  The
        ///     value is MerchantInitiatedBilling.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>BILLINGTYPE</code>
        /// </remarks>
        public string BillingType { get; set; }

        /// <summary>
        ///     Gets or Sets the Description Parameter.
        /// </summary>
        /// <remarks>
        ///     Description of goods or services associated with the
        ///     billing agreement.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>BA_DESC</code>
        /// </remarks>
        public string BaDesc { get; set; }

        /// <summary>
        ///     Gets or Sets the Payment Type Parameter.
        /// </summary>
        /// <remarks>
        ///     Type of payment you require.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PAYMENTTYPE</code>
        /// </remarks>
        public string PaymentType { get; set; }

        /// <summary>
        ///     Gets or Sets the Custom field Parameter.
        /// </summary>
        /// <remarks>
        ///     Custom annotation field for your exclusive use.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>BA_CUSTOM</code>
        /// </remarks>
        public string BaCustom { get; set; }

        /// <summary>
        ///     Gets or Sets the Ship to Name Parameter.
        /// </summary>
        /// <remarks>
        ///     Custom annotation field for your exclusive use.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPTONAME</code>
        /// </remarks>
        public string ShipToName { get; set; }

        /// <summary>
        ///     Gets or Sets the Allow Note Parameter.
        /// </summary>
        /// <remarks>
        ///     Custom annotation field for your exclusive use.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ALLOWNOTE</code>
        /// </remarks>
        public string AllowNote { get; set; }

        #endregion
    }
}