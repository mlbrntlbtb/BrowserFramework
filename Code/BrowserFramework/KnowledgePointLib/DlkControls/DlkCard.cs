using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using System.Linq;
using CommonLib.DlkUtility;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("Card")]
    public class DlkCard : DlkBaseControl
    {
        #region Constructors
        public DlkCard(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkCard(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCard(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        public void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
        }

        #region Keywords

        /// <summary>
        /// Clicks project image based on card number parameter
        /// </summary>
        /// <param name="CardNumber"></param>

        [Keyword("ClickProjectContent", new String[] { "1|text|CardNumber|1",
                                                       "2|text|ContentType|Value"})]
        public void ClickProjectContent(string CardNumber, string ContentType)
        {
            try
            {
                Initialize();
                string xpath = GetXpathByContentType(ContentType);
                int cardNumber = Int32.Parse(CardNumber) - 1;

                if (xpath != null)
                    mElement.FindElements(By.XPath(xpath)).ToArray()[cardNumber].Click();
                else
                    throw new Exception("Click() failed: Unsupported ContentType");
               
                DlkLogger.LogInfo("Click() executed");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Clicks project option based on the title displayed
        /// </summary>
        /// <param name="OptionTitle"></param>
        [Keyword("ClickProjectOptions", new String[] { "1|text|OptionTitle|Value" })]
        public void ClickProjectOptions(string OptionTitle)
        {
            try
            {
                Initialize();
                IWebElement optionElement = DlkEnvironment.AutoDriver.FindElements(By.XPath(".//*[@role='tooltip']//span[contains(@class, 'MuiTypography-caption')]")).FirstOrDefault(option => option.Text.ToLower().Trim() == OptionTitle.ToLower());

                if (optionElement != null)
                    optionElement.Click();
                else
                    throw new Exception("ClickProjectOptions() failed: Selected option not available");

                DlkLogger.LogInfo("ClickProjectOptions() executed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickProjectOptions() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies if card content type specified exists.
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        /// <param name="CardNumber"></param>
        /// <param name="ContentType"></param>
        [Keyword("VerifyProjectContentExists", new String[] { "1|text|TrueOrFalse|True",
                                                              "2|text|CardNumber|1",
                                                              "3|text|ContentType|Value" })]
        public void VerifyProjectContentExists(string TrueOrFalse, string CardNumber, string ContentType)
        {
            try
            {
                Initialize();
                string xpath = GetXpathByContentType(ContentType);
                int cardNumber = Int32.Parse(CardNumber) - 1;

                IWebElement element = mElement.FindElements(By.XPath(xpath)).ToArray()[cardNumber];
                bool isElementExists = false;
                if (element != null)
                    isElementExists = true;
                DlkAssert.AssertEqual("VerifyProjectContentExists() : " + mControlName, Convert.ToBoolean(TrueOrFalse), isElementExists);

            }
            catch (Exception e)
            {
                throw new Exception("VerifyProjectContentExists() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies if Card exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyExists", new String[] { "1|text|TrueOrFalse|TRUE" })]
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
        /// Verifies text
        /// </summary>
        /// <param name="CardNumber"></param>
        /// <param name="ContentType"></param>
        /// <param name="ExpectedText"></param>
        [Keyword("VerifyText", new String[] { "1|text|CardNumber|1",
                                              "2|text|ContentType|Value",
                                              "3|text|ExpectedText|Value" })]
        public void VerifyText(String CardNumber, String ContentType, String ExpectedText)
        {
            try
            {
                Initialize();
                int.TryParse(CardNumber, out int cardNumber);
                var cards = GetCards();
                string xpath = GetXpathByContentType(ContentType);
                string elementText = null,
                       textIdentifier = null;

                if (cards == null || cards.Count <= 0) throw new Exception("Cannot find card controls.");

                elementText = DlkString.RemoveCarriageReturn(cards[cardNumber - 1].FindElements(By.XPath(xpath)).FirstOrDefault().Text.Trim());
                
                // switch statement for special content type that requires string manipulation. otherwise, default text is enough
                switch (ContentType)
                {
                    case "PublishDate":
                        textIdentifier = "Published ";
                        elementText = (elementText.Substring(elementText.IndexOf(textIdentifier) + textIdentifier.Length)).Substring(0, 10); // get published date only
                        break;
                    case "Status":
                        textIdentifier = " - ";
                        elementText = elementText.Substring(elementText.IndexOf(textIdentifier) + textIdentifier.Length);
                        break;
                    case "State":
                        elementText = DlkString.RemoveCarriageReturn(mElement.FindElements(By.XPath("(" + xpath + ")[" + CardNumber + "]")).FirstOrDefault().GetAttribute("alt").Trim());
                        elementText = elementText.Substring(elementText.LastIndexOf("Approved For ") + 13);
                        break;
                }

                DlkAssert.AssertEqual("VerifyText() : " + mControlName, ExpectedText, elementText);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }
        /// <summary>
        /// Verifies partial text based on content type
        /// </summary>
        /// <param name="CardNumber"></param>
        /// <param name="ContentType"></param>
        /// <param name="ExpectedText"></param>

        [Keyword("VerifyPartialText", new String[] { "1|text|CardNumber|1",
                                                     "2|text|ContentType|Value",
                                                     "3|text|ExpectedText|Value" })]
        public void VerifyPartialText(String CardNumber, String ContentType, String ExpectedText)
        {
            try
            {
                Initialize(); 
                int.TryParse(CardNumber, out int cardNumber);
                var cards = GetCards();
                string xpath = GetXpathByContentType(ContentType);
                string elementText = null,
                       textIdentifier = null;

                if (cards == null || cards.Count <= 0) throw new Exception("Cannot find card controls.");

                elementText = DlkString.RemoveCarriageReturn(cards[cardNumber - 1].FindElements(By.XPath(xpath)).FirstOrDefault().Text.Trim());

                // switch statement for special content type that requires string manipulation. otherwise, default text is enough
                switch (ContentType)
                {
                    case "PublishDate":
                        textIdentifier = "Published ";
                        elementText = (elementText.Substring(elementText.IndexOf(textIdentifier) + textIdentifier.Length)).Substring(0, 10); // get published date only
                        break;
                    case "Status":
                        textIdentifier = " - ";
                        elementText = elementText.Substring(elementText.IndexOf(textIdentifier) + textIdentifier.Length);
                        break;
                }

                Boolean partOfText = elementText.Contains(ExpectedText);
                if (partOfText)
                    elementText = ExpectedText;

                DlkAssert.AssertEqual("VerifyPartialText() : " + mControlName, ExpectedText, elementText);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPartialText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies card image exists
        /// </summary>
        /// <param name="CardNumber"></param>
        /// <param name="ContentType"></param>
        /// <param name="TrueOrFalse"></param>
        [Keyword("VerifyCardImageExists", new String[] { "1|text|CardNumber|1",
                                                         "2|text|ContentType|Value",
                                                         "3|text|TrueOrFalse|Value" })]
        public void VerifyCardImageExists(String CardNumber, String ContentType, String TrueOrFalse)
        {
            try
            {
                Initialize();
                int.TryParse(CardNumber, out int cardNumber);
                var cards = GetCards();
                if (cards == null || cards.Count <= 0) throw new Exception("Cannot find card controls.");

                string xpath = GetXpathByContentType(ContentType);
                IWebElement element = cards[cardNumber - 1].FindElements(By.XPath(xpath)).FirstOrDefault();
                bool isElementExists = false;
                if (element != null)
                    isElementExists = true;
                DlkAssert.AssertEqual("VerifyCardImageExists() : " + mControlName, Convert.ToBoolean(TrueOrFalse), isElementExists);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCardImageExists() failed : " + e.Message, e);
            }
        }
        /// <summary>
        /// Get card index by card text and assign to variable.
        /// </summary>
        /// <param name="CardName"></param>
        /// <param name="VariableName"></param>
        [Keyword("GetCardNumberByCardName", new String[] { "1|text|CardName|1",
                                                           "2|text|VariableName|Value"})]
        public void GetCardNumberByCardName(String CardName, String VariableName)
        {
            try
            {
                Initialize();
                var cards = GetCards();
                if (cards == null || cards.Count <= 0) throw new Exception("Cannot find card controls.");

                var card = cards.FirstOrDefault(elem => elem.Text.ToLower().Contains(CardName.ToLower()));
                if (card == null)
                {
                    throw new Exception("Cannot find card with value: " + CardName);
                }
                else
                {
                    var cardIndex = (cards.IndexOf(card) + 1).ToString();
                    DlkVariable.SetVariable(VariableName, cardIndex);
                    DlkLogger.LogInfo("AssignCellValueToVariable()", mControlName, "Variable:[" + VariableName + "], Value:[" + cardIndex + "].");
                }
            }
            catch (Exception e)
            {
                throw new Exception("GetCardNumberByProductName() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Private methods
        private List<IWebElement> GetCards()
        {
            return mElement.FindElements(By.XPath(".//*[contains(@class,'projectGridItem') or contains(@class,'productListingCardItem')]")).Where(elem => elem.Enabled).ToList();
        }

        private string GetXpathByContentType(string contentType)
        {
            string xpath = null;
            switch (contentType)
            {
                //div[contains(@class,'MuiGrid-align-content-xs-space-around')]//div[contains(@class,'MuiTypography-caption')])[1]
                case "PublishDate":
                case "Status":
                    xpath = ".//div[contains(@class,'MuiTypography-caption')]";
                    break;
                case "ProductName":
                    xpath = ".//*[contains(@class,'productListingCardProductName')]";
                    break;
                case "ListingType":
                    xpath = ".//p[contains(@class,'productListingType')]";
                    break;
                case "Category":
                    xpath = ".//p[contains(@class, 'MuiTypography-root')]";
                    break;
                case "State":
                    xpath = ".//img[contains(@alt,'Approved For')]";
                    break;
                case "CompanyName":
                    xpath = ".//a[@target='_blank']";
                    break;
                case "CompanyLogo":
                    xpath = ".//a[@target='_blank']//img"; ;
                    break;
                case "CompanyImage":
                    xpath = ".//img[contains(@class,'listingCardCarouselImagePreview')]";
                    break;
                // Contents under Grid Item type
                case "ProjectName":
                    xpath = ".//a//span[text()]";
                    break;
                case "ProjectImage":
                    xpath = ".//img";
                    break;
                case "ProjectFavoriteIcon":
                    xpath = ".//button[contains(@class,'projectFavoriteToggleButton')]";
                    break;
                case "ProjectDetails":
                    xpath = ".//*[contains(@class,'projectInfoPopoutAnchor')]";
                    break;
                case "ProjectOptions":
                    xpath = ".//*[contains(@class,'projectMenuAnchor ')]";
                    break;
                //Teams slider
                case "TeamOptions":
                    xpath = ".//*[contains(@class,'projectTeamInfoPopoutAnchor')]";
                    break;
                //Product Listing
                case "ProductImage":
                    xpath = ".//*[contains(@class,'CardMedia')]";
                    break;
                default:
                    xpath = null;
                    break;
            }
            return xpath;
        }
        #endregion

    }
}

