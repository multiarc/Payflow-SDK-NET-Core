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
    ///     Used for ExpressCheckout with Billing Agreement (Reference Transaction) without Purchase DO operation.
    /// </summary>
    /// <remarks>
    ///     <seealso cref="EcSetBaRequest" />
    ///     <seealso cref="EcGetBaRequest" />
    /// </remarks>
    public class EcdoBaRequest : EcDoRequest
    {
        #region "Constructor"

        /// <summary>
        ///     Constructor for ECDoBARequest
        /// </summary>
        /// <param name="token">String</param>
        /// <param name="payerId">String</param>
        /// <remarks>
        ///     ECDoBARequest is used to set the data required for a Express Checkout DO operation
        ///     with Billing Agreement (Reference Transaction) without Purchase.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		
        /// 		//Create the ECDoBARequest object
        /// 		ECDoBARequest DoEC = new ECDoBARequest("[tokenid]","[payerid]");
        /// 		
        /// 		.............
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		.............
        /// 		
        /// 		//Create the ECDoBARequest object
        /// 		Dim DoEC As ECDoBARequest = new ECDoBARequest("[tokenid]","[payerid]")
        /// 		
        /// 		.............
        ///  </code>
        /// </example>
        public EcdoBaRequest(string token, string payerId) : base(token, payerId, PayflowConstants.ActiontypeDoba)
        {
        }

        #endregion
    }
}