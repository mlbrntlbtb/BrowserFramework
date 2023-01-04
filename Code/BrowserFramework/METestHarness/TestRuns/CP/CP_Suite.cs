using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METestHarness.Sys;
using METestHarness.Tests;

namespace METestHarness.TestRuns.CP
{
    public class CP_Suite : TestRun
    {
        public CP_Suite(string emailAddresses) : base(emailAddresses) { }

        public override void ExecuteTests()
        {
            //MSS
            new VerifyField().Run(this, Driver.Browser.CHROME, "CP_STAGING_C71STAGEM");
            new VerifyFieldFail().Run(this, Driver.Browser.CHROME, "CP_STAGING_C71STAGEM");
            new CheckUser().Run(this, Driver.Browser.CHROME, "CP_STAGING_C71STAGEM");
            new DeleteUser().Run(this, Driver.Browser.CHROME, "CP_STAGING_C71STAGEM");
            new CPBuildAcceptanceTest().Run(this, Driver.Browser.CHROME, "CP_STAGING_C71STAGEM");


            //ORA
            new VerifyField().Run(this, Driver.Browser.CHROME, "CP_STAGING_C71STAGEO");
            new VerifyFieldFail().Run(this, Driver.Browser.CHROME, "CP_STAGING_C71STAGEO");
            new CheckUser().Run(this, Driver.Browser.CHROME, "CP_STAGING_C71STAGEO");
            new DeleteUser().Run(this, Driver.Browser.CHROME, "CP_STAGING_C71STAGEO");
            new CPBuildAcceptanceTest().Run(this, Driver.Browser.CHROME, "CP_STAGING_C71STAGEO");

        }
    }
}
