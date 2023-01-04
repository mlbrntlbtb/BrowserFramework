using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("Tile")]
    public class DlkTile : DlkBaseControl
    {
        private IWebElement tile;
        private const string tileContainer = ".//div[contains(@class,'MuiGrid-grid-lg-3')]";
        private const string tileContent = ".//div[contains(@class,'MuiCardContent-root')]";
        #region Constructors
        public DlkTile(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTile(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTile(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        public DlkTile(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        #endregion
        public void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
        }

        #region Keywords
        /// <summary>
        /// Verifies if Tile exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="strExpectedValue"></param>

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TileIndex, String TrueOrFalse)
        {
            try
            {
                Initialize();
                FindTile(TileIndex);
                new DlkBaseControl("Tile", tile).VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }
        /// <summary>
        /// Verifies category header 
        /// </summary>
        /// <param name="ExpectedValue"></param>

        [Keyword("VerifyCategoryHeader", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCategoryHeader(String ExpectedValue)
        {
            try
            {
                Initialize();
                string header = mElement.FindElements(By.XPath("./preceding-sibling::div/h4")).FirstOrDefault().Text;
                DlkAssert.AssertEqual("VerifyCategoryHeader() : " + mControlName, ExpectedValue, header);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCategoryHeader() failed : " + e.Message, e);
            }
        }
        /// <summary>
        /// Verifies if category header exists
        /// </summary>
        /// <param name="TrueOrFalse"></param>

        [Keyword("VerifyCategoryHeaderExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCategoryHeaderExists(String TrueOrFalse)
        {
            try
            {
                Initialize();
                IWebElement categoryHeader = mElement.FindElements(By.XPath("./preceding-sibling::div/h4")).FirstOrDefault();
                new DlkBaseControl("VerifyCategoryHeaderExists ", categoryHeader).VerifyExists(Convert.ToBoolean(TrueOrFalse));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCategoryHeaderExists() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Finds tile using TileIndex then verifies tile header. Sample value: Initial Submission
        /// </summary>
        /// <param name="TileIndex"></param>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyTileHeader", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyTileHeader(String TileIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                FindTile(TileIndex);
                string header = tile.FindElements(By.XPath(".//p[contains(@class,'linedHeading')]")).FirstOrDefault().Text;
                DlkAssert.AssertEqual("VerifyTileHeader() : " + mControlName, ExpectedValue, header);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTileHeader() failed : " + e.Message, e);

            }
        }

        /// <summary>
        /// Finds tile using TileIndex then verifies listing date. Sample value: 11/27/2018
        /// </summary>
        /// <param name="TileIndex"></param>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyListDate", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyListDate(String TileIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                FindTile(TileIndex);
                string listingDateContainer = tile.FindElements(By.XPath(".//p[contains(@class,'linedHeadingSubText')]")).FirstOrDefault().Text,
                       identifier = "Listing Date: ",
                       listingDate = listingDateContainer.Substring(listingDateContainer.IndexOf(identifier) + identifier.Length);
                DlkAssert.AssertEqual("VerifyListingDate() : " + mControlName, ExpectedValue, listingDate);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyListingDate() failed : " + e.Message, e);

            }
        }
        /// <summary>
        /// Finds tile using TileIndex then verifies product listing. Sample value: Enhanced Listing
        /// </summary>
        /// <param name="TileIndex"></param>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyProductListing", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyProductListing(String TileIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                FindTile(TileIndex);
                string productListing = tile.FindElements(By.XPath(".//div[contains(@class,'cardImageImage')]")).FirstOrDefault().Text;
                DlkAssert.AssertEqual("VerifyProductListing() : " + mControlName, ExpectedValue, productListing);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyProductListing() failed : " + e.Message, e);

            }
        }

        /// <summary>
        /// Finds tile using TileIndex then verifies title. Sample value: "Lot Full" Signs
        /// </summary>
        /// <param name="TileIndex"></param>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyTitle", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyTitle(String TileIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                FindTile(TileIndex);
                string title = tile.FindElements(By.XPath(tileContent + "//div[contains(@class,'cardImageContentShortTitleLink')]")).FirstOrDefault().Text;
                DlkAssert.AssertEqual("VerifyTitle() : " + mControlName, ExpectedValue, title);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTitle() failed : " + e.Message, e);

            }
        }

        /// <summary>
        /// Finds tile using TileIndex then verifies subtitle. Sample value: Preinstallation meetiungs
        /// </summary>
        /// <param name="TileIndex"></param>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifySubTitle", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifySubTitle(String TileIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                FindTile(TileIndex);
                string subTitle = tile.FindElements(By.XPath(tileContent + "//div[contains(@class,'cardImageContentLongTitleLink')]")).FirstOrDefault().Text;
                DlkAssert.AssertEqual("VerifyTitle() : " + mControlName, ExpectedValue, subTitle);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTitle() failed : " + e.Message, e);

            }
        }
        /// <summary>
        /// Finds tile using TileIndex then verifies desciption. Sample value: Retain "Preinstallation Conference"...
        /// </summary>
        /// <param name="TileIndex"></param>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyDescription", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyDescription(String TileIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                FindTile(TileIndex);
                string desc = tile.FindElements(By.XPath(tileContent + "//p[contains(@class,'cardImageContentText')]")).FirstOrDefault().Text;
                DlkAssert.AssertEqual("VerifyTitle() : " + mControlName, ExpectedValue, desc);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTitle() failed : " + e.Message, e);

            }
        }

        /// <summary>
        /// Finds tile using TileIndex then verifies if upgrade button exists. 
        /// </summary>
        /// <param name="TileIndex"></param>
        /// <param name="TrueOrFalse"></param>
        [Keyword("VerifyUpgradeButtonExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyUpgradeButtonExists(String TileIndex, String TrueOrFalse)
        {
            try
            {
                Initialize();
                FindTile(TileIndex);
                IWebElement upgradeButton = tile.FindElements(By.XPath("//button")).FirstOrDefault();
                new DlkBaseControl("Tile", upgradeButton).VerifyExists(Convert.ToBoolean(TrueOrFalse));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyUpgradeButtonExists() failed : " + e.Message, e);
            }
        }


        #endregion

        private void FindTile(String TileIndex)
        {
            tile = mElement.FindElements(By.XPath(tileContainer + "[" + Convert.ToInt32(TileIndex) + "]")).FirstOrDefault();
        }
    }
}
