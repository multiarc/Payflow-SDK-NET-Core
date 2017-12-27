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
    ///     Used for Billing Address information
    /// </summary>
    /// <remarks>Billing address is Cardholder's address information.</remarks>
    /// <example>
    ///     <para>Following example shows how to use BillTo.</para>
    ///     <code lang="C#" escaped="false">
    ///   .................
    ///   //Inv is the Invoice object.
    ///   .................
    ///   
    /// 	//Set the Billing Address details.
    /// 	BillTo Bill = new BillTo();
    /// 	Bill.BillToStreet = "123 Main St.";
    /// 	Bill.BillToZip = "12345";
    /// 	Inv.BillTo = Bill;
    /// 	.................
    ///  </code>
    ///     <code lang="Visual Basic" escaped="false">
    ///   .................
    ///   'Inv is the Invoice object.
    ///   .................
    ///   
    /// 	'Set the Billing Address details.
    /// 	Dim Bill As BillTo = New BillTo
    /// 	Bill.BillToStreet = "123 Main St."
    /// 	Bill.BillToZip = "12345"
    /// 	Inv.BillTo = Bill
    /// 	.................
    ///  </code>
    /// </example>
    public sealed class BillTo : Address
    {
        #region "Utility function"

        /// <summary>
        ///     This method copies the common contents
        ///     from billing to shipping address.
        /// </summary>
        /// <returns>ShipTo object</returns>
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
        /// 		//Bill is the object of
        /// 		//BillTo populated with 
        /// 		//the billing addresses.
        /// 		................
        /// 		
        /// 		
        /// 		ShipTo Ship;
        /// 		
        /// 		//Populate shipping addresses
        /// 		//from billing addresses.
        /// 		Ship = Bill.Copy();
        /// 		
        /// 		................
        ///  
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        ///  
        /// 		................
        /// 		'Bill is the object of
        /// 		'BillTo populated with 
        /// 		'the billing addresses.
        /// 		................
        /// 		
        /// 		
        /// 		Dim Ship As ShipTo
        /// 		
        /// 		'Populate shipping addresses
        /// 		'from billing addresses.
        /// 		Ship = Bill.Copy()
        /// 		
        /// 		................
        ///  
        ///  </code>
        /// </example>
        /// <seealso cref="ShipTo" />
        public ShipTo Copy()
        {
            var copyObject = new ShipTo
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
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamStreet, AddressStreet));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamBilltostreet2,
                    AddressStreet2));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamCity, AddressCity));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamState, AddressState));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamBilltocountry,
                    AddressCountry));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamZip, AddressZip));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamPhonenum, AddressPhone));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamBilltophone2, AddressPhone2));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamEmail, AddressEmail));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamFax, AddressFax));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamFirstname, AddressFirstName));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamMiddlename,
                    AddressMiddleName));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLastname, AddressLastName));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamHomephone,
                    BillToHomePhone));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamCompanyname,
                    BillToCompanyName));
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
        }

        #endregion

        #region "Member variables"

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets, Sets Billing HomePhone.
        /// </summary>
        /// <remarks>
        ///     <para>Cardholder’s home telephone number.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>HOMEPHONE</code>
        /// </remarks>
        public string BillToHomePhone { get; set; }

        /// <summary>
        ///     Gets, Sets  CompanyName.
        /// </summary>
        /// <remarks>
        ///     <para>Company Name.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>COMPANYNAME</code>
        /// </remarks>
        public string BillToCompanyName { get; set; }

        #endregion

        #region "Constructors"

        #endregion
    }
}