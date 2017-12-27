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
    ///     This class holds the PayLater Line Item related information.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Line item data describes the details of the PayLater promo codes and can be can be passed
    ///         for each transaction. The convention for passing line item data in name/value pairs
    ///         is that each name/value starts with L_ and ends with n where n is the line item number.
    ///         For example L_PROMOCODEn=101 is promo code 101, with n starting at 0
    ///     </para>
    /// </remarks>
    /// <example>
    ///     <para>Following example shows how to use line item.</para>
    ///     <code lang="C#" escaped="false">
    ///   .................
    ///   //Inv is the Invoice object.
    /// 	 .................
    /// 	 
    /// 	 //Create a line item.
    /// 	 LineItem Item = new LineItem();
    /// 	 
    /// 	 //Add info to line item.
    /// 	 Item.Amt = new Currency(new Decimal(25.12));
    /// 	 Item.PickupStreet = "685A E. Middlefield Rd.";
    /// 	 
    /// 	 //Add line item to invoice.
    /// 	 Inv.AddLineItem(Item);
    /// 	 
    /// 	 ..................
    ///  </code>
    ///     <code lang="Visual Basic" escaped="false">
    ///   .................
    ///   'Inv is the Invoice object.
    /// 	 .................
    /// 	 
    /// 	 //Create a line item.
    /// 	 Dim Item As LineItem  = New LineItem
    /// 	 
    /// 	 'Add info to line item.
    /// 	 Item.Amt = New Currency(new Decimal(25.12))
    /// 	 Item.PickupStreet = "685A E. Middlefield Rd."
    /// 	 
    /// 	 'Add line item to invoice.
    /// 	 Inv.AddLineItem(Item)
    /// 	 
    /// 	 ..................
    ///  </code>
    /// </example>
    public sealed class PayLaterLineItem : BaseRequestDataObject
    {
        #region "Member Variables"

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets, Sets  Promo Code
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The product category for this order. If the
        ///         customers cart contains more than one item, use
        ///         the product category for the most expensive item.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PROMOCODE</code>
        /// </remarks>
        public string PromoCode { get; set; }

        #endregion

        #region "Core functions"

        /// <summary>
        ///     Generates line item request
        /// </summary>
        /// <param name="index">index number of line item</param>
        internal void GenerateRequest(int index)
        {
            try
            {
                var indexVal = index.ToString();
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamPromocode + indexVal,
                    PromoCode));
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

        #region "Constructors"

        #endregion
    }
}