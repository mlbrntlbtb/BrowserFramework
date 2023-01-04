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
    public class Sample1 : TestRun
    {
        public Sample1(string emailAddresses) : base(emailAddresses) { }

        public override void ExecuteTests()
        {
            new CPBuildAcceptanceTest_LH().Run(this, Driver.Browser.CHROME_HEADLESS, "CP_DAILY_C71MQCO12AEHQ");
            new TEBuildAcceptanceTest_LH().Run(this, Driver.Browser.CHROME_HEADLESS, "TE_DAILY_TE10QCO12AUTOTEST");

        }
    }
}
