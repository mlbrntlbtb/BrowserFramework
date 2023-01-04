using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace MaconomyiAccessLib.DlkControls
{
    [ControlType("Notification")]
    public class DlkNotification : DlkBaseControl
    {

        #region PRIVATE VARIABLES

        private Boolean IsInit = false;
        private IList<IWebElement> mNotificationItems;
        private const string notificationItem_XPath = ".//div[contains(@dm-data-id,'dm-conversation') or contains(@dm-data-id,'dm-notification')]";
        private const string notificationItemHeader_XPath = ".//div[contains(@class,'notification-header')]";
        private const string expandArrow_XPath = ".//div[contains(@class,'icon-recordarrow')]";
        private const string collapseArrow_XPath = ".//div[contains(@class,'icon-droparrow')]";
        private const string notificationSubItem_XPath = ".//li[@dm-data-id='dm-notifications-item']";

        #endregion

        #region CONSTRUCTORS

        public DlkNotification(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkNotification(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        //public DlkButton(String ControlName, DlkControl ParentControl, String SearchType, String SearchValue)
        //    : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkNotification(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        #endregion

        #region PUBLIC METHODS

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                this.ScrollIntoViewUsingJavaScript();
                IsInit = true;
                GetNotificationItems();
            }
        }

        public void GetNotificationItems()
        {
            mNotificationItems = mElement.FindElements(By.XPath(notificationItem_XPath))
                .Where(x => x.Displayed).ToList();
        }

        public bool IsNotificationClosed(IWebElement notificationItem)
        {
            bool isClosed = false;

            if (notificationItem.FindElements(By.XPath(expandArrow_XPath)).Where(x => x.Displayed).Any())
            {
                isClosed = true;
            }
            else if (notificationItem.FindElements(By.XPath(collapseArrow_XPath)).Where(x => x.Displayed).Any())
            {
                isClosed = false;
            }
            else
            {
                isClosed = true;
                DlkLogger.LogInfo("No expand/collapse button found.");
            }

            return isClosed;
        }

        public IList<IWebElement> GetNotificationSubItems(IWebElement notificationItem)
        {
            IWebElement notificationSubItemContainer = notificationItem.FindElement(By.TagName("ul"));
            IList<IWebElement> notificationSubItems = notificationSubItemContainer.FindElements(By.XPath(notificationSubItem_XPath))
                .Where(x => x.Displayed).ToList();

            return notificationSubItems;
        }

        #endregion

        #region KEYWORDS

        [Keyword("SelectItem", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectItem(String Item)
        {
            try
            {
                Initialize();

                string notificationItemList = "";
                bool nFound = false;
                int notificationCount = 1;
                foreach (IWebElement notificationItem in mNotificationItems)
                {
                    IWebElement notificationItemHeader = notificationItem.FindElements(By.XPath(notificationItemHeader_XPath)).Count > 0 ?
                        notificationItem.FindElement(By.XPath(notificationItemHeader_XPath)) : throw new Exception("Notification header not found.");
                    string notificationHeader = notificationItemHeader.Text.Trim().ToLower();

                    notificationItemList = String.IsNullOrEmpty(notificationItemList) ? notificationItemList + "(" + notificationCount.ToString() + ") " + notificationHeader :
                        notificationItemList + Environment.NewLine + "(" + notificationCount.ToString() + ") " + notificationHeader;

                    if (!Item.Contains('~'))
                    {
                        if (notificationHeader == Item.ToLower())
                        {
                            notificationItem.Click();
                            DlkLogger.LogInfo("Selecting item: [" + Item + "] ...");
                            nFound = true;
                            break;
                        }
                    }
                    else
                    {
                        string[] Items = Item.Split('~');
                        if(notificationHeader == Items[0].ToLower())
                        {
                            bool isClosed = IsNotificationClosed(notificationItem);
                            if (isClosed)
                            {
                                notificationItem.Click();
                                DlkLogger.LogInfo("Selecting item: [" + Items[0].ToString() + "] ...");
                            }

                            IList<IWebElement> notificationSubItems = GetNotificationSubItems(notificationItem);
                            foreach (IWebElement notificationSubItem in notificationSubItems)
                            {
                                string subItemText = notificationSubItem.Text.Trim().ToLower();
                                notificationItemList += "~" + subItemText;

                                if (subItemText == Items[1].ToLower())
                                {
                                    notificationSubItem.Click();
                                    DlkLogger.LogInfo("Selecting sub item: [" + Items[1].ToString() + "] ...");
                                    nFound = true;
                                    break;
                                }
                            }
                            if (nFound)
                                break;
                        }
                    }
                    notificationCount++;
                }

                if (!nFound)
                {
                    DlkLogger.LogInfo("Actual list: [" + notificationItemList + "]");
                    throw new Exception("[" + Item + "] not found.");
                }

                DlkLogger.LogInfo("SelectItem() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectItem() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectPartialItem", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectPartialItem(String PartialItem)
        {
            try
            {
                Initialize();

                string notificationItemList = "";
                bool nFound = false;
                int notificationCount = 1;
                foreach (IWebElement notificationItem in mNotificationItems)
                {
                    IWebElement notificationItemHeader = notificationItem.FindElements(By.XPath(notificationItemHeader_XPath)).Count > 0 ?
                        notificationItem.FindElement(By.XPath(notificationItemHeader_XPath)) : throw new Exception("Notification header not found.");
                    string notificationHeader = notificationItemHeader.Text.Trim().ToLower();

                    notificationItemList = String.IsNullOrEmpty(notificationItemList) ? notificationItemList + "(" + notificationCount.ToString() + ") " + notificationHeader :
                        notificationItemList + Environment.NewLine + "(" + notificationCount.ToString() + ") " + notificationHeader;

                    if (!PartialItem.Contains('~'))
                    {
                        if (notificationHeader.Contains(PartialItem.ToLower()))
                        {
                            notificationItem.Click();
                            DlkLogger.LogInfo("Selecting partial item: [" + PartialItem + "] ...");
                            nFound = true;
                            break;
                        }
                    }
                    else
                    {
                        string[] Items = PartialItem.Split('~');
                        if (notificationHeader.Contains(Items[0].ToLower()))
                        {
                            bool isClosed = IsNotificationClosed(notificationItem);
                            if (isClosed)
                            {
                                notificationItem.Click();
                                DlkLogger.LogInfo("Selecting partial item: [" + Items[0].ToString() + "] ...");
                            }

                            IList<IWebElement> notificationSubItems = GetNotificationSubItems(notificationItem);
                            foreach (IWebElement notificationSubItem in notificationSubItems)
                            {
                                string subItemText = notificationSubItem.Text.Trim().ToLower();
                                notificationItemList += "~" + subItemText;

                                if (subItemText.Contains(Items[1].ToLower()))
                                {
                                    notificationSubItem.Click();
                                    DlkLogger.LogInfo("Selecting partial sub item: [" + Items[1].ToString() + "] ...");
                                    nFound = true;
                                    break;
                                }
                            }
                            if (nFound)
                                break;
                        }
                    }
                    notificationCount++;
                }

                if (!nFound)
                {
                    DlkLogger.LogInfo("Actual list: [" + notificationItemList + "]");
                    throw new Exception("[" + PartialItem + "] not found.");
                }

                DlkLogger.LogInfo("SelectPartialItem() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectPartialItem() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemInList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemInList(String Item, String ExpectedValue)
        {
            try
            {
                bool expectedValue;
                if (!Boolean.TryParse(ExpectedValue, out expectedValue))
                    throw new Exception("[" + ExpectedValue + "] is not a valid input for parameter TrueOrFalse.");

                Initialize();

                bool actualValue = false;
                string notificationItemList = "";
                bool nFound = false;
                int notificationCount = 1;
                foreach (IWebElement notificationItem in mNotificationItems)
                {
                    IWebElement notificationItemHeader = notificationItem.FindElements(By.XPath(notificationItemHeader_XPath)).Count > 0 ?
                        notificationItem.FindElement(By.XPath(notificationItemHeader_XPath)) : throw new Exception("Notification header not found.");
                    string notificationHeader = notificationItemHeader.Text.Trim().ToLower();

                    notificationItemList = String.IsNullOrEmpty(notificationItemList) ? notificationItemList + "(" + notificationCount.ToString() + ") " + notificationHeader :
                        notificationItemList + Environment.NewLine + "(" + notificationCount.ToString() + ") " + notificationHeader;

                    if (!Item.Contains('~'))
                    {
                        if (notificationHeader == Item.ToLower())
                        {
                            DlkLogger.LogInfo("Header item: [" + Item + "] found...");
                            actualValue = true;
                            nFound = true;
                            break;
                        }
                    }
                    else
                    {
                        string[] Items = Item.Split('~');
                        if (notificationHeader == Items[0].ToLower())
                        {
                            bool isClosed = IsNotificationClosed(notificationItem);
                            if (isClosed)
                            {
                                notificationItem.Click();
                                DlkLogger.LogInfo("Header item: [" + Items[0].ToString() + "] found.");
                            }

                            IList<IWebElement> notificationSubItems = GetNotificationSubItems(notificationItem);
                            foreach (IWebElement notificationSubItem in notificationSubItems)
                            {
                                string subItemText = notificationSubItem.Text.Trim().ToLower();
                                notificationItemList += "~" + subItemText;

                                if (subItemText == Items[1].ToLower())
                                {
                                    DlkLogger.LogInfo("Sub item: [" + Items[1].ToString() + "] found...");
                                    actualValue = true;
                                    nFound = true;
                                    break;
                                }
                            }
                            if (nFound)
                                break;
                        }
                    }
                    notificationCount++;
                }

                if (!nFound)
                {
                    DlkLogger.LogInfo("Actual list: [" + notificationItemList + "]");
                    throw new Exception("[" + Item + "] not found.");

                }

                DlkAssert.AssertEqual("VerifyItemInList(): ", expectedValue, actualValue);
                DlkLogger.LogInfo("VerifyItemInList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyPartialItemInList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyPartialItemInList(String PartialItem, String ExpectedValue)
        {
            try
            {
                bool expectedValue;
                if (!Boolean.TryParse(ExpectedValue, out expectedValue))
                    throw new Exception("[" + ExpectedValue + "] is not a valid input for parameter TrueOrFalse.");

                Initialize();

                bool actualValue = false;
                string notificationItemList = "";
                bool nFound = false;
                int notificationCount = 1;
                foreach (IWebElement notificationItem in mNotificationItems)
                {
                    IWebElement notificationItemHeader = notificationItem.FindElements(By.XPath(notificationItemHeader_XPath)).Count > 0 ?
                        notificationItem.FindElement(By.XPath(notificationItemHeader_XPath)) : throw new Exception("Notification header not found.");
                    string notificationHeader = notificationItemHeader.Text.Trim().ToLower();

                    notificationItemList = String.IsNullOrEmpty(notificationItemList) ? notificationItemList + "(" + notificationCount.ToString() + ") " + notificationHeader :
                        notificationItemList + Environment.NewLine + "(" + notificationCount.ToString() + ") " + notificationHeader;

                    if (!PartialItem.Contains('~'))
                    {
                        if (notificationHeader.Contains(PartialItem.ToLower()))
                        {
                            DlkLogger.LogInfo("Header item: [" + PartialItem + "] found...");
                            actualValue = true;
                            nFound = true;
                            break;
                        }
                    }
                    else
                    {
                        string[] Items = PartialItem.Split('~');
                        if (notificationHeader.Contains(Items[0].ToLower()))
                        {
                            bool isClosed = IsNotificationClosed(notificationItem);
                            if (isClosed)
                            {
                                notificationItem.Click();
                                DlkLogger.LogInfo("Header item: [" + Items[0].ToString() + "] found.");
                            }

                            IList<IWebElement> notificationSubItems = GetNotificationSubItems(notificationItem);
                            foreach (IWebElement notificationSubItem in notificationSubItems)
                            {
                                string subItemText = notificationSubItem.Text.Trim().ToLower();
                                notificationItemList += "~" + subItemText;

                                if (subItemText.Contains(Items[1].ToLower()))
                                {
                                    DlkLogger.LogInfo("Sub item: [" + Items[1].ToString() + "] found...");
                                    actualValue = true;
                                    nFound = true;
                                    break;
                                }
                            }
                            if (nFound)
                                break;
                        }
                    }
                    notificationCount++;
                }

                if (!nFound)
                {
                    DlkLogger.LogInfo("Actual list: [" + notificationItemList + "]");
                    throw new Exception("[" + PartialItem + "] not found.");
                }

                DlkAssert.AssertEqual("VerifyPartialItemInList(): ", expectedValue, actualValue);
                DlkLogger.LogInfo("VerifyPartialItemInList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPartialItemInList() failed : " + e.Message, e);
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

        [Keyword("GetVerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetVerifyExists(String VariableName, String SecondsToWait)
        {
            try
            {
                int wait = 0;
                if (!int.TryParse(SecondsToWait, out wait) || wait == 0)
                    throw new Exception("[" + SecondsToWait + "] is not a valid input for parameter SecondsToWait.");

                bool isExist = Exists(wait);
                string ActualValue = isExist.ToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetVerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetVerifyExists() failed : " + e.Message, e);
            }
        }

        #endregion

    }
}
