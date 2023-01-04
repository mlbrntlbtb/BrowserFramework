using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METestHarness.Sys;
using METestHarness.Tests;
using METestHarness.Tests.Samples;

namespace METestHarness.TestRuns
{
    public class TE_REG_ORA_TE10AUTOTEST : TestRun
    {
        public TE_REG_ORA_TE10AUTOTEST(string emailAddresses) : base(emailAddresses) { }

        public override void ExecuteTests()
        {
            new TEBuildAcceptanceTest_Reg().Run(this, Driver.Browser.CHROME, "TE_REG_ORA_TE10AUTOTEST");
        }
    }
}
