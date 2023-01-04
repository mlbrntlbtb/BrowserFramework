using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HP_SG_Time_RatiosTab_3 : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
            bool ret = true;
            ErrorMessage = string.Empty;
            string currentDate = DateTime.Now.ToString("MM/dd/yyyy");

            try
            {
                StormCommon.Login("Nikka2.0", out ErrorMessage);
                Control banner = new Control("SideBar", "XPATH", "//*[contains(@class,'banner-left')]");
                StormCommon.WaitControlDisplayed(banner);
                
                //1. navigate to SG_Time
                Driver.SessionLogger.WriteLine("Navigate to Activity");
                Control sideBar_TimeConfig = new Control("SideBar", "XPATH", "//*[contains(@class,'nav-item')][@data-app-id='TimeConfig']");
                Thread.Sleep(1000);

                sideBar_TimeConfig.Click();
                Thread.Sleep(1000);

                //2. navigate to SG_Time_Ratios
                Driver.SessionLogger.WriteLine("Navigate to Activity");
                Control sideBar_Time_Ratios = new Control("SideBar", "XPATH", "//*[contains(@class,'nav-item')][@data-app-id='TimeSettingsRatios']");
                Thread.Sleep(1000);

                sideBar_Time_Ratios.Click();
                Thread.Sleep(1000);

                //3. Verify if Summary Title is available
                var summaryTitleExists = new Control("Title", "XPATH", "//*[@data-id='time_ratios_divider_control_level1']//*[contains(@class,'Text')]").Exists();
                if (!summaryTitleExists)
                    throw new Exception("Summary Title does not exist");

                //4. Get Checkbox value - Direct Total
                var CB_DirectTotal = new Control("CB_DirectTotal", "XPATH", "//*[@name='ShowRatio1']");
                var CB_DirectTotal_Value = CB_DirectTotal.GetAttributeValue("checked");

                //5. Set Checkbox value - Direct Total to True
                if(CB_DirectTotal_Value != "true")
                {
                    CB_DirectTotal.Click();
                }

                //6. Set Checkbox value - Direct Total Absence to True
                var CB_DirectTotalAbsence = new Control("CB_DirectTotalAbsence", "XPATH", "//*[@name='ShowRatio2']");
                if (CB_DirectTotalAbsence.GetAttributeValue("checked") != "true")
                {
                    CB_DirectTotalAbsence.Click();
                }

                //7. Set Checkbox value - Target to false
                var CB_Target = new Control("CB_Target", "XPATH", "//*[@name='ShowRatio5']");
                if (CB_Target.GetAttributeValue("checked") != "true")
                {
                    CB_Target.Click();
                }

                //8. Set Checkbox value - Direct Standard to False
                var CB_DirectStandard = new Control("CB_DirectStandard", "XPATH", "//*[@name='ShowRatio4']");
                if (CB_DirectStandard.GetAttributeValue("checked") != "true")
                {
                    CB_DirectStandard.Click();
                }

                //9. Set Checkbox value - Direct Total Absence to True
                var CB_DirectStandardAbsence = new Control("CB_DirectStandardAbsence", "XPATH", "//*[@name='ShowRatio3']");
                if (CB_DirectStandardAbsence.GetAttributeValue("checked") != "true")
                {
                    CB_DirectStandardAbsence.Click();
                }

                //10. Set Checkbox value - Current to True
                var CB_Current = new Control("CB_Current", "XPATH", "//*[@name='ShowRatioCurrent']");
                if (CB_Current.GetAttributeValue("checked") != "true")
                {
                    CB_Current.Click();
                }

                //11. Set Checkbox value - Month-To-Date to False
                var CB_MTD = new Control("CB_MTD", "XPATH", "//*[@name='ShowRatioMTD']");
                if (CB_MTD.GetAttributeValue("checked") == "true")
                {
                    CB_MTD.Click();
                }

                //12. Set Checkbox value - Quarter-To-Date to False
                var CB_QTD = new Control("CB_QTD", "XPATH", "//*[@name='ShowRatioQTD']");
                if (CB_QTD.GetAttributeValue("checked") == "true")
                {
                    CB_QTD.Click();
                }

                //13. Set Checkbox value - Year-To-Date to False
                var CB_YTD = new Control("CB_YTD", "XPATH", "//*[@name='ShowRatioYTD']");
                if (CB_YTD.GetAttributeValue("checked") == "true")
                {
                    CB_YTD.Click();
                }

                //14. Click Save
                new Control("Save", "XPATH", "//*[@data-id='save']").Click();
            }
            catch (Exception ex)
            {
                ret = false;
                ErrorMessage = ex.Message;
                throw new Exception(ex.Message);
            }
            return ret;
        }
    }
}
