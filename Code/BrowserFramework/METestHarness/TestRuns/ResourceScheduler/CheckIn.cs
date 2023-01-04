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
    public class CheckIn : TestRun
    {
        public CheckIn(string emailAddresses) : base(emailAddresses) { }

        public override void ExecuteTests()
        {
            new CheckInTest().Run(this, Driver.Browser.CHROME_HEADLESS);
        }
    }
}
