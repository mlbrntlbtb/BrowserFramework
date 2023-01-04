using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using StormWebLib.System;

namespace StormWebLib.DlkControls
{
    [ControlType("SideBar")]
    public class DlkSideBar : DlkBaseControl
    {
        private String mTabItemsCSS = "div.icon_container";
        private String mTabItemsXPATH = ".//div[contains(@class,'nav-item app-item')]";
        private String mCategoryItemsXPATH = ".//div[contains(@class,'nav-category')]";

        public DlkSideBar(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkSideBar(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkSideBar(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkStormWebFunctionHandler.WaitScreenGetsReady();

            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }

        public new bool VerifyControlType()
        {
            FindElement();
            if (this.mElement.GetAttribute("class").ToLower().Contains("sidebar"))
            {
                return true;
            }
            else
            {
                try
                {
                    IWebElement parentElement = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'sidebar')]"));
                    return true;
                }
                catch (OpenQA.Selenium.NoSuchElementException)
                {
                    return false;
                }

            }
        }

        public new void AutoCorrectSearchMethod(ref string SearchType, ref string SearchValue)
        {
            try
            {
                DlkBaseControl mCorrectControl = new DlkBaseControl("Sidebar", "", "");
                bool mAutoCorrect = false;

                VerifyControlType();
                IWebElement parentSidebar = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'sidebar')]"));
                mCorrectControl = new DlkBaseControl("CorrectControl", parentSidebar);
                mAutoCorrect = true;

                if (mAutoCorrect)
                {
                    String mId = mCorrectControl.GetAttributeValue("id");
                    String mName = mCorrectControl.GetAttributeValue("name");
                    String mClassName = mCorrectControl.GetAttributeValue("class");
                    if (mId != null && mId != "")
                    {
                        SearchType = "ID";
                        SearchValue = mId;
                    }
                    else if (mName != null && mName != "")
                    {
                        SearchType = "NAME";
                        SearchValue = mName;
                    }
                    else if (mClassName != null && mClassName != "")
                    {
                        SearchType = "CLASSNAME";
                        SearchValue = mClassName.Split(' ').First();
                    }
                    else
                    {
                        SearchType = "XPATH";
                        SearchValue = mCorrectControl.FindXPath();
                    }
                }
            }
            catch
            {

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

        [Keyword("Select", new String[] { "1|text|Tab|Opportunity" })]
        public void Select(String Item)
        {
            Boolean bFound = false;
            try
            {
                for (int retryCount = 0; retryCount < this.iFindElementDefaultSearchMax; retryCount++)
                {
                    Initialize();
                    IWebElement mTab = null; 
                    IList<IWebElement> tabs = this.mElement.FindElements(By.CssSelector(mTabItemsCSS));
                    if (tabs.Count <= 0)
                    {
                        //XPATH for new UI
                        tabs = this.mElement.FindElements(By.XPath(mTabItemsXPATH));
                    }

                    for (int i = 0; i < tabs.Count; i++) //check title for legacy sidebar
                    {
                        if (tabs[i].GetAttribute("title").ToLower() == Item.ToLower())
                        {
                            bFound = true;
                            mTab = tabs[i];
                            break;
                        }                        
                    }

                    if (!bFound)
                    {
                        for (int i = 0; i < tabs.Count; i++) //check data-app-id for storm sidebar
                        {
                            if (!String.IsNullOrWhiteSpace(tabs[i].GetAttribute("data-app-id")) && (tabs[i].GetAttribute("data-app-id").ToLower().Trim() == Item.ToLower().Trim())) //check data-app-id attribute for new UI
                            {
                                bFound = true;
                                mTab = tabs[i];
                                break;
                            }
                        }
                    }

                    if (!bFound)
                    {
                        for (int i = 0; i < tabs.Count; i++) //check textcontent as last resort
                        {
                            if (tabs[i].Text.ToLower().Trim() == Item.ToLower().Trim())
                            {
                                bFound = true;
                                mTab = tabs[i];
                                break;
                            }
                        }
                    }
                    
                    if(bFound)
                    {
                        DlkBaseControl ctlTab = new DlkBaseControl("TabItem", mTab);
                        ctlTab.ScrollIntoViewUsingJavaScript();
                        ctlTab.Click();
                        DlkLogger.LogInfo("Select() passed.");
                        break;
                    }

                }
                if (!bFound)
                {
                    throw new Exception("Select() failed. '" + Item + "' not found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTabExists", new String[] {"1|text|Tab Caption|Opportunity", 
                                                              "2|text|Expected Value|True " })]
        public void VerifyTabExists(String TabCaption, String TrueOrFalse)
        {
            bool bFound = false;

            try
            {
                Initialize();
                IList<IWebElement> tabs = mElement.FindElements(By.CssSelector(mTabItemsCSS));
                if (tabs.Count <= 0)
                {
                    //XPATH for new UI
                    tabs = this.mElement.FindElements(By.XPath(mTabItemsXPATH));
                }
                foreach (IWebElement aTab in tabs)
                {
                    if (!String.IsNullOrWhiteSpace(aTab.GetAttribute("data-app-id")) && (aTab.GetAttribute("data-app-id").ToLower().Trim() == TabCaption.ToLower().Trim())) //check data-app-id attribute for new UI
                    {
                        if (aTab.Displayed)
                        {
                            bFound = true;
                        }
                        break;
                    }
                }                

                if (!bFound)
                {
                    foreach (IWebElement aTab in tabs)
                    {
                        if (aTab.GetAttribute("title").ToLower() == TabCaption.ToLower())
                        {
                            if (aTab.Displayed)
                            {
                                bFound = true;
                            }
                            break;
                        }
                    }
                }

                if (!bFound)
                {
                    foreach (IWebElement aTab in tabs)
                    {
                        if (aTab.Text.ToLower().Trim() == TabCaption.ToLower().Trim())
                        {
                            if (aTab.Displayed)
                            {
                                bFound = true;
                            }
                            break;
                        }
                    }
                }
                DlkAssert.AssertEqual("VerifyTabExists()", Convert.ToBoolean(TrueOrFalse), bFound);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTabExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCategoryExists", new String[] {"1|text|Category Caption|Opportunity", 
                                                              "2|text|Expected Value|True " })]
        public void VerifyCategoryExists(String Item, String TrueOrFalse)
        {
            bool bFound = false;

            try
            {
                Initialize();
                IList<IWebElement> categories = mElement.FindElements(By.XPath(mCategoryItemsXPATH));
                foreach (IWebElement aTab in categories)
                {
                    if (aTab.Text.ToLower().Trim() == Item.ToLower().Trim()) 
                    {
                        if (aTab.Displayed)
                        {
                            bFound = true;
                        }
                        break;
                    }
                }
                DlkAssert.AssertEqual("VerifyCategoryExists()", Convert.ToBoolean(TrueOrFalse), bFound);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCategoryExists() failed : " + e.Message, e);
            }
        }


    }
}
