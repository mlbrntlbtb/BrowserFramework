using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkSystem;
using Controls = HRSmartLib.LatestVersion.DlkControls;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using OpenQA.Selenium;
using System.Threading;
using HRSmartLib.LatestVersion.DlkControls;

namespace HRSmartLib.LatestVersion.DlkFunctions
{
    [Component("Administration")]
    public static class DlkAdministration
    {
        #region Declarations
        #endregion

        #region Properties
        #endregion

        #region Methods


        [Keyword("GoTo")]
        public static void GoTo(string PanelTitle, string ContentTitle)
        {
            IWebElement targetElement = null;
            try
            {
                //Get Panel Element
                if (PanelTitle == string.Empty ||
                    ContentTitle == string.Empty)
                {
                    DlkLogger.LogInfo("Skipped.");
                }
                else
                {
                    IList<IWebElement> panelElementList = DlkCommon.DlkCommonFunction.GetElementWithText(PanelTitle, elementTag: "h3");

                    if (panelElementList.Count > 0)
                    {
                        IWebElement parentElement = panelElementList[0].FindElement(By.XPath(".//ancestor::li[contains(@class,'panel')]"));
                        string[] contentParam = ContentTitle.Split('~');
                        List<string> contentParamList = contentParam.ToList();
                        int counter = 0;
                        foreach (string content in contentParamList)
                        {
                            counter++;
                            IList<IWebElement> contentElementList = DlkCommon.DlkCommonFunction.GetElementWithText(content, parentElement, elementTag: "h5");
                            if (contentElementList.Count > 0 &&
                                counter != contentParamList.Count)
                            {
                                parentElement = contentElementList[0].FindElement(By.XPath("../parent::ul"));
                            }
                            else
                            {
                                IList<IWebElement> targetLinkElementList = DlkCommon.DlkCommonFunction.GetElementWithText(content, parentElement, elementTag: "a");
                                if (targetLinkElementList.Count > 0)
                                {
                                    targetElement = targetLinkElementList[0];
                                    targetLinkElementList[0].Click();
                                }
                                else
                                {
                                    throw new Exception("Link : " + content + " not found.");
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Panel Title : " + PanelTitle + " not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("element click intercepted") && targetElement != null)
                {
                    DlkButton clickableElement = new DlkButton("Button", targetElement);
                    clickableElement.Click();
                }
                else
                {
                    throw ex;
                }
            }
        }

        [Keyword("VerifyContentExists")]
        public static void VerifyContentExists(string PanelTitle, string ContentTitle, string TrueOrFalse)
        {
            try
            {
                bool actualResult = false;
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                //Get Panel Element
                if (PanelTitle == string.Empty ||
                    ContentTitle == string.Empty)
                {
                    DlkLogger.LogInfo("Parameter should not be blank.");
                }
                else
                {
                    IList<IWebElement> panelElementList = DlkCommon.DlkCommonFunction.GetElementWithText(PanelTitle, elementTag: "h3");

                    if (panelElementList.Count > 0)
                    {
                        IWebElement parentElement = panelElementList[0].FindElement(By.XPath(".//ancestor::li[contains(@class,'panel')]"));
                        string[] contentParam = ContentTitle.Split('~');
                        List<string> contentParamList = contentParam.ToList();
                        foreach (string content in contentParamList)
                        {
                            IList<IWebElement> contentElementList = DlkCommon.DlkCommonFunction.GetElementWithText(content, parentElement, elementTag: "h5");
                            if (contentElementList.Count > 0)
                            {
                                parentElement = contentElementList[0].FindElement(By.XPath("../parent::ul"));
                            }
                            else
                            {
                                IList<IWebElement> targetLinkElementList = DlkCommon.DlkCommonFunction.GetElementWithText(content, parentElement, elementTag: "a");
                                if (targetLinkElementList.Count > 0)
                                {
                                    actualResult = true;
                                    break;
                                }
                                else
                                {
                                    actualResult = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        actualResult = false;
                    }

                    DlkAssert.AssertEqual("Content Title : " + ContentTitle, expectedResult, actualResult);
                    DlkLogger.LogInfo("VerifyContentExists () successfully executed.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
