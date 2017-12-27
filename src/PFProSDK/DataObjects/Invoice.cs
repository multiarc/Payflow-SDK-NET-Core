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
    ///     Used as the Purchase Invoice class. All the purchase
    ///     related information can be stored in this class.
    /// </summary>
    /// <remarks>
    ///     <para>Following transactions do require invoice object:</para>
    ///     <list type="bullet">
    ///         <item>Sale Transaction</item>
    ///         <item>Authorization Transaction</item>
    ///         <item>Voice Authorization Transaction</item>
    ///         <item>Primary Credit Transaction</item>
    ///         <item>Recurring Transaction : Action --> Add, Payment</item>
    ///     </list>
    ///     <para>
    ///         However, Invoice information can also be passed
    ///         in the following transactions:
    ///     </para>
    ///     <list type="bullet">
    ///         <item>Delayed Capture Transaction</item>
    ///         <item>Credit Transaction</item>
    ///         <item>Void Authorization Transaction</item>
    ///         <item>Reference Credit Transaction</item>
    ///     </list>
    ///     <para>
    ///         By default, the following fields are copied from the
    ///         primary transaction (if they exist) into the reference
    ///         transaction:
    ///     </para>
    ///     <list type="bullet">
    ///         <item>Account number  	Amount  	City  	</item>
    ///         <item>Comment1  	Comment2  	Company Name  	</item>
    ///         <item>Country Cust_Code  	CustIP  	DL  	</item>
    ///         <item>Num  	DOB  	Duty amount  	</item>
    ///         <item>EMail  	Expiration date  	First name  	</item>
    ///         <item>Freight amount  	Invoice number  	Last name  	</item>
    ///         <item>Middle Name  	Purchase order number  	Ship To City  	</item>
    ///         <item>Ship To Country  	Ship To First Name  	Ship To Last Name  	</item>
    ///         <item>Ship To Middle Name  	Ship To State  	Ship To Street  	</item>
    ///         <item>Ship To ZIP  	SS Num  	State  	</item>
    ///         <item>Street  	Suffix  	Swipe data  	</item>
    ///         <item>Tax amount  	Tax exempt  	Telephone  	</item>
    ///         <item>Title		ZIP  	</item>
    ///     </list>
    ///     <para>
    ///         If the invoice is passed in the reference transaction, then the
    ///         new values (if they exist in invoice) are used (except Account number,
    ///         Expiration date, or Swipe data).
    ///     </para>
    /// </remarks>
    /// <example>
    ///     <para>Following example shows how to use invoice.</para>
    ///     <code lang="C#" escaped="false">
    ///   .................
    /// 	// Create a new Invoice data object with the Amount, Billing Address etc. details.
    /// 	Invoice Inv = new Invoice();
    /// 	// Set Amount.
    /// 	Currency Amt = new Currency(new decimal(25.12));
    /// 	Inv.Amt = Amt;
    /// 	Inv.PoNum = "PO12345";
    /// 	Inv.InvNum = "INV12345";
    /// 	Inv.AltTaxAmt = new Currency(new decimal(25.14));
    /// 	// Set the Billing Address details.
    /// 	BillTo Bill = new BillTo();
    /// 	Bill.BillToStreet = "123 Main St.";
    /// 	Bill.BillToZip = "12345";
    /// 	Inv.BillTo = Bill;
    /// 	.................
    ///  </code>
    ///     <code lang="Visual Basic" escaped="false">
    /// 	.................
    /// 	' Create a new Invoice data object with the Amount, Billing Address etc. details.
    /// 	Dim Inv As Invoice = New Invoice
    /// 	' Set Amount.
    /// 	Dim Amt As Currency = New Currency(New Decimal(25.12))
    /// 	Inv.Amt = Amt
    /// 	Inv.PoNum = "PO12345"
    /// 	Inv.InvNum = "INV12345"
    /// 	' Set the Billing Address details.
    /// 	Dim Bill As BillTo = New BillTo
    /// 	Bill.BillToStreet = "123 Main St."
    /// 	Bill.BillToZip = "12345"
    /// 	Inv.BillTo = Bill
    /// 	.................
    ///  </code>
    /// </example>
    public class Invoice : BaseRequestDataObject
    {
        #region "Constructors"

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <remarks>
        ///     <para>Following transactions do require invoice object:</para>
        ///     <list type="bullet">
        ///         <item>Sale Transaction</item>
        ///         <item>Authorization Transaction</item>
        ///         <item>Voice Authorization Transaction</item>
        ///         <item>Primary Credit Transaction</item>
        ///         <item>Recurring Transaction : Action --> Add, Payment</item>
        ///     </list>
        ///     <para>
        ///         However, Invoice information can also be passed
        ///         in the following transactions:
        ///     </para>
        ///     <list type="bullet">
        ///         <item>Delayed Capture Transaction</item>
        ///         <item>Credit Transaction</item>
        ///         <item>Void Authorization Transaction</item>
        ///         <item>Reference Credit Transaction</item>
        ///     </list>
        ///     <para>
        ///         By default, the following fields are copied from the
        ///         primary transaction (if they exist) into the reference
        ///         transaction:
        ///     </para>
        ///     <list type="bullet">
        ///         <item>Account number  	Amount  	City  	</item>
        ///         <item>Comment1  	Comment2  	Company Name  	</item>
        ///         <item>Country Cust_Code  	CustIP  	DL  	</item>
        ///         <item>Num  	DOB  	Duty amount  	</item>
        ///         <item>EMail  	Expiration date  	First name  	</item>
        ///         <item>Freight amount  	Invoice number  	Last name  	</item>
        ///         <item>Middle Name  	Purchase order number  	Ship To City  	</item>
        ///         <item>Ship To Country  	Ship To First Name  	Ship To Last Name  	</item>
        ///         <item>Ship To Middle Name  	Ship To State  	Ship To Street  	</item>
        ///         <item>Ship To ZIP  	SS Num  	State  	</item>
        ///         <item>Street  	Suffix  	Swipe data  	</item>
        ///         <item>Tax amount  	Tax exempt  	Telephone  	</item>
        ///         <item>Title		ZIP  	</item>
        ///         <item>UK: Capture Complete Recurring Type  	</item>
        ///     </list>
        ///     <para>
        ///         If the invoice is passed in the reference transaction, then the
        ///         new values (if they exist in invoice) are used (except Account number,
        ///         Expiration date, or Swipe data).
        ///     </para>
        /// </remarks>
        /// <example>
        ///     <para>Following example shows how to use invoice.</para>
        ///     <code lang="C#" escaped="false">
        ///   .................
        /// 	// Create a new Invoice data object with the Amount, Billing Address etc. details.
        /// 	Invoice Inv = new Invoice();
        /// 	// Set Amount.
        /// 	Currency Amt = new Currency(new decimal(25.12));
        /// 	Inv.Amt = Amt;
        /// 	Inv.PoNum = "PO12345";
        /// 	Inv.InvNum = "INV12345";
        /// 	Inv.AltTaxAmt = new Currency(new decimal(25.14));
        /// 	// Set the Billing Address details.
        /// 	BillTo Bill = new BillTo();
        /// 	Bill.BillToStreet = "123 Main St.";
        /// 	Bill.BillToZip = "12345";
        /// 	Inv.BillTo = Bill;
        /// 	.................
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	.................
        /// 	' Create a new Invoice data object with the Amount, Billing Address etc. details.
        /// 	Dim Inv As Invoice = New Invoice
        /// 	' Set Amount.
        /// 	Dim Amt As Currency = New Currency(New Decimal(25.12))
        /// 	Inv.Amt = Amt
        /// 	Inv.PoNum = "PO12345"
        /// 	Inv.InvNum = "INV12345"
        /// 	' Set the Billing Address details.
        /// 	Dim Bill As BillTo = New BillTo
        /// 	Bill.BillToStreet = "123 Main St."
        /// 	Bill.BillToZip = "12345"
        /// 	Inv.BillTo = Bill
        /// 	.................
        ///  </code>
        /// </example>
        public Invoice()
        {
            _mItemList = new ArrayList();
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
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamInvnum, InvNum));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamAmt, Amt));
                // if no Amt passed, skip CurrencyCode.
                if (Amt != null)
                    RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamCurrency,
                        Amt.CurrencyCode));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamTaxexempt, TaxExempt));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamTaxamt, TaxAmt));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamDutyamt, DutyAmt));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamFreightamt, FreightAmt));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamHandlingamt, HandlingAmt));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamShippingamt, ShippingAmt));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamDiscount, Discount));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamDesc, Desc));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamComment1, Comment1));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamComment2, Comment2));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamDesc1, Desc1));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamDesc2, Desc2));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamDesc3, Desc3));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamDesc4, Desc4));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamCustref, CustRef));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamPonum, PoNum));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamVatregnum, VatRegNum));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamVattaxamt, VatTaxAmt));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamLocaltaxamt, LocalTaxAmt));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamNationaltaxamt,
                    NationalTaxAmt));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamAlttaxamt, AltTaxAmt));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamCommcode, CommCode));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamVattaxpercent,
                    VatTaxPercent));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamInvoicedate, InvoiceDate));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamStarttime, StartTime));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamEndtime, EndTime));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamOrderdate, OrderDate));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamOrdertime, OrderTime));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamRecurring, Recurring));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamItemamt, ItemAmt));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamOrderdesc, OrderDesc));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamRecurringtype,
                    RecurringType));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamMerchdescr, MerchDescr));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamMerchsvc, MerchSvc));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamOrderid, OrderId));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamEchodata, EchoData));
                if (BillTo != null)
                {
                    BillTo.RequestBuffer = RequestBuffer;
                    BillTo.GenerateRequest();
                }

                if (ShipTo != null)
                {
                    ShipTo.RequestBuffer = RequestBuffer;
                    ShipTo.GenerateRequest();
                }

                if (BrowserInfo != null)
                {
                    BrowserInfo.RequestBuffer = RequestBuffer;
                    BrowserInfo.GenerateRequest();
                }

                if (CustomerInfo != null)
                {
                    CustomerInfo.RequestBuffer = RequestBuffer;
                    CustomerInfo.GenerateRequest();
                }

                if (_mItemList != null && _mItemList.Count > 0) GenerateItemRequest();
                if (UserItem != null)
                {
                    UserItem.RequestBuffer = RequestBuffer;
                    UserItem.GenerateRequest();
                }
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
        ///     List of line items
        /// </summary>
        private readonly ArrayList _mItemList;

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets, Sets  BillTo.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Use this property to set the billing
        ///         addresses of the purchase order.
        ///     </para>
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	.................
        /// 	// Inv is the Invoice object
        /// 	.................
        /// 	// Set the Billing Address details.
        /// 	BillTo Bill = New BillTo();
        /// 	Bill.BillToStreet = "123 Main St.";
        /// 	Bill.BillToZip = "12345";
        /// 	Inv.BillTo = Bill;
        /// 	.................
        /// 	</code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	.................
        /// 	' Inv is the Invoice object
        /// 	.................
        /// 	' Set the Billing Address details.
        /// 	Dim Bill As BillTo = New BillTo
        /// 	Bill.BillToStreet = "123 Main St."
        /// 	Bill.BillToZip = "12345"
        /// 	Inv.BillTo = Bill
        /// 	.................
        /// 	</code>
        /// </example>
        public BillTo BillTo { get; set; }

        /// <summary>
        ///     Gets, Sets  ShipTo.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Use this property to set the shipping
        ///         addresses of the purchase order.
        ///     </para>
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	.................
        /// 	// Inv is the Invoice object
        /// 	.................
        /// 	// Set the Shipping Address details.
        /// 	ShipTo Ship = New ShipTo();
        /// 	Ship.ShipToStreet = "685A E. Middlefield Rd.";
        /// 	Ship.ShipToZip = "94043";
        /// 	Inv.ShipTo = Ship;
        /// 	.................
        /// 	</code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	.................
        /// 	' Inv is the Invoice object
        /// 	.................
        /// 	' Set the Shipping Address details.
        /// 	Dim Ship As ShipTo = New ShipTo
        /// 	Ship.ShipToStreet = "685A E. Middlefield Rd."
        /// 	Ship.ShipToZip = "94043"
        /// 	Inv.ShipTo = Ship
        /// 	.................
        /// 	</code>
        /// </example>
        public ShipTo ShipTo { get; set; }

        /// <summary>
        ///     Gets, Sets  TaxExempt.
        /// </summary>
        /// <remarks>
        ///     <para>Is the customer tax exempt? Y or N</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>TAXEXEMPT</code>
        /// </remarks>
        public string TaxExempt { get; set; }

        /// <summary>
        ///     Gets, Sets  InvNum
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Merchant invoice number. This reference number
        ///         (PNREF—generated by PayPal) is used for authorizations
        ///         and settlements.
        ///     </para>
        ///     <para>
        ///         The Acquirer decides if this information will
        ///         appear on the merchant’s bank reconciliation statement.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>INVNUM</code>
        /// </remarks>
        public string InvNum { get; set; }

        /// <summary>
        ///     Gets, Sets  Amt.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Amount (US Dollars) U.S. based currency.
        ///         Specify the exact amount to the cent using a decimal
        ///         point—use 34.00, not 34. Do not include comma
        ///         separators—use 1199.95 not 1,199.95.
        ///     </para>
        ///     <para>
        ///         Your processor and/or Internet merchant account
        ///         provider may stipulate a maximum amount.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>AMT</code>
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	.................
        /// 	// Inv is the Invoice object
        /// 	.................
        ///  // Set the Amount for the invoice.
        ///  // A valid amount is a two decimal value.
        ///  Currency Amt = new Currency(new decimal(25.12))
        ///  //For values which have more than two decimal places 
        ///  Currency Amt = new Currency(new decimal(25.1214));
        ///  Amt.NoOfDecimalDigits = 2;
        ///  //If the NoOfDecimalDigits property is used then it is mandatory to set one of the following properties to true.
        ///  Amt.Round = true;
        ///  Amt.Truncate = true;
        ///  Inv.Amt = Amt;
        /// 	.................
        /// 	</code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	.................
        /// 	' Inv is the Invoice object
        /// 	.................
        ///  'Set the Amount for the invoice.
        ///  'A valid amount is a two decimal value.
        ///  Dim Amt as new Currency(new decimal(25.12))
        ///  'For values which have more than two decimal places 
        ///  Dim Amt as new Currency(new decimal(25.1214))
        ///  Amt.NoOfDecimalDigits = 2
        ///  'If the NoOfDecimalDigits property is used then it is mandatory to set one of the following properties to true.
        ///  Amt.Round = true
        ///  Amt.Truncate = true
        ///  Inv.Amt = Amt;
        ///  ................
        /// 	</code>
        /// </example>
        public Currency Amt { get; set; }

        /// <summary>
        ///     Gets, Sets  TaxAmt.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Tax Amount. Amount should always be a decimal.
        ///         Exact amount to the cent (34.00, not 34).
        ///         Do not include comma separators. Use 1199.95
        ///         instead of 1,199.95.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>TAXAMT</code>
        /// </remarks>
        public Currency TaxAmt { get; set; }

        /// <summary>
        ///     Gets, Sets  DutyAmt.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Sometimes called import tax.
        ///         Amount should always be a decimal.
        ///         Exact amount to the cent (34.00, not 34).
        ///         Do not include comma separators. Use 1199.95
        ///         instead of 1,199.95.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>DUTYAMT</code>
        /// </remarks>
        public Currency DutyAmt { get; set; }

        /// <summary>
        ///     Gets, Sets  FreightAmt.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Freight Amount. Amount should always be a decimal.
        ///         Exact amount to the cent (34.00, not 34).
        ///         Do not include comma separators. Use 1199.95
        ///         instead of 1,199.95.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>FREIGHTAMT</code>
        /// </remarks>
        public Currency FreightAmt { get; set; }

        /// <summary>
        ///     Gets, Sets  HandlingAmt
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Handling Amount. Amount should always be a decimal.
        ///         Exact amount to the cent (34.00, not 34).
        ///         Do not include comma separators. Use 1199.95
        ///         instead of 1,199.95.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>HANDLINGAMT</code>
        /// </remarks>
        public Currency HandlingAmt { get; set; }

        /// <summary>
        ///     Gets, Sets  ShippingAmt
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Shipping Amount. Amount should always be a decimal.
        ///         Exact amount to the cent (34.00, not 34).
        ///         Do not include comma separators. Use 1199.95
        ///         instead of 1,199.95.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SHIPPINGAMT</code>
        /// </remarks>
        public Currency ShippingAmt { get; set; }


        /// <summary>
        ///     Gets, Sets  Discount.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Discount amount on total sale. Amount should always be a decimal.
        ///         Exact amount to the cent (34.00, not 34).
        ///         Do not include comma separators. Use 1199.95
        ///         instead of 1,199.95.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>DISCOUNT</code>
        /// </remarks>
        public Currency Discount { get; set; }

        /// <summary>
        ///     Gets, Sets  Desc.
        /// </summary>
        /// <remarks>
        ///     <para>General description of the transaction.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>DESC</code>
        /// </remarks>
        public string Desc { get; set; }

        /// <summary>
        ///     Gets, Sets  Comment1
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Merchant-defined value for reporting and auditing
        ///         purposes.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>COMMENT1</code>
        /// </remarks>
        public string Comment1 { get; set; }

        /// <summary>
        ///     Gets, Sets  Comment2
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Merchant-defined value for reporting and auditing
        ///         purposes.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>COMMENT2</code>
        /// </remarks>
        public string Comment2 { get; set; }

        /// <summary>
        ///     Gets, Sets  Desc1.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Up to 4 lines of additional description of
        ///         the charge.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>DESC1</code>
        /// </remarks>
        public string Desc1 { get; set; }

        /// <summary>
        ///     Gets, Sets  Desc2.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Up to 4 lines of additional description of
        ///         the charge.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>DESC2</code>
        /// </remarks>
        public string Desc2 { get; set; }

        /// <summary>
        ///     Gets, Sets  Desc3.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Up to 4 lines of additional description of
        ///         the charge.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>DESC3</code>
        /// </remarks>
        public string Desc3 { get; set; }

        /// <summary>
        ///     Gets, Sets  Desc4.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Up to 4 lines of additional description of
        ///         the charge.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>DESC4</code>
        /// </remarks>
        public string Desc4 { get; set; }

        /// <summary>
        ///     Gets, Sets  CustRef.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Merchant-defined identifier for reporting and auditing
        ///         purposes. For example, you can set CUSTREF to the
        ///         invoice number.
        ///     </para>
        ///     <para>
        ///         You can use CUSTREF when performing Inquiry
        ///         transactions. To ensure that you can always access
        ///         the correct transaction when performing an Inquiry,
        ///         you must provide a unique CUSTREF when
        ///         submitting any transaction, including retries.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CUSTREF</code>
        /// </remarks>
        public string CustRef { get; set; }

        /// <summary>
        ///     Gets, Sets  InvoiceDate.
        /// </summary>
        /// <remarks>
        ///     <para>Transaction Date.</para>
        ///     <para>Format: yyyymmdd.</para>
        ///     <para>yyyy - Year, mm - Month, dd - Day.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>INVOICEDATE</code>
        /// </remarks>
        public string InvoiceDate { get; set; }

        /// <summary>
        ///     Gets, Sets  StartTime
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         STARTTIME specifies the beginning of the time
        ///         period during which the transaction specified by the
        ///         CUSTREF occurred.
        ///     </para>
        ///     <para>
        ///         If you set STARTTIME, and not ENDTIME, then
        ///         ENDTIME is defaulted to 30 days after STARTTIME.
        ///         If neither STARTTIME nor ENDTIME is specified, then
        ///         the system searches the last 30 days.
        ///     </para>
        ///     <para>Format: yyyymmddhhmmss</para>
        ///     <para>yyyy - Year, mm - Month dd - Day, hh - Hours, mm - Minutes ss - Seconds.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>STARTTIME</code>
        /// </remarks>
        public string StartTime { get; set; }

        /// <summary>
        ///     Gets, Sets  EndTime.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         ENDTIME specifies the end of the time period during
        ///         which the transaction specified by the CUSTREF occurred.
        ///     </para>
        ///     <para>
        ///         ENDTIME must be less than 30 days after STARTTIME.
        ///         An inquiry cannot be performed across a date range
        ///         greater than 30 days.
        ///     </para>
        ///     <para>
        ///         If you set ENDTIME, and not STARTTIME, then STARTTIME is
        ///         defaulted to 30 days before ENDTIME. If neither
        ///         STARTTIME nor ENDTIME is specified, then the
        ///         system searches the last 30 days.
        ///     </para>
        ///     <para>	Format: yyyymmddhhmmss</para>
        ///     <para>yyyy - Year, mm - Month dd - Day, hh - Hours, mm - Minutes ss - Seconds.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ENDTIME</code>
        /// </remarks>
        public string EndTime { get; set; }

        /// <summary>
        ///     Gets, Sets  PoNum.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Purchase Order Number / Merchant related
        ///         data.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PONUM</code>
        /// </remarks>
        public string PoNum { get; set; }

        /// <summary>
        ///     Gets, Sets  VatRegNum
        /// </summary>
        /// <remarks>
        ///     <para>VAT registration number.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>VATREGNUM</code>
        /// </remarks>
        public string VatRegNum { get; set; }

        /// <summary>
        ///     Gets, Sets  VatTaxAmt.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         VAT Tax Amount. Amount should always be a decimal.
        ///         Exact amount to the cent (34.00, not 34).
        ///         Do not include comma separators. Use 1199.95
        ///         instead of 1,199.95.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>VATTAXAMT</code>
        /// </remarks>
        public Currency VatTaxAmt { get; set; }

        /// <summary>
        ///     Gets, Sets  LocalTaxAmt.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Local Tax Amount. Amount should always be a decimal.
        ///         Exact amount to the cent (34.00, not 34).
        ///         Do not include comma separators. Use 1199.95
        ///         instead of 1,199.95.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>LOCALTAXAMT</code>
        /// </remarks>
        public Currency LocalTaxAmt { get; set; }

        /// <summary>
        ///     Gets, Sets  NationalTaxAmt.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         National Tax Amount. Amount should always be a decimal.
        ///         Exact amount to the cent (34.00, not 34).
        ///         Do not include comma separators. Use 1199.95
        ///         instead of 1,199.95.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>NATIONALTAXAMT</code>
        /// </remarks>
        public Currency NationalTaxAmt { get; set; }

        /// <summary>
        ///     Gets, Sets  AltTaxAmt.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Alternate Tax Amount. Amount should always be a decimal.
        ///         Exact amount to the cent (34.00, not 34).
        ///         Do not include comma separators. Use 1199.95
        ///         instead of 1,199.95.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ALTTAXAMT</code>
        /// </remarks>
        public Currency AltTaxAmt { get; set; }

        /// <summary>
        ///     Gets, Sets  BrowserInfo.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Use this property to set the browser
        ///         related information of the purchase order.
        ///     </para>
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	.................
        /// 	// Inv is the Invoice object
        /// 	.................
        /// 	// Set the Browser Info details.
        /// 	BrowserInfo Browser = New BrowserInfo();
        /// 	Browser.BrowserCountryCode = "USA";
        /// 	Browser.BrowserUserAgent = "IE 6.0";
        /// 	Inv.BrowserInfo = Browser;
        /// 	.................
        /// 	</code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	.................
        /// 	' Inv is the Invoice object
        /// 	.................
        /// 	' Set the Browser Info details.
        /// 	Dim Browser As BrowserInfo = New BrowserInfo
        /// 	Browser.BrowserCountryCode  = "USA"
        /// 	Browser.BrowserUserAgent = "IE 6.0"
        /// 	Inv.BrowserInfo = Browser
        /// 	.................
        /// 	</code>
        /// </example>
        public BrowserInfo BrowserInfo { get; set; }

        /// <summary>
        ///     Gets, Sets  CustomerInfo.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Use this property to set the customer
        ///         related information of the purchase order.
        ///     </para>
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	.................
        /// 	// Inv is the Invoice object
        /// 	.................
        /// 	// Set the Customer Info details.
        /// 	CustomerInfo Cust = New CustomerInfo();
        /// 	Cust.CustCode = "CustXXXXX";
        /// 	Cust.CustIP = "255.255.255.255";
        /// 	Inv.CustomerInfo = Cust;
        /// 	.................
        /// 	</code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	.................
        /// 	' Inv is the Invoice object
        /// 	.................
        /// 	' Set the Customer Info details.
        /// 	Dim Cust As CustomerInfo = New CustomerInfo
        /// 	Cust.CustCode = "CustXXXXX"
        /// 	Cust.CustIP = "255.255.255.255"
        /// 	Inv.CustomerInfo = Cust
        /// 	.................
        /// 	</code>
        /// </example>
        public CustomerInfo CustomerInfo { get; set; }

        /// <summary>
        ///     Gets, Sets  UserItem.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Use this property to set the user
        ///         related information that is echoed back in the response.
        ///     </para>
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	.................
        /// 	// Inv is the Invoice object
        /// 	.................
        /// 	// Set the User Item details.
        ///  UserItem nUser = new UserItem();
        ///  nUser.UserItem1 = "ABCDEF";
        ///  nUser.UserItem2 = "GHIJKL";
        ///  Inv.UserItem = nUser;
        /// 	.................
        /// 	</code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	.................
        /// 	' Inv is the Invoice object
        /// 	.................
        /// 	' Set the User Item details.
        ///  Dim nUser As New UserItem
        ///  nUser.UserItem1 = "ABCDEF"
        ///  nUser.UserItem2 = "GHIJKL"
        ///  Inv.UserItem = nUser
        /// 	.................
        /// 	</code>
        /// </example>
        public UserItem UserItem { get; set; }

        /// <summary>
        ///     Gets, Sets  OrderDate.
        /// </summary>
        /// <remarks>
        ///     <para>Order date.</para>
        ///     <para>Format: mmddyy</para>
        ///     <para>mm - Month, dd - Day, yy - Year.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ORDERDATE</code>
        /// </remarks>
        public string OrderDate { get; set; }

        /// <summary>
        ///     Gets, Sets  OrderTime.
        /// </summary>
        /// <remarks>
        ///     <para>Order Time.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ORDERTIME</code>
        /// </remarks>
        public string OrderTime { get; set; }

        /// <summary>
        ///     Gets, Sets  CommCode.
        /// </summary>
        /// <remarks>
        ///     <para>Commodity Code.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>COMMCODE</code>
        /// </remarks>
        public string CommCode { get; set; }

        /// <summary>
        ///     Gets, Sets  VATTAXPERCENT.
        /// </summary>
        /// <remarks>
        ///     <para>VAT Tax percentage.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>VATTAXPERCENT</code>
        /// </remarks>
        public string VatTaxPercent { get; set; }

        /// <summary>
        ///     Gets, Sets  Recurring.
        /// </summary>
        /// <remarks>
        ///     <para>Is a recurring transaction? Y or N.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>RECURRING</code>
        /// </remarks>
        public string Recurring { get; set; }

        /// <summary>
        ///     Gets, Sets line item Amount.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Item Amount. Amount should always be a decimal.
        ///         Exact amount to the cent (34.00, not 34).
        ///         Do not include comma separators. Use 1199.95
        ///         instead of 1,199.95.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ITEMAMT</code>
        /// </remarks>
        public Currency ItemAmt { get; set; }

        /// <summary>
        ///     Gets, Sets  OrderDesc.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ORDERDESC</code>
        /// </remarks>
        public string OrderDesc { get; set; }

        // ADDED 04/07/07 TS.
        /// <summary>
        ///     Gets, Sets  RecurringType.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         UK Only: The type of transaction occurrence.
        ///         Values are: F = First occurrence, S = Subsequent
        ///         occurrence (default).
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>RECURRINGTYPE</code>
        /// </remarks>
        public string RecurringType { get; set; }

        /// <summary>
        ///     Gets, Sets  MerchDescr
        /// </summary>
        /// <remarks>
        ///     <para>Merchant's description.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>MERCHDESCR</code>
        /// </remarks>
        public string MerchDescr { get; set; }

        /// <summary>
        ///     Gets, Sets  MerchSvc
        /// </summary>
        /// <remarks>
        ///     <para>Merchant's contact number.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>MERCHSVC</code>
        /// </remarks>
        public string MerchSvc { get; set; }

        /// <summary>
        ///     Gets, Sets  OrderId
        /// </summary>
        /// <remarks>
        ///     <para>Order ID is used to prevent duplicate "orders" from being processed.</para>
        ///     <para>This is NOT the same as Request ID; which is used at the transaction level.</para>
        ///     <para>Order ID (ORDERID) is used to check for a duplicate order in the future.</para>
        ///     <para>For example, if you pass ORDERID=10101 and in two weeks another order is processed</para>
        ///     <para>with the same ORDERID, a duplicate condition will occur.  The results you receive</para>
        ///     <para>will be from the original order with DUPLICATE=2 to show that it was ORDERID that</para>
        ///     <para>triggered the duplicate.  The order id is stored for 3 years.</para>
        ///     <para></para>
        ///     <para>Important Note: Order ID functionality to catch duplicate orders processed withing</para>
        ///     <para>seconds of each other is limited.  Order ID should be used in conjunction with Request ID</para>
        ///     <para>to prevent duplicates due to processing / communication errors. DO NOT use ORDERID</para>
        ///     <para>as your only means to check for duplicate transactions.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ORDERID</code>
        /// </remarks>
        public string OrderId { get; set; }

        /// <summary>
        ///     Gets, Sets  EchoData
        /// </summary>
        /// <remarks>
        ///     <para>Echo Data is used to "echo" back data sent for processing in the response.</para>
        ///     <para>For example, if you send "ECHODATA=ADDRESS" then the Billing Address fields</para>
        ///     <para>will be returned in the response.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ECHODATA</code>
        /// </remarks>
        public string EchoData { get; set; }

        #endregion

        #region "LineItem related Methods"

        /// <summary>
        ///     Adds a line item to line item list.
        /// </summary>
        /// <param name="item">Lineitem object</param>
        /// <remarks>
        ///     <para>
        ///         Use this method to add a line item in
        ///         the purchase order.
        ///     </para>
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	.................
        /// 	// Inv is the Invoice object
        /// 	.................
        /// 	// Set the line item details.
        /// 	LineItem Item = New LineItem();
        /// 	Item.PickupStreet = "685A E. Middlefield Rd.";
        /// 	Item.PickupState = "CA";
        /// 	Inv.AddLineItem(Item);
        /// 	.................
        /// 	</code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	.................
        /// 	' Inv is the Invoice object
        /// 	.................
        /// 	' Set the Customer Info details.
        /// 	Dim Item As LineItem = New LineItem
        /// 	Item.PickupStreet = "685A E. Middlefield Rd."
        /// 	Item.PickupState = "CA"
        /// 	Inv.AddLineItem(Item);
        /// 	.................
        /// 	</code>
        /// </example>
        public void AddLineItem(LineItem item)
        {
            _mItemList.Add(item);
        }

        /// <summary>
        ///     Removes a line item from line item list.
        /// </summary>
        /// <param name="index">Index of lineitem to be removed.</param>
        /// <remarks>
        ///     <para>
        ///         Use this method to remove a line item at a particular
        ///         index in the purchase order.
        ///     </para>
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	.................
        /// 	// Inv is the Invoice object
        /// 	.................
        /// 	// Remove item at index 0
        /// 	Inv.RemoveLineItem(0);
        /// 	.................
        /// 	</code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	.................
        /// 	' Inv is the Invoice object
        /// 	.................
        /// 	' Remove item at index 0;
        /// 	Inv.RemoveLineItem(0)
        /// 	.................
        /// 	</code>
        /// </example>
        public void RemoveLineItem(int index)
        {
            _mItemList.RemoveAt(index);
        }

        /// <summary>
        ///     Clears the line item list.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Use this method to clear all the
        ///         line items added to the purchase order.
        ///     </para>
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 	.................
        /// 	// Inv is the Invoice object
        /// 	.................
        /// 	// Remove all line items.
        /// 	Inv.RemoveAllLineItems();
        /// 	.................
        /// 	</code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 	.................
        /// 	' Inv is the Invoice object
        /// 	.................
        /// 	' Remove all line items.
        /// 	Inv.RemoveAllLineItems()
        /// 	.................
        /// 	</code>
        /// </example>
        public void RemoveAllLineItems()
        {
            _mItemList.Clear();
        }

        /// <summary>
        ///     Generates transaction request for line items
        /// </summary>
        private void GenerateItemRequest()
        {
            for (var index = 0; index < _mItemList.Count; index++)
            {
                var item = (LineItem) _mItemList[index];
                if (item != null)
                {
                    item.RequestBuffer = RequestBuffer;
                    item.GenerateRequest(index);
                }
            }
        }

        #endregion
    }
}