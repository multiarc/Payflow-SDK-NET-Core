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

using PFProSDK.Common.Utility;

#endregion

namespace PFProSDK.DataObjects
{
    /// <summary>
    ///     Used for ExpressCheckout with Billing Agreement (Reference Transaction) without Purchase DO operation.
    /// </summary>
    /// <remarks>
    ///     <seealso cref="EcGetBaRequest" />
    ///     <seealso cref="EcSetBaRequest" />
    ///     <seealso cref="EcdoBaRequest" />
    /// </remarks>
    public class EcUpdateBaRequest : ExpressCheckoutRequest
    {
        #region "Core functions"

        /// <summary>
        ///     Generates the transaction request.
        /// </summary>
        internal override void GenerateRequest()
        {
            // This function is not called. All the
            //address information is validated and generated
            //in its respective derived classes.
            base.GenerateRequest();
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamBaStatus, BaStatus));
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamBaDesc, BaDesc));
            RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamBaid, BaId));
        }

        #endregion

        #region "Member Variables"

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets or Sets the ba_status parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>BA_STATUS</code>
        /// </remarks>
        public string BaStatus { get; set; }

        /// <summary>
        ///     Gets or Sets the baid parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>BAID</code>
        /// </remarks>
        public string BaId { get; set; }

        /// <summary>
        ///     Gets or Sets the ba_desc parameter.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>BA_DESC</code>
        /// </remarks>
        public string BaDesc { get; set; }

        #endregion

        #region "Constructor"

        /// <summary>
        ///     Constructor for ECDoBARequest
        /// </summary>
        /// <param name="baId">String</param>
        /// <remarks>
        ///     ECDoBARequest is used to set the data required for a Express Checkout UPDATE operation
        ///     with Billing Agreement (Reference Transaction) without Purchase.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		
        /// 		//Create the ECUpdateBARequest object
        /// 		ECUpdateBARequest UpdateEC = new ECUpdateBARequest("[baid]");
        /// 		
        /// 		.............
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		.............
        /// 		
        /// 		//Create the ECUpdateBARequest object
        /// 		Dim UpdateEC As ECUpdateBARequest = new ECUpdateBARequest("[baid]")
        /// 		
        /// 		.............
        ///  </code>
        /// </example>
        public EcUpdateBaRequest(string baId) : base(PayflowConstants.ActiontypeUpdateba)
        {
            BaId = baId;
        }

        /// <summary>
        ///     Constructor for ECDoBARequest
        /// </summary>
        /// <param name="baId">String</param>
        /// <param name="baStatus">String</param>
        /// <remarks>
        ///     ECDoBARequest is used to set the data required for a Express Checkout UPDATE operation
        ///     with Billing Agreement (Reference Transaction) without Purchase.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		
        /// 		//Create the ECUpdateBARequest object
        /// 		ECUpdateBARequest UpdateEC = new ECUpdateBARequest("[baid]", "[ba_status]");
        /// 		
        /// 		.............
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		.............
        /// 		
        /// 		//Create the ECUpdateBARequest object
        /// 		Dim UpdateEC As ECUpdateBARequest = new ECUpdateBARequest("[baid]", "[ba_status]")
        /// 		
        /// 		.............
        ///  </code>
        /// </example>
        public EcUpdateBaRequest(string baId, string baStatus) : base(PayflowConstants.ActiontypeUpdateba)
        {
            BaId = baId;
            BaStatus = baStatus;
        }

        /// <summary>
        ///     Constructor for ECDoBARequest
        /// </summary>
        /// <param name="baId">String</param>
        /// <param name="baStatus">String</param>
        /// <param name="baDesc">String</param>
        /// <remarks>
        ///     ECDoBARequest is used to set the data required for a Express Checkout UPDATE operation
        ///     with Billing Agreement (Reference Transaction) without Purchase.
        /// </remarks>
        /// <example>
        ///     <code lang="C#" escaped="false">
        /// 		.............
        /// 		
        /// 		//Create the ECUpdateBARequest object
        /// 		ECUpdateBARequest UpdateEC = new ECUpdateBARequest("[baid]", "[ba_status]", ["ba_desc"]);
        /// 		
        /// 		.............
        ///  </code>
        ///     <code lang="Visual Basic" escaped="false">
        /// 		.............
        /// 		
        /// 		//Create the ECUpdateBARequest object
        /// 		Dim UpdateEC As ECUpdateBARequest = new ECUpdateBARequest("[baid]", "[ba_status]", ["ba_desc"])
        /// 		
        /// 		.............
        ///  </code>
        /// </example>
        public EcUpdateBaRequest(string baId, string baStatus, string baDesc) : base(
            PayflowConstants.ActiontypeUpdateba)
        {
            BaId = baId;
            BaStatus = baStatus;
            BaDesc = baDesc;
        }

        #endregion
    }
}