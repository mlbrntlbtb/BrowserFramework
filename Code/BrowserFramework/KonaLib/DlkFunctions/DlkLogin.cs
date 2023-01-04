using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CommonLib.DlkSystem;
using KonaLib.System;

namespace KonaLib.DlkFunctions
{
    [Component("Login")]
    public static class DlkLogin
    {
        [Keyword("Login", new string[] {"1|text|Email|sample@email.com",
                                        "2|text|Password|password"})]
        public static void Login(string sEmail, string sPassword)
        {

            DlkKonaKeywordHandler.ExecuteKeyword("Login", "EmailAddress", "Set", new String[] { sEmail });
            DlkKonaKeywordHandler.ExecuteKeyword("Login", "Password", "Set", new String[] { sPassword });
            DlkKonaKeywordHandler.ExecuteKeyword("Login", "Login", "Click", new String[] { "" });
            DlkKonaKeywordHandler.ExecuteKeyword("Main", "Reminders", "VerifyExists", new String[] { "TRUE" });
        }
    }
}
