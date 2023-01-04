using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METHConverter.Controls
{
    public class Dialog
    {
        public string ClickOkDialogIfExists(string Message)
        {
            StringBuilder action = new StringBuilder();
            action.AppendLine("Function.WaitLoadingFinished(Function.IsCurrentComponentModal(false));");
            action.Append("Function.ClickOkDialogIfExists(\"").Append(Message).AppendLine("\");");
            return action.ToString();
        }

        public string ClickOkDialogWithMessage(string Message)
        {
            StringBuilder action = new StringBuilder();
            action.AppendLine("Function.WaitLoadingFinished(Function.IsCurrentComponentModal(false));");
            action.Append("Function.ClickOkDialogWithMessage(\"").Append(Message).AppendLine("\");");
            return action.ToString();
        }
    }
}
