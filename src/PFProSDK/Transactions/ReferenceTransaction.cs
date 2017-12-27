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
using PFProSDK.DataObjects;

#endregion

namespace PFProSDK.Transactions
{
    /// <summary>
    ///     This class is used as base class for all reference transactions.
    /// </summary>
    /// <remarks>
    ///     This class can be derived to create a new reference transaction
    ///     or can be used as is to submit a new type of reference transaction.
    ///     <para>
    ///         A reference transaction is a transaction which always takes
    ///         the PNRef of a previously submitted transaction.
    ///     </para>
    /// </remarks>
    public class ReferenceTransaction : BaseTransaction
    {
        #region "Properties"

        /// <summary>
        ///     Gets, Sets OrgiPpref
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>ORIGPPREF</code>
        /// </remarks>
        public string OrigPpref { get; set; }

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
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamOrigid, _mOrigId));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamOrigppref, OrigPpref));
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                ex = new TransactionException(ex);
                throw ex;
            }
        }

        #endregion

        #region "Member Variables"

        /// <summary>
        ///     Original Transaction Id. Mandatory for any reference transaction.
        /// </summary>
        private readonly string _mOrigId;

        #endregion

        #region "Constructors"

        /// <summary>
        ///     protected Constructor. This prevents
        ///     creation of an empty Transaction object.
        /// </summary>
        protected ReferenceTransaction()
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="trxType">Transaction Type</param>
        /// <param name="origId">Original Transaction Id.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     This class can be derived to create a new reference transaction
        ///     or can be used as is to submit a new type of reference transaction.
        ///     <para>
        ///         A reference transaction is a transaction which always takes
        ///         the PNRef of a previously submitted transaction.
        ///     </para>
        /// </remarks>
        public ReferenceTransaction(string trxType,
            string origId,
            UserInfo userInfo,
            PayflowConnectionData payflowConnectionData,
            string requestId) : base(trxType, userInfo, payflowConnectionData, requestId)
        {
            _mOrigId = origId;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="trxType">Transaction Type</param>
        /// <param name="origId">Original Transaction Id.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     This class can be derived to create a new reference transaction
        ///     or can be used as is to submit a new type of reference transaction.
        ///     <para>
        ///         A reference transaction is a transaction which always takes
        ///         the PNRef of a previously submitted transaction.
        ///     </para>
        /// </remarks>
        public ReferenceTransaction(
            string trxType,
            string origId,
            UserInfo userInfo,
            string requestId) : base(trxType, userInfo, requestId)
        {
            _mOrigId = origId;
        }


        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="trxType">Transaction Type</param>
        /// <param name="origId">Original Transaction Id.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     This class can be derived to create a new reference transaction
        ///     or can be used as is to submit a new type of reference transaction.
        ///     <para>
        ///         A reference transaction is a transaction which always takes
        ///         the PNRef of a previously submitted transaction.
        ///     </para>
        /// </remarks>
        public ReferenceTransaction(string trxType,
            string origId,
            UserInfo userInfo,
            PayflowConnectionData payflowConnectionData,
            Invoice invoice, string requestId) : base(trxType, userInfo, payflowConnectionData, invoice, requestId)
        {
            _mOrigId = origId;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="trxType">Transaction Type</param>
        /// <param name="origId">Original Transaction Id.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     This class can be derived to create a new reference transaction
        ///     or can be used as is to submit a new type of reference transaction.
        ///     <para>
        ///         A reference transaction is a transaction which always takes
        ///         the PNRef of a previously submitted transaction.
        ///     </para>
        /// </remarks>
        public ReferenceTransaction(string trxType, string origId, UserInfo userInfo, Invoice invoice, string requestId)
            : this(trxType, origId, userInfo, null, invoice, requestId)
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="trxType">Transaction Type</param>
        /// <param name="origId">Original Transaction Id.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="tender">Tender object.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     This class can be derived to create a new reference transaction
        ///     or can be used as is to submit a new type of reference transaction.
        ///     <para>
        ///         A reference transaction is a transaction which always takes
        ///         the PNRef of a previously submitted transaction.
        ///     </para>
        /// </remarks>
        public ReferenceTransaction(string trxType,
            string origId,
            UserInfo userInfo,
            PayflowConnectionData payflowConnectionData,
            Invoice invoice,
            BaseTender tender, string requestId) : base(trxType, userInfo, payflowConnectionData, invoice, tender,
            requestId)
        {
            _mOrigId = origId;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="trxType">Transaction Type</param>
        /// <param name="origId">Original Transaction Id.</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="invoice">Invoice object.</param>
        /// <param name="tender">Tender object.</param>
        /// <param name="requestId">Request Id</param>
        /// <remarks>
        ///     This class can be derived to create a new reference transaction
        ///     or can be used as is to submit a new type of reference transaction.
        ///     <para>
        ///         A reference transaction is a transaction which always takes
        ///         the PNRef of a previously submitted transaction.
        ///     </para>
        /// </remarks>
        public ReferenceTransaction(string trxType, string origId, UserInfo userInfo, Invoice invoice,
            BaseTender tender, string requestId) : this(trxType, origId, userInfo, null, invoice, tender, requestId)
        {
        }

        #endregion
    }
}