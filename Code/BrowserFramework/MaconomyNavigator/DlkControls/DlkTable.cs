using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;


namespace MaconomyNavigatorLib.DlkControls
{
    [ControlType("Table")]
    public class DlkTable : DlkBaseControl
    {
        private Boolean IsInit = false;

        private ReadOnlyCollection<IWebElement> mColumnHeaders;

        public DlkTable(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTable(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                FindColumnHeaders();
                IsInit = true;
            }
        }

        [Keyword("AssignLineIndexToVariable", new String[] { "1|text|Expected Value|TRUE" })]
        public void AssignLineIndexToVariable(String ColumnName, String CellValue, String VariableName)
        {
            try
            {
                Initialize();
                //bool actual = false;
                bool comparison = false;
                int i = 1;
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);
                if (targetColumnNumber == -1)
                {
                    throw new Exception("Column '" + ColumnName + "' was not found.");
                }

                foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::tbody/tr")))
                {
                    IWebElement targetCell = elm.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));
                    //IWebElement targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                    //new DlkBaseControl("", targetInputControl).MouseOver();
                    //new DlkBaseControl("", targetInputControl).ClickUsingJavaScript();
                    //if (new DlkBaseControl("", targetInputControl).GetValue() == CellValue)
                    //{
                    //    DlkMaconomyNavigatorFunctionHandler.AssignToVariable(VariableName, i.ToString());
                    //    DlkLogger.LogInfo("AssignLineIndexToVariable() successfully executed.");
                    //    return;
                    //}
                    if (targetCell.GetAttribute("class").Contains("string") & !(targetCell.FindElements(By.XPath("./descendant::div[@class='open']")).Count > 0))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("Item", targetCell.FindElement(By.XPath("./descendant::input[2]")));
                        if (DlkString.ReplaceCarriageReturn(ctl.GetValue().ToLower(), "") == DlkString.ReplaceCarriageReturn(CellValue.ToLower(), ""))
                        { comparison = true; }
                    }
                    else
                    {
                        DlkBaseControl ctl = new DlkBaseControl("Item", targetCell.FindElement(By.XPath("./descendant::input[1]")));
                        if (DlkString.ReplaceCarriageReturn(ctl.GetAttributeValue("value").ToLower(), "") == DlkString.ReplaceCarriageReturn(CellValue.ToLower(), ""))
                        { comparison = true; }
                    }
                    //if (targetCell.GetAttribute("class").Contains("ng-scope") || targetCell.FindElements(By.XPath("./descendant::div[@class='open']")).Count > 0)
                    //{
                    //    DlkBaseControl ctl = new DlkBaseControl("Item", targetCell.FindElement(By.XPath("./descendant::input[1]")));
                    //    if (DlkString.ReplaceCarriageReturn(ctl.GetValue().ToLower(), "") == DlkString.ReplaceCarriageReturn(CellValue.ToLower(), ""))
                    //    { comparison = true; }
                    //}
                    //else
                    //{
                    //    DlkBaseControl ctl = new DlkBaseControl("Item", targetCell.FindElement(By.XPath("./descendant::input[2]")));
                    //    if (DlkString.ReplaceCarriageReturn(ctl.GetAttributeValue("value").ToLower(), "") == DlkString.ReplaceCarriageReturn(CellValue.ToLower(), ""))
                    //    { comparison = true; }
                    //}
                    if (comparison)
                    {
                        DlkFunctionHandler.AssignToVariable(VariableName, i.ToString());
                        DlkLogger.LogInfo("AssignLineIndexToVariable() successfully executed.");
                        return;
                    }
                    i++;
                }
                throw new Exception("Cell with '" + CellValue + "' was not found.");

            }
            catch (Exception e)
            {
                throw new Exception("AssignLineIndexToVariable() failed : " + e.Message, e);
            }
        }

        [Keyword("AssignValueToVariable", new String[] { "1|text|Expected Value|TRUE" })]
        public void AssignValueToVariable(String ColumnName, String LineIndex, String VariableName)
        {
            try
            {
                Initialize();

                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + " and @class='ng-scope']"));
        

                int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                if (targetColumnNumber == -1)
                {
                    throw new Exception("Column '" + ColumnName + "' was not found.");
                }

                IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));
                //firstCol.Click();
                new DlkBaseControl("FirstCol", firstCol).MouseOver();
                IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));
                IWebElement targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                DlkFunctionHandler.AssignToVariable(VariableName, new DlkBaseControl("Target", targetInputControl).GetValue());
                DlkLogger.LogInfo("AssignValueToVariable() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("AssignValueToVariable() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("SetCellValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetCellValue(String ColumnName, String LineIndex, String Value)
        {
            try
            {
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                // check
                if (targetColumnNumber == -1)
                {
                    throw new Exception("Column '" + ColumnName + "' was not found.");
                }
                IWebElement parentCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]/.."));
                DlkBaseControl parentCtl = new DlkBaseControl("ParentCell", parentCell);
                parentCtl.MouseOver();
                //parentCell.Click();
                parentCtl.Click(3);

                // need to re-initialize, DOM changes upon click
                Initialize();
                targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));

                if (targetCell.GetAttribute("class").Contains("boolean"))
                {
                    if ((!Convert.ToBoolean(Value) & targetCell.FindElements(By.XPath("./descendant::button[@class='fa fw fa-check ng-scope checked']")).Count > 0) || (!(targetCell.FindElements(By.XPath("./descendant::button[@class='fa fw fa-check ng-scope checked']")).Count > 0) & Convert.ToBoolean(Value) == true))
                    {
                        //get and click the keep checkmark inside the cell
                        if (targetCell.FindElements(By.XPath(".//i[1]")).Count > 0)
                        {
                            IWebElement targetLink1 = targetCell.FindElement(By.XPath(".//i[1]"));
                            DlkBaseControl targetLnk1 = new DlkBaseControl("TargetLink", targetLink1);
                            targetLnk1.MouseOver();
                            targetLnk1.ClickUsingJavaScript();
                        }
                        IWebElement targetLink = targetCell.FindElement(By.XPath(".//button[1]"));
                        DlkBaseControl targetLnk = new DlkBaseControl("TargetLink", targetLink);
                        targetLnk.MouseOver();
                        targetLnk.ClickUsingJavaScript();

                    }
                    /* Verify if checked */
                    DlkAssert.AssertEqual("KeepLine()", Convert.ToBoolean(Value), targetCell.FindElements(By.XPath("./descendant::button[@class='fa fw fa-check ng-scope checked']")).Count > 0);
                }
                else
                {
                    IWebElement targetInputControl = null;
                    if (DlkEnvironment.mBrowser.ToLower().Equals("safari"))
                    {
                        bool bOk = false;
                        int attempts = 0;
                        while (!bOk && attempts < 10)
                        {
                            try
                            {
                                attempts++;
                                DlkLogger.LogInfo("Attempting to set cell value... Attempt: " + attempts.ToString());
                                Initialize();
                                targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                                targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));
                                targetCell.Click();
                                Thread.Sleep(1000);

                                // need to re-initialize, DOM changes upon click
                                Initialize();
                                targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                                targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));
                                targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                                targetInputControl.Clear();
                                bOk = true;
                            }
                            catch
                            {
                                /* if clear fails on first try, the element is no longer attached to DOM. Try again */
                            }
                        }
                    }
                    targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                    DlkBaseControl targetCtl = new DlkBaseControl("TargetControl", targetInputControl);
                    try
                    {
                        targetInputControl.Clear();
                    }
                    catch
                    {
                        targetInputControl.Clear();
                    }

                    //targetCell.Click();
                    targetInputControl.SendKeys(Value);

                    /* checking */
                    if (targetCtl.GetValue() != Value)
                    {
                        targetInputControl.Clear();
                        targetInputControl.SendKeys(Value);
                    }

                    /* Insert pause so value won't become blank */
                    Thread.Sleep(1000);
                    //targetCell.Click();
                }
                DlkLogger.LogInfo("SetCellValue() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SetCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("SetCellValueAndEnter", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetCellValueAndEnter(String ColumnName, String LineIndex, String Value)
        {
            try
            {
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                // check
                if (targetColumnNumber == -1)
                {
                    throw new Exception("Column '" + ColumnName + "' was not found.");
                }
                IWebElement parentCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]/.."));
                DlkBaseControl parentCtl = new DlkBaseControl("ParentCell", parentCell);
                parentCtl.MouseOver();
                //parentCell.Click();
                parentCtl.Click(3);

                // need to re-initialize, DOM changes upon click
                Initialize();
                targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));

                if (targetCell.GetAttribute("class").Contains("boolean"))
                {
                    if ((!Convert.ToBoolean(Value) & targetCell.FindElements(By.XPath("./descendant::button[@class='fa fw fa-check ng-scope checked']")).Count > 0) || (!(targetCell.FindElements(By.XPath("./descendant::button[@class='fa fw fa-check ng-scope checked']")).Count > 0) & Convert.ToBoolean(Value) == true))
                    {
                        //get and click the keep checkmark inside the cell
                        if (targetCell.FindElements(By.XPath("./div[1]/i[1]")).Count > 0)
                        {
                            IWebElement targetLink1 = targetCell.FindElement(By.XPath("./div[1]/i[1]"));
                            DlkBaseControl targetLnk1 = new DlkBaseControl("TargetLink", targetLink1);
                            targetLnk1.MouseOver();
                            targetLnk1.ClickUsingJavaScript();
                        }
                        IWebElement targetLink = targetCell.FindElement(By.XPath("./div[1]/button[1]"));
                        DlkBaseControl targetLnk = new DlkBaseControl("TargetLink", targetLink);
                        targetLnk.MouseOver();
                        targetLnk.ClickUsingJavaScript();

                    }
                    /* Verify if checked */
                    DlkAssert.AssertEqual("KeepLine()", Convert.ToBoolean(Value), targetCell.FindElements(By.XPath("./descendant::button[@class='fa fw fa-check ng-scope checked']")).Count > 0);
                }
                else
                {
                    IWebElement targetInputControl = null;
                    if (DlkEnvironment.mBrowser.ToLower().Equals("safari"))
                    {
                        bool bOk = false;
                        int attempts = 0;
                        while (!bOk && attempts < 10)
                        {
                            try
                            {
                                attempts++;
                                DlkLogger.LogInfo("Attempting to set cell value... Attempt: " + attempts.ToString());
                                Initialize();
                                targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                                targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));
                                targetCell.Click();
                                Thread.Sleep(1000);

                                // need to re-initialize, DOM changes upon click
                                Initialize();
                                targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                                targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));
                                targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                                targetInputControl.Clear();
                                bOk = true;
                            }
                            catch
                            {
                                /* if clear fails on first try, the element is no longer attached to DOM. Try again */
                            }
                        }
                    }
                    targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                    DlkBaseControl targetCtl = new DlkBaseControl("TargetControl", targetInputControl);
                    targetInputControl.Clear();
                    targetCell.Click();
                    targetInputControl.SendKeys(Value);

                    /* checking */
                    if (targetCtl.GetValue() != Value)
                    {
                        targetInputControl.Clear();
                        targetInputControl.SendKeys(Value);
                    }

                    /* Insert pause so value won't become blank */
                    Thread.Sleep(1000);
                    targetInputControl.SendKeys(Keys.Return);
                }
                DlkLogger.LogInfo("SetCellValueAndEnter() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SetCellValueAndEnter() failed : " + e.Message, e);
            }
        }

        [Keyword("SendBackspaceToCell", new String[] { "1|text|Expected Value|TRUE" })]
        public void SendBackspaceToCell(String ColumnName, String LineIndex, String NumberOfTimes)
        {
            try
            {
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);
                int counter = 0;
                int maxLoop = Convert.ToInt32(NumberOfTimes);
                // check
                if (targetColumnNumber == -1)
                {
                    throw new Exception("Column '" + ColumnName + "' was not found.");
                }
                IWebElement parentCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]/.."));
                DlkBaseControl parentCtl = new DlkBaseControl("ParentCell", parentCell);
                parentCtl.MouseOver();
                //parentCell.Click();
                parentCtl.Click(3);

                // need to re-initialize, DOM changes upon click
                Initialize();
                targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));


                IWebElement targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                while (counter < maxLoop)
                {
                    targetInputControl.SendKeys(Keys.Backspace);
                    counter++;
                }
                DlkLogger.LogInfo("SendBackspaceToCell() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SendBackspaceToCell() failed : " + e.Message, e);
            }
        }

        [Keyword("SetThenSelectCellValueFromList", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetThenSelectCellValueFromList(String ColumnName, String LineIndex, String Value)
        {
            try
            {
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                // check
                if (targetColumnNumber == -1)
                {
                    throw new Exception("Column '" + ColumnName + "' was not found.");
                }
                IWebElement parentCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]/.."));
                DlkBaseControl parentCtl = new DlkBaseControl("ParentCell", parentCell);
                parentCtl.MouseOver();
                //parentCell.Click();
                parentCtl.Click(3);

                // need to re-initialize, DOM changes upon click
                Initialize();
                targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));
                IWebElement targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                IWebElement resultList = null;
                if (DlkEnvironment.mBrowser.ToLower().Equals("safari"))
                {
                    bool bOk = false;
                    int attempts = 0;
                    while (!bOk && attempts < 10)
                    {
                        try
                        {
                            attempts++;
                            DlkLogger.LogInfo("Attempting to set cell value... Attempt: " + attempts.ToString());
                            Initialize();
                            targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                            targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));
                            targetCell.Click();
                            Thread.Sleep(1000);

                            // need to re-initialize, DOM changes upon click
                            Initialize();
                            targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                            targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));
                            targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                            targetInputControl.Clear();
                            bOk = true;
                        }
                        catch
                        {
                            /* if clear fails on first try, the element is no longer attached to DOM. Try again */
                        }
                    }
                }

                targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                DlkBaseControl targetCtl = new DlkBaseControl("TargetControl", targetInputControl);
                try
                {
                    targetInputControl.Clear();
                }
                catch
                {
                    targetInputControl.Clear();
                }

                targetInputControl.SendKeys(Value);

                resultList = targetCell.FindElements(By.XPath("./descendant::*[@class='result-wrapper']")).First();

                int counter = 0;
                while (resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]")).Count == 0 && counter < 10)
                {
                    DlkLogger.LogInfo("Waiting for items... " + ++counter + "s elapsed");
                    Thread.Sleep(1000);
                }

                IWebElement targetHit = null;
                //int count = 0;

                //targetInputControl.SendKeys(Keys.Backspace);
                //Thread.Sleep(2000);
                //targetInputControl.SendKeys(Value[Value.Length - 1].ToString());

                //while (resultList.FindElements(By.XPath("./descendant::p[1]")).Count == 0 && count < 10)
                //{
                //    Thread.Sleep(1000);
                //    DlkLogger.LogInfo("Waiting for result list... " + ++count + "s");
                //}

                if (resultList.FindElements(By.XPath("./descendant::p[1]")).Count == 0)
                {
                    throw new Exception("Value: " + Value + " not found in result list");
                }
                targetHit = resultList.FindElement(By.XPath("./descendant::p[1]"));
                targetHit.Click();
                //targetInputControl.SendKeys(Keys.Return);
                DlkLogger.LogInfo("SetThenSelectCellValueFromList() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SetThenSelectCellValueFromList() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectCellValueFromList", new String[] { "1|text|Value|SampleValue" })]
        public void SelectCellValueFromList(String ColumnName, String LineIndex, String Value)
        {
            try
            {
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                // check
                if (targetColumnNumber == -1)
                {
                    throw new Exception("Column '" + ColumnName + "' was not found.");
                }

                IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));
                //firstCol.Click();
                new DlkBaseControl("FirstCol", firstCol).MouseOver();
                IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));

                // have a check here if what kind of list
                if (targetCell.FindElements(By.XPath("./descendant::button[1]")).Count > 0)
                {
                    DlkLogger.LogInfo("Control is a dropdown list");
                    IWebElement mDropDownButton = targetCell.FindElement(By.XPath("./descendant::button[1]"));
                    mDropDownButton.Click();
                    IWebElement target = targetCell.FindElement(By.XPath("./descendant::a/span[translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')='"
                        + Value.ToLower() + "']/.."));
                    target.Click();
                    //mDropDownButton.SendKeys(Keys.Return);
                }
                else
                {
                    DlkLogger.LogInfo("Control is a combobox");
                    IWebElement dropdown = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                    IWebElement button = null;
                    int attempts = 0;
                    while ((targetCell.FindElements(By.XPath("./descendant::*[@class='result-wrapper']")).Count <= 0
                        || !targetCell.FindElements(By.XPath("./descendant::*[@class='result-wrapper']"))[0].Displayed)
                        && attempts < 10)
                    {
                        DlkLogger.LogInfo("Attempting to display list attempt " + ++attempts + " ...");
                        button = button = targetCell.FindElement(By.XPath("./descendant::i[@class='caret']"));
                        button.Click();
                    }

                    if (targetCell.FindElements(By.XPath("./descendant::*[@class='result-wrapper']")).Count > 0)
                    {
                        IWebElement resultList = targetCell.FindElements(By.XPath("./descendant::*[@class='result-wrapper']")).First();

                        int counter = 0;
                        while (resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]")).Count == 0 && counter < 10)
                        {
                            DlkLogger.LogInfo("Waiting for items... " + ++counter + "s elapsed");
                            Thread.Sleep(1000);
                        }
                        bool bFound = false;
                        foreach (IWebElement elm in resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]")))
                        {
                            string actualItem = "";
                            if (dropdown.GetAttribute("class").Contains("enum"))
                            {
                                actualItem += DlkString.ReplaceCarriageReturn(elm.Text, "");
                            }
                            else
                            {
                                foreach (IWebElement sub in elm.FindElements(By.TagName("p")))
                                {
                                    actualItem += DlkString.ReplaceCarriageReturn(new DlkBaseControl("sub", sub).GetValue(), "");
                                }
                            }
                            if (actualItem == DlkString.ReplaceCarriageReturn(Value, ""))
                            {
                                new DlkBaseControl("Item", elm).Click();
                                bFound = true;
                                IWebElement targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                                //targetInputControl.SendKeys(Keys.Return);
                                break;
                            }
                        }
                        if (!bFound)
                        {
                            throw new Exception("Item " + Value + " not found on list");
                        }
                    }
                }
                DlkLogger.LogInfo("Successfully executed SelectCellValueFromList()");
            }
            catch (Exception e)
            {
                throw new Exception("SelectCellValueFromList() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectCellValueFromListContains", new String[] { "1|text|Value|SampleValue" })]
        public void SelectCellValueFromListContains(String ColumnName, String LineIndex, String Value)
        {
            try
            {
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                // check
                if (targetColumnNumber == -1)
                {
                    throw new Exception("Column '" + ColumnName + "' was not found.");
                }

                IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));
                //firstCol.Click();
                new DlkBaseControl("FirstCol", firstCol).MouseOver();
                IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));

                // have a check here if what kind of list
                if (targetCell.FindElements(By.XPath("./descendant::button[1]")).Count > 0)
                {
                    DlkLogger.LogInfo("Control is a dropdown list");
                    IWebElement mDropDownButton = targetCell.FindElement(By.XPath("./descendant::button[1]"));
                    mDropDownButton.Click();
                    IWebElement target = targetCell.FindElement(By.XPath("./descendant::a/span[contains(text(),'" + Value.ToLower() + "')]/.."));
                    target.Click();
                    //mDropDownButton.SendKeys(Keys.Return);
                }
                else
                {
                    DlkLogger.LogInfo("Control is a combobox");
                    IWebElement dropdown = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                    IWebElement button = null;
                    int attempts = 0;
                    while ((targetCell.FindElements(By.XPath("./descendant::*[@class='result-wrapper']")).Count <= 0
                        || !targetCell.FindElements(By.XPath("./descendant::*[@class='result-wrapper']"))[0].Displayed)
                        && attempts < 10)
                    {
                        DlkLogger.LogInfo("Attempting to display list attempt " + ++attempts + " ...");
                        button = button = targetCell.FindElement(By.XPath("./descendant::i[@class='caret']"));
                        button.Click();
                    }

                    if (targetCell.FindElements(By.XPath("./descendant::*[@class='result-wrapper']")).Count > 0)
                    {
                        IWebElement resultList = targetCell.FindElements(By.XPath("./descendant::*[@class='result-wrapper']")).First();

                        int counter = 0;
                        while (resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]")).Count == 0 && counter < 10)
                        {
                            DlkLogger.LogInfo("Waiting for items... " + ++counter + "s elapsed");
                            Thread.Sleep(1000);
                        }
                        bool bFound = false;
                        foreach (IWebElement elm in resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]")))
                        {
                            string actualItem = "";
                            if (dropdown.GetAttribute("class").Contains("enum"))
                            {
                                actualItem += DlkString.ReplaceCarriageReturn(elm.Text, "");
                            }
                            else
                            {
                                foreach (IWebElement sub in elm.FindElements(By.TagName("p")))
                                {
                                    actualItem += DlkString.ReplaceCarriageReturn(new DlkBaseControl("sub", sub).GetValue(), "");
                                }
                            }
                            if (actualItem.Contains(DlkString.ReplaceCarriageReturn(Value, "")))
                            {
                                new DlkBaseControl("Item", elm).Click();
                                bFound = true;
                                IWebElement targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                                //targetInputControl.SendKeys(Keys.Return);
                                break;
                            }
                        }
                        if (!bFound)
                        {
                            throw new Exception("Item " + Value + " not found on list");
                        }
                    }
                }
                DlkLogger.LogInfo("Successfully executed SelectCellValueFromListContains()");
            }
            catch (Exception e)
            {
                throw new Exception("SelectCellValueFromListContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCellValue(String ColumnName, String LineIndex, String Value = "")
        {
            try
            {
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                // check
                if (targetColumnNumber == -1)
                {
                    //try checking if column header is image
                    targetColumnNumber = GetColNumberFromImageName(ColumnName);
                    if (targetColumnNumber == -1)
                    {
                        throw new Exception("Column '" + ColumnName + "' was not found.");
                    }
                }

                IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));
                //firstCol.Click();
                new DlkBaseControl("FirstCol", firstCol).MouseOver();
                IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));

                if (targetCell.GetAttribute("class").Contains("boolean"))
                {
                    DlkAssert.AssertEqual("VerifyCellValue()", Convert.ToBoolean(Value), targetCell.FindElements(By.XPath(".//*[contains(@class, 'checked')]")).Count > 0);
                }
                else if (targetCell.FindElements(By.XPath(".//*[1]")).Count > 0)
                {
                    IWebElement targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                    if (targetCell.FindElements(By.XPath("./descendant::input[2][not(contains(@class,'ng-hide'))]")).Count > 0)
                    {
                        targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[2]"));
                    }
                    DlkAssert.AssertEqual("VerifyCellValue()", Value, new DlkBaseControl("Target", targetInputControl).GetValue());
                }
                else
                {
                    DlkAssert.AssertEqual("VerifyCellValue()", Value, new DlkBaseControl("Target", targetCell).GetValue());

                }
                DlkLogger.LogInfo("VerifyCellValue() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTextboxIsVisible", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyTextboxIsVisible(String ColumnName, String LineIndex, String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool bfound = false;
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                // check
                if (targetColumnNumber == -1)
                {
                    //try checking if column header is image
                    targetColumnNumber = GetColNumberFromImageName(ColumnName);
                    if (targetColumnNumber == -1)
                    {
                        throw new Exception("Column '" + ColumnName + "' was not found.");
                    }
                }


                IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));

                if (targetCell.GetAttribute("class").Contains("boolean"))
                {
                    // do nothing
                }
                else if (targetCell.FindElements(By.XPath(".//*[1]")).Count > 0)
                {
                    IWebElement targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[1]"));

                    if (targetInputControl.GetAttribute("class").Contains("ng-valid"))
                    {
                        bfound = true;
                    }

                }
                DlkAssert.AssertEqual("VerifyTextboxIsVisible()", bool.Parse(TrueOrFalse), bfound);
                DlkLogger.LogInfo("VerifyTextboxIsVisible() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextboxIsVisible() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDropDownListCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyDropDownListCount(String ColumnName, String LineIndex, String Count)
        {
            try
            {
                Initialize();
                int actual = 0;
                bool bIsOpenedHere = false;
                IWebElement targetRow = mElement.FindElement(By.XPath(".//tbody/tr[" + LineIndex + "]"));
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                // check
                if (targetColumnNumber == -1)
                {
                    //try checking if column header is image
                    targetColumnNumber = GetColNumberFromImageName(ColumnName);
                    if (targetColumnNumber == -1)
                    {
                        throw new Exception("Column '" + ColumnName + "' was not found.");
                    }
                }


                IWebElement targetCell = targetRow.FindElement(By.XPath(".//td[" + targetColumnNumber + "]"));

       
                if (targetCell.FindElements(By.XPath(".//a")).Count > 0)

                {
                    IWebElement mDropDownButton = targetCell.FindElement(By.XPath(".//a"));
                    DlkLogger.LogInfo("Opening dropdown");
                    mDropDownButton.Click();
                    bIsOpenedHere = true;
                    Thread.Sleep(1000);
                    actual = targetCell.FindElements(By.XPath(".//p")).Count;


                    if (bIsOpenedHere)
                    {
                        DlkLogger.LogInfo("Closing dropdown");
                        mDropDownButton.Click();
                    }
                }

                else

                {
                    throw new Exception("DropDownList not found");
                }
                  
                DlkAssert.AssertEqual("VerifyDropDownListCount()", int.Parse(Count), actual);
                DlkLogger.LogInfo("VerifyDropDownListCount() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDropDownListCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellButton", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCellButton(String ColumnName, String LineIndex, String ButtonName, String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool bfound = false;

                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                IWebElement targetCell = null;
                if (ColumnName.ToLower().Equals("action"))
                {
                    targetCell = targetRow.FindElement(By.XPath("./descendant::td[@class='nobackground']/.."));
                }
                else
                {
                    int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                    // check
                    if (targetColumnNumber == -1)
                    {
                        throw new Exception("Column '" + ColumnName + "' was not found.");
                    }
                    targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]/.."));

                }

                switch (ButtonName.ToLower())
                {
                    case "calendar":
                        if(targetCell.FindElements(By.XPath("./descendant::img[contains(@src,'calendar')]")).Count > 0)
                        {
                            bfound = true;
                        }
                        break;
                    case "find":
                        if(targetCell.FindElements(By.XPath("./descendant::i[contains(@class,'lookup')]/..")).Count > 0)
                        {
                            bfound = true;
                        }
                       break;
                    case "favorite":
                        if (targetCell.FindElements(By.XPath("./descendant::i[contains(@class,'fa-star')]/..")).Count > 0)
                        {
                            bfound = true;
                        }                     
                        break;
                    case "edit":
                        if (targetCell.FindElements(By.XPath("./descendant::a[contains(@title,'Edit')]")).Count > 0)
                        {
                            bfound = true;
                        }
                        break;
                    case "submit":
                        if (targetCell.FindElements(By.XPath("./descendant::a[contains(@title,'Submit')]/..")).Count > 0)
                        {
                            bfound = true;
                        }
                        break;
                    case "delete":
                        if (targetCell.FindElements(By.XPath("./descendant::a[contains(@title,'Delete')]/..")).Count > 0)
                        {
                            bfound = true;
                        }
                        break;
                    case "reopen":
                        if (targetCell.FindElements(By.XPath("./descendant::a[contains(@title,'Reopen')]/..")).Count > 0)
                        {
                            bfound = true;
                        }
                        break;
                    default:;
                        bfound = false;
                        DlkLogger.LogInfo(ButtonName + " does not exist");
                        break;
                      

                }
                DlkAssert.AssertEqual("VerifyCellButton()", bool.Parse(TrueOrFalse), bfound);
                DlkLogger.LogInfo("VerifyCellButton() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellButton() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellValueWithColumnIndex", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCellValueWithColumnIndex(String ColumnNumber, String LineIndex, String Value = "")
        {
            try
            {
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));

                IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));
                //firstCol.Click();
                new DlkBaseControl("FirstCol", firstCol).MouseOver();
                IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + ColumnNumber + "]"));

                if (targetCell.GetAttribute("class").Contains("boolean"))
                {
                    DlkAssert.AssertEqual("VerifyCellValueWithColumnIndex()", Convert.ToBoolean(Value), targetCell.FindElements(By.XPath(".//*[contains(@class,'checked')]")).Count > 0);
                }
                else if (targetCell.FindElements(By.XPath(".//*[1]")).Count > 0)
                {
                    IWebElement targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                    if (targetCell.FindElements(By.XPath("./descendant::input[2][not(contains(@class,'ng-hide'))]")).Count > 0)
                    {
                        targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[2]"));
                    }
                    DlkAssert.AssertEqual("VerifyCellValueWithColumnIndex()", Value, new DlkBaseControl("Target", targetInputControl).GetValue());
                }
                else
                {
                    DlkAssert.AssertEqual("VerifyCellValueWithColumnIndex()", Value, new DlkBaseControl("Target", targetCell).GetValue());
                }
                DlkLogger.LogInfo("VerifyCellValueWithColumnIndex() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellValueWithColumnIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyLineCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyLineCount(String LineCount)
        {
            try
            {
                Initialize();
                int actRowCount = mElement.FindElements(By.XPath("./descendant::tbody/tr")).Count;
                DlkAssert.AssertEqual("VerifyLineCount()", int.Parse(LineCount), actRowCount);
                DlkLogger.LogInfo("VerifyLineCount() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLineCount() failed : " + e.Message, e);
            }
        }

        public void VerifyValueInResultsList(String Value, String ExpectedValue)
        {
            try
            {
                Initialize();
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValueInResultsList() failed : " + e.Message, e);
            }
        }

        [Keyword("ShowDropdownList", new String[] { "1|text|Expected Value|TRUE" })]
        public void ShowDropdownList(String ColumnName, String LineIndex)
        {
            try
            {
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                // check
                if (targetColumnNumber == -1)
                {
                    throw new Exception("Column '" + ColumnName + "' was not found.");
                }
                IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));
                //firstCol.Click();
                new DlkBaseControl("FirstCol", firstCol).MouseOver();
                IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));
                IWebElement button = targetCell.FindElement(By.XPath("./descendant::a[1]"));
                //IWebElement button = targetCell.FindElement(By.XPath("./descendant::a[@class='ng-isolate-scope']"));
                int attempts = 0;
                do
                {
                    DlkLogger.LogInfo("Attempting to display list attempt " + ++attempts + " ...");
                    button.Click();
                }
                while (targetCell.FindElements(By.XPath("./descendant::*[@class='result-wrapper']")).Count <= 0 && attempts < 10);
                DlkLogger.LogInfo("ShowDropdownList() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ShowDropdownList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemInDropDownList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemInDropDownList(String ColumnName, String LineIndex, String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                // check
                if (targetColumnNumber == -1)
                {
                    throw new Exception("Column '" + ColumnName + "' was not found.");
                }
                IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));
                //firstCol.Click();
                new DlkBaseControl("FirstCol", firstCol).MouseOver();
                IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));

                if (targetCell.FindElements(By.XPath("./descendant::button[1]")).Count > 0)
                {
                    DlkLogger.LogInfo("Control is a dropdown list");
                    IWebElement mDropDownButton = targetCell.FindElement(By.XPath("./descendant::button[1]"));
                    mDropDownButton.Click();
                    DlkAssert.AssertEqual("VerifyItemInDropDownList()", bool.Parse(TrueOrFalse), targetCell.FindElements(By.XPath("./descendant::a/span[translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')='"
                        + Item.ToLower() + "']/..")).Count > 0);
                    mDropDownButton.Click();
                    DlkLogger.LogInfo("VerifyItemInDropDownList() passed");
                }
                else
                {
                    DlkLogger.LogInfo("Control is a combobox");
                    int attempts = 0;
                    IWebElement dropdown = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                    IWebElement button = null;
                    bool listOpenedHere = false;

                    while ((targetCell.FindElements(By.XPath("./descendant::*[@class='result-wrapper']")).Count <= 0
                        || !targetCell.FindElements(By.XPath("./descendant::*[@class='result-wrapper']"))[0].Displayed)
                        && attempts < 10)
                    {
                        DlkLogger.LogInfo("Attempting to display list attempt " + ++attempts + " ...");
                        listOpenedHere = true;
                        button = targetCell.FindElement(By.XPath(".//i[@class='caret']"));
                        button.Click();
                    }

                    if (targetCell.FindElements(By.XPath("./descendant::*[@class='result-wrapper']")).Count > 0)
                    {
                        IWebElement resultList = targetCell.FindElements(By.XPath("./descendant::*[@class='result-wrapper']")).First();
                        bool actual = false;
                        bool expected = bool.Parse(TrueOrFalse);

                        int counter = 0;
                        while (resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]")).Count == 0 && counter < 10)
                        {
                            DlkLogger.LogInfo("Waiting for items... " + ++counter + "s elapsed");
                            Thread.Sleep(1000);
                        }

                        foreach (IWebElement elm in resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]")))
                        {
                            string actualItem = "";
                            if (dropdown.GetAttribute("class").Contains("enum"))
                            {
                                actualItem += DlkString.ReplaceCarriageReturn(elm.Text, "");
                            }
                            else
                            {
                                foreach (IWebElement sub in elm.FindElements(By.TagName("p")))
                                {
                                    actualItem += DlkString.ReplaceCarriageReturn(new DlkBaseControl("sub", sub).GetValue(), "");
                                }
                            }
                            if (actualItem == DlkString.ReplaceCarriageReturn(Item, ""))
                            {
                                actual = true;
                                break;
                            }
                        }
                        DlkAssert.AssertEqual("VerifyItemInDropDownList()", expected, actual);
                        if (listOpenedHere) // close if list was opened in this routine
                        {
                            //Maconomy 1.2 : Pause inserted here to let the busy spinner disappear before proceeding with the click, otherwise error will be encountered.
                            Thread.Sleep(500);
                            button.Click();
                        }
                        DlkLogger.LogInfo("VerifyItemInDropDownList() passed");
                    }
                    else
                    {
                        throw new Exception("Failed to display dropdown list after " + attempts + " attempts");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemInDropDownList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDropDownList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyDropDownList(String ColumnName, String LineIndex, String Items)
        {
            try
            {
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                // check
                if (targetColumnNumber == -1)
                {
                    throw new Exception("Column '" + ColumnName + "' was not found.");
                }
                IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));
                //firstCol.Click();
                new DlkBaseControl("FirstCol", firstCol).MouseOver();
                IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));

                if (targetCell.FindElements(By.XPath("./descendant::button[1]")).Count > 0)
                {
                    DlkLogger.LogInfo("Control is a dropdown list");
                    IWebElement mDropDownButton = targetCell.FindElement(By.XPath("./descendant::button[1]"));
                    mDropDownButton.Click();
                    string actual = "";
                    foreach (IWebElement elm in targetCell.FindElements(By.XPath("./descendant::a")))
                    {
                        actual += new DlkBaseControl("Text", elm.FindElement(By.XPath("./span[1]"))).GetValue().ToUpper() + "~";
                    }
                    actual = actual.Trim('~');
                    DlkAssert.AssertEqual("VerifyDropDownList()", DlkString.ReplaceCarriageReturn(Items.ToUpper(), ""), DlkString.ReplaceCarriageReturn(actual, ""));
                    mDropDownButton.Click();
                    DlkLogger.LogInfo("VerifyDropDownList() passed");
                }
                else
                {
                    DlkLogger.LogInfo("Control is a combobox");
                    IWebElement dropdown = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                    IWebElement button = targetCell.FindElement(By.XPath("./descendant::a[1]"));
                    //IWebElement button = targetCell.FindElement(By.XPath(".//a[@class='dropdown-toggle']"));
                    int attempts = 0;
                    bool listOpenedHere = false;

                    while ((targetCell.FindElements(By.XPath("./descendant::*[@class='result-wrapper']")).Count <= 0
                        || !targetCell.FindElements(By.XPath("./descendant::*[@class='result-wrapper']"))[0].Displayed)
                        && attempts < 10)
                    {
                        DlkLogger.LogInfo("Attempting to display list attempt " + ++attempts + " ...");
                        listOpenedHere = true;
                        button.Click();
                    }

                    if (targetCell.FindElements(By.XPath("./descendant::*[@class='result-wrapper']")).Count > 0)
                    {
                        IWebElement resultList = targetCell.FindElements(By.XPath("./descendant::*[@class='result-wrapper']")).First();
                        //Thread.Sleep(3000);
                        string[] expected = Items.Split('~');
                        ReadOnlyCollection<IWebElement> actual = resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]"));
                        int counter = 0;
                        while (actual.Count == 0 && counter < 10)
                        {
                            actual = resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]"));
                            DlkLogger.LogInfo("Waiting for items... " + ++counter + "s elapsed");
                            Thread.Sleep(1000);
                        }

                        int expectedCount = expected.Count();
                        int actualCount = actual.Count;
                        // check for count equality
                        if (actualCount != expectedCount)
                        {
                            throw new Exception("Actual count: " + actualCount + " did not match Expected count: " + expectedCount);
                        }

                        for (int i = 0; i < expected.Count(); i++)
                        {
                            string actualItem = "";
                            if (dropdown.GetAttribute("class").Contains("enum"))
                            {

                                DlkBaseControl ctl = new DlkBaseControl("ActualControl", actual[i]);
                                if (ctl.GetValue().Equals(" "))
                                {
                                    //do nothing
                                }
                                else
                                {
                                    DlkAssert.AssertEqual("VerifyDropDownList()", DlkString.ReplaceCarriageReturn(expected[i], ""), ctl.GetValue());
                                }
                            }
                            else
                            {
                                foreach (IWebElement sub in actual[i].FindElements(By.TagName("p")))
                                {
                                    actualItem += DlkString.ReplaceCarriageReturn(new DlkBaseControl("sub", sub).GetValue(), "");
                                }
                                DlkBaseControl ctl = new DlkBaseControl("ActualControl", actual[i]);
                                DlkAssert.AssertEqual("VerifyDropDownList()", DlkString.ReplaceCarriageReturn(expected[i], ""), actualItem);
                            }
                        }
                        if (listOpenedHere) // close if list was opened in this routine
                        {
                            button.Click();
                        }
                        DlkLogger.LogInfo("VerifyDropDownList() passed");
                    }
                    else
                    {
                        throw new Exception("Failed to display dropdown list after " + attempts + " attempts");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDropDownList() failed : " + e.Message, e);
            }
        }

        [Keyword("ShowFindDialog", new String[] { "1|text|Expected Value|TRUE" })]
        public void ShowFindDialog(String ColumnName, String LineIndex)
        {
            try
            {
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                // check
                if (targetColumnNumber == -1)
                {
                    throw new Exception("Column '" + ColumnName + "' was not found.");
                }
                IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));
                IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));
                firstCol.Click();
                IWebElement button = targetCell.FindElement(By.XPath("./descendant::a[@class='ng-scope']"));
                button.Click();
                DlkLogger.LogInfo("ShowFindDialog() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ShowFindDialog() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectLine", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectLine(String LineIndex)
        {
            try
            {
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + " and @class='ng-scope']"));
                IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));
                firstCol.Click();
                //try
                //{
                //    //for find job screen, click select
                //    mElement=mElement.FindElement(By.XPath("./ancestor::div[@class='modal-dialog modal-lg']//*[contains(@class,'footer')]/button[.='Select']"));
                //    if (mElement.Displayed)
                //    {
                //        mElement.Click();
                //    }
                //}
                //catch
                //{
                //}
                DlkLogger.LogInfo("SelectLine() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectLine() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// This keyword looks at the selected row and checks if the line options are existing
        /// and compares the expected results to the actual values of each line option.
        /// </summary>
        /// <param name="LineIndex"></param>
        /// <param name="ExpectedValues"></param>
        [Keyword("VerifyLineButtons", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyLineButtons(String LineIndex, String ExpectedValues)
        {
            try
            {
                Initialize();
                //find the row that the QE needs to check the line options
                IWebElement targetRow = mElement.FindElement(By.XPath(string.Format("./descendant::tbody/tr[{0}]/td[1]", LineIndex)));
                //highlight the row by clicking on it
                DlkLogger.LogInfo(string.Format("Attempting to click row number {0}", LineIndex));
                targetRow.Click();
                DlkLogger.LogInfo(string.Format("Row number {0} was clicked", LineIndex));
                Thread.Sleep(2000);//to give time for the buttons to load.

                //do: check if the line options exist
                ReadOnlyCollection<IWebElement> lineOptions = targetRow.FindElements(By.XPath("../descendant::li"));
                if (lineOptions.Count > 0)
                {
                    DlkLogger.LogInfo(string.Format("VerifyIfLineOptionsExist() successfully executed. {0} line option(s) found."
                        , lineOptions.Count.ToString()));
                }
                else
                {
                    throw new Exception(string.Format("Line option(s) on line number {0} were not found", LineIndex));
                }
                //if the line options exist, do: comparison of actual and expected
                string[] expected = ExpectedValues.Split('~');
                int loopCounter = 0;
                //for each <li> element, go to its corresponding <a> tags to compare the actual values with the expected values
                foreach (var item in lineOptions)
                {
                    string lineOptionTitleAttributeValue = item.FindElement(By.XPath("./a")).GetAttribute("Title");
                    DlkLogger.LogInfo(string.Format("Comparing [{0}] and [{1}]", expected[loopCounter], lineOptionTitleAttributeValue));
                    DlkAssert.AssertEqual("VerifyIfLineOptionsExist", expected[loopCounter], lineOptionTitleAttributeValue);
                    DlkLogger.LogInfo(string.Format("[{0}] and [{1}] are equal", expected[loopCounter], lineOptionTitleAttributeValue));
                    loopCounter++;
                }

            }
            catch (Exception e)
            {
                throw new Exception("VerifyIfLineOptionsExist() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickLineOption", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickLineOption(String LineIndex, String Option)
        {
            try
            {
                Initialize();
                ClickDropdownArrow(LineIndex);
                Thread.Sleep(1000);
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                var hover = new DlkBaseControl("Line Index", targetRow);
                hover.MouseOver();
                IWebElement opt = null;
                // two tries, Delete seems to be 2 items
                try
                {
                    DlkLogger.LogInfo("Attempting to click first instance of option");
                    opt = targetRow.FindElements(By.XPath("./descendant::a[text()='" + Option + "']")).First();
                    opt.Click();
                    //new DlkBaseControl("Option", opt).ClickUsingJavaScript();
                }
                catch
                {
                    DlkLogger.LogInfo("Attempting to click last instance of option");
                    opt = targetRow.FindElements(By.XPath("./descendant::a[text()='" + Option + "']")).Last();
                    opt.Click();
                    //new DlkBaseControl("Option", opt).ClickUsingJavaScript();

                }
                if (Option.ToLower().Equals("insert"))
                {
                    targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                    targetRow.Click();
                }
                DlkLogger.LogInfo("ClickLineOption() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickLineOption() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickAttachmentOption", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickAttachmentOption(String LineIndex, String Option)
        {
            try
            {
                Initialize();
                ClickAttachmentIcon(LineIndex);
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));

                IWebElement opt = null;
                // two tries, Delete seems to be 2 items
                try
                {
                    DlkLogger.LogInfo("Attempting to click first instance of option");
                    opt = targetRow.FindElements(By.XPath("./descendant::a[text()='" + Option + "']")).First();
                    opt.Click();
                }
                catch
                {
                    DlkLogger.LogInfo("Attempting to click last instance of option");
                    opt = targetRow.FindElements(By.XPath("./descendant::a[text()='" + Option + "']")).Last();
                    opt.Click();
                }

                DlkLogger.LogInfo("ClickAttachmentOption() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickAttachmentOption() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickCellLink", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickCellLink(String LineIndex, String ColumnName)
        {
            try
            {
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                // check
                if (targetColumnNumber == -1)
                {
                    throw new Exception("Column '" + ColumnName + "' was not found.");
                }
                IWebElement parentCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]/.."));
                DlkBaseControl parentCtl = new DlkBaseControl("ParentCell", parentCell);
                parentCtl.MouseOver();
                //parentCell.Click();
                //parentCtl.Click(3);

                // need to re-initialize, DOM changes upon click
                Initialize();
                targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));

                if (!targetCell.GetAttribute("class").Contains("boolean"))
                {
                    if (targetCell.FindElements(By.XPath("./descendant::div[@class='cut']/span[1]")).Count > 0)
                    {
                        //get and click the keep checkmark inside the cell
                        IWebElement targetLink = targetCell.FindElement(By.XPath("./descendant::div[@class='cut']/span[1]"));
                        DlkBaseControl targetLnk = new DlkBaseControl("TargetLink", targetLink);
                        targetLnk.ClickUsingJavaScript();
                    }
                }
                DlkLogger.LogInfo("ClickCellLink() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickCellLink() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickCellButton", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickCellButton(String ColumnName, String LineIndex, String ButtonName)
        {
            try
            {
                Initialize();
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                IWebElement parentCell = null;
                if (ColumnName.ToLower().Equals("action"))
                {
                    parentCell = targetRow.FindElement(By.XPath("./descendant::td[@class='nobackground']/.."));
                }
                else
                {
                    int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                    // check
                    if (targetColumnNumber == -1)
                    {
                        throw new Exception("Column '" + ColumnName + "' was not found.");
                    }
                    parentCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]/.."));

                }

                DlkBaseControl parentCtl = new DlkBaseControl("ParentCell", parentCell);
                parentCtl.MouseOver();
                //parentCell.Click();
                parentCtl.Click(3);

                // need to re-initialize, DOM changes upon click
                Initialize();
                targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                IWebElement targetCell = null;
                if (ColumnName.ToLower().Equals("action"))
                {
                    targetCell = targetRow.FindElement(By.XPath("./descendant::td[@class='nobackground']/.."));
                }
                else
                {
                    int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                    // check
                    if (targetColumnNumber == -1)
                    {
                        throw new Exception("Column '" + ColumnName + "' was not found.");
                    }
                    targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]/.."));

                }

                switch (ButtonName.ToLower())
                {
                    case "calendar":
                        IWebElement calendar = targetCell.FindElement(By.XPath("./descendant::img[contains(@src,'calendar')]"));
                        calendar.Click();
                        break;
                    case "find":
                        IWebElement find = targetCell.FindElement(By.XPath("./descendant::i[contains(@class,'lookup')]/.."));
                        find.Click();
                        break;
                    case "favorite":
                        IWebElement favorite = targetCell.FindElement(By.XPath("./descendant::i[contains(@class,'fa-star')]/.."));
                        favorite.Click();
                        break;
                    case "edit":
                        IWebElement edit = targetCell.FindElement(By.XPath("./descendant::a[contains(@title,'Edit')]"));
                        edit.Click();
                        break;
                    case "submit":
                        IWebElement submit = targetCell.FindElement(By.XPath("./descendant::a[contains(@title,'Submit')]/.."));
                        submit.Click();
                        break;
                    case "delete":
                        IWebElement delete = targetCell.FindElement(By.XPath("./descendant::a[contains(@title,'Delete')]/.."));
                        delete.Click();
                        break;
                    case "reopen":
                        IWebElement reopen = targetCell.FindElement(By.XPath("./descendant::a[contains(@title,'Reopen')]/.."));
                        reopen.Click();
                        break;
                    default:
                        throw new Exception(ButtonName + " is an unrecognized button name");
                }
                DlkLogger.LogInfo("ClickCellButton() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickCellButton() failed : " + e.Message, e);
            }
        }
        [Keyword("SelectCell", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectCell(String LineIndex, String ColumnName)
        {
            try
            {
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                // check
                if (targetColumnNumber == -1)
                {
                    throw new Exception("Column '" + ColumnName + "' was not found.");
                }
                IWebElement parentCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]/.."));
                DlkBaseControl parentCtl = new DlkBaseControl("ParentCell", parentCell);
                parentCtl.MouseOver();
                parentCtl.Click(3);

                // need to re-initialize, DOM changes upon click
                Initialize();
                targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));


                /* if click fails on first try, the element is no longer attached to DOM. Try again */
                IWebElement targetInputControl = null;
                targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                DlkBaseControl targetCtl = new DlkBaseControl("TargetControl", targetInputControl);
                try
                {
                    targetInputControl.Click();
                }

                catch
                {
                    targetInputControl.Click();
                }

                DlkLogger.LogInfo("SelectCell() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectCell() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// This keyword will compare if the expected value of the n-th table header matches the actual
        /// value of the table header in the screen.
        /// </summary>
        /// <param name="ColumnIndex"></param>
        /// <param name="ExpectedResult"></param>
        [Keyword("VerifyTableColumnHeaderText")]
        public void VerifyTableColumnHeaderText(String ColumnIndex, String ExpectedResult)
        {
            try
            {
                Initialize();
                //go to the "grandparent"
                var tableColumnHeader = mElement.FindElement(By.XPath(string.Format("../../div[contains(@class,'header')]/table[@class='table']/descendant::th[{0}]", ColumnIndex)));
                var tableColumnHeaderText = tableColumnHeader.Text;
                if (tableColumnHeaderText != null)
                {
                    DlkAssert.AssertEqual("VerifyTableColumnHeaderText()", ExpectedResult
                    , tableColumnHeaderText); //search the specific table header relative to the unique table div
                    DlkLogger.LogInfo("VerifyTableColumnHeaderText() successfully executed.");
                }
                else
                {
                    throw new Exception(tableColumnHeaderText + " is NULL");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTableColumnHeaderText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// This keyword will compare if the expected value of the n-th table row name matches the actual
        /// value of the table row name in the screen.
        /// </summary>
        /// <param name="RowIndex"></param>
        /// <param name="ExpectedResult"></param>
        [Keyword("VerifyRowHeader")]
        public void VerifyRowHeader(String RowIndex, String ExpectedResult)
        {
            try
            {
                Initialize();

                IWebElement tableRowHeader = mElement.FindElement(By.XPath(string.Format("../../div[contains(@class,'ng-scope')]/table[contains(@class,'hover')]/descendant::td[contains(@class,'ng-binding') and contains(@style,'uppercase')][{0}]", RowIndex)));

                var tableRowName = tableRowHeader.Text;
                if (tableRowName != null)
                {
                    DlkAssert.AssertEqual("VerifyRowHeader()", ExpectedResult
                    , tableRowName);
                    DlkLogger.LogInfo("VerifyRowHeader() successfully executed.");
                }
                else
                {
                    throw new Exception(tableRowName + " is NULL");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowHeader() failed : " + e.Message, e);
            }

        }
        [Keyword("VerifyLineOptionsListExists")]
        public void VerifyLineOptionsListExists(String LineIndex, String TrueOrFalse)
        {

            try
            {
                Initialize();
                ClickDropdownArrow(LineIndex);
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                DlkAssert.AssertEqual("VerifyLineOptionsListExists()", Convert.ToBoolean(TrueOrFalse), targetRow.FindElements(By.XPath("./descendant::*[@class='dropdown-menu dropdown-menu-right line-action']")).Count > 0);
                DlkLogger.LogInfo("VerifyLineOptionsListExists() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLineOptionsListExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyLineOptions")]
        public void VerifyLineOptions(String LineIndex, String ListItems)
        {
            try
            {
                Initialize();
                ClickDropdownArrow(LineIndex);
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                String listVisibleOptions = "";
                ReadOnlyCollection<IWebElement> optionItems = targetRow.FindElements(By.XPath("./descendant::a[contains(@class,'ng-binding')]"));
                foreach (IWebElement opt in optionItems)
                {
                    if (string.IsNullOrEmpty(opt.Text) || string.IsNullOrEmpty(opt.Text.Trim()))
                    {
                        continue;
                    }
                    listVisibleOptions += opt.Text + "~";
                }
                listVisibleOptions = listVisibleOptions.Trim('~');
                DlkAssert.AssertEqual("VerifyLineOptions", ListItems, listVisibleOptions);
                /* Close the the dropdown list */
                //targetRow.SendKeys(Keys.Escape);
                CloseLineOptions(LineIndex);
                DlkLogger.LogInfo("VerifyLineOptions() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLineOptions() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// This keyword verifies if the line option dropdown contains a specific item/option.
        /// </summary>
        /// <param name="LineIndex"></param>
        /// <param name="ItemName"></param>
        /// <param name="TrueOrFalse"></param>
        [Keyword("VerifyLineOptionVisible")]
        public void VerifyLineOptionVisible(String LineIndex, String ItemName, String TrueOrFalse)
        {
            try
            {
                Initialize();
                ClickDropdownArrow(LineIndex);
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                DlkAssert.AssertEqual("VerifyLineOptionVisible()", Convert.ToBoolean(TrueOrFalse), targetRow.FindElement(By.XPath("./descendant::a[text()='" + ItemName + "'][contains(@class,'ng-binding')]")).Text.ToLower().Equals(ItemName.ToLower()));
                DlkLogger.LogInfo("VerifyLineOptionVisible() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLineOptionVisible() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// This keyword verifies if the line option dropdown contains a specific item/option that is disabled/enabled.
        /// </summary>
        /// <param name="LineIndex"></param>
        /// <param name="ItemName"></param>
        /// <param name="TrueOrFalse"></param>
        [Keyword("VerifyLineOptionDisabled")]
        public void VerifyLineOptionDisabled(String LineIndex, String ItemName, String TrueOrFalse)
        {
            try
            {
                Initialize();
                ClickDropdownArrow(LineIndex);
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                //get the lineoption based on input text
                var lineOption = targetRow.FindElement(By.XPath("./descendant::a[text()='" + ItemName + "'][contains(@class,'ng-binding')]"));
                //throw exception if line option is not visible
                if (!lineOption.Text.ToLower().Equals(ItemName.ToLower()))
                {
                    throw new Exception("Line option is not visible.");
                }
                //check if line option is disabled by checking for class name "disabled"
                var isDisabled = lineOption.GetAttribute("class").Contains("disabled");
                DlkAssert.AssertEqual("VerifyLineOptionDisabled()", Convert.ToBoolean(TrueOrFalse), isDisabled);
                DlkLogger.LogInfo("VerifyLineOptionDisabled() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLineOptionDisabled() failed : " + e.Message, e);
            }
        }

        [Keyword("DisplayLineDetails", new String[] { "1|text|Expected Value|TRUE" })]
        public void DisplayLineDetails(String LineIndex)
        {
            try
            {
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                IWebElement btn = targetRow.FindElement(By.XPath("./descendant::*[@class='icon-dots']"));
                new DlkBaseControl("targetButton", btn).MouseOver();
                btn.Click();
                DlkLogger.LogInfo("DisplayLineDetails() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("DisplayLineDetails() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyLineDetailsHeader", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyLineDetailsHeader(String LineIndex, String TrueOrFalse)
        {
            //todo: verify label

            try
            {
                Initialize();
                IWebElement popOver = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]/*[@class='popover']"));
                IWebElement header = popOver.FindElement(By.XPath("./descendant::h4[@class='ng-binding']"));
                DlkAssert.AssertEqual("VerifyLineDetailsHeader()", Convert.ToBoolean(TrueOrFalse), header.Text);
                DlkLogger.LogInfo("VerifyLineDetailsHeader() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLineDetailsHeader() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies an item in the list that appears below a textbox as you type in a table cell.
        /// </summary>
        /// <param name="ItemToBeVerified"></param>
        /// <param name="TrueOrFalse"></param>
        [Keyword("VerifyItemInTextBoxList")]
        public void VerifyItemInTextBoxList(String LineIndex, String ColumnName, String ItemToBeVerified, String TrueOrFalse)
        {
            ////div[contains(@class,'table-body')]//table[@class='row ng-scope']
            Initialize();
            //get specific cell
            //Initialize();
            IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
            int targetColumnNumber = GetColumnNumberFromName(ColumnName);

            // check
            if (targetColumnNumber == -1)
            {
                throw new Exception("Column '" + ColumnName + "' was not found.");
            }
            IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));
            new DlkBaseControl("FirstCol", firstCol).MouseOver();
            IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]/descendant::input"));
            bool found = false;
            //wait for dropdown suggestions/autocomplete feature
            Thread.Sleep(2000);
            //gets all of the dropdown items
            foreach (IWebElement dropDownItem in targetCell.FindElements(By.XPath("./parent::div//div[@class='result']/descendant::div[@class='row ng-scope']")))
            {
                if (dropDownItem.FindElements(By.XPath("./div/p")).Count > 0)
                {
                    //go to next item
                    if (found)
                    {
                        break;
                    }
                    //check each <p> tag if it contains the desired text
                    foreach (var p in dropDownItem.FindElements(By.XPath("./div/p")))
                    {
                        DlkLogger.LogInfo("Looking at text.. " +p.Text);
                        if (p.Text.Trim().Equals(ItemToBeVerified))
                        {
                            found=true;
                            DlkLogger.LogInfo("Found item "+ p.Text);
                            break;
                        }
                        else
                        {
                            //go to next
                            continue;
                        }                  
                    }
                }
            }
            if (!found)
            {
                throw new Exception("Item was not found.");
            }
            else
            {
                DlkLogger.LogInfo("VerifyItemInTextBoxList() passed");
            }
        }

        //[Keyword("KeepLine")]
        //public void KeepLine(String LineIndex)
        //{
        //    try
        //    {                
        //        // Implementation
        //        Initialize();
        //        // Create IWebElement for target row
        //        IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
        //        int targetColumnNumber = GetColumnNumberFromName("KEEP");

        //        // check
        //        if (targetColumnNumber == -1)
        //        {
        //            throw new Exception("Column '" + "KEEP" + "' was not found.");
        //        }

        //        //get and click the cell that contains the keep checkmark
        //        IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));
        //        DlkBaseControl targetCtl = new DlkBaseControl("TargetCell", targetCell);
        //        targetCtl.ClickUsingJavaScript();

        //        //get and click the keep checkmark inside the cell
        //        IWebElement targetLink = targetCell.FindElement(By.XPath("./a[contains(@style, 'color')]"));
        //        DlkBaseControl targetLnk = new DlkBaseControl("TargetLink", targetLink);
        //        targetLnk.ClickUsingJavaScript();

        //        // Log success message
        //        DlkLogger.LogInfo("KeepLine() successfully executed.");
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("KeepLine() failed : " + e.Message, e);
        //    }
        //}

        //[Keyword("VerifyKeepLine")]
        //public void VerifyKeepLine(String LineIndex, String ExpectedValue)
        //{
        //    try
        //    {
        //        // Implementation
        //        Initialize();
        //        IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
        //        int targetColumnNumber = GetColumnNumberFromName("KEEP");

        //        // check
        //        if (targetColumnNumber == -1)
        //        {
        //            throw new Exception("Column '" + "KEEP" + "' was not found.");
        //        }

        //        //get the cell that contains the keep checkmark we want to verify
        //        IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));
        //        DlkBaseControl targetCtl = new DlkBaseControl("TargetCell", targetCell);

        //        //verify if the keep checkmark is checked
        //        DlkAssert.AssertEqual("KeepLine()", Convert.ToBoolean(ExpectedValue), targetCell.FindElements(By.XPath("./a[contains(@style, '80')]")).Count > 0);

        //        DlkLogger.LogInfo("VerifyKeepLine() successfully executed.");
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("VerifyKeepLine() failed : " + e.Message, e);
        //    }
        //}


        private void FindColumnHeaders()
        {
            List<IWebElement> collection = new List<IWebElement>();

            IWebElement tableHeader = mElement.FindElement(By.XPath("./preceding::div[contains(@class,'table-header')][1]/table"));

            if (mElement.GetAttribute("class").Contains("ng-scope"))
            {
                foreach (IWebElement elm in tableHeader.FindElements(By.XPath("./thead//th")))
                {
                    if (elm.Displayed)
                    {
                        collection.Add(elm);
                    }
                }
                //if (!string.IsNullOrEmpty(elm.Text))
                //{
                //    collection.Add(elm);
                //}
                //else if (elm.FindElements(By.CssSelector("span")).Count > 0)
                //{
                //    collection.Add(elm);
                //}
            }

            else
            {
                foreach (IWebElement elm in mElement.FindElements(By.XPath("./thead//th[contains(@class,'ng-binding')]")))
                {
                    if (elm.Displayed)
                    {
                        collection.Add(elm);
                    }
                }
            }
            //foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::thead/descendant::th/div")))
            //{
            //    if (!string.IsNullOrEmpty(elm.Text))
            //    {
            //        collection.Add(elm);
            //    }
            //}
            mColumnHeaders = new ReadOnlyCollection<IWebElement>(collection);
        }

        private int GetColumnNumberFromName(string ColumnName)
        {
            int ret = -1;
            for (int i = 0; i < mColumnHeaders.Count; i++)
            {
                if (new DlkBaseControl("Header", mColumnHeaders[i]).GetValue().ToLower() == ColumnName.ToLower())
                {
                    ret = i + 1;
                    break;
                }
            }
            return ret;
        }

        [Keyword("GetLineIndexWithColumnValue", new String[] { "1|text|ColumnName|ID",
                                                         "2|text|SearchedValue|1234",
                                                         "3|text|VariableName|LineIndex"})]
        public void GetLineIndexWithColumnValue(string ColumnName, string Value, string VariableName)
        {
            Initialize();
            //if(Value == ""){
            //    Value = null;
            //}
            int lineIndex = -1;
            int i = 1;
            string cellValue = GetCellValue(i, ColumnName);
            while (cellValue != null)
            {
                if (cellValue == Value)
                {
                    lineIndex = i;
                    break;
                }

                i++;
                cellValue = GetCellValue(i, ColumnName);
            }

            if (lineIndex > -1)
            {
                DlkVariable.SetVariable(VariableName, lineIndex.ToString());
                DlkLogger.LogInfo("GetRowWithColumnValue() passed.");
            }
            else
            {
                DlkVariable.SetVariable(VariableName, lineIndex.ToString());
                //throw new Exception("GetRowWithColumnValue() failed. Unable to find row.");
            }
        }

        [Keyword("GetLineCount", new String[] { "1|text|Row|1", 
                                                "2|text|VariableName|RowCount"})]
        public void GetLineCount(string VariableName)
        {
            try
            {

                Initialize();
                int actRowCount = mElement.FindElements(By.XPath("./descendant::tbody/tr")).Count;
                DlkVariable.SetVariable(VariableName, actRowCount.ToString());
                DlkLogger.LogInfo("GetLineCount() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetLineCount() failed : " + e.Message, e);
            }

        }

        [Keyword("GetCellValue", new String[] { "1|text|Row|1", 
                                                "2|text|VariableName|RowCount"})]
        public void GetCellValue(String ColumnName, String LineIndex, String VariableName)
        {
            try
            {

                Initialize();
                string cellValue = GetCellValue(Convert.ToInt32(LineIndex), ColumnName);
                DlkVariable.SetVariable(VariableName, cellValue);
                DlkLogger.LogInfo("GetCellValue() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetCellValue() failed : " + e.Message, e);
            }

        }

        [Keyword("EnterSimultaneousKeys", new String[] { "1|text|Row|1", 
                                                "2|text|VariableName|RowCount"})]
        public void EnterSimultaneousKeys(String ColumnNumber, String RowNumber, String CtrlShiftAlt, String ConcurrentKey)
        {
            try
            {
                int rowNum;
                int columnNum;
                int.TryParse(RowNumber, out columnNum);
                int.TryParse(RowNumber, out rowNum);
                Initialize();
                var cell = mElement.FindElement(By.XPath(string.Format("../..//div[contains(@class,'body')]/table[contains(@class,'table')]/tbody/tr[{0}]/td[{1}]/descendant::input[1]", RowNumber, ColumnNumber)));
                InputConcurrentKey(rowNum, columnNum, CtrlShiftAlt, ConcurrentKey, cell);
                DlkLogger.LogInfo("EnterSimultaneousKeys() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("EnterSimultaneousKeys() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyIfDropdownListContains", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyIfDropdownListContains(String ColumnName, String LineIndex, String Contains)
        {
            try
            {
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                // check
                if (targetColumnNumber == -1)
                {
                    throw new Exception("Column '" + ColumnName + "' was not found.");
                }
                IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));
                //firstCol.Click();
                new DlkBaseControl("FirstCol", firstCol).MouseOver();
                IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));
                IWebElement button = targetCell.FindElement(By.XPath("./descendant::a[1]"));
                //IWebElement button = targetCell.FindElement(By.XPath("./descendant::a[@class='ng-isolate-scope']"));
                int attempts = 0;
                do
                {
                    DlkLogger.LogInfo("Attempting to display list attempt " + ++attempts + " ...");
                    button.Click();
                }
                while (targetCell.FindElements(By.XPath("./descendant::*[@class='result-wrapper']")).Count <= 0 && attempts < 10);
                //look at each list item and check if word exists
                DlkLogger.LogInfo("The list has been displayed");
                foreach (var listItem in targetCell.FindElements(By.XPath("./descendant::a/div/p")))
                {
                    if (listItem.Text.ToLower().Contains(Contains.ToLower()))
                    {
                        DlkLogger.LogInfo("An item was found containing the string: " + Contains);
                        DlkLogger.LogInfo("Actual Item: " + listItem.Text);
                    }
                }

                DlkLogger.LogInfo("VerifyIfDropdownListContains() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyIfDropdownListContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellReadonly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCellReadonly(String ColumnName, String LineIndex, String Value = "")
        {
            try
            {
                Initialize();
                Value = Value.ToLower();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                // check
                if (targetColumnNumber == -1)
                {
                    //try checking if column header is image
                    targetColumnNumber = GetColNumberFromImageName(ColumnName);
                    if (targetColumnNumber == -1)
                    {
                        throw new Exception("Column '" + ColumnName + "' was not found.");
                    }
                }

                IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));
         
                new DlkBaseControl("FirstCol", firstCol).MouseOver();
                IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));

                if (targetCell.GetAttribute("class").Contains("boolean"))
                {
                    DlkAssert.AssertEqual("VerifyCellReadonly()", Convert.ToBoolean(Value), targetCell.FindElements(By.XPath(".//*[contains(@class, 'checked')]")).Count > 0);
                }
                else if (targetCell.FindElements(By.XPath(".//*[1]")).Count > 0)
                {
                    IWebElement targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                    if (targetCell.FindElements(By.XPath("./descendant::input[2][not(contains(@class,'ng-hide'))]")).Count > 0)
                    {
                        targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[2]"));
                    }
                    DlkAssert.AssertEqual("VerifyCellReadonly()", Value, new DlkBaseControl("Target", targetInputControl).IsReadOnly().ToLower());
                }
                else
                {
                    DlkAssert.AssertEqual("VerifyCellReadonly()", Value, new DlkBaseControl("Target", targetCell).IsReadOnly().ToLower());

                }
                DlkLogger.LogInfo("VerifyCellReadonly() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellReadonly() failed : " + e.Message, e);
            }
        }


        internal void InputConcurrentKey(int rowNum, int columnNum, string specialKey, string concurrentKey, IWebElement cell)
        {
            try
            {
                Initialize();
                //this works and with less code than using Actions.Sendkeys(keys.ctrl).sendkeys(concurrentkey).Perform();
                switch (specialKey.ToLower())
                {
                    case "ctrl":
                        cell.SendKeys(Keys.Control + concurrentKey);
                        break;
                    case "shift":
                        cell.SendKeys(Keys.Shift + concurrentKey);
                        break;
                    case "alt":
                        cell.SendKeys(Keys.Alt + concurrentKey);
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                throw;
            }
        }

        public string GetCellValue(int LineIndex, string ColumnName)
        {
            Initialize();
            string ret = "";
            IWebElement targetRow = null;
            try
            {
                targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {
                return null;
            }

            int targetColumnNumber = GetColumnNumberFromName(ColumnName);

            // check
            if (targetColumnNumber == -1)
            {
                //try checking if column header is image
                targetColumnNumber = GetColNumberFromImageName(ColumnName);
                if (targetColumnNumber == -1)
                {
                    throw new Exception("Column '" + ColumnName + "' was not found.");
                }
            }

            IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));
            //firstCol.Click();
            new DlkBaseControl("FirstCol", firstCol).MouseOver();
            IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));

            if (targetCell.FindElements(By.XPath(".//*[1]")).Count > 0)
            {
                IWebElement targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                if (targetCell.FindElements(By.XPath("./descendant::input[2][not(contains(@class,'ng-hide'))]")).Count > 0)
                {
                    targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[2]"));
                }
                ret = new DlkBaseControl("Target", targetInputControl).GetValue();
            }
            else
            {
                ret = new DlkBaseControl("Target", targetCell).GetValue();

            }
            return ret;
        }

        /// <summary>
        /// This function is for retrieving column numbers without header names but images
        /// </summary>
        private int GetColNumberFromImageName(string ColumnName)
        {
            string colName = ColumnName.ToLower();
            int ret = -1;
            for (int i = 0; i < mColumnHeaders.Count; i++)
            {
                if (mColumnHeaders[i].GetAttribute("source").ToLower().Contains(colName))
                {
                    ret = i + 1;
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// Opens the Dropdown list in LineOptions
        /// </summary>
        /// <param name="LineIndex"></param>
        private void ClickDropdownArrow(String LineIndex)
        {
            IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
            IWebElement columnToBeClickedToFocusRow = targetRow.FindElement(By.XPath("./descendant::td[2]"));
            int attempts = 0;
            DlkBaseControl x = new DlkBaseControl("FirstCol", columnToBeClickedToFocusRow);
            x.MouseOver();
            x.Click();
            //firstCol.Click();
            Thread.Sleep(1000);

            //reassign targetrow; DOM changes upon click
            targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
            IWebElement btn = targetRow.FindElement(By.XPath(".//div[@class='dropdown ng-isolate-scope']//*[@class='icon-droparrow2']"));
            while (attempts < 5)
            {
                try
                {
                    new DlkBaseControl("targetButton", btn).MouseOver();
                    btn.Click();
                    break;
                }
                catch (Exception)
                {
                    //do nothing
                }
                attempts++;
            }

            //old code:
            //IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));

            //if (targetRow.FindElements(By.XPath("./descendant::a[@aria-expanded='true']")).Count <= 0)
            //{
            //    new DlkBaseControl("FirstCol", firstCol).MouseOver();
            //    IWebElement btn = targetRow.FindElement(By.XPath("./descendant::*[@class='icon-droparrow2 dropdown-toggle']"));
            //    new DlkBaseControl("targetButton", btn).MouseOver();
            //    btn.Click();
            //}
        }

        /// <summary>
        /// Opens the Attachment list in Attachments
        /// </summary>
        /// <param name="LineIndex"></param>
        private void ClickAttachmentIcon(String LineIndex)
        {
            IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
            IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));
            int attempts = 0;
            new DlkBaseControl("FirstCol", firstCol).MouseOver();
            firstCol.Click();

            //reassign targetrow; DOM changes upon click
            targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
            IWebElement btn = targetRow.FindElement(By.XPath(".//div[@class='dropdown']//a[1]"));
            while (attempts < 5)
            {
                try
                {
                    new DlkBaseControl("targetButton", btn).MouseOver();
                    btn.Click();
                    break;
                }
                catch (Exception)
                {
                    //do nothing
                }
                attempts++;
            }
        }

        /// <summary>
        /// Closes dropdown list in LineOptions
        /// </summary>
        private void CloseLineOptions(String LineIndex)
        {
            IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
            IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));
            new DlkBaseControl("FirstCol", firstCol).MouseOver();
            firstCol.Click();
        }

    }
}
