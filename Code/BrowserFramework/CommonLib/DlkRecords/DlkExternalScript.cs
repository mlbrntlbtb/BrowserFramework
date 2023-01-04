using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DlkRecords
{
    /// <summary>
    /// This record holds the details for the actions during test capture
    /// </summary>
    public class DlkExternalScript
    {
        public string Path { get; set; }
        public int Order { get; set; }
        public DlkExternalScriptType Type { get; set; }
        public string Arguments { get; set; }
        public string StartIn { get; set; }
        public bool WaitToFinish { get; set; }

        public string Name {
            get { return System.IO.Path.GetFileName(Path); }
        }

        public DlkExternalScript(string path, int order, string arguments, string startin, bool wait , DlkExternalScriptType type)
        {
            Path = path;
            Order = order;
            Arguments = arguments;
            StartIn = startin;
            WaitToFinish = wait;
            Type = type;
        }

        public DlkExternalScript(int order, DlkExternalScriptType type)
        {
            Path = "";
            Order = order;
            Arguments = "";
            StartIn = "";
            WaitToFinish = false;
            Type = type ;
        }

        public DlkExternalScript Clone(){
            return (DlkExternalScript) this.MemberwiseClone();
        }
    

}

    public enum DlkExternalScriptType
    {
        PreExecutionScript,
        PostExecutionScript
    }
}
