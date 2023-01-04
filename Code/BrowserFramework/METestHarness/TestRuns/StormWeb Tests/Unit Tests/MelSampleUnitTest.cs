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
    public class MelSampleUnitTest : TestRun
    {
        public MelSampleUnitTest(string emailAddresses) : base(emailAddresses) { }

        public override void ExecuteTests()
        {
            //HP_EMP_CreateSearchAndDeleteEmployee EMP1 = new HP_EMP_CreateSearchAndDeleteEmployee();
            //EMP1.Run(this, Driver.Browser.CHROME);
            //HP_EMP_EditRecordUsingQuickEdit EMP2 = new HP_EMP_EditRecordUsingQuickEdit();
            //EMP2.Run(this, Driver.Browser.CHROME);
            HP_SG_Time_OptionsTab_3 EMP3 = new HP_SG_Time_OptionsTab_3();
            EMP3.Run(this, Driver.Browser.CHROME);
        }
    }
}
