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
using System.Xml;
using PFProSDK.Common;
using PFProSDK.Common.Exceptions;
using PFProSDK.Common.Utility;

#endregion

namespace PFProSDK.DataObjects
{
    /// <summary>
    ///     Container class for response messages
    ///     specific Fraud Protections Services
    /// </summary>
    /// <remarks>
    ///     This class contains the fraud protection
    ///     services related response messages and data objects parsed
    ///     from the xml data in the fraud response.
    ///     <seealso cref="FpsXmlData" />
    /// </remarks>
    public sealed class FraudResponse : BaseResponseDataObject
    {
        #region "Constructors"

        /// <summary>
        ///     Constructor for Fraud response.
        /// </summary>
        internal FraudResponse()
        {
        }

        #endregion

        #region "Properties"

        /// <summary>
        ///     Gets, Sets  PreFpsMsg
        /// </summary>
        /// <remarks>
        ///     Gets the FPS Pre FPS message.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>PREFPSMSG</code>
        /// </remarks>
        public string PreFpsMsg { get; private set; }

        /// <summary>
        ///     Gets, Sets  PostFpsMsg
        /// </summary>
        /// <remarks>
        ///     Gets the FPS Post FPS message.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>POSTFPSMSG</code>
        /// </remarks>
        public string PostFpsMsg { get; private set; }

        /// <summary>
        ///     Gets, Sets  Fps_PreXmlData
        /// </summary>
        /// <remarks>
        ///     Gets the FPS Pre Xml data message populated in
        ///     FpsXmlData object.
        ///     Its an itemized list of responses for trigerred filters
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>FPS_PREXMLDATA</code>
        ///     <seealso cref="FpsXmlData" />
        /// </remarks>
        public FpsXmlData FpsPreXmlData { get; private set; }

        /// <summary>
        ///     Gets, Sets  Fps_PostXmlData
        /// </summary>
        /// <remarks>
        ///     Gets the FPS Post Xml data message populated in
        ///     FpsXmlData object.
        ///     <para>Maps to Payflow Parameter:</para>
        ///     <code>FPS_POSTXMLDATA</code>
        ///     <seealso cref="FpsXmlData" />
        /// </remarks>
        public FpsXmlData FpsPostXmlData { get; private set; }

        #endregion

        #region "Functions"

        /// <summary>
        ///     Sets the Response params in
        ///     response data objects.
        /// </summary>
        /// <param name="responseHashTable">Response Hash table by ref</param>
        internal void SetParams(ref Hashtable responseHashTable)
        {
            try
            {
                PreFpsMsg = (string) responseHashTable[PayflowConstants.ParamPrefpsmsg];
                PostFpsMsg = (string) responseHashTable[PayflowConstants.ParamPostfpsmsg];


                responseHashTable.Remove(PayflowConstants.ParamPrefpsmsg);
                responseHashTable.Remove(PayflowConstants.ParamPostfpsmsg);

                SetFpsXmlData(ref responseHashTable);
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

        /// <summary>
        ///     Sets the Fps Xml data
        /// </summary>
        /// <param name="responseHashTable">Response Hash table by ref</param>
        private void SetFpsXmlData(ref Hashtable responseHashTable)
        {
            string xmlData = null;
            try
            {
                xmlData = (string) responseHashTable[PayflowConstants.ParamFpsPrexmldata];
                FpsPreXmlData = SetRules(xmlData);
                xmlData = (string) responseHashTable[PayflowConstants.ParamFpsPostxmldata];
                FpsPostXmlData = SetRules(xmlData);
                responseHashTable.Remove(PayflowConstants.ParamFpsPrexmldata);
                responseHashTable.Remove(PayflowConstants.ParamFpsPostxmldata);
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

        /// <summary>
        ///     Sets the FPS rules applied.
        /// </summary>
        /// <param name="xmlData">Xml String</param>
        /// <returns>FPS Xml Data object</returns>
        private FpsXmlData SetRules(string xmlData)
        {
            try
            {
                var fpsData = new FpsXmlData();
                if (xmlData != null && xmlData.Length > 0)
                {
                    ArrayList ruleList = null;

                    ruleList = ParseXmlData(xmlData);
                    if (ruleList != null && ruleList.Count > 0) fpsData.SetRuleList(ruleList);
                }

                return fpsData;
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

        /// <summary>
        ///     Parses FPS Xml String Data
        /// </summary>
        /// <param name="xmlData">Xml String Data</param>
        /// <returns>Rulelist</returns>
        private ArrayList ParseXmlData(string xmlData)
        {
            try
            {
                var ruleList = new ArrayList();
                var fpsXmlRules = new XmlDocument();
                fpsXmlRules.LoadXml(xmlData);
                var ruleNodeList = fpsXmlRules.GetElementsByTagName(PayflowConstants.XmlParamRule);
                if (ruleNodeList != null && ruleNodeList.Count > 0)
                    foreach (XmlNode ruleNode in ruleNodeList)
                    {
                        string tempValue;
                        var fraudRule = new Rule();
                        tempValue = ruleNode.Attributes.GetNamedItem(PayflowConstants.XmlParamNum).Value;
                        if (tempValue != null) fraudRule.Num = int.Parse(tempValue);
                        fraudRule.RuleId = ruleNode.SelectSingleNode(PayflowConstants.XmlParamRuleid).InnerText;
                        fraudRule.RuleAlias = ruleNode.SelectSingleNode(PayflowConstants.XmlParamRulealias).InnerText;
                        fraudRule.RuleDescription = ruleNode.SelectSingleNode(PayflowConstants.XmlParamRuledescription)
                            .InnerText;
                        fraudRule.Action = ruleNode.SelectSingleNode(PayflowConstants.XmlParamAction).InnerText;
                        fraudRule.TriggeredMessage =
                            ruleNode.SelectSingleNode(PayflowConstants.XmlParamTriggeredmessage).InnerText;
                        var ruleVendorParamNode = ruleNode.SelectSingleNode(PayflowConstants.XmlParamRulevendorparms);
                        if (ruleVendorParamNode != null)
                        {
                            var ruleParamList = ruleVendorParamNode.SelectNodes(PayflowConstants.XmlParamRuleparameter);
                            if (ruleParamList != null)
                                foreach (XmlNode ruleParamNode in ruleParamList)
                                {
                                    var ruleParam = new RuleParameter();
                                    string tempValue1;
                                    tempValue1 = ruleParamNode.Attributes.GetNamedItem(PayflowConstants.XmlParamNum)
                                        .Value;
                                    if (tempValue1 != null)
                                        ruleParam.Num = int.Parse(tempValue);
                                    ruleParam.Name = ruleParamNode.SelectSingleNode(PayflowConstants.XmlParamName)
                                        .InnerText;
                                    ruleParam.Type = ruleParamNode.SelectSingleNode(PayflowConstants.XmlParamValue)
                                        .Attributes.GetNamedItem(PayflowConstants.XmlParamType).Value;
                                    ruleParam.Value = ruleParamNode.SelectSingleNode(PayflowConstants.XmlParamValue)
                                        .InnerText;
                                    fraudRule.RuleVendorParms.Add(ruleParam);
                                }
                        }

                        ruleList.Add(fraudRule);
                    }

                return ruleList;
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var error = new ErrorObject("Error While parsing XmlData", ex.Message);
                var dEx = new DataObjectException(error);
                throw dEx;
            }

            //catch
            //{
            //    ErrorObject Error = new PayPal.Payments.Common.ErrorObject("Error While parsing XmlData", "");
            //    DataObjectException DEx = new DataObjectException(Error);
            //    throw DEx;
            //}
        }

        #endregion
    }
}