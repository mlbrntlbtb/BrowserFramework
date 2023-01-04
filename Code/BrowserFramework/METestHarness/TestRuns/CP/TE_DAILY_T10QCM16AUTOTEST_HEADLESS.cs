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
    public class TE_DAILY_T10QCM16AUTOTEST_HEADLESS : TestRun
    {
        public TE_DAILY_T10QCM16AUTOTEST_HEADLESS(string emailAddresses) : base(emailAddresses) { }

        public override void ExecuteTests()
        {
            new TE_BA().Run(this, Driver.Browser.CHROME_HEADLESS, "TE_DAILY_T10QCM16AUTOTEST");
        }
    }
}
