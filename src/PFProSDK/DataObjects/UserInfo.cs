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
    ///     Used for PayPal User account information
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This is a required class for a strong assembly
    ///         transactions. This class is used to store the
    ///         user credential needed to authenticate the user
    ///         performing the transaction.
    ///     </para>
    ///     <para>
    ///         Every transaction takes UserInfo
    ///         mandatorily.
    ///     </para>
    ///     <para>Following are the required user credentials:</para>
    ///     <list type="table">
    ///         <listheader>
    ///             <term>Payflow Parameter</term>
    ///             <description>Description</description>
    ///         </listheader>
    ///         <item>
    ///             <term>USER</term>
    ///             <description>
    ///                 Login name. This value is case-sensitive.
    ///                 The login name created while registering for the Payflow
    ///                 account.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>VENDOR</term>
    ///             <description>
    ///                 Login name. This value is case-sensitive.
    ///                 The login name created while registering for the Payflow
    ///                 account.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>PARTNER</term>
    ///             <description>
    ///                 The authorized PayPal Reseller that
    ///                 registered this account for the Payflow service
    ///                 provided you with a Partner ID.
    ///                 If you registered yourself, use PayPal.
    ///                 Case-sensitive.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>PWD</term>
    ///             <description>Case-sensitive 7 to 32-character password.</description>
    ///         </item>
    ///     </list>
    /// </remarks>
    public sealed class UserInfo : BaseRequestDataObject
    {
        #region "Constructors"

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="user">User id</param>
        /// <param name="vendor">Vendor id</param>
        /// <param name="partner">Partner id</param>
        /// <param name="pwd">Password</param>
        /// <remarks>
        ///     <para>
        ///         This is a required class for a strong assembly
        ///         transactions. This class is used to store the
        ///         user credential needed to authenticate the user
        ///         performing the transaction.
        ///     </para>
        ///     <para>
        ///         Every transaction takes UserInfo
        ///         mandatorily.
        ///     </para>
        ///     <para>Following are the required user credentials:</para>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Payflow Parameter</term>
        ///             <description>Description</description>
        ///         </listheader>
        ///         <item>
        ///             <term>USER</term>
        ///             <description>
        ///                 Login name. This value is case-sensitive.
        ///                 The login name created while registering for the Payflow
        ///                 account.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>VENDOR</term>
        ///             <description>
        ///                 Login name. This value is case-sensitive.
        ///                 The login name created while registering for the Payflow
        ///                 account.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>PARTNER</term>
        ///             <description>
        ///                 The authorized PayPal Reseller that
        ///                 registered this account for the Payflow service
        ///                 provided you with a Partner ID.
        ///                 If you registered yourself, use PayPal.
        ///                 Case-sensitive.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>PWD</term>
        ///             <description>Case-sensitive 6- to 32-character password.</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        ///  ..............
        ///  // Create the User data object with the required user details.
        ///  UserInfo User = new UserInfo("user", "vendor", "partner", "password");
        ///  ..............
        /// </code>
        ///     <code lang="Visual Basic" escaped="false">
        ///  ..............
        ///  ' Create the User data object with the required user details.
        ///  Dim User As UserInfo = New UserInfo("user", "vendor", "partner", "password");
        ///  ..............
        /// </code>
        /// </example>
        public UserInfo(string user, string vendor, string partner, string pwd)
        {
            _mUser = user;
            _mVendor = vendor;
            _mPartner = partner;
            _mPwd = pwd;
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
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamUser, _mUser));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamVendor, _mVendor));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamPartner, _mPartner));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamPwd, _mPwd));
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

        /// <summary>
        ///     User id
        /// </summary>
        private readonly string _mUser;

        /// <summary>
        ///     Vendor id
        /// </summary>
        private readonly string _mVendor;

        /// <summary>
        ///     Partner id
        /// </summary>
        private readonly string _mPartner;

        /// <summary>
        ///     Password
        /// </summary>
        private readonly string _mPwd;

        #endregion
    }
}