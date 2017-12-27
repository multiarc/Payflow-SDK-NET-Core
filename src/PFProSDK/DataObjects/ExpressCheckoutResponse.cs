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
    ///     This  class serves as base class of all ExpressCheckout response classes.
    /// </summary>
    /// <remarks>
    ///     <para>Each response object is associated with a particular type of expressCheckout operation.</para>
    ///     <para>
    ///         Following are the reponse objects associated with
    ///         different operations of ExpressChecout:
    ///     </para>
    ///     <list type="table">
    ///         <listheader>
    ///             <term>ExpressCheckout operation.</term>
    ///             <description>Request data object</description>
    ///         </listheader>
    ///         <item>
    ///             <term>SET operation for ExpressCheckout.</term>
    ///             <description>ExpressCheckoutResponse</description>
    ///         </item>
    ///         <item>
    ///             <term>GET operation for ExpressCheckout.</term>
    ///             <description>
    ///                 <see cref="EcGetResponse">ECGetResponse</see>
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>DO operation for ExpressCheckout.</term>
    ///             <description>
    ///                 <see cref="EcDoResponse">ECDoResponse</see>
    ///             </description>
    ///         </item>
    ///     </list>
    /// </remarks>
    public class ExpressCheckoutResponse : BaseResponseDataObject
    {
        #region "Constructor"

        /// <summary>
        ///     constructor
        /// </summary>
        internal ExpressCheckoutResponse()
        {
        }

        #endregion

        #region "Properties"

        /// <summary>
        ///     Retuns the token for the transaction.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>TOKEN</code>
        /// </remarks>
        public string Token { get; private set; }

        #endregion

        #region "Core functions"

        /// <summary>
        ///     Sets Response params
        /// </summary>
        /// <param name="responseHashTable">Response Hash table by ref</param>
        internal virtual void SetParams(ref Hashtable responseHashTable)
        {
            try
            {
                Token = (string) responseHashTable[PayflowConstants.ParamToken];
                responseHashTable.Remove(PayflowConstants.ParamToken);
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

        #region "Member Variable"

        #endregion
    }
}