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

using System.Threading.Tasks;
using PFProSDK.Common.Logging;
using PFProSDK.Common.Utility;

#endregion

namespace PFProSDK.Communication
{
    /// <summary>
    ///     Represents RetryState.
    /// </summary>
    internal abstract class RetryState : PaymentState
    {
        #region "Constructors"

        /// <summary>
        ///     Copy Constructor for RetryState.
        /// </summary>
        /// <param name="currentPmtState">Current Payment State.</param>
        public RetryState(PaymentState currentPmtState) : base(currentPmtState)
        {
        }

        #endregion

        #region "Functions"

        /// <summary>
        ///     Execute function.
        /// </summary>
        public override Task ExecuteAsync()
        {
            Logger.Instance.Log("PayPal.Payments.Communication.RetryState.Execute(): Entered.",
                PayflowConstants.SeverityDebug);
            Connection.Disconnect();
            SetStateSuccess();
            Logger.Instance.Log("PayPal.Payments.Communication.RetryState.Execute(): Exiting.",
                PayflowConstants.SeverityDebug);
            return Task.CompletedTask;
        }

        #endregion
    }
}