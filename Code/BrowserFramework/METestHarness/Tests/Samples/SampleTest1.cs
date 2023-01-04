using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;

namespace METestHarness.Tests
{
    public class SampleTest1 : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
            bool ret = true;
            ErrorMessage = string.Empty;
            if (!StormCommon.Login("SAMPLE", out ErrorMessage))
            {
                ret = false;
            }
            return ret;
        }
    }
}
