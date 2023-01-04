using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFTLib.DlkControls.Contract
{
    public interface IComboBox 
    {
        void Select(string item);
        void VerifyList(string items);
        void SelectByIndex(int index);
    }
}
