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
    ///     This class holds the User1 to User10 related information.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Line item data describes the details of the item purchased and can be can be passed
    ///         for each transaction. The convention for passing line item data in name/value pairs
    ///         is that each name/value starts with L_ and ends with n where n is the line item number.
    ///         For example L_QTY0=1 is the quantity for line item 0 and is equal to 1,
    ///         with n starting at 0
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
    public sealed class UserItem : BaseRequestDataObject
    {
        #region "Core functions"

        /// <summary>
        ///     Generates user item request
        /// </summary>
        internal override void GenerateRequest()
        {
            try
            {
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamUser1, UserItem1));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamUser2, UserItem2));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamUser3, UserItem3));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamUser4, UserItem4));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamUser5, UserItem5));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamUser6, UserItem6));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamUser7, UserItem7));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamUser8, UserItem8));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamUser9, UserItem9));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamUser10, UserItem10));
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

        #region "Member Variables"

        #endregion

        #region "Constructors"

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets, Sets user item.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         These ten string type parameters are intended to store temporary data (for example, variables,
        ///         session IDs, order numbers, and so on). These parameters enable you to return the values
        ///         to your server by using the Post or Silent Post feature.
        ///         Note: UserItem1 through UserItem10 are not displayed to the customer and are not stored in
        ///         the PayPal transaction database.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>USER1</code>
        /// </remarks>
        public string UserItem1 { get; set; }

        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>USER2</code>
        /// </remarks>
        public string UserItem2 { get; set; }

        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>USER3</code>
        /// </remarks>
        public string UserItem3 { get; set; }

        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>USER4</code>
        /// </remarks>
        public string UserItem4 { get; set; }

        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>USER5</code>
        /// </remarks>
        public string UserItem5 { get; set; }

        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>USER6</code>
        /// </remarks>
        public string UserItem6 { get; set; }

        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>USER7</code>
        /// </remarks>
        public string UserItem7 { get; set; }

        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>USER8</code>
        /// </remarks>
        public string UserItem8 { get; set; }

        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>USER9</code>
        /// </remarks>
        public string UserItem9 { get; set; }

        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>USERn</code>
        /// </remarks>
        public string UserItem10 { get; set; }

        #endregion
    }
}