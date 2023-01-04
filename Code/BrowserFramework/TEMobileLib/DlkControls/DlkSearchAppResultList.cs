using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;

namespace TEMobileLib.DlkControls
{
    [ControlType("SearchAppResultList")]
    public class DlkSearchAppResultList : DlkBaseControl
    {
        private Boolean IsInit = false;

        public DlkSearchAppResultList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkSearchAppResultList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkSearchAppResultList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }

        }

        [Keyword("Select")]
        public void Select(String Value)
        {
            try
            {
                Initialize();
                mElement.FindElement(By.XPath("./descendant::div[@class='autoCItem' and text()='" + Value + "']")).Click();
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectByIndex")]
        public void SelectByIndex(String Index)
        {
            try
            {
                Initialize();
                int idx;

                if (!int.TryParse(Index, out idx))
                {
                    throw new Exception("Index is not a valid integer.");
                }
                
                IReadOnlyCollection<IWebElement> itms = mElement.FindElements(By.XPath("./descendant::div[@class='autoCItem']"));

                if (idx <= 0 || idx > itms.Count)
                {
                    throw new Exception("Index cannot be less than or equal to 0 or larger than number of result list items.");
                }

                itms.ToList()[idx - 1].Click();
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectContains")]
        public void SelectContains(String Value)
        {
            try
            {
                Initialize();
                Initialize();
                mElement.FindElement(By.XPath("./descendant::div[@class='autoCItem' and contains(text(),'" + Value + "')]")).Click();
            }
            catch (Exception e)
            {
                throw new Exception("SelectContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists")]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                Initialize();
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyList")]
        public void VerifyList(String ExpectedValues)
        {
            try
            {
                Initialize();
                String comboBoxItems = "";
                IList<IWebElement> mList = mElement.FindElements(By.XPath(".//div[@class='autoCItem']"));
                this.ScrollIntoViewUsingJavaScript();

                foreach (IWebElement mItem in mList)
                {
                    if (mItem.Displayed)
                    {
                        comboBoxItems += mItem.Text + "~";
                    }
                }
                DlkAssert.AssertEqual("VerifyList()", ExpectedValues, DlkString.NormalizeNonBreakingSpace(comboBoxItems).Trim('~'));
                DlkLogger.LogInfo("VerifyList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAvailableInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyAvailableInList")]
        public void VerifyAvailableInList(String ItemValue, String ExpectedValue)
        {
            try
            {
                Initialize();
                this.ScrollIntoViewUsingJavaScript();                
                DlkAssert.AssertEqual("VerifyAvailableInList()", Convert.ToBoolean(ExpectedValue),
                    mElement.FindElements(By.XPath("./descendant::div[@class='autoCItem' and text()='" + ItemValue + "']")).Count() > 0);
                DlkLogger.LogInfo("VerifyAvailableInList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAvailableInList() failed : " + e.Message, e);
            }
        }

        [Keyword("GetExists", new String[] { "1|text|VariableName|MyVar" })]
        public void GetExists(string sVariableName)
        {
            try
            {
                string sControlExists = Exists(3).ToString();
                DlkVariable.SetVariable(sVariableName, sControlExists);
                DlkLogger.LogInfo("Successfully executed GetExists(). Value : " + sControlExists);
            }
            catch (Exception e)
            {
                throw new Exception("GetExists() failed : " + e.Message, e);
            }
        }
    }
}
