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
    public class CP_DAILY_C71MQCO12AEHQ_HEADLESS : TestRun
    {
        public CP_DAILY_C71MQCO12AEHQ_HEADLESS(string emailAddresses) : base(emailAddresses) { }

        public override void ExecuteTests()
        {
            new CPBuildAcceptanceTest().Run(this, Driver.Browser.CHROME_HEADLESS, "CP_DAILY_C71MQCO12AEHQ");
        }
    }
}
