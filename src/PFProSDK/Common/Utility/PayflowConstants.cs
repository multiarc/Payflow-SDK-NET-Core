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

#endregion

namespace PFProSDK.Common.Utility
{
    /// <summary>
    ///     Summary description for PayflowConstants.
    /// </summary>
    public sealed class PayflowConstants
    {
        #region "Private Constructor"

        /// <summary>
        ///     Private constructor
        ///     for PayflowConstants
        /// </summary>
        private PayflowConstants()
        {
        }

        #endregion

        /// <summary>
        ///     Populates Error code hash table
        /// </summary>
        /// <returns>Error code hash table</returns>
        private static Hashtable PopulateErrorCodes()
        {
            var errorCodeTable = new Hashtable();
            errorCodeTable.Add(ESokConnFailed, "-1");
            errorCodeTable.Add(EParmName, "-6");
            errorCodeTable.Add(EParmNameLen, "-7");
            errorCodeTable.Add(ETimeoutWaitResp, "-12");
            errorCodeTable.Add(ENullHostString, "-23");
            errorCodeTable.Add(EInvalidTimeout, "-30");
            errorCodeTable.Add(ECommonName, "-32");
            errorCodeTable.Add(EMissingRequestId, "-41");
            errorCodeTable.Add(EEmptyParamList, "-100");
            errorCodeTable.Add(EContxtInitFailed, "-103");
            errorCodeTable.Add(EUnknownState, "-104");
            errorCodeTable.Add(EInvalidNvp, "-105");
            errorCodeTable.Add(EResponseFormatError, "-106");
            errorCodeTable.Add(EVersionNotSupported, "-107");
            errorCodeTable.Add(EConfigError, "-109");
            errorCodeTable.Add(ELogError, "-110");
            errorCodeTable.Add(EMsgfileInitError, "-111");
            errorCodeTable.Add(ECurrencyProcessError, "-113");
            return errorCodeTable;
        }

        /// <summary>
        ///     Populates Message code hash table
        /// </summary>
        /// <returns>Message code hash table</returns>
        private static Hashtable PopulateErrorMessages()
        {
            var errorMessageTable = new Hashtable();
            errorMessageTable.Add(ESokConnFailed, "Failed to connect to host");
            errorMessageTable.Add(EParmName, "Parameter list format error: & in name");
            errorMessageTable.Add(EParmNameLen, "Parameter list format error: invalid [] name length clause");
            errorMessageTable.Add(ETimeoutWaitResp, "Timeout waiting for response");
            errorMessageTable.Add(ENullHostString, "Host address not specified");
            errorMessageTable.Add(EInvalidTimeout, "Invalid timeout value");
            errorMessageTable.Add(EMissingRequestId, "Required Request ID not found in request.");
            errorMessageTable.Add(EEmptyParamList, "Parameter List cannot be empty");
            errorMessageTable.Add(EContxtInitFailed, "Context Initialization failed");
            errorMessageTable.Add(EUnknownState, "Unexpected transaction state");
            errorMessageTable.Add(EInvalidNvp, "Invalid name value pair request");
            errorMessageTable.Add(EResponseFormatError, "Invalid response format");
            errorMessageTable.Add(EVersionNotSupported, "This XMLPay Version is not supported");
            errorMessageTable.Add(EConfigError, ConfigError);
            errorMessageTable.Add(ELogError, LoggingError);
            errorMessageTable.Add(EMsgfileInitError, "Could not initialize from message file.");
            errorMessageTable.Add(ECurrencyProcessError,
                "Unable to round and truncate the currency value simultaneously. You can set only one of the two properties Round OR Truncate in the Data Object Currency.");
            errorMessageTable.Add(ECommonName,
                "The certificate chain did not validate, common name did not match URL.");
            return errorMessageTable;
        }

        // Stores the Client Version - Update as needed.

        #region "Communication Objects Related Constants"

        //Default timeout in seconds
        /// <summary>
        ///     Default Timeout in seconds (45 sec)
        /// </summary>
        internal const int DefaultTimeout = 45;

        /// <summary>
        ///     Default Connection reconnect Time span in seconds (3 sec)
        /// </summary>
        internal const int DefaultRetryconnectiontime = 3000;

        /// <summary>
        ///     Default Payflow Host port (443)
        /// </summary>
        internal const int DefaultHostport = 443;

        /// <summary>
        ///     SDK Client Prefix
        /// </summary>
        internal const string ClientPrefix = "PNCLIENT";

        /// <summary>
        ///     SDK Client Type (N --> .NET)
        /// </summary>
        internal const string ClientType = "N";

        // if you change the SDK Client Version (CLIENT_VERSION) below, remember to 
        // change the assembly version in the AssemblyInfo.cs to match.
        /// <summary>
        ///     SDK Client Version (400 --> V4 protocol)
        /// </summary>
        internal const string ClientVersion = "450";

        /// <summary>
        ///     SDK User Agent (Payflow SDK for .NET)
        /// </summary>
        internal const string UserAgent = "Payflow SDK for .NET";

        /// <summary>
        ///     SDK Request Type
        /// </summary>
        internal const string StrongAssembly = "Strong";

        /// <summary>
        ///     SDK Request Type
        /// </summary>
        internal const string WeakAssembly = "Weak";

        /// <summary>
        ///     Maximum retry attempts (3)
        /// </summary>
        internal const int MaxRetry = 3;

        /// <summary>
        ///     XmlPay ID
        /// </summary>
        internal const string XmlId = "<XMLPayRequest";

        /// <summary>
        ///     XmlPay Response ID
        /// </summary>
        internal const string XmlRespId = "<XMLPayResponse";

        /// <summary>
        ///     XML Content Type (text/xml)
        /// </summary>
        internal const string XmlContentType = "text/xml";

        /// <summary>
        ///     Name value Content Type (text/namevalue)
        /// </summary>
        internal const string NvContentType = "text/namevalue";

        /// <summary>
        ///     XML Pay namespace (http://www.paypal.com/XMLPay)
        /// </summary>
        internal const string XmlpayNamespace = "http://www.paypal.com/XMLPay";

        /// <summary>
        ///     XML Pay request tag (XMLPayRequest)
        /// </summary>
        internal const string XmlpayRequestTag = "XMLPayRequest";

        #endregion

        #region "General Constants"

        /// <summary>
        ///     Empty String
        /// </summary>
        public const string EmptyString = "";

        /// <summary>
        ///     Default Country Code
        /// </summary>
        internal const string CurrencycodeDefault = CurrencycodeUsd;

        /// <summary>
        ///     Currency code for USD
        /// </summary>
        internal const string CurrencycodeUsd = "USD"; // 05/09/07 Changed to USD from 840 as default. tsieber

        /// <summary>
        ///     NVP Delimiter (&amp;)
        /// </summary>
        internal const string DelimiterNvp = "&";

        /// <summary>
        ///     NVP Opening Brace ([)
        /// </summary>
        internal const string OpeningBraceNvp = "[";

        /// <summary>
        ///     NVP Closing Brace (])
        /// </summary>
        internal const string ClosingBraceNvp = "]";

        /// <summary>
        ///     Constant Underscore
        /// </summary>
        internal const string Underscore = "_";

        /// <summary>
        ///     Constant DOT
        /// </summary>
        internal const string Dot = ".";

        /// <summary>
        ///     NVP Separator (=)
        /// </summary>
        internal const string SeparatorNvp = "=";

        /// <summary>
        ///     Numeric values initialize
        /// </summary>
        internal const long InvalidNumber = -384783;

        /// <summary>
        ///     Duplicate tag
        /// </summary>
        internal const string TagDuplicate = "DUPLIACTE_NAME_KEY";

        /// <summary>
        ///     Xml Pay Param Result
        /// </summary>
        internal const string XmlParamResult = "Result";

        /// <summary>
        ///     Xml Pay param Message
        /// </summary>
        internal const string XmlParamMessage = "Message";

        #endregion

        #region "Tender Related Constants"

        /// <summary>
        ///     Card Tender (C)
        /// </summary>
        internal const string TendertypeCard = "C";

        /// <summary>
        ///     ACH Tender (A)
        /// </summary>
        internal const string TendertypeAch = "A";

        /// <summary>
        ///     Telecheck Tender (K)
        /// </summary>
        internal const string TendertypeTelecheck = "K";

        /// <summary>
        ///     PayPal Tender (P)
        /// </summary>
        internal const string TendertypePaypal = "P";

        #region "ACH Tender Related Constants"

        /// <summary>
        ///     ACH Authtype CCD
        /// </summary>
        internal const string AchAuthTypeCcd = "CCD";

        /// <summary>
        ///     ACH Authtype PPD
        /// </summary>
        internal const string AchAuthTypePpd = "PPD";

        /// <summary>
        ///     ACH Authtype POP
        /// </summary>
        internal const string AchAuthTypePop = "POP";

        /// <summary>
        ///     ACH Authtype WEB
        /// </summary>
        internal const string AchAuthTypeWeb = "WEB";

        /// <summary>
        ///     ACH Authtype RCK
        /// </summary>
        internal const string AchAuthTypeRck = "RCK";

        /// <summary>
        ///     ACH Authtype ARC
        /// </summary>
        internal const string AchAuthTypeArc = "ARC";

        /// <summary>
        ///     ACH Authtype TEL
        /// </summary>
        internal const string AchAuthTypeTel = "TEL";

        #endregion

        #endregion

        #region "Transaction Type Related Constants"

        /// <summary>
        ///     Authorization TrxType (A)
        /// </summary>
        internal const string TrxtypeAuth = "A";

        /// <summary>
        ///     Sale TrxType (S)
        /// </summary>
        internal const string TrxtypeSale = "S";

        /// <summary>
        ///     Credit Trxtype (C)
        /// </summary>
        internal const string TrxtypeCredit = "C";

        /// <summary>
        ///     Voice Authorization Trxtype (F)
        /// </summary>
        internal const string TrxtypeVoiceauth = "F";

        /// <summary>
        ///     Capture Trxtype (D)
        /// </summary>
        internal const string TrxtypeCapture = "D";

        /// <summary>
        ///     Inquiry Trxtype (I)
        /// </summary>
        internal const string TrxtypeInquiry = "I";

        /// <summary>
        ///     Void Trxtype (V)
        /// </summary>
        internal const string TrxtypeVoid = "V";

        /// <summary>
        ///     Recurring Trxtype (R)
        /// </summary>
        internal const string TrxtypeRecurring = "R";

        /// <summary>
        ///     Fraud Review Trxtype (U)
        /// </summary>
        internal const string TrxtypeFraudapprove = "U";

        /// <summary>
        ///     Buyer auth Verify Enrollment Trxtype (E)
        /// </summary>
        internal const string TrxtypeBuyerauthVe = "E";

        /// <summary>
        ///     Buyer auth Validate Authentication Trxtype (Z)
        /// </summary>
        internal const string TrxtypeBuyerauthVa = "Z";

        #endregion

        #region "RMS Related Constants"

        /// <summary>
        ///     RMS Update Action Approve
        /// </summary>
        internal const string RmsUpdateactionApprove = "RMS_APPROVE";

        /// <summary>
        ///     RMS Update Action Decline
        /// </summary>
        internal const string RmsUpdateactionDecline = "RMS_MERCHANT_DECLINE";

        #endregion

        #region "Recurring Transaction Related Constants"

        #region "Action Related Constants"

        /// <summary>
        ///     Recurring Action Add (A)
        /// </summary>
        internal const string RecurringActionAdd = "A";

        /// <summary>
        ///     Recurring Action Modify (M)
        /// </summary>
        internal const string RecurringActionModify = "M";

        /// <summary>
        ///     Recurring Action Reactivate (R)
        /// </summary>
        internal const string RecurringActionReactivate = "R";

        /// <summary>
        ///     Recurring Action Cancel (C)
        /// </summary>
        internal const string RecurringActionCancel = "C";

        /// <summary>
        ///     Recurring Action Inquiry (I)
        /// </summary>
        internal const string RecurringActionInquiry = "I";

        /// <summary>
        ///     Recurring Action Payment (P)
        /// </summary>
        internal const string RecurringActionPayment = "P";

        #endregion

        #region "Payment Period Related Constants"

        /// <summary>
        ///     Recurring PayPeriod Every Week (WEEK)
        /// </summary>
        internal const string RecurringPayperiodWeek = "WEEK";

        /// <summary>
        ///     Recurring PayPeriod Every Two Weeks (BIWK)
        /// </summary>
        internal const string RecurringPayperiodBiwk = "BIWK";

        /// <summary>
        ///     Recurring PayPeriod Twice Every Month (SMMO)
        /// </summary>
        internal const string RecurringPayperiodSmmo = "SMMO";

        /// <summary>
        ///     Recurring PayPeriod Every Four Weeks (FRWK)
        /// </summary>
        internal const string RecurringPayperiodFrwk = "FRWK";

        /// <summary>
        ///     Recurring PayPeriod Every Month (MONT)
        /// </summary>
        internal const string RecurringPayperiodMont = "MONT";

        /// <summary>
        ///     Recurring PayPeriod Every Quarter (QTER)
        /// </summary>
        internal const string RecurringPayperiodQter = "QTER";

        /// <summary>
        ///     Recurring PayPeriod Twice Every Year (SMYR)
        /// </summary>
        internal const string RecurringPayperiodSmyr = "SMYR";

        /// <summary>
        ///     Recurring PayPeriod Every Year (YEAR)
        /// </summary>
        internal const string RecurringPayperiodYear = "YEAR";

        #endregion

        #endregion

        #region "Constants for Message Code"

        /// <summary>
        ///     Error Message Constant for communication error (MSG_1012)
        /// </summary>
        internal const string MsgCommunicationError = "MSG_1012";

        /// <summary>
        ///     Error Message Constant for communication error xml pay (MSG_1005)
        /// </summary>
        internal const string MsgCommunicationErrorXmlpay = "MSG_1013";

        /// <summary>
        ///     Error Message Constant for communication error (MSG_1012)
        /// </summary>
        internal const string MsgCommunicationErrorNoResponseId = "MSG_1015";

        /// <summary>
        ///     Error Message Constant for communication error xml pay (MSG_1005)
        /// </summary>
        internal const string MsgCommunicationErrorXmlpayNoResponseId = "MSG_1016";

        #endregion

        #region "Check Type"

        /// <summary>
        ///     Check type Personal (P)
        /// </summary>
        internal const string CheckTypePersonel = "P";

        /// <summary>
        ///     Check type Corporate (C)
        /// </summary>
        internal const string CheckTypeCorporate = "C";

        #endregion

        #region "Communication Error HashTable"

        /// <summary>
        ///     Communication Error Codes
        /// </summary>
        internal static Hashtable CommErrorCodes = PopulateErrorCodes();

        /// <summary>
        ///     Communication Error Messages
        /// </summary>
        internal static Hashtable CommErrorMessages = PopulateErrorMessages();

        #endregion

        #region "Communication Header Related constants"

        /// <summary>
        ///     Http Header PAYFLOW-Request-ID
        /// </summary>
        internal const string PayflowheaderRequestId = "X-VPS-Request-ID";

        /// <summary>
        ///     Http Header PAYFLOW-CLIENT-TIMEOUT
        /// </summary>
        internal const string PayflowheaderTimeout = "X-VPS-CLIENT-TIMEOUT";

        #endregion

        //Constants for Exception Framework

        #region "Config Constants"

        /// <summary>
        ///     Holds the name of the key used to get the message file name.
        /// </summary>
        internal const string MessageFileName = "MSG_FILE";

        /// <summary>
        ///     Holds the key name which holds the Log file name
        /// </summary>
        internal const string ConfigLogfileName = "LOG_FILE";

        /// <summary>
        ///     Holds the key name which holds the limit for Log file size
        /// </summary>
        internal const string ConfigLogfileSize = "LOGFILE_SIZE";

        /// <summary>
        ///     Holds the key name which holds the Logging level
        /// </summary>
        internal const string ConfigLogLevel = "LOG_LEVEL";

        /// <summary>
        ///     Holds the default Log file name
        /// </summary>
        //internal static String LOGFILE_NAME = AppDomain.CurrentDomain.BaseDirectory + "PayflowSDK.log";
        internal static string LogfileName = "PayflowSDK.log";

        /// <summary>
        ///     Holds the default limit for Log file size
        /// </summary>
        internal const int LogfileSize = 10230000;

        /// <summary>
        ///     Logging Level OFF
        /// </summary>
        public const int LoggingOff = 0;

        #endregion

        #region "Message file constants"

        /// <summary>
        ///     Holds the name of the key in the message file that holds the message code.
        /// </summary>
        internal const string MessageId = "Id";

        /// <summary>
        ///     Holds the name of the key in the message file that holds the message body.
        /// </summary>
        internal const string MessageBody = "Body";

        /// <summary>
        ///     Holds the name of the key in the message file that holds the message severity.
        /// </summary>
        internal const string MessageSeverity = "Severity";

        /// <summary>
        ///     Tag for TRACE in config file
        /// </summary>
        internal const string TraceTag = "TRACE";

        /// <summary>
        ///     Default value of Trace
        /// </summary>
        internal const string TraceDefault = TraceOff;

        /// <summary>
        ///     On value for Trace
        /// </summary>
        internal const string TraceOn = "ON";

        /// <summary>
        ///     Off value for Trace
        /// </summary>
        internal const string TraceOff = "OFF";

        /// <summary>
        ///     Trace value
        /// </summary>
        internal static string Trace = TraceDefault;

        #endregion

        #region "Severity Level constants"

        //--------------------------------------------------------
        //Holds the constants defining the error severity levels
        /// <summary>
        ///     Severity for a FATAL level message.
        /// </summary>
        internal const string ErrorFatal = "FATAL";

        /// <summary>
        ///     Severity for a ERROR level message.
        /// </summary>
        internal const string ErrorError = "ERROR";

        /// <summary>
        ///     Severity for a WARN level message.
        /// </summary>
        internal const string ErrorWarn = "WARN";

        /// <summary>
        ///     Severity for a INFO level message.
        /// </summary>
        internal const string ErrorInfo = "INFO";

        /// <summary>
        ///     Severity for a DEBUG level message.
        /// </summary>
        internal const string ErrorDebug = "DEBUG";
        //--------------------------------------------------------

        //--------------------------------------------------------
        //Holds the integer constants defining the error severity levels
        /// <summary>
        ///     Severity for a FATAL level message.
        /// </summary>
        public const int SeverityFatal = 5;

        /// <summary>
        ///     Severity for a ERROR level message.
        /// </summary>
        public const int SeverityError = 4;

        /// <summary>
        ///     Severity for a WARN level message.
        /// </summary>
        public const int SeverityWarn = 3;

        /// <summary>
        ///     Severity for a INFO level message.
        /// </summary>
        public const int SeverityInfo = 2;

        /// <summary>
        ///     Severity for a DEBUG level message.
        /// </summary>
        public const int SeverityDebug = 1;
        //--------------------------------------------------------

        #endregion

        #region "Messages description"

        //Constants for the Exception
        /// <summary>
        ///     Represents the description for error in the configuration file.
        /// </summary>
        internal const string ConfigError = "Unable to read from the configuration file. ";

        /// <summary>
        ///     Represents the description for error occurred during the logging process.
        /// </summary>
        internal const string LoggingError = "Log error: ";

        /// <summary>
        ///     Error occurred while formatting message.
        /// </summary>
        internal const string MessageFormattingError = "Error occurred while formatting the message. ";

        /// <summary>
        ///     Error occurred while Adding invalid client(Wrapper) header.
        /// </summary>
        internal const string MessageWrapperheaderError = "Error occurred while Adding invalid client(Wrapper) header.";

        /// <summary>
        ///     Error occurred while Writting in to Log file
        /// </summary>
        internal const string MessageLogError = "Unable to log messages. ";

        /// <summary>
        ///     Error occurred while KEYs are not present in config file for Log
        /// </summary>
        internal const string MessageConfigLogError = "Key does not exist in config file:";

        #endregion

        #region "String formation constants"

        //Constants used for Formatting the message string
        /// <summary>
        ///     Format Message Separator (Message)
        /// </summary>
        internal const string FormatMsgSeperator = "Message ";

        /// <summary>
        ///     Format Message Line Separator (---------------------)
        /// </summary>
        internal static string FormatMsgLineseperator = Environment.NewLine;

        /// <summary>
        ///     Format Message  Open Bracket ([)
        /// </summary>
        internal const string FormatMsgOpenbracket = "[";

        /// <summary>
        ///     Format Message  Close Bracket (])
        /// </summary>
        internal const string FormatMsgClosebracket = "]";

        /// <summary>
        ///     Format Message Code body Separator (-)
        /// </summary>
        internal const string FormatMsgCodebodySep = "-";
        //

        #endregion

        #region "Communication Errors Constants"

        /// <summary>
        ///     Communication Error Connection Failed
        /// </summary>
        internal const string ESokConnFailed = "E_SOK_CONN_FAILED";

        /// <summary>
        ///     Communication Error Param Name Error
        /// </summary>
        internal const string EParmName = "E_PARM_NAME";

        /// <summary>
        ///     Communication Error Param Name Length Error
        /// </summary>
        internal const string EParmNameLen = "E_PARM_NAME_LEN";

        /// <summary>
        ///     Communication Error Timeout Waiting for Response
        /// </summary>
        internal const string ETimeoutWaitResp = "E_TIMEOUT_WAIT_RESP";

        /// <summary>
        ///     Communication Error Null Host String
        /// </summary>
        internal const string ENullHostString = "E_NULL_HOST_STRING";

        /// <summary>
        ///     Communication Error Invalid Timeout
        /// </summary>
        internal const string EInvalidTimeout = "E_INVALID_TIMEOUT";

        /// <summary>
        ///     Communication Error Missing Request Id
        /// </summary>
        internal const string EMissingRequestId = "E_MISSING_REQUEST_ID";

        /// <summary>
        ///     Communication Error Empty Param List
        /// </summary>
        internal const string EEmptyParamList = "E_EMPTY_PARAM_LIST";

        /// <summary>
        ///     Communication Error Context Initialization Failed
        /// </summary>
        internal const string EContxtInitFailed = "E_CONTXT_INIT_FAILED";

        /// <summary>
        ///     Communication Error Unknown State
        /// </summary>
        internal const string EUnknownState = "E_UNKNOWN_STATE";

        /// <summary>
        ///     Communication Error Invalid Name Value Pair Request
        /// </summary>
        internal const string EInvalidNvp = "E_INVALID_NVP";

        /// <summary>
        ///     Communication Error Response Format Error
        /// </summary>
        internal const string EResponseFormatError = "E_RESPONSE_FORMAT_ERROR";

        /// <summary>
        ///     Communication Error Version Not Supported
        /// </summary>
        internal const string EVersionNotSupported = "E_VERSION_NOT_SUPPORTED";

        /// <summary>
        ///     Internal Error Configuration Error.
        /// </summary>
        internal const string EConfigError = "E_CONFIG_ERROR";

        /// <summary>
        ///     Internal Error Log error.
        /// </summary>
        internal const string ELogError = "E_LOG_ERROR";

        /// <summary>
        ///     Internal Error Log4net initialization error.
        /// </summary>
        internal const string EMsgfileInitError = "E_MSGFILE_INIT_ERROR";

        /// <summary>
        ///     Internal Error currency Process error.
        /// </summary>
        internal const string ECurrencyProcessError = "E_CURRENCY_PROCESS_ERROR";

        /// <summary>
        ///     Internal Error currency Process error.
        /// </summary>
        internal const string ECommonName = "E_COMMON_NAME";

        #endregion

        #region "Payflow params constants"

        /// <summary>
        ///     Internal Param REQUEST
        /// </summary>
        internal const string IntlParamRequest = "REQUEST";

        /// <summary>
        ///     Internal Param FULLRESPONSE
        /// </summary>
        internal const string IntlParamFullresponse = "FULLRESPONSE";

        /// <summary>
        ///     Internal Param PAYFLOW_HOST
        /// </summary>
        internal const string IntlParamPayflowHost = "PAYFLOW_HOST";

        /// <summary>
        ///     Internal Param PAYFLOW_PORT
        /// </summary>
        internal const string IntlParamPayflowPort = "PAYFLOW_PORT";

        /// <summary>
        ///     Internal Param PAYFLOW_TIMEOUT
        /// </summary>
        internal const string IntlParamPayflowTimeout = "PAYFLOW_TIMEOUT";

        /// <summary>
        ///     Internal Param rule
        /// </summary>
        internal const string XmlParamRule = "rule";

        /// <summary>
        ///     Internal Param num
        /// </summary>
        internal const string XmlParamNum = "num";

        /// <summary>
        ///     Internal Param ruleId
        /// </summary>
        internal const string XmlParamRuleid = "ruleId";

        /// <summary>
        ///     Internal Param ruleAlias
        /// </summary>
        internal const string XmlParamRulealias = "ruleAlias";

        /// <summary>
        ///     Internal Param ruleDescription
        /// </summary>
        internal const string XmlParamRuledescription = "ruleDescription";

        /// <summary>
        ///     Internal Param action
        /// </summary>
        internal const string XmlParamAction = "action";

        /// <summary>
        ///     Internal Param triggeredMessage
        /// </summary>
        internal const string XmlParamTriggeredmessage = "triggeredMessage";

        /// <summary>
        ///     Internal Param rulevendorparms
        /// </summary>
        internal const string XmlParamRulevendorparms = "rulevendorparms";

        /// <summary>
        ///     Internal Param ruleParameter
        /// </summary>
        internal const string XmlParamRuleparameter = "ruleParameter";

        /// <summary>
        ///     Internal Param name
        /// </summary>
        internal const string XmlParamName = "name";

        /// <summary>
        ///     Internal Param value
        /// </summary>
        internal const string XmlParamValue = "value";

        /// <summary>
        ///     Internal Param type
        /// </summary>
        internal const string XmlParamType = "type";

        /// <summary>
        ///     XML Pay Param version
        /// </summary>
        internal const string XmlParamVersion = "version";

        /// <summary>
        ///     XML Pay Param Vendor
        /// </summary>
        internal const string XmlParamVendor = "Vendor";

        /// <summary>
        ///     XML Pay Param User
        /// </summary>
        internal const string XmlParamUser = "User";

        /// <summary>
        ///     XML Pay Param Partner
        /// </summary>
        internal const string XmlParamPartner = "Partner";

        /// <summary>
        ///     XML Pay Param Password
        /// </summary>
        internal const string XmlParamPassword = "Password";

        /// <summary>
        ///     XML Pay Param AcctNum
        /// </summary>
        internal const string XmlParamAcctnum = "AcctNum";

        /// <summary>
        ///     XML Pay Param CardNum
        /// </summary>
        internal const string XmlParamCardnum = "CardNum";

        /// <summary>
        ///     XML Pay Param ExpDate
        /// </summary>
        internal const string XmlParamExpdate = "ExpDate";

        /// <summary>
        ///     XML Pay Param MagData
        /// </summary>
        internal const string XmlParamMagdata = "MagData";

        /// <summary>
        ///     XML Pay Param MICR
        /// </summary>
        internal const string XmlParamMicr = "MICR";

        /// <summary>
        ///     XML Pay Param CVNum
        /// </summary>
        internal const string XmlParamCvnum = "CVNum";

        /// <summary>
        ///     XML Pay Param DL
        /// </summary>
        internal const string XmlParamDl = "DL";

        /// <summary>
        ///     XML Pay Param SS
        /// </summary>
        internal const string XmlParamSs = "SS";

        /// <summary>
        ///     XML Pay Param DOB
        /// </summary>
        internal const string XmlParamDob = "DOB";

        /// <summary>
        ///     XML Pay Start tag
        /// </summary>
        internal const string XmlParamStartTag = "<?xml version='1.0' encoding='UTF-8'?><XMLPayRequest version ='";

        /// <summary>
        ///     Payflow Param AUTHTYPE
        /// </summary>
        internal const string ParamAuthtype = "AUTHTYPE";

        /// <summary>
        ///     Payflow Param PRENOTE
        /// </summary>
        internal const string ParamPrenote = "PRENOTE";

        /// <summary>
        ///     Payflow Param TERMCITY
        /// </summary>
        internal const string ParamTermcity = "TERMCITY";

        /// <summary>
        ///     Payflow Param TERMSTATE
        /// </summary>
        internal const string ParamTermstate = "TERMSTATE";

        /// <summary>
        ///     Payflow Param ABA
        /// </summary>
        internal const string ParamAba = "ABA";

        /// <summary>
        ///     Payflow Param ACCTTYPE
        /// </summary>
        internal const string ParamAccttype = "ACCTTYPE";

        /// <summary>
        ///     Payflow Param TENDER
        /// </summary>
        internal const string ParamTender = "TENDER";

        /// <summary>
        ///     Payflow Param CHKNUM
        /// </summary>
        internal const string ParamChknum = "CHKNUM";

        /// <summary>
        ///     Payflow Param CHKTYPE
        /// </summary>
        internal const string ParamChktype = "CHKTYPE";

        /// <summary>
        ///     Payflow Param STREET
        /// </summary>
        internal const string ParamStreet = "BILLTOSTREET";

        /// <summary>
        ///     Payflow Param BILLTOSTREET2
        /// </summary>
        internal const string ParamBilltostreet2 = "BILLTOSTREET2";

        /// <summary>
        ///     Payflow Param CITY
        /// </summary>
        internal const string ParamCity = "BILLTOCITY";

        /// <summary>
        ///     Payflow Param STATE
        /// </summary>
        internal const string ParamState = "BILLTOSTATE";

        /// <summary>
        ///     Payflow Param COUNTRY
        /// </summary>
        internal const string ParamCountry = "COUNTRY";

        // Changed from BILLTOCOUNTRY to COUNTRY since Recurring
        // Billing does not map BILLTOCOUNTRY correctly.
        /// <summary>
        ///     Payflow Param BILLTOCOUNTRY
        /// </summary>
        internal const string ParamBilltocountry = "BILLTOCOUNTRY";

        /// <summary>
        ///     Payflow Param ZIP
        /// </summary>
        internal const string ParamZip = "BILLTOZIP";

        /// <summary>
        ///     Payflow Param PHONENUM
        /// </summary>
        internal const string ParamPhonenum = "BILLTOPHONENUM";

        /// <summary>
        ///     Payflow Param BILLTOPHONE2
        /// </summary>
        internal const string ParamBilltophone2 = "BILLTOPHONE2";

        /// <summary>
        ///     Payflow Param EMAIL
        /// </summary>
        internal const string ParamEmail = "BILLTOEMAIL";

        /// <summary>
        ///     Payflow Param FAX
        /// </summary>
        internal const string ParamFax = "BILLTOFAX";

        /// <summary>
        ///     Payflow Param FIRSTNAME
        /// </summary>
        internal const string ParamFirstname = "BILLTOFIRSTNAME";

        /// <summary>
        ///     Payflow Param MIDDLENAME
        /// </summary>
        internal const string ParamMiddlename = "BILLTOMIDDLENAME";

        /// <summary>
        ///     Payflow Param LASTNAME
        /// </summary>
        internal const string ParamLastname = "BILLTOLASTNAME";

        /// <summary>
        ///     Payflow Param HOMEPHONE
        /// </summary>
        internal const string ParamHomephone = "HOMEPHONE";

        /// <summary>
        ///     Payflow Param BROWSERTIME
        /// </summary>
        internal const string ParamBrowsertime = "BROWSERTIME";

        /// <summary>
        ///     Payflow Param BROWSERCOUNTRYCODE
        /// </summary>
        internal const string ParamBrowsercountrycode = "BROWSERCOUNTRYCODE";

        /// <summary>
        ///     Payflow Param ECHODATA
        /// </summary>
        internal const string ParamEchodata = "ECHODATA";

        /// <summary>
        ///     Payflow Param BROWSERUSERAGENT
        /// </summary>
        internal const string ParamBrowseruseragent = "BROWSERUSERAGENT";

        /// <summary>
        ///     Payflow Param ACSURL
        /// </summary>
        internal const string ParamAcsurl = "ACSURL";

        /// <summary>
        ///     Payflow Param AUTHENICATION_ID
        /// </summary>
        internal const string ParamAuthenicationId = "AUTHENTICATION_ID";

        /// <summary>
        ///     Payflow Param AUTHENICATION_STATUS
        /// </summary>
        internal const string ParamAuthenicationStatus = "AUTHENTICATION_STATUS";

        /// <summary>
        ///     Payflow Param CAVV
        /// </summary>
        internal const string ParamCavv = "CAVV";

        /// <summary>
        ///     Payflow Param ECI
        /// </summary>
        internal const string ParamEci = "ECI";

        /// <summary>
        ///     Payflow Param MD
        /// </summary>
        internal const string ParamMd = "MD";

        /// <summary>
        ///     Payflow Param PAREQ
        /// </summary>
        internal const string ParamPareq = "PAREQ";

        /// <summary>
        ///     Payflow Param XID
        /// </summary>
        internal const string ParamXid = "XID";

        /// <summary>
        ///     Payflow Param MICR
        /// </summary>
        internal const string ParamMicr = "MICR";

        /// <summary>
        ///     Payflow Param NAME
        /// </summary>
        internal const string ParamName = "NAME";

        /// <summary>
        ///     Payflow Param DL
        /// </summary>
        internal const string ParamDl = "DL";

        /// <summary>
        ///     Payflow Param SS
        /// </summary>
        internal const string ParamSs = "SS";

        /// <summary>
        ///     Payflow Param REQNAME
        /// </summary>
        internal const string ParamReqname = "REQNAME";

        /// <summary>
        ///     Payflow Param CUSTCODE
        /// </summary>
        internal const string ParamCustcode = "CUSTCODE";

        /// <summary>
        ///     Payflow Param CUSTIP
        /// </summary>
        internal const string ParamCustip = "CUSTIP";

        /// <summary>
        ///     Payflow Param CUSTHOSTNAME
        /// </summary>
        internal const string ParamCusthostname = "CUSTHOSTNAME";

        /// <summary>
        ///     Payflow Param CUSTBROWSER
        /// </summary>
        internal const string ParamCustbrowser = "CUSTBROWSER";

        /// <summary>
        ///     Payflow Param CUSTVATREGNUM
        /// </summary>
        internal const string ParamCustvatregnum = "CUSTVATREGNUM";

        /// <summary>
        ///     Payflow Param DOB
        /// </summary>
        internal const string ParamDob = "DOB";

        /// <summary>
        ///     Payflow Param CUSTID
        /// </summary>
        internal const string ParamCustid = "CUSTID";

        /// <summary>
        ///     Payflow Param COMPANYNAME
        /// </summary>
        internal const string ParamCompanyname = "COMPANYNAME";

        /// <summary>
        ///     Payflow Param CORPNAME
        /// </summary>
        internal const string ParamCorpname = "CORPNAME";

        /// <summary>
        ///     Payflow Param MERCHDESCR
        /// </summary>
        internal const string ParamMerchdescr = "MERCHDESCR";

        /// <summary>
        ///     Payflow Param MERCHSVC
        /// </summary>
        internal const string ParamMerchsvc = "MERCHSVC";

        /// <summary>
        ///     Payflow Param ADDLMSGS
        /// </summary>
        internal const string ParamAddlmsgs = "ADDLMSGS";

        /// <summary>
        ///     Payflow Param PREFPSMSG
        /// </summary>
        internal const string ParamPrefpsmsg = "PREFPSMSG";

        /// <summary>
        ///     Payflow Param POSTFPSMSG
        /// </summary>
        internal const string ParamPostfpsmsg = "POSTFPSMSG";

        /// <summary>
        ///     Payflow Param RESPTEXT
        /// </summary>
        internal const string ParamResptext = "RESPTEXT";

        /// <summary>
        ///     Payflow Param PROCAVS
        /// </summary>
        internal const string ParamProcavs = "PROCAVS";

        /// <summary>
        ///     Payflow Param PROCCARDSECURE
        /// </summary>
        internal const string ParamProccardsecure = "PROCCARDSECURE";

        /// <summary>
        ///     Payflow Param PROCCVV2
        /// </summary>
        internal const string ParamProccvv2 = "PROCCVV2";

        /// <summary>
        ///     Payflow Param HOSTCODE
        /// </summary>
        internal const string ParamHostcode = "HOSTCODE";

        /// <summary>
        ///     Payflow Param INVNUM
        /// </summary>
        internal const string ParamInvnum = "INVNUM";

        /// <summary>
        ///     Payflow Param AMT
        /// </summary>
        internal const string ParamAmt = "AMT";

        /// <summary>
        ///     Payflow Param TAXEXEMPT
        /// </summary>
        internal const string ParamTaxexempt = "TAXEXEMPT";

        /// <summary>
        ///     Payflow Param TAXAMT
        /// </summary>
        internal const string ParamTaxamt = "TAXAMT";

        /// <summary>
        ///     Payflow Param DUTYAMT
        /// </summary>
        internal const string ParamDutyamt = "DUTYAMT";

        /// <summary>
        ///     Payflow Param FREIGHTAMT
        /// </summary>
        internal const string ParamFreightamt = "FREIGHTAMT";

        /// <summary>
        ///     Payflow Param HANDLINGAMT
        /// </summary>
        internal const string ParamHandlingamt = "HANDLINGAMT";

        /// <summary>
        ///     Payflow Param SHIPPINGAMT
        /// </summary>
        internal const string ParamShippingamt = "SHIPPINGAMT";

        /// <summary>
        ///     Payflow Param DISCOUNT
        /// </summary>
        internal const string ParamDiscount = "DISCOUNT";

        /// <summary>
        ///     Payflow Param DESC
        /// </summary>
        internal const string ParamDesc = "DESC";

        /// <summary>
        ///     Payflow Param COMMENT1
        /// </summary>
        internal const string ParamComment1 = "COMMENT1";

        /// <summary>
        ///     Payflow Param COMMENT2
        /// </summary>
        internal const string ParamComment2 = "COMMENT2";

        /// <summary>
        ///     Payflow Param DESC1
        /// </summary>
        internal const string ParamDesc1 = "DESC1";

        /// <summary>
        ///     Payflow Param DESC2
        /// </summary>
        internal const string ParamDesc2 = "DESC2";

        /// <summary>
        ///     Payflow Param DESC3
        /// </summary>
        internal const string ParamDesc3 = "DESC3";

        /// <summary>
        ///     Payflow Param DESC4
        /// </summary>
        internal const string ParamDesc4 = "DESC4";

        /// <summary>
        ///     Payflow Param CUSTREF
        /// </summary>
        internal const string ParamCustref = "CUSTREF";

        /// <summary>
        ///     Payflow Param PONUM
        /// </summary>
        internal const string ParamPonum = "PONUM";

        /// <summary>
        ///     Payflow Param VATREGNUM
        /// </summary>
        internal const string ParamVatregnum = "VATREGNUM";

        /// <summary>
        ///     Payflow Param VATTAXAMT
        /// </summary>
        internal const string ParamVattaxamt = "VATTAXAMT";

        /// <summary>
        ///     Payflow Param LOCALTAXAMT
        /// </summary>
        internal const string ParamLocaltaxamt = "LOCALTAXAMT";

        /// <summary>
        ///     Payflow Param NATIONALTAXAMT
        /// </summary>
        internal const string ParamNationaltaxamt = "NATIONALTAXAMT";

        /// <summary>
        ///     Payflow Param ALTTAXAMT
        /// </summary>
        internal const string ParamAlttaxamt = "ALTTAXAMT";

        /// <summary>
        ///     Payflow Param COMMCODE
        /// </summary>
        internal const string ParamCommcode = "COMMCODE";

        /// <summary>
        ///     Payflow Param INVOICEDATE
        /// </summary>
        internal const string ParamInvoicedate = "INVOICEDATE";

        /// <summary>
        ///     Payflow Param STARTTIME
        /// </summary>
        internal const string ParamStarttime = "STARTTIME";

        /// <summary>
        ///     Payflow Param ENDTIME
        /// </summary>
        internal const string ParamEndtime = "ENDTIME";

        /// <summary>
        ///     Payflow Param ORDERDATE
        /// </summary>
        internal const string ParamOrderdate = "ORDERDATE";

        /// <summary>
        ///     Payflow Param ORDERTIME
        /// </summary>
        internal const string ParamOrdertime = "ORDERTIME";

        /// <summary>
        ///     Payflow Param L_AMTn
        /// </summary>
        internal const string ParamLAmt = "L_AMT";

        /// <summary>
        ///     Payflow Param L_COSTn
        /// </summary>
        internal const string ParamLCost = "L_COST";

        /// <summary>
        ///     Payflow Param L_FREIGHTAMTn
        /// </summary>
        internal const string ParamLFreightamt = "L_FREIGHTAMT";

        /// <summary>
        ///     Payflow Param L_HANDLINGAMTn
        /// </summary>
        internal const string ParamLHandlingamt = "L_HANDLINGAMT";

        /// <summary>
        ///     Payflow Param L_TAXAMTn
        /// </summary>
        internal const string ParamLTaxamt = "L_TAXAMT";

        /// <summary>
        ///     Payflow Param L_UOMn
        /// </summary>
        internal const string ParamLUom = "L_UOM";

        /// <summary>
        ///     Payflow Param L_PICKUPSTREETn
        /// </summary>
        internal const string ParamLPickupstreet = "L_PICKUPSTREET";

        /// <summary>
        ///     Payflow Param L_PICKUPSTATEn
        /// </summary>
        internal const string ParamLPickupstate = "L_PICKUPSTATE";

        /// <summary>
        ///     Payflow Param L_PICKUPCOUNTRYn
        /// </summary>
        internal const string ParamLPickupcountry = "L_PICKUPCOUNTRY";

        /// <summary>
        ///     Payflow Param L_PICKUPCITYn
        /// </summary>
        internal const string ParamLPickupcity = "L_PICKUPCITY";

        /// <summary>
        ///     Payflow Param L_PICKUPZIPn
        /// </summary>
        internal const string ParamLPickupzip = "L_PICKUPZIP";

        /// <summary>
        ///     Payflow Param L_DESCn
        /// </summary>
        internal const string ParamLDesc = "L_DESC";

        /// <summary>
        ///     Payflow Param L_DISCOUNTn
        /// </summary>
        internal const string ParamLDiscount = "L_DISCOUNT";

        /// <summary>
        ///     Payflow Param L_MANUFACTURERn
        /// </summary>
        internal const string ParamLManufacturer = "L_MANUFACTURER";

        /// <summary>
        ///     Payflow Param L_PRODCODEn
        /// </summary>
        internal const string ParamLProdcode = "L_PRODCODE";

        /// <summary>
        ///     Payflow Param ITEMAMT
        /// </summary>
        internal const string ParamItemamt = "ITEMAMT";

        /// <summary>
        ///     Payflow Param L_ITEMNUMBERn
        /// </summary>
        internal const string ParamLItemnumber = "L_ITEMNUMBER";

        /// <summary>
        ///     Payflow Param L_QTYn
        /// </summary>
        internal const string ParamLQty = "L_QTY";

        /// <summary>
        ///     Payflow Param L_SKUn
        /// </summary>
        internal const string ParamLSku = "L_SKU";

        /// <summary>
        ///     Payflow Param L_TAXRATEn
        /// </summary>
        internal const string ParamLTaxrate = "L_TAXRATE";

        /// <summary>
        ///     Payflow Param L_TAXTYPEn
        /// </summary>
        internal const string ParamLTaxtype = "L_TAXTYPE";

        /// <summary>
        ///     Payflow Param L_TYPEn
        /// </summary>
        internal const string ParamLType = "L_TYPE";

        /// <summary>
        ///     Payflow Param L_COMMCODEn
        /// </summary>
        internal const string ParamLCommcode = "L_COMMCODE";

        /// <summary>
        ///     Payflow Param L_TRACKINGNUMn
        /// </summary>
        internal const string ParamLTrackingnum = "L_TRACKINGNUM";

        /// <summary>
        ///     Payflow Param L_COSTCENTERNUMn
        /// </summary>
        internal const string ParamLCostcenternum = "L_COSTCENTERNUM";

        /// <summary>
        ///     Payflow Param L_CATALOGNUMn
        /// </summary>
        internal const string ParamLCatalognum = "L_CATALOGNUM";

        /// <summary>
        ///     Payflow Param L_UPCn
        /// </summary>
        internal const string ParamLUpc = "L_UPC";

        /// <summary>
        ///     Payflow Param L_UNSPSCCODE
        /// </summary>
        internal const string ParamLUnspsccode = "L_UNSPSCCODE";

        /// <summary>
        ///     Payflow Param L_NAME
        /// </summary>
        internal const string ParamLName = "L_NAME";

        /// <summary>
        ///     Payflow Param EXPDATE
        /// </summary>
        internal const string ParamExpdate = "EXPDATE";

        /// <summary>
        ///     Payflow Param CVV2
        /// </summary>
        internal const string ParamCvv2 = "CVV2";

        /// <summary>
        ///     Payflow Param ACCT
        /// </summary>
        internal const string ParamAcct = "ACCT";

        /// <summary>
        ///     Payflow Param COMMCARD
        /// </summary>
        internal const string ParamCommcard = "COMMCARD";

        /// <summary>
        ///     Payflow Param PROFILENAME
        /// </summary>
        internal const string ParamProfilename = "PROFILENAME";

        /// <summary>
        ///     Payflow Param START
        /// </summary>
        internal const string ParamStart = "START";

        /// <summary>
        ///     Payflow Param TERM
        /// </summary>
        internal const string ParamTerm = "TERM";

        /// <summary>
        ///     Payflow Param PAYPERIOD
        /// </summary>
        internal const string ParamPayperiod = "PAYPERIOD";

        /// <summary>
        ///     Payflow Param OPTIONALTRX
        /// </summary>
        internal const string ParamOptionaltrx = "OPTIONALTRX";

        /// <summary>
        ///     Payflow Param OPTIONALTRXAMT
        /// </summary>
        internal const string ParamOptionaltrxamt = "OPTIONALTRXAMT";

        /// <summary>
        ///     Payflow Param RETRYNUMDAYS
        /// </summary>
        internal const string ParamRetrynumdays = "RETRYNUMDAYS";

        /// <summary>
        ///     Payflow Param MAXFAILPAYMENTS
        /// </summary>
        internal const string ParamMaxfailpayments = "MAXFAILPAYMENTS";

        /// <summary>
        ///     Payflow Param NUMFAILPAYMENTS
        /// </summary>
        internal const string ParamNumfailpayments = "NUMFAILPAYMENTS";

        /// <summary>
        ///     Payflow Param ORIGPROFILEID
        /// </summary>
        internal const string ParamOrigprofileid = "ORIGPROFILEID";

        /// <summary>
        ///     Payflow Param PAYMENTHISTORY
        /// </summary>
        internal const string ParamPaymenthistory = "PAYMENTHISTORY";

        /// <summary>
        ///     Payflow Param PAYMENTNUM
        /// </summary>
        internal const string ParamPaymentnum = "PAYMENTNUM";

        /// <summary>
        ///     Payflow Param RECURRING
        /// </summary>
        internal const string ParamRecurring = "RECURRING";

        /// <summary>
        ///     Payflow Param PROFILEID
        /// </summary>
        internal const string ParamProfileid = "PROFILEID";

        /// <summary>
        ///     Payflow Param RPREF
        /// </summary>
        internal const string ParamRpref = "RPREF";

        /// <summary>
        ///     Payflow Param TRXPNREF
        /// </summary>
        internal const string ParamTrxpnref = "TRXPNREF";

        /// <summary>
        ///     Payflow Param TRXRESULT
        /// </summary>
        internal const string ParamTrxresult = "TRXRESULT";

        /// <summary>
        ///     Payflow Param TRXRESPMSG
        /// </summary>
        internal const string ParamTrxrespmsg = "TRXRESPMSG";

        /// <summary>
        ///     Payflow Param STATUS
        /// </summary>
        internal const string ParamStatus = "STATUS";

        /// <summary>
        ///     Payflow Param PAYMENTSLEFT
        /// </summary>
        internal const string ParamPaymentsleft = "PAYMENTSLEFT";

        /// <summary>
        ///     Payflow Param NEXTPAYMENT
        /// </summary>
        internal const string ParamNextpayment = "NEXTPAYMENT";

        /// <summary>
        ///     Payflow Param END
        /// </summary>
        internal const string ParamEnd = "END";

        /// <summary>
        ///     Payflow Param AGGREGATEAMT
        /// </summary>
        internal const string ParamAggregateamt = "AGGREGATEAMT";

        /// <summary>
        ///     Payflow Param AGGREGATEOPTIONALAMT
        /// </summary>
        internal const string ParamAggregateoptionalamt = "AGGREGATEOPTIONALAMT";

        /// <summary>
        ///     Payflow Param SHIPTOFIRSTNAME
        /// </summary>
        internal const string ParamShiptofirstname = "SHIPTOFIRSTNAME";

        /// <summary>
        ///     Payflow Param SHIPTOMIDDLENAME
        /// </summary>
        internal const string ParamShiptomiddlename = "SHIPTOMIDDLENAME";

        /// <summary>
        ///     Payflow Param SHIPTOLASTNAME
        /// </summary>
        internal const string ParamShiptolastname = "SHIPTOLASTNAME";

        /// <summary>
        ///     Payflow Param SHIPTOSTREET
        /// </summary>
        internal const string ParamShiptostreet = "SHIPTOSTREET";

        /// <summary>
        ///     Payflow Param SHIPTOCITY
        /// </summary>
        internal const string ParamShiptocity = "SHIPTOCITY";

        /// <summary>
        ///     Payflow Param SHIPTOSTATE
        /// </summary>
        internal const string ParamShiptostate = "SHIPTOSTATE";

        /// <summary>
        ///     Payflow Param SHIPTOZIP
        /// </summary>
        internal const string ParamShiptozip = "SHIPTOZIP";

        /// <summary>
        ///     Payflow Param SHIPTOCOUNTRY
        /// </summary>
        internal const string ParamShiptocountry = "SHIPTOCOUNTRY";

        /// <summary>
        ///     Payflow Param P_RESULTn
        /// </summary>
        internal const string ParamPResulTn = "P_RESULTn";

        /// <summary>
        ///     Payflow Param P_PNREFn
        /// </summary>
        internal const string ParamPPnreFn = "P_PNREFn";

        /// <summary>
        ///     Payflow Param P_TRANSTATEn
        /// </summary>
        internal const string ParamPTranstatEn = "P_TRANSTATEn";

        /// <summary>
        ///     Payflow Param P_TENDERn
        /// </summary>
        internal const string ParamPTendeRn = "P_TENDERn";

        /// <summary>
        ///     Payflow Param P_TRANSTIMEn
        /// </summary>
        internal const string ParamPTranstimEn = "P_TRANSTIMEn";

        /// <summary>
        ///     Payflow Param P_AMOUNTn
        /// </summary>
        internal const string ParamPAmTn = "P_AMTn";

        /// <summary>
        ///     Payflow Param FPS_PREXMLDATA
        /// </summary>
        internal const string ParamFpsPrexmldata = "FPS_PREXMLDATA";

        /// <summary>
        ///     Payflow Param FPS_POSTXMLDATA
        /// </summary>
        internal const string ParamFpsPostxmldata = "FPS_POSTXMLDATA";

        /// <summary>
        ///     Payflow Param SHIPTOSTREET2
        /// </summary>
        internal const string ParamShiptostreet2 = "SHIPTOSTREET2";

        /// <summary>
        ///     Payflow Param SHIPTOPHONE
        /// </summary>
        internal const string ParamShiptophone = "SHIPTOPHONE";

        /// <summary>
        ///     Payflow Param SHIPTOPHONE2
        /// </summary>
        internal const string ParamShiptophone2 = "SHIPTOPHONE2";

        /// <summary>
        ///     Payflow Param SHIPTOEMAIL
        /// </summary>
        internal const string ParamShiptoemail = "SHIPTOEMAIL";

        /// <summary>
        ///     Payflow Param SHIPCARRIER
        /// </summary>
        internal const string ParamShipcarrier = "SHIPCARRIER";

        /// <summary>
        ///     Payflow Param SHIPMETHOD
        /// </summary>
        internal const string ParamShipmethod = "SHIPMETHOD";

        /// <summary>
        ///     Payflow Param SHIPFROMZIP
        /// </summary>
        internal const string ParamShipfromzip = "SHIPFROMZIP";

        /// <summary>
        ///     Payflow Param SHIPPEDFROMZIP
        /// </summary>
        internal const string ParamShippedfromzip = "SHIPPEDFROMZIP";

        /// <summary>
        ///     Payflow Param SWIPE
        /// </summary>
        internal const string ParamSwipe = "SWIPE";

        /// <summary>
        ///     Payflow Param RESULT
        /// </summary>
        internal const string ParamResult = "RESULT";

        /// <summary>
        ///     Payflow Param PNREF
        /// </summary>
        internal const string ParamPnref = "PNREF";

        /// <summary>
        ///     Payflow Param RESPMSG
        /// </summary>
        internal const string ParamRespmsg = "RESPMSG";

        /// <summary>
        ///     Payflow Param AUTHCODE
        /// </summary>
        internal const string ParamAuthcode = "AUTHCODE";

        /// <summary>
        ///     Payflow Param AVSADDR
        /// </summary>
        internal const string ParamAvsaddr = "AVSADDR";

        /// <summary>
        ///     Payflow Param AVSZIP
        /// </summary>
        internal const string ParamAvszip = "AVSZIP";

        /// <summary>
        ///     Payflow Param CARDSECURE
        /// </summary>
        internal const string ParamCardsecure = "CARDSECURE";

        /// <summary>
        ///     Payflow Param CVV2MATCH
        /// </summary>
        internal const string ParamCvv2Match = "CVV2MATCH";

        /// <summary>
        ///     Payflow Param EMAILMATCH
        /// </summary>
        internal const string ParamEmailmatch = "EMAILMATCH";

        /// <summary>
        ///     Payflow Param PHONEMATCH
        /// </summary>
        internal const string ParamPhonematch = "PHONEMATCH";

        /// <summary>
        ///     Payflow Param IAVS
        /// </summary>
        internal const string ParamIavs = "IAVS";

        /// <summary>
        ///     Payflow Param ORIGRESULT
        /// </summary>
        internal const string ParamOrigresult = "ORIGRESULT";

        /// <summary>
        ///     Payflow Param TRANSSTATE
        /// </summary>
        internal const string ParamTransstate = "TRANSSTATE";

        /// <summary>
        ///     Payflow Param USER
        /// </summary>
        internal const string ParamUser = "USER";

        /// <summary>
        ///     Payflow Param VENDOR
        /// </summary>
        internal const string ParamVendor = "VENDOR";

        /// <summary>
        ///     Payflow Param PARTNER
        /// </summary>
        internal const string ParamPartner = "PARTNER";

        /// <summary>
        ///     Payflow Param PWD
        /// </summary>
        internal const string ParamPwd = "PWD";

        /// <summary>
        ///     Payflow Param TRXTYPE
        /// </summary>
        internal const string ParamTrxtype = "TRXTYPE";

        /// <summary>
        ///     Payflow Param VERBOSITY
        /// </summary>
        internal const string ParamVerbosity = "VERBOSITY";

        /// <summary>
        ///     Payflow Param PARES
        /// </summary>
        internal const string ParamPares = "PARES";

        /// <summary>
        ///     Payflow Param CURRENCY
        /// </summary>
        internal const string ParamCurrency = "CURRENCY";

        /// <summary>
        ///     Payflow Param PUR_DESC
        /// </summary>
        internal const string ParamPurDesc = "PUR_DESC";

        /// <summary>
        ///     Payflow Param ORIGID
        /// </summary>
        internal const string ParamOrigid = "ORIGID";

        /// <summary>
        ///     Payflow Param UPDATEACTION
        /// </summary>
        internal const string ParamUpdateaction = "UPDATEACTION";

        /// <summary>
        ///     Payflow Param ACTION
        /// </summary>
        internal const string ParamAction = "ACTION";

        /// <summary>
        ///     Payflow Param VIT_OSNAME
        /// </summary>
        internal const string ParamVitOsname = "VIT_OSNAME";

        /// <summary>
        ///     Payflow Param VIT_OSARCH
        /// </summary>
        internal const string ParamVitOsarch = "VIT_OSARCH";

        /// <summary>
        ///     Payflow Param VIT_OSVERSION
        /// </summary>
        internal const string ParamVitOsversion = "VIT_OSVERSION";

        /// <summary>
        ///     Payflow Param VIT_SDKRUNTIMEVERSION
        /// </summary>
        internal const string ParamVitSdkruntimeversion = "VIT_SDKRUNTIMEVERSION";

        /// <summary>
        ///     Payflow Param VIT_PROXY
        /// </summary>
        internal const string ParamVitProxy = "VIT_PROXY";

        /// <summary>
        ///     Payflow Param VIT_WRAPTYPE
        /// </summary>
        internal const string ParamVitWraptype = "VIT_WRAPTYPE";

        /// <summary>
        ///     Payflow Param VIT_WRAPVERSION
        /// </summary>
        internal const string ParamVitWrapversion = "VIT_WRAPVERSION";

        /// <summary>
        ///     Payflow Param REQUEST_ID
        /// </summary>
        internal const string ParamRequestId = "REQUEST_ID";

        /// <summary>
        ///     Payflow Param VATTAXPERCENT
        /// </summary>
        internal const string ParamVattaxpercent = "VATTAXPERCENT";

        /// <summary>
        ///     Payflow Param DUPLICATE
        /// </summary>
        internal const string ParamDuplicate = "DUPLICATE";

        /// <summary>
        ///     Payflow Param DATE_TO_SETTLE
        /// </summary>
        internal const string ParamDateToSettle = "DATE_TO_SETTLE";

        /// <summary>
        ///     Payflow Param BATCHID
        /// </summary>
        internal const string ParamBatchid = "BATCHID";

        /// <summary>
        ///     Recurring Inquiry Response Param Prefix
        /// </summary>
        internal const string PrefixRecurringInquiryResp = "P_";

        /// <summary>
        ///     Payflow Param SETTLE_DATE
        /// </summary>
        internal const string ParamSettleDate = "SETTLE_DATE";

        /// <summary>
        ///     Payflow Param ORIGPNREF
        /// </summary>
        internal const string ParamOrigpnref = "ORIGPNREF";

        /// <summary>
        ///     Payflow Param TOKEN
        /// </summary>
        internal const string ParamToken = "TOKEN";

        /// <summary>
        ///     Payflow Param MAXAMT
        /// </summary>
        internal const string ParamMaxamt = "MAXAMT";

        /// <summary>
        ///     Payflow Param RETURNURL
        /// </summary>
        internal const string ParamReturnurl = "RETURNURL";

        /// <summary>
        ///     Payflow Param CANCELURL
        /// </summary>
        internal const string ParamCancelurl = "CANCELURL";

        /// <summary>
        ///     Payflow Param REQCONFIRMSHIPPING
        /// </summary>
        internal const string ParamReqconfirmshipping = "REQCONFIRMSHIPPING";

        /// <summary>
        ///     Payflow Param NOSHIPPING
        /// </summary>
        internal const string ParamNoshipping = "NOSHIPPING";

        /// <summary>
        ///     Payflow Param ADDROVERRIDE
        /// </summary>
        internal const string ParamAddroverride = "ADDROVERRIDE";

        /// <summary>
        ///     Payflow Param LOCALECODE
        /// </summary>
        internal const string ParamLocalecode = "LOCALECODE";

        /// <summary>
        ///     Payflow Param PAGESTYLE
        /// </summary>
        internal const string ParamPagestyle = "PAGESTYLE";

        /// <summary>
        ///     Payflow Param HDRIMG
        /// </summary>
        internal const string ParamHdrimg = "HDRIMG";

        /// <summary>
        ///     Payflow Param HDRBORDERCOLOR
        /// </summary>
        internal const string ParamHdrbordercolor = "HDRBORDERCOLOR";

        /// <summary>
        ///     Payflow Param HDRBACKCOLOR
        /// </summary>
        internal const string ParamHdrbackcolor = "HDRBACKCOLOR";

        /// <summary>
        ///     Payflow Param PAYFLOWCOLOR
        /// </summary>
        internal const string ParamPayflowcolor = "PAYFLOWCOLOR";

        /// <summary>
        ///     Payflow Param POSTALCODE
        /// </summary>
        internal const string ParamPostalcode = "POSTALCODE";

        /// <summary>
        ///     Payflow Param COUNTRYCODE
        /// </summary>
        internal const string ParamCountrycode = "COUNTRYCODE";

        /// <summary>
        ///     Payflow Param PAYERID
        /// </summary>
        internal const string ParamPayerid = "PAYERID";

        /// <summary>
        ///     Payflow Param NOTIFYURL
        /// </summary>
        internal const string ParamNotifyurl = "NOTIFYURL";

        /// <summary>
        ///     Payflow Param L_XXXXn
        /// </summary>
        internal const string ParamItemnumber = "L_XXXXn";

        /// <summary>
        ///     Payflow Param BUTTONSOURCE
        /// </summary>
        internal const string ParamButtonsource = "BUTTONSOURCE";

        /// <summary>
        ///     Payflow Param PAYERSTATUS
        /// </summary>
        internal const string ParamPayerstatus = "PAYERSTATUS";

        /// <summary>
        ///     Payflow Param SHIPTOCOUNTRYCODE
        /// </summary>
        internal const string ParamShiptocountrycode = "SHIPTOCOUNTRYCODE";

        /// <summary>
        ///     Payflow Param SHIPTOBUSINESS
        /// </summary>
        internal const string ParamShiptobusiness = "SHIPTOBUSINESS";

        /// <summary>
        ///     Payflow Param ADDRSTATUS
        /// </summary>
        internal const string ParamAddrstatus = "ADDRSTATUS";

        /// <summary>
        ///     Payflow Param PPREF
        /// </summary>
        internal const string ParamPpref = "PPREF";

        /// <summary>
        ///     Payflow Param FEEAMT
        /// </summary>
        internal const string ParamFeeamt = "FEEAMT";

        /// <summary>
        ///     Payflow Param SETTLEAMT
        /// </summary>
        internal const string ParamSettleamt = "SETTLEAMT";

        /// <summary>
        ///     Payflow Param EXCHANGERATE
        /// </summary>
        internal const string ParamExchangerate = "EXCHANGERATE";

        /// <summary>
        ///     Payflow Param PENDINGREASON
        /// </summary>
        internal const string ParamPendingreason = "PENDINGREASON";

        /// <summary>
        ///     Payflow Param PAYMENTTYPE
        /// </summary>
        internal const string ParamPaymenttype = "PAYMENTTYPE";

        /// <summary>
        ///     Payflow Param PAYMENTDATE
        /// </summary>
        internal const string ParamPaymentdate = "PAYMENTDATE";

        /// <summary>
        ///     Payflow Param PAYMENTSTATUS
        /// </summary>
        internal const string ParamPaymentstatus = "PAYMENTSTATUS";

        /// <summary>
        ///     Payflow Param CUSTOM
        /// </summary>
        internal const string ParamCustom = "CUSTOM";

        /// <summary>
        ///     Payflow Param MERCHANTSESSIONID
        /// </summary>
        internal const string ParamMerchantsessionid = "MERCHANTSESSIONID";

        /// <summary>
        ///     Payflow Param ORDERDESC
        /// </summary>
        internal const string ParamOrderdesc = "ORDERDESC";

        /// <summary>
        ///     Payflow Param ORIGPPREF
        /// </summary>
        internal const string ParamOrigppref = "ORIGPPREF";

        /// <summary>
        ///     Payflow Param CARDSTART
        /// </summary>
        internal const string ParamCardstart = "CARDSTART";

        /// <summary>
        ///     Payflow Param CARDISSUE
        /// </summary>
        internal const string ParamCardissue = "CARDISSUE";

        /// <summary>
        ///     Payflow Param CORRELATIONID
        /// </summary>
        internal const string ParamCorrelationid = "CORRELATIONID";

        /// <summary>
        ///     Payflow Param CAPTURECOMPLETE
        /// </summary>
        internal const string ParamCapturecomplete = "CAPTURECOMPLETE";

        /// <summary>
        ///     Payflow Param RECURRINGTYPE
        /// </summary>
        internal const string ParamRecurringtype = "RECURRINGTYPE";

        /// <summary>
        ///     Payflow Param NOTE
        /// </summary>
        internal const string ParamNote = "NOTE";

        /// <summary>
        ///     Payflow Param MEMO
        /// </summary>
        internal const string ParamMemo = "MEMO";

        /// <summary>
        ///     Payflow Param BALAMT
        /// </summary>
        internal const string ParamBalamt = "BALAMT";

        /// <summary>
        ///     Payflow Param AMEXID
        /// </summary>
        internal const string ParamAmexid = "AMEXID";

        /// <summary>
        ///     Payflow Param AMEXPOSDATA
        /// </summary>
        internal const string ParamAmexposdata = "AMEXPOSDATA";

        /// <summary>
        ///     Payflow Param BILLINGTYPE
        /// </summary>
        internal const string ParamBillingtype = "BILLINGTYPE";

        /// <summary>
        ///     Payflow Param BA_DESC
        /// </summary>
        internal const string ParamBaDesc = "BA_DESC";

        /// <summary>
        ///     Payflow Param BA_CUSTOM
        /// </summary>
        internal const string ParamBaCustom = "BA_CUSTOM";

        /// <summary>
        ///     Payflow Param BA_FLAG
        /// </summary>
        internal const string ParamBaFlag = "BA_FLAG";

        /// <summary>
        ///     Payflow Param BAID
        /// </summary>
        internal const string ParamBaid = "BAID";

        /// <summary>
        ///     Payflow Param BA_STATUS
        /// </summary>
        internal const string ParamBaStatus = "BA_STATUS";

        /// <summary>
        ///     Payflow Param MERCHANTNAME
        /// </summary>
        internal const string ParamMerchantname = "MERCHANTNAME";

        /// <summary>
        ///     Payflow Param MERCHANTSTREET
        /// </summary>
        internal const string ParamMerchantstreet = "MERCHANTSTREET";

        /// <summary>
        ///     Payflow Param MERCHANTCITY
        /// </summary>
        internal const string ParamMerchantcity = "MERCHANTCITY";

        /// <summary>
        ///     Payflow Param MERCHANTSTATE
        /// </summary>
        internal const string ParamMerchantstate = "MERCHANTSTATE";

        /// <summary>
        ///     Payflow Param MERCHANTCOUNTRYCODE
        /// </summary>
        internal const string ParamMerchantcountrycode = "MERCHANTCOUNTRYCODE";

        /// <summary>
        ///     Payflow Param MERCHANTZIP
        /// </summary>
        internal const string ParamMerchantzip = "MERCHANTZIP";

        /// <summary>
        ///     Payflow Param DOREAUTHORIZATION
        /// </summary>
        internal const string ParamDoreauthorization = "DOREAUTHORIZATION";

        /// <summary>
        ///     Payflow Param SHIPPINGMETHOD
        /// </summary>
        internal const string ParamShippingmethod = "SHIPPINGMETHOD";

        /// <summary>
        ///     Payflow Param PROMOCODEOVERRIDE
        /// </summary>
        internal const string ParamPromocodeoverride = "PROMOCODEOVERRIDE";

        /// <summary>
        ///     Payflow Param PROFILEADDRESSCHANGEDATE
        /// </summary>
        internal const string ParamProfileaddresschangedate = "PROFILEADDRESSCHANGEDATE";

        /// <summary>
        ///     Payflow Param PAYPALCHECKOUTBTNTYPE
        /// </summary>
        internal const string ParamPaypalcheckoutbtntype = "PAYPALCHECKOUTBTNTYPE";

        /// <summary>
        ///     Payflow Param PRODUCTCATEGORY
        /// </summary>
        internal const string ParamProductcategory = "PRODUCTCATEGORY";

        /// <summary>
        ///     Payflow Param PROMOCODE
        /// </summary>
        internal const string ParamPromocode = "PROMOCODE";

        /// <summary>
        ///     Payflow Param TRANSTIME
        /// </summary>
        internal const string ParamTranstime = "TRANSTIME";

        /// <summary>
        ///     Payflow Param CARDTYPE
        /// </summary>
        internal const string ParamCardtype = "CARDTYPE";

        /// <summary>
        ///     Payflow Param ORIGAMT
        /// </summary>
        internal const string ParamOrigamt = "ORIGAMT";

        /// <summary>
        ///     Payflow Param PARTIALAUTH
        /// </summary>
        internal const string ParamPartialauth = "PARTIALAUTH";

        /// <summary>
        ///     Payflow Param EXTRSPMSG
        /// </summary>
        internal const string ParamExtrspmsg = "EXTRSPMSG";

        /// <summary>
        ///     Payflow Param SECURETOKEN
        /// </summary>
        internal const string ParamSecuretoken = "SECURETOKEN";

        /// <summary>
        ///     Payflow Param SECURETOKENID
        /// </summary>
        internal const string ParamSecuretokenid = "SECURETOKENID";

        /// <summary>
        ///     Payflow Param CREATESECURETOKEN
        /// </summary>
        internal const string ParamCreatesecuretoken = "CREATESECURETOKEN";

        /// <summary>
        ///     Payflow Param VISACARDLEVEL
        /// </summary>
        internal const string ParamVisacardlevel = "VISACARDLEVEL";

        /// <summary>
        ///     Payflow Param ALLOWNOTE
        /// </summary>
        internal const string ParamAllownote = "ALLOWNOTE";

        /// <summary>
        ///     Payflow Param REQBILLINGADDRESS
        /// </summary>
        internal const string ParamReqbillingaddress = "REQBILLINGADDRESS";

        /// <summary>
        ///     Payflow Param SHIPTONAME
        /// </summary>
        internal const string ParamShiptoname = "SHIPTONAME";

        /// <summary>
        ///     Payflow Param ORDERID
        /// </summary>
        internal const string ParamOrderid = "ORDERID";

        /// <summary>
        ///     Payflow Param USER1
        /// </summary>
        internal const string ParamUser1 = "USER1";

        /// <summary>
        ///     Payflow Param USER2
        /// </summary>
        internal const string ParamUser2 = "USER2";

        /// <summary>
        ///     Payflow Param USER3
        /// </summary>
        internal const string ParamUser3 = "USER3";

        /// <summary>
        ///     Payflow Param USER4
        /// </summary>
        internal const string ParamUser4 = "USER4";

        /// <summary>
        ///     Payflow Param USER5
        /// </summary>
        internal const string ParamUser5 = "USER5";

        /// <summary>
        ///     Payflow Param USER6
        /// </summary>
        internal const string ParamUser6 = "USER6";

        /// <summary>
        ///     Payflow Param USER7
        /// </summary>
        internal const string ParamUser7 = "USER7";

        /// <summary>
        ///     Payflow Param USER8
        /// </summary>
        internal const string ParamUser8 = "USER8";

        /// <summary>
        ///     Payflow Param USER9
        /// </summary>
        internal const string ParamUser9 = "USER9";

        /// <summary>
        ///     Payflow Param USER10
        /// </summary>
        internal const string ParamUser10 = "USER10";

        #endregion

        #region "Client Information Constants"

        /// <summary>
        ///     Constant for PAYFLOW-CLIENT-DURATION
        /// </summary>
        internal const string PayflowheaderClientDuration = "X-VPS-VIT-CLIENT-DURATION";

        /// <summary>
        ///     Constant for PAYFLOW-CLIENT-VERSION
        /// </summary>
        internal const string PayflowheaderClientVersion = "X-VPS-VIT-CLIENT-VERSION";

        /// <summary>
        ///     Constant for PAYFLOW-OS-ARCHITECTURE
        /// </summary>
        internal const string PayflowheaderOsArchitecture = "X-VPS-VIT-OS-ARCHITECTURE";

        /// <summary>
        ///     Constant for PAYFLOW-OS-NAME
        /// </summary>
        internal const string PayflowheaderOsName = "X-VPS-VIT-OS-NAME";

        /// <summary>
        ///     Constant for PAYFLOW-OS-VERSION
        /// </summary>
        internal const string PayflowheaderOsVersion = "X-VPS-VIT-OS-VERSION";

        /// <summary>
        ///     Constant for PAYFLOW-PROXY
        /// </summary>
        internal const string PayflowheaderProxy = "X-VPS-VIT-PROXY";

        /// <summary>
        ///     Constant for PAYFLOW-RUNTIME-VERSION
        /// </summary>
        internal const string PayflowheaderRuntimeVersion = "X-VPS-VIT-RUNTIME-VERSION";

        /// <summary>
        ///     Constant for PAYFLOW-INTEGRATION-PRODUCT
        /// </summary>
        internal const string PayflowheaderIntegrationProduct = "X-VPS-VIT-INTEGRATION-PRODUCT";

        /// <summary>
        ///     Constant for PAYFLOW-INTEGRATION-VERSION
        /// </summary>
        internal const string PayflowheaderIntegrationVersion = "X-VPS-VIT-INTEGRATION-VERSION";

        /// <summary>
        ///     Constant for PAYFLOW-CLIENT-TYPE
        /// </summary>
        internal const string PayflowheaderClientType = "X-VPS-VIT-CLIENT-TYPE";

        /// <summary>
        ///     Constant for PAYFLOW-CLIENT-TYPE
        /// </summary>
        internal const string PayflowheaderAssembly = "X-VPS-VIT-ASSEMBLY";

        #endregion

        #region "ActionRelated Constants"

        /// <summary>
        ///     Set Action (S)
        /// </summary>
        internal const string ActiontypeSet = "S";

        /// <summary>
        ///     Get Action (G)
        /// </summary>
        internal const string ActiontypeGet = "G";

        /// <summary>
        ///     Do Action (D)
        /// </summary>
        internal const string ActiontypeDo = "D";

        /// <summary>
        ///     Update Action (U)
        /// </summary>
        internal const string ActiontypeUpdateba = "U";

        /// <summary>
        ///     SetBA Action (Z)
        /// </summary>
        internal const string ActiontypeSetba = "Z";

        /// <summary>
        ///     GETBA Action (W)
        /// </summary>
        internal const string ActiontypeGetba = "W";

        /// <summary>
        ///     DOBA Action (X)
        /// </summary>
        internal const string ActiontypeDoba = "X";

        #endregion
    }
}