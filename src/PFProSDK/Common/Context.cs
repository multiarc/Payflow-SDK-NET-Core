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
using System.Runtime.InteropServices;
using System.Text;
using PFProSDK.Common.Logging;
using PFProSDK.Common.Utility;

namespace PFProSDK.Common
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    ///     This class contains the all error message generated for the class containing
    ///     the context.This also contains the highest severity level contained by the
    ///     context
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// -----------------------------------------------------------------------------
    public class Context
    {
        #region "Constructor"

        /// <summary>
        ///     Constructor for Context
        /// </summary>
        internal Context()
        {
        }

        #endregion

        #region "Member variable"

        /// <summary>
        ///     Holds the collection of error objects for the context instance.
        /// </summary>
        private ArrayList _mErrorObjects = new ArrayList();

        /// <summary>
        ///     Indicates the highest severity level error in the array list.
        /// </summary>
        private int _mHighestErrorLvl;

        #endregion

        #region "Properties"

        //Gets the highestErrorLvl
        /// <summary>
        ///     Indicates the highest severity level error in the array list.
        /// </summary>
        public int HighestErrorLvl
        {
            get
            {
                var errCnt = 0;
                var errMaxCnt = 0;
                var errSeverityLevel = 0;
                PopulateErrors();
                errMaxCnt = _mErrorObjects.Count;
                for (errCnt = 0; errCnt < errMaxCnt; errCnt++)
                {
                    errSeverityLevel = ((ErrorObject) _mErrorObjects[errCnt]).SeverityLevel;
                    if (_mHighestErrorLvl < errSeverityLevel) _mHighestErrorLvl = errSeverityLevel;
                }

                return _mHighestErrorLvl;
            }
        }

        /// <summary>
        ///     Indicates if the Error messages due to Logger class needs to be added to the context.
        /// </summary>
        public bool LoadLoggerErrs { get; set; }

        #endregion

        #region "Methods"

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     This method adds the passed error object in the array list contained by
        ///     the context object
        /// </summary>
        /// <param name="errObject">ErrorObject</param>
        /// <returns>Nothing</returns>
        /// <remarks>
        /// </remarks>
        /// -----------------------------------------------------------------------------
        internal void AddError(ErrorObject errObject)
        {
            if (_mErrorObjects == null) _mErrorObjects = new ArrayList();
            _mErrorObjects.Insert(0, errObject);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     This method adds the passed arraylist of error objects
        ///     to the context object
        /// </summary>
        /// <param name="errorObjects">Array List</param>
        /// <returns>Nothing</returns>
        /// <remarks>
        /// </remarks>
        /// -----------------------------------------------------------------------------
        internal void AddErrors(ArrayList errorObjects)
        {
            _mErrorObjects.InsertRange(0, errorObjects);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     This method will log all the error and exceptions contained in the ErrorObjects
        ///     arraylist.This will then call the log method from the Logger class after
        ///     converting the array list into a ErrorObject array.
        /// </summary>
        /// <returns>Nothing</returns>
        /// <remarks>
        /// </remarks>
        /// -----------------------------------------------------------------------------
        public bool LogErrors()
        {
            //This method will log all the errors using the Logger class
            //convert the array list into a array of error objects
            try
            {
                var errCnt = 0;
                Logger instance = null;
                var populatedErr = new ArrayList(0);

                if (_mErrorObjects != null)
                {
                    instance = Logger.Instance;
                    populatedErr = instance.PopulateErrorDetails(_mErrorObjects);
                    _mErrorObjects.Clear();
                    _mErrorObjects.InsertRange(errCnt, populatedErr);
                    instance.Log(_mErrorObjects);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     This method will check if the context contains any error message.This method
        ///     can be used for checking if the context is empty.
        /// </summary>
        /// <returns>
        ///     boolean value 'true' - If errors are present
        ///     false in case of no errors.
        /// </returns>
        /// <remarks>
        /// </remarks>
        /// -----------------------------------------------------------------------------
        public bool IsErrorContained()
        {
            if (_mErrorObjects != null)
            {
                if (_mErrorObjects.Count > 0)
                    return true;

                return false;
            }

            return false;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     This method will check if the context contains a specific error message.This method
        ///     can be used for checking if the context is empty.
        /// </summary>
        /// <param name="error">Error Object</param>
        /// <returns>
        ///     boolean value 'true' - If specific Error is present
        ///     false in case of no errors.
        /// </returns>
        /// <remarks>
        /// </remarks>
        /// -----------------------------------------------------------------------------
        internal bool IsCommunicationErrorContained(ErrorObject error)
        {
            if (IsErrorContained())
            {
                foreach (ErrorObject err in _mErrorObjects)
                    if (err != null && err.MessageCode.Equals(error.MessageCode))
                        if (err.MessageParams != null && error.MessageParams != null)
                            if (err.MessageParams[0].Equals(error.MessageParams[0]))
                                return true;
                return false;
            }

            return false;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     This method will return the error object from the Context as per the index
        ///     passed to the function.If the index value passed is more than the count of the
        ///     errors in the array list then it returns a null.
        /// </summary>
        /// <param name="index">int</param>
        /// <returns>ErrorObject</returns>
        /// <remarks>
        /// </remarks>
        /// -----------------------------------------------------------------------------
        //Get the error object form the array list depending on the index passed
        public ErrorObject GetError(int index)
        {
            PopulateErrors();
            if (index < _mErrorObjects.Count) return (ErrorObject) _mErrorObjects[index];
            return null;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     This method returns the array list populated with all the error contained
        ///     in the context
        /// </summary>
        /// <returns>Array List</returns>
        /// <remarks>
        /// </remarks>
        /// -----------------------------------------------------------------------------
        //Get all the error contained by the context
        public ArrayList GetErrors()
        {
            PopulateErrors();
            return _mErrorObjects;
        }


        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     This method will return the array list populated with all the error contained
        ///     in the context which are equal to or above the severity level passed to the
        ///     function
        /// </summary>
        /// <param name="sevLvl">integer</param>
        /// <returns>Array List</returns>
        /// <remarks>
        /// </remarks>
        /// -----------------------------------------------------------------------------
        //Get all the error contained by the context equal to or above a severity level
        public ArrayList GetErrors(int sevLvl)
        {
            var highSevErrors = new ArrayList();
            var errMaxCount = 0;
            var errCnt = 0;
            PopulateErrors();
            errMaxCount = _mErrorObjects.Count;
            for (errCnt = 0; errCnt < errMaxCount; errCnt++)
                if (((ErrorObject) _mErrorObjects[errCnt]).SeverityLevel >=
                    sevLvl)
                    highSevErrors.Add(_mErrorObjects[errCnt]);
            return highSevErrors;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     This method will return the total number of errors contained in the
        ///     Context Object.
        /// </summary>
        /// <returns>Integer</returns>
        /// <remarks>
        /// </remarks>
        /// -----------------------------------------------------------------------------
        //Get the count of ErrorObjects in the Context
        public int GetErrorCount()
        {
            if (_mErrorObjects != null) return _mErrorObjects.Count;
            return 0;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     This method will populate all the error objects contained in the arraylist with
        ///     details such as the severity level and message body.It uses 'PopulateErrorDetails'
        ///     method of the Logger class.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// -----------------------------------------------------------------------------
        //Populate the error objects with information from the sorted List
        private void PopulateErrors()
        {
            var errCnt = 0;
            Logger instance = null;
            var populatedErr = new ArrayList(0);

            if (_mErrorObjects != null)
            {
                instance = Logger.Instance;
                populatedErr = instance.PopulateErrorDetails(_mErrorObjects);
                if (LoadLoggerErrs)
                {
                    //PopulatedErr.AddRange (Instance.GetLoggerErrs);
                    //Check for duplicate Logger errors
                    var tempList = instance.GetLoggerErrs;
                    if (tempList != null)
                        for (var i = 0; i < tempList.Count; i++)
                            if (!populatedErr.Contains(tempList[i]))
                                populatedErr.Add((ErrorObject) tempList[i]);
                }

                _mErrorObjects.Clear();
                _mErrorObjects.InsertRange(errCnt, populatedErr);
            }
        }


        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     This method overrides the toString() method of the System.Object Class.This method
        ///     converts the information in the Context in the string format.The format is as follows:
        ///     Message (Message Number in the context)-----------------------------
        ///     [(Message severity Level)](Message code)-(Formatted message body with context info)
        ///     Message stack Trace
        /// </summary>
        /// <returns>
        ///     Returns all the messages contained by the context in the string format.
        /// </returns>
        /// <remarks>
        /// </remarks>
        /// -----------------------------------------------------------------------------
        public override string ToString()
        {
            var retVal = new StringBuilder("");
            var errCount = 0;
            var errMaxCount = 0;
            ErrorObject err;

            PopulateErrors();
            errMaxCount = _mErrorObjects.Count;
            for (errCount = 0; errCount < errMaxCount; errCount++)
            {
                err = (ErrorObject) _mErrorObjects[errCount];
                if (errMaxCount > 0)
                {
                    retVal.Append(PayflowConstants.FormatMsgSeperator);
                    retVal.Append(errCount + 1);
                    retVal.Append(PayflowConstants.FormatMsgLineseperator);
                    retVal.Append(Environment.NewLine);
                }

                retVal.Append(PayflowConstants.FormatMsgOpenbracket);
                retVal.Append(GetStringSeverity(err.SeverityLevel));
                retVal.Append(PayflowConstants.FormatMsgClosebracket);
                //RetVal.Append(Err.MessageCode);
                //RetVal.Append(PayflowConstants.FORMAT_MSG_CODEBODY_SEP);
                retVal.Append(err);
                retVal.Append(Environment.NewLine);
                retVal.Append(err.ErrorStackTrace);
                if (errCount < errMaxCount - 1)
                    retVal.Append(Environment.NewLine);
            }

            return retVal.ToString();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     This method is another overload for the method toString().This method
        ///     converts the information in the Context in the string format.This will return
        ///     the formatted error string for messages that have severity level equal to or above
        ///     the severitylevel parameter passed to this function.The messages for different errors
        ///     are separated by the separator format passed to the method.In case no separator is
        ///     passed a new line character is used.
        /// </summary>
        /// <param name="severityLevel">
        ///     All the errors messages which have severity levels equal
        ///     to or greater than this is returned
        /// </param>
        /// <param name="seperator">Message separator.</param>
        /// <returns>
        ///     Returns the messages contained by the context in the string format.
        /// </returns>
        /// <remarks>
        /// </remarks>
        /// -----------------------------------------------------------------------------
        public string ToString(int severityLevel, string seperator)
        {
            var retVal = new StringBuilder("");
            var errCount = 0;
            var errMaxCount = 0;
            ErrorObject err;
            var errObjects = new ArrayList(0);
            errObjects = GetErrors(severityLevel);

            errMaxCount = errObjects.Count;
            for (errCount = 0; errCount < errMaxCount; errCount++)
            {
                err = (ErrorObject) errObjects[errCount];
                retVal.Append(err);
                if (errCount < errMaxCount - 1)
                    if (seperator != null && seperator.Length != 0)
                        retVal.Append(seperator);
                    else
                        retVal.Append(Environment.NewLine);
            }

            return retVal.ToString();
        }

        /// <summary>
        ///     This resets the context object
        /// </summary>
        /// <returns>Void</returns>
        public void ClearErrors()
        {
            if (_mErrorObjects != null)
            {
                _mErrorObjects.Clear();
                _mHighestErrorLvl = 0;
            }
        }

        /// <summary>
        ///     This gets the severity level for a
        ///     severity integer value.
        /// </summary>
        /// <param name="severity">Severity level integer value</param>
        /// <returns>Severity level string value</returns>
        private static string GetStringSeverity(int severity)
        {
            var retVal = PayflowConstants.ErrorWarn;

            switch (severity)
            {
                case PayflowConstants.SeverityDebug:
                    retVal = PayflowConstants.ErrorDebug;
                    break;
                case PayflowConstants.SeverityInfo:
                    retVal = PayflowConstants.ErrorInfo;
                    break;
                case PayflowConstants.SeverityWarn:
                    retVal = PayflowConstants.ErrorWarn;
                    break;
                case PayflowConstants.SeverityError:
                    retVal = PayflowConstants.ErrorError;
                    break;
                case PayflowConstants.SeverityFatal:
                    retVal = PayflowConstants.ErrorFatal;
                    break;
            }

            return retVal;
        }

        /// <summary>
        ///     Not implemented
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Object.Equals</returns>
        [ComVisible(false)]
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        ///     Not implemented
        /// </summary>
        /// <returns>Object.GetHashCode</returns>
        [ComVisible(false)]
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    } // END CLASS DEFINITION Context
} // Payments.Common