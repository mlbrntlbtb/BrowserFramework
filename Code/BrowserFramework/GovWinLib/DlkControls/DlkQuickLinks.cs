using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("QuickLinks")]
    public class DlkQuickLinks : DlkBaseControl
    {
        private String mstrLinksCSS = "tr>td>a";
        private List<DlkBaseControl> mlstLinks;


        public DlkQuickLinks(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkQuickLinks(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkQuickLinks(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        /**public DlkQuickLinks(String ControlName, String FrameName, String SearchType, String SearchValue)
            : base(ControlName, FrameName, SearchType, SearchValue) { } **/

        public void Initialize()
        {
            mlstLinks = new List<DlkBaseControl>();
            FindElement();
            FindLinks();
        }

        private void FindLinks()
        {
            IList<IWebElement> lstLinkElements;
            lstLinkElements = mElement.FindElements(By.CssSelector(mstrLinksCSS));
            foreach (IWebElement linkElement in lstLinkElements)
            {
                mlstLinks.Add(new DlkBaseControl("Link", linkElement));
            }
        }

        [Keyword("Select", new String[] { "1|text|Link Caption|My Task Orders"})]
        public void Select(String LinkCaption)
        {
            bool bFound = false;
            String strActualLinks = "";

            Initialize();
            foreach(DlkBaseControl link in mlstLinks)
            {
                strActualLinks = strActualLinks + link.GetValue() + " ";
                if (link.GetValue().ToLower().Contains(LinkCaption.ToLower()))
                {
                    link.Click();
                    bFound = true;
                    break;
                }
            }
            if (bFound)
            {
                DlkLogger.LogInfo("Successfully executed Select(). Control : " + mControlName + " : " + LinkCaption);
            }
            else
            {
                throw new Exception("Select() failed. Control : " + mControlName + " : '" + LinkCaption +
                                        "' link not found. : Actual Links = " + strActualLinks);
            }
        }

        [RetryKeyword("VerifyLinkExists", new String[] {   "1|text|Link Caption|My Task Orders",
                                                            "2|text|Expected Value|TRUE"})]
        public void VerifyLinkExists(String LinkCaption, String TrueOrFalse)
        {
            String linkCaption = LinkCaption;
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    bool bFound = false;
                    String strActualLinks = "";

                    Initialize();
                    foreach (DlkBaseControl link in mlstLinks)
                    {
                        strActualLinks = strActualLinks + link.GetValue() + " ";
                        if (link.GetValue().ToLower().Contains(linkCaption.ToLower()))
                        {
                            bFound = true;
                            break;
                        }
                    }

                    DlkAssert.AssertEqual("VerifyLinkExists()", Convert.ToBoolean(expectedValue), bFound);
                }, new String[]{"retry"});
        }

        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
            {
                Initialize();
                /*Boolean bExists = Exists();

                if (bExists == Convert.ToBoolean(expectedValue))
                {
                    DlkLogger.LogInfo("VerifyExists() passed : Actual = " + Convert.ToString(bExists) + " : Expected = " + expectedValue);
                }
                else
                {
                    throw new Exception("VerifyExists() failed : Actual = " + Convert.ToString(bExists) + " : Expected = " + expectedValue));
                }*/
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
            }, new String[]{"retry"});
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

        [RetryKeyword("VerifyLinkItemCount", new String[] {"1|text|Link Caption|My Task Orders",
                                                            "2|text|Expected Count|3"})]
        public void VerifyLinkItemCount(String LinkCaption, String TrueOrFalse)
        {
            String linkCaption = LinkCaption;
            String expectedCount = TrueOrFalse;

            this.PerformAction(() =>
                {
                    bool bFound = false;
                    String strActualLinks = "";
                    String strLinkText = "";

                    try
                    {
                        Initialize();
                        foreach (DlkBaseControl link in mlstLinks)
                        {
                            strActualLinks = strActualLinks + link.GetValue() + " ";
                            strLinkText = link.GetValue().ToLower();
                            if (strLinkText.Contains(linkCaption.ToLower()))
                            {
                                String linkCount = Regex.Match(strLinkText, "\\(.*\\)").Value;
                                if (linkCount != "")
                                {
                                    double ActualCount = Convert.ToDouble(linkCount.Substring(1, linkCount.Length - 2));
                                    double ExpectedCount = Convert.ToDouble(expectedCount);
                                    DlkAssert.AssertEqual("VerifyLinkItemCount()", ActualCount, ExpectedCount);
                                }
                                else
                                {
                                    throw new Exception("VerifyLinkItemCount() failed. Link '" + linkCaption +
                                                    "' do not have item count. Actual link text '" + strLinkText + "'");
                                }
                                bFound = true;
                                break;
                            }
                        }
                        if (!bFound)
                        {
                            throw new Exception("VerifyLinkItemCount() failed. Control : " + mControlName + " : '" + linkCaption +
                                                    "' link not found. : Actual Links = " + strActualLinks);
                        }
                    }
                    catch (Exception e)
                    {
                            throw new Exception("VerifyLinkItemCount() failed. " + e.Message);                        
                    }
                }, new String[]{"retry"});
        }

        
    }
}

