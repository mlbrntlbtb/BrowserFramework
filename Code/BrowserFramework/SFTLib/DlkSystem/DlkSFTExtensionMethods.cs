using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFTLib.DlkSystem
{
    public static class DlkSFTExtensionMethods
    {
        public static List<string> ConvertTildeDelimitedStringToList(this string tildeDelimitedString)
        {
            return tildeDelimitedString.Split('~').ToList();
        }
        /// <summary>
        /// Compares the list to the list from parameter strictly in order.
        /// </summary>
        /// <param name="firstList"></param>
        /// <param name="secondList"></param>
        public static void CompareToOtherList(this List<string> firstList, List<string> secondList)
        {
            foreach (var firstItem in firstList)
            {
                if (firstItem != secondList[firstList.IndexOf(firstItem)])
                    throw new Exception(String.Format("Item {0} and {1} do not match.", firstItem, secondList[firstList.IndexOf(firstItem)]));
            }
        }
        public static bool Contains(this string parameter, params string[] items)
        {
            foreach (var item in items)
            {
                if (parameter.Contains(item))
                    return true;
            }
            return false;
        }
        public static string GetCellValue(this IWebElement webElement)
        {
            var checkbox = webElement.FindElements(By.XPath(".//input")).FirstOrDefault();
            if (checkbox != null && checkbox.GetAttribute("Type") == "checkbox")
            {
                if (webElement.TagName == "div")
                    webElement = webElement.FindElement(By.XPath(".//input"));
                return webElement.HasAnAttributeOf("checked") ? "true" : "false";
            }
            else
            {
                var cellText = webElement.Text.Contains("\r\n") ? webElement.Text.Replace("\r\n", "~").Trim() : webElement.Text;
                return cellText.FormatSpace();
            }
                
        }
        public static void RightClickOnElement(this IWebElement webElement, string menuToSelect)
        {
            OpenQA.Selenium.Interactions.Actions actions = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
            actions = actions.ContextClick(webElement);
            actions.Perform();
            Thread.Sleep(1000);
            var verticalBox = webElement.FindElements(By.XPath("//*[contains(@class,'x-vertical-box-overflow-body')]"))
                .FirstOrDefault(element => element.Displayed);
            var verticalBoxItems = verticalBox.FindElements(By.XPath(".//*[contains(@class,'x-component-default x-menu-item')]"));
            var itemToClick = verticalBoxItems.FirstOrDefault(item => item.Text.Trim() == menuToSelect);

            if (itemToClick == null)
                throw new Exception($"{menuToSelect} not found");
            else
                itemToClick.Click();
        }
        public static IList<string> GetWebElementsValues(this IReadOnlyCollection<IWebElement> webElements)
        {
            List<string> values = new List<string>();
            foreach (var element in webElements)
                values.Add(DlkString.RemoveCarriageReturn(new DlkBaseControl("Control",element).GetValue()));

            return values;
        }
        public static void ClickUsingJS(this IWebElement element) {
            element.ExecJS("arguments[0].click()");
        }
        public static void ExecJS(this IWebElement element, String expression) {
            IJavaScriptExecutor js = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
            js.ExecuteScript(expression, element);
        }

        public static String ExecJSWithStringReturnValue(this IWebElement element, String expression)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
            return (String) js.ExecuteScript(expression, element);
        }

        public static void DoubleClick(this IWebElement element, int secToSleep = 0)
        {
            Thread.Sleep(secToSleep);
            if (DlkEnvironment.mBrowser == "IE")
                element.ExecJS("arguments[0].fireEvent('ondblclick');");
            else
                element.ExecJS($"var event = new MouseEvent('dblclick', {{'view': window,'bubbles': true,'cancelable': true}}); arguments[0].dispatchEvent(event);");
        }
        public static bool HasAnAttributeOf(this IWebElement element, String attr)
        {
            bool result = false;
            try
            {
                var value = element.GetAttribute(attr);
                result = value != null;
            }
            catch (Exception) { }
            return result;
        }
        
        public static String FormatSpace(this String text)
        {
            var textToReturn = string.Empty;
            foreach(char c in text)
            {
                if (Convert.ToByte(c) == 63 || Convert.ToByte(c) == 160)
                    textToReturn += ' ';
                else
                    textToReturn += c;
            }
            return textToReturn;
        }
        
    }
}

