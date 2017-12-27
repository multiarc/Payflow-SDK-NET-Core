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
    ///     Represents transaction receive state.
    /// </summary>
    internal class TransactionReceiveState : ReceiveState
    {
        #region "Constructors"

        /// <summary>
        ///     Copy constructor for TransactionReceiveState.
        /// </summary>
        /// <param name="currentPaymentState">Current Payment State object.</param>
        public TransactionReceiveState(PaymentState currentPaymentState) : base(currentPaymentState)
        {
        }

        #endregion

        #region "Functions"

        /// <summary>
        ///     Sets the received response
        /// </summary>
        /// <param name="response">response</param>
        /// <returns>true if response set, false otherwise.</returns>
        public override bool SetReceiveResponse(string response)
        {
            var retVal = false;
            Logger.Instance.Log(
                "PayPal.Payments.Communication.TransactionReceiveState.SetReceiveResponse(String): Entered.",
                PayflowConstants.SeverityDebug);
            if (response == null)
            {
                Logger.Instance.Log(
                    "PayPal.Payments.Communication.TransactionReceiveState.SetReceiveResponse(String): Response = null",
                    PayflowConstants.SeverityWarn);
                retVal = false;
            }
            else if (response.Length == 0)
            {
                Logger.Instance.Log(
                    "PayPal.Payments.Communication.TransactionReceiveState.SetReceiveResponse(String): Response.Length = 0",
                    PayflowConstants.SeverityWarn);
                retVal = false;
            }
            else
            {
                TransactionResponse = response;
                Logger.Instance.Log(
                    "PayPal.Payments.Communication.TransactionReceiveState.SetReceiveResponse(String): Response = " +
                    TransactionResponse, PayflowConstants.SeverityInfo);
                retVal = true;
            }

            SetProgressComplete();


            Logger.Instance.Log(
                "PayPal.Payments.Communication.TransactionReceiveState.SetReceiveResponse(String): Exiting.",
                PayflowConstants.SeverityDebug);
            return retVal;
        }

        #endregion
    }
}