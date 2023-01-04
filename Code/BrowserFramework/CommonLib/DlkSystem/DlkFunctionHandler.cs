using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using CommonLib.DlkUtility;
using CommonLib.DlkRecords;
using System.IO;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace CommonLib.DlkSystem
{
    [ControlType("Function")]  
    public static class DlkFunctionHandler
    {
        public static List<string> mTurnedOffSteps = new List<string>();

        public static void ExecuteFunction(String Keyword, String[] Parameters)
        {
            switch(Keyword)
            {
                case "AssignToVariable":
                    AssignToVariable(Parameters[0], Parameters[1]);
                    break;
                case "CaptureScreenshot":
                    CaptureScreenshot(Parameters[0]);
                    break;
                case "ComputeNumberOfDays":
                    ComputeNumberOfDays(Parameters[0], Parameters[1], Parameters[2]);
                    break;
                case "CreateTRD":
                    CreateTRD(Parameters[0], Parameters[1], Parameters[2]);
                    break;
                case "DateFromToday":
                    DateFromToday(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                    break;
                case "DateFromInputDate":
                    DateFromInputDate(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4]);
                    break;
                case "DateFormat":
                    DateFormat(Parameters[0], Parameters[1], Parameters[2]);
                    break;
                case "DateToday":
                    DateToday(Parameters[0]);
                    break;
                case "GetDayFromInputDate":
                    GetDayFromInputDate(Parameters[0], Parameters[1]);
                    break;
                case "StartOfWeek":
                    StartOfWeek(Parameters[0], Parameters[1]);
                    break;
                case "EndOfWeek":
                    EndOfWeek(Parameters[0], Parameters[1]);
                    break;
                case "StartOfMonth":
                    StartOfMonth(Parameters[0], Parameters[1]);
                    break;
                case "EndOfMonth":
                    EndOfMonth(Parameters[0], Parameters[1]);
                    break;
                case "SendKeys":
                    SendKeys(Parameters[0]);
                    break;
                case "TextConcat":
                    TextConcat(Parameters[0], Parameters[1], Parameters[2]);
                    break;
                case "TextEqual":
                    TextEqual(Parameters[0], Parameters[1]);
                    break;
                case "TextReplace":
                    TextReplace(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                    break;
                case "TextSubstring":
                    TextSubstring(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                    break;
                case "TurnOffTestSteps":
                    TurnOffTestSteps(Parameters[0]);
                    break;
                case "PerformMathOperation":
                    PerformMathOperation(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                    break;
                case "LogComment":
                    LogComment(Parameters[0]);
                    break;
                case "SetDelayRetries":
                    SetDelayRetries(Parameters[0], Parameters[1]);
                    break;
                case "CloseBrowser":
                    CloseBrowser();
                    break;
                case "ScrollUp":
                    ScrollUp();
                    break;
                case "ScrollDown":
                    ScrollDown();
                    break;
                case "Wait":
                    Wait(Parameters[0]);
                    break;
                case "MoveFile":
                    MoveFile(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                    break;
                case "DocDiff":
                    DocDiffAPI(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                    break;
                case "LetterCase":
                    LetterCase(Parameters[0],Parameters[1],Parameters[2]);
                    break;
                case "CopyFile":
                    CopyFile(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                    break;
                case "EmptyFolder":
                    EmptyFolder(Parameters[0]);
                    break;
                case "VerifyFileExtension":
                    VerifyFileExtension(Parameters[0], Parameters[1]);
                    break;
                case "GenerateValue":
                    GenerateValue(Parameters[0], Parameters[1], Parameters[2]);
                    break;
                case "GetProductRootDirectory":
                    GetProductRootDirectory(Parameters[0]);
                    break;
                case "VerifyDownloadedFileExists":
                    VerifyDownloadedFileExists(Parameters[0], Parameters[1]);
                    break;
                case "DocDiffUpdateConfig":
                    DocDiffUpdateConfig(Parameters[0], Parameters[1], Parameters[2]);
                    break;
                default:
                    throw new Exception("Unknown function. Function:" + Keyword);
            }

        }

        /// <summary>
        /// Provides ability to assign a value to a variable
        /// </summary>
        /// <param name="VariableName"></param>
        /// <param name="AttributeName"></param>
        [Keyword("AssignToVariable")]
        public static void AssignToVariable(String VariableName, String Value)
        {
            DlkVariable.SetVariable(VariableName, Value);
            DlkLogger.LogInfo("Successfully executed AssignToVariable(). Variable:[" + VariableName + "], Value:[" + Value + "].");
        }

        [Keyword("CaptureScreenshot")]
        public static void CaptureScreenshot(string FilePath)
        {
            try
            {
                DlkLogger.LogScreenCapture("info", FilePath, false);
                DlkLogger.LogInfo("CaptureScreenshot() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("CaptureScreenshot() failed. " + e.Message);
            }
        }

        [Keyword("DateFromToday")]
        public static void DateFromToday(String VariableName, String AdditionalYear, String AdditionalMonth, String AdditionalDay)
        {
            int addYear;
            int addMonth;
            int addDay;

            DateTime dtNow = SetDateToUSCultureInfo(DateTime.Now.ToString());

            if (!int.TryParse(AdditionalYear, out addYear))
            {
                throw new Exception("Invalid AdditionalYear parameter: " + AdditionalYear);
            }
            else
            {
                dtNow = dtNow.AddYears(addYear);
            }

            if (!int.TryParse(AdditionalMonth, out addMonth))
            {
                throw new Exception("Invalid AdditionalMonth parameter: " + AdditionalMonth);
            }
            else
            {
                dtNow = dtNow.AddMonths(addMonth);
            }

            if (!int.TryParse(AdditionalDay, out addDay))
            {
                throw new Exception("Invalid AdditionalDay parameter: " + AdditionalDay);
            }
            else
            {
                dtNow = dtNow.AddDays(addDay);
            }

            string dtValue = dtNow.ToString("M/d/yyyy");
            DlkVariable.SetVariable(VariableName, dtValue);
            DlkLogger.LogInfo("Successfully executed DateFromToday(). Variable:[" + VariableName + "], Value:[" + dtValue + "].");

        }

        [Keyword("DateFromInputDate")]
        public static void DateFromInputDate(String VariableName, String InputDate, String AdditionalYear, String AdditionalMonth, String AdditionalDay)
        {
            int addYear;
            int addMonth;
            int addDay;
            DateTime dtInput;

            if (!DateTime.TryParse(InputDate, out dtInput))
            {
                throw new Exception("Invalid InputDate parameter: " + InputDate);
            }
            else
            {
                dtInput = SetDateToUSCultureInfo(InputDate);
            }
            if (!int.TryParse(AdditionalYear, out addYear))
            {
                throw new Exception("Invalid AdditionalYear parameter: " + AdditionalYear);
            }
            else
            {
                dtInput = dtInput.AddYears(addYear);
            }

            if (!int.TryParse(AdditionalMonth, out addMonth))
            {
                throw new Exception("Invalid AdditionalMonth parameter: " + AdditionalMonth);
            }
            else
            {
                dtInput = dtInput.AddMonths(addMonth);
            }

            if (!int.TryParse(AdditionalDay, out addDay))
            {
                throw new Exception("Invalid AdditionalDay parameter: " + AdditionalDay);
            }
            else
            {
                dtInput = dtInput.AddDays(addDay);
            }

            string dtValue = dtInput.ToString("M/d/yyyy");
            DlkVariable.SetVariable(VariableName, dtValue);
            DlkLogger.LogInfo("Successfully executed DateFromInputDate(). Variable:[" + VariableName + "], Value:[" + dtValue + "].");
        }

        [Keyword("GetDayFromInputDate")]
        public static void GetDayFromInputDate(String VariableName, String InputDate)
        {
            if (!DateTime.TryParse(InputDate, out DateTime dtInput))
            {
                throw new Exception("Invalid InputDate parameter: " + InputDate);
            }
            else
            {
                dtInput = SetDateToUSCultureInfo(InputDate);
            }

            string dyValue = dtInput.Day.ToString();
            DlkVariable.SetVariable(VariableName, dyValue);
            DlkLogger.LogInfo("Successfully executed GetDayFromInputDate(). Variable:[" + VariableName + "], Value:[" + dyValue + "].");
        }

        [Keyword("DateToday")]
        public static void DateToday(String VariableName)
        {
            DateTime dtNow = SetDateToUSCultureInfo(DateTime.Now.ToString());

            string dtValue = dtNow.ToString("M/d/yyyy");
            DlkVariable.SetVariable(VariableName, dtValue);
            DlkLogger.LogInfo("Successfully executed DateToday(). Variable:[" + VariableName + "], Value:[" + dtValue + "].");
        }

        [Keyword("DateFormat")]
        public static void DateFormat(String VariableName, String InputDate, String Format)
        {
            try
            {
                string dtValue = DlkString.GetDateAsText(Convert.ToDateTime(InputDate), Format);
                DlkVariable.SetVariable(VariableName, dtValue);
                DlkLogger.LogInfo("Successfully executed DateFormat(). Variable:[" + VariableName + "], Value:[" + dtValue + "].");
            }
            catch (Exception e)
            {
                throw new Exception("Invalid input date or date format: " + e.Message);
            }
        }

        [Keyword("StartOfWeek")]
        public static void StartOfWeek(String VariableName, String InputDate)
        {
            try
            {
                //get the date of Monday
                DateTime inputDate = Convert.ToDateTime(InputDate);
                int offset = DayOfWeek.Monday - inputDate.DayOfWeek;
                DateTime startDate = inputDate.AddDays(offset);

                string dtValue = startDate.ToString("M/d/yyyy");
                DlkVariable.SetVariable(VariableName, dtValue);
                DlkLogger.LogInfo("Successfully executed StartOfWeek(). Variable:[" + VariableName + "], Value:[" + dtValue + "].");
            }
            catch (Exception e)
            {
                throw new Exception("Invalid input date or date format: " + e.Message);
            }
        }

        [Keyword("EndOfWeek")]
        public static void EndOfWeek(String VariableName, String InputDate)
        {
            try
            {
                //get the date of Sunday
                DateTime inputDate = Convert.ToDateTime(InputDate);
                int days = DayOfWeek.Sunday - inputDate.DayOfWeek;
                DateTime endDate = inputDate.AddDays(days).AddDays(7);

                string dtValue = endDate.ToString("M/d/yyyy");
                DlkVariable.SetVariable(VariableName, dtValue);
                DlkLogger.LogInfo("Successfully executed EndOfWeek(). Variable:[" + VariableName + "], Value:[" + dtValue + "].");
            }
            catch (Exception e)
            {
                throw new Exception("Invalid input date or date format: " + e.Message);
            }
        }

        [Keyword("StartOfMonth")]
        public static void StartOfMonth(String VariableName, String InputDate)
        {
            try
            {
                //get the 1st day of the Month
                DateTime inputDate = Convert.ToDateTime(InputDate);
                DateTime startDate = new DateTime(inputDate.Year, inputDate.Month, 1);

                string dtValue = startDate.ToString("M/d/yyyy");
                DlkVariable.SetVariable(VariableName, dtValue);
                DlkLogger.LogInfo("Successfully executed DateFormat(). Variable:[" + VariableName + "], Value:[" + dtValue + "].");
            }
            catch (Exception e)
            {
                throw new Exception("Invalid input date or date format: " + e.Message);
            }
        }

        [Keyword("EndOfMonth")]
        public static void EndOfMonth(String VariableName, String InputDate)
        {
            try
            {
                //get the last day of the Month
                DateTime inputDate = Convert.ToDateTime(InputDate);
                DateTime firstDay = new DateTime(inputDate.Year, inputDate.Month, 1);
                DateTime endDate = firstDay.AddMonths(1).AddDays(-1);

                string dtValue = endDate.ToString("M/d/yyyy");
                DlkVariable.SetVariable(VariableName, dtValue);
                DlkLogger.LogInfo("Successfully executed DateFormat(). Variable:[" + VariableName + "], Value:[" + dtValue + "].");
            }
            catch (Exception e)
            {
                throw new Exception("Invalid input date or date format: " + e.Message);
            }
        }

        [Keyword("ComputeNumberOfDays")]
        public static void ComputeNumberOfDays(String StartDate, String EndDate, String VariableName)
        {
            try
            {
                DateTime dateFrom = DateTime.Parse(StartDate);
                DateTime dateTo = DateTime.Parse(EndDate);

                int result = DateTime.Compare(dateFrom, dateTo);
                if (result <= 0)
                {
                    int numDays = (int)(dateTo - dateFrom).TotalDays;
                    DlkVariable.SetVariable(VariableName, numDays.ToString());
                    DlkLogger.LogInfo(string.Format("StartDate[{0}], EndDate[{1}]", StartDate, EndDate));
                    DlkLogger.LogInfo("Successfully executed ComputeNumberOfDays (). Variable: [" + VariableName + "], Value[" + numDays + "]");
                }
                else
                {
                    DlkLogger.LogInfo("ComputeNumberOfDays () : Invalid date range");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ComputeNumberOfDays () : " + e.Message, e);
            }
        }

        [Keyword("CreateTRD")]
        public static void CreateTRD(String ColumnNames, String ColumnValues, String FilePath)
        {
            try
            {
                if (!FilePath.ToLower().EndsWith(".trd"))
                {
                    throw new Exception("Invalid FilePath. FilePath must be .trd.");
                }
                List<string> columnNames = ColumnNames.Split('~').ToList();
                List<string> columnValues = ColumnValues.Split('~').ToList();
                if (columnNames.Count == 0)
                {
                    throw new Exception("Invalid parameters. Column names cannot be empty.");
                }
                if (columnNames.Count > columnValues.Count)
                {
                    int blankValuesCount = columnNames.Count - columnValues.Count;
                    for (int numBlank = 0; numBlank < blankValuesCount; numBlank++)
                    {
                        columnValues.Add("");
                    }
                }
                foreach (string columnName in columnNames)
                {
                    if (!ValidateTRDColumnName(columnName))
                    {
                        throw new Exception("Invalid column name " + columnName + ". Column names must not be empty, must not start with an underscore, and must not contain any other special characters.");
                    }
                }
                if (HasColumnDuplicates(columnNames))
                {
                    throw new Exception("Invalid column names. Columns must have no duplicates.");
                }
                if (File.Exists(FilePath))
                {
                    FileInfo fi = new FileInfo(FilePath);
                    fi.IsReadOnly = false;
                    File.Delete(FilePath);
                }

                List<XElement> datarecs = new List<XElement>();
                foreach (string rec in columnNames)
                {
                    XElement elm = new XElement("datarecord", new XAttribute("name", rec), new XElement("datavalue", columnValues[columnNames.IndexOf(rec)]));
                    datarecs.Add(elm);
                }

                XElement root = new XElement("data", datarecs);
                XDocument doc = new XDocument(root);
                doc.Save(FilePath);
                DlkLogger.LogInfo("Successfully executed CreateTRD()");
            }
            catch (Exception e)
            {
                throw new Exception("CreateTRD () : " + e.Message, e);
            }
        }

        [Keyword("SendKeys", new String[] { @"1|text|Keys|Control~Alt~Delete" })]
        public static void SendKeys(String Keys)
        {
            string[] arrKeys = Keys.Split('~');
            string strKeyToSend = string.Empty;
            List<string> specialKey = new List<string>(); 

            try
            {
                OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                Type typeOfKeys = typeof(OpenQA.Selenium.Keys);

                // To handle special case of 'Ctrl+~/Ctrl~~' for shortcut key of Next tab in Costpoint/TE
                if (Keys.Contains("~~"))
                {
                    FieldInfo fld = typeOfKeys.GetField(arrKeys[0]);
                    if (fld != null)
                    {
                        strKeyToSend += fld.GetValue(typeOfKeys).ToString();
                        strKeyToSend += "`".ToLower();
                    }
                }
                else
                {
                    int arrKeysLen = arrKeys.Count();
                    // parse input Keys and convert to Selenium.Keys equivalent
                    for (int i = 0; i < arrKeysLen; i++)
                    {
                        FieldInfo fld = typeOfKeys.GetField(arrKeys[i]);

                        if (fld != null) // key is a special key, Tab, Control, etc.
                        {
                            string specialKeyCode = fld.GetValue(typeOfKeys).ToString();

                            if (arrKeysLen > 1 && i != arrKeysLen - 1) // press down special keys
                            {
                                mAction.KeyDown(specialKeyCode);
                                specialKey.Add(specialKeyCode);
                            }
                            else
                            {
                                mAction.SendKeys(specialKeyCode);
                                strKeyToSend += specialKeyCode;
                            }                      
                        }
                        else // key is a standard input key, A, 2, etc.
                        {
                            mAction.SendKeys(arrKeys[i].ToLower());
                            strKeyToSend += arrKeys[i].ToLower();
                        }
                    }
                }
                DlkLogger.LogInfo("SendKeys() : keys to send = " + string.Join("", specialKey)  + strKeyToSend);
                specialKey.ForEach(key => mAction.KeyUp(key)); // release special keys
                mAction.Build().Perform();
                DlkLogger.LogInfo("Successfully executed SendKeys()");
            }
            catch (Exception e)
            {
                throw new Exception("SendKeys() failed : " + e.Message, e);
            }
        }

        [Keyword("TextConcat")]
        public static void TextConcat(String VariableName, String OriginalText, String TextToConcatenate)
        {
            string strResult = string.Concat(OriginalText, TextToConcatenate);
            DlkVariable.SetVariable(VariableName, strResult);
            DlkLogger.LogInfo("Successfully executed TextConcat(). Variable:[" + VariableName + "], Value:[" + strResult + "].");
        }

        [Keyword("TextReplace")]
        public static void TextReplace(String VariableName, String InputText, String TexttoReplace, String TextReplacement)
        {
            string strResult = InputText.Replace(TexttoReplace, TextReplacement);
            DlkVariable.SetVariable(VariableName, strResult);
            DlkLogger.LogInfo("Successfully executed TextReplace(). Variable:[" + VariableName + "], Value:[" + strResult + "].");
        }

        [Keyword("TextSubstring")]
        public static void TextSubstring(String VariableName, String InputText, String StartIndex, String Length)
        {
            int startIndex;
            int length;
            string strResult = "";
            if (!int.TryParse(StartIndex, out startIndex))
            {
                throw new Exception("Invalid StartIndex parameter: " + StartIndex);
            }
            else
            {
                if (!int.TryParse(Length, out length))
                {
                    throw new Exception("Invalid Length parameter: " + Length);
                }
                else
                {
                    try
                    {
                        if(length < 0){
                            /* if length is negative, move count of length to the left of the start index */
                            length = Math.Abs(length);
                            startIndex= (startIndex-length) + 1;
                            strResult = InputText.Substring(startIndex, length);
                        }
                        else{
                            /* count length to the right of start index */
                            strResult = InputText.Substring(startIndex, length);
                        }
                        DlkVariable.SetVariable(VariableName, strResult);
                        DlkLogger.LogInfo("Successfully executed TextSubstring(). Variable:[" + VariableName + "], Value:[" + strResult + "].");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error Retrieving Substring: " + ex.Message);
                    }
                }
            }

        }        

        [Keyword("LogComment", new String[] { @"1|text|Comment|This is a comment" })]
        public static void LogComment(String CommentToLog)
        {
            try
            {
                DlkLogger.LogInfo("LogComment: " + CommentToLog);
            }
            catch (Exception ex)
            {
                throw new Exception("LogComment() failed: " + ex.Message);
            }
        }

        [Keyword("PerformMathOperation", new String[] { @"1|text|Comment|This is a comment" })]
        public static void PerformMathOperation(String FirstOperand, String SecondOperand, String Operator, String VariableName)
        {
            try
            {
                Decimal res = 0;
                Decimal num1 = 0;
                Decimal num2 = 0;

                if (!Decimal.TryParse(FirstOperand, out num1))
                {
                    throw new Exception("PerformMathOperation(): Invalid Input1 Parameter: " + FirstOperand);
                }
                if (!Decimal.TryParse(SecondOperand, out num2))
                {
                    throw new Exception("PerformMathOperation(): Invalid Input2 Parameter: " + SecondOperand);
                }

                /* Determine number of decimal places of output by getting the max decimal places between Operand1 and Operand2 */
                int firstOperandNumDecimal = FirstOperand.Contains('.') ? (FirstOperand.Length - 1) - FirstOperand.IndexOf('.') : 0;
                int secondOperandNumDecimal = SecondOperand.Contains('.') ? (SecondOperand.Length - 1) - SecondOperand.IndexOf('.') : 0;
                int numDecimalPlaces = Math.Max(firstOperandNumDecimal, secondOperandNumDecimal);

                int firstOperandWholeNumString = FirstOperand.Contains('.') ? FirstOperand.IndexOf('.') : FirstOperand.Contains('-') ? FirstOperand.Length - 1 : FirstOperand.Length;
                int secondOperandWholeNumString = SecondOperand.Contains('.') ? SecondOperand.IndexOf('.') : SecondOperand.Contains('-') ? SecondOperand.Length - 1 : SecondOperand.Length;
                int numberWholeNumber = Math.Max(firstOperandWholeNumString, secondOperandWholeNumString);

                string fmtWhole = string.Empty;
                string fmtDecimal = string.Empty;

                for (int i = 0; i < numberWholeNumber; i++)
                {
                    fmtWhole += "0";
                }

                for (int i = 0; i < numDecimalPlaces; i++)
                {
                    fmtDecimal += "0";
                }

                string fmt = !fmtDecimal.Equals("") ? string.Format("{0}.{1}", fmtWhole, fmtDecimal) : fmtWhole;

                switch (Operator)
                {
                    case "+":
                        DlkLogger.LogInfo("PerformMathOperation(). Operation:[" + FirstOperand + "] + [" + SecondOperand + "].");
                        res = num1 + num2;
                        break;
                    case "-":
                        DlkLogger.LogInfo("PerformMathOperation(). Operation:[" + FirstOperand + "] - [" + SecondOperand + "].");
                        res = num1 - num2;
                        break;
                    case "*":
                        DlkLogger.LogInfo("PerformMathOperation(). Operation:[" + FirstOperand + "] * [" + SecondOperand + "].");
                        res = num1 * num2;
                        break;
                    case "/":
                        DlkLogger.LogInfo("PerformMathOperation(). Operation:[" + FirstOperand + "] / [" + SecondOperand + "].");
                        res = num1 / num2;
                        break;
                    default:
                        throw new Exception("PerformMathOperation(): Unsupported operator " + Operator);
                }
                /* Round UP the output to numDecimalPlaces */

                decimal resDec = decimal.Round(res, numDecimalPlaces, MidpointRounding.AwayFromZero);
                String resString = resDec.ToString();
                if (!resString.Contains('-') && (FirstOperand.Substring(0,1).Contains('0') || SecondOperand.Substring(0, 1).Contains('0')))
                    resString = resDec.ToString(fmt);
                else
                    resString = resDec.ToString();

                DlkVariable.SetVariable(VariableName, resString);
                DlkLogger.LogInfo("Successfully executed PerformMathOperation(). Variable:[" + VariableName + "], Value:[" + resString + "].");
            }
            catch (Exception ex)
            {
                throw new Exception("PerformMathOperation() failed: " + ex.Message);
            }
        }

        [Keyword("TurnOffTestSteps", new String[] { @"1|text|StepsToTurnOff|1~3~5" })]
        public static void TurnOffTestSteps(String StepsToTurnOff)
        {
            mTurnedOffSteps = new List<string>(StepsToTurnOff.Split('~'));
        }

        private static DateTime SetDateToUSCultureInfo(String dateToSet)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            DateTime dt = DateTime.Parse(dateToSet, cultureInfo); //parse date to en-US date format
            return dt;
        }

        [Keyword("SetDelayRetries")]
        public static void SetDelayRetries(String Delay, String Retries)
        {
            DlkEnvironment.VerifyRetryDelay = Convert.ToInt32(Delay);
            DlkEnvironment.VerifyRetryCount = Convert.ToInt32(Retries);
        }

        [Keyword("CloseBrowser")]
        public static void CloseBrowser()
        {
            try
            {
                DlkEnvironment.CloseBrowser();
                DlkLogger.LogInfo("Successfully executed CloseBrowser()");
            }
            catch (Exception e)
            {
                throw new Exception("CloseBrowser() failed : " + e.Message);
            }
        }

        [Keyword("ScrollUp")]
        public static void ScrollUp()
        {
            try
            {
                ((OpenQA.Selenium.Remote.RemoteWebDriver)DlkEnvironment.AutoDriver).ExecuteScript("scroll(20000,0)");
                DlkLogger.LogInfo("Successfully executed ScrollUp()");
            }
            catch (Exception e)
            {
                throw new Exception("ScrollUp() failed : " + e.Message);
            }
        }

        [Keyword("ScrollDown")]
        public static void ScrollDown()
        {
            try
            {
                ((OpenQA.Selenium.Remote.RemoteWebDriver)DlkEnvironment.AutoDriver).ExecuteScript("scroll(0,20000)");
                DlkLogger.LogInfo("Successfully executed ScrollDown()");
            }
            catch (Exception e)
            {
                throw new Exception("ScrollDown() failed : " + e.Message);
            }
        }

        [Keyword("Wait", new String[] { "1|text|Wait Time (secs)|30" })]
        public static void Wait(String WaitTime)
        {
            int sec = 0;

            if (int.TryParse(WaitTime, out sec))
            {
                for (int cnt = 1; cnt <= sec; cnt++)
                {
                    Thread.Sleep(1000);
                    DlkLogger.LogInfo("Wait() : Waiting ... " + cnt + " sec elapsed");
                }
            }
        }

        [Keyword("MoveFile")]
        public static void MoveFile(String FileName, String SourcePath, String DestinationPath, String OverwriteExisting = "False")
        {
            string sourceFile = Path.Combine(SourcePath, FileName);
            string destinationFile = Path.Combine(DestinationPath, FileName);
            try
            {
                bool overwriteExistingFile = Convert.ToBoolean(OverwriteExisting);
                if (File.Exists(destinationFile) && overwriteExistingFile)
                {
                    File.Delete(destinationFile);
                }
                File.Move(sourceFile,destinationFile);
                DlkLogger.LogInfo("Successfully executed MoveFile() of " + sourceFile + " to " + destinationFile + ".");
            }
            catch (Exception e)
            {
                throw new Exception("MoveFile() failed : " + e.Message);
            }
        }

        [Keyword("DocDiff")]
        public static void DocDiffAPI(String ConfigFile, String ExpectedFile, String ActualFile, String OutputFile)
        {
            StringBuilder sb = new StringBuilder();
            string inputOutputFile = OutputFile;

            if (OutputFile.Contains('.'))
            {
                var splitOF = OutputFile.Split('.');
                OutputFile = String.Join(".", splitOF.Select((o, i) =>
                {
                    if (i == splitOF.Count() - 1) return "html";
                    else return o;
                }));
            }
            else OutputFile = OutputFile + ".html";

            DlkLogger.LogInfo("DocDiff: ConfigFile: " + ConfigFile);
            DlkLogger.LogInfo("DocDiff: ExpectedFile: " + ExpectedFile);
            DlkLogger.LogInfo("DocDiff: ActualFile: " + ActualFile);
            DlkLogger.LogInfo("DocDiff: OutputFile: " + OutputFile);

            if (string.IsNullOrWhiteSpace(ConfigFile))
            {
                sb.AppendLine("ConfigFile should not be empty.");
            }
            if (string.IsNullOrWhiteSpace(ExpectedFile))
            {
                sb.AppendLine("ExpectedFile should not be empty.");
            }
            if (string.IsNullOrWhiteSpace(ActualFile))
            {
                sb.AppendLine("ActualFile should not be empty.");
            }
            if (string.IsNullOrWhiteSpace(inputOutputFile))
            {
                sb.AppendLine("OutputFile should not be empty.");
            }
            if (sb.Length != 0)
            {
                string missingMessage = sb.ToString();
                missingMessage = missingMessage.Substring(0, missingMessage.LastIndexOf("\r"));
                throw new Exception(missingMessage);
            }

            //if absolute path, use it. if not, form path from default folder
            string mExpectedFile = Path.IsPathRooted(ExpectedFile) ? ExpectedFile : Path.Combine(DlkEnvironment.mDirDocDiffExpectedFile, ExpectedFile);
            string mActualFile = Path.IsPathRooted(ActualFile) ? ActualFile : Path.Combine(DlkEnvironment.mDirDocDiffActualFile, ActualFile);
            string mConfigFile = Path.IsPathRooted(ConfigFile) ? ConfigFile : Path.Combine(DlkEnvironment.mDirDocDiffConfigFile, ConfigFile);

            //check if OutputFile is a valid file name
            string mOutputFile;
            if (Path.IsPathRooted(OutputFile))
            {
                mOutputFile = OutputFile;
            }
            else if (!string.IsNullOrEmpty(OutputFile) && OutputFile.IndexOfAny(Path.GetInvalidFileNameChars()) < 0)
            {
                string outputPath = Path.Combine(DlkEnvironment.mDirDocDiff, "OutputFile");
                //create directory: if directory already exist, it does nothing
                Directory.CreateDirectory(outputPath);
                mOutputFile = Path.Combine(outputPath, OutputFile);
            }
            else
            {
                throw new Exception("DocDiff(). OutputFile is invalid: " + OutputFile);
            }

            if (!File.Exists(mConfigFile))
            {
                sb.AppendLine("DocDiff(). ConfigFile does not exist: " + mConfigFile);
            }
            if (!File.Exists(mExpectedFile))
            {
                sb.AppendLine("DocDiff(). ExpectedFile does not exist: " + mExpectedFile);
            }
            if (!File.Exists(mActualFile))
            {
                // might be saving... wait for upto 60 s
                Boolean bActualFile = false;
                for (int i = 0; i < 120; i++)
                {
                    if (File.Exists(mActualFile))
                    {
                        bActualFile = true;
                        break;
                    }
                    Thread.Sleep(500);
                }
                if (!bActualFile)
                {
                    sb.AppendLine("DocDiff(). ActualFile does not exist: " + mActualFile);
                }
            }
            if (sb.Length != 0)
            {
                string missingMessage = sb.ToString();
                missingMessage = missingMessage.Substring(0, missingMessage.LastIndexOf("\r"));
                throw new Exception(missingMessage);
            }
            if (File.Exists(mOutputFile))
            {
                FileInfo fi = new FileInfo(mOutputFile);
                fi.IsReadOnly = false;
                File.Delete(mOutputFile);
                DlkLogger.LogInfo("DocDiff: Found and deleted existing output file: " + mOutputFile);
            }

            DlkEnvironment.RunProcess(DlkEnvironment.mDirTools + @"TestRunner\DocDiff.exe",
                GetDocDiffArguments(mExpectedFile, mActualFile, mOutputFile, mConfigFile), 
                DlkEnvironment.mDirTools + @"TestRunner",
                false,
                0);

            // wait for upto 30 seconds
            for (int i = 0; i < 60; i++)
            {
                if (File.Exists(mOutputFile))
                {
                    break;
                }
                Thread.Sleep(500);
            }

            if (File.Exists(mOutputFile))
            {
                XDocument DlkHTML = XDocument.Load(mOutputFile);
                var data = DlkHTML.Descendants("data").FirstOrDefault();
                if (data == null) throw new Exception("DocDiff(): No data found on the document.");

                var errorCount = data.Element("errorcount").Value;
                var sheetNameCompCount = data.Element("sheetnamecompcount").Value;

                bool haveErrors = Convert.ToInt32(errorCount) > 0;
                bool sheetNameError = Convert.ToInt32(sheetNameCompCount) > 0;
                if (haveErrors)
                {
                    string errorType = sheetNameError ? "sheet name comparison" : "comparison";
                    throw new Exception("DocDiff(): Errors found during " + errorType + ". See outputfile: " + mOutputFile);
                }
                else DlkLogger.LogInfo("DocDiff(): No differences found between Expected and Actual files.");
            }
            else
            {
                throw new Exception("DocDiff(): Output file not found. File: " + mOutputFile);
            }
        }

        private static string GetDocDiffArguments(string expectedFile, string actualFile, string outputFile, string configFile)
        {
            string mExpectedFile = "\"" + expectedFile + "\"";
            string mActualFile = "\"" + actualFile + "\"";
            string mOutputFile = "\"" + outputFile + "\"";
            string mConfigFile = "\"" + configFile + "\"";

            return " -expected " + mExpectedFile + " -actual " + mActualFile + " -output " + mOutputFile + " -config " + mConfigFile;
        }

        /// <summary>
        /// Checks and returns the validity of a column name based on Test Editor Data View column name checks
        /// </summary>
        /// <param name="columnName">Column name to check</param>
        private static bool ValidateTRDColumnName(string columnName)
        {
            return (!string.IsNullOrEmpty(columnName) && (!columnName.StartsWith("_") && new Regex("^[A-Za-z0-9]*(?:_[A-Za-z0-9]+)*$").IsMatch(columnName)));
        }

        /// <summary>
        /// Checks and returns whether there are duplicates in a list of column names
        /// </summary>
        /// <param name="columnNames">ist of column names</param>
        private static bool HasColumnDuplicates(List<string> columnNames)
        {
            List<string> lowerCaseList = columnNames.ConvertAll(x => x.ToLower());
            return lowerCaseList.Count != lowerCaseList.Distinct().Count();
        }

        [Keyword("LetterCase")]
        public static void LetterCase(String VariableName, String InputString, String Format)
        {
            try
            {
                string dtValue = DlkString.GetCaseAsText(Convert.ToString(InputString), Format);
                DlkVariable.SetVariable(VariableName, dtValue);
                DlkLogger.LogInfo("Successfully executed LetterCase(). Variable:[" + VariableName + "], Value:[" + dtValue + "].");
            }
            catch (Exception e)
            {
                throw new Exception("Invalid input string: " + e.Message);
            }
        }

        [Keyword("TextEqual")]
        public static void TextEqual(String FirstInput, String SecondInput)
        {
            try
            {
                DlkAssert.AssertEqual("TextEqual()", FirstInput, SecondInput);
                DlkLogger.LogInfo("Successfully executed TextEqual().");
            }
            catch (Exception e)
            {
                throw new Exception("TextEqual() failed: " + e.Message);
            }
        }

        [Keyword("CopyFile")]
        public static void CopyFile(String FileName, String SourceFilePath, String DestinationFilePath, String OverwriteExisting)
        {
            string sourceFile = Path.Combine(SourceFilePath, FileName);
            string destinationFile = Path.Combine(DestinationFilePath, FileName);
            try
            {
                bool overwriteExistingFile = Convert.ToBoolean(OverwriteExisting);
                File.Copy(sourceFile, destinationFile, overwriteExistingFile);
                DlkLogger.LogInfo("Successfully executed CopyFile() of " + FileName + " to " + DestinationFilePath + ".");  
            }
            catch (Exception e)
            {
                throw new Exception("CopyFile() failed : " + e.Message);
            }
        }

        [Keyword("EmptyFolder")]
        public static void EmptyFolder(String FolderPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(FolderPath);

                foreach (FileInfo fi in dir.GetFiles())
                {
                    fi.Delete();
                }

                foreach (DirectoryInfo di in dir.GetDirectories())
                {
                    EmptyFolder(di.FullName);
                    di.Delete();
                }
                DlkLogger.LogInfo("Successfully executed EmptyFolder() of " + FolderPath + ".");
            }
            catch (Exception e)
            {
                throw new Exception("EmptyFolder() failed : " + e.Message);
            }
        }

        [Keyword("VerifyFileExtension")]
        public static void VerifyFileExtension(String FullFilePath, String ExpectedExtension)
        {
            try
            {
                FileInfo file = new FileInfo(FullFilePath);
                string ActualExtension = file.Extension;
                DlkAssert.AssertEqual("VerifyFileExtension() : ", ExpectedExtension, ActualExtension);
                DlkLogger.LogInfo("Successfully executed VerifyFileExtension().");

            }
            catch(Exception e)
            {
                throw new Exception("VerifyFileExtension() failed : " + e.Message);
            }
        }

        /// <summary>
        /// Generates random value
        /// </summary>
        /// <param name="Prefix">User defined prefix</param>
        /// <param name="NumberOfRandomValue">Number of random value [alphanumeric]</param>
        /// <param name="VariableName">Variable which the random value will be assigned</param>
        [Keyword("GenerateValue", new String[] { "Prefix|Text|AA", "NumberOfRandomValue|Text|2", "VariableName|Text|ID" })]
        public static void GenerateValue(string Prefix, string NumberOfRandomValue, string VariableName)
        {
            try
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

                if (!int.TryParse(NumberOfRandomValue, out int randomCount))
                {
                    throw new Exception($"Parameter 'NumberOfRandomValue' must be an integer value.");
                }
                else if (randomCount < 1)
                {
                    throw new Exception($"NumberOfRandomValue must be greater than zero.");
                }

                Random random = new Random();
                string randomValue = new string(Enumerable.Repeat(chars, randomCount)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
                DlkVariable.SetVariable(VariableName, Prefix + randomValue);

                DlkLogger.LogInfo("GenerateValue(): Successfully executed");
            }
            catch (Exception e)
            {
                throw new Exception("GenerateValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetProductRootDirectory", new String[] { "VariableName|Text|ProductDir"})]
        public static void GetProductRootDirectory(string VariableName)
        {
            try
            {
                DlkVariable.SetVariable(VariableName, DlkEnvironment.mDirProductsRoot);
            }
            catch (Exception e)
            {
                throw new Exception("GetProductRootDirectory() failed : " + e.Message);
            }
        }

        /// <summary>
        /// Checks whether file is existing under default Downloads folder
        /// </summary>
        /// <param name="FileNameWithExtension">File name with extension</param>
        /// <param name="DeleteFile">Set to true if file needs to be deleted after checking</param>
        [Keyword("VerifyDownloadedFileExists")]
        public static void VerifyDownloadedFileExists(string FileNameWithExtension, string DeleteFile)
        {
            bool existing = false;
            string fullFilePath = "";
            try
            {
                string downloadsPath = DlkKnownFolders.GetPath(KnownFolder.Downloads);
                fullFilePath = Path.Combine(downloadsPath, FileNameWithExtension);
                existing = File.Exists(fullFilePath);

                DlkAssert.AssertEqual("VerifyDownloadedFileExists", true, existing);
                DlkLogger.LogInfo("VerifyDownloadedFileExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDownloadedFileExists() failed : " + e.Message, e);
            }
            finally
            {
                if (Convert.ToBoolean(DeleteFile) && existing)
                {
                    File.Delete(fullFilePath);
                }
            }
        }

        /// <summary>
        /// Updates the configuration value on DocDiff 
        /// </summary>
        /// <param name="FilePath">Configuration file to update</param>
        /// <param name="Config">Configuration node</param>
        /// <param name="Value">Configuration value</param>
        [Keyword("DocDiffUpdateConfig")]
        public static void DocDiffUpdateConfig(string FilePath, string Config, string Value)
        {
            try
            {
                string _filePath = string.Empty;

                if(string.IsNullOrEmpty(FilePath))
                {
                    throw new Exception("FilePath field must not be empty");
                }

                if (string.IsNullOrEmpty(Config))
                {
                    throw new Exception("Config field must not be empty");
                }

                if (string.IsNullOrEmpty(Value))
                {
                    throw new Exception("Value field must not be empty");
                }

                if (!DlkString.IsValidConfigValue(Value.Trim()))
                {
                    throw new Exception("Value contains invalid characters");
                }

                if(Path.IsPathRooted(FilePath.Trim()))
                {
                    _filePath = FilePath.Trim();
                }
                else
                {
                    _filePath = DlkEnvironment.mDirDocDiffConfigFile + FilePath.Trim();
                }

                FileInfo fileInfo = new FileInfo(_filePath);
                if (!fileInfo.Exists ||fileInfo.IsReadOnly)
                {
                    throw new Exception("File does not exist or read only");
                }

                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(_filePath);
                var xElem = xDoc.SelectSingleNode("//type[@name='" + Config.Trim() + "']");
                if (xElem is null)
                {
                    throw new Exception(string.Format("Config '{0}' does not exist on file '{1}'", Config, _filePath));
                }

                xElem.InnerText = Value.Trim();
                xDoc.Save(_filePath);

                DlkLogger.LogInfo("Successfully executed DocDiffUpdateConfig(). Config:[" + Config + "], Value:[" + Value + "].");
            }
            catch(Exception ex)
            {
                throw new Exception(string.Format("DocDiffUpdateConfig() failed : An error occured while trying to update the config file.\n\nError: {0}", ex.Message));
            }
        }
    }
}
