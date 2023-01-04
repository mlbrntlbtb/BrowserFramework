using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METHConverter.Controls
{
    public class Menu
    {
        public string SelectMenu(string Control, string MenuPath)
        {
            StringBuilder action = new StringBuilder();
            string[] menuItems = MenuPath.Split('~');
            int i = 0;
            foreach (string menuItem in menuItems)
            {
                action.Append("new SeleniumControl(sDriver,\"");
                action.Append(menuItem);
                action.Append("\", \"xpath\",");
                switch (i)
                {
                    case 0:
                        action.Append("\"//span[@class='wMnuHead");
                        break;
                    case 1:
                        action.Append("\"//*[@class='wMnuPickLbl");
                        break;
                    case 2:
                    case 3:
                        action.Append("\"//*[@class='tlbrDDItem");
                        break;
                }
                action.Append("'][.='");
                action.Append(i == 0 ? menuItem.ToUpper() : menuItem);
                action.Append("']\").Click();");
                action.AppendLine(); ;
                i++;
            }
            return action.ToString();
        }
    }
}
