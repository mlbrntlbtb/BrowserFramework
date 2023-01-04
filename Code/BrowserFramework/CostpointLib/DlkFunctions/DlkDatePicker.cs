using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace CostpointLib.DlkFunctions
{
    [Component("DatePicker")]
    public static class DlkDatePicker
    {

        [Keyword("ClickDay", new String[] { "1|text|Day|31" })]
        public static void ClickDay(string Day)
        {
            try
            {
                int day;

                if (!int.TryParse(Day, out day))
                {
                    throw new Exception("Invalid day string provided. Please input numeric value.");
                }

                string id = "calDate" + Day;

                IWebElement target = DlkEnvironment.AutoDriver.FindElement(By.XPath("//*[@class='popupCalDate' and text()='" + Day + "']"));
                target.Click();
            }
            catch (Exception e)
            {
                throw new Exception("ClickDay() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifySelectedDay", new String[] { "1|text|Day|31" })]
        public static void VerifySelectedDay(string Day)
        {
            try
            {
                IWebElement tile1;
                IWebElement tile2;
                IWebElement tile3;
                IWebElement target = null;
                string tile1BC = string.Empty;
                string tile2BC = string.Empty;
                string tile3BC = string.Empty;

                int cntTile = DlkEnvironment.AutoDriver.FindElements(By.ClassName("popupCalDate")).Count;
                int idx = 0;

                while (idx < cntTile)
                {
                    tile1 = DlkEnvironment.AutoDriver.FindElements(By.ClassName("popupCalDate"))[idx];
                    tile1BC = tile1.GetCssValue("color");
                    if (idx + 1 == cntTile)
                    {
                        target = tile1;
                        break;
                    }
                    tile2 = DlkEnvironment.AutoDriver.FindElements(By.ClassName("popupCalDate"))[idx + 1];
                    tile2BC = tile2.GetCssValue("color");
                    if (idx + 2 == cntTile)
                    {
                        target = tile2;
                        break;
                    }
                    tile3 = DlkEnvironment.AutoDriver.FindElements(By.ClassName("popupCalDate"))[idx + 2];
                    tile3BC = tile3.GetCssValue("color");

                    if (tile1BC == tile2BC && tile1BC == tile3BC)
                    {
                        // do nothing;
                    }
                    else
                    {
                        if (tile1BC != tile2BC)
                        {
                            if (tile1BC == tile3BC)
                            {
                                target = tile2;
                            }
                            else
                            {
                                target = tile1;
                            }
                        }
                        else if (tile1BC != tile3BC)
                        {
                            if (tile1BC == tile2BC)
                            {
                                target = tile3;
                            }
                            else
                            {
                                target = tile1;
                            }
                        }
                        break;
                    }

                    // iterate
                    idx += 2;
                }
                if (target == null)
                {
                    throw new Exception("Target Day not found");
                }
                DlkAssert.AssertEqual("VerifySelectedDay()", Day, target.Text);
                DlkLogger.LogInfo("VerifySelectedDay passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifySelectedDay() : " + e.Message, e);
            }
        }   
    }
}
