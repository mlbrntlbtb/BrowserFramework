using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METHConverter.Controls
{
    public class NavigationPath
    {
        public string VerifyMenuPath(string Control, string ExpectedValue)
        {
            StringBuilder action = new StringBuilder();
            action.Append("Function.AssertEqual(\"").Append(ExpectedValue).Append("\",").Append(Control).AppendLine(".GetValue().Trim());");
            return action.ToString();
        }
    }
}
