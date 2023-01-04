using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkControls;

namespace Recorder.Model
{
    public class Control
    {
        public Enumerations.ControlType Type { get; set; }
        public Descriptor Descriptor { get; set; }
        public string Value { get; set; }
        public int AncestorFormIndex { get; set; }
        public DlkBaseControl Base { get; set; }
        public string Id { get; set; }
        public string Class { get; set; }
        public string PopRefId { get; set; }
    }
}
