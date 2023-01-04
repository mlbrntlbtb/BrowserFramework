using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Interactions;
using CommonLib.DlkSystem;
using System.Drawing;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Android;
using System.Threading;

namespace CommonLib.DlkControls
{
    public class DlkMobileControl : DlkBaseControl
    {
        public const string ATTRIB_RESOURCE_ID_WEB = "id";
        public const string ATTRIB_RESOURCE_ID_NATIVE = "resourceId";
        public const string ATTRIB_CLASS_NAME = "className";

        public DlkMobileControl(string ControlName, string SearchMethod, string SearchParameters)
            : base(ControlName, SearchMethod, SearchParameters) { }
        public DlkMobileControl(string ControlName, string SearchType, string[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkMobileControl(string ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void ScollIntoViewUsingWebView()
        {
            try
            {
                var idNative = mElement.GetAttribute(ATTRIB_RESOURCE_ID_NATIVE);
                if (!string.IsNullOrEmpty(idNative))
                {
                    DlkEnvironment.mLockContext = false;
                    DlkEnvironment.SetContext("WEBVIEW");
                    var webElem = DlkEnvironment.AutoDriver.FindElements(By.Id(idNative)).FirstOrDefault();
                    var willScrollOnAssignment = (webElem as OpenQA.Selenium.Appium.AppiumWebElement).LocationOnScreenOnceScrolledIntoView;
                }
            }
            catch
            {
                /* Do nothing */
            }
            finally
            {
                DlkEnvironment.SetContext("NATIVE");
            }
        }

        public static void ScrollIntoViewJS(string searcType, string searchParam)
        {
            //String scrollViewContainer_finder = "new UiSelector().resourceIdMatches(\".*id/your_scroll_view_id\")";
            //String neededElement_finder = "new UiSelector().resourceIdMatches(\".*id/elemnt1\")";

            //WebElement abc = driver.findElement(MobileBy.AndroidUIAutomator("new UiScrollable(" + scrollViewContainer_finder + ")" +
            //                ".scrollIntoView(" + neededElement_finder + ")"));

        }

        public static void Swipe(SwipeDirection direction)
        {
            try
            {
                var size = DlkEnvironment.AutoDriver.Manage().Window.Size;
                int startX = size.Width / 2;
                int startY = size.Height / 2;
                int endX = startX;
                int endY = startY;
                switch (direction)
                {
                    case SwipeDirection.Right:
                        endX = (int)(startX * 1 * 0.75);
                        endY = 0;
                        break;
                    case SwipeDirection.Left:
                        endX = (int)(startX * -1 * 0.75);
                        endY = 0;
                        break;
                    case SwipeDirection.Down:
                        endX = 0;
                        endY = (int)(startY * 1 * 0.75);
                        break;
                    case SwipeDirection.Up:
                    default:
                        endX = 0;
                        endY = (int)(startY * -1 * 0.75);
                        break;
                }
                var action = new TouchAction(DlkEnvironment.AutoDriver as AppiumDriver<AppiumWebElement>);
                action.Press(startX, startY).MoveTo(endX, endY).Release().Perform();
            }
            catch (Exception ex)
            {
                DlkLogger.LogWarning("An error was encountered on Swipe(): " + ex.Message);
                throw;
            }
        }

        public static bool SwipeFromBottomToUp()
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                Dictionary<string, string> scrollObject = new Dictionary<string, string>();
                scrollObject.Add("direction", "down");
                js.ExecuteScript("mobile: scroll", scrollObject);
                DlkLogger.LogInfo("Swipe down was Successfully done");
                return true;
            }
            catch
            {
                DlkLogger.LogInfo("Swipe down was not successful");
            }
            return false;
        }

        public static bool SwipeFromUpToBottom()
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                Dictionary<string, string> scrollObject = new Dictionary<string, string>();
                scrollObject.Add("direction", "up");
                js.ExecuteScript("mobile: scroll", scrollObject);
                DlkLogger.LogInfo("Swipe up was Successfully done");
                return true;
            }
            catch
            {
                DlkLogger.LogInfo("Swipe up was not successful");
            }
            return false;
        }

        public IWebElement GetMeInOtherContext(bool IsWebView, bool RevertContext=true)
        {
            IWebElement ret = null;
            try
            {
                if (IsWebView) // get native
                {
                    var idWebView = mElement.GetAttribute(ATTRIB_RESOURCE_ID_WEB);
                    if (!string.IsNullOrEmpty(idWebView))
                    {
                        DlkEnvironment.mLockContext = false;
                        DlkEnvironment.SetContext("NATIVE");
                        ret = DlkEnvironment.AutoDriver.FindElements(By.Id(idWebView)).FirstOrDefault();
                    }
                }
                else // get webview
                {
                    var idNative = mElement.GetAttribute(ATTRIB_RESOURCE_ID_NATIVE);
                    if (!string.IsNullOrEmpty(idNative))
                    {
                        DlkEnvironment.mLockContext = false;
                        DlkEnvironment.SetContext("WEBVIEW");
                        ret = DlkEnvironment.AutoDriver.FindElements(By.Id(idNative)).FirstOrDefault();
                    }
                }
            }
            catch
            {
                // Do nothing. return null.
            }
            finally
            {
                if (RevertContext)
                    DlkEnvironment.SetContext(IsWebView ? "WEBVIEW" : "NATIVE");
            }
            return ret;
        }

        public void HideKeyboard()
        {
            try
            {
                if (DlkEnvironment.mBrowser.ToLower() == "ios")
                {
                    DlkLogger.LogInfo("Attempting to hide keyboard on iOS...");

                    if (!IsContextNative())
                    {
                        DlkEnvironment.SetContext("NATIVE");
                    }

                    Point coordinates = new Point();
                    var iOSDriver = (IOSDriver<AppiumWebElement>)DlkEnvironment.AutoDriver;
                    var exitButtons = iOSDriver.FindElementsByIosNsPredicate("(type == 'XCUIElementTypeButton' AND (name == 'Done' AND label == 'Done'))").Where(elem => elem.Displayed && elem.Enabled);

                    if (exitButtons.Any())//skip tapping if there are no exit buttons present on the screen
                    {
                        //set coordinates to center of element
                        coordinates.X = Convert.ToInt32(exitButtons.FirstOrDefault().Location.X + exitButtons.FirstOrDefault().Size.Width / 2);
                        coordinates.Y = Convert.ToInt32(exitButtons.FirstOrDefault().Location.Y + exitButtons.FirstOrDefault().Size.Height / 2);

                        TouchAction tapAction = new TouchAction(iOSDriver);
                        tapAction.Tap(coordinates.X, coordinates.Y);
                        iOSDriver.PerformTouchAction(tapAction);
                    }

                    if (IsContextNative())
                    {
                        DlkEnvironment.SetContext("WEBVIEW");
                    }
                    DlkLogger.LogInfo("Keyboard successfully hidden.");
                }
            }
            catch
            {
                //do nothing
            }
        }

        public static bool IsContextNative()
        {
            try
            {
                AppiumDriver<AppiumWebElement> appiumDriver = (AppiumDriver<AppiumWebElement>)DlkEnvironment.AutoDriver;
                return appiumDriver.Context.Contains("NATIVE");
            }
            catch
            {
                throw new Exception("Cannot retrieve WebDriver context.");
            }
        }

        public static void ShowNotificationBar()
        {
            try
            {
                //OpenNotifications is implemented for Android only. Separate manual implementation needed for iOS.
                if (DlkEnvironment.mBrowser.ToLower() == "android")
                {
                    AndroidDriver<AppiumWebElement> appiumDriver = (AndroidDriver<AppiumWebElement>)DlkEnvironment.AutoDriver;
                    appiumDriver.OpenNotifications();
                    Thread.Sleep(DlkEnvironment.mShortWaitMs);//add pause for notif animation to finish.
                    DlkLogger.LogInfo("Notification bar successfully opened.");
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogWarning("An error was encountered on ShowNotificationBar(): " + ex.Message);
                throw;
            }

        }

        public static void CloseNotificationBar()
        {
            try
            {
                //OpenNotifications is implemented for Android only. Separate manual implementation needed for iOS.
                if (DlkEnvironment.mBrowser.ToLower() == "android")
                {
                    AndroidDriver<AppiumWebElement> appiumDriver = (AndroidDriver<AppiumWebElement>)DlkEnvironment.AutoDriver;
                    appiumDriver.PressKeyCode(AndroidKeyCode.Back);
                    Thread.Sleep(DlkEnvironment.ShortWaitMs);//wait for notification bar animation to close
                    DlkLogger.LogInfo("Notification bar successfully closed.");
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogWarning("An error was encountered on CloseNotificationBar(): " + ex.Message);
                throw;
            }
        }

        public static void EnterPIN(string PINValue)
        {
            try
            {
                // only for Android - separate implementation/modification is needed for iOS
                AndroidDriver<AppiumWebElement> appiumDriver = (AndroidDriver<AppiumWebElement>)DlkEnvironment.AutoDriver;
                Actions action = new Actions(appiumDriver);
                action.SendKeys(PINValue).Perform();
                appiumDriver.PressKeyCode(AndroidKeyCode.Enter);
                DlkLogger.LogInfo("PIN input successful");
            }
            catch (Exception ex)
            {
                DlkLogger.LogWarning("An error was encountered on EnterPIN(): " + ex.Message);
                throw;
            }
        }

        public static void TapNotificationHeader(String NotificationText)
        {
            try
            {
                if (!IsContextNative())
                {
                    DlkEnvironment.SetContext("NATIVE");
                }

                //OpenNotifications is implemented for Android only. Separate manual implementation needed for iOS.
                if (DlkEnvironment.mBrowser.ToLower() == "android")
                {
                    AppiumDriver<AppiumWebElement> appiumDriver = (AppiumDriver<AppiumWebElement>)DlkEnvironment.AutoDriver;
                    var notificationHeader = appiumDriver.FindElementsByXPath($".//*[@text='{ NotificationText.Trim() }']/parent::*[contains(@resource-id,'notification_header')]").FirstOrDefault(notif => notif.Displayed);
                    Thread.Sleep(DlkEnvironment.mShortWaitMs);//wait before inspecting header buttons

                    if (notificationHeader.FindElementsByXPath(".//*[@content-desc='Expand']").Any(expand => expand.Enabled))
                    {
                        new DlkBaseControl("Expand Notification", notificationHeader).Tap();
                    }
                    else if (notificationHeader.FindElementsByXPath(".//*[@content-desc='Collapse']").Any(collapse => collapse.Enabled))
                    {
                        DlkLogger.LogInfo("Notification already expanded. Proceeding to next step...");
                    }

                    DlkLogger.LogInfo("Notification successfully expanded.");
                }

                if (IsContextNative())
                {
                    DlkEnvironment.SetContext("WEBVIEW");
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogWarning("An error was encountered on ExpandNotification(): " + ex.Message);
                throw;
            }

        }

        public static void AcceptNotification(String SelectedNotification, String NotifButtonText)
        {
            try
            {
                if (!IsContextNative())
                {
                    DlkEnvironment.SetContext("NATIVE");
                }

                //OpenNotifications is implemented for Android only. Separate manual implementation needed for iOS. 
                if (DlkEnvironment.mBrowser.ToLower() == "android")
                {
                    AppiumDriver<AppiumWebElement> appiumDriver = (AppiumDriver<AppiumWebElement>)DlkEnvironment.AutoDriver;
                    var notificationButton = appiumDriver.FindElementsByXPath($".//*[@text='{ SelectedNotification.Trim() }']//ancestor::*[contains(@resource-id, 'status_bar_latest_event')]//*[@content-desc=\"{ NotifButtonText.Trim() }\" and contains(@resource-id, 'action')]").FirstOrDefault(button => button.Enabled);

                    if (notificationButton == null) throw new Exception($"Cannot find notification button '{ NotifButtonText }'");

                    new DlkBaseControl("Notification Button", notificationButton).Tap();
                    DlkLogger.LogInfo("Successfully accepted notification button.");
                }

                if (IsContextNative())
                {
                    DlkEnvironment.SetContext("WEBVIEW");
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogWarning("An error was encountered on AcceptNotification(): " + ex.Message);
                throw;
            }

        }
    }

    public class DlkMobilePicker : DlkMobileControl
    {
        private const int INT_CLICK_OFFSET_INITIAL_VAL = 10;
        private const int INT_CLICK_OFFET_MAX_VAL = 50;
        private const string XPATH_ITEMS = ".//div[contains(@class, 'x-dataview-item')]";

        public DlkMobilePicker(string ControlName, string SearchMethod, string SearchParameters)
            : base(ControlName, SearchMethod, SearchParameters) { }
        public DlkMobilePicker(string ControlName, string SearchType, string[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkMobilePicker(string ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private void Initialize()
        {
            FindElement();
        }

        public virtual void Select(string ItemToSelect)
        {
            try
            {
                List<IWebElement> mItems = mElement.FindElements(By.XPath(XPATH_ITEMS)).ToList();
                bool bToBeSelectedFound = false;
                IWebElement mTarget = null;

                for (int i = 0; i < mItems.Count; i++)
                {
                    var pickerItem = mItems[i].FindElement(By.TagName("div"));
                    if (pickerItem.Text.Trim() == ItemToSelect)
                    {
                        mTarget = mItems[i];
                        bToBeSelectedFound = true;
                        break;
                    }
                }
                if (!bToBeSelectedFound)
                {
                    throw new Exception("Unable to find value '" + ItemToSelect + "'.");
                }

                DlkBaseControl mTargetItem = new DlkBaseControl("Selected", mTarget);

                /* scroll into view */
                mTargetItem.ScrollIntoView();

                /* Click with magic offset */
                var offset = INT_CLICK_OFFSET_INITIAL_VAL;
                var currentSelectedText = string.Empty;
                do
                {
                    mTargetItem.Click(offset, offset);
                    offset += INT_CLICK_OFFSET_INITIAL_VAL;
                    var selected = mItems.FirstOrDefault(x => x.GetAttribute("class").Contains("selected"));
                    currentSelectedText = selected == null ? string.Empty : selected.FindElement(By.TagName("div")).Text;
                }
                while (currentSelectedText != ItemToSelect && offset <= INT_CLICK_OFFET_MAX_VAL);
            }
            catch
            {
                throw;
            }
        }

        public virtual void SelectByIndex(String OneBasedItemIndex)
        {
            try
            {
                Initialize();
                int index = 0;
                if (!int.TryParse(OneBasedItemIndex, out index))
                {
                    throw new Exception("Invalid index'" + OneBasedItemIndex + "'.");
                }
                List<IWebElement> mItems = mElement.FindElements(By.XPath(XPATH_ITEMS)).ToList();
                string textOfTarget = string.Empty;
                if (index <= 0 || index > mItems.Count)
                {
                    throw new Exception("Invalid index'" + OneBasedItemIndex + "'.");
                }
                IWebElement mTarget = null;

                var pickerItems = mItems[index - 1].FindElements(By.TagName("div"));
                if (pickerItems.Any())
                {
                    mTarget = mItems[index - 1];
                    textOfTarget = pickerItems.First().Text;
                }
                else
                {
                    throw new Exception("Index '" + OneBasedItemIndex + "' out bounds of picker items '" + mItems.Count + "'.");
                }

                DlkBaseControl mTargetItem = new DlkBaseControl("Selected", mTarget);

                /* scroll into view */
                mTargetItem.ScrollIntoView();

                /* Click with magic offset */
                var offset = INT_CLICK_OFFSET_INITIAL_VAL;
                var currentSelectedText = string.Empty;
                do
                {
                    mTargetItem.Click(offset, offset);
                    offset += INT_CLICK_OFFSET_INITIAL_VAL;
                    var selected = mItems.FirstOrDefault(x => x.GetAttribute("class").Contains("selected"));
                    currentSelectedText = selected == null ? string.Empty : selected.FindElement(By.TagName("div")).Text;
                }
                while (currentSelectedText != textOfTarget && offset <= INT_CLICK_OFFET_MAX_VAL);
            }
            catch
            {
                throw;
            }

        }
    }

}
