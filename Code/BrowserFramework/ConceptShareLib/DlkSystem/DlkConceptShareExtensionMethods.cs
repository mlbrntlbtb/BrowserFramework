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

namespace ConceptShareLib.DlkSystem
{
    public static class DlkConceptShareExtensionMethods
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
            if (webElement.GetAttribute("Type") == "checkbox")
                return webElement.Selected ? "true" : "false";
            else
                return webElement.Text;
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
    }
}

