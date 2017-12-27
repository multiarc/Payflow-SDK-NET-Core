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
    ///     This class holds the Invoice Line Item item related information.
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
    public sealed class LineItem : BaseRequestDataObject
    {
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

                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLAmt + indexVal, Amt));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLCost + indexVal, Cost));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLFreightamt + indexVal,
                    FreightAmt));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLHandlingamt + indexVal,
                    HandlingAmt));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLTaxamt + indexVal,
                    TaxAmt));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLUom + indexVal, Uom));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLPickupstreet + indexVal,
                    PickupStreet));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLPickupstate + indexVal,
                    PickupState));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLPickupcountry + indexVal,
                    PickupCountry));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLPickupcity + indexVal,
                    PickupCity));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLPickupzip + indexVal,
                    PickupZip));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLDesc + indexVal, Desc));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLDiscount + indexVal,
                    Discount));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLManufacturer + indexVal,
                    Manufacturer));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLProdcode + indexVal,
                    ProdCode));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLItemnumber + indexVal,
                    ItemNumber));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLName + indexVal, Name));
                if (Qty != PayflowConstants.InvalidNumber)
                    RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLQty + indexVal, Qty));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLSku + indexVal, Sku));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLTaxrate + indexVal,
                    TaxRate));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLTaxtype + indexVal,
                    TaxType));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLType + indexVal, Type));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLCommcode + indexVal,
                    CommCode));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLTrackingnum + indexVal,
                    TrackingNum));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLCostcenternum + indexVal,
                    CostCenterNum));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLCatalognum + indexVal,
                    CatalogNum));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLUpc + indexVal, Upc));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLUnspsccode + indexVal,
                    UnspscCode));
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

        #region "Properties"

        /// <summary>
        ///     Gets, Sets line item Amt.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Total line item amount including tax and
        ///         discount. + for debit, - for credits.
        ///         Amount should always be a decimal.
        ///         Exact amount to the cent (34.00, not 34).
        ///         Do not include comma separators. Use 1199.95
        ///         instead of 1,199.95.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_AMTn</code>
        /// </remarks>
        public Currency Amt { get; set; }

        /// <summary>
        ///     Gets, Sets line item Cost.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Cost per item, excluding tax. Amount should always be a decimal.
        ///         Exact amount to the cent (34.00, not 34).
        ///         Do not include comma separators. Use 1199.95
        ///         instead of 1,199.95.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_COSTn</code>
        /// </remarks>
        public Currency Cost { get; set; }

        /// <summary>
        ///     Gets, Sets line item FreightAmt.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Freight Amount per item. Amount should always be a decimal.
        ///         Exact amount to the cent (34.00, not 34).
        ///         Do not include comma separators. Use 1199.95
        ///         instead of 1,199.95.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_FREIGHTAMTn</code>
        /// </remarks>
        public Currency FreightAmt { get; set; }

        /// <summary>
        ///     Gets, Sets line item TaxAmt.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Tax Amount per item. Amount should always be a decimal.
        ///         Exact amount to the cent (34.00, not 34).
        ///         Do not include comma separators. Use 1199.95
        ///         instead of 1,199.95.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_TAXAMTn</code>
        /// </remarks>
        public Currency TaxAmt { get; set; }

        /// <summary>
        ///     Gets, Sets line item UOM.
        /// </summary>
        /// <remarks>
        ///     <para>Item unit of measure.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_UOMn</code>
        /// </remarks>
        public string Uom { get; set; }

        /// <summary>
        ///     Gets, Sets line item PickupStreet.
        /// </summary>
        /// <remarks>
        ///     <para>Item drop-off address1.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_PICKUPSTREETn</code>
        /// </remarks>
        public string PickupStreet { get; set; }

        /// <summary>
        ///     Gets, Sets line item PickupState.
        /// </summary>
        /// <remarks>
        ///     <para>Item drop-off state.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_PICKUPSTATEn</code>
        /// </remarks>
        public string PickupState { get; set; }

        /// <summary>
        ///     Gets, Sets line item PickupCountry.
        /// </summary>
        /// <remarks>
        ///     <para>Item drop-off country.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_PICKUPCOUNTRYn</code>
        /// </remarks>
        public string PickupCountry { get; set; }

        /// <summary>
        ///     Gets, Sets line item PickupCity.
        /// </summary>
        /// <remarks>
        ///     <para>Item drop-off city.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_PICKUPCITYn</code>
        /// </remarks>
        public string PickupCity { get; set; }

        /// <summary>
        ///     Gets, Sets line item PickupZip.
        /// </summary>
        /// <remarks>
        ///     <para>Item drop-off zip.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_PICKUPZIPn</code>
        /// </remarks>
        public string PickupZip { get; set; }

        /// <summary>
        ///     Gets, Sets line item Desc.
        /// </summary>
        /// <remarks>
        ///     <para>Item description.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_DESCn</code>
        /// </remarks>
        public string Desc { get; set; }

        /// <summary>
        ///     Gets, Sets line item Discount.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Discount Amount per item. Amount should always be a decimal.
        ///         Exact amount to the cent (34.00, not 34).
        ///         Do not include comma separators. Use 1199.95
        ///         instead of 1,199.95.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_DISCOUNTn</code>
        /// </remarks>
        public Currency Discount { get; set; }

        /// <summary>
        ///     Gets, Sets line item Manufacturer.
        /// </summary>
        /// <remarks>
        ///     <para>Item manufacturer.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_MANUFACTURERn</code>
        /// </remarks>
        public string Manufacturer { get; set; }

        /// <summary>
        ///     Gets, Sets line item ProdCode.
        /// </summary>
        /// <remarks>
        ///     <para>Item product code.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_PRODCODEn</code>
        /// </remarks>
        public string ProdCode { get; set; }

        /// <summary>
        ///     Gets, Sets line item Qty.
        /// </summary>
        /// <remarks>
        ///     <para>Quantity per item.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_QTYn</code>
        /// </remarks>
        public long Qty { get; set; } = PayflowConstants.InvalidNumber;

        /// <summary>
        ///     Gets, Sets line item SKU.
        /// </summary>
        /// <remarks>
        ///     <para>Item SKU.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_SKUn</code>
        /// </remarks>
        public string Sku { get; set; }

        /// <summary>
        ///     Gets, Sets line item TaxRate.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Tax Rate Amount. Amount should always be a decimal.
        ///         Exact amount to the cent (34.00, not 34).
        ///         Do not include comma separators. Use 1199.95
        ///         instead of 1,199.95.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_TAXRATEn</code>
        /// </remarks>
        public Currency TaxRate { get; set; }

        /// <summary>
        ///     Gets, Sets line item TaxType.
        /// </summary>
        /// <remarks>
        ///     <para>Item tax type.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_TAXTYPEn</code>
        /// </remarks>
        public string TaxType { get; set; }

        /// <summary>
        ///     Gets, Sets line item Type.
        /// </summary>
        /// <remarks>
        ///     <para>Item type.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_TYPEn</code>
        /// </remarks>
        public string Type { get; set; }

        /// <summary>
        ///     Gets, Sets line item CommCode.
        /// </summary>
        /// <remarks>
        ///     <para>Item commodity code.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_COMMCODEn</code>
        /// </remarks>
        public string CommCode { get; set; }

        /// <summary>
        ///     Gets, Sets line item TrackingNum.
        /// </summary>
        /// <remarks>
        ///     <para>Item tracking number.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_TRACKINGNUMn</code>
        /// </remarks>
        public string TrackingNum { get; set; }

        /// <summary>
        ///     Gets, Sets line item CostCenterNum.
        /// </summary>
        /// <remarks>
        ///     <para>Item cost center number.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_COSTCENTERNUMn</code>
        /// </remarks>
        public string CostCenterNum { get; set; }

        /// <summary>
        ///     Gets, Sets line item CatalogNum.
        /// </summary>
        /// <remarks>
        ///     <para>Item catalog number.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_CATALOGNUMn</code>
        /// </remarks>
        public string CatalogNum { get; set; }

        /// <summary>
        ///     Gets, Sets line item UPC.
        /// </summary>
        /// <remarks>
        ///     <para>Item universal product code.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_UPCn</code>
        /// </remarks>
        public string Upc { get; set; }

        /// <summary>
        ///     Gets, Sets line item HandlingAmt.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Item Handling Amount. Amount should always be a decimal.
        ///         Exact amount to the cent (34.00, not 34).
        ///         Do not include comma separators. Use 1199.95
        ///         instead of 1,199.95.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_HANDLINGAMTn</code>
        /// </remarks>
        public Currency HandlingAmt { get; set; }

        /// <summary>
        ///     Gets, Sets line item unspsc code.
        /// </summary>
        /// <remarks>
        ///     <para>Item UnspscCode.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_UNSPSCCODEn</code>
        /// </remarks>
        public string UnspscCode { get; set; }

        /// <summary>
        ///     Gets, Sets line item name.
        /// </summary>
        /// <remarks>
        ///     <para>Item UnspscCode.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_NAMEn</code>
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        ///     Gets, Sets line item number.
        /// </summary>
        /// <remarks>
        ///     <para></para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>L_xxxxn</code>
        /// </remarks>
        public string ItemNumber { get; set; }

        #endregion

        #region "Constructors"

        #endregion
    }
}