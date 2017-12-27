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

using System;
using System.Collections;
using PFProSDK.Common.Utility;

namespace PFProSDK.Common
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    ///     This class contains the error message along with the message code ,Severity level
    ///     of the error and the stack Trace.This class represents the format of an error/message.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// -----------------------------------------------------------------------------
    public class ErrorObject
    {
        #region "Methods"

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     This function overrides the object.toString method.
        ///     This function formats the error message by filling the place holders with the
        ///     context parameters
        /// </summary>
        /// <returns>Formatted error String </returns>
        /// <remarks>
        /// </remarks>
        /// -----------------------------------------------------------------------------
        public override string ToString()
        {
            //This needs to get the message bode using the message code 
            string formattedMessage;
            if (MessageParams != null)
            {
                var msgParams = MessageParams.ToArray();
                MessageParams.CopyTo(msgParams);
                try
                {
                    formattedMessage = string.Format(MessageBody, msgParams);
                }
                catch (Exception ex)
                {
                    var stackTrace = PayflowConstants.EmptyString;
                    PayflowUtility.InitStackTraceOn();

                    if (PayflowConstants.TraceOn.Equals(PayflowConstants.Trace)) stackTrace = " " + ex;

                    formattedMessage = PayflowConstants.MessageFormattingError + stackTrace;
                }
            }
            else
            {
                formattedMessage = MessageBody;
            }

            return formattedMessage;
        }

        #endregion

        #region "Member Variables"

        #endregion

        #region "Property Declarations"

        //Gets the property for the Message Body of the Error Object
        internal string MessageBody { get; }

        //Gets the property for the Message Code of the Error Object
        /// <summary>
        ///     Gets Message Code
        /// </summary>
        public string MessageCode { get; }

        /*Gets the property for the Stack Trace of the Error Object.
           * This is applicable only if the error is an exception*/

        /// <summary>
        ///     Gets Stack Trace of the Error Object.
        /// </summary>
        public string ErrorStackTrace { get; }

        //Gets the property for the mSeverityLevel of the Error Object
        /// <summary>
        ///     Gets the SeverityLevel
        /// </summary>
        public int SeverityLevel { get; }


        /// <summary>
        ///     Gets MessageParams
        /// </summary>
        public ArrayList MessageParams { get; }

        #endregion

        #region "Constructors"

        // Used for Validation Errors which don?t have a stack trace.
        /// <summary>
        ///     Used for Validation Errors which don?t have a stack trace.
        /// </summary>
        /// <param name="severity">Severity level for the error.</param>
        /// <param name="msgCode">Message Code.</param>
        /// <param name="msgCodeParams">Parameters which are used as context information.</param>
        internal ErrorObject(int severity, string msgCode,
            string[] msgCodeParams)
        {
            SeverityLevel = severity;
            MessageCode = msgCode;
            MessageParams = new ArrayList();
            MessageParams.AddRange(msgCodeParams);
        }

        // Used for populating error message from the Message xml file.
        /// <summary>
        ///     Used for populating error message from the Message xml file.
        /// </summary>
        /// <param name="severity">Severity level for the error.</param>
        /// <param name="msgCode">Message Code.</param>
        /// <param name="msgBody">Message Description for the Error.</param>
        internal ErrorObject(int severity, string msgCode, string msgBody)
        {
            SeverityLevel = severity;
            MessageCode = msgCode;
            MessageBody = msgBody;
            ErrorStackTrace = PayflowConstants.EmptyString;
            MessageParams = new ArrayList();
        }

        // Used for Exception objects, which have a stack trace.
        /// <summary>
        ///     Used for Exception objects, which have a stack trace.
        /// </summary>
        /// <param name="severity">Severity level for the error.</param>
        /// <param name="msgCode">Message Code.</param>
        /// <param name="msgCodeParams">Parameters which are used as context information.</param>
        /// <param name="stackTrace">Stack Trace information for the Error.</param>
        internal ErrorObject(int severity, string msgCode, string[] msgCodeParams, string stackTrace) : this(severity,
            msgCode, msgCodeParams)
        {
            ErrorStackTrace = stackTrace;
        }

        // Used for copying the error object in the logger class
        /// <summary>
        ///     Used for copying the error object in the logger class.
        /// </summary>
        /// <param name="severity">Severity level for the error.</param>
        /// <param name="msgCode">Message Code.</param>
        /// <param name="msgBody">Message Description for the Error.</param>
        /// <param name="msgCodeParams">Parameters which are used as context information.</param>
        /// <param name="stackTrace">Stack Trace information for the Error.</param>
        internal ErrorObject(int severity, string msgCode, string msgBody, string[] msgCodeParams,
            string stackTrace) : this(
            severity, msgCode, msgCodeParams)
        {
            MessageBody = msgBody;
            ErrorStackTrace = stackTrace;
        }

        // Used for Exception objects without any message code.
        /// <summary>
        ///     Used for Exception objects without any message code.
        /// </summary>
        /// <param name="msgBody">Message Description for the Error.</param>
        /// <param name="stackTrace">Stack Trace information for the Error.</param>
        internal ErrorObject(string msgBody, string stackTrace)
        {
            SeverityLevel = PayflowConstants.SeverityFatal;
            MessageBody = msgBody;
            ErrorStackTrace = stackTrace;
            MessageCode = PayflowConstants.EmptyString;
            MessageParams = new ArrayList();
        }

        // Used for Exception objects without any message code and stack trace.
        /// <summary>
        ///     Used for Exception objects without any message code and stack trace.
        /// </summary>
        /// <param name="msgBody">Message Description for the Error.</param>
        internal ErrorObject(string msgBody)
        {
            MessageBody = msgBody;
            MessageCode = PayflowConstants.EmptyString;
            MessageParams = new ArrayList();
            ErrorStackTrace = PayflowConstants.EmptyString;
            SeverityLevel = PayflowConstants.SeverityFatal;
        }

        #endregion
    }
}