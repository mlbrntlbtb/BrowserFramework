using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.DlkSystem
{
    public interface IKeywordHandler
    {
        void ExecuteKeyword(String Screen, String ControlName, String Keyword, String[] Parameters);
    }
}
