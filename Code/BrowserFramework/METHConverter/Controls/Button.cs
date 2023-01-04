using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METHConverter.Controls
{
    public class Button
    {
        public string Click(string Control)
        {
            StringBuilder action = new StringBuilder();
            action.Append("Function.WaitControlDisplayed(").Append(Control).AppendLine(");");
            action.Append("if (").Append(Control).Append(".mElement.GetAttribute(\"class\") == \"popupBtn\" && ");
            action.AppendLine("sDriver.BrowserType == \"ie\")");
            action.Append(Control).AppendLine(".Click(5,5);");
            action.Append("else ");
            action.Append(Control).AppendLine(".Click(4.5);");
            return action.ToString();
        }
    }
}
