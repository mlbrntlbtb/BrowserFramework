using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TestRunner.Common;

namespace TestRunner.AdvancedScheduler.Model
{
    public class TreeViewSelectedItem
    {
        public TreeViewItem tvi { get; set; }
        public BFFolder dragSourceFolder { get; set; }
        public KwDirItem dirItem { get; set; }
    }
}
