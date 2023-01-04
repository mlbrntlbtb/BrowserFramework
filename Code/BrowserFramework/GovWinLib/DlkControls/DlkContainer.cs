using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("Container")]
    public class DlkContainer : DlkBaseControl
    {
        private Boolean IsInit = false;
        protected DlkProcessing mProcessing = null;
        protected Dictionary<String,Dictionary<String,dynamic>> mRows;
        protected Dictionary<String, dynamic> vars;

        public DlkContainer(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkContainer(String ControlName, String SearchType, String SearchValue, DlkProcessing processing)
            : base(ControlName, SearchType, SearchValue) { mProcessing = processing; }
        public DlkContainer(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkContainer(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        protected virtual void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }
            else
            {
                if (IsElementStale())
                {
                    FindElement();
                }
            }
        }

        void mapContainer()
        {
            //IList<IWebElement> rows = DlkEnvironment.AutoDriver.FindElements(By.XPath(mSearchValues[0] + "//div[@class[contains(.,'Container')]/div"));
            IList<IWebElement> rows = mElement.FindElements(By.XPath(".//div[contains(@class,'Container')]/div[contains(@class,'Row')]"));
            mRows = new Dictionary<string, Dictionary<String,dynamic>>();

            int indx = 0;            
            foreach (IWebElement row in rows)
            {
                vars = new Dictionary<string,dynamic>();

                vars.Add("TextBox"+indx, new DlkTextBox("TextBox"+indx, row.FindElement(By.XPath(".//input[@type='text']"))));
                if(indx > 1)
                    vars.Add("Link"+indx, new DlkLink("Link"+indx, row.FindElement(By.XPath(".//a"))));

                mRows.Add(indx.ToString(),vars);                

                indx++;
            }
        }

        [Keyword("SetText", new String[] {"1|text|Row|O{Row}",
                                                    "2|text|Text|Text"})]
        public void SetText(String Row, String Text)
        {
            try
            {
                int iRow = Convert.ToInt32(Row) - 1;

                while(!this.Exists())
                    Thread.Sleep(-1);
                
                Initialize();
                mapContainer();

                if (!mRows.ContainsKey(iRow.ToString()))
                    throw new Exception("SetText() failed. Row not found.");

                DlkTextBox txtBox = (DlkTextBox)mRows.ElementAt(iRow).Value.ElementAt(0).Value;

                txtBox.Set(Text);

                DlkLogger.LogInfo("SetText() successfully executed.");
            }
            catch (InvalidOperationException invalid)
            {
                throw new Exception(string.Format("Exception of type {0} caught in button SetText() method.", invalid.GetType()));
            }

        }

        [Keyword("VerifyText", new String[] {"1|text|Row|O{Row}",
                                                    "2|text|Text|Text"})]
        public void VerifyText(String Row, String Text)
        {
            try
            {
                int iRow = Convert.ToInt32(Row) - 1;

                while (!this.Exists())
                    Thread.Sleep(-1);

                Initialize();
                mapContainer();

                if (!mRows.ContainsKey(iRow.ToString()))
                    throw new Exception("VerifyText() failed. Row not found.");

                DlkTextBox txtBox = (DlkTextBox)mRows[iRow.ToString()]["TextBox" + iRow];

                txtBox.VerifyText(Text);

                DlkLogger.LogInfo("VerifyText() successfully executed.");
            }
            catch (InvalidOperationException invalid)
            {
                throw new Exception(string.Format("Exception of type {0} caught in button VerifyText() method.", invalid.GetType()));
            }

        }

        [RetryKeyword("VerifyRowExists", new String[] { "1|text|Row|O{Row}", "2|text|Expected Value|TRUE" })]
        public void VerifyRowExists(String Row, String ExpectedValue)
        {
            try
            {
                int iRow = Convert.ToInt32(Row) - 1;
                Boolean expectedVal = Convert.ToBoolean(ExpectedValue);
                Boolean actualVal = false;

                while (!this.Exists())
                    Thread.Sleep(-1);

                Initialize();
                mapContainer();

                if (!mRows.ContainsKey(iRow.ToString()))
                    actualVal = false;
                else
                    actualVal = true;

                DlkLogger.LogInfo("Successfully executed VerifyRowExists().");
                DlkAssert.AssertEqual("VerifyRowExists",expectedVal, actualVal);
            }
            catch (InvalidOperationException invalid)
            {
                throw new Exception(string.Format("Exception of type {0} caught in button VerifyRowExists() method.", invalid.GetType()));
            }
        }

        [Keyword("ClickRowLink", new String[] { "1|text|Row|O{Row}" })]
        public void ClickRowLink(String Row)
        {
            try
            {
                int iRow = Convert.ToInt32(Row) - 1;

                while (!this.Exists())
                    Thread.Sleep(-1);

                Initialize();
                mapContainer();

                if (!mRows.ContainsKey(iRow.ToString()))
                    throw new Exception("ClickRowLink() failed. Row not found.");

                DlkLink currentRow = (DlkLink)mRows[iRow.ToString()]["Link" + iRow];

                currentRow.Click();

                DlkLogger.LogInfo("Successfully executed ClickRowLink().");
            }
            catch (InvalidOperationException invalid)
            {
                throw new Exception(string.Format("Exception of type {0} caught in button ClickRowLink() method.", invalid.GetType()));
            }
        }

        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
            {
                try
                {
                    base.VerifyExists(Convert.ToBoolean(expectedValue));
                    DlkLogger.LogInfo("VerifyExists() passed");
                }
                catch (Exception e)
                {
                    throw e;
                }
            }, new String[] { "retry" }); //skip until timeout and retries remain
        }

        [RetryKeyword("GetIfExists", new String[] { "1|text|Expected Value|TRUE",
                                                            "2|text|VariableName|ifExist"})]
        public new void GetIfExists(String VariableName)
        {
            this.PerformAction(() =>
            {

                Boolean bExists = base.Exists();
                DlkVariable.SetVariable(VariableName, Convert.ToString(bExists));

            }, new String[] { "retry" });
        }
    }
}
