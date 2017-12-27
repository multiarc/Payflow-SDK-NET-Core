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
using PFProSDK.Common.Exceptions;
using PFProSDK.Common.Utility;

#endregion

namespace PFProSDK.DataObjects
{
    /// <summary>
    ///     Used for Extended param information
    /// </summary>
    /// <remarks>
    ///     Extended data are the Payflow parameters which are
    ///     not mapped through the data objects.
    ///     This class can be used to send such extended parameter information
    ///     in the transaction request.
    /// </remarks>
    /// <example>
    ///     Following example shows how to use this class.
    ///     <code lang="C#" escaped="false">
    /// 		.............
    /// 		// Trans is the transaction object.
    /// 		.............
    /// 		
    /// 		// Set the extended data value.
    /// 		ExtendData ExtData = new ExtendData("PAYFLOW_PARAM_NAME","Param Value");
    /// 		
    /// 		// Add extended data to transaction.
    /// 		Trans.AddToExtendData(ExtData);
    /// 		
    ///  </code>
    ///     <code lang="Visual Basic" escaped="false">
    /// 		.............
    /// 		' Trans is the transaction object.
    /// 		.............
    /// 		
    /// 		' Set the extended data value.
    /// 		Dim ExtData As ExtendData  = new ExtendData("PAYFLOW_PARAM_NAME","Param Value")
    /// 		
    /// 		' Add extended data to transaction.
    /// 		Trans.AddToExtendData(ExtData)
    /// 		
    ///  </code>
    /// </example>
    public sealed class ExtendData : BaseRequestDataObject
    {
        #region "Constructors"

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="paramName">Payflow pram name</param>
        /// <param name="paramValue">param value</param>
        /// <remarks>
        ///     Extended data are the Payflow parameters which are
        ///     not mapped through the data objects.
        ///     This class can be used to send such extended parameter information
        ///     in the transaction request.
        /// </remarks>
        /// <example>
        ///     Following example shows how to use this class.
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		// Trans is the transaction object.
        /// 		.............
        /// 		
        /// 		// Set the extended data value.
        /// 		ExtendData ExtData = new ExtendData("PFPRO_PARAM_NAME","Param Value");
        /// 		
        /// 		// Add extended data to transaction.
        /// 		Trans.AddToExtendData(ExtData);
        /// 		
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		.............
        /// 		' Trans is the transaction object.
        /// 		.............
        /// 		
        /// 		' Set the extended data value.
        /// 		Dim ExtData As ExtendData  = new ExtendData("PFPRO_PARAM_NAME","Param Value")
        /// 		
        /// 		' Add extended data to transaction.
        /// 		Trans.AddToExtendData(ExtData)
        /// 		
        ///  </code>
        /// </example>
        public ExtendData(string paramName, string paramValue)
        {
            ParamName = paramName;
            ParamValue = paramValue;
        }

        #endregion

        #region "Core functions"

        /// <summary>
        ///     Generates the transaction request.
        /// </summary>
        internal override void GenerateRequest()
        {
            try
            {
                RequestBuffer.Append(PayflowUtility.AppendToRequest(ParamName, ParamValue));
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var dEx = new DataObjectException(ex);
                throw dEx;
            }

            //catch
            //{
            //    throw new Exception();
            //}
        }

        #endregion

        #region "Member Variables"

        #endregion

        #region Properties"

        /// <summary>
        ///     ParamName
        /// </summary>
        public string ParamName { get; }

        /// <summary>
        ///     ParamValue
        /// </summary>
        public string ParamValue { get; }

        #endregion
    }
}