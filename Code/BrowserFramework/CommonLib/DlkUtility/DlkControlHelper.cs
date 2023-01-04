using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkControls;

namespace CommonLib.DlkUtility
{
    public abstract class DlkControlHelper
    {
        public abstract string DetectControlType(DlkBaseControl control);
        public abstract void AutoCorrectSearchMethod(DlkBaseControl control, string controlType, ref string SearchType, ref string SearchValue);
        public abstract string[] GetControlTypes();
    }
}
