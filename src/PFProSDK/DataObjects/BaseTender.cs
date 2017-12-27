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
    ///     This abstract class serves as base class
    ///     of all tender objects.
    /// </summary>
    /// <remarks>
    ///     <para>Each tender type is associated with a Payment Device.</para>
    ///     <para>
    ///         Following are the Payment Devices associated with
    ///         different tender types:
    ///     </para>
    ///     <list type="table">
    ///         <listheader>
    ///             <term>Tender type</term>
    ///             <description>Payment Device Data Object</description>
    ///         </listheader>
    ///         <item>
    ///             <term>ACHTender</term>
    ///             <description>
    ///                 <see cref="BankAcct">BankAcct Class</see>
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>CardTender</term>
    ///             <description>
    ///                 <para>
    ///                     <see cref="CreditCard">CreditCard Class</see>
    ///                 </para>
    ///                 <para>
    ///                     <see cref="PurchaseCard">PurchaseCard Class</see>
    ///                 </para>
    ///                 <para>
    ///                     <see cref="SwipeCard">SwipeCard Class</see>
    ///                 </para>
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>CheckTender</term>
    ///             <description>
    ///                 <see cref="CheckPayment">CheckPayment Class</see>
    ///             </description>
    ///         </item>
    ///     </list>
    /// </remarks>
    public class BaseTender : BaseRequestDataObject
    {
        #region "Constructors"

        /// <summary>
        ///     Constructor for BaseTender.
        /// </summary>
        /// <param name="tender">Tender Type ("C"/"A"/"K")</param>
        /// <param name="payDevice">Payment Device</param>
        /// Abstract class. Instance cannot be created directly.
        public BaseTender(string tender, PaymentDevice payDevice)
        {
            Tender = tender;
            _mPaymentDevice = payDevice;
        }

        #endregion

        #region "Core functions"

        /// <summary>
        ///     Generates the transaction request.
        /// </summary>
        internal override void GenerateRequest()
        {
            try
            {
                if (_mPaymentDevice != null)
                {
                    _mPaymentDevice.RequestBuffer = RequestBuffer;
                    _mPaymentDevice.GenerateRequest();
                }

                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamTender, Tender));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamChknum, ChkNum));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamChktype, ChkType));
//				RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.PARAM_DLSTATE , mDlState));
//				RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.PARAM_CONSENTMEDIUM, mConsentMedium));
//				RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.PARAM_CUSTOMERTYPE, mCustomerType));
//				RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.PARAM_BANKNAME, mBankName));
//				RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.PARAM_BANKSTATE, mBankState));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamDl, Dl));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamSs, Ss));
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
        ///     Holds the associated
        ///     payment device.
        /// </summary>
        private readonly PaymentDevice _mPaymentDevice;

        /*/// <summary>
        /// Holds Dl State
        /// </summary>
        ///private String mDlState;
        
        /// <summary>
        /// Holds Consent medium
        /// </summary>
        ///private String mConsentMedium;
        
        /// <summary>
        /// Holds Customer type
        /// </summary>
        ///private String mCustomerType;
        
        /// <summary>
        /// Holds Bank Name
        /// </summary>
        ///private String mBankName;
        
        /// <summary>
        /// Holds Bank State
        /// </summary>
        ///private String mBankState;*/

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets, Sets Tender Type.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>TENDER</code>
        /// </remarks>
        protected string Tender { get; }

        /// <summary>
        ///     Gets, Sets Check number.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         For ACH - The check serial number.
        ///         Required for POP, ARC, and RCK.
        ///     </para>
        ///     <para>
        ///         For TeleCheck - Account holder’s next unused
        ///         (available) check number.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CHKNUM</code>
        /// </remarks>
        public virtual string ChkNum { get; set; }

        /// <summary>
        ///     Gets, Sets Check type.
        /// </summary>
        /// <remarks>
        ///     <para>Check type.</para>
        ///     <para>Allowed CheckTypes are:</para>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>CHKTYPE</term>
        ///             <description>Description</description>
        ///         </listheader>
        ///         <item>
        ///             <term>P</term>
        ///             <description>Personal.</description>
        ///         </item>
        ///         <item>
        ///             <term>C</term>
        ///             <description>Company.</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public virtual string ChkType { get; set; }

        /// <summary>
        ///     Gets, Sets DL
        /// </summary>
        /// <remarks>
        ///     <para> Driver’s license number.</para>
        ///     <para>Format: XXnnnnnnnn</para>
        ///     <para>XX = State Code, nnnnnnnn = DL Number</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>DL</code>
        /// </remarks>
        public virtual string Dl { get; set; }

        /// <summary>
        ///     Gets, Sets SS
        /// </summary>
        /// <remarks>
        ///     <para>Account holder’s social security number.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SS</code>
        /// </remarks>

        public virtual string Ss { get; set; }

        #endregion
    }
}