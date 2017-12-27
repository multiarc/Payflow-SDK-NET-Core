#region "Copyright"

//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

#endregion

#region "Imports"

using System;
using System.Threading.Tasks;
using PFProSDK.Common;
using PFProSDK.Common.Logging;
using PFProSDK.Common.Utility;

#endregion

namespace PFProSDK.Communication
{
    /// <summary> InitState - PayPal Payment State</summary>
    internal abstract class InitState : PaymentState
    {
        /// <summary>
        ///     Sets the appropriate server file path for the connection
        ///     and initializes the connection uri.
        /// </summary>
        public override Task ExecuteAsync()
        {
            var isConnected = false;
            Logger.Instance.Log("PayPal.Payments.Communication.InitState.Execute(): Entered.",
                PayflowConstants.SeverityDebug);
            if (!InProgress)
                return Task.CompletedTask;
            try
            {
                Logger.Instance.Log("PayPal.Payments.Communication.InitState.Execute(): Initializing Connection.",
                    PayflowConstants.SeverityInfo);
                //Begin Payflow Timeout Check Point 2
                long timeRemainingMsec;
                var timedOut =
                    PayflowUtility.IsTimedOut(Connection.TimeOut, Connection.StartTime, out timeRemainingMsec);
                if (timedOut)
                {
                    var addlMessage = "Input timeout value in millisec : " + Connection.TimeOut;
                    var err = PayflowUtility.PopulateCommError(PayflowConstants.ETimeoutWaitResp, null,
                        PayflowConstants.SeverityFatal, IsXmlPayRequest, addlMessage);
                    if (!CommContext.IsCommunicationErrorContained(err)) CommContext.AddError(err);
                }
                else
                {
                    Connection.TimeOut = timeRemainingMsec;
                }

                //End Payflow Timeout Check Point 2
                isConnected = Connection.ConnectToServer();
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(
                    "PayPal.Payments.Communication.InitState.Execute(): Error occurred While Initializing Connection.",
                    PayflowConstants.SeverityError);
                Logger.Instance.Log("PayPal.Payments.Communication.InitState.Execute(): Exception " + ex,
                    PayflowConstants.SeverityError);
                isConnected = false;
            }
            //catch
            //{
            //    IsConnected = false;
            //}
            finally
            {
                if (isConnected)
                {
                    Logger.Instance.Log(
                        "PayPal.Payments.Communication.InitState.Execute(): Connection Initialization =  Success",
                        PayflowConstants.SeverityInfo);
                    SetStateSuccess();
                }
                else
                {
                    Logger.Instance.Log(
                        "PayPal.Payments.Communication.InitState.Execute(): Initialized Connection = Failure",
                        PayflowConstants.SeverityInfo);
                    SetStateFail();
                }
            }

            Logger.Instance.Log("PayPal.Payments.Communication.InitState.Execute(): Exiting.",
                PayflowConstants.SeverityDebug);
            return Task.CompletedTask;
        }

        #region "Properties"

        #endregion

        #region "Constructors"

        /// <summary>
        ///     Constructor for InitState.
        /// </summary>
        /// <param name="connection">PaymentConnection Object.</param>
        /// <param name="initialParameterList">Initial Parameter list.</param>
        /// <param name="psmContext">Context Object by ref</param>
        public InitState(PaymentConnection connection, string initialParameterList, ref Context psmContext) : base(
            connection, initialParameterList, ref psmContext)
        {
        }

        /// <summary>
        ///     Copy Constructor for InitState.
        /// </summary>
        /// <param name="currentPaymentState">PaymentState Object.</param>
        public InitState(PaymentState currentPaymentState) : base(currentPaymentState)
        {
        }

        #endregion
    }
}