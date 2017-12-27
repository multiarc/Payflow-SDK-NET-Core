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
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Extensions.Configuration;
using PFProSDK.Common.Logging;
using PFProSDK.DataObjects;

#endregion

namespace PFProSDK.Common.Utility
{
    /// <summary>
    ///     Utility class
    /// </summary>
    public sealed class PayflowUtility
    {
        #region "Constructors"

        /// <summary>
        ///     Constructor for PayflowUtility
        /// </summary>
        private PayflowUtility()
        {
        }

        #endregion

        #region "Properties"

        /// <summary>
        ///     Generates Request Id
        /// </summary>
        public static string RequestId
        {
            get
            {
                string requestId;
                var guidValue = Guid.NewGuid();
                requestId = guidValue.ToString("N");
                return requestId;
            }
        }

        /// <summary>
        ///     Gets, sets mTraceInitialized
        /// </summary>
        internal static bool TraceInitialized { get; set; }

        #endregion

        #region "Functions"

        /// <summary>
        ///     Appends a name value pair to request
        /// </summary>
        /// <param name="name">Payflow  param name</param>
        /// <param name="value">Value</param>
        /// <returns>Formatted name value pair string</returns>
        internal static string AppendToRequest(string name, object value)
        {
            string retVal = null;
            var nvPair = new StringBuilder();

            if (name == null || value == null) return PayflowConstants.EmptyString;

            /*if(Value != null && Value.ToString().Length == 0)
            {
                return "";
            }*/

            var stringValue = value.ToString();
            nvPair.Append(name);
            nvPair.Append(PayflowConstants.OpeningBraceNvp);
            nvPair.Append(stringValue.Length);
            nvPair.Append(PayflowConstants.ClosingBraceNvp);
            nvPair.Append(PayflowConstants.SeparatorNvp);
            nvPair.Append(stringValue);
            nvPair.Append(PayflowConstants.DelimiterNvp);

            retVal = nvPair.ToString();

            return retVal;
        }

        /// <summary>
        ///     Locates value from name value pair and masks or
        ///     returns it.
        /// </summary>
        /// <param name="paramList">Paramalist</param>
        /// <param name="name">Payflow  Param name</param>
        /// <param name="maskFoundValue">
        ///     true if sensitive fields from the param list need to be masked,
        ///     false if need to extract a param value from param list
        /// </param>
        /// <returns>Param Value if MaskFoundValue is false, Masked param list if MaskFoundValue is true</returns>
        public static string LocateValueForName(string paramList, string name, bool maskFoundValue)
        {
            string value;
            if (maskFoundValue)
                value = paramList;
            else
                value = PayflowConstants.EmptyString;

            if (paramList != null && paramList.Length > 0)
            {
                var nameIndex = paramList.IndexOf(name + PayflowConstants.SeparatorNvp);
                if (nameIndex < 0)
                {
                    nameIndex = paramList.IndexOf(name + PayflowConstants.OpeningBraceNvp);
                    if (nameIndex < 0) return value;
                }

                var prevNameIndex = nameIndex;
                if (nameIndex > 0)
                    if (paramList[nameIndex - 1] != '&')
                    {
                        nameIndex = paramList.IndexOf(name + PayflowConstants.SeparatorNvp, prevNameIndex);
                        if (nameIndex < 0)
                        {
                            nameIndex = paramList.IndexOf(name + PayflowConstants.OpeningBraceNvp, prevNameIndex + 1);
                            if (nameIndex < 0) return value;
                        }
                    }


                var nvSeparatorIndex = paramList.IndexOf("=", nameIndex);
                if (nvSeparatorIndex > 0)
                {
                    var nvDelimiterIndex = paramList.IndexOf("&", nvSeparatorIndex);
                    if (nvDelimiterIndex + 1 < paramList.Length && paramList[nvDelimiterIndex + 1] == '&')
                    {
                        nvDelimiterIndex += 2;
                        nvDelimiterIndex = paramList.IndexOf("&", nvDelimiterIndex);
                    }

                    if (nvDelimiterIndex < 0) nvDelimiterIndex = paramList.Length;

                    if (maskFoundValue)
                    {
                        int maskIndex;
                        var valueArr = value.ToCharArray();
                        if (name == PayflowConstants.ParamAcct)
                            for (maskIndex = nvSeparatorIndex + 7; maskIndex < nvDelimiterIndex - 4; maskIndex++)
                                valueArr[maskIndex] = 'X';
                        else
                            for (maskIndex = nvSeparatorIndex + 1; maskIndex < nvDelimiterIndex; maskIndex++)
                                valueArr[maskIndex] = 'X';

                        value = new string(valueArr);
                    }
                    else
                    {
                        value = paramList.Substring(nvSeparatorIndex + 1, nvDelimiterIndex - (nvSeparatorIndex + 1));
                    }
                }
            }

            return value;
        }


        /// <summary>
        ///     Check if timeout has occurred
        /// </summary>
        /// <param name="timeOutMsec">Time out in Msec</param>
        /// <param name="startTimeMsec">Start time in Msec</param>
        /// <param name="timeRemainingMsec">out Time remaining in Msec</param>
        /// <returns>True if Timed out, false otherwise</returns>
        internal static bool IsTimedOut(long timeOutMsec, long startTimeMsec, out long timeRemainingMsec)
        {
            var currentTimeMsec = DateTime.Now.Ticks / 10000;
            var timeElapsedMsec = currentTimeMsec - startTimeMsec;
            timeRemainingMsec = timeOutMsec - timeElapsedMsec;
            Logger.Instance.Log("Time Remaining = " + timeRemainingMsec, PayflowConstants.SeverityInfo);
            if (timeRemainingMsec >= 0) return false;

            return true;
        }

        /// <summary>
        ///     Retrieves XmlPay version from Xml Pay Request.
        /// </summary>
        /// <param name="request">Xml Pay Request.</param>
        /// <returns>String Value of version</returns>
        internal static string GetXmlVersion(string request)
        {
            Logger.Instance.Log("PayPal.Payments.Communication.PaymentState.GetXmlVersion(String): Entered.",
                PayflowConstants.SeverityDebug);
            string version;
            version = GetXmlAttribute(request, PayflowConstants.XmlParamVersion);
            Logger.Instance.Log("PayPal.Payments.Communication.PaymentState.GetXmlVersion(String): Exiting.",
                PayflowConstants.SeverityDebug);
            return version;
        }

        /// <summary>
        ///     Retrieves value of given Xml attribute from Xml Pay Request.
        /// </summary>
        /// <param name="request">Xml Pay Request</param>
        /// <param name="attribute">Attribute Tag Name</param>
        /// <returns></returns>
        internal static string GetXmlAttribute(string request, string attribute)
        {
            Logger.Instance.Log("PayPal.Payments.Communication.PaymentState.GetXmlAttribute(String,String): Entered.",
                PayflowConstants.SeverityDebug);

            var retVal = PayflowConstants.EmptyString;
            if (request != null && request.Length > 0)
            {
                string attributeValue = null;
                var xmlPayRequest = new XmlDocument();
                xmlPayRequest.LoadXml(request);
                var xmlPayChildNodes = xmlPayRequest.ChildNodes;
                foreach (XmlNode xmlPayNode in xmlPayChildNodes)
                    if (xmlPayNode.LocalName.Equals(PayflowConstants.XmlpayRequestTag))
                    {
                        var xmlPayReqAttributes = xmlPayNode.Attributes;
                        if (xmlPayReqAttributes != null)
                        {
                            var attributeNode = xmlPayReqAttributes.GetNamedItem(attribute);
                            if (attributeNode != null) attributeValue = attributeNode.InnerText;
                        }

                        break;
                    }

                retVal = attributeValue;
            }

            return retVal;
        }

        /// <summary>
        ///     Gets the Xml Namespace from the XmlPay Request.
        /// </summary>
        /// <param name="request">Xml Pay Request.</param>
        /// <returns>String Value of Xml Namespace.</returns>
        internal static string GetXmlNameSpace(string request)
        {
            Logger.Instance.Log("PayPal.Payments.Communication.PaymentState.GetXmlNameSpace(String): Entered.",
                PayflowConstants.SeverityDebug);

            var xmlNameSpace = "";
            string xmlPayVersion = null;
            xmlPayVersion = GetXmlVersion(request);
            if (!"1.0".Equals(xmlPayVersion)) xmlNameSpace = PayflowConstants.XmlpayNamespace;

            Logger.Instance.Log("PayPal.Payments.Communication.PaymentState.GetXmlNameSpace(String): Exiting.",
                PayflowConstants.SeverityDebug);
            return xmlNameSpace;
        }

        /// <summary>
        ///     Gets the inner text from an xml node.
        /// </summary>
        /// <param name="xmlPayRequest">XmlPay Request loaded in System.Xml.XmlDocument.</param>
        /// <param name="nodeName">Node Name</param>
        /// <returns>Inner text in node.</returns>
        internal static string GetXmlNodeValue(XmlDocument xmlPayRequest, string nodeName)
        {
            var retVal = PayflowConstants.EmptyString;
            if (xmlPayRequest != null && nodeName != null && nodeName.Length > 0)
            {
                var nodeList = xmlPayRequest.GetElementsByTagName(nodeName);
                if (nodeList != null && nodeList.Count == 1)
                {
                    var nodeElement = nodeList[0];
                    if (nodeElement != null) retVal = nodeElement.InnerText;
                }
            }

            return retVal;
        }


        /// <summary>
        ///     Populates Errors from Exceptions.
        /// </summary>
        /// <param name="commMessageCode">Error Message Code</param>
        /// <param name="ex">Occurred Exception, pass null if no Exception.</param>
        /// <param name="addMessage">Additional Message</param>
        /// <param name="isXmlPayReq">True if request is xml pay request, false otherwise</param>
        /// <param name="severityLevel">Severity Level</param>
        /// <returns>Populated ErrorObject.</returns>
        internal static ErrorObject PopulateCommError(string commMessageCode,
            Exception ex, int severityLevel, bool isXmlPayReq, string addMessage)
        {
            string message;
            string messageCode;
            var trace = PayflowConstants.EmptyString;

            InitStackTraceOn();

            string[] msgParams = null;

            if (addMessage == null)
                addMessage = PayflowConstants.EmptyString;
            else if (addMessage.Length > 0)
                addMessage = " " + addMessage;

            if (ex != null && 0 == string.Compare(PayflowConstants.TraceOn, PayflowConstants.Trace, true))
                trace = " " + ex;

            message = (string) PayflowConstants.CommErrorMessages[commMessageCode]
                      + addMessage + trace;

            if (isXmlPayReq)
            {
                messageCode = PayflowConstants.MsgCommunicationErrorXmlpay;
                msgParams = new[] {(string) PayflowConstants.CommErrorCodes[commMessageCode], message};
            }
            else
            {
                messageCode = PayflowConstants.MsgCommunicationError;
                msgParams = new[] {(string) PayflowConstants.CommErrorCodes[commMessageCode], message};
            }


            var initError = new ErrorObject(severityLevel, messageCode, msgParams);
            return initError;
        }

        /// <summary>
        ///     Masks the sensitive fields in the
        ///     param list which will be used for
        ///     logging purpose.
        /// </summary>
        /// <param name="parmList">Paramlist to be masked.</param>
        /// <returns>Masked param list.</returns>
        internal static string MaskSensitiveFields(string parmList)
        {
            string retVal;
            if (parmList != null && parmList.Length > 0)
                if (parmList.IndexOf(PayflowConstants.XmlId) >= 0)
                    retVal = MaskXmlPayRequest(parmList);
                else
                    retVal = MaskNvpRequest(parmList);
            else
                retVal = parmList;

            return retVal;
        }

        /// <summary>
        ///     Masks XMLPay Request
        /// </summary>
        /// <param name="parmList">XMLPay request to be masked</param>
        /// <returns>Masked XMLPay Request</returns>
        internal static string MaskXmlPayRequest(string parmList)
        {
            string retVal;
            try
            {
                var xmlPayRequest = new XmlDocument();
                xmlPayRequest.LoadXml(parmList);
                //Mask ACCT if present : Corresponding XmlPay element --> AcctNum or CardNum
                MaskXmlNodeValue(ref xmlPayRequest, PayflowConstants.XmlParamAcctnum);
                MaskXmlNodeValue(ref xmlPayRequest, PayflowConstants.XmlParamCardnum);
                //Mask EXPDATE if present : Corresponding XmlPay element --> ExpDate
                //PayflowUtility.MaskXmlNodeValue(ref XmlPayRequest, PayflowConstants.XML_PARAM_EXPDATE);
                //Mask SWIPE if present : Corresponding XmlPay element --> MagData
                MaskXmlNodeValue(ref xmlPayRequest, PayflowConstants.XmlParamMagdata);
                //Mask MICR if present : Corresponding XmlPay element --> MICR or MagData
                MaskXmlNodeValue(ref xmlPayRequest, PayflowConstants.XmlParamMicr);
                //Mask CVV2 if present : Corresponding XmlPay element --> CVNum
                MaskXmlNodeValue(ref xmlPayRequest, PayflowConstants.XmlParamCvnum);
                //Mask PWD if present : Corresponding XmlPay element --> Password
                MaskXmlNodeValue(ref xmlPayRequest, PayflowConstants.XmlParamPassword);
                //Mask DL if present : Corresponding XmlPay element --> DL
                MaskXmlNodeValue(ref xmlPayRequest, PayflowConstants.XmlParamDl);
                //Mask SS if present : Corresponding XmlPay element --> CVNum
                MaskXmlNodeValue(ref xmlPayRequest, PayflowConstants.XmlParamSs);
                //Mask DOB if present : Corresponding XmlPay element --> DOB
                MaskXmlNodeValue(ref xmlPayRequest, PayflowConstants.XmlParamDob);

                retVal = xmlPayRequest.InnerXml;
            }
            catch
            {
                retVal = parmList;
            }

            return retVal;
        }

        /// <summary>
        ///     Masks the NVP request
        /// </summary>
        /// <param name="parmList">Param List to be masked</param>
        /// <returns>Masked Param List</returns>
        internal static string MaskNvpRequest(string parmList)
        {
            var logParmList = parmList;
            //Mask ACCT if present
            logParmList = LocateValueForName(logParmList, PayflowConstants.ParamAcct, true);
            //Mask EXPDATE if present
            //LogParmList = PayflowUtility.LocateValueForName(LogParmList, PayflowConstants.PARAM_EXPDATE, true);
            //Mask SWIPE if present
            logParmList = LocateValueForName(logParmList, PayflowConstants.ParamSwipe, true);
            //Mask MICR if present
            logParmList = LocateValueForName(logParmList, PayflowConstants.ParamMicr, true);
            //Mask CVV2 if present
            logParmList = LocateValueForName(logParmList, PayflowConstants.ParamCvv2, true);
            //Mask PWD 
            logParmList = LocateValueForName(logParmList, PayflowConstants.ParamPwd, true);
            //Mask DL if present
            logParmList = LocateValueForName(logParmList, PayflowConstants.ParamDl, true);
            //Mask SS if present
            logParmList = LocateValueForName(logParmList, PayflowConstants.ParamSs, true);
            //Mask DOB if present
            logParmList = LocateValueForName(logParmList, PayflowConstants.ParamDob, true);
            //Mask VIT_OSNAME if present
            logParmList = LocateValueForName(logParmList, PayflowConstants.ParamVitOsname, true);
            //Mask VIT_OSARCH if present
            logParmList = LocateValueForName(logParmList, PayflowConstants.ParamVitOsarch, true);
            //Mask VIT_OSVERSION if present
            logParmList = LocateValueForName(logParmList, PayflowConstants.ParamVitOsversion, true);
            //Mask VIT_SDKRUNTIMEVERSION if present
            logParmList =
                LocateValueForName(logParmList, PayflowConstants.ParamVitSdkruntimeversion, true);
            //Mask VIT_PROXY if present
            logParmList = LocateValueForName(logParmList, PayflowConstants.ParamVitProxy, true);
            return logParmList;
        }

        /// <summary>
        ///     This method replaces the inner text of the node NodeName in the XMLPayRequest xml document with XXXXX
        /// </summary>
        /// <param name="xmlPayRequest">This is the XML Document</param>
        /// <param name="nodeName">This is the node name whose inner text is to be masked</param>
        internal static void MaskXmlNodeValue(ref XmlDocument xmlPayRequest, string nodeName)
        {
            var retVal = "";
            var maskValue = "";
            if (xmlPayRequest != null && nodeName != null && nodeName.Length > 0)
            {
                var nodeList = xmlPayRequest.GetElementsByTagName(nodeName);
                if (nodeList != null && nodeList.Count > 0)
                {
                    var n = 0;
                    do
                    {
                        var nodeElement = nodeList[n];
                        if (nodeElement != null)
                        {
                            retVal = nodeElement.InnerText;


                            for (var i = 0; i < retVal.Length; i++) maskValue = maskValue + "X";

                            nodeElement.InnerText = maskValue;
                            n++;
                            maskValue = "";
                        }
                    } while (n < nodeList.Count);
                }
            }
        }

        internal static void InitStackTraceOn()
        {
            if (!TraceInitialized)
                try
                {
                    string stackTraceOn;
                    stackTraceOn = AppSettings(PayflowConstants.TraceTag);
                    if (0 == string.Compare(PayflowConstants.TraceOn, stackTraceOn, true))
                        PayflowConstants.Trace = PayflowConstants.TraceOn;
                }
                catch
                {
                    PayflowConstants.Trace = PayflowConstants.TraceDefault;
                }

            TraceInitialized = true;
        }

        /// <summary>
        ///     Provides the status of the transaction based on the transaction response.
        /// </summary>
        /// <param name="resp">Response obtained from PayPal Payment Gateway.</param>
        /// <returns>
        ///     String result for the transaction:
        ///     <list type="bullet">
        ///         <item>If Transaction Result = 0  then Transaction Successful.</item>
        ///         <item>If Transaction Result != 0 then Transaction Failed.</item>
        ///     </list>
        /// </returns>
        public static string GetStatus(Response resp)
        {
            string status = null;

            if (resp.TransactionResponse != null && 0 == resp.TransactionResponse.Result)
                status = "Transaction Successful.";
            else
                status = "Transaction Failed.";

            return status;
        }

        /// <summary>
        ///     Provides the status of the transaction based on the transaction responses.
        /// </summary>
        /// <param name="transactionResponse">Transaction response string obtained from PayPal Payment Gateway.</param>
        /// <returns>
        ///     String result for the transaction:
        ///     <list type="bullet">
        ///         <item>If Transaction Result = 0 Transaction Successful.</item>
        ///         <item>If Transaction Result != 0 then Transaction Failed.</item>
        ///     </list>
        /// </returns>
        public static string GetStatus(string transactionResponse)
        {
            var status = PayflowConstants.EmptyString;
            var isTrxRespXmlPay = false;
            var nullTrxResp = false;
            int index;
            if (transactionResponse != null)
            {
                index = transactionResponse.IndexOf(PayflowConstants.XmlRespId);
                if (index >= 0)
                    isTrxRespXmlPay = true;
                else
                    isTrxRespXmlPay = false;
            }
            else
            {
                nullTrxResp = true;
            }


            var trxResult = PayflowConstants.EmptyString;

            if (!nullTrxResp)
                if (isTrxRespXmlPay)
                    trxResult = GetXmlPayNodeValue(transactionResponse, PayflowConstants.XmlParamResult);
                else
                    trxResult = LocateValueForName(transactionResponse, PayflowConstants.ParamResult, false);

            if (!nullTrxResp)
                if ("0".Equals(trxResult))
                    status = "Transaction Successful.";
                else
                    status = "Transaction Failed.";

            return status;
        }

        /// <summary>
        ///     Returns a boolean value indicating the status of the transaction
        /// </summary>
        /// <param name="resp">Response obtained from PayPal Payment Gateway.</param>
        /// <returns>true if transaction was success</returns>
        public static bool GetTransactionStatus(Response resp)
        {
            return resp.TransactionResponse != null && 0 == resp.TransactionResponse.Result;
        }


        /// <summary>
        ///     Gets the inner text of a node from an XMLPay request
        /// </summary>
        /// <param name="xmlPayRequest">XMLPay request string</param>
        /// <param name="nodeName">Node name</param>
        /// <returns>Inner text string</returns>
        public static string GetXmlPayNodeValue(string xmlPayRequest, string nodeName)
        {
            string retVal = null;
            try
            {
                var xmlPayDoc = new XmlDocument();
                xmlPayDoc.LoadXml(xmlPayRequest);
                retVal = GetXmlNodeValue(xmlPayDoc, nodeName);
            }
            catch
            {
                retVal = null;
            }

            return retVal;
        }


        internal static ArrayList AlignContext(Context context, bool isXmlPayRequest)
        {
            var errors = context.GetErrors();
            var retVal = new ArrayList();
            var errorCount = errors.Count;
            int index;
            for (index = 0; index < errorCount; index++)
            {
                var error = (ErrorObject) errors[index];
                var messageCode = error.MessageCode;
                if (error != null)
                    if (messageCode != null && messageCode.Length > 0)
                    {
                        var msg1012 = false;
                        var msg1013 = false;
                        var msg1015 = false;
                        var msg1016 = false;

                        if ("MSG_1012".Equals(messageCode))
                            msg1012 = true;
                        else if ("MSG_1013".Equals(messageCode))
                            msg1013 = true;
                        else if ("MSG_1015".Equals(messageCode))
                            msg1015 = true;
                        else if ("MSG_1016".Equals(messageCode))
                            msg1016 = true;

                        if (isXmlPayRequest)
                        {
                            if (msg1013 || msg1016)
                            {
                                retVal.Add(error);
                            }
                            else
                            {
                                ErrorObject newError = null;
                                try
                                {
                                    if (msg1012)
                                    {
                                        var msgParams = error.MessageParams;
                                        var newMsgParams = new[]
                                        {
                                            (string) msgParams[0],
                                            (string) msgParams[1]
                                        };
                                        newError = new ErrorObject(error.SeverityLevel, "MSG_1013", newMsgParams,
                                            error.ErrorStackTrace);
                                    }
                                    else if (msg1015)
                                    {
                                        var msgParams = error.MessageParams;
                                        var newMsgParams = new[]
                                        {
                                            (string) msgParams[0],
                                            (string) msgParams[1]
                                        };
                                        newError = new ErrorObject(error.SeverityLevel, "MSG_1016", newMsgParams,
                                            error.ErrorStackTrace);
                                    }
                                    else
                                    {
                                        var errMessage = error.ToString();
                                        newError = PopulateCommError(PayflowConstants.EUnknownState,
                                            null, error.SeverityLevel, true, errMessage);
                                    }
                                }
                                catch
                                {
                                    newError = null;
                                }

                                if (newError != null) retVal.Add(newError);
                            }
                        }
                        else
                        {
                            if (msg1012 || msg1015)
                            {
                                retVal.Add(error);
                            }
                            else
                            {
                                ErrorObject newError = null;
                                try
                                {
                                    if (msg1013)
                                    {
                                        var msgParams = error.MessageParams;
                                        var newMsgParams = new[]
                                        {
                                            (string) msgParams[2],
                                            (string) msgParams[3]
                                        };
                                        newError = new ErrorObject(error.SeverityLevel, "MSG_1012", newMsgParams,
                                            error.ErrorStackTrace);
                                    }
                                    else if (msg1016)
                                    {
                                        var msgParams = error.MessageParams;
                                        var newMsgParams = new[]
                                        {
                                            (string) msgParams[1],
                                            (string) msgParams[2]
                                        };
                                        newError = new ErrorObject(error.SeverityLevel, "MSG_1015", newMsgParams,
                                            error.ErrorStackTrace);
                                    }
                                    else
                                    {
                                        var errMessage = error.ToString();
                                        newError = PopulateCommError(PayflowConstants.EUnknownState,
                                            null, error.SeverityLevel, false, errMessage);
                                    }
                                }
                                catch
                                {
                                    newError = null;
                                }

                                if (newError != null) retVal.Add(newError);
                            }
                        }
                    }
                    else
                    {
                        ErrorObject newError = null;
                        try
                        {
                            var errMessage = error.ToString();
                            if (errMessage != null && errMessage.Length > 0)
                            {
                                var result = PayflowConstants.EmptyString;
                                var respMsg = PayflowConstants.EmptyString;
                                var errIsXmlPay = errMessage.IndexOf(PayflowConstants.XmlRespId) >= 0;
                                //Check whether the error string is in nvp format
                                // or xml pay format.
                                if (errIsXmlPay)
                                {
                                    //Try to get values in nodes Result, RespMsg
                                    result = GetXmlPayNodeValue(errMessage,
                                        PayflowConstants.XmlParamResult);
                                    respMsg = GetXmlPayNodeValue(errMessage,
                                        PayflowConstants.XmlParamMessage);
                                }
                                else
                                {
                                    //Try to get RESULT , RESPMSG from the error if 
                                    // available.
                                    result = LocateValueForName(errMessage, PayflowConstants.ParamResult,
                                        false);
                                    respMsg = LocateValueForName(errMessage,
                                        PayflowConstants.ParamRespmsg, false);
                                }

                                if (result != null && result.Length > 0 && respMsg != null && respMsg.Length > 0)
                                {
                                    var newErrMessage = new StringBuilder("");
                                    if (isXmlPayRequest && !errIsXmlPay)
                                    {
                                        newErrMessage =
                                            new StringBuilder("<XMLPayResponse xmlns='http://www.paypal.com/XMLPay'");

                                        newErrMessage.Append(
                                            "><ResponseData><TransactionResults><TransactionResult><Result>");
                                        newErrMessage.Append(result);
                                        newErrMessage.Append("</Result><Message>");
                                        newErrMessage.Append(respMsg);
                                        newErrMessage.Append(
                                            "</Message></TransactionResult></TransactionResults></ResponseData></XMLPayResponse>");
                                        newError = new ErrorObject(error.SeverityLevel, "", newErrMessage.ToString());
                                    }
                                    else if (!isXmlPayRequest && errIsXmlPay)
                                    {
                                        newErrMessage = new StringBuilder(PayflowConstants.ParamResult);
                                        newErrMessage.Append(PayflowConstants.SeparatorNvp);
                                        newErrMessage.Append(result);
                                        newErrMessage.Append(PayflowConstants.DelimiterNvp);
                                        newErrMessage.Append(PayflowConstants.ParamRespmsg);
                                        newErrMessage.Append(PayflowConstants.SeparatorNvp);
                                        newErrMessage.Append(respMsg);

                                        newError = new ErrorObject(error.SeverityLevel, "", newErrMessage.ToString());
                                    }
                                    else
                                    {
                                        newError = new ErrorObject(error.SeverityLevel, "", errMessage);
                                    }
                                }
                                else
                                {
                                    var newErrMessage = new StringBuilder("");
                                    if (isXmlPayRequest)
                                    {
                                        newErrMessage =
                                            new StringBuilder("<XMLPayResponse xmlns='http://www.paypal.com/XMLPay'");

                                        newErrMessage.Append(
                                            "><ResponseData><TransactionResults><TransactionResult><Result>");
                                        newErrMessage.Append(
                                            (string) PayflowConstants.CommErrorCodes[PayflowConstants.EUnknownState]);
                                        newErrMessage.Append("</Result><Message>");
                                        newErrMessage.Append(
                                            (string) PayflowConstants.CommErrorMessages[
                                                PayflowConstants.EUnknownState] + " " + errMessage);
                                        newErrMessage.Append(
                                            "</Message></TransactionResult></TransactionResults></ResponseData></XMLPayResponse>");
                                    }
                                    else
                                    {
                                        newErrMessage = new StringBuilder(PayflowConstants.ParamResult);
                                        newErrMessage.Append(PayflowConstants.SeparatorNvp);
                                        newErrMessage.Append(
                                            (string) PayflowConstants.CommErrorCodes[PayflowConstants.EUnknownState]);
                                        newErrMessage.Append(PayflowConstants.DelimiterNvp);
                                        newErrMessage.Append(PayflowConstants.ParamRespmsg);
                                        newErrMessage.Append(PayflowConstants.SeparatorNvp);
                                        newErrMessage.Append(
                                            (string) PayflowConstants.CommErrorMessages[
                                                PayflowConstants.EUnknownState] + " " + errMessage);
                                    }

                                    newError = new ErrorObject(error.SeverityLevel, "", newErrMessage.ToString());
                                }
                            }
                        }
                        catch
                        {
                            newError = null;
                        }

                        if (newError != null) retVal.Add(newError);
                    }
            }

            return retVal;
        }

        private static readonly Dictionary<string, string> Configuration = new Dictionary<string, string>();

        private static bool _isConfigured;

        public static void Configure(IConfigurationSection configuration)
        {
            if (!_isConfigured)
                lock (Configuration)
                {
                    if (!_isConfigured)
                    {
                        _isConfigured = true;

                        foreach (var item in configuration.GetChildren()) Configuration.Add(item.Key, item.Value);
                    }
                }
        }

        /// <summary>
        ///     Returns AppSettings
        /// </summary>
        /// <param name="appSettingsKey"></param>
        /// <returns></returns>
        public static string AppSettings(string appSettingsKey)
        {
            if (Configuration.TryGetValue(appSettingsKey, out var result)) return result;

            return null;
        }

        #endregion
    }
}