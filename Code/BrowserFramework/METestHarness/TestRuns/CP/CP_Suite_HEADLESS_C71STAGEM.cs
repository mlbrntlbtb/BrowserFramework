using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METestHarness.Sys;
using METestHarness.Tests;

namespace METestHarness.TestRuns.CP
{
    public class CP_Suite_HEADLESS_C71STAGEM : TestRun
    {
        public CP_Suite_HEADLESS_C71STAGEM(string emailAddresses) : base(emailAddresses) { }

        public override void ExecuteTests()
        {
            new VerifyField().Run(this, Driver.Browser.CHROME_HEADLESS, "CP_STAGING_C71STAGEM",false);
            new VerifyFieldFail().Run(this, Driver.Browser.CHROME_HEADLESS, "CP_STAGING_C71STAGEM",false);
            new CPBuildAcceptanceTest_Full().Run(this, Driver.Browser.CHROME_HEADLESS, "CP_STAGING_C71STAGEM");            
        }
    }
}
