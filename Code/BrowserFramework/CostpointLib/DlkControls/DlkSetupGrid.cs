using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CostpointLib.DlkControls
{
    [ControlType("SetupGrid")]
    public class DlkSetupGrid : DlkBaseControl
    {
        public DlkSetupGrid(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkSetupGrid(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkSetupGrid(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private String mstrRowXPATH = ".//div[@class='dashSetupRow']";
        private String mstrNameCellXPATH = ".//div[contains(@class, 'NameCell')]";
        private String mstrAddButtonXPATH = ".//div[@class='dashAddIcon']";
        private String mstrEditButtonXPATH = ".//div[@class='dashEditIcon']";
        private String mstrMoveUpButtonXPATH = ".//div[@class='dashArrowUpIcon']";
        private String mstrMoveDownButtonXPATH = ".//div[@class='dashArrowDownIcon']";
        private String mstrRemoveButtonXPATH = ".//div[@class='dashRemoveIcon']";
        private String mstrGridHeaderXPATH = ".//div[contains(@class, 'HeaderText')]";

        public void Initialize()
        {
            FindElement();
        }

        [Keyword("VerifyNameCellValue", new String[] { "1|text|Row|1",
                                                       "2|text|ExpectedValue|My Menu"})]
        public void VerifyNameCellValue(String Row, String ExpectedValue)
        {
            try
            {
                Initialize();
                List<IWebElement> mlstCells = mElement.FindElements(By.XPath(mstrNameCellXPATH)).ToList();
                string actualValue = mlstCells[Convert.ToInt32(Row) - 1].Text.ToLower();
                DlkAssert.AssertEqual("VerifyNameCellValue()", ExpectedValue.ToLower(), actualValue);
                DlkLogger.LogInfo("VerifyNameCellValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyNameCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyHeaderValue", new String[] {"1|text|ExpectedValue|My Menu"})]
        public void VerifyHeaderValue(String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement mGridHeader = mElement.FindElements(By.XPath(mstrGridHeaderXPATH)).Any() ? mElement.FindElement(By.XPath(mstrGridHeaderXPATH)) : null;
                if (mGridHeader == null)
                {
                    throw new Exception("VerifyHeaderValue() failed : header not found in grid");
                }
                string actualValue = mGridHeader.Text.ToLower().Split('(', ')')[1];
                DlkAssert.AssertEqual("VerifyHeaderValue()", ExpectedValue.ToLower(), actualValue);
                DlkLogger.LogInfo("VerifyHeaderValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyHeaderValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetHeaderValue", new String[] { "1|text|VariableName|SelectedValue" })]
        public void GetHeaderValue(String VariableName)
        {
            try
            {
                Initialize();
                IWebElement mGridHeader = mElement.FindElements(By.XPath(mstrGridHeaderXPATH)).Any() ? mElement.FindElement(By.XPath(mstrGridHeaderXPATH)) : null;
                if (mGridHeader == null)
                {
                    throw new Exception("GetHeaderValue() failed : header not found in grid");
                }
                string actualValue = mGridHeader.Text.ToLower().Split('(', ')')[1];
                DlkVariable.SetVariable(VariableName, actualValue);
                DlkLogger.LogInfo("GetHeaderValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetHeaderValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetNameCellValue", new String[] { "1|text|Row|1",
                                                    "2|text|VariableName|MyVar"})]
        public void GetNameCellValue(String Row, String VariableName)
        {
            try
            {
                Initialize();
                List<IWebElement> mlstCells = mElement.FindElements(By.XPath(mstrNameCellXPATH)).ToList();
                string actualValue = mlstCells[Convert.ToInt32(Row) - 1].Text;
                DlkVariable.SetVariable(VariableName, actualValue);
                DlkLogger.LogInfo("GetNameCellValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetNameCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetGridRowWithCellValue", new String[] { "1|text|Value|AP Current",
                                                           "2|text|VariableName|MyVar"})]
        public void GetGridRowWithCellValue(String Value, String VariableName)
        {
            try
            {
                Initialize();
                int rowIndex = -1;
                List<IWebElement> mlstCells = mElement.FindElements(By.XPath(".//div[contains(@class, 'NameCell')]")).ToList();
                foreach (IWebElement elmt in mlstCells)
                {
                    if (elmt.Text == Value)
                    {
                        rowIndex = mlstCells.IndexOf(elmt) + 1;
                    }
                }
                if (rowIndex < 0)
                {
                    throw new Exception("GetGridRowWithCellValue() failed : value not found in any of the rows");
                }
                DlkVariable.SetVariable(VariableName, rowIndex.ToString());
                DlkLogger.LogInfo("GetGridRowWithCellValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetGridRowWithCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickRowButton", new String[] { "1|text|Row|1",
                                                  "2|text|ButtonToClick|Add"})]
        public void ClickRowButton(String Row, String ButtonToClick)
        {
            try
            {
                Initialize();
                List<IWebElement> mlstRows = mElement.FindElements(By.XPath(mstrRowXPATH)).ToList();
                IWebElement row = mlstRows[Convert.ToInt32(Row) - 1];
                string btnXPATH = "";
                switch (ButtonToClick)
                {
                    case "Add":
                        btnXPATH = mstrAddButtonXPATH;
                        break;
                    case "Edit":
                        btnXPATH = mstrEditButtonXPATH;
                        break;
                    case "Move Up":
                        btnXPATH = mstrMoveUpButtonXPATH;
                        break;
                    case "Move Down":
                        btnXPATH = mstrMoveDownButtonXPATH;
                        break;
                    case "Remove":
                        btnXPATH = mstrRemoveButtonXPATH;
                        break;
                    default:
                        throw new Exception("ClickRowButton() failed : Button not supported");
                }
                IWebElement buttonToClick = row.FindElement(By.XPath(btnXPATH));
                buttonToClick.Click();
            }
            catch (Exception e)
            {
                throw new Exception("ClickRowButton() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowButtonExists", new String[] { "1|text|Row|1",
                                                  "2|text|ButtonToClick|Add",
                                                  "3|text|ExpectedValue|True"})]
        public void VerifyRowButtonExists(String Row, String ButtonToVerify, String ExpectedValue)
        {
            try
            {
                Initialize();
                List<IWebElement> mlstRows = mElement.FindElements(By.XPath(mstrRowXPATH)).ToList();
                IWebElement row = mlstRows[Convert.ToInt32(Row) - 1];
                string btnXPATH = "";
                switch (ButtonToVerify)
                {
                    case "Add":
                        btnXPATH = mstrAddButtonXPATH;
                        break;
                    case "Edit":
                        btnXPATH = mstrEditButtonXPATH;
                        break;
                    case "Move Up":
                        btnXPATH = mstrMoveUpButtonXPATH;
                        break;
                    case "Move Down":
                        btnXPATH = mstrMoveDownButtonXPATH;
                        break;
                    case "Remove":
                        btnXPATH = mstrRemoveButtonXPATH;
                        break;
                    default:
                        throw new Exception("VerifyRowButtonExists() failed : Button not supported");
                }
                IWebElement buttonToVerify = row.FindElements(By.XPath(btnXPATH)).FirstOrDefault(elmt => elmt.Displayed);
                bool actualValue = buttonToVerify != null;
                DlkAssert.AssertEqual("VerifyRowButtonExists() : " + mControlName, Convert.ToBoolean(ExpectedValue), actualValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowButtonExists() failed : " + e.Message, e);
            }
        }
    }
}
