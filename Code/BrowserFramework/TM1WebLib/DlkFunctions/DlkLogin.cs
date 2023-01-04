using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CommonLib.DlkSystem;
using TM1WebLib.System;
using CommonLib.DlkControls;
namespace TM1WebLib.DlkFunctions
{
    [Component("Login")]
    public static class DlkLogin
    {

        [Keyword("Login", new string[] {"1|text|User|superuser1",
                                        "2|text|Password|password",
                                        "3|text|System|deltek_QE"})]
        public static void Login(string sUser, string sPassword, string sServer)
        {
            DlkTM1WebKeywordHandler.ExecuteKeyword("Login", "UserID", "Set", new String[] { sUser });
            Thread.Sleep(5000);
            DlkTM1WebKeywordHandler.ExecuteKeyword("Login", "Password", "Set", new String[] { sPassword });
            DlkTM1WebKeywordHandler.ExecuteKeyword("Login", "Server", "Select", new String[] { sServer });
            Thread.Sleep(5000);
            DlkTM1WebKeywordHandler.ExecuteKeyword("Login", "Login", "Click", new String[] { "" });
            Thread.Sleep(8000);

        }
    }
}
