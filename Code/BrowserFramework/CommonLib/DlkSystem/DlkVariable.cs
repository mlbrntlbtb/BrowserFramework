using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using CommonLib.DlkRecords;
using CommonLib.DlkUtility;

namespace CommonLib.DlkSystem
{
    /// <summary>
    /// static Class is used to manage variable name value pairs
    /// </summary>
    public static class DlkVariable
    {
        private static Dictionary<String, String> DictVariables = new Dictionary<string,string>();
        private static int[] maskedIndexes;
        /// <summary>
        /// Used to add or reset a variable name / value pair
        /// </summary>
        /// <param name="VariableName">the name of the var</param>
        /// <param name="VariableValue">the value of the var</param>
        public static void SetVariable(String VariableName, String VariableValue)
        {
            // we add % to the name before and after so that it can be unique when called

            String mVariableName = @"%" + VariableName + @"%";
            
            // old format
            if (DictVariables.ContainsKey(mVariableName))
            {
                DictVariables[mVariableName] = VariableValue;
            }
            else
            {
                DictVariables.Add(mVariableName, VariableValue);
            }


            /* this porttion will be for all tests moving forward TASK 486606 */
            /* remove above code when use of %<VAR>% has been obsolete */
            string mVariableName2 = "O{" + VariableName + "}";

            if (DictVariables.ContainsKey(mVariableName2))
            {
                DictVariables[mVariableName2] = VariableValue;
            }
            else
            {
                DictVariables.Add(mVariableName2, VariableValue);
            }
        }
        public static String GetVariable(String VariableName)
        {
            String mCurrentValue = "";
            try
            {
                mCurrentValue = DictVariables[VariableName];
            }
            catch { }
            return mCurrentValue;
        }
        public static String[] SubstituteVariables(String[] mTestParams)
        {
            List<String> mNewTestParams = new List<String>();
            String mRealParam = "";
            bool bFound = false;
            int i = 0;

            foreach (String mCurrentParam in mTestParams)
            {
                if (mCurrentParam.Contains("G{"))
                {
                    DataTable dt = new DataTable();
                    string glbVarFileName = DlkTestRunnerApi.mGlobalVarFileName == String.Empty ? "GlobalVar" : DlkTestRunnerApi.mGlobalVarFileName;
                    string glbVarDoc = System.IO.Path.Combine(DlkEnvironment.mDirProduct, @"UserTestData\Data\" + glbVarFileName + ".csv");
                    string varName = mCurrentParam.Replace("G{", "").Replace("}", "");

                    if (!File.Exists(glbVarDoc))
                    {
                            throw new Exception("No global variable doc for this product exists.");
                    }

                    try
                    {
                        dt = DlkCSVHelper.CSVParse(glbVarDoc);
                    }
                    catch (Exception e)
                    {
                        DlkLogger.LogInfo("Variable assignment failed:" + e);
                    }

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[0].ToString() == varName)
                        {
                            mRealParam = row[1].ToString();
                            bFound = true;
                        }
                    }

                    if (!bFound) // warn user if no record of the variable is found
                    {
                        mRealParam = "";

                        //Throw exception if product is iAccess only
                        if (DlkEnvironment.mProductFolder.Contains("MaconomyiAccess"))
                            throw new Exception("No record of variable [" + varName + "] found.");
                        else
                            DlkLogger.LogInfo("No record of variable [" + varName + "] found.");
                    }
                    DlkLogger.LogInfo("Variable substitutions made. Was: " + mCurrentParam + ", Now: " + (maskedIndexes == null ? mRealParam : maskedIndexes.Contains(i) ? GetMaskedParameter(mRealParam) : mRealParam));
                }
                else
                {
                    mRealParam = SubVars(mCurrentParam, i);
                }
                i++;
                mNewTestParams.Add(mRealParam);
            }
            return mNewTestParams.ToArray();
        }

        /// <summary>
        /// Masked password text
        /// </summary>
        /// <param name="mRealParam">Real parameter value</param>
        /// <returns>masked parameter text</returns>
        private static string GetMaskedParameter(string mRealParam)
        {
            string maskedText = "";

            foreach (var item in mRealParam.ToCharArray())
            {
                maskedText += DlkPasswordMaskedRecord.PasswordChar;
            }
            return maskedText;
        }

        /// <summary>
        /// Substitute variables with masked indexes
        /// </summary>
        /// <param name="mTestParams">test step parameters</param>
        /// <param name="mMaskedIndexes">masked parameter indexes</param>
        /// <returns>substituted values</returns>
        public static String[] SubstituteVariables(String[] mTestParams, int[] mMaskedIndexes)
        {
            maskedIndexes = mMaskedIndexes;
            return SubstituteVariables(mTestParams);
        }

        //private static String SubVars(String StringContainingVariable)
        //{
        //    String mReturn = StringContainingVariable;
        //    while (mReturn.Contains(@"%"))
        //    {
        //        int iStart = mReturn.IndexOf('%');
        //        String mTmp = mReturn.Substring(iStart + 1);
        //        if (!mTmp.Contains(@"%"))
        //        {
        //            break;
        //        }
        //        int iEnd = mTmp.IndexOf('%');
        //        String mVar =  @"%" + mTmp.Substring(0, iEnd) + @"%";
        //        mReturn = mReturn.Replace(mVar, DlkVariable.GetVariable(mVar));
        //    }
        //    if (mReturn != StringContainingVariable)
        //    {
        //        DlkLogger.LogInfo("Variable substitutions made. Was: " + StringContainingVariable + ", Now: " + mReturn);
        //    }
        //    return mReturn;
        //}

        private static String SubVars(String Variable, int parameterIndex)
        {
            string ret = Variable;
            bool bFound = false;
            foreach (string key in DictVariables.Keys)
            {
                if (Variable.Contains(key)) // check if key is in string
                {
                    ret = ret.Replace(key, GetVariable(key));
                    bFound = true;
                }
            }
            if (bFound)
            {
                DlkLogger.LogInfo("Variable substitutions made. Was: " + Variable + ", Now: " + (maskedIndexes == null ? ret: maskedIndexes.Contains(parameterIndex) ? GetMaskedParameter(ret) : ret));
            }
            return ret;
        }

        //private static bool mIsClosingBracket = false;

        //public static string UT_Harness()
        //{
        //    string msgString = string.Empty;
        //    string[] inputs = {
        //                          "%MyParam1%",
        //                          "%MyParam1%|%MyParam2%|%MyParam3%",
        //                          "%MyParam1%|%MyParam2%|Literal",
        //                          "%MyParam1%|Literal|%MyParam2%",
        //                          "Literal|%MyParam1%|Literal",
        //                          "Literal|Literal|Literal",
        //                          "Literal|%MyParam1%|%MyParam2%",
        //                          "Percent %|%MyParam1%|Literal",
        //                          "% Rate|Literal|Literal",
        //                          "Literal|Literal|Percent %",
        //                          "% Rate|Literal|Percent %",
        //                          "|Literal|Literal",
        //                          "Literal|Literal|",
        //                          "Literal||Literal"
        //                      };

        //    string[] expectedOutputs = {
        //                                 "O{MyParam1}",
        //                                 "O{MyParam1}|O{MyParam2}|O{MyParam3}",
        //                                 "O{MyParam1}|O{MyParam2}|Literal",
        //                                 "O{MyParam1}|Literal|O{MyParam2}",
        //                                 "Literal|O{MyParam1}|Literal",
        //                                 "Literal|Literal|Literal",
        //                                 "Literal|O{MyParam1}|O{MyParam2}",
        //                                 "Percent %|O{MyParam1}|Literal",
        //                                 "% Rate|Literal|Literal",
        //                                 "Literal|Literal|Percent %",
        //                                 "% Rate|Literal|Percent %",
        //                                 "|Literal|Literal",
        //                                 "Literal|Literal|",
        //                                 "Literal||Literal"
        //                             };

        //    //foreach (string ipt in inputs)
        //    for (int idx = 0; idx < inputs.Count(); idx++)
        //    {
        //        System.Diagnostics.Debug.Assert(TransformOutputVarToNewFormat(inputs[idx]) == expectedOutputs[idx]);
        //        msgString += "UT " + (idx + 1).ToString() + " successful\n";
        //    }

        //    return msgString;
        //}

        public static string TransformOutputVarToNewFormat(string Input)
        {
            string[] params2 = Input.Split('|');
            string ret = string.Empty;


            foreach (string prm in params2)
            {
                ret += TransformToNewFormat(prm) + "|";
            }

            ret = ret.Substring(0, ret.Length - 1);

            return ret;
            
            //string ret = TransformToNewFormat(Input);

            //if (mIsClosingBracket)
            //{
            //    ret = DlkString.ReplaceLastOccurrence(ret, "O{", "%");
            //    mIsClosingBracket = false;
            //}
            //return ret;
        }

        private static string TransformToNewFormat(string Input)
        {
            char[] arrInput = Input.ToCharArray();
            string newString = string.Empty;

            if (!Input.StartsWith("%") || !Input.EndsWith("%"))
            {
                return Input;
            }

            string strWithoutThePercentSign = Input.Trim('%');

            newString = "O{" + strWithoutThePercentSign + "}";

            //foreach (char c in arrInput)
            //{
            //    string append = c.ToString();
            //    if (c == '%')
            //    {
            //        if (!mIsClosingBracket)
            //        {
            //            append = "O{";
            //            mIsClosingBracket = true;
            //        }
            //        else
            //        {
            //            append = "}";
            //            mIsClosingBracket = false;
            //        }
            //    }
            //    newString += append;
            //}

            return newString;
        }
    }
}
