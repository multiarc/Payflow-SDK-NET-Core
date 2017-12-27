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
    ///     This class is used to create and use an ACH
    ///     ( Automatic Clearing House ) Tender type.
    /// </summary>
    /// <remarks>
    ///     BankAcct is the Payment device associated with this
    ///     tender type.
    /// </remarks>
    /// <seealso cref="BankAcct" />
    public sealed class AchTender : BaseTender
    {
        #region "Constructors"

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="bnkAcct">Bank Account object.</param>
        /// <remarks>
        ///     ACHTender should be used to perform the transactions
        ///     in which the user provides his bank account details for
        ///     the online payment processing.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>TENDER</code>
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		//BnkAcct is the populated BankAcct object.
        /// 		.............
        /// 		
        /// 		//Create the Tender object
        /// 		ACHTender Tender = new ACHTender(BnkAcct);
        /// 		
        /// 		.............
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		.............
        /// 		//BnkAcct is the populated BankAcct object.
        /// 		.............
        /// 		
        /// 		'Create the Tender object
        /// 		Dim Tender As ACHTender = new ACHTender(BnkAcct)
        /// 		
        /// 		.............
        ///  </code>
        ///     <seealso cref="BankAcct" />
        /// </example>
        public AchTender(BankAcct bnkAcct) : base(PayflowConstants.TendertypeAch, bnkAcct)
        {
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
                base.GenerateRequest();
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamAuthtype, AuthType));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamPrenote, PreNote));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamTermcity, TermCity));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamTermstate, TermState));
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
        ///     Sets,gets the Authtype.
        /// </summary>
        /// <remarks>
        ///     <para>The type of authorization received from the payer.</para>
        ///     <para>Allowed Auth Types are:</para>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>AUTHTYPE</term>
        ///             <description>Description</description>
        ///         </listheader>
        ///         <item>
        ///             <term>CCD</term>
        ///             <description>Default for B2B format accounts</description>
        ///         </item>
        ///         <item>
        ///             <term>PPD</term>
        ///             <description>
        ///                 Standard customer authorization method)
        ///                 for B2C format accounts.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>ARC</term>
        ///             <description>
        ///                 Accounts Receivables check entry
        ///                 for a single entry debit.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>RCK</term>
        ///             <description>
        ///                 Re-presented check entry for a
        ///                 single entry debit.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>WEB</term>
        ///             <description>
        ///                 The customer authorized the payment
        ///                 over the Internet.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>TEL</term>
        ///             <description>Debit authorization obtained by telephone.</description>
        ///         </item>
        ///         <item>
        ///             <term>POP</term>
        ///             <description>
        ///                 Point of Purchase check entry for a
        ///                 single entry debit.
        ///             </description>
        ///         </item>
        ///     </list>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>AUTHTYPE</code>
        /// </remarks>
        public string AuthType { get; set; }

        /// <summary>
        ///     Gets, Sets the Prenote.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Prenote indicates a prenotification payment with no amount.
        ///         Used to verify bank account validity. Receiving banks are not required
        ///         to respond to prenotification payments.
        ///     </para>
        ///     <para>Allowed Prenote values are:</para>
        ///     <list type="table">
        ///         <listheader>
        ///             <term>PRENOTE</term>
        ///             <description>Description</description>
        ///         </listheader>
        ///         <item>
        ///             <term>N</term>
        ///             <description>Default. AMT needs to be passed.</description>
        ///         </item>
        ///         <item>
        ///             <term>Y</term>
        ///             <description>Default. AMT does not need to be passed.</description>
        ///         </item>
        ///     </list>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PRENOTE</code>
        /// </remarks>
        public string PreNote { get; set; }

        /// <summary>
        ///     Gets, Sets the Termcity.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         City where the merchant's terminal is located.
        ///         Used only for POP.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>TERMCITY</code>
        /// </remarks>
        public string TermCity { get; set; }

        /// <summary>
        ///     Gets, Sets the Termstate.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         State where the merchant's terminal is located.
        ///         Used only for POP.
        ///     </para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>TERMSTATE</code>
        /// </remarks>
        public string TermState { get; set; }

        #endregion
    }
}