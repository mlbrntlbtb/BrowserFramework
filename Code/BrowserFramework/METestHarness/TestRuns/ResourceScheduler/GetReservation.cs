using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METestHarness.Sys;
using METestHarness.Tests;
using METestHarness.Tests.ResourceScheduler;

namespace METestHarness.TestRuns.ResourceScheduler
{
    public class GetReservation : TestRun
    {
        public GetReservation(string emailAddresses) : base(emailAddresses) { }

        public override void ExecuteTests()
        {
            new GetReservationList().Run(this, Driver.Browser.CHROME_HEADLESS);
        }
    }
}
