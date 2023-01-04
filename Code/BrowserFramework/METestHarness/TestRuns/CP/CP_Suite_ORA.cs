using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METestHarness.Sys;
using METestHarness.Tests;

namespace METestHarness.TestRuns.CP
{
    public class CP_Suite_ORA : TestRun
    {
        public CP_Suite_ORA(string emailAddresses) : base(emailAddresses) { }

        public override void ExecuteTests()
        {
            new VerifyField().Run(this, Driver.Browser.CHROME, "CP_STAGING_C71STAGEO", false);
            new VerifyFieldFail().Run(this, Driver.Browser.CHROME, "CP_STAGING_C71STAGEO", false);
            new CPBuildAcceptanceTest_Full().Run(this, Driver.Browser.CHROME, "CP_STAGING_C71STAGEO");

        }
    }
}
