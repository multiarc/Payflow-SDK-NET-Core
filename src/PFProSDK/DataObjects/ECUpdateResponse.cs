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
using PFProSDK.Common.Exceptions;
using PFProSDK.Common.Utility;

#endregion


namespace PFProSDK.DataObjects
{
    /// <summary>
    ///     Used for ExpressCheckout Update operation.
    /// </summary>
    /// <remarks>
    ///     <seealso cref="ExpressCheckoutResponse" />
    /// </remarks>
    public class EcUpdateResponse : ExpressCheckoutResponse
    {
        #region "Constructor"

        /// <summary>
        ///     constructor
        /// </summary>
        internal EcUpdateResponse()
        {
        }

        #endregion

        #region "Core functions"

        /// <summary>
        ///     Sets Response params
        /// </summary>
        /// <param name="responseHashTable">Response Hash table by ref</param>
        internal override void SetParams(ref Hashtable responseHashTable)
        {
            try
            {
                BaStatus = (string) responseHashTable[PayflowConstants.ParamBaStatus];
                BaDesc = (string) responseHashTable[PayflowConstants.ParamBaDesc];

                responseHashTable.Remove(PayflowConstants.ParamBaStatus);
                responseHashTable.Remove(PayflowConstants.ParamBaDesc);
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

        #region "Member variable"

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets the BA_STATUS parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>BA_STATUS</code>
        /// </remarks>
        public string BaStatus { get; private set; }

        /// <summary>
        ///     Gets the BA_DESC parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>BA_DESC</code>
        /// </remarks>
        public string BaDesc { get; private set; }

        #endregion
    }
}