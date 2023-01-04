 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;
using System.Xml.Linq;
using System.IO;

namespace METestHarness.Tests
{
    public class CheckInTest : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
                /* Read File */
                String fileName = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "rslog.xml");
                string targetUrl = GetUrlFromFile(fileName);

                /* Go to URL */
                Driver.Instance.Url = targetUrl;
                
                /* Set value to false before attempt */
                UpdateFileValue(fileName, "chksuccess", "false");

                /* Click Checkin */
                try
                {
                    Driver.SessionLogger.WriteLine("Clicking check-in...", Logger.MessageType.INF);
                    IWebElement checkin = Driver.Instance.FindElements(By.Id("chkCheckIn")).FirstOrDefault();
                    if (checkin is null)
                    {
                        throw new Exception("Cannot find check-in checkbox.");
                    }
                    checkin.Click();
                    Driver.SessionLogger.WriteLine("Successfully clicked check-in...", Logger.MessageType.INF);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error clicking check-n checkbox. [ln 40]" + ex.Message);
                }

                /* Click Save */
                try
                {
                    Driver.SessionLogger.WriteLine("Clicking Save...", Logger.MessageType.INF);
                    IWebElement save = Driver.Instance.FindElements(By.XPath("//*[contains(@onclick, 'saveInstance')]")).FirstOrDefault();
                    if (save is null)
                    {
                        throw new Exception("Cannot find save button.");
                    }
                    save.Click();
                    Driver.SessionLogger.WriteLine("Successfully clicked save...", Logger.MessageType.INF);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error clicking save  button. [ln 40]" + ex.Message);
                }

                /* Set value to true if success */
                UpdateFileValue(fileName, "chksuccess", "true");
            }
			catch (Exception ex)
			{
				ret = false;
				ErrorMessage = ex.Message;
				throw new Exception(ex.Message);
			}
			return ret;
        }

        private string GetUrlFromFile(String FileName)
        {
            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXml(FileName);
                string ret = string.Empty;
                if (File.Exists(FileName))
                {
                    var configFile = ds.Tables[1];
                    var item = configFile.Rows[0][0].ToString();
                    if (item != null)
                    {
                        ret = item;
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void UpdateFileValue(String FileName, String Node, String NodeValue)
        {
            try
            {
                XDocument xDoc = XDocument.Load(FileName);
                xDoc.Root.Element("extra").SetElementValue(Node,NodeValue);
                xDoc.Save(FileName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
	
}

