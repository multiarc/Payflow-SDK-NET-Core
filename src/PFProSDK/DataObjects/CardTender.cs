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
    ///     Used for Card tender related information
    /// </summary>
    /// <remarks>
    ///     CreditCard, PurchaseCard and SwipeCard are
    ///     the Payment devices associated with this tender type.
    ///     <seealso cref="CreditCard" />
    ///     <seealso cref="PurchaseCard" />
    ///     <seealso cref="SwipeCard" />
    /// </remarks>
    public sealed class CardTender : BaseTender
    {
        #region "Constructors"

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="credCard">Credit Card object</param>
        /// <remarks>
        ///     This constructor is used to create a CardTender
        ///     with CreditCard as the payment device
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		//CredCard is the CreditCard object.
        /// 		.............
        /// 		
        /// 		CardTender Tender = new CardTender(CredCard);
        /// 		
        /// 		..............
        ///   </code>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		'CredCard is the CreditCard object.
        /// 		.............
        /// 		
        /// 		Dim Tender As CardTender = new CardTender(CredCard)
        /// 		
        /// 		..............
        ///   </code>
        /// </example>
        /// <seealso cref="CreditCard" />
        public CardTender(CreditCard credCard) : base(PayflowConstants.TendertypeCard, credCard)
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="purCard">Purchase Card object</param>
        /// <remarks>
        ///     This constructor is used to create a CardTender
        ///     with PurchaseCard as the payment device
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		//PurCard is the PurchaseCard object.
        /// 		.............
        /// 		
        /// 		CardTender Tender = new CardTender(PurCard);
        /// 		
        /// 		..............
        ///   </code>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		'PurCard is the PurchaseCard object.
        /// 		.............
        /// 		
        /// 		Dim Tender As CardTender = new CardTender(PurCard)
        /// 		
        /// 		..............
        ///   </code>
        /// </example>
        /// <seealso cref="PurchaseCard" />
        public CardTender(PurchaseCard purCard) : base(PayflowConstants.TendertypeCard, purCard)
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="swpCard">Swipe Card object</param>
        /// <remarks>
        ///     This constructor is used to create a CardTender
        ///     with PurchaseCard as the payment device
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		//SwpCard is the SwipeCard object.
        /// 		.............
        /// 		
        /// 		CardTender Tender = new CardTender(SwpCard);
        /// 		
        /// 		..............
        ///   </code>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		'SwpCard is the SwipeCard object.
        /// 		.............
        /// 		
        /// 		Dim Tender As CardTender = new CardTender(SwpCard)
        /// 		
        /// 		..............
        ///   </code>
        /// </example>
        /// <seealso cref="SwipeCard" />
        public CardTender(SwipeCard swpCard) : base(PayflowConstants.TendertypeCard, swpCard)
        {
        }

        #endregion
    }
}