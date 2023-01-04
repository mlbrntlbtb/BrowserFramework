using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;


namespace KonaLib.DlkControls
{
    [ControlType("ToDo")]
    public class DlkToDo : DlkBaseControl
    {
        public DlkToDo(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkToDo(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkToDo(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        public DlkToDo(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        

        public void Initialize()
        {
            FindElement();

        }

        [Keyword("EnterNew", new String[] { "1|text|Value|Sample ToDo" })]
        public void EnterNew(string sValue)
        {
            DlkTextBox newToDo = new DlkTextBox("NewToDo", "ID", "new_toDo");
            newToDo.Set(sValue);
        }

        [Keyword("GetIndexWithText", new String[] { "1|text|SearchedText|Follow-up~Sample ToDo",
                                                    "2|text|VariableName|MyIndex"})]
        public void GetIndexWithText(string sSearchedText, string sVariableName)
        {
            Initialize();
            string[] arrToDoTexts =  sSearchedText.Split('~');
            bool bFound = false;
            int iToDoIndex;
            IReadOnlyCollection<IWebElement> toDos = mElement.FindElements(By.XPath("//div[@class='toDo_container']"));
            for (iToDoIndex = 0; iToDoIndex < toDos.Count; iToDoIndex++ )
            {
                IWebElement textAreaElement = toDos.ElementAt(iToDoIndex).FindElement(By.XPath("./textarea"));
                DlkBaseControl TextAreaCtl = new DlkBaseControl("TextArea", textAreaElement);
                if (TextAreaCtl.GetValue() == arrToDoTexts[0])
                {
                    if (arrToDoTexts.Count() > 1)
                    {
                        try
                        {
                            IWebElement associationElement = toDos.ElementAt(iToDoIndex).FindElement(By.XPath(".//a[contains(text(), '" + arrToDoTexts[1] + "')]"));
                            bFound = true;
                            break;
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        bFound = true;
                        break;
                    }
                }

            }
            if(bFound)
            {
                DlkVariable.SetVariable(sVariableName, (iToDoIndex + 1).ToString());
                DlkLogger.LogInfo("GetIndexWithText() passed.");
            }
            else
            {
                throw new Exception("GetIndexWithText() failed. Unable to find ToDo with text '" + sSearchedText + "'.");
            }
        }

        [Keyword("ClickToDoButton", new String[] { "1|text|Index|%MyIndex%",
                                                   "2|text|ButtonCaption|Snooze"})]
        public void ClickToDoButton(string sIndex, string ButtonCaption)
        {
            Initialize();
            IWebElement toDoElement = mElement.FindElement(By.XPath("//div[@class='toDo_container'][" + sIndex + "]"));
            DlkBaseControl toDoCtl = new DlkBaseControl("ToDo", toDoElement);
            toDoCtl.MouseOver();
            IWebElement buttonElement = toDoElement.FindElement(By.XPath("//a[@title='" + ButtonCaption + "']"));
            DlkButton btnToDoButton = new DlkButton("ToDoButton", buttonElement);
            btnToDoButton.Click();
        }

        [Keyword("VerifyToDoExists", new String[] { "1|text|SearchedText|Follow-up~Sample ToDo",
                                                    "2|text|ExpectedValue|TRUE"})]
        public void VerifyToDoExists(string sSearchedText, string sExpectedValue)
        {
            Initialize();
            string[] arrToDoTexts = sSearchedText.Split('~');
            bool bFound = false;
            int iToDoIndex;
            IReadOnlyCollection<IWebElement> toDos = mElement.FindElements(By.XPath("//div[@class='toDo_container']"));
            for (iToDoIndex = 0; iToDoIndex < toDos.Count; iToDoIndex++)
            {
                IWebElement textAreaElement = toDos.ElementAt(iToDoIndex).FindElement(By.XPath("./textarea"));
                DlkBaseControl TextAreaCtl = new DlkBaseControl("TextArea", textAreaElement);
                if (TextAreaCtl.GetValue() == arrToDoTexts[0])
                {
                    if (arrToDoTexts.Count() > 1)
                    {
                        try
                        {
                            IWebElement associationElement = toDos.ElementAt(iToDoIndex).FindElement(By.XPath(".//a[contains(text(), '" + arrToDoTexts[1] + "')]"));
                            bFound = true;
                            break;
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        bFound = true;
                        break;
                    }
                }

            }
            DlkAssert.AssertEqual("VerifyToDoExists()", Convert.ToBoolean(sExpectedValue), bFound);
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String strExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(strExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

       


    }
}
