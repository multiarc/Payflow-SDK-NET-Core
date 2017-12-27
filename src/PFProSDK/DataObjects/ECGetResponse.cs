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
    ///     Used for ExpressCheckout get operation.
    /// </summary>
    /// <remarks>
    ///     <seealso cref="ExpressCheckoutResponse" />
    ///     <seealso cref="EcDoResponse" />
    /// </remarks>
    public class EcGetResponse : ExpressCheckoutResponse
    {
        #region "Constructor"

        /// <summary>
        ///     constructor
        /// </summary>
        internal EcGetResponse()
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
                EMail = (string) responseHashTable[PayflowConstants.ParamEmail];
                PayerId = (string) responseHashTable[PayflowConstants.ParamPayerid];
                PayerStatus = (string) responseHashTable[PayflowConstants.ParamPayerstatus];
                ShipToFirstName = (string) responseHashTable[PayflowConstants.ParamShiptofirstname];
                ShipToLastName = (string) responseHashTable[PayflowConstants.ParamShiptolastname];
                ShipToCountry = (string) responseHashTable[PayflowConstants.ParamShiptocountry];
                ShipToBusiness = (string) responseHashTable[PayflowConstants.ParamShiptobusiness];
                AddressStatus = (string) responseHashTable[PayflowConstants.ParamAddrstatus];
                FirstName = (string) responseHashTable[PayflowConstants.ParamFirstname];
                LastName = (string) responseHashTable[PayflowConstants.ParamLastname];
                ShipToStreet = (string) responseHashTable[PayflowConstants.ParamShiptostreet];
                ShipToStreet2 = (string) responseHashTable[PayflowConstants.ParamShiptostreet2];
                ShipToCity = (string) responseHashTable[PayflowConstants.ParamShiptocity];
                ShipToState = (string) responseHashTable[PayflowConstants.ParamShiptostate];
                ShipToZip = (string) responseHashTable[PayflowConstants.ParamShiptozip];
                CountryCode = (string) responseHashTable[PayflowConstants.ParamCountrycode];
                PhoneNum = (string) responseHashTable[PayflowConstants.ParamPhonenum];
                BaFlag = (string) responseHashTable[PayflowConstants.ParamBaFlag];


                responseHashTable.Remove(PayflowConstants.ParamEmail);
                responseHashTable.Remove(PayflowConstants.ParamPayerid);
                responseHashTable.Remove(PayflowConstants.ParamPayerstatus);
                responseHashTable.Remove(PayflowConstants.ParamShiptofirstname);
                responseHashTable.Remove(PayflowConstants.ParamShiptolastname);
                responseHashTable.Remove(PayflowConstants.ParamShiptocountry);
                responseHashTable.Remove(PayflowConstants.ParamShiptobusiness);
                responseHashTable.Remove(PayflowConstants.ParamAddrstatus);
                responseHashTable.Remove(PayflowConstants.ParamFirstname);
                responseHashTable.Remove(PayflowConstants.ParamLastname);
                responseHashTable.Remove(PayflowConstants.ParamShiptostreet);
                responseHashTable.Remove(PayflowConstants.ParamShiptostreet2);
                responseHashTable.Remove(PayflowConstants.ParamShiptocity);
                responseHashTable.Remove(PayflowConstants.ParamShiptostate);
                responseHashTable.Remove(PayflowConstants.ParamShiptozip);
                responseHashTable.Remove(PayflowConstants.ParamCountrycode);
                responseHashTable.Remove(PayflowConstants.ParamPhonenum);
                responseHashTable.Remove(PayflowConstants.ParamBaFlag);
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

        #region "Member variables"

        #endregion

        #region "properties"

        /// <summary>
        ///     Gets the EMail parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>EMAIL</code>
        /// </remarks>
        public string EMail { get; private set; }

        /// <summary>
        ///     Gets the payerid parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PAYERID</code>
        /// </remarks>
        public string PayerId { get; private set; }

        /// <summary>
        ///     Gets the payerstatus parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PAYERSTATUS</code>
        /// </remarks>
        public string PayerStatus { get; private set; }

        /// <summary>
        ///     Gets the shiptofirstname parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPTOFIRSTNAME</code>
        /// </remarks>
        public string ShipToFirstName { get; private set; }

        /// <summary>
        ///     Gets the shiptolastname parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPTOLASTNAME</code>
        /// </remarks>
        public string ShipToLastName { get; private set; }

        /// <summary>
        ///     Gets the ShipToCountry parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPTOCOUNTRY</code>
        /// </remarks>
        public string ShipToCountry { get; private set; }

        /// <summary>
        ///     Gets the ShipToBusiness parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPTOBUSINESS</code>
        /// </remarks>
        public string ShipToBusiness { get; private set; }

        /// <summary>
        ///     Gets the AddressStatus parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ADDRSTATUS</code>
        /// </remarks>
        public string AddressStatus { get; private set; }

        /// <summary>
        ///     Gets the FirstName parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>FIRSTNAME</code>
        /// </remarks>
        public string FirstName { get; private set; }

        /// <summary>
        ///     Gets the LastName parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>LASTNAME</code>
        /// </remarks>
        public string LastName { get; private set; }

        /// <summary>
        ///     Gets the ShipToStreet parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPTOSTREET</code>
        /// </remarks>
        public string ShipToStreet { get; private set; }

        /// <summary>
        ///     Gets the ShipToStreet2 parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPTOSTREET2</code>
        /// </remarks>
        public string ShipToStreet2 { get; private set; }

        /// <summary>
        ///     Gets the ShipToCity parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPTOCITY</code>
        /// </remarks>
        public string ShipToCity { get; private set; }

        /// <summary>
        ///     Gets the ShipToState parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPTOSTATE</code>
        /// </remarks>
        public string ShipToState { get; private set; }

        /// <summary>
        ///     Gets the ShipToZip parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPTOZIP</code>
        /// </remarks>
        public string ShipToZip { get; private set; }

        /// <summary>
        ///     Gets the CountryCode parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>COUNTRYCODE</code>
        /// </remarks>
        public string CountryCode { get; private set; }

        /// <summary>
        ///     Gets the PHONENUM parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PHONENUM</code>
        /// </remarks>
        public string PhoneNum { get; private set; }

        /// <summary>
        ///     Gets the BA_FLAG parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>BA_FLAG</code>
        /// </remarks>
        public string BaFlag { get; private set; }

        #endregion
    }
}