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
    ///     Used for BankAcct information.
    /// </summary>
    /// <remarks>
    ///     BankAcct is associated with ACHTender.
    ///     <seealso cref="AchTender" />
    /// </remarks>
    public sealed class BankAcct : PaymentDevice
    {
        #region "Constructors"

        /// <summary>
        ///     Constructor for BankAcct
        /// </summary>
        /// <param name="acct">Bank Account number</param>
        /// <param name="aba">Aba number</param>
        /// <remarks>
        ///     BankAcct should be used to perform the transactions
        ///     in which the user provides his bank account details for
        ///     the online payment processing.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		
        /// 		//Create the BankAcct object
        /// 		BankAcct Account = new BankAcct("XXXXXXXXXXX","XXXXXXXXXXX");
        /// 		
        /// 		.............
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		.............
        /// 		
        /// 		//Create the BankAcct object
        /// 		Dim Account As BankAcct = new BankAcct("XXXXXXXXXXX","XXXXXXXXXXX")
        /// 		
        /// 		.............
        ///  </code>
        /// </example>
        public BankAcct(string acct, string aba) : base(acct)
        {
            Aba = aba;
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
                //Get the request from base.			
                base.GenerateRequest();
                //Add ABA and ACCTTYPE to parameter list.
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamAba, Aba));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamAccttype, AcctType));
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
        ///     Gets, Sets Aba.
        /// </summary>
        /// <remarks>
        ///     <para>Target Bank's transit ABA routing number.</para>
        ///     <para>Appies only to ACH transactions.(8-digit number)</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ABA</code>
        /// </remarks>
        public string Aba { get; set; }

        /// <summary>
        ///     Gets, Sets Acct type
        /// </summary>
        /// <remarks>
        ///     <para>Customer's bank account type</para>
        ///     <para>Allowed AcctType values are:</para>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>ACCTTYPE</term>
        ///             <description>Description</description>
        ///         </listheader>
        ///         <item>
        ///             <term>C</term>
        ///             <description>Checking account</description>
        ///         </item>
        ///         <item>
        ///             <term>S</term>
        ///             <description>Savings account</description>
        ///         </item>
        ///     </list>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ACCTTYPE</code>
        /// </remarks>
        public string AcctType { get; set; }

        #endregion
    }
}