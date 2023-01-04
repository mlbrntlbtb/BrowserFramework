using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using System.Collections.Generic;
using System.Linq;

namespace SBCLib.DlkControls
{
    [ControlType("ManufacturerGrid")]
    public class DlkManufacturerGrid : DlkBaseControl
    {
        #region Constructors
        public DlkManufacturerGrid(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkManufacturerGrid(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkManufacturerGrid(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region Declarations
        private IList<IWebElement> lstProducts = null;
        private IList<IWebElement> lstManufacturers = null;
        private const string mProductListXpath = ".//div[@id='productTypeContainer'][not(contains(@class,'hide'))]";
        private const string mManufacturerListXpath = ".//div[contains(@class,'product_table')]//ul[contains(@class,'product-checkbox')]/li[not(contains(@class,'hide'))]";
        private const string mProductTypeXpath = ".//div[contains(@class,'nav-product-name-list')]//div[contains(@class,'nav_element')]//span[contains(@class,'product-field')]";
        private const string mUpdateProductTypeXpath = ".//div[contains(@class,'update_product_table')]//a[contains(@class,'update_product_link')]";
        //private const string mExpandProductXpath = ".//div[contains(@class,'nav_product')]//div[contains(@class,'nav_element')]//a[@class='product-in-out']//*[contains(@class,'product-in-out')]";
        private const string mExpandProductXpath = ".//a[@class='product-in-out']//*[contains(@class,'ul_search')]";

        #endregion

        public void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
            FetchProducts();
        }

        #region Keywords

        /// <summary>
        /// Verifies if control exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies if there are product types existing
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("VerifyProductTypeExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyProductTypeExists(String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool bProductExists = lstProducts.Count > 0;
                DlkAssert.AssertEqual("VerifyProductTypeExists()", Convert.ToBoolean(TrueOrFalse), bProductExists);
                DlkLogger.LogInfo("VerifyProductTypeExists passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyProductTypeExists failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Select a product type given the index
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("SelectProductType", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectProductType(String RowIndex)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(RowIndex, out int rowindex)) throw new Exception($"Index: [{RowIndex}] is not a valid integer input.");
                IWebElement element = lstProducts.ElementAt(rowindex - 1).FindElements(By.XPath(mProductTypeXpath)).FirstOrDefault();
                if (element == null) throw new Exception($"Product Type at index[{RowIndex}] is not found.");
                new DlkBaseControl("Item", element).ScrollIntoViewUsingJavaScript();
                element.Click();
                DlkLogger.LogInfo("SelectProductType() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectProductType() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Select a product type given the index
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("ExpandProductType", new String[] { "1|text|Expected Value|TRUE" })]
        public void ExpandProductType(String RowIndex)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(RowIndex, out int rowindex)) throw new Exception($"Index: [{RowIndex}] is not a valid integer input.");
                IWebElement element = lstProducts.ElementAt(rowindex - 1).FindElements(By.XPath(mExpandProductXpath)).FirstOrDefault();
                if (element == null) throw new Exception($"Product Type at index[{RowIndex}] is not found.");
                new DlkBaseControl("Item", element).ScrollIntoViewUsingJavaScript();
                element.Click();
                DlkLogger.LogInfo("ExpandProductType() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ExpandProductType() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Click the update product type button given the index
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("UpdateProductType", new String[] { "1|text|Expected Value|TRUE" })]
        public void UpdateProductType(String RowIndex)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(RowIndex, out int rowindex)) throw new Exception($"Index: [{RowIndex}] is not a valid integer input.");
                IWebElement element = lstProducts.ElementAt(rowindex - 1).FindElements(By.XPath(mUpdateProductTypeXpath)).FirstOrDefault();
                if (element == null) throw new Exception($"Button at index[{RowIndex}] is not found.");
                if (element.GetAttribute("class").ToLower().Contains("disabled")) throw new Exception($"Button at index[{RowIndex}] is currently disabled. Cannot perform click. ");
                new DlkBaseControl("Item", element).ScrollIntoViewUsingJavaScript();
                element.Click();
                DlkLogger.LogInfo("UpdateProductType() passed");
            }
            catch (Exception e)
            {
                throw new Exception("UpdateProductType() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Click the cell given the index
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("ClickCell", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickCell(String RowIndex, String ColIndex)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(RowIndex, out int rowindex)) throw new Exception($"Row Index: [{RowIndex}] is not a valid integer input.");
                if (!Int32.TryParse(ColIndex, out int colindex)) throw new Exception($"Col Index: [{ColIndex}] is not a valid integer input.");

                //Get manufacturers of a product type
                IWebElement element = lstProducts.ElementAt(rowindex - 1);
                FetchManufacturers(element);
                IWebElement target = lstManufacturers.ElementAt(colindex - 1);
                new DlkBaseControl("Item", target).ScrollIntoViewUsingJavaScript();
                target.Click();
                DlkLogger.LogInfo("ClickCell() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickCell() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies if the product type is selected or not
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("VerifyProductSelected", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyProductSelected(String RowIndex, String TrueOrFalse)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(RowIndex, out int rowindex)) throw new Exception($"Index: [{RowIndex}] is not a valid integer input.");
                IWebElement element = lstProducts.ElementAt(rowindex - 1).FindElements(By.XPath(mProductTypeXpath)).FirstOrDefault();
                if (element == null) throw new Exception($"Product Type at index[{RowIndex}] is not found.");
                Boolean ActValue = element.GetAttribute("class").Contains("selected");
                DlkAssert.AssertEqual("VerifyProductSelected()", Convert.ToBoolean(TrueOrFalse), ActValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyProductSelected() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies if the cell is selected or not
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("VerifyCellSelected", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCellSelected(String RowIndex, String ColIndex, string TrueOrFalse)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(RowIndex, out int rowindex)) throw new Exception($"Row Index: [{RowIndex}] is not a valid integer input.");
                if (!Int32.TryParse(ColIndex, out int colindex)) throw new Exception($"Col Index: [{ColIndex}] is not a valid integer input.");

                //Get manufacturers of a product type
                IWebElement element = lstProducts.ElementAt(rowindex - 1);
                FetchManufacturers(element);
                IWebElement target = lstManufacturers.ElementAt(colindex - 1);
                Boolean ActValue = target.FindElement(By.TagName("div")).GetAttribute("class").Contains("selected");
                DlkAssert.AssertEqual("VerifyCellSelected()", Convert.ToBoolean(TrueOrFalse), ActValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellSelected() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Private Methods
        private void FetchProducts()
        {
            lstProducts = mElement.FindElements(By.XPath(mProductListXpath)).ToList();
        }
        private void FetchManufacturers(IWebElement ProductItem)
        {
            lstManufacturers = ProductItem.FindElements(By.XPath(mManufacturerListXpath)).ToList();
        }
        #endregion
    }
}
