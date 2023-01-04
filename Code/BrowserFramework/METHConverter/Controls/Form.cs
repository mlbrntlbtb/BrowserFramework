using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METHConverter.Controls
{
    public class Form
    {
        #region PUBLIC METHODS (Keywords)
        public string Close(string Control)
        {
            StringBuilder action = new StringBuilder();
            action.Append("Function.WaitControlDisplayed(").Append(Control).AppendLine(");");
            
            action.Append("IWebElement formBttn = ").Append(TitleSelector(Control, "Close")).AppendLine(".Where(x=>x.Displayed).FirstOrDefault();");
            action.AppendLine("\t" + "if(formBttn!=null) { new SeleniumControl(sDriver, \"Close\", formBttn).ScrollIntoViewUsingJavaScript(); formBttn.Click();}");
            action.AppendLine("\t" + "else throw new Exception(\"Close Button not found \");");
            
            return action.ToString();
        }
        
        public string ClickButton(string Control, string ButtonName)
        {
            string[] buttonNames = ButtonName.Split('~');
            StringBuilder action = new StringBuilder();
            action.Append("Function.WaitControlDisplayed(").Append(Control).AppendLine(");");


            string titleSelect, textContentSelect;
            if (buttonNames.Count() == 1)
            {
                titleSelect = TitleSelector(Control, ButtonName);
                textContentSelect = TextContentSelector(Control, ButtonName);
                
                action.Append("IWebElement formBttn = ").Append(titleSelect).Append(".Count <= 0 ? ");
                action.Append(textContentSelect).AppendLine(".FirstOrDefault() :");
                action.Append(titleSelect).AppendLine(".FirstOrDefault();");
                action.AppendLine("if (formBttn!=null) { new SeleniumControl(sDriver ,\"FormButton\",formBttn).MouseOver(); formBttn.Click(); }");
                action.AppendLine("else throw new Exception(\" " + ButtonName + " not found \");");
            }
            else
            {
                action.AppendLine("//TODO: Auto-generated code for ClickButton (Series) on Form not yet implemented.");
            }
            
            return action.ToString();
        }

        public string ClickButtonIfExists(string Control, string ButtonName)
        {
            StringBuilder action = new StringBuilder();

            action.Append("Function.WaitControlDisplayed(").Append(Control).AppendLine(");");

            string titleSelect = TitleSelector(Control, ButtonName);
            string textContentSelect = TextContentSelector(Control, ButtonName);
            
            action.Append("IWebElement formBttn = ").Append(titleSelect).Append(".Count <= 0 ? ");
            action.Append(textContentSelect).AppendLine(".FirstOrDefault() :");
            action.Append(textContentSelect).AppendLine(".FirstOrDefault();");
            action.AppendLine("if (formBttn!=null && formBttn.Displayed)");
            action.AppendLine("{ new SeleniumControl(sDriver ,\"FormButton\",formBttn).MouseOver(); formBttn.Click();");
            action.Append("this.ScriptLogger.WriteLine(\"Button [").Append(ButtonName).AppendLine("] found and clicked.\", Logger.MessageType.INF);");
            action.AppendLine("}");
            
            return action.ToString();            
        }
        #endregion

        #region PRIVATE METHODS
        private string TitleSelector(string Control, string ButtonName)
        {
            return Control + ".mElement.FindElements(By.CssSelector(\"*[title*='" + ButtonName + "']\"))";
        }

        private string TextContentSelector(string Control, string ButtonName)
        {
            return Control + ".mElement.FindElements(By.XPath(\".//*[contains(text(),'" + ButtonName + "')]\"))";
        }
        #endregion 
    }
}
