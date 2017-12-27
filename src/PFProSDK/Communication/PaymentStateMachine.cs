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
using System.Collections;
using System.Threading.Tasks;
using PFProSDK.Common;
using PFProSDK.Common.Logging;
using PFProSDK.Common.Utility;
using PFProSDK.DataObjects;

#endregion

namespace PFProSDK.Communication
{
    /// <summary>
    ///     Payment State Driver class.
    /// </summary>
    internal sealed class PaymentStateMachine
    {
        #region "Constructors"

        /// <summary>
        ///     Private constructor for PaymentStateMachine
        /// </summary>
        private PaymentStateMachine()
        {
            _psmContext = new Context();
            _mConnection = new PaymentConnection(ref _psmContext);
        }

        #endregion

        #region "Member Variables"

        /// <summary>
        ///     OS Name
        /// </summary>
        private string _mVitOsName;

        /// <summary>
        ///     OS Architecture.
        /// </summary>
        private string _mVitOsArch;

        /// <summary>
        ///     OS Version.
        /// </summary>
        private string _mVitOsVersion;

        /// <summary>
        ///     .NET Version.
        /// </summary>
        private string _mVitRuntimeVersion = PayflowConstants.EmptyString;

        /// <summary>
        ///     Proxy Used (Y/N)
        /// </summary>
        private string _mVitProxy;

        /// <summary>
        ///     Payment State object.
        /// </summary>
        private PaymentState _mPaymentState;

        /// <summary>
        ///     Connection object.
        /// </summary>
        private readonly PaymentConnection _mConnection;

        /// <summary>
        ///     Context object.
        /// </summary>
        private Context _psmContext;

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets the instance of PaymentStateMachine.
        /// </summary>
        public static PaymentStateMachine Instance => new PaymentStateMachine();


        /// <summary>
        ///     Gets transaction response.
        /// </summary>
        public string Response
        {
            get
            {
                string retVal = null;
                if (_mPaymentState != null) retVal = _mPaymentState.TransactionResponse;
                return retVal;
            }
        }

        /// <summary>
        ///     Gets the Request Id
        /// </summary>
        public string RequestId
        {
            get
            {
                string retVal = null;
                if (_mPaymentState != null) retVal = _mPaymentState.Connection.RequestId;
                return retVal;
            }
        }

        /// <summary>
        ///     Gets the transaction start time.
        /// </summary>
        public long StartTime
        {
            get
            {
                long retVal = 0;
                if (_mPaymentState != null) retVal = _mPaymentState.Connection.StartTime;
                return retVal;
            }
        }

        /// <summary>
        ///     Gets, Sets the transaction timeout.
        /// </summary>
        public long TimeOut
        {
            get
            {
                long retVal = 0;
                if (_mPaymentState != null) retVal = _mPaymentState.Connection.TimeOut;
                return retVal;
            }
            set => _mPaymentState.Connection.TimeOut = value;
        }

        /// <summary>
        ///     Gets the context object.
        /// </summary>
        internal Context PsmContext => _psmContext;

        /// <summary>
        ///     Gets XML Pay Request flag.
        /// </summary>
        public bool IsXmlPayRequest
        {
            get
            {
                var retVal = false;
                if (_mPaymentState != null) retVal = _mPaymentState.IsXmlPayRequest;
                return retVal;
            }
        }

        /// <summary>
        ///     Gets the Inprogress status of transaction.
        /// </summary>
        public bool InProgress
        {
            get
            {
                var retVal = false;
                if (_mPaymentState != null) retVal = _mPaymentState.InProgress;
                return retVal;
            }
        }

        /// <summary>
        ///     Client information.
        /// </summary>
        public ClientInfo ClientInfo { get; private set; }

        #endregion

        #region "Functions"

        /// <summary>
        ///     Sets the Version Tracking information
        ///     in NV Request.
        /// </summary>
        private void SetVersionTracking()
        {
            Logger.Instance.Log("PayPal.Payments.Communication.PaymentStateMachine.SetVersionTracking(): Entered.",
                PayflowConstants.SeverityDebug);


            _mVitOsVersion = Environment.OSVersion.Version.ToString();
            _mVitOsName = Environment.OSVersion.ToString();
            _mVitOsArch = Environment.OSVersion.Platform.ToString();
            _mVitRuntimeVersion = Environment.Version.ToString();
            if (_mConnection.IsProxy)
                _mVitProxy = "Y";
            else
                _mVitProxy = "N";

            //Check whether OS Version string is also present
            // in the Os name string value. If found, remove it 
            //from the string.
            if (_mVitOsVersion != null && _mVitOsName != null)
            {
                var indexOfVersion = _mVitOsName.IndexOf(_mVitOsVersion);
                if (indexOfVersion > 0) _mVitOsName = _mVitOsName.Remove(indexOfVersion, _mVitOsVersion.Length);
            }

            ClientInfo.OsVersion = _mVitOsVersion;
            ClientInfo.OsName = _mVitOsName;
            ClientInfo.OsArchitecture = _mVitOsArch;
            ClientInfo.RunTimeVersion = _mVitRuntimeVersion;
            ClientInfo.Proxy = _mVitProxy;
        }


        /// <summary>
        ///     Initializes transaction context.
        /// </summary>
        /// <param name="hostAddress">Payflow Host Address.</param>
        /// <param name="hostPort">Payflow Host Port.</param>
        /// <param name="timeOut">Transaction timeout.</param>
        /// <param name="proxyAddress">Proxy Address.</param>
        /// <param name="proxyPort">Proxy Port.</param>
        /// <param name="proxyLogon">Proxy Logon Id.</param>
        /// <param name="proxyPassword">Proxy Password.</param>
        /// <param name="clientInfo">Client Info</param>
        public void InitializeContext(string hostAddress, int hostPort, int timeOut, string proxyAddress, int proxyPort,
            string proxyLogon, string proxyPassword, ClientInfo clientInfo)
        {
            Logger.Instance.Log(
                "PayPal.Payments.Communication.PaymentStateMachine.InitializeContext(String, int, int, String, int, String, String): Entered.",
                PayflowConstants.SeverityDebug);
            try
            {
                _mConnection.InitializeConnection(hostAddress, hostPort, timeOut, proxyAddress, proxyPort, proxyLogon,
                    proxyPassword);
                if (clientInfo != null)
                    ClientInfo = clientInfo;
                else
                    ClientInfo = new ClientInfo();

                SetVersionTracking();
                _mConnection.ClientInfo = ClientInfo;
            }
            catch (Exception ex)
            {
                var err = PayflowUtility.PopulateCommError(PayflowConstants.EEmptyParamList, ex,
                    PayflowConstants.SeverityError, _mPaymentState.IsXmlPayRequest,
                    null);
                if (!PsmContext.IsCommunicationErrorContained(err)) PsmContext.AddError(err);
            }
            finally
            {
                Logger.Instance.Log(
                    "PayPal.Payments.Communication.PaymentStateMachine.InitializeContext(String,int,int,String, nt,String,String): Exiting.",
                    PayflowConstants.SeverityDebug);
            }
        }


        /// <summary>
        ///     Initialized Transaction.
        /// </summary>
        /// <param name="paramList">Parameter List</param>
        /// <param name="requestId">Request Id</param>
        public void InitTrans(string paramList, string requestId)
        {
            Logger.Instance.Log("PayPal.Payments.Communication.PaymentStateMachine.InitTrans(String): Entered.",
                PayflowConstants.SeverityDebug);

            try
            {
                _mConnection.RequestId = requestId;
                _mPaymentState = new SendInitState(_mConnection, paramList, ref _psmContext);
            }
            catch (Exception ex)
            {
                var err = PayflowUtility.PopulateCommError(PayflowConstants.EContxtInitFailed, ex,
                    PayflowConstants.SeverityError, _mPaymentState.IsXmlPayRequest,
                    null);
                if (!PsmContext.IsCommunicationErrorContained(err)) PsmContext.AddError(err);
            }
            finally
            {
                Logger.Instance.Log("PayPal.Payments.Communication.PaymentStateMachine.InitTrans(String): Exiting.",
                    PayflowConstants.SeverityDebug);
            }
        }

        /// <summary>
        ///     Executes the transaction.
        /// </summary>
        public async Task ExecuteAsync()
        {
            Logger.Instance.Log("PayPal.Payments.Communication.PaymentStateMachine.Execute(): Entered.",
                PayflowConstants.SeverityDebug);

            try
            {
                if (PsmContext.HighestErrorLvl == PayflowConstants.SeverityFatal)
                {
                    var trxResponse = _mPaymentState.TransactionResponse;
                    string message;
                    if (trxResponse != null && trxResponse.Length > 0)
                    {
                        message = trxResponse;
                    }
                    else
                    {
                        var errorList = _psmContext.GetErrors(PayflowConstants.SeverityFatal);
                        var firstFatalError = (ErrorObject) errorList[0];
                        message = firstFatalError.ToString();
                    }

                    _mPaymentState.SetTransactionFail = message;
                }
                else
                {
                    Logger.Instance.Log(
                        "PayPal.Payments.Communication.PaymentStateMachine.Execute(): Current State = " +
                        _mPaymentState,
                        PayflowConstants.SeverityDebug);
                    await _mPaymentState.ExecuteAsync();
                }
            }
            catch (Exception ex)
            {
                var err = PayflowUtility.PopulateCommError(PayflowConstants.EUnknownState, ex,
                    PayflowConstants.SeverityError,
                    _mPaymentState.IsXmlPayRequest,
                    null);
                if (!PsmContext.IsCommunicationErrorContained(err)) PsmContext.AddError(err);
            }
            finally
            {
                // perform state transition
                _mPaymentState = GetNextState(_mPaymentState);
                ClientInfo = _mConnection.ClientInfo;
                Logger.Instance.Log(
                    "PayPal.Payments.Communication.PaymentStateMachine.Execute(): Exiting.  Current State = " +
                    _mPaymentState.GetType(),
                    PayflowConstants.SeverityDebug);
            }
        }

        /// <summary>
        ///     Changes the Payment States depending upon
        ///     the current state status.
        /// </summary>
        /// <param name="currentPmtState">Current Payment State.</param>
        /// <returns>Next Payment State</returns>
        private PaymentState GetNextState(PaymentState currentPmtState)
        {
            Logger.Instance.Log(
                "PayPal.Payments.Communication.PaymentStateMachine.GetNextState(PaymentState): Entered.",
                PayflowConstants.SeverityDebug);

            if (currentPmtState.Success && currentPmtState.InProgress)
            {
                if (currentPmtState is TransactionReceiveState)
                {
                    // exit state
                    currentPmtState.SetTransactionSuccess();
                }
                else if (currentPmtState is SendInitState)
                {
                    Logger.Instance.Log(
                        "PayPal.Payments.Communication.PaymentStateMachine.GetNextState(PaymentState.Success): SentInitState Entered.",
                        PayflowConstants.SeverityDebug);

                    currentPmtState = new TransactionSendState(currentPmtState);
                }
                else if (currentPmtState is TransactionSendState)
                {
                    Logger.Instance.Log(
                        "PayPal.Payments.Communication.PaymentStateMachine.GetNextState(PaymentState.Success): TransactionSentState Entered.",
                        PayflowConstants.SeverityDebug);
                    currentPmtState = new TransactionReceiveState(currentPmtState);
                }
                else if (currentPmtState is SendRetryState)
                {
                    Logger.Instance.Log(
                        "PayPal.Payments.Communication.PaymentStateMachine.GetNextState(PaymentState.Success): SendRetryState Entered.",
                        PayflowConstants.SeverityDebug);
                    currentPmtState = new SendReconnectState(currentPmtState);
                }
                else if (currentPmtState is SendReconnectState)
                {
                    Logger.Instance.Log(
                        "PayPal.Payments.Communication.PaymentStateMachine.GetNextState(PaymentState.Success): SendReconnectState Entered.",
                        PayflowConstants.SeverityDebug);
                    currentPmtState = new SendInitState(currentPmtState);
                }
                // unknown state
                else
                {
                    var addlMessage = "Unknown State, Current State = " + _mPaymentState;
                    var err = PayflowUtility.PopulateCommError(PayflowConstants.EUnknownState, null,
                        PayflowConstants.SeverityFatal, _mPaymentState.IsXmlPayRequest,
                        addlMessage);
                    if (!PsmContext.IsCommunicationErrorContained(err)) PsmContext.AddError(err);
                }
            }
            else if (currentPmtState.Failed && currentPmtState.InProgress)
            {
                Logger.Instance.Log(
                    "PayPal.Payments.Communication.PaymentStateMachine.GetNextState(PaymentState): Current Payment State Failed.  Current State = " +
                    _mPaymentState,
                    PayflowConstants.SeverityDebug);
                if (currentPmtState is ReconnectState)
                {
                    // exit state
                    if (!PsmContext.IsErrorContained())
                    {
                        var addlMessage =
                            "Exceeded Reconnect attempts, check context for error, Current reconnect attempt = " +
                            _mPaymentState.AttemptNo;
                        var err = PayflowUtility.PopulateCommError(PayflowConstants.ETimeoutWaitResp, null,
                            PayflowConstants.SeverityFatal, _mPaymentState.IsXmlPayRequest,
                            addlMessage);
                        if (!PsmContext.IsCommunicationErrorContained(err)) PsmContext.AddError(err);
                    }
                    else
                    {
                        var errList = new ArrayList();
                        errList.AddRange(PsmContext.GetErrors());
                        var highestSevLevel = PsmContext.HighestErrorLvl;

                        int errorListIndex;
                        var errorListSize = errList.Count;
                        for (errorListIndex = 0; errorListIndex < errorListSize; errorListIndex++)
                        {
                            var err = (ErrorObject) errList[errorListIndex];
                            if (err.SeverityLevel == highestSevLevel)
                            {
                                int index;
                                var size = err.MessageParams.Count;
                                var msgCodeParams = new string[size];
                                for (index = 0; index < size; index++)
                                    msgCodeParams[index] = (string) err.MessageParams[index];

                                var error = new ErrorObject(PayflowConstants.SeverityFatal, err.MessageCode,
                                    msgCodeParams);
                                errList[errorListIndex] = error;
                                break;
                            }
                        }

                        PsmContext.ClearErrors();
                        PsmContext.AddErrors(errList);
                    }
                }
                else if (currentPmtState is SendInitState)
                {
                    Logger.Instance.Log(
                        "PayPal.Payments.Communication.PaymentStateMachine.GetNextState(PaymentState.Failed): SendInitState Entered.",
                        PayflowConstants.SeverityDebug);
                    currentPmtState = new SendReconnectState(currentPmtState);
                }
                else if (currentPmtState is TransactionSendState)
                {
                    Logger.Instance.Log(
                        "PayPal.Payments.Communication.PaymentStateMachine.GetNextState(PaymentState.Failed): TransactionSendState Entered.",
                        PayflowConstants.SeverityDebug);
                    currentPmtState = new SendRetryState(currentPmtState);
                }
                else if (currentPmtState is TransactionReceiveState)
                {
                    Logger.Instance.Log(
                        "PayPal.Payments.Communication.PaymentStateMachine.GetNextState(PaymentState.Failed): TransactionReceiveState Entered.",
                        PayflowConstants.SeverityDebug);
                    currentPmtState = new SendRetryState(currentPmtState);
                }
                // unknown state
                else
                {
                    var addlMessage = "Current State = " + _mPaymentState;
                    var err = PayflowUtility.PopulateCommError(PayflowConstants.EUnknownState, null,
                        PayflowConstants.SeverityFatal, _mPaymentState.IsXmlPayRequest, addlMessage);
                    if (!PsmContext.IsCommunicationErrorContained(err)) PsmContext.AddError(err);
                }
            }

            Logger.Instance.Log(
                "PayPal.Payments.Communication.PaymentStateMachine.GetNextState(PaymentState): Obtained State = "
                + _mPaymentState.GetType(),
                PayflowConstants.SeverityInfo);
            Logger.Instance.Log(
                "PayPal.Payments.Communication.PaymentStateMachine.GetNextState(PaymentState): Exiting.",
                PayflowConstants.SeverityDebug);
            return currentPmtState;
        }

        #endregion
    }
}