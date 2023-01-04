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
    public class CheckOut : TestRun
    {
        public CheckOut(string emailAddresses) : base(emailAddresses) { }

        public override void ExecuteTests()
        {
            new CheckOutTest().Run(this, Driver.Browser.CHROME_HEADLESS);
        }
    }
}
