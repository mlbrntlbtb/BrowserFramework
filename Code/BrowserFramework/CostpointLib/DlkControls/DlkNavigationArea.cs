using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace CostpointLib.DlkControls
{
    public class DlkNavigationArea : DlkBaseControl
    {
        private ReadOnlyCollection<IWebElement> mNodes;
        private String mStrMemberItemClass = "";

        public String MemberItemClass
        {
            get { return (mStrMemberItemClass); }
            set { mStrMemberItemClass = value; }
        }

       public DlkNavigationArea(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkNavigationArea(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkNavigationArea(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();            
        }

        public void Select(String sNode)
        {
            bool bFound = false;

            Initialize();

            mNodes = mElement.FindElements(By.XPath(mStrMemberItemClass));

            foreach (IWebElement aNode in mNodes)
            {
                if (aNode.Text.Trim().ToLower() == sNode.Trim().ToLower())
                {
                    bFound = true;
                    DlkBaseControl navNode = new DlkBaseControl("Menu", aNode);
                    if (DlkEnvironment.mBrowser.ToLower() == "safari")
                    {
                        navNode.ClickUsingJavaScript(false);
                    }
                    else
                    {
                        navNode.Click();
                    }
                    break;
                }
            }
            if (bFound)
            {
                DlkLogger.LogInfo("Successfully executed Select(). Control : " + mControlName + " : " + sNode);
            }
            else
            {
                throw new Exception("Select() failed. Control : " + mControlName + " : " + sNode);
            }
        }

        public bool VerifyNode(String Node)
        {
            bool bRet = false;

            Initialize();

            mNodes = mElement.FindElements(By.XPath(mStrMemberItemClass));
            foreach (IWebElement aNode in mNodes)
            {
                if (aNode.Text.ToLower() == Node.ToLower())
                {
                    bRet = true;
                    break;
                }
            }

            return bRet;
        }



    }
}
