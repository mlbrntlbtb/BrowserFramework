using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using OpenQA.Selenium;
using System.Drawing;

namespace AjeraTimeLib.DlkSystem
{
    public class DlkAjeraTimeBaseControl : DlkBaseControl
    {
        /// <summary>
        /// the element we are interacting with. Selenium deals with elements; we wrap logic into controls as that allows us to provide
        /// a practical experience (i.e. textboxes cannot do the same as buttons)
        /// </summary>
        public IList<IWebElement> mElementList;

        #region CONSTRUCTORS
        public DlkAjeraTimeBaseControl(string ControlName, string SearchType, string SearchValue)
            : base(ControlName, SearchType, SearchValue)
        {
        }

        public DlkAjeraTimeBaseControl(string ControlName, string SearchType, string[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
        }

        public DlkAjeraTimeBaseControl(string ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement)
        {
        }

        public DlkAjeraTimeBaseControl(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue)
        {
        }

        public DlkAjeraTimeBaseControl(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector)
            : base(ControlName, ExistingParentWebElement, CSSSelector)
        {
        }

        #endregion
        /// <summary>
        /// Used to find an element. An element must be found before being used.
        /// </summary>
        public void FindElements()
        {
            FindElements(iFindElementDefaultSearchMax); // look for upto n seconds by default
        }

        /// <summary>
        /// Used to find an element. An element must be found before being used.
        /// </summary>
        /// <param name="iSecToWait"></param>
        public void FindElements(int iSecToWait)
        {

            mElementList = null;
            Stopwatch mWatch = Stopwatch.StartNew();
            mWatch.Start();
            int i = 0, iMax = iSecToWait * 1000;
            Boolean bFound = false;
            while (mWatch.ElapsedMilliseconds < iMax)
            {
                try
                {
                    switch (mSearchType.ToLower())
                    {
                        case "id":
                            mElementList = DlkEnvironment.AutoDriver.FindElements(By.Id(mSearchValues[0]));
                            break;
                        case "name":
                            mElementList = DlkEnvironment.AutoDriver.FindElements(By.Name(mSearchValues[0]));
                            break;
                        case "css":
                            mElementList = DlkEnvironment.AutoDriver.FindElements(By.CssSelector(mSearchValues[0]));
                            break;
                        case "xpath":
                            mElementList = DlkEnvironment.AutoDriver.FindElements(By.XPath(mSearchValues[0]));
                            break;
                        case "classname":
                        case "class":
                            mElementList = DlkEnvironment.AutoDriver.FindElements(By.ClassName(mSearchValues[0]));
                            break;
                        default:
                            break;
                    }
                    if (mElementList.Count > 0) // we found and defined the control
                    {
                        if (DlkEnvironment.mIsMobile && !mElement.Displayed)
                        {
                            ScrollIntoViewUsingJavaScript();
                        }

                        bFound = true;
                        break;
                    }
                }
                catch
                {
                    DlkLogger.LogWarning("Couldn't find control [" + mControlName + "] .");
                }

                Thread.Sleep(100);
                i = i + (int)mWatch.ElapsedMilliseconds;
            }
            mWatch.Stop();

            if (!bFound)
            {
                throw new Exception("Couldn't find control [" + mControlName + "] within timeout: " + iSecToWait);
            }
        }

        public void InitializeSelectedElement(string RowNumber)
        {
            FindElements();
            int iTargetItemPos = Convert.ToInt32(RowNumber);
            if (iTargetItemPos > mElementList.Count)
            {
                throw new Exception("Index out of bounds.");
            }
            mElement = mElementList[iTargetItemPos - 1];
        }

        public Boolean SelectedElementExists(string RowNumber)
        {

            Boolean bExists = false;
            try
            {
                InitializeSelectedElement(RowNumber);
                if (mElement.Displayed)
                {
                    bExists = true;
                }
            }
            catch
            {
                bExists = false;
            }
            return bExists;
        }


        public void InitializeAllElements()
        {
            FindElements();
        }
    }
}
