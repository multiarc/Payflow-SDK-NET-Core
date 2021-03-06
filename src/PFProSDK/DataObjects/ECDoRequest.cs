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
    ///     Used for ExpressCheckout DO operation.
    /// </summary>
    /// <remarks>
    ///     <seealso cref="EcSetRequest" />
    ///     <seealso cref="EcGetRequest" />
    /// </remarks>
    public class EcDoRequest : ExpressCheckoutRequest
    {
        #region "Properties"

        /// <summary>
        ///     Gets or Sets the payerid parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PAYERID</code>
        /// </remarks>
        public string PayerId { get; set; }

        #endregion

        #region "Core functions"

        /// <summary>
        ///     Generates the transaction request.
        /// </summary>
        internal override void GenerateRequest()
        {
            // This function is not called. All the
            //address information is validated and generated
            //in its respective derived classes.
            base.GenerateRequest();
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamPayerid, PayerId));
        }

        #endregion

        #region "Member Variables"

        #endregion

        #region "Constructors"

        /// <summary>
        ///     Constructor for ECDoRequest
        /// </summary>
        /// <param name="token">String</param>
        /// <param name="payerId">String</param>
        /// <remarks>
        ///     ECDoRequest is used to set the data required for a Express Checkout DO operation.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		
        /// 		//Create the ECDoRequest object
        /// 		ECDoRequest DoEC = new ECDoRequest("[tokenid]","[payerid]");
        /// 		
        /// 		.............
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		.............
        /// 		
        /// 		//Create the ECDoRequest object
        /// 		Dim DoEC As ECDoRequest = new ECDoRequest("[tokenid]","[payerid]")
        /// 		
        /// 		.............
        ///  </code>
        /// </example>
        public EcDoRequest(string token, string payerId) : base(PayflowConstants.ActiontypeDo, token)
        {
            PayerId = payerId;
        }

        protected EcDoRequest(string token, string payerId, string action) : base(PayflowConstants.ActiontypeDoba,
            token)
        {
            PayerId = payerId;
        }

        #endregion
    }
}