using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METHConverter.Controls
{
    public class ComboBox
    {
        public string Select(string Control, string Value)
        {
            StringBuilder action = new StringBuilder();
            action.Append("Function.WaitControlDisplayed(").Append(Control).AppendLine(");");
            action.Append(Control).AppendLine(".ScrollIntoViewUsingJavaScript();");
            action.Append(Control).AppendLine(".Click();");
            action.AppendLine("SeleniumControl comboBoxList = new SeleniumControl(sDriver,\"ComboBoxList\", \"class_display\", \"tCCBV\");");
            action.AppendLine("Function.WaitControlDisplayed(comboBoxList);");
            action.Append("IWebElement mItem = comboBoxList.mElement.FindElements(By.CssSelector(\"div\"))");
            action.Append(".Where(x => x.Displayed && new SeleniumControl( sDriver, \"x\", x).GetValue().Trim().ToLower() == \"");
            action.Append(Value.ToLower()).AppendLine("\").FirstOrDefault();");
            action.AppendLine("if(mItem != null)");
            action.AppendLine("mItem.Click();");
            action.AppendLine("else");
            action.Append(" throw new Exception(\"[").Append(Value).Append("] not found in list.\");");
            return action.ToString();
        }
    }
}
