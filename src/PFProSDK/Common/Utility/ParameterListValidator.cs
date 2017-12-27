#region "Copyright"

//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

#endregion

#region "Imports"

using System;
using System.Collections;
using System.Xml;
using PFProSDK.Common.Logging;

#endregion

namespace PFProSDK.Common.Utility
{
    /// <summary>
    ///     Parameter List Validator Class.
    /// </summary>
    internal sealed class ParameterListValidator
    {
        #region "Private Constructor"

        /// <summary>
        ///     private Constructor.
        /// </summary>
        private ParameterListValidator()
        {
        }

        #endregion

        #region "Validator functions"

        /// <summary>
        ///     Validates the parameter list.
        /// </summary>
        /// <param name="paramList">Parameter List</param>
        /// <param name="isXmlPayReq">true if Request is XmlPay, false otherwise.</param>
        /// <param name="currentContext">Context object by reference.</param>
        public static void Validate(string paramList, bool isXmlPayReq, ref Context currentContext)
        {
            Logger.Instance.Log(
                "PayPal.Payments.Common.Utility.ParameterListValidator.Validate(String,bool,String,String,ref Context): Entered",
                PayflowConstants.SeverityDebug);
            try
            {
                if (isXmlPayReq)
                {
                    var xmlPayReq = new XmlDocument();
                    xmlPayReq.LoadXml(paramList);
                }
                else
                {
                    if (paramList != null && paramList.Length > 0) ParseNvpList(paramList, ref currentContext, false);
                }
            }
            catch (Exception ex)
            {
                var err = PayflowUtility.PopulateCommError(PayflowConstants.EInvalidNvp, ex,
                    PayflowConstants.SeverityFatal, isXmlPayReq, null);
                if (!currentContext.IsCommunicationErrorContained(err)) currentContext.AddError(err);
            }
            //catch 
            //{
            //    ErrorObject Err = PayflowUtility.PopulateCommError(PayflowConstants.E_INVALID_NVP, null, PayflowConstants.SEVERITY_FATAL, IsXmlPayReq, null);
            //    if (!CurrentContext.IsCommunicationErrorContained(Err))
            //    {
            //        CurrentContext.AddError(Err);
            //    }

            //}
            finally
            {
                Logger.Instance.Log(
                    "PayPal.Payments.Common.Utility.ParameterListValidator.Validate(String,bool,String,String,ref Context): Exiting",
                    PayflowConstants.SeverityDebug);
            }
        }

        /// <summary>
        ///     Validates Name Value Pair Request.
        /// </summary>
        /// <param name="paramList">Name Value Param List.</param>
        /// <param name="currentContext">Context object by reference.</param>
        /// <param name="populateResponseHashTable">
        ///     True will populate the return hashtable, false will just parse the request and
        ///     check for validity
        /// </param>
        /// <returns>Name value hash table</returns>
        public static Hashtable ParseNvpList(string paramList, ref Context currentContext,
            bool populateResponseHashTable)
        {
            Logger.Instance.Log(
                "PayPal.Payments.Common.Utility.ParameterListValidator.ParseNVPList(String,String,String,ref context,bool): Entered",
                PayflowConstants.SeverityDebug);
            long paramListLen = paramList.Length;
            var index = 0;
            var openBracket = false;
            var closeBracket = false;
            string addlMessage;
            ErrorObject err;
            var paramListHashTable = new Hashtable();
            if (paramList == null || paramList.Length <= 0)
            {
                err = PayflowUtility.PopulateCommError(PayflowConstants.EEmptyParamList, null,
                    PayflowConstants.SeverityFatal, false, null);
                currentContext.AddError(err);
            }

            while (index < paramListLen && currentContext.HighestErrorLvl < PayflowConstants.SeverityFatal)
            {
                long nameBuffSize = 1000;
                long valBuffSize = 1000;
                long lenBuffSize = 1000;
                var nameBuffer = new char[nameBuffSize];
                var lenValueBuffer = new char[lenBuffSize];
                var valueBuffer = new char[valBuffSize];
                char[] tempArray = null;
                var lenIndex = 0;
                var nameIndex = 0;

                while (index < paramListLen && paramList[index] != '\0' && paramList[index] != '=')
                {
                    if (paramList[index] == '[')
                    {
                        if (openBracket)
                        {
                            addlMessage = "Found unmatched '[' followed by another '[' at index  " + (index + 1);
                            err = PayflowUtility.PopulateCommError(PayflowConstants.EParmNameLen, null,
                                PayflowConstants.SeverityFatal, false, addlMessage);
                            if (!currentContext.IsCommunicationErrorContained(err)) currentContext.AddError(err);
                            break;
                        }

                        openBracket = true;
                        index++;
                        continue;
                    }

                    if (paramList[index] == ']')
                    {
                        if (!openBracket)
                        {
                            addlMessage = "Unmatched ']' at index " + (index + 1);
                            err = PayflowUtility.PopulateCommError(PayflowConstants.EParmNameLen, null,
                                PayflowConstants.SeverityFatal, false, addlMessage);
                            if (!currentContext.IsCommunicationErrorContained(err)) currentContext.AddError(err);
                            break;
                        }

                        if (index + 1 < paramListLen && paramList[index + 1] != '=')
                        {
                            addlMessage = "']' is not followed by '=' in param list at index " + (index + 1);
                            err = PayflowUtility.PopulateCommError(PayflowConstants.EParmNameLen, null,
                                PayflowConstants.SeverityFatal, false, addlMessage);
                            if (!currentContext.IsCommunicationErrorContained(err)) currentContext.AddError(err);
                            break;
                        }

                        if (index + 1 < paramListLen && paramList[index - 1] == '[')
                        {
                            addlMessage = "Length of value not found in '[]' at index " + (index + 1);
                            err = PayflowUtility.PopulateCommError(PayflowConstants.EParmNameLen, null,
                                PayflowConstants.SeverityFatal, false, addlMessage);
                            if (!currentContext.IsCommunicationErrorContained(err)) currentContext.AddError(err);
                            break;
                        }

                        if (closeBracket)
                        {
                            addlMessage = "Found unmatched ']' followed by another ']' at index  " + (index + 1);
                            err = PayflowUtility.PopulateCommError(PayflowConstants.EParmNameLen, null,
                                PayflowConstants.SeverityFatal, false, addlMessage);
                            if (!currentContext.IsCommunicationErrorContained(err)) currentContext.AddError(err);
                            break;
                        }

                        index++;
                        closeBracket = true;
                        continue;
                    }

                    if (openBracket && !closeBracket)
                    {
                        //increase the size of LenValueBuffer if required
                        if (lenIndex >= lenBuffSize)
                        {
                            lenBuffSize += 2000;
                            tempArray = new char[lenBuffSize];
                            Array.Copy(lenValueBuffer, tempArray, lenValueBuffer.LongLength);
                            lenValueBuffer = tempArray;
                        }

                        lenValueBuffer[lenIndex] = paramList[index];
                        lenIndex++;
                        index++;
                    }
                    else
                    {
                        //increase the size of NameBuffer if required
                        if (nameIndex >= nameBuffSize)
                        {
                            nameBuffSize += 2000;
                            tempArray = new char[nameBuffSize];
                            Array.Copy(nameBuffer, tempArray, nameBuffer.LongLength);
                            nameBuffer = tempArray;
                        }

                        nameBuffer[nameIndex] = paramList[index];
                        if (nameBuffer[nameIndex] == '&')
                        {
                            addlMessage = new string(nameBuffer);
                            addlMessage = addlMessage.Trim('\0');
                            err = PayflowUtility.PopulateCommError(PayflowConstants.EParmName, null,
                                PayflowConstants.SeverityFatal, false, addlMessage);
                            if (!currentContext.IsCommunicationErrorContained(err)) currentContext.AddError(err);
                        }

                        index++;
                        nameIndex++;
                    }
                }

                //skip '='
                if (index < paramListLen && paramList[index] != '\0') index++;

                if (openBracket && !closeBracket)
                {
                    addlMessage = "Unmatched '[' at index " + (index + 1);
                    err = PayflowUtility.PopulateCommError(PayflowConstants.EParmNameLen, null,
                        PayflowConstants.SeverityFatal, false, addlMessage);
                    if (!currentContext.IsCommunicationErrorContained(err)) currentContext.AddError(err);
                    break;
                }

                /*if(OpenBracket && CloseBracket && LenValueBuffer != null && LenValueBuffer.Length > 0 && LenValueBuffer[0] == '0')
                {
                    String Len = new string(LenValueBuffer).Trim('\0');
                    AddlMessage = "Invalid param length = " + Len;
                    Err =  PayflowUtility.PopulateCommError(PayflowConstants.E_PARM_NAME_LEN,null,PayflowConstants.SEVERITY_FATAL,false,AddlMessage);
                    if(!CurrentContext.IsCommunicationErrorContained(Err))
                    {
                        CurrentContext.AddError(Err);
                    }
                    break;
                    
                }*/

                if (openBracket && closeBracket && lenValueBuffer != null && lenValueBuffer.Length > 0 &&
                    lenValueBuffer[0] == '-')
                {
                    var len = new string(lenValueBuffer).Trim('\0');
                    addlMessage = "Invalid param length = " + len;
                    err = PayflowUtility.PopulateCommError(PayflowConstants.EParmNameLen, null,
                        PayflowConstants.SeverityFatal, false, addlMessage);
                    if (!currentContext.IsCommunicationErrorContained(err)) currentContext.AddError(err);
                    break;
                }

                var valIndex = 0;
                while (index < paramListLen && paramList[index] != '\0' &&
                       currentContext.HighestErrorLvl < PayflowConstants.SeverityFatal)
                {
                    if (lenValueBuffer != null && lenValueBuffer.Length > 0 && lenValueBuffer[0] != '\0')
                    {
                        var lenString = new string(lenValueBuffer);
                        lenString = lenString.Trim();
                        int len;
                        try
                        {
                            len = int.Parse(lenString);
                        }
                        catch (Exception ex)
                        {
                            var name = new string(nameBuffer).Trim('\0');
                            addlMessage = "Value in [] is not numeric data, data in '[]' =  " + lenString.Trim('\0') +
                                          "for Name = " + name;
                            err = PayflowUtility.PopulateCommError(PayflowConstants.EParmNameLen, ex,
                                PayflowConstants.SeverityFatal, false, addlMessage);
                            if (!currentContext.IsCommunicationErrorContained(err)) currentContext.AddError(err);
                            break;
                        }
                        //catch 
                        //{
                        //    String Name = new string(NameBuffer).Trim('\0');
                        //    AddlMessage = "Value in [] is not numeric data, data in '[]' =  " + LenString.Trim('\0') + "for Name = " + Name;
                        //    Err = PayflowUtility.PopulateCommError(PayflowConstants.E_PARM_NAME_LEN, null, PayflowConstants.SEVERITY_FATAL, false, AddlMessage);
                        //    if (!CurrentContext.IsCommunicationErrorContained(Err))
                        //    {
                        //        CurrentContext.AddError(Err);
                        //    }
                        //    break;
                        //}

                        var ampIndex = index + len;
                        if (ampIndex < paramListLen && paramList[ampIndex] != '&')
                        {
                            var name = new string(nameBuffer).Trim('\0');
                            addlMessage = "Param length in '[]' does not match actual value length.Param Name = " +
                                          name;
                            err = PayflowUtility.PopulateCommError(PayflowConstants.EParmNameLen, null,
                                PayflowConstants.SeverityFatal, false, addlMessage);
                            if (!currentContext.IsCommunicationErrorContained(err)) currentContext.AddError(err);
                            index += len + 1;
                            break;
                        }

                        //increase the size of ValueBuffer if required
                        if (len >= valBuffSize)
                        {
                            // PPSCR00576399 - fixed out of bounds array issue. 11/09/2007 tsieber
                            valBuffSize += len + 2000;
                            valueBuffer = new char[valBuffSize];
                        }

                        int valueIndex;
                        for (valueIndex = 0; valueIndex < len && index + valueIndex < paramListLen; valueIndex++)
                            valueBuffer[valueIndex] = paramList[index + valueIndex];
                        index += len + 1;
                        break;
                    }

                    //increase the size of NameBuffer if required
                    if (valIndex >= valBuffSize)
                    {
                        valBuffSize += 2000;
                        tempArray = new char[valBuffSize];
                        Array.Copy(valueBuffer, tempArray, valueBuffer.LongLength);
                        valueBuffer = tempArray;
                    }

                    if (paramList[index] == '&')
                        if (index + 1 < paramListLen && paramList[index + 1] == '&')
                        {
                            valueBuffer[valIndex] = paramList[index];
                        }
                        else if (paramList[index - 1] == '&')
                        {
                            valueBuffer[valIndex] = paramList[index];
                        }
                        else
                        {
                            index++;
                            valIndex++;
                            break;
                        }
                    else
                        valueBuffer[valIndex] = paramList[index];

                    index++;
                    valIndex++;
                }

                //put data in hash table as name - value
                if (populateResponseHashTable)
                {
                    var name = new string(nameBuffer).Trim('\0');
                    var value = new string(valueBuffer).Trim('\0');
                    if (paramListHashTable.Contains(name))
                        name = name + PayflowConstants.TagDuplicate + PayflowUtility.RequestId;

                    paramListHashTable.Add(name, value);
                }

                openBracket = false;
                closeBracket = false;
                nameBuffer = null;
                lenValueBuffer = null;
                valueBuffer = null;
                tempArray = null;
            }

            Logger.Instance.Log(
                "PayPal.Payments.Common.Utility.ParameterListValidator.ParseNVPList(String,String,String,ref context,bool): Exiting",
                PayflowConstants.SeverityDebug);
            return paramListHashTable;
        }

//		internal static bool ValidateNVPHighLevel(String ParamList, out String AddlMessage)
//		{
//			bool Invalid = false;
//			AddlMessage = PayflowConstants.EMPTY_STRING;
//			Logger.Instance.Log("PayPal.Payments.Common.Utility.ParameterListValidator.ValidateNVPHighLevel(String,out String): Entered", PayflowConstants.SEVERITY_DEBUG);
//			if (ParamList.StartsWith(PayflowConstants.DELIMITER_NVP))
//			{
//				Invalid = true;
//				AddlMessage = "Param list begins with '&'";
//			}
//			else if (ParamList.IndexOf(PayflowConstants.SEPARATOR_NVP) < 0)
//			{
//				Invalid = true;
//				AddlMessage = "No separator '=' found in param list";
//			}
//			else if (ParamList.StartsWith(PayflowConstants.SEPARATOR_NVP) || ParamList.EndsWith(PayflowConstants.SEPARATOR_NVP))
//			{
//				Invalid = true;
//				AddlMessage = "Param list begins or ends with '='";
//			}
//			else if (ParamList.StartsWith(PayflowConstants.OPENING_BRACE_NVP) || ParamList.EndsWith(PayflowConstants.OPENING_BRACE_NVP))
//			{
//				Invalid = true;
//				AddlMessage = "Param list begins or ends with '['";
//			}
//			else if (ParamList.StartsWith(PayflowConstants.CLOSING_BRACE_NVP) || ParamList.EndsWith(PayflowConstants.CLOSING_BRACE_NVP))
//			{
//				Invalid = true;
//				AddlMessage = "Param list begins or ends with ']'";
//			}
//			else
//			{
//				int Index = ParamList.LastIndexOf(PayflowConstants.DELIMITER_NVP);
//				if (Index >= 0 && Index < ParamList.Length)
//				{
//					if(ParamList[Index-1] != '&' && ParamList.IndexOf(PayflowConstants.SEPARATOR_NVP, Index) < 0)
//					{
//						Invalid = true;
//						AddlMessage = "Parameter list format error: unmatched name";
//					}
//				}
//			}
//			Logger.Instance.Log("PayPal.Payments.Common.Utility.ParameterListValidator.ValidateNVPHighLevel(String,out String): Exiting", PayflowConstants.SEVERITY_DEBUG);
//			return Invalid;
//		}

        #endregion
    }
}