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
    ///     Used for shipping address information
    /// </summary>
    /// <remarks>Shipping address is destination address information.</remarks>
    /// <example>
    ///     <para>Following example shows how to use ShipTo.</para>
    ///     <code lang="C#" escaped="false">
    ///   .................
    ///   //Inv is the Invoice object.
    ///   .................
    ///   
    /// 	//Set the Shipping Address details.
    /// 	ShipTo Ship = new ShipTo();
    /// 	Ship.ShipToStreet = "685A E. Middlefield Rd.";
    /// 	Ship.ShipToStree2 = "Apt. #2";
    /// 	Ship.ShipToZip = "94043";
    /// 	Inv.ShipTo = Ship;
    /// 	.................
    ///  </code>
    ///     <code lang="Visual Basic" escaped="false">
    ///   .................
    ///   'Inv is the Invoice object.
    ///   .................
    ///   
    /// 	'Set the Shipping Address details.
    /// 	Dim Ship As ShipTo = New ShipTo
    /// 	Ship.ShipToStreet = "685A E. Middlefield Rd."
    /// 	Ship.ShipToStree2 = "Apt. #2";
    /// 	Ship.ShipToZip = "94043"
    /// 	Inv.ShipTo = Ship
    /// 	.................
    ///  </code>
    /// </example>
    public sealed class ShipTo : Address
    {
        #region "Utility function"

        /// <summary>
        ///     This method copies the common contents
        ///     from shipping to billing address.
        /// </summary>
        /// <returns>Billing Address object</returns>
        /// <remarks>
        ///     This method can be used to
        ///     populate the shipping addresses directly
        ///     from the billing addresses when
        ///     both are the same.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        ///  
        /// 		................
        /// 		//Ship is the object of
        /// 		//ShipTo populated with 
        /// 		//the shipping addresses.
        /// 		................
        /// 		
        /// 		
        /// 		BillTo Bill;
        /// 		
        /// 		//Populate billing addresses
        /// 		//from shipping addresses.
        /// 		Bill = Ship.Copy();
        /// 		
        /// 		................
        ///  
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        ///  
        /// 		................
        /// 		'Ship is the object of
        /// 		'ShipTo populated with 
        /// 		'the shipping addresses.
        /// 		................
        /// 		
        /// 		
        /// 		BillTo Bill;
        /// 		
        /// 		'Populate billing addresses
        /// 		'from shipping addresses.
        /// 		Bill = Ship.Copy()
        /// 		
        /// 		................
        ///  
        ///  </code>
        /// </example>
        /// <seealso cref="BillTo" />
        public BillTo Copy()
        {
            var copyObject = new BillTo
            {
                AddressCity = AddressCity,
                AddressCountry = AddressCountry,
                AddressEmail = AddressEmail,
                AddressFax = AddressFax,
                AddressFirstName = AddressFirstName,
                AddressLastName = AddressLastName,
                AddressMiddleName = AddressMiddleName,
                AddressPhone2 = AddressPhone2,
                AddressPhone = AddressPhone,
                AddressState = AddressState,
                AddressStreet = AddressStreet,
                AddressStreet2 = AddressStreet2,
                AddressZip = AddressZip
            };
            return copyObject;
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
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamShiptostreet, AddressStreet));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamShiptostreet2,
                    AddressStreet2));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamShiptocity, AddressCity));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamShiptostate, AddressState));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamShiptocountry,
                    AddressCountry));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamShiptozip, AddressZip));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamShiptophone, AddressPhone));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamShiptophone2, AddressPhone2));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamShiptoemail, AddressEmail));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamShiptofirstname,
                    AddressFirstName));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamShiptomiddlename,
                    AddressMiddleName));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamShiptolastname,
                    AddressLastName));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamShipcarrier, ShipCarrier));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamShipmethod, ShipMethod));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamShipfromzip, ShipFromZip));
                RequestBuffer.Append(
                    PayflowUtility.AppendToRequest(PayflowConstants.ParamShippedfromzip, ShipFromZip));
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

        #region "Properties"

        /// <summary>
        ///     Gets, Sets shipping method
        /// </summary>
        /// <remarks>
        ///     Shipping method
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPMETHOD</code>
        /// </remarks>
        public string ShipMethod { get; set; }

        /// <summary>
        ///     Gets, Sets shipping carrier
        /// </summary>
        /// <remarks>
        ///     Shipping carrier
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPCARRIER</code>
        /// </remarks>
        public string ShipCarrier { get; set; }

        /// <summary>
        ///     Gets, Sets ship from zip
        /// </summary>
        /// <remarks>
        ///     Ship from postal code (called ZIP code in the USA).
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPFROMZIP</code>
        /// </remarks>
        public string ShipFromZip { get; set; }

        #endregion

        #region "Constructors"

        #endregion
    }
}