using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METHConverter.Controls
{
    public class TextBox
    {
        public string VerifyText(string Control, string ExpectedValue)
        {
            StringBuilder action = new StringBuilder();
            action.Append("Function.AssertEqual(\"").Append(ExpectedValue).Append("\",").Append(Control).AppendLine(".GetAttributeValue(\"value\"));");
            return action.ToString();
        }

        public string Set(string Control, string Value)
        {
            StringBuilder action = new StringBuilder();
            action.Append(Control).AppendLine(".Click();");
            action.Append(Control).Append(".SendKeys(\"").Append(Value).AppendLine("\", true);");

            action.AppendLine("Function.WaitLoadingFinished(Function.IsCurrentComponentModal(false));");

            action.Append(Control).AppendLine(".SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);");
            return action.ToString();
        }

        public string VerifyIfBlank(string Control, string ExpectedValue)
        {
            StringBuilder action = new StringBuilder();
            bool expectedValue = Convert.ToBoolean(ExpectedValue);
            action.Append("Function.AssertEqual(").Append(expectedValue.ToString().ToLower()).Append(",").Append("String.IsNullOrEmpty(").Append(Control).AppendLine(".GetAttributeValue(\"value\")));");
            return action.ToString();
        }

        public string ClickTextBoxButton(string Control)
        {
            StringBuilder action = new StringBuilder();
            action.Append(Control).AppendLine(".Click();");
            action.AppendLine("Thread.Sleep(800);");
            action.AppendLine("new SeleniumControl(sDriver,\"TextBoxButton\",\"class_display\",\"lookupIcon\").Click();");
            return action.ToString();
        }
    }
}
