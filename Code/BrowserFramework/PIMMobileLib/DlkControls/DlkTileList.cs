using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using CommonLib.DlkUtility;

namespace PIMMobileLib.DlkControls
{
    [ControlType("TileList")]
    public class DlkTileList: DlkBaseControl
    {
        #region DECLARATIONS

        #endregion

        #region CONSTRUCTOR
        public DlkTileList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTileList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTileList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
        }
        #endregion

        #region KEYWORDS

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String TileName)
        {
            try
            {
                bool bFound = false;
                Initialize();
                
                Dictionary<string, IWebElement> tileList = new Dictionary<string, IWebElement>();
                Action addElement = () => {
                    var mtileList = this.mElement.FindElements(By.XPath(".//*[contains(@resource-id,'mobileTile')]"));
                    foreach (IWebElement item in mtileList) //4 5 4 4
                    {
                        IWebElement itemTile = null;
                        try
                        {
                            //try to find if may header
                            itemTile = item.FindElement(By.Id("tileHeader"));
                            DlkBaseControl tile = new DlkBaseControl("TileHeader", itemTile);
                            if (!tileList.ContainsKey(tile.GetValue()))
                            {
                                tileList.Add(tile.GetValue(), item);
                                if (tile.GetValue().Trim().ToLower() == TileName.Trim().ToLower())
                                {
                                    tile.Tap();
                                    bFound = true;
                                    break;
                                }
                            }
                        }
                        catch
                        {
                            //swipe screen
                            SwipeToDisplay(item);
                        }
                    }
                };


                var newlyAdded = 0;
                var prevCount = 0;
                do
                {
                    prevCount = prevCount + newlyAdded;
                    addElement();
                    newlyAdded = tileList.Count - prevCount;
                    //Swipe(SwipeDirection.Up, mElement.Size.Height / 2);
                    //SwipeToDisplay(mElement);
                } while (newlyAdded != 0 && !bFound); //no new items and tileheader still not found

                if (!bFound)
                {
                    throw new Exception("Tile '" + TileName + "' not found");
                }
                else
                {
                    DlkLogger.LogInfo("Select - '" + TileName + "' passed");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        private void SwipeToDisplay(IWebElement element)
        {
            Point elementCoord = element.Location;
            Point lstCenterCoord = GetNativeViewCenterCoordinates();
            double dYTranslation = Convert.ToDouble(elementCoord.Y) - Convert.ToDouble(lstCenterCoord.Y);
            if (dYTranslation < 0)
            {
                Swipe(SwipeDirection.Down, Convert.ToInt32(Math.Abs(dYTranslation)));
            }
            else
            {
                Swipe(SwipeDirection.Up, Convert.ToInt32(dYTranslation));
            }
        }

        #endregion
        


    }
}
