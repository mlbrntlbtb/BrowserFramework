using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METHConverter.Controls
{
    public class Tab
    {
        private String mstrTabItemsCSS = "span[class*='TabLbl']";

        public string Select(string Control, string TabName)
        {
            StringBuilder action = new StringBuilder();

            action.Append("Function.WaitControlDisplayed(").Append(Control).AppendLine(");");            
            action.Append("IWebElement mTab = ").Append(Control).Append(".mElement.FindElements(OpenQA.Selenium.By.XPath(\".//span[contains(@class, 'TabLbl')]\")).Where(x => new SeleniumControl(sDriver,\"Tab\", x).GetValue() == \"").Append(TabName).AppendLine("\").FirstOrDefault();");
            action.Append("if (sDriver.BrowserType.ToLower() != \"ie\") ");
            action.AppendLine("new SeleniumControl(sDriver,\"Tab\", mTab).ScrollIntoViewUsingJavaScript();");
            action.Append("else ");
            action.AppendLine("new SeleniumControl(sDriver,\"Tab\", mTab).ScrollTab(mTab);");
            action.AppendLine("mTab.Click();");
            return action.ToString();
        }

        public string VerifyTabs(string Control, string TabNames)
        {
            StringBuilder action = new StringBuilder();
            action.Append("Function.WaitControlDisplayed(").Append(Control).AppendLine(");");
            action.Append("string[] strTabs = ").Append(Control).Append(".mElement.FindElements(By.CssSelector(\"").Append(mstrTabItemsCSS);
            action.AppendLine ("\")).Where(x => x.Displayed).Select(x => new SeleniumControl( sDriver, \"x\", x).GetValue().Trim()).ToArray();");
            action.Append("Function.AssertEqual(String.Join(\"~\", strTabs)").Append(", \"").Append(TabNames).AppendLine("\");");
            return action.ToString();
        }
    }
}
