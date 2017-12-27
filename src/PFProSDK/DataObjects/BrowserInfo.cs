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
    ///     Used for Browser related information.
    /// </summary>
    /// <remarks>
    ///     Use the BrowserInfo object for the user
    ///     browser related information.
    /// </remarks>
    /// <example>
    ///     <para>
    ///         Following example shows how to use a
    ///         Browser Info object.
    ///     </para>
    ///     <code lang="C#" escaped="false">
    /// 	.................
    /// 	// Inv is the Invoice object
    /// 	.................
    /// 	// Set the Browser Info details.
    /// 	BrowserInfo Browser = New BrowserInfo();
    /// 	Browser.BrowserCountryCode = "USA";
    /// 	Browser.BrowserUserAgent = "IE 6.0";
    /// 	Inv.BrowserInfo = Browser;
    /// 	.................
    /// 	</code>
    ///     <code lang="Visual Basic" escaped="false">
    /// 	.................
    /// 	' Inv is the Invoice object
    /// 	.................
    /// 	' Set the Browser Info details.
    /// 	Dim Browser As BrowserInfo = New BrowserInfo
    /// 	Browser.BrowserCountryCode  = "USA"
    /// 	Browser.BrowserUserAgent = "IE 6.0"
    /// 	Inv.BrowserInfo = Browser
    /// 	.................
    /// 	</code>
    /// </example>
    public sealed class BrowserInfo : BaseRequestDataObject
    {
        #region "Core functions"

        /// <summary>
        ///     Generates the transaction request.
        /// </summary>
        internal override void GenerateRequest()
        {
            try
            {
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamBrowsertime, BrowserTime));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamBrowsercountrycode,
                    BrowserCountryCode));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamBrowseruseragent,
                    BrowserUserAgent));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamCustom, Custom));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamButtonsource,
                    ButtonSource));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamNotifyurl, NotifyUrl));
                RequestBuffer.Append(PayflowUtility.AppendToRequest(PayflowConstants.ParamMerchantsessionid,
                    MerchantSessionId));
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

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets, Sets Browser time.
        /// </summary>
        /// <remarks>
        ///     <para>Browser's local time.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>BROWSERTIME</code>
        /// </remarks>
        public string BrowserTime { get; set; }

        /// <summary>
        ///     Gets, Sets Browser Country code.
        /// </summary>
        /// <remarks>
        ///     <para>Browser's local Country Code.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>BROWSERCOUNTRYCODE</code>
        /// </remarks>
        public string BrowserCountryCode { get; set; }

        /// <summary>
        ///     Gets, Sets Browser user agent.
        /// </summary>
        /// <remarks>
        ///     <para>Browser's user agent.</para>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>COUNTRYCODE</code>
        /// </remarks>
        public string BrowserUserAgent { get; set; }

        /// <summary>
        ///     Gets or Sets the custom parameter for Direct Payment and Express checkout.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>CUSTOM</code>
        /// </remarks>
        public string Custom { get; set; }

        /// <summary>
        ///     Gets or Sets the buttonsource parameter for Direct Payment and Express checkout.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>BUTTONSOURCE</code>
        /// </remarks>
        public string ButtonSource { get; set; }

        /// <summary>
        ///     Gets or Sets the NotifyUrl parameter for Direct Payment and Express checkout.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>NOTIFYURL</code>
        /// </remarks>
        public string NotifyUrl { get; set; }

        /// <summary>
        ///     Gets or Sets the MerchantSessionId parameter for Direct Payment.
        /// </summary>
        /// <remarks>
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>MERCHANTSESSIONID</code>
        /// </remarks>
        public string MerchantSessionId { get; set; }

        #endregion
    }
}