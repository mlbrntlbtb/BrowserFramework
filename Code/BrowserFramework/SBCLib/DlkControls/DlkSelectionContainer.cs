using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using System.Linq;
using CommonLib.DlkUtility;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System.Collections.Generic;

namespace SBCLib.DlkControls
{
    [ControlType("SelectionContainer")]
    public class DlkSelectionContainer : DlkBaseControl
    {

        #region Constructors
        public DlkSelectionContainer(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkSelectionContainer(String ControlName, String SearchType, String[] SearchValues)
        : base(ControlName, SearchType, SearchValues) { }
        public DlkSelectionContainer(String ControlName, IWebElement ExistingWebElement)
        : base(ControlName, ExistingWebElement) { }
        #endregion

        public void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
        }

        #region Constants

        const string STR_PM = "ProductMasterspec";
        const string STR_CM = "ContactManufacturer";
        const string STR_RESOURCES = "Resources";
        const string STR_RADIO = "radio";
        const string STR_CHECK = "Check";

        #endregion

        #region Keywords

        [Keyword("VerifyExists")]
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

        [Keyword("VerifyCheckBoxEditable")]
        public void VerifyCheckBoxEditable(String Index, String TrueOrFalse)
        {
            try
            {
                var ActualResult = IsEnabled(STR_CHECK, Index);
                var ExpectedResult = Convert.ToBoolean(TrueOrFalse);

                DlkAssert.AssertEqual("VerifyCheckBoxEditable() : ", ExpectedResult, ActualResult);
                DlkLogger.LogInfo("VerifyCheckBoxEditable() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCheckBoxEditable() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRadioEditable")]
        public void VerifyRadioEditable(String Index, String TrueOrFalse)
        {
            try
            {
                var ActualResult = IsEnabled(STR_CHECK, Index);
                var ExpectedResult = Convert.ToBoolean(TrueOrFalse);

                DlkAssert.AssertEqual("VerifyRadioEditable() : ", ExpectedResult, ActualResult);
                DlkLogger.LogInfo("VerifyRadioEditable() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRadioEditable() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Selects an items checkbox
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("SelectItem")]
        public void SelectItem(String Index)
        {
            try
            {
                Select(STR_CHECK, Index);

                DlkLogger.LogInfo("SelectItem() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectItem() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Selects an items radio button
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("SelectItemRadioButton")]
        public void SelectItemRadioButton(String Index)
        {
            try
            {
                Select(STR_RADIO, Index);

                DlkLogger.LogInfo("SelectItemRadioButton() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectItemRadioButton() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Selects a link of an Item
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("SelectItemLink")]
        public void SelectItemLink(String ItemIndexOrName, String LinkIndexOrName)
        {
            try
            {
                Initialize();
                int index;
                IWebElement item;
                if (Int32.TryParse(ItemIndexOrName, out index))
                    item = TryGetItem(index);
                else
                    item = TryGetItemByName(ItemIndexOrName);

                var itemLinks = item.FindElements(By.XPath(".//*[contains(@class, 'options')]//*[contains(@ng-repeat-start, 'mProduct')]")).ToList();
                if (itemLinks.Count <= 1) throw new Exception("Links not found.");
                itemLinks.RemoveAt(0); //Will Remove the first item as it is always just a seperator.

                IWebElement link;

                if (Int32.TryParse(LinkIndexOrName, out int link_index))
                    link = itemLinks.ElementAt(link_index - 1);
                else
                {
                    link = itemLinks.Where(l => l.Text.Trim() == LinkIndexOrName.Trim()).FirstOrDefault();
                    if (link == null) throw new Exception($"Cannot find link ({LinkIndexOrName}).");
                }
                
                link.Click();

                DlkLogger.LogInfo("SelectItemLink() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectItemLink() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Will click the ProductMasterspec button of a specified item
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("ClickProductMasterspec")]
        public void ClickProductMasterspec(String ItemIndex)
        {
            try
            {
                ClickItemButton(STR_PM, ItemIndex);

                DlkLogger.LogInfo("ClickProductMasterspec() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickProductMasterspec() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Will click the ContactManufacturer button of a specified item
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("ClickContactManufacturer")]
        public void ClickContactManufacturer(String ItemIndex)
        {
            try
            {
                ClickItemButton(STR_CM, ItemIndex);

                DlkLogger.LogInfo("ClickContactManufacturer() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickContactManufacturer() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Will select a Resources of a specified item
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("SelectResources")]
        public void SelectResources(String ItemIndex, String ResourceIndexOrName)
        {
            try
            {
                var resourses = ClickItemButton(STR_RESOURCES, ItemIndex);
                var options = resourses.FindElements(By.XPath("./option")).ToList();
                if (options.Count < 1) throw new Exception("No options found.");
                options.RemoveAt(0); // to remove the default option

                IWebElement option;

                if (Int32.TryParse(ResourceIndexOrName, out int index))
                    option = options.ElementAt(index - 1);
                else
                {
                    option = options.Where(l => l.Text.Trim() == ResourceIndexOrName.Trim()).FirstOrDefault();
                    if (option == null) throw new Exception("No options found.");
                }

                option.Click();

                DlkLogger.LogInfo("SelectResources() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectResources() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Private Methods

        private void Select(String RadioOrCheckXpathClass, String IndexOrName)
        {
            Initialize();
            var item = Int32.TryParse(IndexOrName, out int index) ? TryGetItem(index) : TryGetItemByName(IndexOrName);

            var toSelect = item.FindElements(By.XPath($".//div[contains(@class,'{RadioOrCheckXpathClass}')]")).FirstOrDefault();
            if (toSelect == null) throw new Exception($"No selection found at item [{index}]");

            toSelect.Click();
        }

        private bool IsEnabled(String RadioOrCheckXpathClass, String IndexOrName)
        {
            Initialize();
            var item = Int32.TryParse(IndexOrName, out int index) ? TryGetItem(index) : TryGetItemByName(IndexOrName);

            var itemToVerify = item.FindElements(By.XPath($".//div[contains(@class,'{RadioOrCheckXpathClass}')]")).FirstOrDefault();
            if (itemToVerify == null) throw new Exception($"No selection found at item [{index}]");

            return !itemToVerify.GetAttribute("class").Contains("disabled");
        }

        private IWebElement ClickItemButton(String ButtonName, String ItemIndex)
        {
            Initialize();
            int itemIndex = TryIfStringIndexValid(ItemIndex);
            var item = TryGetItem(itemIndex);
            string xpathClass = string.Empty;

            switch (ButtonName)
            {
                case STR_PM:
                    xpathClass = "pm";
                    break;
                case STR_CM:
                    xpathClass = "chat";
                    break;
                case STR_RESOURCES:
                    xpathClass = "resources";
                    break;
                default:
                    throw new Exception($"Unsupported Button. [{ButtonName}]");
            }

            var Button = item.FindElements(By.XPath($".//*[contains(@class, '{xpathClass}')]")).FirstOrDefault();
            if (Button == null) throw new Exception($"Cannot find {ButtonName} button.");
            if (Button.GetAttribute("class").Contains("off")) throw new Exception($"{ButtonName} button is not available.");

            Button.Click();

            return Button;
        }

        private IWebElement TryGetItem(int index)
        {
            var items = mElement.FindElements(By.XPath(".//*[contains(@class, 'divSelection')]")).ToList();
            if (!(items.Count > 0)) throw new Exception("Items not found.");
            if (index > items.Count() && index < 1) throw new Exception($"Index [{index}] is out of range.");

            return items.ElementAt(index - 1);
        }

        private IWebElement TryGetItemByName(String ItemName)
        {
            var items = mElement.FindElements(By.XPath(".//*[contains(@class,'divProductInfo_new')]/*[contains(@class,'manufacturer')]")).ToList();
            var item = items.Where(itm => itm.Text.Trim() == ItemName.Trim()).FirstOrDefault();
            if (item == null) throw new Exception($"Cannot find item {ItemName}");
            item = item.FindElements(By.XPath(".//ancestor::*[contains(@class, 'divSelection ')]")).FirstOrDefault();
            if (item == null) throw new Exception($"Cannot find item {ItemName}");
            return item;
        }

        private int TryIfStringIndexValid(String Index)
        {
            if (!Int32.TryParse(Index, out int index))
                throw new Exception($"Index: [{Index}] is not a valid integer input.");

            return index;
        }

        #endregion

    }
}
