using System;
using System.Linq;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using DocDiff;
using System.IO;
using System.Threading;
using System.Xml.Linq;
using CommonLib.DlkUtility;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Interactions;

namespace SBCLib.DlkControls
{
    [ControlType("DataContainer")]
    public class DlkDataContainer : DlkBaseControl
    {
        #region Constructors
        public DlkDataContainer(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDataContainer(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDataContainer(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region Declarations
        private IWebElement mContent = null;
        private IWebElement mHeader = null;
        private IWebElement mFooter = null;
        private const string mContentXpath = ".//div[translate(@id,'0123456789','')='mCSB__container']";
        private const string mHeaderID= "sectionHeaderDiv";
        private const string mFooterID = "sectionFooterDiv";
        private const string reviewNoteXPath = ".//span[contains(@class,'review_note')]";
        private const string projectNoteXPath = ".//span[contains(@class,'project_note')]";
        private const string officemasterNoteXPath = ".//span[contains(@class,'office_master_note')]";
        private const string STR_ACCEPTED = "accepted";
        private const string STR_DELETED = "deleted";
        private const string STR_DEFAULT = "default";
        private string[] ARR_ACCEPTEDVALUES = { STR_DEFAULT ,STR_ACCEPTED, STR_DELETED };        

        #endregion

        private void Initialize()
        {
            FindElement();
            FindParts();
        }

        #region Keywords
        /// <summary>
        ///  Verifies if control exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="strExpectedValue"></param>
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
        ///  Assigns value to a variable name.
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("AssignValueToVariable", new String[] { "1|text|Expected Value|TRUE" })]
        public new void AssignValueToVariable(String VariableName)
        {
            try
            {
                Initialize();
                String content = mContent.Text;
                DlkVariable.SetVariable(VariableName, content);
                DlkLogger.LogInfo("AssignValueToVariable() passed");
            }
            catch (Exception e)
            {
                throw new Exception("AssignValueToVariable() failed : " + e.Message, e);
            }
        }
        
        #region NOTE KEYWORDS

        /// <summary>
        ///  Inserts a note in a given textblock ID. Accepts three types of notes - Review, Project, OfficeMaster
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("InsertNote", new String[] { "1|text|Expected Value|TRUE" })]
        public void InsertNote(String ID, String NoteType)
        {
            try
            {
                Initialize();
                string reviewNoteXPath = ".//div[@class='completeNotes']//div[contains(@class,'review_note')]";
                string projectNoteXPath = ".//div[@class='completeNotes']//div[contains(@class,'project_note')]";
                string officemasterNoteXPath = ".//div[@class='completeNotes']//div[contains(@class,'office_master_note')]";
                IWebElement targetNote = null;

                IWebElement targetText = mContent.FindElements(By.Id(ID)).FirstOrDefault();
                switch (NoteType.ToLower())
                {
                    case "review":
                        targetNote = targetText.FindElements(By.XPath(reviewNoteXPath)).FirstOrDefault();
                        break;
                    case "project":
                        targetNote = targetText.FindElements(By.XPath(projectNoteXPath)).FirstOrDefault();
                        break;
                    case "officemaster":
                        targetNote = targetText.FindElements(By.XPath(officemasterNoteXPath)).FirstOrDefault();
                        break;
                    default:
                        throw new Exception($"[{NoteType}] note type is not supported. Please confirm with developer for supported types.");
                }

                if (targetNote == null) throw new Exception($"No matching note found at ID {ID}"); 
                targetNote.Click();
                DlkLogger.LogInfo("InsertNote() passed");

            }
            catch (Exception e)
            {
                throw new Exception("InsertNote() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Removes a note in a given an instance. Accepts three types of notes - Review, Project, OfficeMaster
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("RemoveNote", new String[] { "1|text|Expected Value|TRUE" })]
        public void RemoveNote(String Instance, String NoteType)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(Instance, out int instNum)) throw new Exception($"Instance: [{Instance}] is not a valid integer input.");
                string removeXpath = ".//span[contains(@id,'removeimg')]";
                IWebElement targetNote = FetchNote(instNum, NoteType);
                targetNote.FindElement(By.XPath(removeXpath)).Click();
                DlkLogger.LogInfo("RemoveNote() passed");

            }
            catch (Exception e)
            {
                throw new Exception("RemoveNote() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Selects a note in a given an instance. Accepts three types of notes - Review, Project, OfficeMaster
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("SelectNote", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectNote(String Instance, String NoteType)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(Instance, out int instNum)) throw new Exception($"Instance: [{Instance}] is not a valid integer input.");
                IWebElement targetNote = FetchNote(instNum, NoteType);
                targetNote.Click();
                DlkLogger.LogInfo("SelectNote() passed");

            }
            catch (Exception e)
            {
                throw new Exception("SelectNote() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Verifies content of a note in a given an instance. Accepts three types of notes - Review, Project, OfficeMaster
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("GetNoteContent", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetNoteContent(String Instance, String NoteType, String VariableName)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(Instance, out int instNum)) throw new Exception($"Instance: [{Instance}] is not a valid integer input.");
                IWebElement targetNote = FetchNote(instNum, NoteType);
                string mValue = targetNote.FindElement(By.TagName("pre")).Text;
                DlkVariable.SetVariable(VariableName, mValue);
                DlkLogger.LogInfo($"GetNoteContent() passed. Variable:[{VariableName}], Value:[{mValue}].");
            }
            catch (Exception e)
            {
                throw new Exception("GetNoteContent() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Verifies content of a note in a given an instance. Accepts three types of notes - Review, Project, OfficeMaster
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("VerifyNoteContent", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyNoteContent(String Instance, String NoteType, String ExpectedText)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(Instance, out int instNum)) throw new Exception($"Instance: [{Instance}] is not a valid integer input.");
                IWebElement targetNote = FetchNote(instNum, NoteType);
                DlkAssert.AssertEqual("VerifyNoteContent(): ", ExpectedText, DlkString.RemoveCarriageReturn(targetNote.Text));
                DlkLogger.LogInfo("VerifyNoteContent() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyNoteContent() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Verifies background color of a note in a given an instance. Accepts three types of notes - Review, Project, OfficeMaster. ColorCode should be in Hex value
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("VerifyNoteColor", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyNoteColor(String Instance, String NoteType, String HexColorCode)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(Instance, out int instNum)) throw new Exception($"Instance: [{Instance}] is not a valid integer input.");
                IWebElement targetNote = FetchNote(instNum, NoteType);
                string ActColor = targetNote.GetCssValue("background-color");
                ActColor = ActColor.Contains("rgb") ? ConvertRGBToHex(ActColor) : ActColor ;
                DlkAssert.AssertEqual("VerifyNoteColor(): ", HexColorCode.ToUpper(), ActColor.ToUpper());
                DlkLogger.LogInfo("VerifyNoteColor() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyNoteColor() failed : " + e.Message, e);
            }
        }


        /// <summary>
        ///  Verifies if notes are displayed. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("VerifyNotesExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyNotesExists(String TrueOrFalse)
        {
            try
            {
                Initialize();
                Boolean bExists = mContent.FindWebElementsCoalesce(false, By.XPath(reviewNoteXPath),
                        By.XPath(projectNoteXPath), By.XPath(officemasterNoteXPath)) == null ? false : true;
                DlkAssert.AssertEqual("VerifyNotesExists(): ", Convert.ToBoolean(TrueOrFalse), bExists);
                DlkLogger.LogInfo("VerifyNotesExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyNotesExists() failed : " + e.Message, e);
            }
        }
        #endregion

        #region TEXT KEYWORDS

        /// <summary>
        /// Will Verify if the changes on the desired element is hidden
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="TrueOrFalse"></param>
        [Keyword("VerifyIsChangesHidden")]
        public void VerifyIsChangesHidden(String ID, String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool.TryParse(TrueOrFalse, out bool ExpectedValue);


                var textContainer = mElement.FindElements(By.XPath($".//*[@id='{ID}']//span[@contenteditable]")).FirstOrDefault();
                if (textContainer == null) throw new Exception("Cannot find Text Container.");

                var isChangesHidden = textContainer.GetAttribute("class").Contains("hideChanges");

                DlkAssert.AssertEqual("VerifyIsChangesHidden(): ", ExpectedValue, isChangesHidden);
                DlkLogger.LogInfo("VerifyIsChangesHidden() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyIsChangesHidden() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Adds text in a given textblock ID.
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("AddNewText", new String[] { "1|text|Expected Value|TRUE" })]
        public void AddNewText(String ID)
        {
            try
            {
                Initialize();
                IWebElement targetText = mContent.FindElements(By.Id(ID)).FirstOrDefault();
                if (targetText == null) throw new Exception($"No matching text found at ID {ID}");
                HoverAndClick(targetText.FindElement(By.Id($"i{ID}")));
                DlkLogger.LogInfo("AddNewText() passed");

            }
            catch (Exception e)
            {
                throw new Exception("AddNewText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Exclude or Include text in a given textblock ID.
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("ExcludeText", new String[] { "1|text|Expected Value|TRUE" })]
        public void ExcludeText(String ID)
        {
            try
            {
                Initialize();
                IWebElement targetText = mContent.FindElements(By.Id(ID)).FirstOrDefault();
                if (targetText == null) throw new Exception($"No matching text found at ID {ID}");
                if (targetText.GetAttribute("class").Contains("strike"))
                {
                    DlkLogger.LogInfo("Text is already in excluded state. No action performed...");
                }
                else
                {
                    HoverAndClick(targetText.FindElement(By.Id($"e{ID}")));
                }
                DlkLogger.LogInfo("ExcludeText() passed");

            }
            catch (Exception e)
            {
                throw new Exception("ExcludeText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Exclude or Include text in a given textblock ID.
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("IncludeText", new String[] { "1|text|Expected Value|TRUE" })]
        public void IncludeText(String ID)
        {
            try
            {
                Initialize();
                IWebElement targetText = mContent.FindElements(By.Id(ID)).FirstOrDefault();
                if (targetText == null) throw new Exception($"No matching text found at ID {ID}");
                if (!targetText.GetAttribute("class").Contains("strike"))
                {
                    DlkLogger.LogInfo("Text is not in excluded state. No action performed...");
                }
                else
                {
                    HoverAndClick(targetText.FindElement(By.Id($"e{ID}")));
                }
                DlkLogger.LogInfo("IncludeText() passed");

            }
            catch (Exception e)
            {
                throw new Exception("IncludeText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Indents text in a given textblock ID.
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("IndentText", new String[] { "1|text|Expected Value|TRUE" })]
        public void IndentText(String ID)
        {
            try
            {
                Initialize();
                string indentButtonXPath = ".//div[@class='indentOutdentImages']//div[@class='indent']";
                IWebElement targetText = mContent.FindElements(By.Id(ID)).FirstOrDefault();
                if (targetText == null) throw new Exception($"No matching text found at ID {ID}");
                new DlkBaseControl("GhostBar", targetText.FindElement(By.Id($"e{ID}"))).MouseOver();
                HoverAndClick(targetText.FindElement(By.XPath(indentButtonXPath)));
                DlkLogger.LogInfo("IndentText() passed");

            }
            catch (Exception e)
            {
                throw new Exception("IndentText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Outdents text in a given textblock ID.
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("OutdentText", new String[] { "1|text|Expected Value|TRUE" })]
        public void OutdentText(String ID)
        {
            try
            {
                Initialize();
                string outdentButtonXPath = ".//div[@class='indentOutdentImages']//div[@class='outdent']";
                IWebElement targetText = mContent.FindElements(By.Id(ID)).FirstOrDefault();
                if (targetText == null) throw new Exception($"No matching text found at ID {ID}");
                new DlkBaseControl("GhostBar", targetText.FindElement(By.Id($"e{ID}"))).MouseOver();
                HoverAndClick(targetText.FindElement(By.XPath(outdentButtonXPath)));
                DlkLogger.LogInfo("OutdentText() passed");

            }
            catch (Exception e)
            {
                throw new Exception("OutdentText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Verifies the level in a given textblock ID.
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("VerifyLevel", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyLevel(String ID, String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement targetText = mContent.FindElements(By.Id(ID)).FirstOrDefault();
                if (targetText == null) throw new Exception($"No matching text found at ID {ID}");
                string ActValue = targetText.FindElement(By.XPath(".//span[@class='bullet']")).Text;
                DlkAssert.AssertEqual("VerifyLevel(): ", ExpectedValue, ActValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLevel() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Gets the ID of the next text container of the specified ID and stores it in variable.
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("GetNextTextID", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetNextTextID(String ID, String VariableName)
        {
            try
            {
                Initialize();
                string mID = string.Empty;
                IWebElement targetText = mContent.FindElements(By.Id(ID)).FirstOrDefault();
                if (targetText == null) throw new Exception($"No matching text found at ID {ID}");
                new DlkBaseControl("Target", targetText).ScrollIntoViewUsingJavaScript(); // target must be displayed

                bool hasChildren = targetText.FindElements(By.XPath(".//div[@class='dd-list element']")).Count > 0 ? 
                                   targetText.FindElement(By.XPath(".//div[@class='dd-list element']")).FindElements(By.TagName("div")).Count > 0 : false;
                //If text has children list, get the first list item
                if (hasChildren)
                {
                    IWebElement firstChild = targetText.FindElement(By.XPath(".//div[@class='dd-list element']")).FindElements(By.XPath(".//div[contains(@class,'dd-item')]")).FirstOrDefault();
                    if (firstChild == null) throw new Exception("No succeeding text found.");
                    mID = firstChild.GetAttribute("id");
                }
                else
                {                    
                    IWebElement mNext = targetText.FindElements(By.XPath($".//following-sibling::div[contains(@class,'dd-item')]")).FirstOrDefault();
                    if (mNext == null)
                    {
                        //If no succeeding text, try checking succeeding text from parent
                        IWebElement mParent = targetText.FindElements(By.XPath(".//parent::div[contains(@class,'dd-list')]")).FirstOrDefault()
                                                .FindElements(By.XPath(".//parent::div[contains(@class,'dd-item')]")).FirstOrDefault();
                        string mClass = mParent.GetAttribute("class");
                        mNext = mParent.FindElements(By.XPath($".//following-sibling::div[@class='{mClass}']")).FirstOrDefault();
                        if (mNext == null)throw new Exception("No succeeding text found.");
                    }
                    mID = mNext.GetAttribute("id");
                }
                DlkVariable.SetVariable(VariableName, mID);
                DlkLogger.LogInfo($"GetNextTextID() passed. Variable:[{VariableName}], Value:[{mID}].");
            }
            catch (Exception e)
            {
                throw new Exception("GetNextTextID() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Clicks a link inside a given textblock ID.
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("ClickTextLink", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickTextLink(String ID)
        {
            try
            {
                Initialize();
                IWebElement targetText = mContent.FindElements(By.Id(ID)).FirstOrDefault();
                if (targetText == null) throw new Exception($"No matching text found at ID {ID}");
                new DlkBaseControl("Target", targetText).ScrollIntoViewUsingJavaScript(); // target must be displayed

                IWebElement mLink = targetText.FindElements(By.XPath(".//span[contains(@class,'product url')]")).FirstOrDefault();
                if (mLink == null) throw new Exception($"No link found at ID {ID}");
                mLink.Click();
                DlkLogger.LogInfo("ClickTextLink() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickTextLink() failed : " + e.Message, e);
            }
        }


        [Keyword("VerifyTextDisplayed", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyTextDisplayed(String ID, String TrueOrFalse)
        {
            try
            {
                Initialize();
                var element = mElement.FindElements(By.XPath($".//*[@id='{ID}']")).FirstOrDefault();
                DlkAssert.AssertEqual("VerifyTextDisplayed(): ", Convert.ToBoolean(TrueOrFalse), element.Displayed);
                DlkLogger.LogInfo("VerifyTextDisplayed() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextDisplayed() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTextContent", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyTextContent(String ID, String ExpectedText)
        {
            try
            {
                Initialize();
                var element = mElement.FindElements(By.XPath($".//*[@id='{ID}']")).FirstOrDefault();
                new DlkBaseControl("element", element).ScrollIntoViewUsingJavaScript();
                var elementText = element.GetAttribute("class").Contains("dd-handle")
                    ? element.Text
                    : element.FindElements(By.XPath(".//*[contains(@class, 'dd-handle')]")).FirstOrDefault().Text;

                var ActualText = Regex.Replace(elementText, @"\s+", " "); // this regex will remove extra spaces
                DlkAssert.AssertEqual("VerifyTextContent (): ", ExpectedText, ActualText);
                DlkLogger.LogInfo("VerifyTextContent() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextContent() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTextExcluded", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyTextExcluded(String ID, String TrueOrFalse)
        {
            try
            {
                Initialize();
                var element = mElement.FindElements(By.XPath($".//*[@id='{ID}']")).FirstOrDefault();
                if (element == null) throw new Exception($"Cannot find element with an ID: {ID}");

                bool isTextExcluded = element.GetAttribute("class").Contains("strike");

                DlkAssert.AssertEqual("VerifyTextExcluded(): ", Convert.ToBoolean(TrueOrFalse), isTextExcluded);
                DlkLogger.LogInfo("VerifyTextExcluded() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextExcluded () failed : " + e.Message, e);
            }
        }
        #endregion

        #region OPTIONS KEYWORDS
        /// <summary>
        ///  Verifies if the option with the specified ID is highlighted as current
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("VerifyOptionHighlighted", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyOptionHighlighted(String ID, String TrueOrFalse)
        {
            try
            {
                Initialize();
                IWebElement targetText = mContent.FindElements(By.Id(ID)).FirstOrDefault();
                if (targetText == null) throw new Exception($"No matching option found at ID {ID}");
                Boolean bHighlighted = targetText.GetAttribute("class").Contains("currentServingOption");
                DlkAssert.AssertEqual("VerifyOptionHighlighted(): ", Convert.ToBoolean(TrueOrFalse), bHighlighted);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyOptionHighlighted() failed : " + e.Message, e);
            }
        }

        [Keyword("RemoveOption", new String[] { "1|text|Expected Value|TRUE" })]
        public void RemoveOption(String ID)
        {
            try
            {
                Initialize();
                var removeButton = mElement.FindElements(By.XPath($".//*[@id='{ID}']//following-sibling::*[@class='update']")).FirstOrDefault();
                if (removeButton == null) throw new Exception("Remove Button not found");
                new DlkBaseControl("removeButton", removeButton).ClickUsingJavaScript();
                DlkLogger.LogInfo("RemoveOption() passed");
            }
            catch (Exception e)
            {
                throw new Exception("RemoveOption() failed : " + e.Message, e);
            }
        }


        /// <summary>
        ///  Verifies the status of all options in a given TextID
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("VerifyAllOptionsStatus", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyAllOptionsStatus(String TextID, String Status)
        {
            try
            {
                Initialize();
                string bState = string.Empty;

                if (!ARR_ACCEPTEDVALUES.Any(x => x.Equals(Status.ToLower()))) { throw new Exception($"Invalid value: [{Status}] . Values accepted for this parameter: {STR_DEFAULT}, {STR_ACCEPTED}, {STR_DELETED}."); }
                IWebElement targetText = mContent.FindElements(By.Id(TextID)).FirstOrDefault();
                if (targetText == null) throw new Exception($"No matching text found at ID {TextID}");
                List<IWebElement> options = targetText.FindElements(By.XPath(".//span[contains(@class,'option')]")).Where(x => x.Displayed).ToList();
                //Remove all option prefix and suffix from list
                options.RemoveAll(x => x.GetAttribute("class").Contains("fix"));
                //If verifying for accepted status, remove insert options from the checks since AcceptAllOptions button doesn't affect them:
                if (Status.ToLower().Equals(STR_ACCEPTED)) { options.RemoveAll(x=> IsInsertOption(x)); }

                //Check if there's at least one option available.
                if (options != null) { 
                    foreach ( IWebElement opt in options)
                    {
                        //Get state of current option and compare with expected. If one option doesn't match, exit loop.
                        bState = GetOptionState(opt);
                        if (Status.ToLower() != bState) break;
                    }
                }
                DlkAssert.AssertEqual("VerifyAllOptionsStatus(): ", Status.ToLower(), bState);
                DlkLogger.LogInfo("VerifyAllOptionsStatus() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAllOptionsStatus() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Verifies the status of an option in a given TextID
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("VerifySingleOptionStatus", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifySingleOptionStatus(String OptionID, String Status)
        {
            try
            {
                Initialize();
                if (!ARR_ACCEPTEDVALUES.Any(x => x.Equals(Status.ToLower()))) { throw new Exception($"Invalid value: [{Status}] . Values accepted for this parameter: {STR_DEFAULT}, {STR_ACCEPTED}, {STR_DELETED}."); }
                IWebElement option = mContent.FindElements(By.Id(OptionID)).FirstOrDefault();
                if (option == null) throw new Exception($"No matching option found at ID {OptionID}");
                DlkAssert.AssertEqual("VerifySingleOptionStatus(): ", Status.ToLower() , GetOptionState(option));
                DlkLogger.LogInfo("VerifySingleOptionStatus() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifySingleOptionStatus() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTextBold", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyTextBold(String ID, String TextToVerifyIfBold, String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool isTextBold = false;

                var section = mElement.FindElements(By.XPath($".//*[@id='{ID}']//*[contains(@class, 'element_text')]/span")).FirstOrDefault();
                if (section == null) throw new Exception($"Cannot find section with ID [{ID}]");

                var allBoldText = section.FindElements(By.XPath("./b")).ToList();
                if (allBoldText.Count > 0)
                    isTextBold = allBoldText.Select(bt => bt.Text.Trim()).Contains(TextToVerifyIfBold.Trim());

                DlkAssert.AssertEqual("VerifyTextBold(): ", Convert.ToBoolean(TrueOrFalse), isTextBold);
                DlkLogger.LogInfo("VerifyTextBold() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextBold() failed : " + e.Message, e);
            }
        }

        [Keyword("SetTextBold", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetTextBold(String ID, String StartCaretIndex, String CharCountToSelect)
        {
            try
            {
                Initialize();
                int startIndex;
                int charCount;

                bool isValidStartIndex = int.TryParse(StartCaretIndex, out startIndex);
                if (!isValidStartIndex) throw new Exception($"Invalid start index [{StartCaretIndex}]");
                bool isValidEndIndex = int.TryParse(CharCountToSelect, out charCount);
                if (!isValidEndIndex) throw new Exception($"Invalid end index [{CharCountToSelect}]");

                ClickTextButton(1, ID, startIndex, charCount);

                DlkLogger.LogInfo("SetTextBold() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetTextBold() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickTextButton", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickTextButton(String ID, String ButtonIndex, String StartCaretIndex, String CharCountToSelect)
        {
            try
            {
                Initialize();
                int buttonIndex;
                int startIndex;
                int charCount;

                bool isValidButtonIndex = int.TryParse(ButtonIndex, out buttonIndex);
                if (!isValidButtonIndex) throw new Exception($"Invalid start index [{ButtonIndex}]");
                bool isValidStartIndex = int.TryParse(StartCaretIndex, out startIndex);
                if (!isValidStartIndex) throw new Exception($"Invalid start index [{StartCaretIndex}]");
                bool isValidEndIndex = int.TryParse(CharCountToSelect, out charCount);
                if (!isValidEndIndex) throw new Exception($"Invalid end index [{CharCountToSelect}]");

                ClickTextButton(buttonIndex, ID, startIndex, charCount);

                DlkLogger.LogInfo("ClickTextButton() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickTextButton() failed : " + e.Message, e);
            }
        }

        [Keyword("EditText", new String[] { "1|text|Expected Value|TRUE" })]
        public void EditText(String ID, String StartCaretIndex, String CharCountToChange, String TextToReplace)
        {
            try
            {
                Initialize();
                int startIndex;
                int charCount;

                bool isValidStartIndex = int.TryParse(StartCaretIndex, out startIndex);
                if (!isValidStartIndex) throw new Exception($"Invalid start index [{StartCaretIndex}]");
                bool isValidEndIndex = int.TryParse(CharCountToChange, out charCount);
                if (!isValidEndIndex) throw new Exception($"Invalid end index [{CharCountToChange}]");

                var section = mElement.FindElements(By.XPath($".//*[@id='{ID}']//*[contains(@class, 'element_text')]/span")).FirstOrDefault();
                if (section == null) throw new Exception($"Cannot find section with ID [{ID}]");

                SetCaretPosition(section, startIndex, charCount);

                new Actions(DlkEnvironment.AutoDriver)
                    .SendKeys(Keys.Backspace)
                    .SendKeys(TextToReplace)
                    .Perform();

                DlkLogger.LogInfo("EditText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("EditText() failed : " + e.Message, e);
            }
        }

        [Keyword("SetCaretPosition", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetCaretPosition(String ID, String StartCaretIndex)
        {
            try
            {
                Initialize();
                int startIndex;

                bool isValidStartIndex = int.TryParse(StartCaretIndex, out startIndex);
                if (!isValidStartIndex) throw new Exception($"Invalid start index [{StartCaretIndex}]");

                var section = mElement.FindElements(By.XPath($".//*[@id='{ID}']//*[contains(@class, 'element_text')]/span")).FirstOrDefault();
                if (section == null) throw new Exception($"Cannot find section with ID [{ID}]");

                SetCaretPosition(section, startIndex);

                DlkLogger.LogInfo("SetCaretPosition() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetCaretPosition() failed : " + e.Message, e);
            }
        }

        [Keyword("InsertText", new String[] { "1|text|Expected Value|TRUE" })]
        public void InsertText(String ID, String StartCaretIndex, String TextToInsert)
        {
            try
            {
                Initialize();
                int startIndex;

                bool isValidStartIndex = int.TryParse(StartCaretIndex, out startIndex);
                if (!isValidStartIndex) throw new Exception($"Invalid start index [{StartCaretIndex}]");

                var section = mElement.FindElements(By.XPath($".//*[@id='{ID}']//*[contains(@class, 'element_text')]/span")).FirstOrDefault();
                if (section == null) throw new Exception($"Cannot find section with ID [{ID}]");

                SetCaretPosition(section, startIndex);

                new Actions(DlkEnvironment.AutoDriver)
                    .SendKeys(TextToInsert)
                    .Perform();

                DlkLogger.LogInfo("InsertText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("InsertText() failed : " + e.Message, e);
            }
        }

        ///// <summary>
        /////  Verifies if the number of remaining options matches the specified value
        ///// </summary>
        ///// <param name="strExpectedValue"></param>
        //[Keyword("VerifyOptionsRemainingCount", new String[] { "1|text|Expected Value|TRUE" })]
        //public void VerifyOptionsRemainingCount(String ExpectedValue)
        //{
        //    try
        //    {
        //        Initialize();
        //        List<string> validIDList = new List<string>();
        //        List<IWebElement> optionsList = mContent.FindElements(By.XPath(".//span[@class='choicegroup']")).ToList();
        //        foreach(IWebElement option in optionsList)
        //        {
        //            IWebElement divContainer = option.FindElements(By.XPath(".//ancestor::div[contains(@class,'dd-item paragraph')]")).FirstOrDefault();
        //            if (!divContainer.GetAttribute("class").Contains("strike"))
        //            {
        //                if(!validIDList.Any(x=>x.Equals(divContainer.GetAttribute("id")))) { validIDList.Add(divContainer.GetAttribute("id")); }                        
        //            }
        //        }
        //        int actValue = validIDList.Count();
        //        DlkAssert.AssertEqual("VerifyOptionsRemainingCount(): ", ExpectedValue, actValue.ToString());
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("VerifyOptionsRemainingCount() failed : " + e.Message, e);
        //    }
        //}
        #endregion
        /// <summary>
        ///  Verifies the formatting of a specified text
        /// </summary>
        /// <param name="strExpectedValue"></param>
        //[Keyword("VerifyFormatting", new String[] { "1|text|Expected Value|TRUE" })]
        //public void VerifyFormatting (String ExpectedFormat)
        //{
        //    try
        //    {
        //        Initialize();
        //        String targetXPATH = ".//div[@class='commentwrapper'][2]//span[@class='element_text']//span[contains(@class,'text')]";
        //        IWebElement target = mContent.FindElement(By.XPath(targetXPATH));
        //        new DlkBaseControl("Item", target).ScrollIntoViewUsingJavaScript();
        //        string ActualFormat = target.GetAttribute("style");
        //        DlkAssert.AssertEqual("VerifyFormatting():", ExpectedFormat, ActualFormat);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("VerifyFormatting() failed : " + e.Message, e);
        //    }
        //}

        /// <summary>
        ///  Clicks the Bold button if it exists
        /// </summary>
        /// <param name="strExpectedValue"></param>
        //[Keyword("ApplyBold", new String[] { "1|text|Expected Value|TRUE" })]
        //public void ApplyBold()
        //{
        //    try
        //    {
        //        Initialize();
        //        string boldfloatXPath = ".//div[@class='float-tool-cont']";
        //        IWebElement boldFloat = mContent.FindElements(By.XPath(boldfloatXPath)).FirstOrDefault();
        //        if(boldFloat != null)
        //        {
        //            boldFloat.Click();
        //        }
        //        else
        //        {
        //            throw new Exception($"Object [Bold] not found.");
        //        }
        //        DlkLogger.LogInfo("ApplyBold() passed");
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("ApplyBold() failed : " + e.Message, e);
        //    }
        //}

        /// <summary>
        ///  Verifies if control exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("CompareToDocument", new String[] { "1|text|Expected Value|TRUE" })]
        public void CompareToDocument(String ExpectedFile, String OutputFile)
        {
            try
            {
                Initialize();
                //String content = mContent.GetAttribute("innerText");

                //if absolute path, use it. if not, form path from default folder
                string mExpectedFile = Path.IsPathRooted(ExpectedFile) ? ExpectedFile : Path.Combine(DlkEnvironment.mDirDocDiffExpectedFile, ExpectedFile);

                //check if OutputFile is a valid file name
                string mOutputFile;
                if (Path.IsPathRooted(OutputFile))
                {
                    mOutputFile = OutputFile;
                }
                else if (!string.IsNullOrEmpty(OutputFile) && OutputFile.IndexOfAny(Path.GetInvalidFileNameChars()) < 0)
                {
                    string outputPath = Path.Combine(DlkEnvironment.mDirDocDiff, "OutputFile");
                    //create directory: if directory already exist, it does nothing
                    Directory.CreateDirectory(outputPath);
                    mOutputFile = Path.Combine(outputPath, OutputFile);
                }
                else
                {
                    throw new Exception("CompareToDocument(). OutputFile is invalid: " + OutputFile);
                }

                Program.ExecuteDocDiff(mExpectedFile, GetListContent(mContent), mOutputFile);
                for (int i = 0; i < 60; i++)
                {
                    if (File.Exists(mOutputFile))
                    {
                        break;
                    }
                    Thread.Sleep(500);
                }
                if (File.Exists(mOutputFile))
                {
                    XDocument DlkXml = XDocument.Load(mOutputFile);
                    var data = from doc in DlkXml.Descendants("info")
                               select new
                               {
                                   errorcount = doc.Element("errorcount").Value
                               };
                    var sheetnameComparison = from doc in DlkXml.Descendants("result")
                                              select new
                                              {
                                                  testtype = doc.Element("testtype").Value
                                              };
                    foreach (var val in data)
                    {
                        int iErrorCount = Convert.ToInt32(val.errorcount);
                        bool sheetNameError = sheetnameComparison.Count(x => x.testtype == "SheetNameComparison") > 0;
                        if (iErrorCount > 0)
                        {
                            string errorType = sheetNameError ? "sheet name comparison" : "comparison";
                            throw new Exception("CompareToDocument(): Errors found during " + errorType + ". See outputfile: " + mOutputFile);
                        }
                        else
                        {
                            DlkLogger.LogInfo("CompareToDocument(): No differences found between Expected and Actual files.");
                        }
                    }
                }
                else
                {
                    throw new Exception("CompareToDocument(): Output file not found. File: " + mOutputFile);
                }
                DlkLogger.LogInfo("CompareToDocument() passed");
            }
            catch (Exception e)
            {
                throw new Exception("CompareToDocument() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Private Methods

        private void ClickTextButton(int buttonIndex, string ID, int startIndex, int charCount)
        {
            try
            {
                var section = mElement.FindElements(By.XPath($".//*[@id='{ID}']//*[contains(@class, 'element_text')]/span")).FirstOrDefault();
                if (section == null) throw new Exception($"Cannot find section with ID [{ID}]");

                SetCaretPosition(section, startIndex, charCount);

                var button = mElement.FindElements(By.XPath($".//*[@id='{ID}']//*[contains(@class, 'flt-icon') and contains(@class, 'bld')][{buttonIndex}]")).FirstOrDefault();
                if (button == null) throw new Exception("Button not found.");
                button.Click();
            }
            catch (Exception e)
            {
                throw new Exception("ClickTextButton() : " + e.Message, e);
            }
        }

        private void SetCaretPosition(IWebElement element, int startIndex, int charCountToSelect = -1)
        {
            try
            {
                // Initializing caret position
                var driver = DlkEnvironment.AutoDriver;
                element.Click();
                new Actions(driver)
                    .KeyDown(element, Keys.Control)
                    .SendKeys(element, "a")
                    .KeyUp(Keys.Control)
                    .SendKeys(element, Keys.Left)
                    .Perform();

                // Will set caret to startIndex
                for (var i = 0; i < startIndex; i++) new Actions(driver).SendKeys(Keys.Right).Perform();


                // (OPTIONAL) Will set caret to highlight charCount
                if (charCountToSelect != -1)
                    for (var i = 0; i < charCountToSelect; i++) 
                        new Actions(driver)
                            .KeyDown(Keys.Shift)
                            .SendKeys(Keys.Right)
                            .KeyUp(Keys.Shift)
                            .Perform();
                    
            }
            catch (Exception e)
            {
                throw new Exception("SetCaretPosition() : " + e.Message, e);
            }
        }

        private void FindParts()
        {
            mContent = mElement.FindElements(By.XPath(mContentXpath)).FirstOrDefault();
            mHeader = mElement.FindElements(By.Id(mHeaderID)).FirstOrDefault();
            mFooter = mElement.FindElements(By.Id(mFooterID)).FirstOrDefault();
        }

        private IWebElement FetchNote(int index, String NoteType)
        {
            try
            {
                IWebElement ret = null;
                switch (NoteType.ToLower())
                {
                    case "review":
                        ret = mContent.FindElements(By.XPath(reviewNoteXPath)).ElementAt(index - 1);
                        break;
                    case "project":
                        ret = mContent.FindElements(By.XPath(projectNoteXPath)).ElementAt(index - 1);
                        break;
                    case "officemaster":
                        ret = mContent.FindElements(By.XPath(officemasterNoteXPath)).ElementAt(index - 1);
                        break;
                    default:
                        throw new Exception($"[{NoteType}] note type is not supported. Please confirm with developer for supported types.");
                }
                return ret;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private List<string> GetListContent(IWebElement Element)
        {
            List<String> ret = new List<String>();
            List<IWebElement> mTexts = new List<IWebElement>();
            IList<IWebElement> mElms = Element.FindWebElementsCoalesce(true, By.TagName("div"));
            DlkLogger.LogInfo("Retrieving text from web...");

            //add section name
            IWebElement sectionName = Element.FindElements(By.XPath(".//span[@class='capture-text']")).FirstOrDefault();
            if (sectionName != null) mTexts.Add(sectionName);

            //sift through each div if it's a valid text container        
            foreach (IWebElement elm in mElms)
            {
                string classValue = elm.GetAttribute("class").ToLower();
                if (classValue.Equals("part")) mTexts.Add(elm);
                if (classValue.Contains("endofsection")) mTexts.Add(elm);
                if (classValue.Contains("element_main") & !classValue.Contains("note"))
                {
                    IWebElement captureText = elm.FindElements(By.XPath(".//*[contains(@class,'capture-text')]")).FirstOrDefault();
                    if (captureText != null) mTexts.Add(captureText);
                }
            }            
            //add each text to List
            foreach (IWebElement element in mTexts)
            {
                string txt = DlkString.RemoveCarriageReturn(element.GetAttribute("innerText"));
                ret.Add(txt);
            }
            return ret;
        }

        private string ConvertRGBToHex(String ColorCode)
        {
            string colorCode = ColorCode.Substring(5).TrimEnd(')');
            int R = Convert.ToInt32(colorCode.Split(',').ElementAt(0));
            int G = Convert.ToInt32(colorCode.Split(',').ElementAt(1));
            int B = Convert.ToInt32(colorCode.Split(',').ElementAt(2));
            Color mColor = Color.FromArgb(R,G,B);
            string ret = $"#{mColor.R:X2}{mColor.G:X2}{mColor.B:X2}";
            return ret;
        }

        private void HoverAndClick(IWebElement Target)
        {
            DlkBaseControl targetButton = new DlkBaseControl("Button", Target);
            targetButton.ScrollIntoView();
            targetButton.MouseOver();
            targetButton.Click();
        }

        private string GetOptionState(IWebElement Option)
        {
            string optionClass = Option.GetAttribute("class");
            bool bPrefixExists = Option.FindElements(By.XPath(".//*[contains(@class,'option_prefix')]")).Count > 0;
            string bState = optionClass.Contains("selected") ? STR_ACCEPTED :
                            !bPrefixExists ? STR_ACCEPTED : //This is for special case: Insert options. Prefix disappears when Insert option is accepted
                            optionClass.Contains("strike") ? STR_DELETED :
                            STR_DEFAULT;
            return bState;
        }

        /// <summary>
        /// Returns true if the chosen option is an Insert option. Insert options are enclosed by <>
        /// </summary>
        /// <param name="Option"></param>
        /// <returns></returns>
        private bool IsInsertOption(IWebElement Option)
        {
            return Option.FindElement(By.XPath(".//*[contains(@class,'option_prefix')]")).Text.Equals("<");
        }

        #endregion
    }
}
