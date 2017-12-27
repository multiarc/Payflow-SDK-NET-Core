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
    ///     Used for ExpressCheckout Do operation.
    /// </summary>
    /// <remarks>
    ///     <seealso cref="ExpressCheckoutResponse" />
    ///     <seealso cref="EcGetResponse" />
    /// </remarks>
    public class EcDoResponse : ExpressCheckoutResponse
    {
        #region "Constructor"

        /// <summary>
        ///     constructor
        /// </summary>
        internal EcDoResponse()
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
                Amt = (string) responseHashTable[PayflowConstants.ParamAmt];
                SettleAmt = (string) responseHashTable[PayflowConstants.ParamSettleamt];
                TaxAmt = (string) responseHashTable[PayflowConstants.ParamTaxamt];
                ExchangeRate = (string) responseHashTable[PayflowConstants.ParamExchangerate];
                PaymentDate = (string) responseHashTable[PayflowConstants.ParamPaymentdate];
                PaymentStatus = (string) responseHashTable[PayflowConstants.ParamPaymentstatus];
                BaId = (string) responseHashTable[PayflowConstants.ParamBaid];

                responseHashTable.Remove(PayflowConstants.ParamAmt);
                responseHashTable.Remove(PayflowConstants.ParamFeeamt);
                responseHashTable.Remove(PayflowConstants.ParamSettleamt);
                responseHashTable.Remove(PayflowConstants.ParamTaxamt);
                responseHashTable.Remove(PayflowConstants.ParamExchangerate);
                responseHashTable.Remove(PayflowConstants.ParamPendingreason);
                responseHashTable.Remove(PayflowConstants.ParamPaymentdate);
                responseHashTable.Remove(PayflowConstants.ParamPaymentstatus);
                responseHashTable.Remove(PayflowConstants.ParamPaymenttype);
                responseHashTable.Remove(PayflowConstants.ParamBaid);
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

        #region "Member variable"

        #endregion

        #region "properties"

        /// <summary>
        ///     Gets the amt parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>AMT</code>
        /// </remarks>
        public string Amt { get; private set; }

        /// <summary>
        ///     Gets the settleamt parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>SETTLEAMT</code>
        /// </remarks>
        public string SettleAmt { get; private set; }

        /// <summary>
        ///     Gets the taxamt parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>TAXAMT</code>
        /// </remarks>
        public string TaxAmt { get; private set; }

        /// <summary>
        ///     Gets the exchangerate parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>EXCHANGERATE</code>
        /// </remarks>
        public string ExchangeRate { get; private set; }

        /// <summary>
        ///     Gets the PaymentDate parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PAYMENTDATE</code>
        /// </remarks>
        public string PaymentDate { get; private set; }

        /// <summary>
        ///     Gets the PaymentStatus parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PAYMENTSTATUS</code>
        /// </remarks>
        public string PaymentStatus { get; private set; }

        /// <summary>
        ///     Gets the BAID parameter
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>BAID</code>
        /// </remarks>
        public string BaId { get; private set; }

        #endregion
    }
}