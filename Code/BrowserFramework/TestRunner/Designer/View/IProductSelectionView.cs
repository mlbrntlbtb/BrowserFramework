using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRunner.Common;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;

namespace TestRunner.Designer.View
{
    public interface IProductSelectionView
    {
        List<DlkTargetApplication>  AvailableProducts { get; set; }
        DlkTargetApplication SelectedProduct { get; set; }
    }

}