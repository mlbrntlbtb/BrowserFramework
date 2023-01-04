using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METHConverter.Controls
{
    public class CheckBox
    {
        public string Set(string Control, string Value)
        {
            StringBuilder action = new StringBuilder();
            action.Append("Function.WaitControlDisplayed(").Append(Control).AppendLine(");");
            action.Append(Control).AppendLine(".ScrollIntoViewUsingJavaScript();");

            action.Append("if (Convert.ToBoolean(").Append(Control).Append(".GetAttributeValue(\"checked\")) != ").Append(Convert.ToBoolean(Value).ToString().ToLower()).Append(") ");
            action.Append(Control).AppendLine(".Click(4.3);");
            
            return action.ToString();
        }
    }
}
