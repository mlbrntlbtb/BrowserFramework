using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recorder.Model
{
    public class Step
    {
        public string Execute { get; set; }
        public string Screen { get; set; }
        public string Control { get; set; }
        public string Keyword { get; set; }
        public string Parameters { get; set; }
        public string Delay { get; set; }
        public int Block { get; set; }
    }
}
