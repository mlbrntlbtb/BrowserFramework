using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFTLib.DlkControls.Contract
{
    public interface IGridWithFooter : IGrid
    {
        void PagerFirst();
        void PagerPrevious();
        void SetPageNavigatorInput(int pageNumber);
        void PagerNext();
        void PagerLast();
        void SetRowsPerPage(int rowsNumber);
    }
}
