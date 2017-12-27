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
using PFProSDK.DataObjects;

#endregion

namespace PFProSDK.Transactions
{
    /// <summary>
    ///     This abstract class serves as base class for
    ///     Buyer auth transactions.
    /// </summary>
    public class BuyerAuthTransaction : BaseTransaction
    {
        #region "Core functions"

        /// <summary>
        ///     Generates the transaction request.
        /// </summary>
        internal override void GenerateRequest()
        {
            try
            {
                base.GenerateRequest();
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var te = new TransactionException(ex);
                throw te;
            }

            //catch
            //{
            //    throw new Exception();
            //}
        }

        #endregion

        #region "Constructors"

        /// <summary>
        ///     protected Constructor. This prevents
        ///     creation of an empty Transaction object.
        /// </summary>
        protected BuyerAuthTransaction()
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="trxType">Transaction type</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="payflowConnectionData">Connection credentials object.</param>
        /// <param name="requestId">Request Id</param>
        protected BuyerAuthTransaction(string trxType, UserInfo userInfo, PayflowConnectionData payflowConnectionData,
            string requestId)
            : base(trxType, userInfo, payflowConnectionData, requestId)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="trxType">Transaction type</param>
        /// <param name="userInfo">User Info object populated with user credentials.</param>
        /// <param name="requestId">Request Id</param>
        protected BuyerAuthTransaction(string trxType, UserInfo userInfo, string requestId)
            : this(trxType, userInfo, null, requestId)
        {
        }

        #endregion
    }
}