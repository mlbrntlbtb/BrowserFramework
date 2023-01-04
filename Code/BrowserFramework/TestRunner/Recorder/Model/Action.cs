using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recorder.Model
{
    public class Action
    {
        public Enumerations.ActionType Type { get; set; }
        public Control Target { get; set; }
        public Screen Screen { get; set; }
        public int Block { get; set; }
        public bool AlertResponse { get; set; }
    }
}
