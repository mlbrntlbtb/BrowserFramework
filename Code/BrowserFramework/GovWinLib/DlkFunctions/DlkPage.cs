using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;
using System.Reflection;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
//using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;

using GovWinLib.DlkControls;
using GovWinLib.System;

namespace GovWinLib.DlkFunctions
{
    [Component("Page")]
    public class DlkPage
    {
        private static String sUrl = "";

        private static void Back()
        {
            DlkEnvironment.AutoDriver.Url = sUrl;
            DlkEnvironment.AutoDriver.Navigate();
        }

        private static void PerformAction(Action action, params String[] inputs)
        {
            if (action == null)
                throw new ArgumentNullException("action"); // slightly safer...

            var actionFactory = new KeywordActionFactory(inputs);
            actionFactory.GenerateAction()
                         .Do(action);
        }

        [Keyword("ClickWhiteSpaceOnPage")]
        public static void ClickWhiteSpaceOnPage()
        {
            DlkEnvironment.AutoDriver.FindElement(By.XPath("//html")).Click();
        }

        [Keyword("VerifyLinkBulletListSameModalHeaderName")]
        public static void VerifyLinkBulletListSameModalHeaderName(String ListXPath)
        {
            IList<IWebElement> linkList = DlkEnvironment.AutoDriver.FindElements(By.XPath(ListXPath));

            foreach(IWebElement link in linkList)
            {
                if (link.Text.ToLower().Equals("govwin"))
                    ClickVerifyModalHeader(link.Text, link.Text, ListXPath + "[contains(.,'GovWin')]");
                else
                    ClickVerifyModalHeader(link.Text, link.Text);
            }

            DlkLogger.LogInfo("Succesfully Verified all links in Bullet List.");
        }

        [Keyword("ClickLink", new String[] { "1|text|Link Caption|Tab1"})]
        public static void ClickLink(String LinkCaption)
        {
            PerformAction(() =>
            {
                sUrl = DlkEnvironment.AutoDriver.Url;
                IWebElement link = DlkEnvironment.AutoDriver.FindElement(By.XPath("//a[contains(.,'" + LinkCaption + "')]"));
                link.Click();
                DlkLogger.LogInfo("Succesfully clicked link: " + LinkCaption);
            }, new String[] { "retry" });
        }

        [Keyword("VerifyHeader", new String[] { "1|text|Link Caption|Tab1" })]
        public static void VerifyHeader(String HeaderCaption)
        {
            PerformAction(() =>
            {
                IWebElement header;
                if (DlkEnvironment.AutoDriver.FindElements(By.XPath("//span[@class='productNameHeader']")).Any())
                {
                    header = DlkEnvironment.AutoDriver.FindElement(By.XPath("//span[@class='productNameHeader']"));
                }
                else
                {
                    header = DlkEnvironment.AutoDriver.FindElement(By.XPath("//h1"));
                }
                DlkAssert.AssertEqual("VerifyHeader: ", HeaderCaption, header.Text);
            }, new String[] { "retry" });
        }

        [Keyword("VerifyHeaderContains", new String[] { "1|text|Link Caption|Tab1" })]
        public static void VerifyHeaderContains(String HeaderCaption)
        {
            PerformAction(() =>
            {
                IWebElement header;
                if (DlkEnvironment.AutoDriver.FindElements(By.XPath("//span[@class='productNameHeader']")).Any())
                {
                    header = DlkEnvironment.AutoDriver.FindElement(By.XPath("//span[@class='productNameHeader']"));
                }
                else
                {
                    header = DlkEnvironment.AutoDriver.FindElement(By.XPath("//h1"));
                }
                bool actual = header.Text.Contains(HeaderCaption.Trim());
                DlkAssert.AssertEqual("VerifyHeaderContains: ", true, actual);
                
            }, new String[] { "retry" });
        }

        [Keyword("VerifySubHeader", new String[] { "1|text|Link Caption|Tab1" })]
        public static void VerifySubHeader(String SubHeaderCaption)
        {
            PerformAction(() =>
            {
                IWebElement header;
                if (DlkEnvironment.AutoDriver.FindElements(By.XPath("//span[@class='pageTitleSubheader']")).Any())
                {
                    header = DlkEnvironment.AutoDriver.FindElement(By.XPath("//span[@class='pageTitleSubheader']"));
                }
                else
                {
                    header = DlkEnvironment.AutoDriver.FindElement(By.XPath("//h2"));
                }

                DlkAssert.AssertEqual("VerifySubHeader: ", SubHeaderCaption, header.Text);
            }, new String[] { "retry" });
        }

        [Keyword("VerifySubHeaderContains", new String[] { "1|text|Link Caption|Tab1" })]
        public static void VerifySubHeaderContains(String SubHeaderCaption)
        {
            PerformAction(() =>
            {
                IWebElement header;
                if (DlkEnvironment.AutoDriver.FindElements(By.XPath("//span[@class='pageTitleSubheader']")).Any())
                {
                    header = DlkEnvironment.AutoDriver.FindElement(By.XPath("//span[@class='pageTitleSubheader']"));
                }
                else
                {
                    header = DlkEnvironment.AutoDriver.FindElement(By.XPath("//h2"));
                }
                bool actual = header.Text.Contains(SubHeaderCaption);
                DlkAssert.AssertEqual("VerifyHeaderContains: ", true, actual);
            }, new String[] { "retry" });
        }

        [Keyword("VerifyModalHeaderContains", new String[] { "1|text|ExpectedModalHeader|Caption" })]
        public static void VerifyModalHeaderContains(String ExpectedModalHeader)
        {
            PerformAction(() =>
            {              
                IWebElement modalHeader = DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@id='cboxContent']//h2"));
                bool actual = modalHeader.Text.Contains(ExpectedModalHeader);
                DlkAssert.AssertEqual("VerifyModalHeaderContains", true, actual);
                DlkEnvironment.AutoDriver.FindElement(By.Id("cboxClose")).Click();
            }, new String[] { "retry" });
        }

        [Keyword("VerifyMegaMenuLinkListSameHeaderName")]
        public static void VerifyMegaMenuLinkListSameHeaderName(String Megamenu, String ListXPath)
        {
            IWebElement megaMenuLink = DlkEnvironment.AutoDriver.FindElement(By.XPath("//ul[@id='megaMenu']//a[contains(.,'" + Megamenu + "')]"));
            megaMenuLink.Click();
            IList<IWebElement> linkList = DlkEnvironment.AutoDriver.FindElements(By.XPath(ListXPath));

            foreach (IWebElement link in linkList)
            {                
                link.Click();
                Thread.Sleep(5000);
                IWebElement linkHeader = DlkEnvironment.AutoDriver.FindElement(By.XPath("//span[@class='pageTitleSubheader']"));
                if (linkHeader != null)
                {
                    DlkAssert.AssertEqual("ClickVerifyNextPageHeader() ", link.Text, linkHeader.Text);
                }
                else
                {
                    IWebElement header = DlkEnvironment.AutoDriver.FindElement(By.XPath("//h1"));
                    DlkAssert.AssertEqual("ClickVerifyNextPageHeader() ", link.Text, header.Text);
                }
                Back();
                megaMenuLink.Click();
            }

            DlkLogger.LogInfo("Succesfully Verified all links in List.");
        }

        [Keyword("GetListCount")]
        public static void GetListCount(String TableName,String OutputVariable)
        {

            int cnt = DlkEnvironment.AutoDriver.FindElements(By.XPath("//span[contains(.,'"+TableName+"')]//parent::div//parent::td//parent::tr//following-sibling::tr//li")).Count;
            DlkVariable.SetVariable(OutputVariable, cnt.ToString());
            DlkLogger.LogInfo("GetListCount Successful. List Count = " + cnt);
        }

        [Keyword("GetResultsTableCount")]
        public static void GetResultsTableCount(String OutputVariable)
        {
            IWebElement resTable = DlkEnvironment.AutoDriver.FindElement(By.XPath("//table[contains(@class,'resultsTable')]"));
            int cnt = resTable.FindElements(By.XPath(".//tbody//tr")).Count -1;
            DlkVariable.SetVariable(OutputVariable, cnt.ToString());
            DlkLogger.LogInfo("GetResultsTableDataCount Successful. Table Row Count = " + cnt);
        }

        [Keyword("GetDataTableCount")]
        public static void GetDataTableCount(String OutputVariable)
        {
            IWebElement dataTable = DlkEnvironment.AutoDriver.FindElement(By.XPath("//table[contains(@class,'dataTable')]"));
            int cnt = dataTable.FindElements(By.XPath(".//tbody//tr[string-length(text()) = 0]")).Count;
            DlkVariable.SetVariable(OutputVariable, cnt.ToString());
            DlkLogger.LogInfo("GetDataTableCount Successful. Table Row Count = " + cnt);
        }

        [Keyword("GetSubResultsTableCount")]
        public static void GetSubResultsTableCount(String OutputVariable)
        {
            int x = 0;
            IList<IWebElement> tabs = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@id='tabs']//li"));
            for(int i = 0; i < tabs.Count; i++)
            { 
                int y=0;
                tabs = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@id='tabs']//li"));
                tabs[i].Click();
                Boolean isPresent = DlkEnvironment.AutoDriver.FindElements(By.XPath("//table[contains(@class,'resultsTable')]")).Count > 0;
                IWebElement resTable;
                if (isPresent)
                {
                    resTable = DlkEnvironment.AutoDriver.FindElement(By.XPath("//table[contains(@class,'resultsTable')]"));
                    y = resTable.FindElements(By.XPath(".//tbody//tr")).Count - 1;
                }
                x = x + y;
                y = 0;
                while(DlkEnvironment.AutoDriver.FindElements(By.XPath("//table[@class='resultsHeaderBtmBorder']//a[contains(.,'Next')]")).Count > 0)
                {
                    DlkEnvironment.AutoDriver.FindElement(By.XPath("//table[@class='resultsHeaderBtmBorder']//a[contains(.,'Next')]")).Click();
                    resTable = DlkEnvironment.AutoDriver.FindElement(By.XPath("//table[contains(@class,'resultsTable')]"));
                    y = resTable.FindElements(By.XPath(".//tbody//tr")).Count - 1;
                    x = x + y;
                }
                
            }
            DlkVariable.SetVariable(OutputVariable, x.ToString());
            DlkLogger.LogInfo("GetSubResultsTableCount Successful. Table Row Count = " + x);
        }

        [Keyword("GetSubDataTableCount")]
        public static void GetSubDataTableCount(String OutputVariable)
        {
            int x = 0;
            IList<IWebElement> tabs = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[contains(@class,'tabNav')]//li"));
            for (int i = 0; i < tabs.Count; i++)
            {
                int y = 0;
                tabs = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[contains(@class,'tabNav')]//li"));
                tabs[i].Click();
                Boolean isPresent = DlkEnvironment.AutoDriver.FindElements(By.XPath("//table[contains(@class,'dataTable')]")).Count > 0;
                IWebElement dataTable;
                if (!DlkEnvironment.AutoDriver.FindElement(By.XPath("//table[contains(@class,'dataTable')]//td")).GetAttribute("class").Contains("empty") && isPresent)
                {
                    dataTable = DlkEnvironment.AutoDriver.FindElement(By.XPath("//table[contains(@id,'DataTables_Table')]"));
                    y = dataTable.FindElements(By.XPath("//tbody//tr")).Count - 1;
                }
                x = x + y;
            }
            DlkVariable.SetVariable(OutputVariable, x.ToString());
            DlkLogger.LogInfo("GetSubDataTableCount Successful. Table Row Count = " + x);
        }

        [Keyword("VerifyListLinksAndCompareItemCount", new String[] { "1|text|Expected Result|True" })]
        public void VerifyLinkAndCompareItemCount(String ulXPATH, String TrueOrFalse)
        {
            int tableCountActual = 0;
            IList<IWebElement> linkList = DlkEnvironment.AutoDriver.FindElements(By.XPath(ulXPATH + "//a"));

            foreach (IWebElement link in linkList)
            {
                //Iterate Thru list
                int expectedCount = ConvertToInt(link.Text.ToString());
                String itemStr = Regex.Replace(link.Text.ToString(), "[^a-zA-Z]", "");

                link.Click();

                Thread.Sleep(5000);
                IWebElement linkHeader = DlkEnvironment.AutoDriver.FindElement(By.XPath("//span[@class='pageTitleSubheader']"));

                if (linkHeader != null)
                {

                    //If SubHeader Format (Classic View)

                    DlkAssert.AssertEqual("VerifyListLinksAndCompareItemCount() Table Row Count: ", expectedCount, tableCountActual);
                    DlkAssert.AssertEqual("ClickVerifyNextPageHeader() ", link.Text, linkHeader.Text);
                }
                else
                {
                    //If Header
                    IWebElement header = DlkEnvironment.AutoDriver.FindElement(By.XPath("//h1"));

                    //Compare Table Count with Link Value                    
                    DlkTable table = new DlkTable("tempTable", "XPATH", "//table");
                    table.GetTableRowCount("tableCount");                    
                    int.TryParse(DlkVariable.GetVariable("O{tableCount}"), out tableCountActual);
                    
                    //If with Tabs

                    //If Links Table

                    //Verification
                    DlkAssert.AssertEqual("VerifyListLinksAndCompareItemCount() Table Row Count: ", expectedCount, tableCountActual);
                    DlkAssert.AssertEqual("VerifyListLinksAndCompareItemCount() Header Compare:", link.Text, header.Text);
                }

            }
        }

        private static int ConvertToInt(String input)
        {
            // Replace everything that is no a digit.
            String inputCleaned = Regex.Replace(input, "[^0-9]", "");

            int value = 0;

            // Tries to parse the int, returns false on failure.
            if (int.TryParse(inputCleaned, out value))
            {
                // The result from parsing can be safely returned.
                return value;
            }

            return 0; // Or any other default value.
        }

        [Keyword("ClickVerifyNextPageHeader", new String[] { "1|text|Link Caption|Tab1", "2|text|Header Caption|ifExist" })]
        public static void ClickVerifyNextPageHeader(String LinkCaption, String HeaderCaption)
        {
            PerformAction(() =>
            {
                sUrl = DlkEnvironment.AutoDriver.Url;
                IWebElement link = DlkEnvironment.AutoDriver.FindElement(By.XPath("//a[contains(.,'" + LinkCaption + "')]"));
                link.Click();
                Thread.Sleep(5000);
                IWebElement header = DlkEnvironment.AutoDriver.FindElement(By.XPath("//h1"));
                DlkAssert.AssertEqual("ClickVerifyNextPageHeader() ", HeaderCaption, header.Text);             
                Back();
            }, new String[] { "retry" });
        }

        [Keyword("ClickVerifyNextPageSubHeader", new String[] { "1|text|Link Caption|Tab1", "2|text|Header Caption|ifExist" })]
        public static void ClickVerifyNextPageSubHeader(String LinkCaption, String SubHeaderCaption)
        {
            PerformAction(() =>
            {
                sUrl = DlkEnvironment.AutoDriver.Url;
                IWebElement link = DlkEnvironment.AutoDriver.FindElement(By.XPath("//a[contains(.,'" + LinkCaption + "')]"));
                link.Click();
                Thread.Sleep(1000);
                IWebElement header = DlkEnvironment.AutoDriver.FindElement(By.XPath("//h2"));
                DlkAssert.AssertEqual("ClickVerifyNextPageSubHeader() ", SubHeaderCaption, header.Text);

                Back();
            }, new String[] { "retry" });

        }

        [Keyword("ClickVerifyNextPageUrl", new String[] { "1|text|Link Caption|Tab1", "2|text|Header Caption|ifExist" })]
        public static void ClickVerifyNextPageUrl(String LinkCaption, String ExpectedUrl)
        {
            PerformAction(() =>
            {
                Thread.Sleep(1000);
                sUrl = DlkEnvironment.AutoDriver.Url;
                IWebElement link = DlkEnvironment.AutoDriver.FindElement(By.XPath("//a[contains(.,'" + LinkCaption + "')]"));
                link.Click();
                Thread.Sleep(1000);
                DlkAssert.AssertEqual("ClickVerifyNextPageUrl", ExpectedUrl, DlkEnvironment.AutoDriver.Url);
                Back();
            }, new String[] { "retry" });
        }

        [Keyword("ParseListItemStringToInt")]
        public static void ParseListItemStringToInt(String ListPartialText,String OutpuVariable)
        {
            PerformAction(() =>
            {
                IWebElement link = DlkEnvironment.AutoDriver.FindElement(By.XPath("//a[contains(.,'" + ListPartialText + "')]"));
                String strCount = Regex.Replace(link.Text, "[^0-9]+", string.Empty);
                if (strCount.Trim().Equals(""))
                    strCount = "0";
                DlkVariable.SetVariable(OutpuVariable, strCount);
            }, new String[] { "retry" });
        }

        [Keyword("ClickVerifyNextPagePartialUrl", new String[] { "1|text|Link Caption|Tab1", "2|text|Header Caption|ifExist" })]
        public static void ClickVerifyNextPagePartialUrl(String LinkCaption, String ExpectedPartialUrl)
        {
            PerformAction(() =>
            {
                sUrl = DlkEnvironment.AutoDriver.Url;
                IWebElement link = DlkEnvironment.AutoDriver.FindElement(By.XPath("//a[contains(.,'" + LinkCaption + "')]"));
                link.Click();
                Thread.Sleep(1000);
                DlkAssert.AssertEqual("ClickVerifyNextPagePartialUrl", true, DlkEnvironment.AutoDriver.Url.Contains(ExpectedPartialUrl));
                Back();
            }, new String[] { "retry" });
        }

        [Keyword("ClickVerifyNextPagePartialUrlNoBack", new String[] { "1|text|Link Caption|Tab1", "2|text|Header Caption|ifExist" })]
        public static void ClickVerifyNextPagePartialUrlNoBack(String LinkCaption, String ExpectedPartialUrl)
        {
            PerformAction(() =>
            {
                sUrl = DlkEnvironment.AutoDriver.Url;
                IWebElement link = DlkEnvironment.AutoDriver.FindElement(By.XPath("//a[contains(.,'" + LinkCaption + "')]"));
                link.Click();
                Thread.Sleep(1000);
                DlkAssert.AssertEqual("ClickVerifyNextPagePartialUrl", true, DlkEnvironment.AutoDriver.Url.Contains(ExpectedPartialUrl));
            }, new String[] { "retry" });
        }

        [Keyword("ClickVerifyNewTabPartialUrl", new String[] { "1|text|Link Caption|Tab1", "2|text|Header Caption|ifExist" })]
        public static void ClickVerifyNewTabPartialUrl(String LinkCaption, String ExpectedPartialUrl)
        {
            Thread.Sleep(1000);
            IWebElement link = DlkEnvironment.AutoDriver.FindElement(By.XPath("//a[contains(.,'" + LinkCaption + "')]"));
            link.Click();
            Thread.Sleep(1000);
            DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
            DlkAssert.AssertEqual("ClickVerifyNewTabPartialUrl", true, DlkEnvironment.AutoDriver.Url.Contains(ExpectedPartialUrl));
            DlkEnvironment.AutoDriver.Close();
            DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[0]);

        }

        [Keyword("ClickVerifyModalHeader", new String[] { "1|text|Link Caption|Tab1", "2|text|Header Caption|ifExist" })]
        public static void ClickVerifyModalHeader(String LinkCaption, String ExpectedModalHeader, String xpath = "")
        {
            PerformAction(() =>
            {
                String srchStr = "//a[contains(.,'" + LinkCaption + "')]";
                if (LinkCaption.ToLower().Equals("govwin"))
                    srchStr = xpath;
                IWebElement link = DlkEnvironment.AutoDriver.FindElement(By.XPath(srchStr));
                link.Click();
                Thread.Sleep(1000);
                IWebElement modalHeader = DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@id='cboxContent']//h2"));
                DlkAssert.AssertEqual("ClickVerifyModalHeader", ExpectedModalHeader, modalHeader.Text);
                DlkEnvironment.AutoDriver.FindElement(By.Id("cboxClose")).Click();
            }, new String[] { "retry" });

        }

        [Keyword("ExpandLinkVerifyText", new String[] { "1|text|Link Caption|Tab1", "2|text|Header Caption|ifExist" })]
        public static void ExpandLinkVerifyText(String LinkCaption, String ExpectedText)
        {
            String searchStr = "//a[contains(.,'" + LinkCaption + "')]";
            IWebElement link = DlkEnvironment.AutoDriver.FindElement(By.XPath(searchStr));
            link.Click();
            IWebElement expandedText = DlkEnvironment.AutoDriver.FindElement(By.XPath(searchStr + "//following-sibling::span"));
            Thread.Sleep(1000);
            DlkAssert.AssertEqual("ExpandLinkVerifyText", ExpectedText, expandedText.Text);
        }
    }
}
