using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METHConverter.Controls
{
    public class Toolbar
    {
        public string ClickToolbarButton(string Control, string Buttons)
        {
            StringBuilder action = new StringBuilder();
            string[] mButtons = Buttons.Split('~');

            action.Append("Function.WaitControlDisplayed(").Append(Control).AppendLine(");");
            
            action.Append("IWebElement tlbrBtn = ").Append(Control).Append(".mElement.FindElements(By.XPath(\".//*[@class='tbBtnContainer']//div[contains(@title,'").Append(mButtons[0]).AppendLine("')]\")).FirstOrDefault();");
            action.Append("if (tlbrBtn==null) throw new Exception(\"Unable to find button ").Append(Buttons).AppendLine(".\");");
            action.AppendLine("tlbrBtn.Click();");

            if (mButtons.Count() > 1)
            { 
                action.Append(Control).Append(".mElement.FindElements(By.XPath(\"//*[@class = 'tlbrDDItem' and contains(text(),'").Append(mButtons[1]).AppendLine("')]\")).FirstOrDefault().Click();");
            }

            return action.ToString();
        }
        public string VerifyToolbarButtonExist(string Control, string ButtonCaption, string ExpectedValue)
        {
            StringBuilder action = new StringBuilder();
            string[] mButtons = ButtonCaption.Split('~');
            action.Append("Function.WaitControlDisplayed(").Append(Control).AppendLine(");");
            action.Append("IWebElement tlbrBtn = ").Append(Control).Append(".mElement.FindElements(By.XPath(\".//*[@class='tbBtnContainer']//div[contains(@title,'").Append(mButtons[0]).AppendLine("')]\")).FirstOrDefault();");
            if (mButtons.Count() > 1)
            {
                action.AppendLine("tlbrBtn.Click();");
                action.Append("tlbrBtn = ").Append(Control).Append(".mElement.FindElements(By.XPath(\"//*[@class = 'tlbrDDItem' and contains(text(),'").Append(mButtons[1]).AppendLine("')]\")).FirstOrDefault();");
            }
            action.AppendLine("Boolean bFound = tlbrBtn != null ? true : false;");
            action.Append("Function.AssertEqual(").Append(ExpectedValue.ToLower()).Append(", bFound").AppendLine(");");
            return action.ToString();
        }
    }
}
