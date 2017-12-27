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
    ///     Used for ExpressCheckout with Billing Agreement (Reference Transaction) without Purchase SET operation.
    /// </summary>
    /// <remarks>
    ///     <seealso cref="EcGetBaRequest" />
    ///     <seealso cref="EcdoBaRequest" />
    /// </remarks>
    public class EcSetBaRequest : EcSetRequest
    {
        #region "Constructor"

        /// <summary>
        ///     Constructor for ECSetBARequest
        /// </summary>
        /// <param name="returnUrl">String</param>
        /// <param name="cancelUrl">String</param>
        /// <param name="billingType">String</param>
        /// <param name="baDesc">String</param>
        /// <param name="paymentType">String</param>
        /// <param name="baCustom">String</param>
        /// <remarks>
        ///     ECSetBARequest is used to set the data required for a Express Checkout Billing Agreement SET operation
        ///     with Billing Agreement (Reference Transaction) without Purchase.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		
        /// 		//Create the ECSetBARequest object
        /// 		ECSetBARequest SetEC = new ECSetBARequest(ReturnUrl, CancelUrl, BillingType, BA_Desc, PaymentType, BA_Custom);
        /// 		
        /// 		.............
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		.............
        /// 		
        /// 		//Create the ECSetBARequest object
        /// 		Dim SetEC As ECSetBARequest = new ECSetBARequest(ReturnUrl, CancelUrl, BillingType, BA_Desc, PaymentType, BA_Custom)
        /// 		
        /// 		.............
        ///  </code>
        /// </example>
        public EcSetBaRequest(string returnUrl, string cancelUrl, string billingType, string baDesc, string paymentType,
            string baCustom)
            : base(returnUrl, cancelUrl, billingType, baDesc, paymentType, baCustom, PayflowConstants.ActiontypeSetba)
        {
        }

        #endregion
    }
}