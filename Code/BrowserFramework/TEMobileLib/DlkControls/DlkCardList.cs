using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMobileLib.DlkControls
{
    [ControlType("CardList")]
    public class DlkCardList : DlkBaseControl
    {
        public DlkCardList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkCardList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCardList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private const int MAX_TRY = 40;

        public void Initialize()
        {
            FindElement();
        }

        [Keyword("VerifyExists")]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyLineText")]
        public void VerifyLineText(String CardRow, String LineRow, String ExpectedLineText)
        {
            try
            {
                var ActualLineText = GetLineText(CardRow, LineRow);

                DlkAssert.AssertEqual("VerifyLineText()", ExpectedLineText, ActualLineText);
                DlkLogger.LogInfo("VerifyLineText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLineText() failed : " + e.Message, e);
            }
        }

        [Keyword("GetLineText")]
        public void GetLineText(String CardRow, String LineRow, String VariableName)
        {
            try
            {
                var ActualLineText = GetLineText(CardRow, LineRow);

                DlkVariable.SetVariable(VariableName, ActualLineText);
                DlkLogger.LogInfo("Successfully executed GetItemCount(). Value: " + ActualLineText);
            }
            catch (Exception e)
            {
                throw new Exception("GetLineText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemCount")]
        public void VerifyItemCount(String ExpectedItemCount)
        {
            try
            {
                string ActualItemCount = IsCardListEmpty() ? "0" : GetAllCards().Count.ToString();

                DlkAssert.AssertEqual("VerifyItemCount()", ExpectedItemCount, ActualItemCount);
                DlkLogger.LogInfo("VerifyItemCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemExistsWithListValue")]
        public void VerifyItemExistsWithListValue(String ValueToSearch, String ExpectedValue)
        {
            try
            {
                bool IsValueExisting;
                if (IsCardListEmpty())
                {
                    IsValueExisting = false;
                }
                else
                {
                    List<IWebElement> AllCards = GetAllCards();
                    IsValueExisting = AllCards.Any(x => x.FindElements(By.XPath("./*[contains(@class, 'lsRowInfo')]/*")).First().Text == ValueToSearch);
                }
                

                DlkAssert.AssertEqual("VerifyItemExistsWithListValue()", ExpectedValue.ToLower(), IsValueExisting.ToString().ToLower());
                DlkLogger.LogInfo("VerifyItemExistsWithListValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemExistsWithListValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetItemCount")]
        public void GetItemCount(String VariableName)
        {
            try
            {
                string ActualItemCount = GetAllCards().Count.ToString();

                DlkVariable.SetVariable(VariableName, ActualItemCount);
                DlkLogger.LogInfo("Successfully executed GetItemCount(). Value: " + ActualItemCount);
            }
            catch (Exception e)
            {
                throw new Exception("GetItemCount() failed : " + e.Message, e);
            }
        }

        [Keyword("DoubleClickValuesListByIndex")]
        public void DoubleClickValuesListByIndex(String CardIndex)
        {
            try
            {
                var cardItem = GetCardItem(CardIndex);
                DlkBaseControl cardRow = new DlkBaseControl("CardRow", cardItem);
                cardRow.DoubleClick();
                DlkLogger.LogInfo("DoubleClickValuesListByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("DoubleClickValuesListByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("Click")]
        public void Click(String CardRow)
        {
            try
            {
                var cardItem = GetCardItem(CardRow);
                new DlkBaseControl("CardItem", cardItem).Click();
                DlkLogger.LogInfo("Click() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("SetCardValue")]
        public void SetCardValue(String CardRow, String Value)
        {
            try
            {
                var cardItem = GetCardItem(CardRow);
                IWebElement cardInputBox;
                bool hasInputBox = cardItem.FindElements(By.ClassName("tsRowHVal")).Any(x => x.Displayed);
                if (hasInputBox)
                {
                    cardInputBox = cardItem.FindElements(By.ClassName("tsRowHVal")).Where(x => x.Displayed).First();
                    new DlkTextBox("InputBox", cardInputBox).Set(Value);
                }
                else
                {
                    throw new Exception("SetCardValue() failed : input box not found");
                }
                DlkLogger.LogInfo("SetCardValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetCardValue() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectCardValue")]
        public void SelectCardValue(String CardRow, String Value)
        {
            try
            {
                var cardItem = GetCardItem(CardRow);
                IWebElement cardInputBox;
                bool hasInputBox = cardItem.FindElements(By.ClassName("tsRowHVal")).Any(x => x.Displayed);
                if (hasInputBox)
                {
                    cardInputBox = cardItem.FindElements(By.ClassName("tsRowHVal")).Where(x => x.Displayed).First();
                    new DlkBaseControl("CardInputBox", cardInputBox).Click();
                    bool hasDropdownItems = DlkEnvironment.AutoDriver.FindElements(By.ClassName("fldAutoCItem")).Any(x => x.Displayed && x.Text.ToLower() == Value.ToLower());
                    if (hasDropdownItems)
                    {
                        IWebElement cardInputDropdownItem = DlkEnvironment.AutoDriver.FindElements(By.ClassName("fldAutoCItem")).Where(x => x.Displayed && x.Text.ToLower() == Value.ToLower()).First();
                        new DlkBaseControl("CardDropdownItem", cardInputDropdownItem).Click();
                    }
                    else
                    {
                        throw new Exception("SelectCardValue() failed : no matching item with input value found");
                    }
                }
                else
                {
                    throw new Exception("SelectCardValue() failed : input box not found");
                }
                DlkLogger.LogInfo("SelectCardValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectCardValue() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickByLineText")]
        public void ClickByLineText(String LineText)
        {
            try
            {
                var cardList = GetAllCards();
                bool bFound = false;
                foreach (IWebElement elmt in cardList)
                {
                    var lineItems = elmt.FindElements(By.XPath("./*[contains(@class, 'lsRowInfo')]/*")).ToList();
                    if (lineItems.Count < 1)
                    {
                        continue;
                    }
                    IWebElement lineItem = lineItems.First();
                    if (lineItem.Text == LineText)
                    {
                        ScrollUntilDisplayed(lineItem);
                        new DlkBaseControl("CardItem", lineItem).Click();
                        bFound = true;
                        DlkLogger.LogInfo("ClickByLineText() passed");
                        break;
                    }
                }
                if (!bFound)
                {
                    throw new Exception("ClickByLineText() failed : line text not found in any card");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickByLineText() failed : " + e.Message, e);
            }
        }

        [Keyword("GetExists", new String[] { "1|text|VariableName|MyVar" })]
        public void GetExists(string sVariableName)
        {
            try
            {
                string sControlExists = Exists(3).ToString();
                DlkVariable.SetVariable(sVariableName, sControlExists);
                DlkLogger.LogInfo("Successfully executed GetExists(). Value : " + sControlExists);
            }
            catch (Exception e)
            {
                throw new Exception("GetExists() failed : " + e.Message, e);
            }
        }

        public string GetLineText(string CardRow, string LineRow)
        {
            int lineRow;
            if (!int.TryParse(LineRow, out lineRow)) throw new Exception($"Invalid line row number [{LineRow}]");

            var cardItem = GetCardItem(CardRow);

            var lineItems = cardItem.FindElements(By.XPath("./*[contains(@class, 'lsRowInfo')]/*")).ToList();
            if (lineItems.Count < 1) throw new Exception("No line items found.");

            if (lineRow > lineItems.Count) throw new Exception($"Line row number [{LineRow}] is greater than the total line items [{lineItems.Count}]");

            var lineItem = lineItems.ElementAt(lineRow-1);
            ScrollUntilDisplayed(lineItem);

            return lineItem.Text.Trim();
        }

        private IWebElement GetCardItem(string CardRow)
        {
            int cardRow;
            if (!int.TryParse(CardRow, out cardRow)) throw new Exception($"Invalid card row number [{CardRow}]");

            var cardList = GetAllCards();

            if (cardRow > cardList.Count) throw new Exception($"Row number [{CardRow}] is greater than the total card items [{cardList.Count}]");

            var cardItem = cardList.ElementAt(cardRow-1);

            ScrollUntilDisplayed(cardItem);

            return cardItem;
        }

        private List<IWebElement> GetAllCards()
        {
            Initialize();
            var cardList = mElement.FindElements(By.XPath(".//*[@class='lsRow']")).ToList();
            if (cardList.Count < 1)
            {
                cardList = mElement.FindElements(By.XPath(".//*[@class='tsRow']")).ToList();
            }
            if (cardList.Count < 1) throw new Exception("No card items found.");
            return cardList;
        }

        private bool IsCardListEmpty()
        {
            Initialize();
            return !mElement.FindElements(By.XPath(".//*[@class='lsRow']")).ToList().Any();
        }

        private void ScrollUntilDisplayed(IWebElement element)
        {
            int tryCount = 0;
            while (!element.Displayed && tryCount < MAX_TRY)
            {
                new DlkBaseControl("CardItem", element).ScrollIntoViewUsingJavaScript();
                tryCount++;
            }
        }
    }
}
