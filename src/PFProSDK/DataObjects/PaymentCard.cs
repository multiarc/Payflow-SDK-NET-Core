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
    ///     This abstract class serves as base
    ///     class for Card Payment devices.
    /// </summary>
    /// <remarks>This class can be extended to create a new payment device type.</remarks>
    public class PaymentCard : PaymentDevice
    {
        #region "Constructors"

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="acct">Card number</param>
        /// <param name="expDate">Card expiry date</param>
        /// <remarks>Abstract class. Instance cannot be created directly.</remarks>
        public PaymentCard(string acct, string expDate) : base(acct)
        {
            _mExpDate = expDate;
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
                base.GenerateRequest();
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamExpdate, _mExpDate));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamCvv2, Cvv2));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamCardstart, CardStart));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamCardissue, CardIssue));
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
        ///     Card Expiry Date
        /// </summary>
        private readonly string _mExpDate;

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets, Sets  Cvv2
        /// </summary>
        /// <remarks>
        ///     Card validation code. This is the 3 or 4 digit code
        ///     present at the back side of the card.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CVV2</code>
        /// </remarks>
        public string Cvv2 { get; set; }

        /// <summary>
        ///     Gets, Sets  CardStart
        /// </summary>
        /// <remarks>
        ///     Used for Switch/Solo Cards.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CARDSTART</code>
        /// </remarks>
        public string CardStart { get; set; }

        /// <summary>
        ///     Gets, Sets  CardIssue
        /// </summary>
        /// <remarks>
        ///     Used for Switch/Solo Cards.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CARDISSUE</code>
        /// </remarks>
        public string CardIssue { get; set; }

        #endregion
    }
}