using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using CommonLib.DlkHandlers;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using System.Data;
using CommonLib.DlkUtility;

namespace CommonLib.DlkRecords
{
    public class DlkData
    {
        #region CONSTANTS
        private const string NODE_DATAREC = "datarecord";
        private const string ATTRIB_NAME = "name";
        private const string NODE_DATAVALUE = "datavalue";
        private const string NODE_DATA = "data";
        private const string DEFAULT_PATH = "";
        #endregion

        public string Path { get; set; }
        public List<DlkDataRecord> Records { get; set; }

        public DlkData(string FilePath)
        {
            /* Initilize members */
            Path = FilePath;
            Records = new List<DlkDataRecord>();

            /* Populate Records if file do exist, if not, leave empty */
            if (File.Exists(FilePath))
            {
                try
                {
                    XDocument dataDoc = XDocument.Load(FilePath);
                    var dataroot = from dataRec in dataDoc.Descendants(NODE_DATAREC)
                                   select new
                                   {
                                       name = dataRec.Attribute(ATTRIB_NAME).Value,
                                       values = dataRec.Elements(NODE_DATAVALUE)
                                   };

                    foreach (var data in dataroot)
                    {
                        DlkDataRecord newrec = new DlkDataRecord();

                        newrec.Name = data.name;
                        newrec.Values = new List<string>();
                        foreach (XElement elm in data.values)
                        {
                            if (DlkTestRunnerSettingsHandler.ApplicationUnderTest == null || DlkTestRunnerSettingsHandler.ApplicationUnderTest.Type.ToLower().Equals("internal"))
                            {
                                newrec.Values.Add(DlkVariable.TransformOutputVarToNewFormat(elm.Value.ToString()));
                            }
                            else
                            {
                                newrec.Values.Add(elm.Value.ToString());
                            }
                            //#if DEBUG
                            //                        newrec.Values.Add(DlkVariable.TransformOutputVarToNewFormat(elm.Value.ToString()));
                            //#else
                            //                        newrec.Values.Add(elm.Value.ToString());
                            //#endif
                        }
                        Records.Add(newrec);
                    }
                }
                catch
                {
                    // Do nothing. If error is encountered, ignore and move on, same behavior as if data file does not exist.
                }
            }
        }

        public void Save(string NewPath=DEFAULT_PATH)
        {
            if (!string.IsNullOrEmpty(NewPath))
            {
                Path = NewPath;
            }

            if (File.Exists(Path))
            {
                FileInfo fi = new FileInfo(Path);
                fi.IsReadOnly = false;
                File.Delete(Path);
            }

            List<XElement> datarecs = new List<XElement>();
            foreach (DlkDataRecord rec in Records)
            {
                List<XElement> datavalues = new List<XElement>();
                foreach (string val in rec.Values)
                {
                    XElement xVal = new XElement(NODE_DATAVALUE, val);
                    datavalues.Add(xVal);
                };

                XElement elm = new XElement(NODE_DATAREC, new XAttribute(ATTRIB_NAME, rec.Name), datavalues);
                datarecs.Add(elm);
            }

            XElement root = new XElement(NODE_DATA, datarecs);
            XDocument doc = new XDocument(root);
            doc.Save(Path);
        }

        /// <summary>
        /// Clears all data values
        /// </summary>
        public void ClearAllRecordValues()
        {
            if (Records != null)
            {
                foreach (DlkDataRecord record in Records)
                {
                    record.Values.Clear();
                }
            }
        }

        /// <summary>
        /// Moves data value index either up or down
        /// </summary>
        /// <param name="rowIndex">Data value index</param>
        /// <param name="up">True=Move up;False=Move down</param>
        public void MoveRow(int rowIndex, bool up)
        {
            if (Records != null)
            {
                int targetRowIndex = up ? rowIndex - 1 : rowIndex + 1;
                foreach (var record in Records)
                {
                    if (record.Values != null)
                    {
                        string targetRow = record.Values[targetRowIndex];
                        string currentRow = record.Values[rowIndex];

                        record.Values[rowIndex] = targetRow;
                        record.Values[targetRowIndex] = currentRow;
                    }
                }
            }
        }

        /// <summary>
        /// Moves column either left or right
        /// </summary>
        /// <param name="colIndex">Data column index</param>
        /// <param name="left">True=Move left;False=Move Right</param>
        public void MoveColumn(int colIndex, bool left)
        {           
            if (Records != null)
            {
                int targetColIndex = left ? colIndex - 1 : colIndex + 1;
                var currentCol = Records[colIndex];
                var targetCol = Records[targetColIndex];

                Records[targetColIndex] = currentCol;
                Records[colIndex] = targetCol;
            }
        }

        /// <summary>
        /// Creates new row for data values
        /// </summary>
        public void NewRow()
        {
            if(Records != null)
            {
                foreach (DlkDataRecord rec in Records)
                {
                    rec.Values.Add(string.Empty);
                }
            }
        }

        /// <summary>
        /// Deletes data column with the specified name
        /// </summary>
        /// <param name="name"></param>
        public void DeleteColumn(string name)
        {
            if (Records != null)
            {
                var targetColumn = Records.Single(rec => rec.Name == name);
                Records.Remove(targetColumn);
            }
        }

        /// <summary>
        /// Flags data values for deletion
        /// </summary>
        /// <param name="row">Data values row index</param>
        public void FlagRowDelete(int row)
        {
            if (Records != null)
            {
                foreach (var record in Records)
                {
                    record.Values[row] = null;
                }
            }
        }

        /// <summary>
        /// Deletes data values that have been flag for deletion
        /// </summary>
        public void DeleteRowValues()
        {
            if (Records != null)
            {
                foreach (var record in Records)
                {
                    if (record.Values != null)
                    {
                        record.Values.RemoveAll(val => val == null);
                    }
                }
            }
        }

        public static void SubstituteDataVariables(DlkTest Test)
        {
            try
            {
                DlkLogger.LogInfo("Starting substitution of data variables...");
                bool needToSubstitute = TestUsesDataVariable(Test);
                if (!File.Exists(Test.Data.Path) && needToSubstitute)
                {
                    throw new Exception("Data file not located, data variable substitution failed.");
                }
                if (needToSubstitute)
                {
                    int targetParameterIndex = Test.mTestInstanceExecuted - 1;
                    if (targetParameterIndex < 0)
                    {
                        throw new Exception("Test instance for execution cannot be less than 0.");
                    }
                    foreach (DlkTestStepRecord step in Test.mTestSteps)
                    {
                        if (targetParameterIndex >= step.mParameters.Count)
                        {
                            throw new Exception("Test instance for execution exceeds total test inbstances of the test.");
                        }
                        step.mParameters[targetParameterIndex] = GetValue(step.mParameters[targetParameterIndex], Test, true);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Replaces the values of mExecute Property of DlkTest
        /// in case the value supplied for the property is a Data-Driven record
        /// </summary>
        /// <param name="Test"></param>
        public static void SubstituteExecuteDataVariables(DlkTest Test)
        {
            try
            {
                DlkLogger.LogInfo("Starting substitution of data variables to execute field...");
                foreach (DlkTestStepRecord step in Test.mTestSteps)
                {
                    if (step.mExecute.Contains("D{"))
                    {
                        step.mExecute = GetValue(step.mExecute, Test);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private static bool TestUsesDataVariable(DlkTest Test)
        {
            foreach (DlkTestStepRecord step in Test.mTestSteps)
            {
                foreach (string param in step.mParameters)
                {
                    if (param.Contains("D{"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks whether input is a data-driven parameter
        /// </summary>
        /// <param name="InputParam">Input param</param>
        /// <returns>TRUE if input is data-driven, FALSE otherwise</returns>
        public static bool IsDataDrivenParam(string InputParam)
        {
            return InputParam.StartsWith("D{") && InputParam.EndsWith("}");
        }

        /// <summary>
        /// Checks whether input is using a global variable parameter
        /// </summary>
        /// <param name="InputParam">Input param</param>
        /// <returns>TRUE if input is using a global variable, FALSE otherwise</returns>
        public static bool IsGlobalVarParam(string InputParam)
        {
            return InputParam.StartsWith("G{") && InputParam.EndsWith("}");
        }

        /// <summary>
        /// Checks whether input is a using an output variable parameter
        /// </summary>
        /// <param name="InputParam">Input param</param>
        /// <returns>TRUE if input is an output variable, FALSE otherwise</returns>
        public static bool IsOutPutVariableParam(string InputParam)
        {
            return InputParam.StartsWith("O{") && InputParam.EndsWith("}");
        }

        /// <summary>
        /// Gets name of data-driven parameter
        /// </summary>
        /// <param name="InputParam">Input param</param>
        /// <returns>name of data-driven parameter</returns>
        public static string GetDataParamName(string InputParam)
        {
            return InputParam.Replace("D{", "").Replace("}", "");
        }

        public static string GetValue(string OldValue, DlkTest Test, bool isParam = false)
        {
            try
            {
                string delimiter = isParam ? DlkTestStepRecord.globalParamDelimiter : "|";
                string ret = "";
                foreach (string param in OldValue.Split(new[] { delimiter }, StringSplitOptions.None))
                {
                    string newValue = param;
                    if (param.Contains("D{"))
                    {
                        foreach (DlkDataRecord rec in Test.Data.Records)
                        {
                            if (rec.Name == param.Replace("D{", "").Replace("}", ""))
                            {
                                if (Test.mTestInstanceExecuted <= Test.mInstanceCount) //To handle missing test instances
                                {
                                    newValue = rec.Values[Test.mTestInstanceExecuted - 1];
                                }
                                else
                                {
                                    newValue = string.Empty; //value will be empty when instance is missing
                                }
                                break;
                            }
                        }
                    }

                    ret += newValue + delimiter;
                }
                ret = ret.Substring(0, ret.Length - delimiter.Length);
                return ret;
            }
            catch
            {
                throw;
            }
        }
    }
}