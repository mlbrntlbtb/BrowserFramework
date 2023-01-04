using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AjeraTimeLib.DlkSystem;
using CommonLib.DlkSystem;
using System.Threading;

namespace AjeraTimeLib.DlkFunctions
{
    public class DlkLogin
    {
        /// <summary>
        /// Performs a login to Ajera
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Password"></param>
        /// <param name="Database"></param>
        public static void Login(String Url, String User, String Password, String Database)
        {
            DlkEnvironment.AutoDriver.Url = Url;

            // use the object store definitions 
            if (User != "")
            {
                DlkAjeraTimeKeywordHandler.ExecuteKeyword("Login", "Username", "Set", new String[] { User });
                DlkAjeraTimeKeywordHandler.ExecuteKeyword("Login", "Password", "Set", new String[] { Password });
                if (!String.IsNullOrEmpty(Database) && !String.IsNullOrWhiteSpace(Database))
                {
                    DlkAjeraTimeKeywordHandler.ExecuteKeyword("Login", "Database", "Select", new String[] { Database });
                }
                Thread.Sleep(250);
                DlkAjeraTimeKeywordHandler.ExecuteKeyword("Login", "Login", "Click", new String[] { "" });
                Thread.Sleep(5000);

            }
        }
    }
}
