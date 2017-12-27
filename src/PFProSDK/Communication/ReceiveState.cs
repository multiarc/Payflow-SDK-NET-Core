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
using System.Threading.Tasks;
using PFProSDK.Common.Logging;
using PFProSDK.Common.Utility;

#endregion

namespace PFProSDK.Communication
{
    /// <summary>
    ///     Represent Receive State.
    /// </summary>
    internal abstract class ReceiveState : PaymentState
    {
        #region "Constructors"

        /// <summary>
        ///     Copy Constructor for RecieveState.
        /// </summary>
        /// <param name="currentPmtState">Current Payment State.</param>
        public ReceiveState(PaymentState currentPmtState) : base(currentPmtState)
        {
        }

        #endregion

        #region "Functions"

        /// <summary>
        ///     Execute function.
        /// </summary>
        public override async Task ExecuteAsync()
        {
            var isReceiveSuccess = false;
            Logger.Instance.Log("PayPal.Payments.Communication.ReceiveState.Execute(): Entered.",
                PayflowConstants.SeverityDebug);
            if (!InProgress)
                return;
            try
            {
                //Begin Payflow Timeout Check Point 4
                long timeRemainingMsec;
                if (PayflowUtility.IsTimedOut(Connection.TimeOut, Connection.StartTime, out timeRemainingMsec))
                {
                    var addlMessage = "Input timeout value in millisec = " + Connection.TimeOut;
                    var err = PayflowUtility.PopulateCommError(PayflowConstants.ETimeoutWaitResp, null,
                        PayflowConstants.SeverityFatal, IsXmlPayRequest, addlMessage);
                    if (!CommContext.IsCommunicationErrorContained(err)) CommContext.AddError(err);
                }
                else
                {
                    Connection.TimeOut = timeRemainingMsec;
                }

                //End Payflow Timeout Check Point 4
                var responseValue = await Connection.ReceiveResponseAsync();
                isReceiveSuccess = SetReceiveResponse(responseValue);
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(
                    "PayPal.Payments.Communication.ReceiveState.Execute(): Error occurred While Receiving Response.",
                    PayflowConstants.SeverityError);
                Logger.Instance.Log("PayPal.Payments.Communication.ReceiveState.Execute(): Exception " + ex,
                    PayflowConstants.SeverityError);
                isReceiveSuccess = false;
            }
            //catch
            //{
            //    IsReceiveSuccess = false;
            //}
            finally
            {
                if (isReceiveSuccess)
                {
                    Logger.Instance.Log(
                        "PayPal.Payments.Communication.ReceiveState.Execute(): Receive Response = Success ",
                        PayflowConstants.SeverityInfo);
                    SetStateSuccess();
                }
                else
                {
                    Logger.Instance.Log(
                        "PayPal.Payments.Communication.ReceiveState.Execute(): Receive Response = Failure ",
                        PayflowConstants.SeverityInfo);
                    SetStateFail();
                }
            }

            Logger.Instance.Log("PayPal.Payments.Communication.ReceiveState.Execute(): Exiting.",
                PayflowConstants.SeverityDebug);
        }


        /// <summary>
        ///     Abstract declaration of SetReceiveResponse
        /// </summary>
        /// <param name="response">Response String.</param>
        /// <returns>True if response set,false otherwise.</returns>
        public abstract bool SetReceiveResponse(string response);

        #endregion
    }
}