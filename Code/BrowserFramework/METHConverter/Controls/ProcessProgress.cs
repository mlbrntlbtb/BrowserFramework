using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METHConverter.Controls
{
    public class ProcessProgress
    {
        public string WaitForProcessFinished(String Timeout)
        {
            StringBuilder action = new StringBuilder();
            action.Append("Function.WaitProcessProgressFinished(").Append(Timeout).AppendLine(");");
            return action.ToString();
        }
    }
}
