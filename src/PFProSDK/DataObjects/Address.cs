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

#endregion

namespace PFProSDK.DataObjects
{
    /// <summary>
    ///     Abstract class to hold the
    ///     Address information.
    /// </summary>
    /// <remarks>This class can be extended to create a new address type.</remarks>
    public abstract class Address : BaseRequestDataObject
    {
        #region "Member Variables"

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets, Sets Street
        /// </summary>
        internal string AddressStreet { get; set; }

        /// <summary>
        ///     Gets, Sets Street2
        /// </summary>
        internal string AddressStreet2 { get; set; }

        /// <summary>
        ///     Gets, Sets City
        /// </summary>
        internal string AddressCity { get; set; }

        /// <summary>
        ///     Gets, Sets State
        /// </summary>
        internal string AddressState { get; set; }

        /// <summary>
        ///     Gets, Sets Zip
        /// </summary>
        internal string AddressZip { get; set; }

        /// <summary>
        ///     Gets, Sets Country
        /// </summary>
        internal string AddressCountry { get; set; }

        /// <summary>
        ///     Gets, Sets Phonenum
        /// </summary>
        internal string AddressPhone { get; set; }

        /// <summary>
        ///     Gets, Sets Phone2
        /// </summary>
        internal string AddressPhone2 { get; set; }

        /// <summary>
        ///     Gets, Sets Email
        /// </summary>
        internal string AddressEmail { get; set; }

        /// <summary>
        ///     Gets, Sets Fax
        /// </summary>
        internal string AddressFax { get; set; }

        /// <summary>
        ///     Gets, Sets Firstname
        /// </summary>
        internal string AddressFirstName { get; set; }

        /// <summary>
        ///     Gets, Sets Middlename
        /// </summary>
        internal string AddressMiddleName { get; set; }

        /// <summary>
        ///     Gets, Sets Lastname
        /// </summary>
        internal string AddressLastName { get; set; }

        #endregion

        #region "Constructors"

        #endregion
    }
}