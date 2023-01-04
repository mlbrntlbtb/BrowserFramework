﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METestHarness.Sys;
using METestHarness.Tests;
using METestHarness.Tests.Samples;

namespace METestHarness.TestRuns
{
    public class CP_STAGING_C71STAGEM : TestRun
    {
        public CP_STAGING_C71STAGEM(string emailAddresses) : base(emailAddresses) { }

        public override void ExecuteTests()
        {
            new CPBuildAcceptanceTest().Run(this, Driver.Browser.CHROME, "CP_STAGING_C71STAGEM");
        }
    }
}
