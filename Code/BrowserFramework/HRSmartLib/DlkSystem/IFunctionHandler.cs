using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.DlkSystem
{
    public interface IFunctionHandler
    {
        void ExecuteFunction(String Screen, String ControlName, String Keyword, String[] Parameters);
    }
}
