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
    public class TE_DAILY_TE10QCO12AUTOTEST : TestRun
    {
        public TE_DAILY_TE10QCO12AUTOTEST(string emailAddresses) : base(emailAddresses) { }

        public override void ExecuteTests()
        {
            new TE_BA().Run(this, Driver.Browser.CHROME, "TE_DAILY_TE10QCO12AUTOTEST");
        }
    }
}
