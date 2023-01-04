using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METHConverter.Controls
{
    public class NavigationMenu
    {
        public string SelectMenu(string Control, string MenuPath)
        {
            StringBuilder action = new StringBuilder();

            action.Append("if(!sDriver.Instance.FindElement(By.CssSelector(\"div[class='navCont']\")).Displayed) ");
            action.AppendLine("new SeleniumControl(sDriver,\"Browse\", \"css\", \"span[id = 'goToLbl']\").Click();");

            string[] menuItems = MenuPath.Split('~');
            int i = 0;
            foreach(string menuItem in menuItems)
            {
                action.Append("new SeleniumControl(sDriver,\"");
                action.Append(menuItem);
                action.Append("\", \"xpath\",");
                action.Append("\"//div[@class='");
                
                switch(i)
                {
                    case 0:
                        action.Append("busItem");
                        break;
                    case 1:
                        action.Append("deptItem");
                        break;
                    case 2:
                    case 3:
                        action.Append("navItem");
                        break;
                }
                action.Append("'][.='");
                action.Append(menuItem);
                action.Append("']\").Click();");
                action.AppendLine(); ;
                i++;
            }
            return action.ToString();
        }
    }
}
