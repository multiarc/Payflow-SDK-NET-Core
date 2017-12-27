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
    ///     This abstract class serves as base class of all
    ///     payment devices.
    /// </summary>
    /// <remarks>
    ///     <para>Each Payment Device is associated with a tender type .</para>
    ///     <para>
    ///         Following are the Payment Devices associated with
    ///         different tender types:
    ///     </para>
    ///     <list type="table">
    ///         <listheader>
    ///             <term>Payment Device Data Object</term>
    ///             <description>Tender type</description>
    ///         </listheader>
    ///         <item>
    ///             <term>BankAcct</term>
    ///             <description>
    ///                 <see cref="AchTender">ACHTender</see>
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>CreditCard, PurchaseCard, SwipeCard</term>
    ///             <description>
    ///                 <para>
    ///                     <see cref="CardTender">CardTender</see>
    ///                 </para>
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>CheckPayment</term>
    ///             <description>
    ///                 <see cref="CheckTender">CheckTender</see>
    ///             </description>
    ///         </item>
    ///     </list>
    /// </remarks>
    public class PaymentDevice : BaseRequestDataObject
    {
        #region "Core functions"

        /// <summary>
        ///     Generates the transaction request.
        /// </summary>
        internal override void GenerateRequest()
        {
            try
            {
                //Generate default NV Pair for Acct field.
                //This is with Name as ACCT. Used for CC,ACH trxns.
                //In case of telecheck, this will be MICR. Handled from derived class.
                //In case of Swipe , this will be SWIPE.Handled from derived class.
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamAcct, Acct));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamName, Name));
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

        #region "Constructors"

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <remarks>Abstract class. Instance cannot be created directly.</remarks>
        internal PaymentDevice()
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="acct">Payment device Number</param>
        /// <remarks>Abstract class. Instance cannot be created directly.</remarks>
        public PaymentDevice(string acct)
        {
            Acct = acct;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="acct">Payment device Number</param>
        /// <param name="name">Payment device holder's name</param>
        /// <remarks>Abstract class. Instance cannot be created directly.</remarks>
        public PaymentDevice(string acct, string name)
        {
            Acct = acct;
            Name = name;
        }

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets, Sets Acct
        /// </summary>
        /// <remarks>
        ///     Account holder's account number.
        ///     <para>Maps to Payflow Parameter:s as follows:</para>
        ///     <code>
        /// <list type="table">
        ///             <listheader>
        ///                 <term>Specific transaction</term>
        ///                 <description>Payflow Parameter</description>
        ///             </listheader>
        ///             <item>
        ///                 <term>Transactions with CreditCard, PurchaseCard, BankAcct payment devices</term>
        ///                 <description>ACCT</description>
        ///             </item>
        ///             <item>
        ///                 <term>Transactions with CheckPayment</term>
        ///                 <description>MICR</description>
        ///             </item>
        ///             <item>
        ///                 <term>Transactions with SwipeCard</term>
        ///                 <description>SWIPE</description>
        ///             </item>
        ///         </list>
        /// </code>
        /// </remarks>
        public virtual string Acct { get; }

        /// <summary>
        ///     Gets, Sets Name
        /// </summary>
        /// <remarks>
        ///     Account holder's name.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>NAME</code>
        /// </remarks>
        public string Name { get; set; }

        #endregion
    }
}