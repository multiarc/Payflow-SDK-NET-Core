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

namespace PFProSDK.Common.Exceptions
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    ///     This is the base class for all the different exception classes.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// -----------------------------------------------------------------------------
    [Serializable]
    internal abstract class BaseException : Exception
    {
        #region "Member variable"

        /// <summary>
        ///     Static context object which holds the information about the different exceptions
        ///     that are raised.
        /// </summary>
        private readonly Context _mErrorContext = new Context(); //This will hold the context for the error

        #endregion

        #region "Properties"

        /*	//returns the context for the exception
        /// <summary>
        /// Static context object which holds the information about the different exceptions 
        /// that are raised.
        /// </summary>
        internal Context ErrContext
        {
            get { return mErrorContext; }
        }
*/

        #endregion

        #region "Constructor"

        //The constructor initializes the context and adds the error object 
        //passed to it
        /// <summary>
        ///     Constructor with Error object as a parameter.
        /// </summary>
        /// <param name="err">ErrorObject which needs to be added to the exception.</param>
        protected BaseException(ErrorObject err)
        {
            _mErrorContext.AddError(err);
        }

        /// <summary>
        ///     Constructor with another exception as a parameter.
        /// </summary>
        /// <param name="ex">
        ///     Exception which needs to be converted into a Base
        ///     exception type.
        /// </param>
        protected BaseException(Exception ex)
        {
            var errObj = new ErrorObject(ex.Message, ex.StackTrace);
            _mErrorContext.AddError(errObj);
        }

        #endregion

        #region "Methods"

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     This method will return the information contained in the exception in
        ///     the String format.This internally calls the context.ToString() method to get
        ///     the string format
        /// </summary>
        /// <returns>String representation of the Exception</returns>
        /// <remarks>
        /// </remarks>
        /// -----------------------------------------------------------------------------
        public override string ToString()
        {
            var retVal = "";
            retVal = _mErrorContext.ToString();
            return retVal;
        }

        /*	/// -----------------------------------------------------------------------------
        /// <summary>
        /// This method will log the information contained in the exception.
        /// This internally calls the context.Log method to log the message.
        /// </summary>
        /// <returns>boolean values indicating success or failure</returns>
        /// <remarks>
        /// </remarks>
        /// -----------------------------------------------------------------------------
        public bool Log()
        {
            return mErrorContext.LogErrors();
        }*/

        internal virtual ErrorObject GetFirstErrorInExceptionContext()
        {
            if (_mErrorContext.GetErrorCount() > 0) return _mErrorContext.GetError(0);
            return null;
        }

        #endregion
    } // END CLASS DEFINITION BaseException
} // Payments.Common.Exceptions