using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunner.AdvancedScheduler.Model
{
    public class ExternalScript
    {
        public string Path { get; set; }
        public int Order { get; set; }
        public Enumerations.ExetrnalScriptType Type { get; set; }
        public string Arguments { get; set; }
        public string StartIn { get; set; }
        public bool WaitToFinish { get; set; }

        public string Name
        {
            get { return System.IO.Path.GetFileName(Path); }
        }

        public ExternalScript()
        {

        }

        /// <summary>
        /// Used to create a copy of the ExternalScript property by property.
        /// </summary>
        /// <param name="script"></param>
        protected ExternalScript(ExternalScript script)
        {
            //If ever there would be an additional property, please include here.
            Path = script.Path;
            Order = script.Order;
            Type = script.Type;
            Arguments = script.Arguments;
            StartIn = script.StartIn;
            WaitToFinish = script.WaitToFinish;
        }

        /// <summary>
        /// Create a copy of the script
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new ExternalScript(this);
        }
    }
}
