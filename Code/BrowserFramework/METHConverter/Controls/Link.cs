using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METHConverter.Controls
{
    public class Link
    {
        public string Click(string Control)
        {
            StringBuilder action = new StringBuilder();
            action.Append("Function.WaitControlDisplayed(").Append(Control).AppendLine(");");
            action.Append(Control).AppendLine(".Click(1.5);");
            return action.ToString();
        }
    }
}
