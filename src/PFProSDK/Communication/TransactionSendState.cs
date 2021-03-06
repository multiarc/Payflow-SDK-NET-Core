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

using PFProSDK.Common.Logging;
using PFProSDK.Common.Utility;

#endregion

namespace PFProSDK.Communication
{
    /// <summary>
    ///     Represents transaction send state.
    /// </summary>
    internal class TransactionSendState : SendState
    {
        #region "Constructors"

        /// <summary>
        ///     Copy constructor for TransactionSendState.
        /// </summary>
        /// <param name="currentPaymentState">Current Payment State object.</param>
        public TransactionSendState(PaymentState currentPaymentState) : base(currentPaymentState)
        {
        }

        #endregion

        #region "Functions"

        /// <summary>
        ///     Gets the request to be sent.
        /// </summary>
        /// <returns>Request to be sent.</returns>
        public override string GetSendRequest()
        {
            Logger.Instance.Log("PayPal.Payments.Communication.TransactionSendState.GetSendRequest(): Entered.",
                PayflowConstants.SeverityDebug);

            var logRequest = TransactionRequest;
            logRequest = PayflowUtility.MaskSensitiveFields(logRequest);


            Logger.Instance.Log(
                "PayPal.Payments.Communication.TransactionSendState.GetSendRequest(): Request = " + logRequest,
                PayflowConstants.SeverityInfo);
            Logger.Instance.Log("PayPal.Payments.Communication.TransactionSendState.GetSendRequest(): Exiting.",
                PayflowConstants.SeverityDebug);
            return TransactionRequest;
        }

        #endregion
    }
}