using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using METestHarness.Common;
using METestHarness.Sys;
using OpenQA.Selenium;

namespace METestHarness.Tests.ResourceScheduler
{
    public class GetReservationList : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
            bool ret = true;
            ErrorMessage = string.Empty;
            try
            {
                string targetUrl = "http://resourcescheduler.deltek.com/resourcescheduler/SearchText.asp";

                /* Go to search URL */
                Driver.Instance.Url = targetUrl;

                /* Fill up form */
                //Click Clear
                IWebElement clearBtn = Driver.Instance.FindElements(By.Id("Reset1")).FirstOrDefault();
                if (clearBtn is null)
                {
                    throw new Exception("Cannot find clear button.");
                }
                clearBtn.Click();
                Thread.Sleep(1000);

                //Set User Contact using the currently logged in account
                IWebElement hdruserName = Driver.Instance.FindElements(By.Id("HeaderNavUserName")).FirstOrDefault();
                if (hdruserName is null)
                {
                    throw new Exception("Cannot find username.");
                }
                string userName = hdruserName.Text;

                IWebElement usercontactTxt = Driver.Instance.FindElements(By.Id("txtUserContact")).FirstOrDefault();
                if (usercontactTxt is null)
                {
                    throw new Exception("Cannot find user contact field.");
                }
                usercontactTxt.SendKeys(userName);

                //Set All Groups
                IWebElement locationList = Driver.Instance.FindElements(By.Id("grploctwo_taglist")).FirstOrDefault();
                if (locationList is null)
                {
                    throw new Exception("Cannot find location list.");
                }
                locationList.Click();
                Thread.Sleep(1000);

                IWebElement allGroups = Driver.Instance.FindElements(By.XPath("//ul[@id='grploctwo_listbox']//li[@data-offset-index='0']")).FirstOrDefault();
                if (allGroups is null)
                {
                    throw new Exception("Cannot find all groups.");
                }
                allGroups.Click();
                Thread.Sleep(1000);

                //Set Specify
                IWebElement specifyBtn = Driver.Instance.FindElements(By.XPath("//input[@id='radDate'][@value='2']")).FirstOrDefault();
                if (specifyBtn is null)
                {
                    throw new Exception("Cannot find user contact field.");
                }
                specifyBtn.Click();

                //Click Show 
                IWebElement showBtn = Driver.Instance.FindElements(By.Id("Submit1")).FirstOrDefault();
                if (showBtn is null)
                {
                    throw new Exception("Cannot find show button.");
                }
                showBtn.Click();
                Thread.Sleep(3000);

                /* Get reservation List */
                /* Check if there are hits and open each reservation */

                List<IWebElement> reservationItems = Driver.Instance.FindElements(By.XPath("//table/tbody/tr")).ToList();
                List<XElement> lstElements = new List<XElement>();
                if (reservationItems.Count > 0)
                {
                    foreach( IWebElement elm in reservationItems)
                    {
                        /*Open each item and get url/checked-in state */
                        elm.FindElements(By.XPath(".//a")).FirstOrDefault().Click();
                        Thread.Sleep(3000);

                        Driver.Instance.SwitchTo().Window(Driver.Instance.WindowHandles[Driver.Instance.WindowHandles.Count -1]);
                        string url = Driver.Instance.Url;
                        IWebElement checkinCtrl = Driver.Instance.FindElements(By.Id("chkCheckIn")).FirstOrDefault();                        
                        string checkedin = checkinCtrl is null ? "false" : checkinCtrl.GetAttribute("checked") ;
                        string previousWindow = "";
                        foreach (String winhandle in Driver.Instance.WindowHandles)
                        {
                            if (winhandle == Driver.Instance.CurrentWindowHandle)
                            {
                                previousWindow = Driver.Instance.WindowHandles[(Driver.Instance.WindowHandles.IndexOf(winhandle)) - 1];
                            }
                        }
                        Driver.Instance.SwitchTo().Window(previousWindow);
                        Thread.Sleep(1000);

                        /* Save contents to element */
                        XElement mItem = new XElement("reservation",
                        new XElement("start", elm.FindElements(By.XPath(".//td[2]")).FirstOrDefault().Text),
                        new XElement("end", elm.FindElements(By.XPath(".//td[3]")).FirstOrDefault().Text),
                        new XElement("checkedin", checkedin),
                        new XElement("resource", elm.FindElements(By.XPath(".//td[8]")).FirstOrDefault().Text),
                        new XElement("url", url)
                        );

                        lstElements.Add(mItem);
                    }                    
                }
                else
                {
                   /* If no hits found... */
                }

                /* Write to file */
                String fileName = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(),"rslog.xml");
                WriteToFile(lstElements, fileName);
                
            }
            catch (Exception ex)
			{
                ret = false;
                ErrorMessage = ex.Message;
                throw new Exception(ex.Message);
            }
            return ret;
        }

        private void WriteToFile(List<XElement> Elms, String FileName)
        {
            try
            {
                XElement mExtra = new XElement("extra", 
                    new XElement("myurl"),
                    new XElement("chksuccess","false"));
                XElement ElmRoot = new XElement("reservations", Elms, mExtra);
                XDocument xDoc = new XDocument(ElmRoot);
                if (File.Exists(FileName))
                {
                    File.Delete(FileName);
                }
                xDoc.Save(FileName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
