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
    /// <summary>
    ///     This Exception class will be used for handling the DataObject Exceptions.
    /// </summary>
    [Serializable]
    internal class DataObjectException : BaseException
    {
        #region "constructor"

        /// <summary>
        ///     Constructor with Error object as a parameter.
        /// </summary>
        /// <param name="errObject">ErrorObject which needs to be added to the exception.</param>
        internal DataObjectException(ErrorObject errObject) : base(errObject)
        {
        }

        /// <summary>
        ///     Constructor with another exception as a parameter.
        /// </summary>
        /// <param name="ex">Exception which needs to be converted into a DataObjectException type. </param>
        internal DataObjectException(Exception ex) : base(ex)
        {
        }

        #endregion
    } // END CLASS DEFINITION DataObjectException
} // Payments.Common.Exceptions