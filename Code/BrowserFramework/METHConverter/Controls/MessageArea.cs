using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METHConverter.Controls
{
    public class MessageArea
    {
        //NOTE: If using WaitLoadingFinished(), set the IsComponentModal parameter to True

        private String mstrCloseMessageAreaCSS = "div[title=Close]";
        private String mstrMessagesXPath = "//span[contains(@class,'msgTextHdr')]/following-sibling::span[not(contains(@class,'msgTextHdr'))]";

        public string Close(string Control)
        {
            StringBuilder action = new StringBuilder();
            action.Append("Function.WaitControlDisplayed(").Append(Control).AppendLine(");");
            action.Append(Control).Append(".mElement").Append(".FindElement(By.CssSelector(\"").Append(mstrCloseMessageAreaCSS).AppendLine("\")).Click();");
            return action.ToString();
        }

        public string VerifyMessageExists(string Control, string MessageText, string ExpectedExists)
        {
            StringBuilder action = new StringBuilder();
            action.Append("Function.WaitControlDisplayed(").Append(Control).AppendLine(");");
            action.Append("IList<IWebElement> msgElements = sDriver.Instance.FindElements(By.XPath(\"").Append(mstrMessagesXPath).AppendLine("\"));");
            action.Append("Function.AssertEqual(").Append(ExpectedExists.ToLower()).Append(", ");
            action.Append("msgElements.ToList().Select(x => new SeleniumControl( sDriver, \"x\", x).GetValue().Trim())");
            action.Append(".Any(x => x == \"").Append(MessageText).AppendLine("\"));");
            return action.ToString();
        }
    }
}
