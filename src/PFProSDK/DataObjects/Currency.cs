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
    ///     This class is used as the currency data type
    ///     by all data and transaction objects.
    /// </summary>
    /// <remarks>
    ///     This class should be used to denote any
    ///     currency parameter. By default, the currency code is
    ///     USD (US Dollars).
    /// </remarks>
    /// <example>
    ///     Following example shows how to use this class.
    ///     <code lang="C#" escaped="false">
    /// 		.............
    /// 		//Inv is the Invoice object
    /// 		.............
    /// 		
    ///      // Set the amount object.
    ///      // Currency Code USD is US ISO currency code.  If no code passed, USD is default.
    ///      // See the Developer's Guide regarding the CURRENCY parameter for the list of
    ///      // three-character currency codes available.
    ///      Currency Amt = new Currency(new decimal(25.25), "USD");  
    /// 		
    ///      // A valid amount has either no decimal value or only a two decimal value. 
    ///      // An invalid amount will generate a result code 4.
    ///      //
    ///      // For values which have more than two decimal places such as:
    ///      // Currency Amt = new Currency(new Decimal(25.2575));
    ///      // You will either need to truncate or round as needed using the following properties:
    ///      //
    ///      // If the NoOfDecimalDigits property is used then it is mandatory to set one of the following
    ///      // properties to true.
    ///      //
    ///      //Amt.Round = true;
    ///      //Amt.Truncate = true;
    ///      //
    ///      // For Currencies without a decimal, you'll need to set the NoOfDecimalDigits = 0.
    ///      //Amt.NoOfDecimalDigits = 0;
    /// 		
    /// 		//Set the amount in the invoice object
    /// 		Inv.Amt = Amt;
    /// 		.............
    ///  </code>
    ///     <code lang="Visual Basic" escaped="false">
    /// 		.............
    /// 		'Inv is the Invoice object
    /// 		.............
    /// 		
    /// 		' Set the amount object.
    ///      ' Currency Code USD is US ISO currency code.  If no code passed, USD is default.
    ///      ' See the Developer's Guide for the list of three-character currency codes available.
    ///      Dim Amt As New Currency(New Decimal(25.25), "USD")
    ///      
    /// 		' A valid amount has either no decimal value or only a two decimal value. 
    /// 		' An invalid amount will generate a result code 4.
    ///      '
    ///      ' For values which have more than two decimal places such as:
    ///      ' Dim Amt As New Currency(New Decimal(25.2575))
    ///      ' You will either need to truncate or round as needed using the following property: Amt.NoOfDecimalDigits
    ///      '
    ///      ' If the NoOfDecimalDigits property is used then it is mandatory to set one of the following
    ///      ' properties to true.
    ///      '
    ///      'Amt.Round = true
    ///      'Amt.Truncate = true
    ///      '
    ///      ' For Currencies without a decimal, you'll need to set the NoOfDecimalDigits = 0.
    ///      'Amt.NoOfDecimalDigits = 0
    /// 		
    /// 		'Set the amount in the invoice object
    /// 		Inv.Amt = Amt;
    /// 		.............
    ///  </code>
    /// </example>
    public sealed class Currency : BaseRequestDataObject
    {
        #region "System.Object Overrides"

        /// <summary>
        ///     Overrides ToString
        /// </summary>
        /// <returns>String value of currency</returns>
        /// <remarks>Formats string value of currency in format "$.CC"</remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		//Inv is the Invoice object
        /// 		.............
        /// 		
        /// 		//Set the invoice amount.
        /// 		Inv.Amt = new Currency(new Decimal(25.12),"USD");
        /// 		String CurrValue = Inv.ToString();
        /// 		.............
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		.............
        /// 		'Inv is the Invoice object
        /// 		.............
        /// 		
        /// 		'Set the invoice amount.
        /// 		Inv.Amt = New Currency(New Decimal(25.12),"USD")
        /// 		CurrValue As String = Inv.ToString
        /// 		.............
        ///  </code>
        /// </example>
        public override string ToString()
        {
            try
            {
                //Overridden ToString. Returns held Currency Value.
                var retVal = PayflowConstants.EmptyString;
                // We need to double check here whether currency value
                // is non-zero positive before converting it.

                // PPSCR00563818 - .NET SDK: Foreign currency not supported
                // Added Globalization - 15/10/07 - tsieber

                // Removed Globalization - Oct 03 2012, tsieber - as GW cannot handle comma's in amounts yet.

                // Creates a CultureInfo for English in the U.S.
                //CultureInfo usCulture = new CultureInfo("en-US");
                // Sets the CurrentCulture to en_US.
                //Thread.CurrentThread.CurrentCulture = usCulture;

                // Clones the NumberFormatInfo and creates
                // a new object for the local currency of France.
                //NumberFormatInfo LocalFormat = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
                // Replaces the default currency symbol with the 
                // local currency symbol.
                //LocalFormat.CurrencySymbol = "";


                if (_mNoOfDecimalDigits < 0) _mNoOfDecimalDigits = 2;

                if (_mRound && _mTruncate)
                {
                    var err = PayflowUtility.PopulateCommError(PayflowConstants.ECurrencyProcessError, null,
                        PayflowConstants.SeverityFatal, false, null);
                    throw new DataObjectException(err);
                }

                if (_mRound)
                {
                    _mCurrencyValue = decimal.Round(_mCurrencyValue, _mNoOfDecimalDigits);
                    retVal = RoundCurrencyValue(_mCurrencyValue.ToString(), _mNoOfDecimalDigits);
                    //RetVal = mCurrencyValue.ToString("c", LocalFormat);
                }
                else if (_mTruncate)
                {
                    //RetVal = TruncateCurrencyValue(mCurrencyValue.ToString("c", LocalFormat),mNoOfDecimalDigits);
                    retVal = TruncateCurrencyValue(_mCurrencyValue.ToString(), _mNoOfDecimalDigits);
                }
                else
                {
                    //RetVal = mCurrencyValue.ToString("c", LocalFormat);
                    retVal = _mCurrencyValue.ToString();
                }

                var indexOfDecimal = retVal.IndexOf(".", 1);
                if (indexOfDecimal < 0 && _mNoOfDecimalDigits != 0) retVal += ".00";
                var tempStr = retVal.Substring(indexOfDecimal + 1, retVal.Length - indexOfDecimal - 1);
                var len = tempStr.Length;
                if (len < 2)
                    for (var i = len; i < 2; i++)
                        retVal = retVal + "0";
                return retVal;
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
        ///     Currency Value
        /// </summary>
        private decimal _mCurrencyValue;

        private bool _mRound;
        private bool _mTruncate;
        private int _mNoOfDecimalDigits = 2;

        #endregion

        #region "Constructors"

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="currencyValue">Currency value</param>
        /// <remarks>Currency code is set as default USD.</remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		//Inv is the Invoice object
        /// 		.............
        /// 		
        /// 		//Set the invoice amount.
        /// 		Inv.Amt = new Currency(new Decimal(25.12));
        /// 		
        /// 		.............
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		.............
        /// 		'Inv is the Invoice object
        /// 		.............
        /// 		
        /// 		'Set the invoice amount.
        /// 		Inv.Amt = New Currency(New Decimal(25.12))
        /// 		
        /// 		.............
        ///  </code>
        /// </example>
        public Currency(decimal currencyValue)
        {
            _mCurrencyValue = currencyValue;
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="currencyValue">Currency value</param>
        /// <param name="currencyCode">3 letter Currency code</param>
        /// <remarks>Currency code if not given is set as default USD.</remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		//Inv is the Invoice object
        /// 		.............
        /// 		
        /// 		//Set the invoice amount.
        /// 		Inv.Amt = new Currency(new Decimal(25.12),"USD");
        /// 		
        /// 		.............
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		.............
        /// 		'Inv is the Invoice object
        /// 		.............
        /// 		
        /// 		'Set the invoice amount.
        /// 		Inv.Amt = New Currency(New Decimal(25.12),"USD")
        /// 		
        /// 		.............
        ///  </code>
        /// </example>
        public Currency(decimal currencyValue, string currencyCode) : this(currencyValue)
        {
            if (currencyCode != null && currencyCode.Length > 0) CurrencyCode = currencyCode;
        }

        #endregion

        #region "Properties"

        /// <summary>
        ///     Sets Currency value rounding flag to true.
        ///     Note that only one of round OR truncate can be set to true.
        /// </summary>
        public bool Round
        {
            set => _mRound = value;
        }

        /// <summary>
        ///     Sets Currency value truncation flag to true.
        ///     Note that only one of round OR truncate can be set to true.
        /// </summary>
        public bool Truncate
        {
            set => _mTruncate = value;
        }

        /// <summary>
        ///     Sets the number of decimal digits required after rounding or truncation.
        /// </summary>
        public int NoOfDecimalDigits
        {
            set => _mNoOfDecimalDigits = value;
        }

        /// <summary>
        ///     Gets the currency code..
        /// </summary>
        public string CurrencyCode { get; } = PayflowConstants.CurrencycodeDefault;

        #endregion

        #region "Core functions"

        #endregion

        #region "Rounding and Truncation Methods"

        /// <summary>
        ///     Rounds the currency String value
        /// </summary>
        /// <param name="currStringValue">Currency String Value</param>
        /// <param name="noOfdecimalDigits">Number of decimal digits to round to</param>
        /// <returns>Rounded Currency String value</returns>
        internal string RoundCurrencyValue(string currStringValue, int noOfdecimalDigits)
        {
            string retVal;
            retVal = currStringValue;
            if (retVal == null || retVal.Length == 0) return PayflowConstants.EmptyString;

            var indexOfDecimal = retVal.IndexOf(".");
            var length = retVal.Length;

            if (indexOfDecimal > 0 && indexOfDecimal < length)
                if (indexOfDecimal == length - 1)
                {
                    for (var i = 0; i < noOfdecimalDigits; i++) retVal += "0";
                }
                else if (noOfdecimalDigits == 0)
                {
                    retVal = retVal.Substring(0, indexOfDecimal);
                }
                else
                {
                    var lenAfterTruncate = indexOfDecimal + noOfdecimalDigits + 1;

                    if (lenAfterTruncate > length)
                    {
                        var padding = lenAfterTruncate - length;
                        for (var i = 0; i < padding; i++) retVal += "0";
                    }
                    else if (lenAfterTruncate < length)
                    {
                        var trimming = length - lenAfterTruncate;
                        var endLen = length - 1;
                        for (var i = 0; i < trimming; i++)
                        {
                            var val = int.Parse(retVal.Substring(endLen, 1));
                            if (val >= 5)
                            {
                                var roundVal = int.Parse(retVal.Substring(endLen - 1, 1));
                                roundVal += 1;
                                if (roundVal >= 10) roundVal = 1;
                                retVal = retVal.Remove(endLen - 1, 2);
                                retVal = retVal.Insert(endLen - 1, roundVal.ToString());
                            }
                            else
                            {
                                retVal = retVal.Remove(endLen, 1);
                            }

                            endLen -= 1;
                        }
                    }
                }

            return retVal;
        }

        /// <summary>
        ///     Truncates the currency String value
        /// </summary>
        /// <param name="currStringValue">Currency String Value</param>
        /// <param name="noOfdecimalDigits">Number of decimal digits to round to</param>
        /// <returns>Truncated Currency String value</returns>
        internal string TruncateCurrencyValue(string currStringValue, int noOfdecimalDigits)
        {
            string retVal;
            retVal = currStringValue;
            if (retVal == null || retVal.Length == 0) return PayflowConstants.EmptyString;

            var indexOfDecimal = retVal.IndexOf(".");
            var length = retVal.Length;
            if (indexOfDecimal > 0 && indexOfDecimal <= length - 1)
                if (indexOfDecimal == length - 1)
                {
                    for (var i = 0; i < noOfdecimalDigits; i++) retVal += "0";
                }
                else if (noOfdecimalDigits == 0)
                {
                    retVal = retVal.Substring(0, indexOfDecimal);
                }

                else
                {
                    var lenAfterTruncate = indexOfDecimal + noOfdecimalDigits + 1;

                    if (lenAfterTruncate > length)
                    {
                        var padding = lenAfterTruncate - length;
                        for (var i = 0; i < padding; i++) retVal += "0";
                    }
                    else if (lenAfterTruncate < length)
                    {
                        retVal = retVal.Substring(0, lenAfterTruncate);
                    }
                }

            return retVal;
        }

        #endregion
    }
}