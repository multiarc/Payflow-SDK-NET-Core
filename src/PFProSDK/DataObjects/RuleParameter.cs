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

#endregion

namespace PFProSDK.DataObjects
{
    /// <summary>
    ///     Used for fraud rule parameter
    /// </summary>
    /// <remarks>Rule parameter are the parameters of each fraud rule.</remarks>
    public class RuleParameter : BaseResponseDataObject
    {
        #region "Constructors"

        /// <summary>
        ///     Constructor for RuleParameter
        /// </summary>
        internal RuleParameter()
        {
        }

        #endregion

        #region "Member Variables"

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets Num
        /// </summary>
        /// <remarks>This is the fraud rule parameter number.</remarks>
        public int Num { get; set; }

        /// <summary>
        ///     Gets Name
        /// </summary>
        /// <remarks>This is the fraud rule parameter name.</remarks>
        public string Name { get; set; }

        /// <summary>
        ///     Gets Value
        /// </summary>
        /// <remarks>This is the fraud rule parameter value.</remarks>
        public string Value { get; set; }

        /// <summary>
        ///     Gets Type
        /// </summary>
        /// <remarks>This is the fraud rule parameter type.</remarks>
        public string Type { get; set; }

        #endregion
    }
}