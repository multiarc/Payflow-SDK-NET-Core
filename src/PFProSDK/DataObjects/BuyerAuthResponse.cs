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

using System.Collections;
using PFProSDK.Common.Utility;

#endregion

namespace PFProSDK.DataObjects
{
    /// <summary>
    ///     Used for the buyerauth operation
    /// </summary>
    public class BuyerAuthResponse : BaseResponseDataObject
    {
        #region "CONSTRUCTOR"

        internal BuyerAuthResponse()
        {
        }

        #endregion

        #region "Functions"

        /// <summary>
        ///     Sets the Response params in
        ///     response data objects.
        /// </summary>
        /// <param name="responseHashTable">Response Hash table by ref</param>
        internal void SetParams(ref Hashtable responseHashTable)
        {
            AcsUrl = (string) responseHashTable[PayflowConstants.ParamAcsurl];
            AuthenticationId = (string) responseHashTable[PayflowConstants.ParamAuthenicationId];
            AuthenticationStatus = (string) responseHashTable[PayflowConstants.ParamAuthenicationStatus];
            Cavv = (string) responseHashTable[PayflowConstants.ParamCavv];
            Eci = (string) responseHashTable[PayflowConstants.ParamEci];
            Md = (string) responseHashTable[PayflowConstants.ParamMd];
            PaReq = (string) responseHashTable[PayflowConstants.ParamPareq];
            Xid = (string) responseHashTable[PayflowConstants.ParamXid];
            responseHashTable.Remove(PayflowConstants.ParamAcsurl);
            responseHashTable.Remove(PayflowConstants.ParamAuthenicationId);
            responseHashTable.Remove(PayflowConstants.ParamAuthenicationStatus);
            responseHashTable.Remove(PayflowConstants.ParamCavv);
            responseHashTable.Remove(PayflowConstants.ParamEci);
            responseHashTable.Remove(PayflowConstants.ParamMd);
            responseHashTable.Remove(PayflowConstants.ParamPareq);
            responseHashTable.Remove(PayflowConstants.ParamXid);
        }

        #endregion

        #region "Member Variables"

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets the acsurl parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ACSURL</code>
        /// </remarks>
        public string AcsUrl { get; private set; }

        /// <summary>
        ///     Gets the authentication_id parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>AUTHENTICATION_ID</code>
        /// </remarks>
        public string AuthenticationId { get; private set; }

        /// <summary>
        ///     Gets the authentication_status parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>AUTHENTICATION_STATUS</code>
        /// </remarks>
        public string AuthenticationStatus { get; private set; }

        /// <summary>
        ///     Gets the CAVV parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CAVV</code>
        /// </remarks>
        public string Cavv { get; private set; }

        /// <summary>
        ///     Gets the ECI parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ECI</code>
        /// </remarks>
        public string Eci { get; private set; }

        /// <summary>
        ///     Gets the PaReq parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PAREQ</code>
        /// </remarks>
        public string PaReq { get; private set; }

        /// <summary>
        ///     Gets the XID parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>XID</code>
        /// </remarks>
        public string Xid { get; private set; }

        /// <summary>
        ///     Gets the MD parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>MD</code>
        /// </remarks>
        public string Md { get; private set; }

        #endregion
    }
}