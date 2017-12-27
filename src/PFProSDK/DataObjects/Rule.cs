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

using System.Collections;

#endregion

namespace PFProSDK.DataObjects
{
    /// <summary>
    ///     Used for fraud rule information
    /// </summary>
    /// <remarks>These are the fraud rules applied for the transaction.</remarks>
    public class Rule : BaseResponseDataObject
    {
        #region "Constructors"

        /// <summary>
        ///     Constructor for Rule
        /// </summary>
        internal Rule()
        {
            RuleVendorParms = new ArrayList();
        }

        #endregion

        #region "Member Variables"

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets, Sets Num
        /// </summary>
        /// <remarks>This is the fraud rule number.</remarks>
        public int Num { get; set; }

        /// <summary>
        ///     Gets, Sets RuleId
        /// </summary>
        /// <remarks>This is the fraud rule id.</remarks>
        public string RuleId { get; set; }

        /// <summary>
        ///     Gets, Sets RuleAlias
        /// </summary>
        /// <remarks>This is the fraud rule alias.</remarks>
        public string RuleAlias { get; set; }

        /// <summary>
        ///     Gets, Sets RuleDescription
        /// </summary>
        /// <remarks>This is the fraud rule description.</remarks>
        public string RuleDescription { get; set; }

        /// <summary>
        ///     Gets, Sets Action
        /// </summary>
        /// <remarks>This is the fraud rule action.</remarks>
        public string Action { get; set; }

        /// <summary>
        ///     Gets, Sets TriggeredMessage
        /// </summary>
        /// <remarks>This is the fraud rule triggered message.</remarks>
        public string TriggeredMessage { get; set; }

        /// <summary>
        ///     Gets, Sets RuleVendorParms
        /// </summary>
        /// <remarks>
        ///     This is the fraud rule vendor params arraylist
        ///     containing objects of RuleParameter.
        /// </remarks>
        public ArrayList RuleVendorParms { get; }

        #endregion
    }
}