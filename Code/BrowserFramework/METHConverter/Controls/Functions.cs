using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METHConverter.Controls
{
    public class Functions
    {
        public string LogComment(string Message)
        {
            StringBuilder action = new StringBuilder();
            action.Append("this.ScriptLogger.WriteLine(\"").Append(Message).AppendLine("\");");
            return action.ToString();
        }

        public string SendKeys(string Keys)
        {
            StringBuilder action = new StringBuilder();
            action.Append("Function.SendKeys(\"").Append(Keys).AppendLine("\");");
            return action.ToString();
        }

        public string Wait(string SecondsToWait)
        {
            StringBuilder action = new StringBuilder();
            action.Append("Function.Wait(").Append(SecondsToWait).AppendLine(");");
            return action.ToString();
        }

        public string ScrollDown()
        {
            StringBuilder action = new StringBuilder();
            action.AppendLine("Function.ScrollDown();");
            return action.ToString();
        }

        public string ScrollUp()
        {
            StringBuilder action = new StringBuilder();
            action.AppendLine("Function.ScrollUp();");
            return action.ToString();
        }
    }
}
