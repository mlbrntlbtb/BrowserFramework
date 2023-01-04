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
    public class SampleUnitTest : TestRun
    {
        public SampleUnitTest(string emailAddresses) : base(emailAddresses) { }

        public override void ExecuteTests()
        {
            //HP_ACT_CreateSearchAndDeleteActivities HP_ACT_CreateSearchAndDeleteActivities = new HP_ACT_CreateSearchAndDeleteActivities();
            //HP_ACT_CreateSearchAndDeleteActivities.Run(this, Driver.Browser.CHROME);

            //HP_ACT_SearchCopyAndDeleteActivities HP_ACT_SearchCopyAndDeleteActivities = new HP_ACT_SearchCopyAndDeleteActivities();
            //HP_ACT_SearchCopyAndDeleteActivities.Run(this, Driver.Browser.CHROME);

            //HP_ACT_SearchAndEditExistingActivitiesPerTab HP_ACT_SearchAndEditExistingActivitiesPerTab = new HP_ACT_SearchAndEditExistingActivitiesPerTab();
            //HP_ACT_SearchAndEditExistingActivitiesPerTab.Run(this, Driver.Browser.CHROME);

            HP_SG_Time_OptionsTab_3 HP_SG_Time_OptionsTab_3 = new HP_SG_Time_OptionsTab_3();
            HP_SG_Time_OptionsTab_3.Run(this, Driver.Browser.CHROME);

            HP_SG_Time_OptionsTab_4 HP_SG_Time_OptionsTab_4 = new HP_SG_Time_OptionsTab_4();
            HP_SG_Time_OptionsTab_4.Run(this, Driver.Browser.CHROME);

            HP_SG_Time_OptionsTab_5 HP_SG_Time_OptionsTab_5 = new HP_SG_Time_OptionsTab_5();
            HP_SG_Time_OptionsTab_5.Run(this, Driver.Browser.CHROME);

            HP_SG_Time_RatiosTab_1 HP_SG_Time_RatiosTab_1 = new HP_SG_Time_RatiosTab_1();
            HP_SG_Time_RatiosTab_1.Run(this, Driver.Browser.CHROME);

            HP_SG_Time_RatiosTab_3 HP_SG_Time_RatiosTab_3 = new HP_SG_Time_RatiosTab_3();
            HP_SG_Time_RatiosTab_3.Run(this, Driver.Browser.CHROME);
        }
    }
}
