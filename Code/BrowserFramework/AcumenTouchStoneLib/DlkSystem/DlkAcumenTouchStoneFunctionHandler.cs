using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using AcumenTouchStoneLib.DlkControls;
using AcumenTouchStoneLib.DlkFunctions;
using OpenQA.Selenium;
using System.Diagnostics;
using CommonLib.DlkUtility;
using System.IO;
using System.Globalization;

namespace AcumenTouchStoneLib.DlkSystem
{
    [ControlType("Function")]
    public static class DlkAcumenTouchStoneFunctionHandler
    {
        public static string errorMessageContent = "";
        public static IWebElement errorMessageElement;
        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }
        public static void ExecuteFunction(String Screen, String ControlName, String Keyword, String[] Parameters)
        {
            if (Screen == "Function")
            {
                switch (Keyword)
                {
                    case "WaitScreenGetsReady":
                        WaitScreenGetsReady();
                        break;
                    case "GoToUrl":
                        GoToUrl(Parameters[0]);
                        break;
                    case "GetEnvironmentDetails":
                        GetEnvironmentDetails(Parameters[0], Parameters[1]);
                        break;
                    case "ClickDialogHeader":
                        ClickDialogHeader();
                        break;
                    case "VerifyFileExists":
                        VerifyFileExists(Parameters[0], Parameters[1], Parameters[2]);
                        break;
                    case "IfThenElse":
                        IfThenElse(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4]);
                        break;
                    case "GetAdjustedDatePeriod":
                        GetAdjustedDatePeriod(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4], Parameters[5]);
                        break;
                    default:
                        DlkFunctionHandler.ExecuteFunction(Keyword, Parameters);
                        break;
                }
            }
            else
            {
                switch (Screen)
                {
                    case "Login":
                        switch (Keyword)
                        {
                            case "Login":
                                Login(Parameters[0], Parameters[1], Parameters[2], Parameters[3]);
                                break;
                            default:
                                throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                        }
                        break;
                    case "Dialog":
                        switch (Keyword)
                        {
                            case "ClickOkDialogWithMessage":
                                DlkDialog.ClickOkDialogWithMessage(Parameters[0]);
                                break;
                            case "ClickCancelDialogWithMessage":
                                DlkDialog.ClickCancelDialogWithMessage(Parameters[0]);
                                break;
                            case "ClickOkDialogIfExists":
                                DlkDialog.ClickOkDialogIfExists(Parameters[0]);
                                break;
                            case "SaveFile":
                                DlkDialog.SaveFile();
                                break;
                            case "FileDownload":
                                DlkDialog.FileDownload(Parameters[0], Parameters[1]);
                                break;
                            case "FileUpload":
                                DlkDialog.FileUpload(Parameters[0], Parameters[1]);
                                break;
                            case "VerifySupportedFileTypes":
                                DlkDialog.VerifySupportedFileTypes(Parameters[0]);
                                break;
                            case "VerifyFileName":
                                DlkDialog.VerifyFileName(Parameters[0]);
                                break;
                            default:
                                throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                        }
                        break;
                    default:
                        throw new Exception("Unknown function. Screen: " + Screen + ", Function:" + Keyword);
                }
            }
        }

        [Keyword("WaitScreenGetsReady")]
        public static void WaitScreenGetsReady()
        {
            try
            {
                IWebElement basePage;
                var pageDialogs = DlkEnvironment.AutoDriver.FindElements(By.XPath("//*[@role='dialog']")).Where(x => x.Displayed).ToList();
                if (pageDialogs.Count > 0)
                {
                    basePage = pageDialogs.Last();
                }
                else
                {
                    basePage = DlkEnvironment.AutoDriver.FindElement(By.XPath("//*"));
                }
                long ctr = 0;
                var spinners = basePage.FindElements(By.XPath(".//*[contains(@class,'spinner')][not(contains(@class,'spinnercontainer'))][not(contains(@class,'spinnerContainer'))][not(contains(@class,'spinner-area'))][not(contains(@class,'ui-spinner'))][not(contains(@class,'spinner_field'))][not(contains(@class,'grid-spinner'))]")).Where(x => x.Displayed).ToList();
                var blockers = basePage.FindElements(By.XPath(".//*[contains(@id,'application-blocker')][not(contains(@style,'display: none;'))]"));
                var mainPageLoading = basePage.FindElements(By.XPath(".//*[contains(@class,'ngcrm en-US')]/*[@id='applicationLoadingOverlay']"));
                var reportViewerSpinner = basePage.FindElements(By.XPath(".//*[@id='sqlrsReportViewer_AsyncWait_Wait']"));
                var afterSaveLoading = basePage.FindElements(By.XPath(".//*[@class='application-loading-message-body']")).Where(x => x.Displayed).ToList();
                var overlayLoading = basePage.FindElements(By.XPath(".//*[@class='application-loading-message-overlay']")).Where(x => x.Displayed).ToList();
                Action<IReadOnlyCollection<IWebElement>> CheckSpinners = (collection) =>
                {
                    ctr = 0;
                    int retryLimit = 3, retry = 0, maxWaitTime = 120000;

                    while (retry < retryLimit)
                    {
                        retry++;
                        Stopwatch mWatch = Stopwatch.StartNew();
                        mWatch.Start();

                        while ((mWatch.ElapsedMilliseconds < maxWaitTime / retryLimit) && (collection.Count(x => x.Displayed == true) > 0))
                        {
                            DlkLogger.LogInfo("Waiting for page to load completely...");
                            Thread.Sleep(1000);
                        }
                        ctr += mWatch.ElapsedMilliseconds;
                        mWatch.Stop();
                        if (collection.Count(x => x.Displayed == true) > 0)
                        {
                            DlkLogger.LogInfo("It's been " + (ctr / 1000).ToString() + " seconds.");
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            retry = 3;
                        }
                    }
                    if (collection.Count(x => x.Displayed == true) > 0)
                    {
                        DlkLogger.LogInfo("Page not loaded within 120 seconds. Proceeding...");
                    }
                };
                List<IWebElement> itemsForWait = new List<IWebElement>();
                itemsForWait.AddRange(mainPageLoading);
                itemsForWait.AddRange(spinners);
                itemsForWait.AddRange(blockers);
                itemsForWait.AddRange(reportViewerSpinner);
                itemsForWait.AddRange(afterSaveLoading);
                itemsForWait.AddRange(overlayLoading);
                DlkLogger.LogInfo("Checking for spinners, blockers and page loading...");
                CheckSpinners(itemsForWait);
                DlkLogger.LogInfo("Slept for " + (ctr / 1000).ToString() + " seconds");
            }
            catch (StaleElementReferenceException)
            {
                //do nothing for now, page is expected to refresh so Stale Element Reference Exception is expected.
            }
            catch (Exception ex)
            {
                throw new Exception("WaitScreenGetsReady() failed : " + ex.Message);
            }
        }

        [Keyword("GoToUrl")]
        public static void GoToUrl(string URL)
        {
            try
            {
                DlkEnvironment.AutoDriver.Navigate().GoToUrl(URL);
            }
            catch (Exception ex)
            {
                throw new Exception("GoToUrl() failed: " + ex.Message);
            }
        }

        [Keyword("GetEnvironmentDetails", new String[] { "Username | SampleVar" })]
        public static void GetEnvironmentDetails(String Detail, String VariableName)
        {
            try
            {
                DlkLoginConfigRecord loginConfigRecord = DlkLoginConfigHandler.GetLoginConfigInfo(DlkEnvironment.mLoginConfigFile, DlkEnvironment.mLoginConfig);
                DlkDecryptionHelper decryptor = new DlkDecryptionHelper();

                string detailValue = "";
                switch (Detail.ToLower())
                {
                    case "url":
                        detailValue = loginConfigRecord.mUrl;
                        break;
                    case "username":
                        detailValue = decryptor.IsDecrpytable(loginConfigRecord.mUser) ? decryptor.DecryptByteArrayToString(Convert.FromBase64String(loginConfigRecord.mUser)) : loginConfigRecord.mUser;
                        break;
                    case "password":
                        detailValue = decryptor.IsDecrpytable(loginConfigRecord.mPassword) ? decryptor.DecryptByteArrayToString(Convert.FromBase64String(loginConfigRecord.mPassword)) : loginConfigRecord.mPassword;
                        break;
                    case "database":
                        detailValue = decryptor.IsDecrpytable(loginConfigRecord.mDatabase) ? decryptor.DecryptByteArrayToString(Convert.FromBase64String(loginConfigRecord.mDatabase)) : loginConfigRecord.mDatabase;
                        break;
                    default:
                        throw new Exception("Invalid input for detail. Detail should either be username, password or database.");
                }

                DlkVariable.SetVariable(VariableName, detailValue);
                DlkLogger.LogInfo("[" + detailValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetEnvironmentDetails() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetEnvironmentDetails() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickDialogHeader")]
        public static void ClickDialogHeader()
        {
            try
            {
                IWebElement dialog = DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@role='dialog'][not(contains(@style,'display: none'))][not(contains(@style,'display:none'))][not(contains(@class,'hidden'))]//div[contains(@class, 'titlebar')]"));
                dialog.Click();
            }
            catch (Exception ex)
            {
                throw new Exception("ClickDialogHeader() failed: " + ex.Message);
            }
        }

        [Keyword("VerifyFileExists")]
        public static void VerifyFileExists(String DefaultFolderOrPath, String FileName, String TrueOrFalse)
        {
            try
            {
                bool expectedValue;
                if (!Boolean.TryParse(TrueOrFalse, out expectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                string verifyPath = "";

                if (Path.IsPathRooted(DefaultFolderOrPath))
                    verifyPath = DefaultFolderOrPath;
                else
                {
                    string currentUserPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    verifyPath = Path.Combine(currentUserPath, DefaultFolderOrPath);
                }

                bool actualValue = File.Exists(Path.Combine(verifyPath, FileName));

                DlkAssert.AssertEqual("VerifyFileExists()", expectedValue, actualValue);
                DlkLogger.LogInfo("VerifyFileExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyFileExists() failed : " + e.Message);
            }
        }

        [Keyword("GetAdjustedDatePeriod", new String[] { "DateInput | DateOffset | CalendarDays | " })]
        public static void GetAdjustedDatePeriod(String DateInput, String DateOffset, String CalendarDays, String StartOnDateVar, String EndOnDateVar, String PeriodEndingVar)
        {
            try
            {
                DateTime dateInput = Convert.ToDateTime(DateInput);
                DateTime startPeriod;
                DateTime endPeriod;
                DateTime startOnDate;
                DateTime endOnDate;
                DateTime periodEndingDate;
                int dateOffset = Convert.ToInt32(DateOffset);
                dateOffset = dateOffset != 0 ? dateOffset < 0 ? dateOffset + 1 : dateOffset - 1 : dateOffset;
                int calendarDays = Convert.ToInt32(CalendarDays);
                calendarDays = calendarDays != 0 ? calendarDays - 1 : calendarDays;

                //Get end date period from date input
                endPeriod = dateInput;
                if (endPeriod.DayOfWeek != DayOfWeek.Friday)
                {
                    while (endPeriod.DayOfWeek != DayOfWeek.Friday)
                    {
                        endPeriod = endPeriod.AddDays(1);
                    }
                }

                //Add offset days to end period to get start on date
                startOnDate = endPeriod.AddDays(dateOffset);
                DlkLogger.LogInfo("Adding offset ["+ DateOffset + "] to end date period: [" + endPeriod.ToString("M/dd/yyyy") + "]...");

                //Add calendar days to start on date to get end on date
                endOnDate = startOnDate.AddDays(calendarDays);
                DlkLogger.LogInfo("Adding calendar days [" + CalendarDays + "] to start on date: [" + startOnDate.ToString("M/dd/yyyy") + "]...");

                //Get start date period from new end on date
                startPeriod = endOnDate;
                if (startPeriod.DayOfWeek != DayOfWeek.Saturday)
                {
                    while (startPeriod.DayOfWeek != DayOfWeek.Saturday)
                    {
                        startPeriod = startPeriod.AddDays(-1);
                    }
                }

                //Add calendar days to start period to get period ending date
                periodEndingDate = startPeriod.AddDays(calendarDays);
                DlkLogger.LogInfo("Adding calendar days [" + CalendarDays + "] to start date period: [" + startPeriod.ToString("M/dd/yyyy") + "]...");

                string newStartOnDate = startOnDate.ToString("M/dd/yyy");
                string newEndOnDate = endOnDate.ToString("M/dd/yyy");
                string newPeriodEnding = periodEndingDate.ToString("M/dd/yyy");

                DlkVariable.SetVariable(StartOnDateVar, newStartOnDate);
                DlkLogger.LogInfo("Start on date: [" + newStartOnDate + "] value set to Variable: [" + StartOnDateVar + "]");

                DlkVariable.SetVariable(EndOnDateVar, newEndOnDate);
                DlkLogger.LogInfo("End on date: [" + newEndOnDate + "] value set to Variable: [" + EndOnDateVar + "]");

                DlkVariable.SetVariable(PeriodEndingVar, newPeriodEnding);
                DlkLogger.LogInfo("Period ending: [" + newPeriodEnding + "] value set to Variable: [" + PeriodEndingVar + "]");

                DlkLogger.LogInfo("GetAdjustedDatePeriod() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetAdjustedDatePeriod() failed : " + e.Message, e);
            }
        }

        [Keyword("IfThenElse")]
        public static void IfThenElse(String VariableValue, String Operator, String ValueToTest, String IfGoToStep, String ElseGoToStep)
        {
            int iGoToStep = -1;
            VariableValue = VariableValue.ToLower();
            ValueToTest = ValueToTest.ToLower();
            double dValueToTest = -1, dVariableValue = -1;
            DateTime dtVariableValue = DateTime.MinValue, dtValueToTest = DateTime.MinValue;

            switch (Operator)
            {
                #region case =
                case "=":
                    if (String.Compare(VariableValue, ValueToTest) == 0)
                    {
                        DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] is equal to [" + ValueToTest + "].");
                        iGoToStep = Convert.ToInt32(IfGoToStep);
                    }
                    else
                    {
                        DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] is not equal to [" + ValueToTest + "].");
                        iGoToStep = Convert.ToInt32(ElseGoToStep);
                    }

                    DlkAcumenTouchStoneTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                #region case !=
                case "!=":
                    if (String.Compare(VariableValue, ValueToTest) != 0)
                    {
                        DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] is not equal to [" + ValueToTest + "].");
                        iGoToStep = Convert.ToInt32(IfGoToStep);
                    }
                    else
                    {
                        DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] is equal to [" + ValueToTest + "].");
                        iGoToStep = Convert.ToInt32(ElseGoToStep);
                    }

                    DlkAcumenTouchStoneTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                #region case >
                case ">":

                    if (double.TryParse(VariableValue, out dVariableValue) && (double.TryParse(ValueToTest, out dValueToTest))) // if numeric
                    {
                        // if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                        //  {
                        if (dVariableValue > dValueToTest)
                        {
                            DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] greater than [" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(IfGoToStep);
                        }
                        else
                        {
                            DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] not greater than [" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(ElseGoToStep);
                        }
                        //  }
                        //  else
                        //  {
                        //     throw new Exception("IfThenElse(): Cannot compare string input values using " + Operator + " operator.");
                        //  }
                    }
                    else if (DateTime.TryParse(VariableValue, out dtVariableValue) && DateTime.TryParse(ValueToTest, out dtValueToTest)) // if date
                    {
                        if (GetDateFormat(VariableValue) == GetDateFormat(ValueToTest))
                        {
                            if (DateTime.Compare(dtVariableValue, dtValueToTest) > 0)
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] greater than [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] not greater than [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                        else
                        {
                            DlkLogger.LogInfo("IfThenElse(): Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(ElseGoToStep);
                        }
                    }
                    else //if not numeric or date
                    {
                        throw new Exception("IfThenElse(): Cannot compare input values using " + Operator + " operator.");
                    }
                    DlkAcumenTouchStoneTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                #region case <
                case "<":
                    if (double.TryParse(VariableValue, out dVariableValue) && (double.TryParse(ValueToTest, out dValueToTest))) // if numeric
                    {
                        // if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                        // {
                        if (dVariableValue < dValueToTest)
                        {
                            DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] less than [" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(IfGoToStep);
                        }
                        else
                        {
                            DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] not less than [" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(ElseGoToStep);
                        }
                        // }
                        // else
                        //  {
                        //      throw new Exception("IfThenElse(): Cannot compare string input values using " + Operator + " operator.");
                        //  }
                    }
                    else if (DateTime.TryParse(VariableValue, out dtVariableValue) && DateTime.TryParse(ValueToTest, out dtValueToTest)) // if date
                    {
                        if (GetDateFormat(VariableValue) == GetDateFormat(ValueToTest))
                        {
                            if (DateTime.Compare(dtVariableValue, dtValueToTest) < 0)
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] less than [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] not less than [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                        else
                        {
                            DlkLogger.LogInfo("IfThenElse(): Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(ElseGoToStep);
                        }
                    }
                    else //if not numeric or date, throw an exception
                    {
                        throw new Exception("IfThenElse(): Cannot compare input values using " + Operator + " operator.");
                    }
                    DlkAcumenTouchStoneTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                #region case >=
                case ">=":
                    if (double.TryParse(VariableValue, out dVariableValue) && (double.TryParse(ValueToTest, out dValueToTest))) // if numeric
                    {
                        // if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                        //  {
                        if (dVariableValue >= dValueToTest)
                        {
                            DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] greater than or equal to [" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(IfGoToStep);
                        }
                        else
                        {
                            DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] not greater than or equal to [" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(ElseGoToStep);
                        }
                        //  }
                        //    else
                        //   {
                        //       throw new Exception("IfThenElse(): Cannot compare string input values using " + Operator + " operator.");
                        //   }
                    }
                    else if (DateTime.TryParse(VariableValue, out dtVariableValue) && DateTime.TryParse(ValueToTest, out dtValueToTest)) // if date
                    {
                        if (GetDateFormat(VariableValue) == GetDateFormat(ValueToTest))
                        {
                            if (DateTime.Compare(dtVariableValue, dtValueToTest) >= 0)
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] greater than or equal to [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] not greater than or equal to [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                        else
                        {
                            DlkLogger.LogInfo("IfThenElse(): Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(ElseGoToStep);
                        }
                    }
                    else //if not numeric or date, throw an exception
                    {
                        throw new Exception("IfThenElse(): Cannot compare input values using " + Operator + " operator.");
                    }
                    DlkAcumenTouchStoneTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                #region case <=
                case "<=":
                    if (double.TryParse(VariableValue, out dVariableValue) && (double.TryParse(ValueToTest, out dValueToTest))) // if numeric
                    {
                        if (!(VariableValue.StartsWith("0")) && !(ValueToTest.StartsWith("0")))
                        {
                            if (dVariableValue <= dValueToTest)
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] less than or equal to [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] not less than or equal to [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                        else
                        {
                            throw new Exception("IfThenElse(): Cannot compare string input values using " + Operator + " operator.");
                        }
                    }
                    else if (DateTime.TryParse(VariableValue, out dtVariableValue) && DateTime.TryParse(ValueToTest, out dtValueToTest)) // if date
                    {
                        if (GetDateFormat(VariableValue) == GetDateFormat(ValueToTest))
                        {
                            if (DateTime.Compare(dtVariableValue, dtValueToTest) <= 0)
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] less than or equal to [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(IfGoToStep);
                            }
                            else
                            {
                                DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] not less than or equal to [" + ValueToTest + "].");
                                iGoToStep = Convert.ToInt32(ElseGoToStep);
                            }
                        }
                        else
                        {
                            DlkLogger.LogInfo("IfThenElse(): Cannot compare dates with different date formats. [" + VariableValue + "|" + ValueToTest + "].");
                            iGoToStep = Convert.ToInt32(ElseGoToStep);
                        }
                    }
                    else //if not numeric or date, throw an exception
                    {
                        throw new Exception("IfThenElse(): Cannot compare input values using " + Operator + " operator.");
                    }
                    DlkAcumenTouchStoneTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                #region contains
                case "contains":
                    if (VariableValue.Contains(ValueToTest))
                    {
                        DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] contains [" + ValueToTest + "].");
                        iGoToStep = Convert.ToInt32(IfGoToStep);
                    }
                    else
                    {
                        DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] does not contain [" + ValueToTest + "].");
                        iGoToStep = Convert.ToInt32(ElseGoToStep);
                    }

                    DlkAcumenTouchStoneTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                #region not contains
                case "not contains":
                    if (!VariableValue.Contains(ValueToTest))
                    {
                        DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] does not contain [" + ValueToTest + "].");
                        iGoToStep = Convert.ToInt32(IfGoToStep);
                    }
                    else
                    {
                        DlkLogger.LogInfo("IfThenElse(): [" + VariableValue + "] contains [" + ValueToTest + "].");
                        iGoToStep = Convert.ToInt32(ElseGoToStep);
                    }

                    DlkAcumenTouchStoneTestExecute.mGoToStep = (iGoToStep - 1); // steps are zero based
                    DlkLogger.LogInfo("Successfully executed IfThenElse(). GoToStep:" + iGoToStep.ToString());
                    break;
                #endregion
                default:
                    throw new Exception("IfThenElse(): Unsupported operator " + Operator);
            }
        }

        private static int GetDateFormat(string inputDate)
        {
            string[] formats = {"MM/dd/yyyy",
                                   "M/d/yyyy",
                                   "MM/dd/yy",
                                   "dd/MM/yyyy",
                                   "d/M/yyyy",
                                   "dd/MM/yy",
                                   "yyyyMMdd",
                                   "yyyy-MM-dd",
                                   "yyMMdd",
                                   "yy-MM-dd",
                                   "yyMMddHHmmss",
                                   "yy-MM-dd HH:mm:ss",
                                   "yyyyMMddHHmmss",
                                   "yyyy-MM-dd HH:mm:ss",
                                   "yyyyMMddhhmmsstt",
                                   "yyyy-MM-dd hh:mm:ss tt",
                                   "MMM yy",
                                   "1-MMM-yy",
                                   "MMM-yy}",
                                   "MMddyy",
                                   "ddMMyy",
                                   "dddd, MMMM dd, yyyy",
                                   "M/dd",
                                   "dd-MMM",
                                   "MMM dd",
                                   "dd-MMM-yy",
                                   "MMM-dd",
                                   "MMM dd, yyyy",
                                   "dd-MMM-yyyy",
                                   "M/dd/yy h:mm tt",
                                   "M/dd/yy H:mm",
                                   "dddd",
                                   "MMMM",
                                   "yyyy-MM-dd hh:mm:ss tt"
                               };
            DateTime dateValue = DateTime.MinValue;

            for (int i = 0; i < formats.Length; i++)
                foreach (string dateStringFormat in formats)
                {
                    if (DateTime.TryParseExact(inputDate, dateStringFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                                                   out dateValue))
                        return i;
                }

            return -1;
        }

        public static void SaveSuccessMessage()
        {
            try
            {
                if (!DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@class='status-message success']")).Any())
                {
                    Thread.Sleep(1000);
                }
                IWebElement message = DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@class='status-message success']"));
                errorMessageContent = message.Text;
                errorMessageElement = message;
            }
            catch (Exception)
            {
                DlkLogger.LogInfo("SuccessMessage cannot be found, proceeding");
            }
        }

        private static void Login(String Url, String User, String Password, String Database)
        {
            DlkEnvironment.AutoDriver.Url = Url;
            int waitLoadingFinished = 300;
            DlkObjectStoreFileControlRecord osLogin = DlkDynamicObjectStoreHandler.GetControlRecord("Login", "Login");
            DlkObjectStoreFileControlRecord osWinAuth = DlkDynamicObjectStoreHandler.GetControlRecord("Login", "WindowsAuthentication");
            DlkButton loginBtn = new DlkButton(osLogin.mKey, osLogin.mSearchMethod, osLogin.mSearchParameters);
            DlkCheckBox loginBox = new DlkCheckBox(osWinAuth.mKey, osWinAuth.mSearchMethod, osWinAuth.mSearchParameters);
            for (int i = 0; i < waitLoadingFinished; i++)
            {
                if (!loginBtn.Exists())
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    DlkLogger.LogInfo(string.Format("Login button was found, page loading was finished in {0} seconds", i.ToString()));
                    break;
                }
            }
            if (User != "")
            {
                if (Database != "")
                {
                    DlkAcumenTouchStoneKeywordHandler.ExecuteKeyword("Login", "Database", "Select", new String[] { Database });
                }
                Thread.Sleep(2000);
                if (loginBox.Exists())
                {
                    DlkAcumenTouchStoneKeywordHandler.ExecuteKeyword("Login", "WindowsAuthentication", "Set", new String[] { "FALSE" });
                    Thread.Sleep(250);
                }
                DlkAcumenTouchStoneKeywordHandler.ExecuteKeyword("Login", "UserID", "Set", new String[] { User });
                DlkAcumenTouchStoneKeywordHandler.ExecuteKeyword("Login", "Password", "Set", new String[] { Password });


                DlkAcumenTouchStoneKeywordHandler.ExecuteKeyword("Login", "Login", "Click", new String[] { "" });
                Thread.Sleep(2000);
                DlkObjectStoreFileControlRecord osRec = DlkDynamicObjectStoreHandler.GetControlRecord("Main", "Navigation");
                DlkSideBar sideBar = new DlkSideBar(osRec.mKey, osRec.mSearchMethod, osRec.mSearchParameters);
                if (!sideBar.Exists(60))
                {
                    DlkLogger.LogWarning("Login() : Main page not loaded within timeout.");
                }
                else
                {
                    Thread.Sleep(8000);
                    DlkLogger.LogInfo("Login() passed.");
                }
            }
        }
    }
}
