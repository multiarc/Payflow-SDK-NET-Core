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
    ///     Used for BuyerAuth Status information.
    /// </summary>
    /// <remarks>
    ///     Use this class to set the BuyerAuth Status related
    ///     information.
    /// </remarks>
    public sealed class BuyerAuthStatus : BaseRequestDataObject
    {
        #region "Core functions"

        /// <summary>
        ///     Generates the transaction request.
        /// </summary>
        internal override void GenerateRequest()
        {
            try
            {
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamAuthenicationId,
                    AuthenticationId));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamAuthenicationStatus,
                    AuthenticationStatus));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamEci, Eci));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamCavv, Cavv));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamXid, Xid));
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

        #region "Constructor"

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets, Sets AuthenticationId.
        /// </summary>
        /// <remarks>
        ///     <para>AuthenticationId.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>AUTHENTICATION_ID</code>
        /// </remarks>
        public string AuthenticationId { get; set; }

        /// <summary>
        ///     Gets, Sets  AuthenticationStatus.
        /// </summary>
        /// <remarks>
        ///     <para>Authentication Status</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>AUTHENTICATION_STATUS</code>
        /// </remarks>
        public string AuthenticationStatus { get; set; }

        /// <summary>
        ///     Gets, Sets  ECI.
        /// </summary>
        /// <remarks>
        ///     <para>ECI</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ECI</code>
        /// </remarks>
        public string Eci { get; set; }

        /// <summary>
        ///     Gets, Sets  CAVV.
        /// </summary>
        /// <remarks>
        ///     <para>CCAVV</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CAVV</code>
        /// </remarks>
        public string Cavv { get; set; }

        /// <summary>
        ///     Gets, Sets  DOB.
        /// </summary>
        /// <remarks>
        ///     <para>XID.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>XID</code>
        /// </remarks>
        public string Xid { get; set; }

        #endregion
    }
}