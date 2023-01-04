using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using ProjectInformationManagementLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectInformationManagementLib.DlkControls
{
    [ControlType("Expander")]
    public class DlkExpander : DlkProjectInformationManagementBaseControl
    {
        #region DECLARATIONS
        private List<IWebElement> mItems;
        private bool _iframeSearchType = false;
        #endregion

        #region CONSTRUCTOR
        public DlkExpander(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkExpander(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkExpander(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkExpander(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            //support for multiple windows
            if (DlkEnvironment.AutoDriver.WindowHandles.Count > 1)
            {
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
            }
            else
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            }

            if (mSearchType.ToLower().Equals("iframe_xpath"))
            {
                _iframeSearchType = true;
                FindElement();
                DlkEnvironment.mSwitchediFrame = true;
                GetItems();
            }
            else
            {
                _iframeSearchType = false;
                FindElement();
                this.ScrollIntoViewUsingJavaScript();
                GetItems();
            }
        }

        public void Terminate()
        {
            if (_iframeSearchType)
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                DlkEnvironment.mSwitchediFrame = false;
            }
        }

        #endregion

        #region KEYWORDS
        [Keyword("VerifyNameExists", new String[] { "Contact Methods|TRUE" })]
        public void VerifyNameExists(String ExpanderName, string TrueOrFalse)
        {
            try
            {
                bool bFound = false;
                Initialize();

                foreach (IWebElement expander in mItems)
                {
                    IWebElement expanderHeader = expander.FindElement(By.XPath("./div[@class='header']/span[@class='title']"));
                    DlkBaseControl header = new DlkBaseControl("ExpanderHeader", expanderHeader);
                    if (header.GetValue().Trim().ToLower() == ExpanderName.Trim().ToLower())
                    {
                        bFound = true;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyNameExists() - " + ExpanderName , Convert.ToBoolean(TrueOrFalse), bFound);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyNameExists() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("Expand", new String[] { "Contact Methods" })]
        public void Expand(String ExpanderName)
        {
            try
            {
                bool bFound = false;
                Initialize();

                foreach (IWebElement expander in mItems)
                {
                    IWebElement expanderHeader = expander.FindElement(By.XPath("./div[@class='header']/span[@class='title']"));
                    DlkBaseControl header = new DlkBaseControl("ExpanderHeader", expanderHeader);
                    if (header.GetValue().Trim().ToLower() == ExpanderName.Trim().ToLower())
                    {
                        if (!expander.GetAttribute("CLASS").Contains("open"))
                        {
                            //if collapsed , expand
                            header.Click();
                            bFound = true;
                        }
                        else
                        {
                            bFound = true;
                            DlkLogger.LogInfo("Expand() - '" + ExpanderName + "' is already expanded.");
                        }
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("Expand() - '" + ExpanderName + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Expand() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("Collapse", new String[] { "Contact Methods" })]
        public void Collapse(String ExpanderName)
        {
            try
            {
                bool bFound = false;
                Initialize();

                foreach (IWebElement expander in mItems)
                {
                    IWebElement expanderHeader = expander.FindElement(By.XPath("./div[@class='header']/span[@class='title']"));
                    DlkBaseControl header = new DlkBaseControl("ExpanderHeader", expanderHeader);
                    if (header.GetValue().Trim().ToLower() == ExpanderName.Trim().ToLower())
                    {
                        if (expander.GetAttribute("CLASS").Contains("open"))
                        {
                            //if expanded , collapse
                            header.Click();
                            bFound = true;
                        }
                        else
                        {
                            bFound = true;
                            DlkLogger.LogInfo("Collapse() - '" + ExpanderName + "' is already collapsed.");
                        }
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("Collapse() - '" + ExpanderName + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Collapse() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("ClickOptions", new String[] { "Refresh|Contact Methods" })]
        public void ClickOptions(String OptionName, String ExpanderName)
        {
            try
            {
                bool bFound = false;
                string selectedActionClass = string.Empty;
                Initialize();

                foreach (IWebElement expander in mItems)
                {
                    IWebElement expanderHeader = expander.FindElement(By.XPath("./div[@class='header']/span[@class='title']"));
                    DlkBaseControl header = new DlkBaseControl("ExpanderHeader", expanderHeader);
                    if (header.GetValue().Trim().ToLower() == ExpanderName.Trim().ToLower())
                    {
                        if (OptionName.Trim().ToLower() == "refresh")
                            selectedActionClass = "/button[@class='refresh']";
                        else if (OptionName.Trim().ToLower() == "popout")
                            selectedActionClass = "/button[@class='popout']";
                        else if (OptionName.Trim().ToLower() == "help")
                            selectedActionClass = "/button[@class='help']";
                        else
                            selectedActionClass = "/button[@class='" + OptionName + "']";

                        IWebElement expanderAction = expander.FindElement(By.XPath(".//div[@class='actions']" + selectedActionClass));
                        if (expanderAction.Displayed)
                        {
                            bFound = true;
                            DlkBaseControl action = new DlkBaseControl("ActionButton", expanderAction);
                            action.Click();
                        }
                        else
                        {
                            bFound = false;
                        }
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("Action - " + OptionName + "' of " + ExpanderName + " not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickOptions() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyOptions", new String[] { "Refresh|Contact Methods" })]
        public void VerifyOptions(String OptionName, String ExpanderName, String TrueOrFalse)
        {
            try
            {
                bool bFound = false;
                string selectedActionClass = string.Empty;
                Initialize();

                foreach (IWebElement expander in mItems)
                {
                    IWebElement expanderHeader = expander.FindElement(By.XPath("./div[@class='header']/span[@class='title']"));
                    DlkBaseControl header = new DlkBaseControl("ExpanderHeader", expanderHeader);
                    if (header.GetValue().Trim().ToLower() == ExpanderName.Trim().ToLower())
                    {
                        if (OptionName.Trim().ToLower() == "refresh")
                            selectedActionClass = "/button[@class='refresh']";
                        else if (OptionName.Trim().ToLower() == "popout")
                            selectedActionClass = "/button[@class='popout']";
                        else if (OptionName.Trim().ToLower() == "help")
                            selectedActionClass = "/button[@class='help']";
                        else 
                            selectedActionClass = "/button[@class='"+ OptionName +"']";

                        try
                        {
                            IWebElement expanderAction = expander.FindElement(By.XPath(".//div[@class='actions']" + selectedActionClass));
                            if (expanderAction.Displayed)
                            {
                                bFound = true;
                            }
                            else
                            {
                                bFound = false;
                            }
                        }
                        catch (NoSuchElementException)
                        {
                            bFound = false;
                        }
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyOptions() : " + OptionName + " of " + ExpanderName, Convert.ToBoolean(TrueOrFalse), bFound);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyOptions() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        #endregion

        #region METHODS
        private List<IWebElement> GetItems()
        {
            List<IWebElement> headerItems = new List<IWebElement>();
            try
            {
                var expanderItems = this.mElement.FindElements(By.XPath(".//div[@id='secTemplate']"));
                List<IWebElement> expanderList = new List<IWebElement>();
                foreach (IWebElement item in expanderItems)
                {
                    if (item.Displayed)
                    {
                        expanderList.Add(item);
                    }
                }
                mItems = expanderList;
            }
            catch
            {
                //do nothing
            }
            return headerItems;
        }
        #endregion

    }
}
