using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;


using OpenQA.Selenium.Interactions;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("TimelineChart")]
    public class DlkTimelineChart : DlkBaseControl
    {
        DlkBaseControl zoomInButton;
        DlkBaseControl zoomOutButton;
        DlkBaseControl moveLeftButton;
        DlkBaseControl moveRightButton;

        List<DlkBaseControl> mTimeLineEventContents;
        DlkBaseControl mTimeLineframe;
        List<DlkBaseControl> mTimeLineMileStones = new List<DlkBaseControl>();
        List<DlkBaseControl> mTimeLineDocuments = new List<DlkBaseControl>();

        public DlkTimelineChart(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }        

        private void Initialize()
        {
            //find container from top div timeline tag
            FindElement();

            try
            {
                //extract the frame only
                mTimeLineframe = new DlkBaseControl("TimelineFrame", this.mElement.FindElement(By.CssSelector("div.timeline-frame")));

                //find buttons timeline-navigation-zoom-in
                zoomInButton = new DlkBaseControl("ZoomIn", mTimeLineframe.mElement.FindElement(By.CssSelector("div.timeline-navigation-zoom-in")));
                zoomOutButton = new DlkBaseControl("ZoomOut", mTimeLineframe.mElement.FindElement(By.CssSelector("div.timeline-navigation-zoom-out")));
                moveLeftButton = new DlkBaseControl("MoveLeft", mTimeLineframe.mElement.FindElement(By.CssSelector("div.timeline-navigation-move-left")));
                moveRightButton = new DlkBaseControl("MoveRight", mTimeLineframe.mElement.FindElement(By.CssSelector("div.timeline-navigation-move-right")));

                IList<IWebElement> eventContents = mTimeLineframe.mElement.FindElements(By.CssSelector("div.timeline-event-content"));
                mTimeLineEventContents = eventContents.Select(a => new DlkBaseControl("", a)).ToList();
                IList<IWebElement> timelineMilestones = mTimeLineframe.mElement.FindElements(By.CssSelector("div.timeline-event-content>div.timelineMilestone"));
                IList<IWebElement> timelineDocs = mTimeLineframe.mElement.FindElements(By.CssSelector("div.timeline-event-content>div.timelineDocument"));
                mTimeLineMileStones = timelineMilestones.Select(milestone => new DlkBaseControl("", milestone)).ToList();
                mTimeLineDocuments = timelineDocs.Select(doc => new DlkBaseControl("", doc)).ToList();
            }
            catch (Exception e)
            {
                throw e;

            }
        }

        //[Keyword("ScrollToView", new string[]{  "1|text|Milestone Name|Sample Text"})]
        public void ScrollToView(string milestoneName)
        {
            try
            {
                Initialize();
                DlkBaseControl milestoneSelected = mTimeLineMileStones.Find(a=>a.GetValue()==milestoneName);                
                IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                jse.ExecuteScript("arguments[0].scrollIntoView(true);", mTimeLineframe.mElement);
                jse.ExecuteScript("arguments[0].scrollIntoView(false);", mTimeLineframe.mElement);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        //date is expecting yyyy-(mm-1)-dd
        [Keyword("SelectDocumentsByDate", new string[] {"1|text|Date|1/1/013"})]
        public void SelectDocumentsByDate(string rawdate)
        {
            string date = "";
            try
            {
                
                DateTime dt = Convert.ToDateTime(rawdate);
                int month = dt.Month;
                int day = dt.Day;
                int year = dt.Year;
                date = string.Format("{0}-{1}-{2}",year,month-1,day );
            }
            catch (Exception e)
            {
                throw e;
            }

            try
            {
                Initialize();
                string searchPattern = string.Format("//a[contains(@onmousedown,'{0}')]", date);
                DlkBaseControl docSelected = mTimeLineDocuments.Find(a => a.mElement.FindElement(By.XPath(searchPattern)) != null );
                if (docSelected is DlkBaseControl)
                {
                    try
                    {
                        IWebElement actualtag = docSelected.mElement.FindElement(By.XPath(searchPattern));

                        int elapsed = 0;
                        TimeSpan timeSpan = new TimeSpan(0, 1, 0); //1 min timeout
                        bool retry = true;
                        while (retry)
                        {
                            Thread.Sleep(1000);
                            elapsed += 1000;
                            if(elapsed > timeSpan.TotalMilliseconds)
                            {
                                throw new Exception(string.Format("Timeout occured. Unable to select doc with date {0}", rawdate));
                            }
                            try
                            {
                                this.ClickZoomOut("1");
                                
                                IWebElement searchedElement = docSelected.mElement.FindElement(By.XPath(searchPattern));                                                                
                                if (searchedElement.Displayed)
                                {
                                    DlkBaseControl element = new DlkBaseControl("", searchedElement);
                                    element.VerifyExists(true);
                                    element.ClickByObjectCoordinates();                                   
                                    retry = false;
                                }                                
                            }
                            catch (Exception innerEx)
                            {
                                //swallow this error since we might want to zoom out one more time
                                if (!innerEx.Message.Contains("cannot be scrolled"))
                                {
                                    throw new Exception("Exception thrown when trying to look for documents");
                                }                                
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        throw new Exception(string.Format("Unable to select document with {0} date",e));
                    }
                }
                else
                    throw new Exception(string.Format("SelectDocumentsByDate() failed. {0} docuement does not exist.", rawdate));
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("SelectDocumentsByDate() failed. Unable to find documents with date {0}", rawdate), e);
            }
        }

        [Keyword("DragRight", new string[] { "1|text|Number of times|1" })]
        public void DragRight(string magnification)
        {
            try
            {
                Initialize();
                int mag = Convert.ToInt32(magnification);

                //IWebElement tArea = DlkEnvironment.AutoDriver.FindElement(By.XPath(@"//div[@class='panelHead'][contains(text(),'Procurement Milestones')]/following-sibling::div[1]"));
                IWebElement tArea = DlkEnvironment.AutoDriver.FindElement(By.XPath(@"//div[@class='timeline-frame']/div"));
                ILocatable tAreaLoc = (ILocatable)DlkEnvironment.AutoDriver.FindElement(By.XPath(@"//div[@class='timeline-frame']/div"));

                IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                jse.ExecuteScript("arguments[0].scrollIntoView(true);", tArea);

                for (int i = 0; i < mag; i++)
                {
                    var contextAction = new Actions(DlkEnvironment.AutoDriver)
                                        .MoveToElement(tArea, (tArea.Size.Width / 3), 0)
                                        .ClickAndHold(tArea)
                                        .DragAndDropToOffset(tArea, (-(tArea.Size.Width / 3)), 0)
                                        .Release(tArea)
                                        .Build(); // tArea, (-(tArea.Size.Width / 3)), 0).Release(tArea).Build();
                    contextAction.Perform();
                }
            }
            catch (Exception e)
            {
                throw new Exception("DragRight() failed " + e.Message);
            }
        }

        [Keyword("DragLeft", new string[] { "1|text|Number of times|1" })]
        public void DragLeft(string magnification)
        {
            try
            {
                Initialize();
                int mag = Convert.ToInt32(magnification);

                //IWebElement tArea = DlkEnvironment.AutoDriver.FindElement(By.XPath(@"//div[@class='panelHead'][contains(text(),'Procurement Milestones')]/following-sibling::div[1]"));
                IWebElement tArea = DlkEnvironment.AutoDriver.FindElement(By.XPath(@"//div[@class='timeline-frame']/div/div/div"));
                ILocatable tAreaLoc = (ILocatable)DlkEnvironment.AutoDriver.FindElement(By.XPath(@"//div[@class='timeline-frame']/div/div/div"));

                IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                jse.ExecuteScript("arguments[0].scrollIntoView(true);", tArea);

                for (int i = 0; i < mag; i++)
                {
                    var contextAction = new Actions(DlkEnvironment.AutoDriver)
                                        .MoveToElement(tArea, (tArea.Size.Width / 3), 0)
                                        .ClickAndHold(tArea)
                                        .DragAndDropToOffset(tArea, ((tArea.Size.Width / 3)), 0)
                                        .Release(tArea)
                                        .Build(); // tArea, (-(tArea.Size.Width / 3)), 0).Release(tArea).Build();
                    contextAction.Perform();
                }
            }
            catch (Exception e)
            {
                throw new Exception("DragLeft() failed " + e.Message);
            }
        }

        [Keyword("ClickZoomOut", new string[] { "1|text|Number of times|1" })]
        public void ClickZoomOut(string magnification)
        {
            try
            {
                Initialize();
                int mag = Convert.ToInt32(magnification);
                for (int i = 0; i < mag; i++)
                {
                    this.zoomOutButton.Click();
                }
            }
            catch(Exception e)
            {
                throw new Exception("ClickZoomOut() failed " + e.Message);
            }
        }

        [Keyword("ClickZoomIn", new string[] { "1|text|Number of times|1" })]
        public void ClickZoomIn(string magnification)
        {
            try
            {
                Initialize();
                int mag = Convert.ToInt32(magnification);
                for (int i = 0; i < mag; i++)
                {
                    this.zoomInButton.Click();
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickZoomIn() failed " + e.Message);
            }
        }

        [Keyword("ClickMoveLeft", new string[] { "1|text|Number of times|1" })]
        public void ClickMoveLeft(string times)
        {
            try
            {
                Initialize();
                int mag = Convert.ToInt32(times);
                for (int i = 0; i < mag; i++)
                {
                    this.moveLeftButton.Click();
                }
            }catch(Exception e)
            {
                throw new Exception("ClickMoveLeft() failed " + e.Message);
            }
        }

        [Keyword("ClickMoveRight", new string[] { "1|text|Number of times|1" })]
        public void ClickMoveRigth(string times)
        {
            try
            {
                Initialize();
                int mag = Convert.ToInt32(times);
                for (int i = 0; i < mag; i++)
                {
                    this.moveRightButton.Click();
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickMoveRight() failed " + e);
            }
        }


        #region Verify method
        [RetryKeyword("VerifyMilestoneExist", new string[]{    "1|text|Milestone Name|Sample Text",
                                                                "2|text|Expected (TRUE or FALSE)|TRUE"})]
        public void VerifyMilestoneExist(String MilestoneName, String TrueOrFalse)
        {
            String milestoneName = MilestoneName;
            String expectedResult = TrueOrFalse;

            this.PerformAction(() =>
            {
                try
                {
                    Initialize();
                    bool actualResult = false;
                    DlkBaseControl milestoneSelected = mTimeLineMileStones.Find(a => a.GetValue() == milestoneName);
                    if (milestoneSelected is DlkBaseControl)
                    {
                        actualResult = true;
                    }

                    DlkAssert.AssertEqual("VerifyMilestoneExist()", Convert.ToBoolean(expectedResult), actualResult);

                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("SelectMilestone() failed. Unable to find {0}", milestoneName), e);
                }
            }, new String[]{"retry"});
        }
        #endregion
    }
}

