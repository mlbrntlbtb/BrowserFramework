using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System.Reflection;
using System.IO;
using GovWinLib.DlkUtility;

namespace GovWinLib.DlkFunctions
{
    [Component("Utility")]
    public static class DlkUtility
    {
        /************
        [Keyword("CaptureScreenShot")]
        public void CaptureScreenShot()
        {
            DlkLogger.LogScreenCapture(false.ToString());
        }

        [Keyword("Wait", new String[] { @"1|text|Wait Time in Secs|60" })]
        public void Wait(String sWaitTime)
        {
            Thread.Sleep(Convert.ToInt32(sWaitTime) * 1000);
        }

        [Keyword("ExecuteScript", new String[] {@"1|text|Script Path|ExternalScripts\PerformLogin.xlsx", 
                                                "2|text|Parameters|Parm1~Parm2~Parm3"})]
        public void ExecuteScript(String sScriptPath, String sScriptParameters)
        {
            String sStepColumn = "Step";
            String sExecuteColumn = "ExecuteStep";
            String sModuleColumn = "Module";
            String sControlColumn = "Control";
            String sKeywordColumn = "Keyword";
            String sParameterColumn = "Parameters";
            String sOutputColumn = "Output";
            String sExecuteStep = "";
            String sParameters = "";

            String sScriptSheet = "";
            String sDataSheet = "";

            DlkLogger.LogInfo("ExecuteExternalScript(): Executing script '" + sScriptPath + "'. Parameters ='" + sScriptParameters + "'");

            //Import Script and Tests sheet from external script
            sScriptSheet = Path.GetFileNameWithoutExtension(DlkEnvironment.mDirTestSuite + sScriptPath) + "_Script";
            sDataSheet = Path.GetFileNameWithoutExtension(DlkEnvironment.mDirTestSuite + sScriptPath) + "_Tests";
            DlkExcelApi.ImportSheet(DlkEnvironment.mDirTestSuite + sScriptPath, "Script", sScriptSheet);
            DlkExcelApi.ImportSheet(DlkEnvironment.mDirTestSuite + sScriptPath, "Tests", sDataSheet);

            //Update the data column in the imported Tests sheet
            String[] arrParameters = sScriptParameters.Split('~');
            for (int i = 0; i < arrParameters.Count(); i++)
            {
                DlkExcelApi.SetCellData(sDataSheet, 2, i + 4, arrParameters[i]);
            }

            //Iterate within the Data Sheet
            //DlkLogger.LogInfo(Convert.ToString(DlkExcelApi.GetRowCount(sDataSheet)));

            for (int iDataRow = 1; iDataRow < DlkExcelApi.GetRowCount(sDataSheet); iDataRow++)
            {
                DlkExcelApi.SetCurrentRow(sDataSheet, iDataRow + 1);

                DlkLogger.LogData(sDataSheet);
                //Iterate within the Sub Script Sheet
                int iStep = 1;
                DlkExcelApi.SetCurrentRow(sScriptSheet, iStep + 1);
                while (DlkExcelApi.GetCellData(sScriptSheet, sModuleColumn) != "")
                {                    
                    sExecuteStep = TranslateParameters(sDataSheet, DlkExcelApi.GetCellData(sScriptSheet, sExecuteColumn));
                    sParameters = TranslateParameters(sDataSheet, DlkExcelApi.GetCellData(sScriptSheet, sParameterColumn));
                    DlkLogger.lo(iStep,
                                    "Step: " + DlkExcelApi.GetCellData(sScriptSheet, sStepColumn) +
                                    "; Execute: " + sExecuteStep +
                                    "; Module: " + DlkExcelApi.GetCellData(sScriptSheet, sModuleColumn) +
                                    "; Control: " + DlkExcelApi.GetCellData(sScriptSheet, sControlColumn) +
                                    "; Keyword: " + DlkExcelApi.GetCellData(sScriptSheet, sKeywordColumn) +
                                    "; Parameters: " + sParameters +
                                    "; Output: " + DlkExcelApi.GetCellData(sScriptSheet, sOutputColumn));
                    DlkEnvironment.CurrentStepOutput = DlkExcelApi.GetCellData(sScriptSheet, sOutputColumn);

                    if (sExecuteStep.ToLower() == "true" && sParameters.ToLower() != "<skip>")
                    {
                        Type ObjectStoreType = null;
                        Object ObjectStoreInstance = null;
                        Object[] args = new Object[] { };

                        String sCompObjectStore = DlkDataDictionaryHandler.GetObjectStore(DlkExcelApi.GetCellData(sScriptSheet, sModuleColumn));
                        ObjectStoreInstance = DlkObjectStoresCache.GetObjectStoreInstance(sCompObjectStore);
                        if (ObjectStoreInstance != null)
                        {
                            try
                            {
                                ObjectStoreType = Type.GetType(sCompObjectStore);
                                MethodInfo executeKeywordLogic = ObjectStoreType.GetMethod("ExecuteKeywordLogic");
                                args = new Object[] {DlkExcelApi.GetCellData(sScriptSheet,sControlColumn),
                                    DlkExcelApi.GetCellData(sScriptSheet, sKeywordColumn),
                                    sParameters.Split('|')};
                                executeKeywordLogic.Invoke(ObjectStoreInstance, args);

                            }
                            catch (Exception e)
                            {
                                DlkLogger.LogException("ExecuteTest() : " + e.Message + e.StackTrace + "\n" +
                                                        "Inner Exectipion: " + e.InnerException.Message + e.InnerException.StackTrace);

                            }
                        }
                    }
                    else
                    {
                        DlkLogger.LogInfo("Skipping test step.");
                    }

                    iStep++;
                    DlkExcelApi.SetCurrentRow(sScriptSheet, iStep + 1);
                }
                DlkLogger.LogLineSeperator(3);

            }

        }

        private String TranslateParameterNode(String sDataSheet, String ParameterNode)
        {
            String value = ParameterNode;
            int openBracketPtr = ParameterNode.Length - 1;
            if (ParameterNode.Length > 3)
            {
                int startPos = value.Substring(0, openBracketPtr + 1).LastIndexOf('{');

                while (startPos != -1)
                {
                    if (startPos > 0)
                    {
                        int lastPos = value.IndexOf('}');
                        switch (value.Substring(startPos - 1, 2).ToLower())
                        {
                            case "d{":
                            case "o{":
                            case "g{":
                            case "r{":
                                if (lastPos == value.Length - 1)
                                {
                                    value = TranslateVariable(sDataSheet, value.Substring(startPos - 1, lastPos - startPos + 2));
                                }
                                else
                                {
                                    value = value.Substring(0, startPos - 2 + 1) + TranslateVariable(sDataSheet, value.Substring(startPos - 1, lastPos - startPos + 2)) + value.Substring(lastPos + 1);
                                }
                                break;
                        }
                        openBracketPtr = startPos - 1;
                        startPos = value.Substring(0, openBracketPtr + 1).LastIndexOf('{');

                    }
                    else
                    {
                        break;
                    }
                }
            }

            return value;
        }

        private String TranslateVariable(String sDataSheet, String sVariable)
        {
            String value = "";
            switch (sVariable.Substring(0, 2).ToLower())
            {
                case "d{":
                    value = DlkExcelApi.GetCellData(sDataSheet, sVariable.Substring(2, sVariable.Length - 3));
                    break;
                case "o{":
                    if (DlkEnvironment.OutputData.ContainsKey(sVariable.Substring(2, sVariable.Length - 3)))
                    {
                        value = DlkEnvironment.OutputData[sVariable.Substring(2, sVariable.Length - 3)];
                    }
                    break;
                case "g{":
                    value = DlkGlobalHandler.GetGlobalVariable(sVariable.Substring(2, sVariable.Length - 3));
                    break;
                case "r{":
                    value = DlkReferenceHandler.GetReferenceData(sVariable.Substring(2, sVariable.Length - 3));
                    break;
            }

            return value;
        }

        private String TranslateParameters(String sDataSheet, String pstrParameters)
        {
            String strTranslatedParameters = "";

            //Handle escape char for |
            pstrParameters = pstrParameters.Replace("\\|", "[pipe]");

            if (pstrParameters != "")
            {
                String[] arrParameters = pstrParameters.Split('|');
                for (int i = 0; i < arrParameters.Count(); i++)
                {
                    arrParameters[i] = TranslateParameterNode(sDataSheet, arrParameters[i]);

                    if (i > 0)
                    {
                        strTranslatedParameters = strTranslatedParameters + "|";
                    }

                    strTranslatedParameters = strTranslatedParameters + arrParameters[i];
                }
            }

            return strTranslatedParameters;
        }
        ****/
    }
}
