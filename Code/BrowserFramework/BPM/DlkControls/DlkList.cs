using System;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium.Interactions;

namespace BPMLib.DlkControls
{
    [ControlType("List")]
    public class DlkList : DlkBaseControl
    {
        public DlkList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        public DlkList(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }


        public void Initialize()
        {
            FindElement();
            DlkEnvironment.mSwitchediFrame = true;
        }

        private void Terminate()
        {
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkEnvironment.mSwitchediFrame = false;
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String strExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(strExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("DoubleClickListItem", new String[] { "1|text|item|sampleitem" })]
        public void DoubleClickListItem(String Item)
        {
            try
            {
                DlkUtility.DlkBPMCommon.WaitForSpinner();
                Initialize();
                var hits = mElement.FindElements(By.XPath("./descendant::*[contains(text(),'" + Item.Split(null).FirstOrDefault().Trim() + "')]/ancestor::td[1]"));

                foreach (IWebElement hit in hits)
                {
                    if (hit.Text != null && DlkString.NormalizeNonBreakingSpace(hit.Text).Trim() == Item)
                    {
                        DlkBaseControl ctr = new DlkBaseControl("ItemList", hit);
                        ctr.DoubleClick();
                        DlkLogger.LogInfo("Successfully executed DoubleClickListItem()");
                        return;
                    }
                }
                throw new Exception("Item '" + Item + "' not found.");
            }
            catch (Exception e)
            {
                throw new Exception("DoubleClickListItem() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("Select", new String[] { "1|text|item|sampleitem" })]
        public void Select(String Item)
        {
            try
            {
                Initialize();
                var hits = mElement.FindElements(By.XPath("./descendant::span[contains(text(),'" + Item.Trim() + "')]/ancestor::td[1]"));

                foreach (IWebElement hit in hits)
                {
                    int separatorPosition = hit.Text.IndexOf(':') + 1;
                    int textLength = separatorPosition > 0 ? separatorPosition : hit.Text.Length;

                    if (hit.Text != null && DlkString.NormalizeNonBreakingSpace(hit.Text.Substring(0, textLength)).Trim() == Item)
                    {
                        Actions action = new Actions(DlkEnvironment.AutoDriver);
                        //try
                        //{
                            IJavaScriptExecutor js = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                            js.ExecuteScript("arguments[0].scrollIntoView(true);", hit);
                            js.ExecuteScript("arguments[0].scrollIntoView(false);", hit);
                            action.MoveToElement(hit, 1, 1).Click().Perform();

                            DlkLogger.LogInfo("Successfully executed Select()");
                            return;
                        //}
                        //catch(Exception ex)
                        //{
                        //    if (ex.Message.Contains("out of bounds of viewport"))
                        //    {
                        //        IJavaScriptExecutor js = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                        //        js.ExecuteScript("arguments[0].scrollIntoView(true);", hit);
                        //        js.ExecuteScript("arguments[0].scrollIntoView(false);", hit);
                        //        action.MoveToElement(hit, 1, 1).Click().Perform();
                        //    }
                        //}
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("SelectItemByIndex", new String[] { "1|text|index|1" })]
        public void SelectItemByIndex(String Index)
        {
            try
            {
                Initialize();
                IWebElement hit = mElement.FindElements(By.TagName("table")).ElementAtOrDefault(int.Parse(Index));

                if (hit != null)
                {
                    Actions action = new Actions(DlkEnvironment.AutoDriver);
                    action.MoveToElement(hit, 1, 1).Click().Perform();
                    DlkLogger.LogInfo("Successfully executed SelectItemByIndex()");
                }
                else
                    throw new ArgumentException("SelectItemByIndex() failed : No items found on list");
            }
            catch (Exception e)
            {
                throw new Exception("SelectItemByIndex() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyInList", new String[] { "" })]
        public void VerifyInList(String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                var hits = mElement.FindElements(By.XPath(".//table"));
                bool expected = bool.Parse(TrueOrFalse);
                bool actual = false;

                foreach (IWebElement hit in hits)
                {
                    string text = DlkString.NormalizeNonBreakingSpace(hit.Text).Trim();
                    DlkLogger.LogInfo(string.Format("Comparing values [Expected: {0} | Actual: {1}]", Item, text));
                    if (hit.Text != null && text == Item)
                    {
                        actual = true;
                        break;
                    }
                }
                DlkLogger.LogInfo("Item '" + Item + (actual ? "' found." : "' not found."));
                DlkAssert.AssertEqual("VerifyInList()", expected, actual);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyInList() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }
    }
}
