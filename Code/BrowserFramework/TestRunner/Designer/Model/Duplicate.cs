using CommonLib.DlkHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunner.Designer.Model
{
    public class Duplicate
    {
        public Match TestToFind { get; set; }
        public Match TestDuplicate { get; set; }
        public List<Match> Duplicates { get; set; }
        public String GoToText
        {
            get
            {
                return "Link";
            }
        }

    }

}
