//#define IOS_NATIVE_SUPPORT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Diagnostics;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;


namespace CommonLib.DlkControls
{
    /// <summary>
    /// This is commonly used as the base control for all other controls. It provides a basic interface with Selenium
    /// </summary>
    public class DlkBaseControl
    {
        public enum SwipeDirection
        {
            Left,
            Up,
            Right,
            Down
        }

        public enum SwipeOrigin
        {
            LeftTop,
            RightTop,
            CenterTop,
            LeftCenter,
            Center,
            RightCenter,
            LeftBottom,
            CenterBottom,
            RightBottom
        }

        private const int INT_SWIPE_WAIT_MS = 800;

        /// <summary>
        /// Pixel density of device. Note that if this cannot be obtained programmatically, should be written in config
        /// </summary>
        private const int INT_DEVICE_DENSITY = 160;

        /// <summary>
        /// this is a default time to search. it is public so it can be adjusted as needed
        /// </summary>
        public int iFindElementDefaultSearchMax { get; set; }

        /// <summary>
        /// an assignable list of elements
        /// </summary>
        public IList<IWebElement> mElms { get; set; }

        /// <summary>
        /// the element we are interacting with. Selenium deals with elements; we wrap logic into controls as that allows us to provide
        /// a practical experience (i.e. textboxes cannot do the same as buttons)
        /// </summary>
        public IWebElement mElement;
        
        /// <summary>
        /// the parent control where the control will be searched
        /// </summary>
        public DlkBaseControl mParentControl = null;

        /// <summary>
        /// the contol name we searched for... assigned when we construct a control
        /// </summary>
        public String mControlName { get; set; }

        /// <summary>
        /// used with mSearchValues to determine how we find the control... assigned when we contruct a control
        /// </summary>
        public String mSearchType = "";

        /// <summary>
        /// used with mSearchType to determine how we find the control... assigned when we contruct a control
        /// </summary>
        public String[] mSearchValues;

        /// <summary>
        /// used to store the original css styles before applying the highlight
        /// </summary>
        private String mOrigStyle;

        private Boolean IsDeclaredAsExistngWebElement = false;

        /// <summary>
        /// constructor for a DlkControl which acts as our interface to selenium logic
        /// </summary>
        /// <param name="ControlName"></param>
        /// <param name="SearchType"></param>
        /// <param name="SearchValue"></param>
        public DlkBaseControl(String ControlName, String SearchType, String SearchValue)
        {
            mControlName = ControlName;
            mSearchType = SearchType;
            mSearchValues = new String[] { SearchValue };
            iFindElementDefaultSearchMax = 40;
            if (DlkTestRunnerApi.mCancellationPending || DlkTestRunnerApi.mAbortionPending) // support for cancellation
            {
                throw new Exception("Test execution was canceled.");
            }
        }

        /// <summary>
        /// constructor for a DlkControl which acts as our interface to selenium logic
        /// </summary>
        /// <param name="ControlName"></param>
        /// <param name="SearchType"></param>
        /// <param name="SearchValues"></param>
        public DlkBaseControl(String ControlName, String SearchType, String[] SearchValues)
        {
            mControlName = ControlName;
            mSearchType = SearchType;
            mSearchValues = SearchValues;
            iFindElementDefaultSearchMax = 40;
            if (DlkTestRunnerApi.mCancellationPending || DlkTestRunnerApi.mAbortionPending) // support for cancellation
            {
                throw new Exception("Test execution was canceled.");
            }
        }

        /// <summary>
        /// constructor for a DlkControl which acts as our interface to selenium logic
        /// </summary>
        /// <param name="ControlName"></param>
        /// <param name="ExistingWebElement"></param>
        public DlkBaseControl(String ControlName, IWebElement ExistingWebElement)
        {
            mControlName = ControlName;
            mElement = ExistingWebElement;
            IsDeclaredAsExistngWebElement = true;
            iFindElementDefaultSearchMax = 40;
            if (DlkTestRunnerApi.mCancellationPending || DlkTestRunnerApi.mAbortionPending) // support for cancellation
            {
                throw new Exception("Test execution was canceled.");
            }
        }

        /// <summary>
        /// constructor for a DlkControl which acts as our interface to selenium logic
        /// </summary>
        /// <param name="ControlName"></param>
        /// <param name="ParentControl"></param>
        /// <param name="SearchType"></param>
        /// <param name="SearchValue"></param>
        public DlkBaseControl(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
        {
            mControlName = ControlName;
            mParentControl = ParentControl;
            mSearchType = SearchType;
            mSearchValues = new String[] { SearchValue };
            iFindElementDefaultSearchMax = 40;
            if (DlkTestRunnerApi.mCancellationPending || DlkTestRunnerApi.mAbortionPending) // support for cancellation
            {
                throw new Exception("Test execution was canceled.");
            }
        }

        /// <summary>
        /// constructor for a DlkControl which acts as our interface to selenium logic
        /// </summary>
        /// <param name="ControlName"></param>
        /// <param name="ParentControl"></param>
        /// <param name="SearchType"></param>
        /// <param name="SearchValue"></param>
        public DlkBaseControl(String ControlName, IWebElement ExistingParentWebElement, String CSSSelector)
        {
            mControlName = ControlName;
            mSearchValues = new String[] { CSSSelector };
            mElement = ExistingParentWebElement;
            IsDeclaredAsExistngWebElement = true;
            if (DlkTestRunnerApi.mCancellationPending || DlkTestRunnerApi.mAbortionPending) // support for cancellation
            {
                throw new Exception("Test execution was canceled.");
            }
        }

        /// <summary>
        /// Used to find an element. An element must be found before being used.
        /// </summary>
        public void FindElement()
        {
            FindElement(iFindElementDefaultSearchMax); // look for upto n seconds by default
        }

        /// <summary>
        /// Used to find an element. An element must be found before being used.
        /// </summary>
        /// <param name="iSecToWait"></param>
        public void FindElement(int iSecToWait)
        {
            if (!DlkEnvironment.mIsMobile && !DlkEnvironment.mSwitchediFrame)
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            }
            if (mParentControl != null)
            {
                FindElementFromParent(iSecToWait);
            }
            else
            {
                if (IsDeclaredAsExistngWebElement)
                {
                    return;
                }

                mElement = null;
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
                                mElement = DlkEnvironment.AutoDriver.FindElement(By.Id(mSearchValues[0]));
                                break;
                            case "iframe_xpath": //uses iframe id/name attributes to locate and switch to iframe
                                //string iframeName = mSearchValues[0].Split('_')[0];
                                //string searchVal = mSearchValues[0].Split('_')[1];
                                char delimeter = new char();

                                if (DlkEnvironment.mProductFolder == "CBI")
                                    delimeter = '~';
                                else
                                    delimeter = '_';

                                string iframeName = mSearchValues[0].Substring(0, mSearchValues[0].IndexOf(delimeter));
                                string searchVal = mSearchValues[0].Substring(mSearchValues[0].IndexOf(delimeter) + 1, mSearchValues[0].Length - mSearchValues[0].IndexOf(delimeter) - 1);
                                int n;
                                bool isNumeric = int.TryParse(iframeName, out n);
                                                               
                                IList<IWebElement> frames;
                                if (isNumeric)
                                {
                                    //mElement = DlkEnvironment.AutoDriver.SwitchTo().Frame(n).FindElement(By.XPath(searchVal));                               

                                    frames = DlkEnvironment.AutoDriver.FindElements(By.XPath("//iframe")).Where( x => x.Displayed).ToList();
                                    Thread.Sleep(1000);
                                    DlkEnvironment.AutoDriver.SwitchTo().Frame(frames[n]);
                                    mElement = DlkEnvironment.AutoDriver.FindElement(By.XPath(searchVal));
                                }
                                else
                                    mElement = DlkEnvironment.AutoDriver.SwitchTo().Frame(iframeName).FindElement(By.XPath(searchVal));
                                break;
                            case "iframe_nested_xpath": //uses iframe id/name attributes to locate and switch to nested iframes
                                string iframeName0 = mSearchValues[0].Substring(0, mSearchValues[0].IndexOf('_'));
                                string[] nestedFrames = iframeName0.Split('~');
                                string searchVal0 = mSearchValues[0].Substring(mSearchValues[0].IndexOf('_') + 1, mSearchValues[0].Length - mSearchValues[0].IndexOf('_') - 1);

                                foreach (string frm in nestedFrames)
                                {
                                    IList<IWebElement> frames0;
                                    int p;
                                    bool isNumeric0 = int.TryParse(frm, out p);
                                    if (isNumeric0)
                                    {
                                        frames0 = DlkEnvironment.AutoDriver.FindElements(By.XPath("//iframe"));
                                        Thread.Sleep(1000);
                                        DlkEnvironment.AutoDriver.SwitchTo().Frame(frames0[p]);
                                    }
                                    else
                                    {
                                        DlkEnvironment.AutoDriver.SwitchTo().Frame(frm);
                                    }
                                }
                                mElement = DlkEnvironment.AutoDriver.FindElement(By.XPath(searchVal0));
                                break;                            
                            case "iframe_nested_xpath_display": //uses iframe id/name attributes to locate and switch to nested displayed iframes
                                string iframeNames = mSearchValues[0].Substring(0, mSearchValues[0].IndexOf('_'));
                                string[] nestedFrameList = iframeNames.Split('~');
                                string searchValue = mSearchValues[0].Substring(mSearchValues[0].IndexOf('_') + 1, mSearchValues[0].Length - mSearchValues[0].IndexOf('_') - 1);

                                foreach (string frm in nestedFrameList)
                                {
                                    IList<IWebElement> frames0;
                                    int p;
                                    bool isNumeric0 = int.TryParse(frm, out p);
                                    if (isNumeric0)
                                    {
                                        frames0 = DlkEnvironment.AutoDriver.FindElements(By.XPath("//iframe")).Where(x => x.Displayed).ToList();
                                        Thread.Sleep(1000);
                                        DlkEnvironment.AutoDriver.SwitchTo().Frame(frames0[p]);
                                    }
                                    else
                                    {
                                        DlkEnvironment.AutoDriver.SwitchTo().Frame(frm);
                                    }
                                }
                                mElement = DlkEnvironment.AutoDriver.FindElement(By.XPath(searchValue));
                                break;
                            case "iframelocator_xpath": //uses xpath to find and switch to single iframe instead of ID/name attribute
                                iframeName = mSearchValues[0].Substring(0, mSearchValues[0].IndexOf('~'));
                                searchVal = mSearchValues[0].Substring(mSearchValues[0].IndexOf('~') + 1, mSearchValues[0].Length - mSearchValues[0].IndexOf('~') - 1);

                                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                                IWebElement frame = DlkEnvironment.AutoDriver.FindElement(By.XPath(iframeName));
                                DlkEnvironment.AutoDriver.SwitchTo().Frame(frame);
                                mElement = DlkEnvironment.AutoDriver.FindElement(By.XPath(searchVal));
                                break;
                            case "iframelocator_nested_xpath"://uses xpaths to find and switch to nested iframes instead of ID/name attributes
                                iframeName = mSearchValues[0].Substring(0, mSearchValues[0].IndexOf("_/"));
                                string[] nestedIframes = iframeName.Split('~');
                                searchVal = mSearchValues[0].Substring(mSearchValues[0].IndexOf("_/") + 1).Trim();

                                foreach (string frm in nestedIframes)
                                {
                                    Thread.Sleep(1000);
                                    frame = DlkEnvironment.AutoDriver.FindElement(By.XPath(frm));
                                    DlkEnvironment.AutoDriver.SwitchTo().Frame(frame);
                                }
                                mElement = DlkEnvironment.AutoDriver.FindElement(By.XPath(searchVal));
                                break;
                            case "linktext":
                                mElement = DlkEnvironment.AutoDriver.FindElement(By.LinkText(mSearchValues[0]));
                                break;
                            case "name":
                                mElement = DlkEnvironment.AutoDriver.FindElement(By.Name(mSearchValues[0]));
                                break;
                            case "css":
                                mElement = DlkEnvironment.AutoDriver.FindElement(By.CssSelector(mSearchValues[0]));
                                break;
                            case "xpath":
                                mElement = DlkEnvironment.AutoDriver.FindElement(By.XPath(mSearchValues[0]));
                                break;
                            case "classname":
                            case "class":
                                mElement = DlkEnvironment.AutoDriver.FindElement(By.ClassName(mSearchValues[0]));
                                break;
                            case "partiallinktext":
                                mElement = DlkEnvironment.AutoDriver.FindElement(By.PartialLinkText(mSearchValues[0]));
                                break;
                            case "tagname_text":
                                mElms = DlkEnvironment.AutoDriver.FindElements(By.TagName(mSearchValues[0]));
                                foreach (IWebElement mElm in mElms)
                                {
                                    if (mElm.Text == mSearchValues[1])
                                    {
                                        mElement = mElm;
                                        break;
                                    }
                                }
                                break;
                            case "tagname_attribute":
                                mElms = DlkEnvironment.AutoDriver.FindElements(By.TagName(mSearchValues[0]));
                                foreach (IWebElement mElm in mElms)
                                {
                                    if (mElm.GetAttribute(mSearchValues[1]).ToString() == mSearchValues[2])
                                    {
                                        mElement = mElm;
                                        break;
                                    }

                                }
                                break;
                            case "img_src":
                                mElms = DlkEnvironment.AutoDriver.FindElements(By.TagName("img"));
                                foreach (IWebElement mElm in mElms)
                                {
                                    if (mElm.GetAttribute("src") == null)
                                    {
                                        //skip
                                    }
                                    else if (mElm.GetAttribute("src").Contains(mSearchValues[0]))
                                    {
                                        mElement = mElm;
                                        break;
                                    }
                                }
                                break;
                            case "  ":
                                mElement = DlkEnvironment.AutoDriver.FindElement(By.Id(mSearchValues[0])).FindElement(By.TagName(mSearchValues[1]));
                                break;
                            case "parentid_childclass":
                                mElement = DlkEnvironment.AutoDriver.FindElement(By.Id(mSearchValues[0])).FindElement(By.ClassName(mSearchValues[1]));
                                break;
                            case "parentid_childcss":
                                mElement = DlkEnvironment.AutoDriver.FindElement(By.Id(mSearchValues[0])).FindElement(By.CssSelector(mSearchValues[1]));
                                break;
                            case "parentclass_childtag":
                                mElement = DlkEnvironment.AutoDriver.FindElement(By.ClassName(mSearchValues[0])).FindElement(By.TagName(mSearchValues[1]));
                                break;
                            case "class_display":
                                mElms = DlkEnvironment.AutoDriver.FindElements(By.ClassName(mSearchValues[0]));
                                foreach (IWebElement mElm in mElms)
                                {
                                    if (mElm.Displayed == true)
                                    {
                                        mElement = mElm;
                                        break;
                                    }
                                }
                                break;
                            case "xpath_display":
                                mElms = DlkEnvironment.AutoDriver.FindElements(By.XPath(mSearchValues[0]));
                                bool bDisplayed = false;

                                foreach (IWebElement mElm in mElms)
                                {
                                    if (DlkEnvironment.mIsMobile && !mElm.Displayed)
                                    {
                                        try
                                        {
                                            new DlkBaseControl("FoundElement", mElm).ScrollIntoViewUsingJavaScript();
                                        }
                                        catch
                                        {
                                            // do nothing if problem encountered on scroll 
                                        }
                                    }
                                    if (mElm.Displayed)
                                    {
                                        mElement = mElm;
                                        bDisplayed = true;
                                        break;
                                    }
                                }
                                if (!DlkEnvironment.mIsMobile && !bDisplayed) //bypass this check for mobile
                                {
                                    foreach (IWebElement mElm in mElms)
                                    {
                                        if (mElm.GetCssValue("display") != "none")
                                        {
                                            mElement = mElm;
                                            break;
                                        }
                                    }
                                }
                                break;
                            case "multiple":
                                List<By> bys = new List<By>();
                                bys.AddRange(mSearchValues.Select(x => GetBy(x)));
                                mElement = DlkEnvironment.AutoDriver.FindElement(bys.ToArray());
                                break;
                            case "iframe_xpath_multiple_return":

                                string iframeName2 = mSearchValues[0].Substring(0, mSearchValues[0].IndexOf('_'));
                                string searchVal2 = mSearchValues[0].Substring(mSearchValues[0].IndexOf('_') + 1, mSearchValues[0].Length - mSearchValues[0].IndexOf('_') - 1);

                                int n2;
                                bool isNumeric2 = int.TryParse(iframeName2, out n2);

                                if (isNumeric2)
                                {
                                    //mElement = DlkEnvironment.AutoDriver.SwitchTo().Frame(n).FindElement(By.XPath(searchVal));                               

                                    frames = DlkEnvironment.AutoDriver.FindElements(By.XPath("//iframe")).Where(x => x.Displayed).ToList();
                                    Thread.Sleep(1000);
                                    DlkEnvironment.AutoDriver.SwitchTo().Frame(frames[n2]);
                                    mElms = DlkEnvironment.AutoDriver.FindElements(By.XPath(searchVal2));
                                    mElement = mElms[0];
                                }
                                else
                                {
                                    mElms = DlkEnvironment.AutoDriver.SwitchTo().Frame(iframeName2).FindElements(By.XPath(searchVal2));
                                    mElement = mElms[0];
                                }
                                break;
                            default:
                                break;
                        }
                        if (mElement != null) // we found and defined the control
                        {
                            //DlkLogger.LogInfo("Successfully identified control: " + mControlName + ", Time to identify: " + i.ToString() +"ms");
#if IOS_NATIVE_SUPPORT
                            if (DlkEnvironment.mIsMobile && !(DlkEnvironment.AutoDriver as AppiumDriver<AppiumWebElement>).Context.Contains("NATIVE") && !mElement.Displayed)
#else
                            if(DlkEnvironment.mIsMobile && !mElement.Displayed)
#endif
                            {
                                ScrollIntoViewUsingJavaScript();
                            }
                            //if (mElement.Displayed == true)
                            //{
                            bFound = true;
                            break;
                            //}
                        }
                        else /* Log if element is not found */
                        {
                            DlkLogger.LogWarning("Couldn't find control [" + mControlName + "] .");
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
        }
        
        /// <summary>
        /// Used to find an element from a parent element. An element must be found before being used.
        /// </summary>
        /// <param name="iSecToWait"></param>
        public void FindElementFromParent(int iSecToWait)
        {
            mParentControl.FindElement();
            mElement = null;
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
                            mElement = mParentControl.mElement.FindElement(By.Id(mSearchValues[0]));
                            break;
                        case "linktext":
                            mElement = mParentControl.mElement.FindElement(By.LinkText(mSearchValues[0]));
                            break;
                        case "name":
                            mElement = mParentControl.mElement.FindElement(By.Name(mSearchValues[0]));
                            break;
                        case "css":
                            mElement = mParentControl.mElement.FindElement(By.CssSelector(mSearchValues[0]));
                            break;
                        case "xpath":
                            mElement = mParentControl.mElement.FindElement(By.XPath(mSearchValues[0]));
                            break;
                        case "classname":
                        case "class":
                            mElement = mParentControl.mElement.FindElement(By.ClassName(mSearchValues[0]));
                            break;
                        case "partiallinktext":
                            mElement = mParentControl.mElement.FindElement(By.PartialLinkText(mSearchValues[0]));
                            break;
                        case "tagname_text":
                            mElms = mParentControl.mElement.FindElements(By.TagName(mSearchValues[0]));
                            foreach (IWebElement mElm in mElms)
                            {
                                if (mElm.Text == mSearchValues[1])
                                {
                                    mElement = mElm;
                                    break;
                                }
                            }
                            break;
                        case "tagname_attribute":
                            mElms = mParentControl.mElement.FindElements(By.TagName(mSearchValues[0]));
                            foreach (IWebElement mElm in mElms)
                            {
                                if (mElm.GetAttribute(mSearchValues[1]).ToString() == mSearchValues[2])
                                {
                                    mElement = mElm;
                                    break;
                                }

                            }
                            break;
                        case "img_src":
                            mElms = mParentControl.mElement.FindElements(By.TagName("img"));
                            foreach (IWebElement mElm in mElms)
                            {
                                if (mElm.GetAttribute("src") == null)
                                {
                                    //skip
                                }
                                else if (mElm.GetAttribute("src").Contains(mSearchValues[0]))
                                {
                                    mElement = mElm;
                                    break;
                                }
                            }
                            break;
                        case "  ":
                            mElement = mParentControl.mElement.FindElement(By.Id(mSearchValues[0])).FindElement(By.TagName(mSearchValues[1]));
                            break;
                        case "parentid_childclass":
                            mElement = mParentControl.mElement.FindElement(By.Id(mSearchValues[0])).FindElement(By.ClassName(mSearchValues[1]));
                            break;
                        case "parentid_childcss":
                            mElement = mParentControl.mElement.FindElement(By.Id(mSearchValues[0])).FindElement(By.CssSelector(mSearchValues[1]));
                            break;
                        case "parentclass_childtag":
                            mElement = mParentControl.mElement.FindElement(By.ClassName(mSearchValues[0])).FindElement(By.TagName(mSearchValues[1]));
                            break;
                        case "class_display":
                            mElms = mParentControl.mElement.FindElements(By.ClassName(mSearchValues[0]));
                            foreach (IWebElement mElm in mElms)
                            {
                                if (mElm.GetCssValue("display") != "none")
                                {
                                    mElement = mElm;
                                    break;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    if (mElement != null) // we found and defined the control
                    {
                        if (mElement.Displayed == true)
                        {
                            bFound = true;
                            break;
                        }
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

        /// <summary>
        /// According to Selenium doc, this will find the object using coordinates 
        /// </summary>
        public void ClickByObjectCoordinates()
        {
            FindElement();
            OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
            mAction.Click(mElement).Build().Perform();
        }

        /// <summary>
        /// Gets a point for where the object is
        /// </summary>
        /// <param name="bCenterTop"></param>
        /// <returns></returns>
        public Point GetElementLocationOnScreen(Boolean bCenterTop)
        {
            FindElement();
            // off set element location by window position
            Point mWindowPos = new Point(0, 0);
            if (DlkEnvironment.mBrowser.ToLower() != "chrome")
            {
                mWindowPos = DlkEnvironment.AutoDriver.Manage().Window.Position;
            }
            //DlkLogger.LogDebug("WinPos", mWindowPos);

            Point mPoint = mElement.Location;
            //DlkLogger.LogDebug("Element", mPoint);

            mPoint.Offset(mWindowPos);
            //DlkLogger.LogDebug("Element after position offset", mPoint);

            // adjust to center of element
            if (bCenterTop)
            {
                mPoint.Offset(mElement.Size.Width / 2, 0);
            }
            //DlkLogger.LogDebug("Element after center offset", mPoint);

            int iOffSetHeight = GetWindowHeightOffSet();
            mPoint.Offset(0, iOffSetHeight);

            return mPoint;
        }

        /// <summary>
        /// uses java script to get an offset height
        /// </summary>
        /// <returns></returns>
        public int GetWindowHeightOffSet()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
            int iOuterHeight = 0, iInnerHeight = 0, iOffSetHeight = 0;
            iOuterHeight = Convert.ToInt32(jse.ExecuteScript("return window.outerHeight"));
            iInnerHeight = Convert.ToInt32(jse.ExecuteScript("return window.innerHeight"));
            iOffSetHeight = iOuterHeight - iInnerHeight;
            return iOffSetHeight;
        }

        /// <summary>
        /// Perfroms a mouse over the element
        /// </summary>
        public void MouseOver()
        {
            Cursor.Position = new Point(0, 0);
            if (DlkEnvironment.mBrowser.ToLower() == "firefox")
            {
                MouseOverFirefox();
            }
            else if(DlkEnvironment.mBrowser.ToLower() == "safari")
            {
                MouseOverUsingJavaScript();
            }
            else
            {
                MouseOverStandard();
            }
        }

        /// <summary>
        /// standard mouse over code using selenium interactions
        /// </summary>
        private void MouseOverStandard()
        {
            FindElement();
            OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
            mAction.MoveToElement(mElement).Build().Perform();
        }

        /// <summary>
        /// firefox specific mouse over code
        /// </summary>
        private void MouseOverFirefox()
        {
            // Firefox works sporadically; we have to try a few things :)
            DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.CurrentWindowHandle);
            DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
            FindElement();

            // attempt to move mouse to correctplace
            Point mPoint = GetElementLocationOnScreen(true);
            Cursor.Position = new Point(0, 0);
            Thread.Sleep(250);
            Cursor.Position = mPoint;
            Thread.Sleep(250);

            OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
            for (int i = 1; i <= 5; i++)
            {
                mAction.MoveToElement(mElement).Perform();
                Thread.Sleep(100);
            }
            Thread.Sleep(250);
        }

        /// <summary>
        /// Perfroms a mouse over the element with an offset value
        /// </summary>
        public void MouseOverOffset(int x, int y)
        {
            Cursor.Position = new Point(0, 0);
            Thread.Sleep(DlkEnvironment.mMediumWaitMs);
            FindElement();
            Point mPoint = GetElementLocationOnScreen(true);
            Cursor.Position = new Point(0, 0);
            mPoint.Offset(x, y);
            Cursor.Position = mPoint;
            OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
            for (int i = 1; i <= 5; i++)
            {
                mAction.MoveToElement(mElement).Perform();
                Thread.Sleep(100);
            }
        }

        public void MouseOverUsingJavaScript()
        {
            FindElement();
            String mouseOverScript = "if(document.createEvent){" +
                                        " var evObj = document.createEvent('MouseEvents');" +
                                        " evObj.initEvent('mouseover', true, false);" +
                                        " arguments[0].dispatchEvent(evObj);" +
                                        "}" +
                                        "else if(document.createEventObject){" +
                                        " arguments[0].fireEvent('onmouseover');" +
                                        "}";
            //String mouseOverScript = "return arguments[0].mouseover();";
            IJavaScriptExecutor js = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
            js.ExecuteScript(mouseOverScript, mElement);
            DlkLogger.LogInfo("MouseOverUsingJavaScript() completed.");
        }

        /// <summary>
        /// selenium interactions code to click and hold an element
        /// </summary>
        public void ClickAndHold()
        {
            FindElement();
            if (DlkEnvironment.mIsMobile)
            {
                TapAndHold(3000);
            }
            else
            {
                OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                mAction.ClickAndHold(mElement).Build().Perform();
            }
            DlkLogger.LogInfo("ClickAndHold() completed.");
        }

        public void DragAndDropToElement(IWebElement target)
        {
            FindElement();
            OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
            //mAction.DragAndDrop(mElement, target).Build().Perform();
            mAction.ClickAndHold(mElement).MoveToElement(target).Release().Build().Perform();
        }

        public void DragAndDropToOffset(int offsetX, int offsetY)
        {
            FindElement();
            OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
            //mAction.DragAndDropToOffset(mElement, offsetX, offsetY).Build().Perform();
            mAction.ClickAndHold(mElement).MoveByOffset(offsetX, offsetY).Release().Build().Perform();
        }

        /// <summary>
        /// clicks the element if it exists
        /// </summary>
        /// <param name="iMaxTimeToWaitSec"></param>
        /// <param name="bError"></param>
        public void ClickIfExists(int iMaxTimeToWaitSec, Boolean bError)
        {
            if (Exists(iMaxTimeToWaitSec))
            {
                Click();
            }
            else
            {
                if (bError)
                {
                    throw new Exception("Expected control to exist. Control: " + mControlName + ", Timeout: " + iMaxTimeToWaitSec.ToString());
                }
                else
                {
                    DlkLogger.LogInfo("Control does not exist. Control: " + mControlName + ", Timeout: " + iMaxTimeToWaitSec.ToString());
                }
            }
        }

        /// <summary>
        /// clicks the element if it exists
        /// </summary>
        /// <param name="iMaxTimeToWaitSec"></param>
        /// <param name="bError">Throw or Log Error Info</param>
        public void ClickIfExistsAndEnabled(int iMaxTimeToWaitSec, Boolean bError)
        {
            if (ExistsAndEnabled(iMaxTimeToWaitSec))
            {
                mElement.Click();
            }
            else
            {
                if (bError)
                {
                    throw new Exception("Expected control does not exist/enabled. Control: " + mControlName + ", Timeout: " + iMaxTimeToWaitSec.ToString());
                }
                else
                {
                    DlkLogger.LogInfo("Control does not exist/disabled. Control: " + mControlName + ", Timeout: " + iMaxTimeToWaitSec.ToString());
                }
            }
        }

        private void ClickElement()
        {
            
            mElement.Click();
        }

        private void ClickElement(int offsetX, int offsetY)
        {
            FindElement();
            OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
            mAction.MoveToElement(mElement, offsetX, offsetY).Click().Perform();
        }

        public void ClickElementIfExistsAndEnabled(int offsetX, int offsetY, bool bError)
        {
            FindElement();
            if (mElement.Displayed && mElement.Enabled)
            {
                OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                mAction.MoveToElement(mElement, offsetX, offsetY).Click().Perform();
            }
            else
            {
                if (bError)
                {
                    throw new Exception("Expected control does not exist/enabled. Control: " + mControlName);
                }
                else
                {
                    DlkLogger.LogInfo("Control does not exist/disabled. Control: " + mControlName);
                }
            }
        }

        /// <summary>
        /// Finds and clicks the element
        /// </summary>
        public void Click()
        {
            //I would need to track the browser title for the UIAutomation to get the upload/download dialog.
            DlkEnvironment.mPreviousTitle = DlkEnvironment.AutoDriver.Title;
            FindElement();
            if (DlkEnvironment.mIsMobile)
            {
                Tap();
            }
            else
            {
                ClickElement();
            }
            
            DlkLogger.LogInfo("Successfully executed Click() : " + mControlName);
        }

        /// <summary>
        /// finds and clicks the element at a specific point
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        public void Click(int offsetX, int offsetY)
        {
            DlkEnvironment.mPreviousTitle = DlkEnvironment.AutoDriver.Title;
            if(DlkEnvironment.mIsMobile)
            {
                FindElement();
                Tap(offsetX, offsetY);
            }
            else
            {
                ClickElement(offsetX, offsetY);
            
            }
            DlkLogger.LogInfo("Successfully executed Click(). offsetX: " + offsetX.ToString() + ", offsetY: " + offsetY.ToString() + ", Control: " + mControlName);
        }

        /// <summary>
        /// clicks the element and waits n seconds
        /// </summary>
        /// <param name="iSecToWait"></param>
        public void Click(int iSecToWait)
        {
            DlkEnvironment.mPreviousTitle = DlkEnvironment.AutoDriver.Title;
            Click(Convert.ToDouble(iSecToWait));
        }

        /// <summary>
        /// clicks the element and waits n seconds
        /// </summary>
        /// <param name="dbSecToWait"></param>
        public void Click(double dbSecToWait)
        {
            try
            {
                Click();
            }
            catch (Exception e)
            {
                //Known issue with Chrome driver. When an element overlaps the desired element to be clicked, it will render the desired element to be unclickable.
                if(e.Message.Contains("Element is not clickable at point"))
                {
                    Click(5, 5);
                }
                else
                {
                    throw e;
                }
            }
            int iWait = Convert.ToInt32(dbSecToWait * 1000);
            Thread.Sleep(iWait);
        }

        /// <summary>
        /// clicks the element using javascript instead of selenium core logic
        /// </summary>
        public void ClickUsingJavaScript(bool useJQuery = true)
        {
            IJavaScriptExecutor jse = DlkEnvironment.AutoDriver as IJavaScriptExecutor;
            String script = useJQuery ? "$(arguments[0]).click()" : "arguments[0].click()";
            jse.ExecuteScript(script, mElement);
            DlkLogger.LogInfo("Successfully executed ClickUsingJavaScript() : " + mControlName);
        }

        public void Press(int NumSeconds)
        {
            FindElement();
            if (DlkEnvironment.mIsMobile)
            {
                TapAndHold(NumSeconds);
            }
            else
            {

            }
        }

        private void TapAndHold(int numSeconds=-1)
        {
            AppiumDriver<AppiumWebElement> appiumDriver = (AppiumDriver<AppiumWebElement>)DlkEnvironment.AutoDriver;
                        Point elementCoord = GetNativeViewCenterCoordinates();

            // Adjust to device edge if negative coordinates
            elementCoord.X = elementCoord.X >= 0 ? elementCoord.X : 0;

            if (DlkEnvironment.mBrowser.ToLower() == "ios") //depending on the version of iOS, we need to consider the servicebar in computing the Y coordinate
            {
                int serviceBarHeight = 0;
                //if (Double.Parse((string)appiumDriver.Capabilities.GetCapability("platformVersion")) >= 8)
                //{
                //    serviceBarHeight = 0;
                //}
                elementCoord.Y = elementCoord.Y <= DlkEnvironment.mDeviceHeight - serviceBarHeight ? elementCoord.Y : DlkEnvironment.mDeviceHeight - serviceBarHeight - 1;
            }
            else
            {
                elementCoord.Y = elementCoord.Y <= DlkEnvironment.mDeviceHeight ? elementCoord.Y : DlkEnvironment.mDeviceHeight - 1;
            }

            //Switch to NativeView
            DlkEnvironment.SetContext("NATIVE");

            TouchAction tapAction = new TouchAction(appiumDriver);

            if (numSeconds <= 0)
            {
                tapAction.LongPress(mElement).Perform();
                //tapAction.LongPress(elementCoord.X, elementCoord.Y).Perform();
            }
            else
            {
                //tapAction.Press(mElement).Wait(numSeconds).Perform();
                if (DlkEnvironment.mBrowser.ToLower() == "ios") //depending on the version of iOS, we need to consider the servicebar in computing the Y coordinate
                {
                    tapAction.Press(elementCoord.X, elementCoord.Y).Wait(numSeconds).Release().Perform();
                }
                else
                {
                    /* Temporarily no release, on Android broken appium implementation of of press with wait then release */
                    tapAction.Press(elementCoord.X, elementCoord.Y).Perform();
                }
            }

            //Revert back to WebView
            DlkEnvironment.SetContext("WEBVIEW");
        }

        private void MouseDown(int numSeconds=-1)
        {

        }
        
        public void Tap()
        {
            AppiumDriver<AppiumWebElement> appiumDriver = (AppiumDriver<AppiumWebElement>) DlkEnvironment.AutoDriver;
            bool isNative = appiumDriver.Context.Contains("NATIVE");

            Point elementCoord = GetNativeViewCenterCoordinates();
            if (!isNative)
            {
                // Adjust to device edge if negative coordinates
                elementCoord.X = elementCoord.X >= 0 ? elementCoord.X : 0;
                //Switch to NativeView
                DlkEnvironment.SetContext("NATIVE");
            }

            TouchAction tapAction = new TouchAction(appiumDriver);
            tapAction.Tap(elementCoord.X, elementCoord.Y);
            appiumDriver.PerformTouchAction(tapAction);

            if (!isNative)
            {
                //Revert back to WebView
                DlkEnvironment.SetContext("WEBVIEW");
            }
        }

        public void Tap(int offsetX, int offsetY)
        {
            AppiumDriver<AppiumWebElement> appiumDriver = (AppiumDriver<AppiumWebElement>)DlkEnvironment.AutoDriver;
            bool isNative = appiumDriver.Context.Contains("NATIVE");

            Point elementCoord = GetNativeViewCenterCoordinates();
            if (!isNative)
            {
                // Adjust to device edge if negative coordinates
                elementCoord.X = elementCoord.X >= 0 ? elementCoord.X : 0;
                //Switch to NativeView
                DlkEnvironment.SetContext("NATIVE");
            }

            TouchAction tapAction = new TouchAction(appiumDriver);
            tapAction.Tap(elementCoord.X + offsetX, elementCoord.Y + offsetY);
            appiumDriver.PerformTouchAction(tapAction);

            if (!isNative)
            {
                //Revert back to WebView
                DlkEnvironment.SetContext("WEBVIEW");
            }
        }

        public static void TapInSuccession(List<Point> NativeLocationsToTap, int WaitInBetweenTapsInMs = 500)
        {
            AppiumDriver<AppiumWebElement> appiumDriver = (AppiumDriver<AppiumWebElement>)DlkEnvironment.AutoDriver;
            bool isNative = appiumDriver.Context.Contains("NATIVE");

            if (!isNative)
            {
                //Switch to NativeView
                DlkEnvironment.SetContext("NATIVE");
            }

            foreach (Point pt in NativeLocationsToTap)
            {
                try
                {
                    TouchAction tapAction = new TouchAction(appiumDriver);
                    tapAction.Tap(pt.X, pt.Y);
                    appiumDriver.PerformTouchAction(tapAction);
                    Thread.Sleep(WaitInBetweenTapsInMs);
                }
                catch
                {
                    throw new Exception("Error encountered tapping at [" + pt.X + "," + pt.Y + "]");
                }
            }

            if (!isNative)
            {
                //Revert back to WebView
                DlkEnvironment.SetContext("WEBVIEW");
            }
        }

        //TODO: Need to polish up the coordinates
        public void Swipe(string direction, int distance)
        {
            AppiumDriver<AppiumWebElement> appiumDriver = (AppiumDriver<AppiumWebElement>)DlkEnvironment.AutoDriver;
            Point elementCoord = GetNativeViewCenterCoordinates();
            

            //Switch to NativeView
            DlkEnvironment.SetContext("NATIVE");
            int iHScroll = 0;
            int iVScroll = 0;

            switch (direction)
            {
                case "up":
                    iVScroll = elementCoord.Y - 1; 
                    break;
                case "down":
                    iVScroll = DlkEnvironment.mDeviceHeight - elementCoord.Y - 1;
                    break;
                case "left":
                    iHScroll = elementCoord.X - 1;
                    break;
                case "right":
                    iHScroll = DlkEnvironment.mDeviceWidth - elementCoord.X - 1;
                    break;
                default:
                    throw new Exception("Swipe() failed. Invalid direction '" + direction + "'.");
            }
            Double dDistance = Convert.ToDouble(distance);
            while (dDistance > 0)
            {
                TouchAction swipeAction = new TouchAction(appiumDriver);

                double dXMovement = 0;
                double dYMovement = 0;
                if (direction == "up")
                {
                    if (dDistance < iVScroll)
                    {
                        dYMovement = 0 - dDistance;
                        dDistance = dDistance - dDistance;
                    }                        
                    else
                    {
                        dYMovement = 0 - iVScroll;
                        dDistance = dDistance - iVScroll;
                    }
                }
                else if (direction == "down")
                {
                    if (dDistance < iVScroll)
                    {
                        dYMovement = dDistance;
                        dDistance = dDistance - dDistance;
                    }
                    else
                    {
                        dYMovement = iVScroll;
                        dDistance = dDistance - iVScroll;
                    }

                }
                else if (direction == "left")
                {
                    if (dDistance < iHScroll)
                    {
                        dXMovement = 0 - dDistance;
                        dDistance = dDistance - dDistance;
                    }
                    else
                    {
                        dXMovement = 0 - iHScroll;
                        dDistance = dDistance - iHScroll;
                    }
                    
                }
                else if (direction == "right")
                {
                    if (dDistance < iHScroll)
                    {
                        dXMovement = dDistance;
                        dDistance = dDistance - dDistance;
                    }
                    else
                    {
                        dXMovement = iHScroll;
                        dDistance = dDistance - iHScroll;
                    }
                }
                swipeAction.Press(elementCoord.X, elementCoord.Y).Wait(INT_SWIPE_WAIT_MS).MoveTo(
                    dXMovement, dYMovement).Release();
                appiumDriver.PerformTouchAction(swipeAction);

            }

            //Revert back to WebView
            DlkEnvironment.SetContext("WEBVIEW");
        }

        public void Swipe(SwipeDirection Direction, int Distance, SwipeOrigin Origin=SwipeOrigin.Center)
        {
            AppiumDriver<AppiumWebElement> appiumDriver = (AppiumDriver<AppiumWebElement>)DlkEnvironment.AutoDriver;
            TouchAction swipeAction = new TouchAction(appiumDriver);
            Point elementCenterCoordinates = GetNativeViewCenterCoordinates();
            Point elementTopCoordinates = GetNativeViewCoordinates();
            Point elementBottomCoordinates = ConvertToNativeViewCoordinates(mElement.Location.X + mElement.Size.Width, mElement.Location.Y + mElement.Size.Height);

            /* Determine origin point based from parameter */
            Point swipeOrigin = new Point();
            switch (Origin)
            {
                case SwipeOrigin.CenterBottom:
                    if (elementBottomCoordinates.X > DlkEnvironment.mDeviceWidth|| elementTopCoordinates.X < 0)
                    {
                        int visibleRightX = elementBottomCoordinates.X > DlkEnvironment.mDeviceWidth ? DlkEnvironment.mDeviceWidth: elementBottomCoordinates.X;
                        int visibleLeftX = elementTopCoordinates.X < 0 ? 0 : elementTopCoordinates.X;
                        swipeOrigin.X = (visibleRightX - visibleLeftX) / 2;
                    }
                    else
                    {
                        swipeOrigin.Y = elementCenterCoordinates.Y;
                    }
                    swipeOrigin.Y = elementBottomCoordinates.Y > DlkEnvironment.mDeviceHeight ? DlkEnvironment.mDeviceHeight - 1 : elementBottomCoordinates.Y;
                    break;
                case SwipeOrigin.CenterTop:
                    if (elementBottomCoordinates.X > DlkEnvironment.mDeviceWidth || elementTopCoordinates.X < 0)
                    {
                        int visibleRightX = elementBottomCoordinates.X > DlkEnvironment.mDeviceWidth ? DlkEnvironment.mDeviceWidth : elementBottomCoordinates.X;
                        int visibleLeftX = elementTopCoordinates.X < 0 ? 0 : elementTopCoordinates.X;
                        swipeOrigin.X = (visibleRightX - visibleLeftX) / 2;
                    }
                    else
                    {
                        swipeOrigin.Y = elementCenterCoordinates.Y;
                    }
                    swipeOrigin.Y = elementTopCoordinates.Y < 0 ? 1 : elementTopCoordinates.Y;
                    break;
                case SwipeOrigin.LeftBottom:
                    swipeOrigin.X = elementTopCoordinates.X < 0 ? 1 : elementTopCoordinates.X;
                    swipeOrigin.Y = elementBottomCoordinates.Y > DlkEnvironment.mDeviceHeight ? DlkEnvironment.mDeviceHeight - 1 : elementBottomCoordinates.Y;
                    break;
                case SwipeOrigin.LeftCenter:
                    swipeOrigin.X = elementTopCoordinates.X < 0 ? 1 : elementTopCoordinates.X;
                    if (elementBottomCoordinates.Y > DlkEnvironment.mDeviceHeight || elementTopCoordinates.Y < 0)
                    {
                        int visibleBottomY = elementBottomCoordinates.Y > DlkEnvironment.mDeviceHeight ? DlkEnvironment.mDeviceHeight : elementBottomCoordinates.Y;
                        int visibleTopY = elementTopCoordinates.Y < 0 ? 0 : elementTopCoordinates.Y;
                        swipeOrigin.Y = (visibleBottomY - visibleTopY) / 2;
                    }
                    else
                    {
                        swipeOrigin.Y = elementCenterCoordinates.Y;
                    }
                    break;
                case SwipeOrigin.LeftTop:
                    swipeOrigin.X = elementTopCoordinates.X < 0 ? 1 : elementTopCoordinates.X;
                    swipeOrigin.Y = elementTopCoordinates.Y < 0 ? 1 : elementTopCoordinates.Y;
                    break;
                case SwipeOrigin.RightBottom:
                    swipeOrigin.X = elementBottomCoordinates.X > DlkEnvironment.mDeviceWidth ? DlkEnvironment.mDeviceWidth - 1 : elementBottomCoordinates.X;
                    swipeOrigin.Y = elementBottomCoordinates.Y > DlkEnvironment.mDeviceHeight ? DlkEnvironment.mDeviceHeight - 1 : elementBottomCoordinates.Y;
                    break;
                case SwipeOrigin.RightCenter:
                    swipeOrigin.X = elementBottomCoordinates.X > DlkEnvironment.mDeviceWidth ? DlkEnvironment.mDeviceWidth - 1 : elementBottomCoordinates.X;
                    if (elementBottomCoordinates.Y > DlkEnvironment.mDeviceHeight || elementTopCoordinates.Y < 0)
                    {
                        int visibleBottomY = elementBottomCoordinates.Y > DlkEnvironment.mDeviceHeight ? DlkEnvironment.mDeviceHeight : elementBottomCoordinates.Y;
                        int visibleTopY = elementTopCoordinates.Y < 0 ? 0 : elementTopCoordinates.Y;
                        swipeOrigin.Y = (visibleBottomY - visibleTopY) / 2;
                    }
                    else
                    {
                        swipeOrigin.Y = elementCenterCoordinates.Y;
                    }
                    break;
                case SwipeOrigin.RightTop:
                    swipeOrigin.X = elementBottomCoordinates.X > DlkEnvironment.mDeviceWidth ? DlkEnvironment.mDeviceWidth - 1 : elementBottomCoordinates.X;
                    swipeOrigin.Y = elementTopCoordinates.Y < 0 ? 1 : elementTopCoordinates.Y;
                    break;
                default: // center
                    swipeOrigin.X = elementCenterCoordinates.X;
                    if (elementBottomCoordinates.Y > DlkEnvironment.mDeviceHeight || elementTopCoordinates.Y < 0)
                    {
                        int visibleBottomY = elementBottomCoordinates.Y > DlkEnvironment.mDeviceHeight ? DlkEnvironment.mDeviceHeight : elementBottomCoordinates.Y;
                        int visibleTopY = elementTopCoordinates.Y < 0 ? 0 : elementTopCoordinates.Y;
                        swipeOrigin.Y = (visibleBottomY - visibleTopY) / 2;
                    }
                    else
                    {
                        swipeOrigin.Y = elementCenterCoordinates.Y;
                    }

                    break;
            }

            Point swipeDestination = new Point();
            switch (Direction)
            {
                case SwipeDirection.Up:
                    swipeDestination.X = swipeOrigin.X;
                    swipeDestination.Y = (swipeOrigin.Y - Distance) < 0 ? 1 : swipeOrigin.Y - Distance;
                    break;
                case SwipeDirection.Down:
                    swipeDestination.X = swipeOrigin.X;
                    swipeDestination.Y = (swipeOrigin.Y + Distance) > DlkEnvironment.mDeviceHeight 
                        ? DlkEnvironment.mDeviceHeight  - 1 : Convert.ToInt32((swipeOrigin.Y + Distance) * 0.95);
                    break;
                case SwipeDirection.Left:
                    swipeDestination.X = (swipeOrigin.X - Distance) < 0 ? 1 : swipeOrigin.X - Distance;
                    swipeDestination.Y = swipeOrigin.Y;
                    break;
                case SwipeDirection.Right:
                    swipeDestination.X = (swipeOrigin.X + Distance) > DlkEnvironment.mDeviceWidth 
                        ? DlkEnvironment.mDeviceWidth - 1 : swipeOrigin.X + Distance;
                    swipeDestination.Y = swipeOrigin.Y;
                    break;
                default: // Should not hit
                    break;
            }
            
            bool isNative = appiumDriver.Context.Contains("NATIVE");

            if (!isNative)
            {
                DlkEnvironment.SetContext("NATIVE");
                appiumDriver.Swipe(swipeOrigin.X, swipeOrigin.Y, swipeDestination.X, swipeDestination.Y, INT_SWIPE_WAIT_MS);
                DlkEnvironment.SetContext("WEBVIEW");
            }
            else
            {
                appiumDriver.Swipe(swipeOrigin.X, swipeOrigin.Y, swipeDestination.X, swipeDestination.Y, INT_SWIPE_WAIT_MS);
            }
        }

        public Point ConvertToNativeViewCoordinates(int x, int y)
        {
            Point nativeCoords = new Point();
            AppiumDriver<AppiumWebElement> appiumDriver = (AppiumDriver<AppiumWebElement>)DlkEnvironment.AutoDriver;
            bool isNative = appiumDriver.Context.Contains("NATIVE");

            if (!isNative)
            {
                //Coordinates extracted from webview uses CSS coordinates which 
                //has different scaling from the actual device screen.

                var selector = DlkEnvironment.mBrowser.ToLower() == "android" ? By.CssSelector("html>body") : By.CssSelector("html");
                //First compute for the scale factor.
                IWebElement htmlBody = DlkEnvironment.AutoDriver.FindElement(selector);
                Size bodySize = htmlBody.Size;
                double scaleX = Convert.ToDouble(DlkEnvironment.mDeviceWidth) / Convert.ToDouble(bodySize.Width);
                double scaleY = 0.0;
                if (DlkEnvironment.mBrowser.ToLower() == "ios" && DlkEnvironment.mStatusBarHeight != 20) //We need to consider the servicebar in computing the Y ratio
                {
                    //scaleY = Convert.ToDouble(DlkEnvironment.mDeviceHeight) / (Convert.ToDouble(bodySize.Height) + DlkEnvironment.mStatusBarHeight);
                    nativeCoords = mElement.Location;
                    return nativeCoords;
                }
                else
                {
                    scaleY = Convert.ToDouble(DlkEnvironment.mDeviceHeight) / Convert.ToDouble(bodySize.Height);
                }

                //Apply scale transformation to the current web element
                nativeCoords.X = Convert.ToInt32(Math.Round(scaleX * Convert.ToDouble(x)));
                nativeCoords.Y = Convert.ToInt32(Math.Round(scaleY * Convert.ToDouble(y)));

                //Adjust X coordinates (there were instance that X is a negative coordinate)
                //nativeCoords.X = nativeCoords.X >= 0 ? nativeCoords.X : 0;

                //Adjust Y coordinates based on the status bar
                if (DlkEnvironment.mBrowser.ToLower() == "ios") //depending on the version of iOS, we need to consider the servicebar in computing the Y coordinate
                {
                    //do nothing. Status bar is already considered in the scale computation.
                    //nativeCoords.Y = nativeCoords.Y - DlkEnvironment.mStatusBarHeight;
                }
                else
                {
                    double statusBarWeightOffSetRate = 1d / (DlkEnvironment.mDeviceHeight / INT_DEVICE_DENSITY);
                    double weightedStatusBarOffSet = DlkEnvironment.mStatusBarHeight * (1 - (statusBarWeightOffSetRate * (nativeCoords.Y / INT_DEVICE_DENSITY)));
                    if (weightedStatusBarOffSet < 0)
                    {
                        weightedStatusBarOffSet = 0;
                    }
                    nativeCoords.Y = nativeCoords.Y >= 0 ? nativeCoords.Y + Convert.ToInt32(weightedStatusBarOffSet) : nativeCoords.Y;
                }
            }
            else
            {
                nativeCoords = new Point(x, y);
            }

            return nativeCoords;
        }

        public Point GetNativeViewCoordinates()
        {
            Point nativeCoords = new Point();
            AppiumDriver<AppiumWebElement> appiumDriver = (AppiumDriver<AppiumWebElement>)DlkEnvironment.AutoDriver;
            bool isNative = appiumDriver.Context.Contains("NATIVE");

            if (!isNative)
            {
                //Coordinates extracted from webview uses CSS coordinates which 
                //has different scaling from the actual device screen.

                var selector = DlkEnvironment.mBrowser.ToLower() == "android" ? By.CssSelector("html>body") : By.CssSelector("html");
                //First compute for the scale factor.
                IWebElement htmlBody = DlkEnvironment.AutoDriver.FindElement(selector);
                Size bodySize = htmlBody.Size;
                double scaleX = Convert.ToDouble(DlkEnvironment.mDeviceWidth) / Convert.ToDouble(bodySize.Width);
                double scaleY = 0.0;
                if (DlkEnvironment.mBrowser.ToLower() == "ios" && DlkEnvironment.mStatusBarHeight != 20) //We need to consider the servicebar in computing the Y ratio
                {
                    //scaleY = Convert.ToDouble(DlkEnvironment.mDeviceHeight) / (Convert.ToDouble(bodySize.Height) + DlkEnvironment.mStatusBarHeight);
                    nativeCoords = mElement.Location;
                    nativeCoords.X = nativeCoords.X + 1;
                    nativeCoords.Y = nativeCoords.Y + 1;

                    return nativeCoords;
                }
                else
                {
                    scaleY = Convert.ToDouble(DlkEnvironment.mDeviceHeight) / Convert.ToDouble(bodySize.Height);
                }

                //Apply scale transformation to the current web element
                nativeCoords.X = Convert.ToInt32(Math.Round(scaleX * Convert.ToDouble(mElement.Location.X)));
                nativeCoords.Y = Convert.ToInt32(Math.Round(scaleY * Convert.ToDouble(mElement.Location.Y)));

                //Adjust X coordinates (there were instance that X is a negative coordinate)
                //nativeCoords.X = nativeCoords.X >= 0 ? nativeCoords.X : 0;

                nativeCoords.X = nativeCoords.X + 1;
                nativeCoords.Y = nativeCoords.Y + 1;

                //Adjust Y coordinates based on the status bar
                if (DlkEnvironment.mBrowser.ToLower() == "ios") //depending on the version of iOS, we need to consider the servicebar in computing the Y coordinate
                {
                    //do nothing. Status bar is already considered in the scale computation.
                    //nativeCoords.Y = nativeCoords.Y - DlkEnvironment.mStatusBarHeight;
                }
                else
                {
                    double statusBarWeightOffSetRate = 1d / (DlkEnvironment.mDeviceHeight / INT_DEVICE_DENSITY);
                    double weightedStatusBarOffSet = DlkEnvironment.mStatusBarHeight * (1 - (statusBarWeightOffSetRate * (nativeCoords.Y / INT_DEVICE_DENSITY)));
                    if (weightedStatusBarOffSet < 0)
                    {
                        weightedStatusBarOffSet = 0;
                    }
                    nativeCoords.Y = nativeCoords.Y >= 0 ? nativeCoords.Y + Convert.ToInt32(weightedStatusBarOffSet) : nativeCoords.Y;
                }
            }
            else
            {
                nativeCoords = mElement.Location;
            }

            return nativeCoords;
        }

        public Point GetNativeViewCenterCoordinates()
        {
            Point nativeCoords = new Point();
            AppiumDriver<AppiumWebElement> appiumDriver = (AppiumDriver<AppiumWebElement>)DlkEnvironment.AutoDriver;
            bool isNative = appiumDriver.Context.Contains("NATIVE");
            
            if (!isNative)
            {
                //Coordinates extracted from webview uses CSS coordinates which 
                //has different scaling from the actual device screen.

                var selector = DlkEnvironment.mBrowser.ToLower() == "android" ? By.CssSelector("html>body") : By.CssSelector("html");
                //First compute for the scale factor.
                IWebElement htmlBody = DlkEnvironment.AutoDriver.FindElement(selector);
                Size bodySize = htmlBody.Size;
                double scaleX = Convert.ToDouble(DlkEnvironment.mDeviceWidth) / Convert.ToDouble(bodySize.Width);
                double scaleY = 0.0;

                if (DlkEnvironment.mBrowser.ToLower() == "ios" && DlkEnvironment.mStatusBarHeight != 20) 
                {
                    //set coordinates to center of element
                    //no need to compute for offset in ios for status bar sizes not equal to the default (20)
                    nativeCoords.X = Convert.ToInt32(mElement.Location.X + mElement.Size.Width / 2);
                    nativeCoords.Y = Convert.ToInt32(mElement.Location.Y + mElement.Size.Height / 2);
                    return nativeCoords;
                }
                else
                {
                    scaleY = Convert.ToDouble(DlkEnvironment.mDeviceHeight) / Convert.ToDouble(bodySize.Height);
                }

                //Apply scale transformation to the current web element
                nativeCoords.X = Convert.ToInt32(Math.Round(scaleX * Convert.ToDouble(mElement.Location.X) + ((scaleX * Convert.ToDouble(mElement.Size.Width)) / 2)));
                nativeCoords.Y = Convert.ToInt32(Math.Round(scaleY * Convert.ToDouble(mElement.Location.Y) + ((scaleY * Convert.ToDouble(mElement.Size.Height)) / 2)));

                //Adjust X coordinates (there were instance that X is a negative coordinate)
                //nativeCoords.X = nativeCoords.X >= 0 ? nativeCoords.X : 0;

                //Adjust Y coordinates based on the status bar
                if (DlkEnvironment.mBrowser.ToLower() == "ios")
                {
                    //do nothing. Status bar is already considered in the scale computation.
                }
                else
                {
                    double statusBarWeightOffSetRate = 1d / (DlkEnvironment.mDeviceHeight / INT_DEVICE_DENSITY);
                    double weightedStatusBarOffSet = DlkEnvironment.mStatusBarHeight * (1 - (statusBarWeightOffSetRate * (nativeCoords.Y / INT_DEVICE_DENSITY)));
                    if (weightedStatusBarOffSet < 0)
                    {
                        weightedStatusBarOffSet = 0;
                    }
                    nativeCoords.Y = nativeCoords.Y >= 0 ? nativeCoords.Y + Convert.ToInt32(weightedStatusBarOffSet) : nativeCoords.Y;
                }
            }
            else
            {
                //set coordinates to center of element
                nativeCoords.X = Convert.ToInt32(mElement.Location.X + mElement.Size.Width / 2);
                nativeCoords.Y = Convert.ToInt32(mElement.Location.Y + mElement.Size.Height / 2);
            }
            return nativeCoords;
        }
        
        /// <summary>
        /// Double clicks the element
        /// </summary>
        /// <param name="dbSecToWait"></param>
        public void DoubleClick()
        {
            FindElement();
            OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
            mAction.DoubleClick(mElement).Perform();
            DlkLogger.LogInfo("Successfully executed DoubleClick()");
        }

        /// <summary>
        /// Sets the focus on the element using javascript instead of selenium core logic
        /// </summary>
        public void FocusUsingJavaScript()
        {
            IJavaScriptExecutor javascript = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;

            javascript.ExecuteScript("arguments[0].focus();", mElement);
        }

        /// <summary>
        /// Verifies the element exists
        /// </summary>
        /// <param name="ExpResult"></param>
        public void VerifyExists(Boolean ExpResult)
        {
            Boolean bExists = Exists();
            DlkAssert.AssertEqual("VerifyExists() : " + mControlName, ExpResult, bExists);
            if(DlkEnvironment.mProductFolder=="GovWin")
                DlkVariable.SetVariable("IfExist", bExists.ToString());
        }


        public void GetIfExists(String Variable)
        {
            Boolean bExists = Exists();
            DlkVariable.SetVariable(Variable, bExists.ToString());
        }

        /// <summary>
        /// verifies the element exists within a period
        /// </summary>
        /// <param name="iSecsToWait"></param>
        /// <returns></returns>
        public Boolean ExistsAndEnabled(int iSecsToWait)
        {
            Boolean bExistsEnabled = false;
            try
            {
                FindElement(iSecsToWait);
                if (mElement.Displayed && mElement.Enabled)
                {
                    bExistsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == DlkLogger.STR_CANCELLATION_MESSAGE)
                {
                    throw;
                }
                bExistsEnabled = false;
            }
            return bExistsEnabled;
        }

        /// <summary>
        /// verifies the element exists within a period
        /// </summary>
        /// <param name="iSecsToWait"></param>
        /// <returns></returns>
        public Boolean Exists(int iSecsToWait)
        {
            Boolean bExists = false;
            try
            {
                FindElement(iSecsToWait);
                if (mElement.Displayed)
                {
                    bExists = true;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == DlkLogger.STR_CANCELLATION_MESSAGE)
                {
                    throw;
                }
                bExists = false;
            }
            return bExists;
        }

        /// <summary>
        /// true/false if the element exists or not
        /// </summary>
        /// <returns></returns>
        public Boolean Exists()
        {
            Boolean bExists = false;
            bExists = Exists(2);
            return bExists;
        }

        /// <summary>
        /// asserts if the specified attribute has the specified value
        /// </summary>
        /// <param name="AttribName"></param>
        /// <param name="ExpectedValue"></param>
        public void VerifyAttribute(String AttribName, String ExpectedValue)
        {
            String ActValue = GetAttributeValue(AttribName);
            DlkAssert.AssertEqual("VerifyAttribute()", ExpectedValue, ActValue);
        }

        /// <summary>
        /// returns a value for the specified attribute
        /// </summary>
        /// <param name="AttribName"></param>
        /// <returns></returns>
        public String GetAttributeValue(String AttribName)
        {
            FindElement();
            String ActValue = this.mElement.GetAttribute(AttribName);
            if (ActValue != null)
            {
                ActValue = ActValue.Trim();
            }
            return ActValue;

        }

        /// <summary>
        /// gets the path to the element; useful for debugging
        /// </summary>
        /// <returns></returns>
        public String GetPath()
        {
            return GetPath(mElement, false);
        }

        /// <summary>
        /// gets the path to the element; useful for debugging
        /// </summary>
        /// <returns></returns>
        public List<String> GetPathAsList()
        {
            return GetPathAsList(mElement, false);
        }

        /// <summary>
        /// gets the path to the element; useful for debugging
        /// </summary>
        /// <param name="ElementToExamine"></param>
        /// <param name="bStopAtFirstDiv"></param>
        /// <returns></returns>
        public String GetPath(IWebElement ElementToExamine, Boolean bStopAtFirstDiv)
        {
            String result = "";
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    IWebElement mTmp;
                    if (i == 0)
                    {
                        mTmp = ElementToExamine.FindElement(By.XPath("."));
                    }
                    else
                    {
                        mTmp = ElementToExamine.FindElement(By.XPath(".."));
                    }
                    String mTag = mTmp.TagName;
                    if ((mTag == "") || (mTag == null))
                    {
                        break;
                    }
                    String mID = mTmp.GetAttribute("id");
                    String mClass = mTmp.GetAttribute("class");
                    String newData = mTag;
                    if (mID != "")
                    {
                        newData = newData + @"#" + mID;
                    }
                    else
                    {
                        if (mClass != "")
                        {
                            newData = newData + @"." + mClass;
                        }
                    }
                    if (i > 0)
                    {
                        result = ">" + result;
                    }
                    result = newData + result;
                    if (bStopAtFirstDiv)
                    {
                        if (mTag.ToLower() == "div")
                        {
                            break;
                        }
                    }
                    if (mTag == "html")
                    {
                        break;
                    }
                    ElementToExamine = mTmp;
                }
                catch
                {
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// gets the path to the element; useful for debugging
        /// </summary>
        /// <param name="ElementToExamine"></param>
        /// <param name="bStopAtFirstDiv"></param>
        /// <returns></returns>
        public List<String> GetPathAsList(IWebElement ElementToExamine, Boolean bStopAtFirstDiv)
        {
            String mPath = GetPath(ElementToExamine, bStopAtFirstDiv);
            List<String> mResults = mPath.Split('>').ToList();
            return mResults;
        }

        /// <summary>
        /// returns the parent element
        /// </summary>
        /// <returns></returns>
        public IWebElement GetParent()
        {
            FindElement();
            IWebElement mTmpElm = mElement.FindElement(By.XPath(".."));
            return mTmpElm;
        }

        /// <summary>
        /// returns the target element
        /// </summary>
        /// <returns></returns>
        public IWebElement GetTargetElement()
        {
            return mElement;
        }

        /// <summary>
        /// scrolls the element into view using the Selenium interactions MoveToElement code
        /// </summary>
        public void ScrollIntoView()
        {
            FindElement();
            if (DlkEnvironment.mIsMobile)
            {
                /* Magic scroll: invoking LocationOnScreenOnceScrolledIntoView actually scrolls to view */
                /* in future: find out why and replace below with more elegant solution */
                var willScrollOnAssignment = (mElement as OpenQA.Selenium.Appium.AppiumWebElement).LocationOnScreenOnceScrolledIntoView;
            }
            else
            {
                OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                mAction.MoveToElement(mElement);
            }
        }

        /// <summary>
        /// Returns a value of an element by looking at these properties: text, textContext, value, innerHtml, innerText
        /// The first property with a value get returned
        /// Please do not reorder the properties : just us GetAttribute where this is not effective
        /// </summary>
        /// <returns></returns>
        public String GetValue()
        {
            String sValue = "";

            if (mElement == null)
            {
                FindElement();
            }

            sValue = mElement.Text;
            if ((sValue != "") && (sValue != null))
            {
                
                return sValue;
            }

            sValue = mElement.GetAttribute("textContext");
            if ((sValue != "") && (sValue != null))
            {
                return sValue;
            }

            sValue = mElement.GetAttribute("checked");
            if (bool.TryParse(sValue,out bool _) || 
                (sValue == null && new string[] { "fcb","tcb"}.Contains(mElement.GetAttribute("class").ToLower()))) //unchecked checkbox returning null
            {
                return sValue??"false";
            }

            sValue = mElement.GetAttribute("value");
            if ((sValue != "") && (sValue != null))
            {
                return sValue;
            }

            sValue = mElement.GetAttribute("innerText");
            if ((sValue != "") && (sValue != null))
            {
                return sValue;
            }

            sValue = mElement.GetAttribute("innerHTML");
            if ((sValue != "") && (sValue != null)  && (sValue != "\r\n"))
            {
                return sValue;
            }

            sValue = mElement.GetAttribute("placeholder");
            if ((sValue != "") && (sValue != null))
            {
                return sValue;
            }

            sValue = mElement.GetAttribute("title");
            if ((sValue != "") && (sValue != null))
            {
                return sValue;
            }

            return "";
        }

        /// <summary>
        /// returns the height of the element
        /// </summary>
        /// <returns></returns>
        public int GetHeight()
        {
            FindElement();
            return Convert.ToInt32(mElement.Size.Height);
        }

        /// <summary>
        /// returns the width of the element
        /// </summary>
        /// <returns></returns>
        public int GetWidth()
        {
            FindElement();
            return Convert.ToInt32(mElement.Size.Width);
        }

        /// <summary>
        /// captues and compare images of elements
        /// </summary>
        /// <param name="ExpFile"></param>
        /// <param name="ActImageName"></param>
        /// <param name="iOffSetX"></param>
        /// <param name="iOffSetY"></param>
        /// <param name="iExtraWidth"></param>
        /// <param name="iExtraHeight"></param>
        public void CaptureAndCompareImage(String ExpFile, String ActImageName, int iOffSetX, int iOffSetY, int iExtraWidth, int iExtraHeight)
        {
            // get a full screen capture
            String sTmpFile = GetScreenCapture();

            String sCroppedFile = "", sOutputFileOfPixelDiff = "";
            int iRetries =26, iDelMax=15, iTmpOffsetX=0, iTmpOffsetY=0;
            for (int i = 1; i <= iRetries; i++)
            {
                DlkLogger.LogInfo("Attempt: " + i.ToString());

                // get the cropped screen capture for the area to compare
                sCroppedFile = GetCroppedScreenCapture(sTmpFile, ActImageName, (0 + iTmpOffsetX + iOffSetX),
                    (0 + iTmpOffsetY + iOffSetY),
                     (0 + iExtraWidth), (0) + iExtraHeight);

                // compare
                sOutputFileOfPixelDiff = sCroppedFile;
                sOutputFileOfPixelDiff = sOutputFileOfPixelDiff.Replace(".Png", "_PixelDiff.Png");

                // we want to exit on the first time if no exp file exists --> this is how we create one
                if (i == 1)
                {
                    DlkAssert.AssertEqual("Expected result image files exists.", true, File.Exists(ExpFile));
                }
                try
                {
                    DlkImageCompare.CompareResult cr = DlkImageCompare.Compare(ExpFile, sCroppedFile, sOutputFileOfPixelDiff);
                    DlkAssert.AssertEqual("Image comparison", DlkImageCompare.CompareResult.ciCompareOk, cr);
                    break;
                }
                catch
                {
                    if (i == iRetries)
                    {
                        DlkLogger.LogData("Actual Result Image: " + sCroppedFile);
                        DlkLogger.LogData("Expexted Result Image: " + ExpFile);
                        DlkLogger.LogData("Pixel Diff Image: " + sOutputFileOfPixelDiff);
                        throw;
                    }
                    else
                    {
                        // delete the files - we only save them for the last error
                        for (int j = 1; j <= iDelMax; j++)
                        {
                            try
                            {
                                File.Delete(sCroppedFile);
                                File.Delete(sOutputFileOfPixelDiff);
                                break;
                            }
                            catch
                            {
                                if (j == iDelMax)
                                {
                                    throw;
                                }
                                Thread.Sleep(1000);
                            }
                        }

                        int k = i + 1;

                        switch (k)
                        {
                            case 2:
                                iTmpOffsetX = 1;
                                iTmpOffsetY = 0;
                                break;
                            case 3:
                                iTmpOffsetX = -1;
                                iTmpOffsetY = 0;
                                break;
                            case 4:
                                iTmpOffsetX = 0;
                                iTmpOffsetY = 1;
                                break;
                            case 5:
                                iTmpOffsetX = 0;
                                iTmpOffsetY = -1;
                                break;
                            case 6:
                                iTmpOffsetX = 1;
                                iTmpOffsetY = 1;
                                break;
                            case 7:
                                iTmpOffsetX = -1;
                                iTmpOffsetY = -1;
                                break;
                            case 8:
                                iTmpOffsetX = 1;
                                iTmpOffsetY = -1;
                                break;
                            case 9:
                                iTmpOffsetX = -1;
                                iTmpOffsetY = 1;
                                break;
                            case 10:
                                iTmpOffsetX = 2;
                                iTmpOffsetY = 0;
                                break;
                            case 11:
                                iTmpOffsetX = 0;
                                iTmpOffsetY = 2;
                                break;
                            case 12:
                                iTmpOffsetX = -2;
                                iTmpOffsetY = 0;
                                break;
                            case 13:
                                iTmpOffsetX = 0;
                                iTmpOffsetY = -2;
                                break;
                            case 14:
                                iTmpOffsetX = 2;
                                iTmpOffsetY = 1;
                                break;
                            case 15:
                                iTmpOffsetX = 2;
                                iTmpOffsetY = -1;
                                break;
                            case 16:
                                iTmpOffsetX = -2;
                                iTmpOffsetY = 1;
                                break;
                            case 17:
                                iTmpOffsetX = -2;
                                iTmpOffsetY = -1;
                                break;
                            case 18:
                                iTmpOffsetX = 1;
                                iTmpOffsetY = 2;
                                break;
                            case 19:
                                iTmpOffsetX = 1;
                                iTmpOffsetY = -2;
                                break;
                            case 20:
                                iTmpOffsetX = -1;
                                iTmpOffsetY = 2;
                                break;
                            case 21:
                                iTmpOffsetX = -1;
                                iTmpOffsetY = -2;
                                break;
                            case 22:
                                iTmpOffsetX = 2;
                                iTmpOffsetY = 2;
                                break;
                            case 23:
                                iTmpOffsetX = 2;
                                iTmpOffsetY = -2;
                                break;
                            case 24:
                                iTmpOffsetX = -2;
                                iTmpOffsetY = 2;
                                break;
                            case 25:
                                iTmpOffsetX = -2;
                                iTmpOffsetY = -2;
                                break;
                            default:
                                iTmpOffsetX = 0;
                                iTmpOffsetY = 0;
                                break;
                        }
                    }
                }
            }

            // cleanup
            for (int j = 1; j <= iDelMax; j++) // needed as the file could be locked for a few seconds
            {
                try
                {
                    File.Delete(sCroppedFile);
                    File.Delete(sOutputFileOfPixelDiff);
                    File.Delete(sTmpFile);
                    break;
                }
                catch
                {
                    if (j == iDelMax)
                    {
                        throw;
                    }
                    Thread.Sleep(1000);
                }
            }
        }

        /// <summary>
        /// Gets a screen capture
        /// </summary>
        /// <returns></returns>
        private String GetScreenCapture()
        {
            // take a screen capture of just the browser
            Screenshot ss = ((ITakesScreenshot)DlkEnvironment.AutoDriver).GetScreenshot();
            String sTmpFile = DlkEnvironment.mDirTestResultsCurrent + "img_" + string.Format("{0:yyMMddhhmmss}", DateTime.Now) + ".Png";
            ss.SaveAsFile(sTmpFile, ScreenshotImageFormat.Png);
            return sTmpFile;
        }
        private String GetCroppedScreenCapture(String sOrigFile, String ImageName, int iOffsetX, int iOffsetY, int iExtraWidth, int iExtraHeight)
        {
            // determine the rect size
            Point mPoint = mElement.Location;
            mPoint.Offset(iOffsetX, iOffsetY);
            Size mSize = new Size(GetWidth() + iExtraWidth, GetHeight() + iExtraHeight);
            //DlkLogger.LogDebug("elm location", mPoint);

            // load ss into bmp
            Image mImg = Image.FromFile(sOrigFile);
            Bitmap bmpScreenshotOrig = new Bitmap(mSize.Width, mSize.Height, PixelFormat.Format32bppRgb);

            Graphics gfxScreenshot = Graphics.FromImage(bmpScreenshotOrig);
            gfxScreenshot.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gfxScreenshot.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            gfxScreenshot.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;


            // draw the select part of the screen we want to the top left corner of the screen
            gfxScreenshot.DrawImage(mImg, new Rectangle(new Point(0,0),mSize), new Rectangle(mPoint, mSize), GraphicsUnit.Pixel);
            //gfxScreenshot.CopyFromScreen(0, 0, 0, 0, mSize, CopyPixelOperation.SourceCopy);

            // copy to a new bmp cutting out only the image size for the element (what we copied to the top left corner)
            //Bitmap bmpScreenshot = bmpScreenshotOrig.Clone(new Rectangle(0, 0, mSize.Width, mSize.Height), bmpScreenshotOrig.PixelFormat);

            //gfxScreenshot.DrawRectangle(new Pen(Color.Black),new Rectangle(mPoint,mSize));

            // Save the screenshot to the specified path that the user has chosen
            String sFile = DlkEnvironment.mDirTestResultsCurrent + ImageName + "_" + Environment.MachineName + "_" + string.Format("{0:yyMMddhhmmss}", DateTime.Now) + ".Png";
            //Thread.Sleep(1000);
            //String sFile2 = DlkEnvironment.DirScreenshots + ImageName + "_" + Environment.MachineName + "_" + string.Format("{0:yyMMddhhmmss}", DateTime.Now) + ".Png";
            bmpScreenshotOrig.Save(sFile, ImageFormat.Png);
            mImg.Dispose();
            //bmpScreenshotOrig.Save(sFile2, ImageFormat.Png);
            gfxScreenshot.Dispose();
            //bmpScreenshot.Dispose();
            bmpScreenshotOrig.Dispose();
            return sFile;
        }

        /// <summary>
        /// Provides ability to assign an element attribute to a variable and use that dynamically in a test
        /// </summary>
        /// <param name="VariableName"></param>
        /// <param name="AttributeName"></param>
        public void AssignToVariable(String VariableName, String AttributeName)
        {
            String mValue = mElement.GetAttribute(AttributeName);
            DlkVariable.SetVariable(VariableName, mValue);
            DlkLogger.LogInfo("AssignToVariable()", mControlName, "Variable:[" + VariableName + "], Value:[" + mValue + "].");
        }

        /// <summary>
        /// Assigns the supplied variable name to whatever GetValue() returns
        /// </summary>
        /// <param name="VariableName"></param>
        [Keyword("AssignValueToVariable")]
        public void AssignValueToVariable(String VariableName)
        {
            //remove trailing spaces from the value before setting to variable
            String mValue = GetValue().TrimEnd();
            DlkVariable.SetVariable(VariableName, mValue);
            DlkLogger.LogInfo("AssignValueToVariable()", mControlName, "Variable:[" + VariableName + "], Value:[" + mValue + "].");
        }

        /// <summary>
        /// Assigns the supplied variable name to whatever GetValue() returns
        /// </summary>
        /// <param name="VariableName"></param>
        [Keyword("AssignPartialValueToVariable")]
        public virtual void AssignPartialValueToVariable(string VariableName, string StartIndex, string Length)
        {
            try
            {
                String txtValue = GetValue();
                if (string.IsNullOrEmpty(txtValue))
                {
                    DlkVariable.SetVariable(VariableName, string.Empty);
                }
                else
                {
                    DlkVariable.SetVariable(VariableName, txtValue.Substring(int.Parse(StartIndex), int.Parse(Length)));
                }
                DlkLogger.LogInfo("Successfully executed AssignPartialValueToVariable().");
            }
            catch (Exception e)
            {
                throw new Exception("AssignPartialValueToVariable() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Set the value of an attribute of the control
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        public void SetAttribute(string attributeName, string value)
        {
            IJavaScriptExecutor javascript = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
            javascript.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2])", mElement, attributeName, value);
        }

        /// <summary>
        /// Highlight the control
        /// </summary>
        /// <param name="bPersist"></param>
        public void Highlight(Boolean bPersist, HighlightColor Color=HighlightColor.Default)
        {
            FindElement();
            const int wait = 150;
            mOrigStyle = mElement.GetAttribute("style");
            //remove color, border, and background-color style
            string[] styles = mOrigStyle.Split(';');
            string newStyle = "";
            for (int i = 0; i < styles.Count(); i++)
            {
                if (!styles[i].Contains("color:") && !styles[i].Contains("border:"))
                {
                    newStyle = newStyle + styles[i] + ";";
                }
            }
            if (bPersist)
            {
                switch(Color)
                {
                    case HighlightColor.Blue:
                        SetAttribute("style", newStyle + "background-color: blue;");
                        break;
                    default:
                        SetAttribute("style", newStyle + "background-color: yellow;");
                        break;
                }
            }
            else
            {
                switch(Color)
                {
                    case HighlightColor.Blue:
                        SetAttribute("style", newStyle + "color: blue; border: 10px solid blue; background-color: silver;");
                        Thread.Sleep(wait);
                        SetAttribute("style", newStyle + "color: silver; border: 10px solid silver; background-color: blue;");
                        Thread.Sleep(wait);
                        SetAttribute("style", newStyle + "color: blue; border: 10px solid blue; background-color: silver;");
                        Thread.Sleep(wait);
                        SetAttribute("style", newStyle + "color: silver; border: 10px solid silver; background-color: blue;");
                        Thread.Sleep(wait);
                        break;
                    default:
                        SetAttribute("style", newStyle + "color: yellow; border: 10px solid yellow; background-color: black;");
                        Thread.Sleep(wait);
                        SetAttribute("style", newStyle + "color: black; border: 10px solid black; background-color: yellow;");
                        Thread.Sleep(wait);
                        SetAttribute("style", newStyle + "color: yellow; border: 10px solid yellow; background-color: black;");
                        Thread.Sleep(wait);
                        SetAttribute("style", newStyle + "color: black; border: 10px solid black; background-color: yellow;");
                        Thread.Sleep(wait);
                        break;
                }
                ClearHightlight();
            }

        }

        public enum HighlightColor
        {
            Blue,
            Default
        }

        /// <summary>
        /// Returns the original css style of the element to remove the highlight
        /// </summary>
        public void ClearHightlight()
        {
            FindElement();
            SetAttribute("style", mOrigStyle);
        }

        /// <summary>
        /// Returns "true" if element is read only otherwise "false"
        /// </summary>
        public String IsReadOnly()
        {
            String sValue = "";
            if (mElement == null)
            {
                FindElement();
            }
            sValue = mElement.GetAttribute("readonly");
            if (sValue != null)
            {
                DlkLogger.LogInfo("readonly");
                if (sValue == "readonly")
                {
                    sValue = "TRUE";
                }
                return sValue;
            }

            sValue = mElement.GetAttribute("readOnly");
            if (sValue != null)
            {
                DlkLogger.LogInfo("readOnly");
                return sValue;
            }

            sValue = mElement.GetAttribute("isDisabled");
            if (sValue != null)
            {
                DlkLogger.LogInfo("isDisabled");
                return sValue;
            }

            sValue = mElement.GetAttribute("disabled");
            if (sValue != null)
            {
                DlkLogger.LogInfo("disabled");
                return sValue;
            }

            sValue = mElement.GetAttribute("class");
            if (sValue.Contains("disabled"))
            {
                DlkLogger.LogInfo("disabled");
                return "true";
            }

            sValue = mElement.GetAttribute("contenteditable");
            if (sValue != null && sValue.Contains("false"))
            {
                DlkLogger.LogInfo("disabled");
                return "true";
            }

            sValue = mElement.GetAttribute("src");
            if (sValue != null && sValue.ToLower().Contains("disabled"))
            {
                DlkLogger.LogInfo("disabled");
                return "true";
            }

            sValue = mElement.GetAttribute("class");
            if (sValue != null && sValue.ToLower().Contains("readonly"))
            {
                DlkLogger.LogInfo("class-readonly");
                return "true";
            }

            if (DlkEnvironment.mIsMobile)
            {
                sValue = mElement.GetAttribute("dis");
                if (sValue != null)
                {
                    DlkLogger.LogInfo("disabled");
                    return "true";
                }

                IWebElement mParent = mElement.FindElement(By.XPath(".."));
                sValue = mParent.GetAttribute("class");
                if (sValue.Contains("disabled"))
                {
                    DlkLogger.LogInfo("disabled");
                    return "true";
                }
                sValue = mParent.GetAttribute("style");
                
                if (sValue.Contains("opacity: 0.5"))
                {
                    DlkLogger.LogInfo("disabled");
                    return "true";
                }

                sValue = mElement.GetAttribute("style");
                if (sValue.Contains("gradient(top, gray, gray, gray, gray)"))
                {
                    DlkLogger.LogInfo("disabled");
                    return "true";
                }
            }
            return "false";
        }

        public void ScrollIntoViewUsingJavaScript(bool IgnoreForIE = false)
        {
            // JPV: Temp hotfix for IE UI issue introduced by calls to this routine. Observe if no adverse effects
            if (IgnoreForIE && DlkEnvironment.mBrowser.ToLower() == "ie")
            {
                return;
            }
            IJavaScriptExecutor javascript = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;

            javascript.ExecuteScript("arguments[0].scrollIntoView(true);", mElement);
            javascript.ExecuteScript("arguments[0].scrollIntoView(false);", mElement);
        }

        /// <summary>
        /// Used by the Object Store Recorder tool to verify the control type of the recorded control
        /// </summary>
        public bool VerifyControlType()
        {
            return false;
        }

        /// <summary>
        /// Used by the Object Store Recorder tool to auto correct the entered SearchType and SearchValue.
        /// Returns true if there is a suggested search method
        /// </summary>
        /// <param name="SearchType"></param>
        /// <param name="SearchValue"></param>
        public void AutoCorrectSearchMethod(ref string SearchType, ref string SearchValue)
        {
        }

        public string FindXPath()
        {
            return (String)((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript(
                "gPt=function(c)"+
                "{"+
                    "if(c.id!=='')"+
                    "{"+
                        "return'id(\"'+c.id+'\")'"+
                    "}"+
                    "if(c===document.body)"+
                    "{"+
                        "return c.tagName"+
                    "}"+
                    "var a=0;"+
                    "var e=c.parentNode.childNodes;"+
                    "for(var b=0;b<e.length;b++)"+
                    "{"+
                        "var d=e[b];"+
                        "if(d===c)"+
                        "{"+
                            "return gPt(c.parentNode)+'/'+c.tagName+'['+(a+1)+']'"+
                        "}"+
                        "if(d.nodeType===1&&d.tagName===c.tagName)"+
                        "{"+
                            "a++"+
                        "}"+
                    "}"+
                "};"+
                "return gPt(arguments[0]).toLowerCase();", mElement);
        }

        /// <summary>
        /// Recursive function to find parent using tag name.
        /// </summary>
        /// <param name="tagName">Tag name of the parent element.</param>
        /// <param name="element">Child element which will be use as starting point.</param>
        /// <returns></returns>
        public IWebElement FindParentByTagName(string tagName, IWebElement element = null)
        {
            IWebElement parentElement = null;

            if (element == null)
            {
                element = mElement;
            }

            try
            {
                if (element.TagName.Trim().Equals("html"))
                {
                    DlkLogger.LogInfo("Parent with tag name " + tagName + " not found.");
                    return null;
                }
                else if (element.TagName.Trim().Equals(tagName))
                {
                    parentElement = element;
                }
                else if (element.FindElements(By.XPath("..")).Count > 0)
                {
                    parentElement = FindParentByTagName(tagName, element.FindElement(By.XPath("..")));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return parentElement;
        }

        /// <summary>
        /// Recursive function that will find parent element of mElement or argument.
        /// </summary>
        /// <param name="attribute">attribute of the parent element to search.</param>
        /// <param name="attributeValue">attribute value of the parent element to search.</param>
        /// <param name="element">Child element which will be use as starting point.</param>
        /// <returns></returns>
        public IWebElement FindParentByAttribute(string attribute, string attributeValue, IWebElement element = null)
        {
            IWebElement parentElement = null;

            if (element == null)
            {
                element = mElement;
            }

            try
            {
                string elementAttributeValue = element.GetAttribute(attribute);

                if (element.TagName.Equals("html"))
                {
                    DlkLogger.LogInfo("Parent with attribute value " + attributeValue + " not found.");
                    parentElement = null;
                }
                else if (elementAttributeValue != null &&
                         elementAttributeValue.Contains(attributeValue))
                {
                    parentElement = element;
                }
                else if (element.FindElements(By.XPath("..")).Count > 0)
                {
                    parentElement = FindParentByAttribute(attribute, attributeValue, element.FindElement(By.XPath("..")));
                }
            }
            catch
            {
                parentElement = null;
            }

            return parentElement;
        }


        public void ShiftTab()
        {
            mElement.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);
        }

        public DlkBaseControl GetControl()
        {
            return this;
        }

        public Boolean IsElementStale()
        {
            try
            {
                String sTagName = mElement.TagName;
                return false;
            }
            catch (StaleElementReferenceException)
            {
                return true;
            }
        }

        /// <summary>
        /// Get By object from input string
        /// </summary>
        /// <param name="PipeDelimitedValue">String like 'ID|Somevalue'</param>
        /// <returns>By object</returns>
        private By GetBy(string PipeDelimitedValue)
        {
            string[] arr = PipeDelimitedValue.Split('|');
            switch (arr.First().ToLower())
            {
                case "id":
                    return By.Id(arr.Last());
                case "xpath":
                    return By.XPath(arr.Last());
                case "class":
                    return By.ClassName(arr.Last());
                default:
                    break;
            }
            return null;
        }
    }
}

