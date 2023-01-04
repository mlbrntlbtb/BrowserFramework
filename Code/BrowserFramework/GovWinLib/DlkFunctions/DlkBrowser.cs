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
    [Component("Browser")]
    public class DlkBrowser
    {

        [Keyword("Back")]
        public static void Back()
        {
            DlkEnvironment.AutoDriver.Navigate().Back();
        }

        [Keyword("Close")]
        public static void Close()
        {
            DlkEnvironment.AutoDriver.Close();
        }

        [Keyword("Forward")]
        public static void Forward()
        {
            DlkEnvironment.AutoDriver.Navigate().Forward();
        }

        [Keyword("Refresh")]
        public static void Refresh()
        {
            DlkEnvironment.AutoDriver.Navigate().Refresh();
        }

        [Keyword("GoToUrl", new String[] { "1|text|Url|http://www.google.com" })]
        public static void GoToUrl(String sUrl)
        {
            DlkEnvironment.AutoDriver.Url = sUrl;
            DlkEnvironment.AutoDriver.Navigate();
        }

        [Keyword("GoToPartialUrl", new String[] { "1|text|Partial Url without domain part|/neo/opportunity/view/45253" })]
        public static void GoToPartialUrl(String partialUrl)
        {
            string sUrl = DlkEnvironment.AutoDriver.Url;
            string cleanPartialUrl = partialUrl.TrimStart('/');
            Uri tempUri = new Uri(sUrl);
            string partialDomainPartUrl = sUrl.Substring(0, sUrl.IndexOf(tempUri.PathAndQuery) + 1).TrimEnd('/');
            string newUrl = string.Format("{0}/{1}", partialDomainPartUrl, cleanPartialUrl);

            DlkEnvironment.AutoDriver.Url = newUrl;
            DlkEnvironment.AutoDriver.Navigate();
        }

        [Keyword("GetUrl", new String[] {"1|text|VariableName|MyUrl"})]
        public static void GetUrl(String sVariableName)
        {

            DlkVariable.SetVariable(sVariableName, DlkEnvironment.AutoDriver.Url);
        }

        [Keyword("GetBrowserTitle", new String[] { "1|text|VariableName|MyBrowserTitle" })]
        public static void GetBrowserTitle(String sVariableName)
        {

            DlkVariable.SetVariable(sVariableName, DlkEnvironment.AutoDriver.Title);           
        }

        [Keyword("VerifyPartialUrl", new String[] { "1|text|Partial Url without domain part|/neo/opportunity/view/45253" })]
        public static void VerifyPartialUrl(String expectedPartialUrl)
        {

            string sUrl = DlkEnvironment.AutoDriver.Url;
            Uri tempUri = new Uri(sUrl);
            string actualPartialUrl = tempUri.PathAndQuery;

            DlkAssert.AssertEqual("VerifyPartialUrl", expectedPartialUrl, actualPartialUrl);
        }

        [Keyword("VerifyUrl", new String[] { "1|text|ExpectedUrl|http://www.google.com" })]
        public static void VerifyUrl(String sExpectedUrl)
        {
            DlkAssert.AssertEqual("VerifyURL", sExpectedUrl, DlkEnvironment.AutoDriver.Url);
        }

        [Keyword("FocusBrowserWithTitle", new String[] { "1|text|Title|GovWinIQ -",
                                                         "2|text|Timeout|60" })]
        public static void FocusBrowserWithTitle(String sTitle, String sTimeout)
        {
            DlkEnvironment.SetBrowserFocus(sTitle, Convert.ToInt32(sTimeout));
        }

        [Keyword("FocusBrowserWithUrl", new String[] { "1|text|Url|www.google.com",
                                                       "2|text|Timeout|60"})]
        public static void FocusBrowserWithUrl(String sUrl, String sTimeout)
        {
            DlkEnvironment.SetBrowserFocusByUrl(sUrl, Convert.ToInt32(sTimeout));
        }
    }
}

