using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METHConverter.Controls
{
    public class RadioButton
    {

        public string Select(string Control, string Value)
        {
            StringBuilder action = new StringBuilder();
            if (Convert.ToBoolean(Value))
            {
                action.Append("Function.WaitControlDisplayed(").Append(Control).AppendLine(");");
                action.Append(Control).AppendLine(".Click();");
            }            
            return action.ToString();
        }
    }
}
