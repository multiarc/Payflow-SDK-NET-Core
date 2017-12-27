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
    ///     Represents SendState.
    /// </summary>
    internal abstract class SendState : PaymentState
    {
        #region "Constructors"

        /// <summary>
        ///     Copy constructor for SendState.
        /// </summary>
        /// <param name="currentPaymentState">Current Payment State object.</param>
        public SendState(PaymentState currentPaymentState) : base(currentPaymentState)
        {
        }

        #endregion

        #region "Functions"

        /// <summary>
        ///     Abstract Declaration of
        ///     GetSendRequest
        /// </summary>
        /// <returns>request to be sent</returns>
        public abstract string GetSendRequest();

        /// <summary>
        ///     Execute function
        /// </summary>
        public override async Task ExecuteAsync()
        {
            var isSendSuccess = false;

            Logger.Instance.Log("PayPal.Payments.Communication.SendState.Execute(): Entered.",
                PayflowConstants.SeverityDebug);
            if (!InProgress)
                return;
            try
            {
                //Begin Payflow Timeout Check Point 3
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

                //End Payflow Timeout Check Point 3
                isSendSuccess = await Connection.SendToServer(GetSendRequest());
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(
                    "PayPal.Payments.Communication.SendState.Execute(): Error occurred While Initializing Connection.",
                    PayflowConstants.SeverityError);
                Logger.Instance.Log("PayPal.Payments.Communication.SendState.Execute(): Exception " + ex,
                    PayflowConstants.SeverityError);
                isSendSuccess = false;
            }
            //catch
            //{
            //    IsSendSuccess = false;
            //}
            finally
            {
                if (isSendSuccess)
                {
                    Logger.Instance.Log("PayPal.Payments.Communication.SendState.Execute(): Send Data =  Success ",
                        PayflowConstants.SeverityInfo);
                    SetStateSuccess();
                }
                else
                {
                    Logger.Instance.Log("PayPal.Payments.Communication.SendState.Execute(): Send Data =  Failure ",
                        PayflowConstants.SeverityInfo);
                    SetStateFail();
                }
            }

            Logger.Instance.Log("PayPal.Payments.Communication.SendState.Execute(): Exiting.",
                PayflowConstants.SeverityDebug);
        }

        #endregion
    }
}