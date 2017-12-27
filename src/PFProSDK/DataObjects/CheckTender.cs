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
    ///     Used for Check tender related information.
    /// </summary>
    /// <remarks>
    ///     CheckPayment is the Payment device associated with
    ///     this tender type.
    ///     <seealso cref="CheckPayment" />
    /// </remarks>
    public sealed class CheckTender : BaseTender
    {
        #region "Constructors"

        /// <summary>
        ///     Constructor for CheckTender
        /// </summary>
        /// <param name="check">Check Payment object.</param>
        /// <remarks>
        ///     This constructor is used to create a CheckTender
        ///     with CheckPayment as the payment device
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		//Check is the CheckPayment object.
        /// 		.............
        /// 		
        /// 		CheckTender Tender = new CheckTender(Check);
        /// 		
        /// 		..............
        ///   </code>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		'Check is the CheckPayment object.
        /// 		.............
        /// 		
        /// 		Dim Tender As CheckTender = new CheckTender(Check)
        /// 		
        /// 		..............
        ///   </code>
        /// </example>
        /// <seealso cref="CheckPayment" />
        public CheckTender(CheckPayment check) : base(PayflowConstants.TendertypeTelecheck, check)
        {
        }

        #endregion
    }
}