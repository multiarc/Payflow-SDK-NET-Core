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
    ///     Used for Check Payment related information
    /// </summary>
    /// <remarks>
    ///     CheckPayment is associated with CheckTender.
    ///     <seealso cref="CheckTender" />
    /// </remarks>
    public sealed class CheckPayment : PaymentDevice
    {
        #region "Constructors"

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <remarks>
        ///     This is used as Payment Device for the CheckTender.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>Micr --> MICR</code>
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		
        /// 		CheckPayment PayDevice = new CheckPayment("XXXXXXXXXX");
        /// 		
        /// 		..............
        ///   </code>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		
        /// 		Dim PayDevice As CheckPayment = new CheckPayment("XXXXXXXXXX")
        /// 		
        /// 		..............
        ///   </code>
        /// </example>
        /// <seealso cref="CheckTender" />
        /// <param name="micr">MICR value</param>
        public CheckPayment(string micr) : base(micr)
        {
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
                //Put the base field Acct as MICR.
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamMicr, Acct));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamName, Name));
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
    }
}