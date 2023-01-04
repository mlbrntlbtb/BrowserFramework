using System;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace ngCRMLib.DlkControls
{
    [ControlType("QuickEdit")]
    public class DlkQuickEdit : DlkBaseControl
    {
        private const String RegClass = "quick-edit";
        private const String OneFourthClass = "span-one-fourth";
        private const String HeaderClass = "header-title-container"; 

        private String mType = "";

        //Regular Quick Edit controls
        private String mRegLabelXPath = ".//span[@class='label']";
        //private String mRegContentXPath = ".//span[@class='content']";
        private String mHeaderLabelXPath = ".//div[contains(@class,'header-title')]";
        private String mRegContentXPath = ".//span[contains(@class,'content')]";
        //private String mRegDivContentXPath = ".//div[@class='content']";
        //private String mRegDivSpanXPath = "./span[2]";
        private String mRegEditIconXPath = ".//span[@class='icon-edit']";
        
        //One-Fourth Quick Edit controls
        private String mOneFourthLabelXPath = "./div/span[contains(@class, 'label')]";
       
        private String mOneFourthContentXPath = "./div[2]";
        private String mOneFourthEditIconXPath = "./div/div[@class='icon-edit']";

        private String mTentativeQuickEditType = "";

        #region Constructors
        public DlkQuickEdit(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkQuickEdit(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        //public DlkQuickEdit(String ControlName, DlkControl ParentControl, String SearchType, String SearchValue)
        //    : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkQuickEdit(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        public void Initialize()
        {

            FindElement();
            //if ((this.mElement.GetAttribute("class").ToLower().Contains(OneFourthClass)) && (!this.mElement.GetAttribute("class").ToLower().Contains(RegClass)))
            //{
            //    mType = OneFourthClass;
            //}
            //else 
            //    mType = RegClass;            
            if (this.mElement.GetAttribute("class").ToLower().Contains(RegClass))
            {
                if ((this.mElement.GetAttribute("class").ToLower().Contains(OneFourthClass)) || (this.mElement.GetAttribute("class").ToLower().Contains("summary-block")))
                {
                    mType = OneFourthClass;
                }
                else if ((this.mElement.GetAttribute("class").ToLower().Contains(HeaderClass)))
                {
                    mType = HeaderClass;
                }
                else
                {
                    mType = RegClass;
                }
            }


        }

        public new bool VerifyControlType()
        {
            FindElement();
            if(this.mElement.GetAttribute("class").ToLower().Contains(RegClass))
            {
                if (this.mElement.GetAttribute("class").ToLower().Contains(OneFourthClass))
                {
                    mTentativeQuickEditType = OneFourthClass;
                }
                else if ((this.mElement.GetAttribute("class").ToLower().Contains(HeaderClass)))
                {
                    mTentativeQuickEditType = HeaderClass;
                }
                else
                {
                    mTentativeQuickEditType = RegClass;
                }
                return true;
            }
            else
            {
                try
                {
                    IWebElement parentElement = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'span-one-forth')]"));
                    mTentativeQuickEditType = OneFourthClass;
                    return true;
                }
                catch
                {   
                }

                try
                {
                    IWebElement parentElement = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'quick-edit')]"));
                    mTentativeQuickEditType = RegClass;
                    return true;
                }
                catch (OpenQA.Selenium.NoSuchElementException)
                {
                    return false;
                }

            }
        }

        public new void AutoCorrectSearchMethod(ref string SearchType, ref string SearchValue)
        {
            try
            {
                DlkBaseControl mCorrectControl = new DlkBaseControl("QuickEdit", "", "");
                bool mAutoCorrect = false;

                VerifyControlType();
                switch (mTentativeQuickEditType)
                {
                    case RegClass:
                        IWebElement parentRegQuickEdit = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'quick-edit')]"));
                        mCorrectControl = new DlkBaseControl("CorrectControl", parentRegQuickEdit);
                        mAutoCorrect = true;
                        break;
                    case HeaderClass:
                        IWebElement parentHeaderQuickEdit = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'quick-edit')]"));
                        mCorrectControl = new DlkBaseControl("CorrectControl", parentHeaderQuickEdit);
                        mAutoCorrect = true;
                        break;
                    case OneFourthClass:
                        IWebElement parentOneFourthQuickEdit = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'span-one-forth')]"));
                        mCorrectControl = new DlkBaseControl("CorrectControl", parentOneFourthQuickEdit);
                        mAutoCorrect = true;
                        break;
                }

                if (mAutoCorrect)
                {
                    String mId = mCorrectControl.GetAttributeValue("id");
                    String mName = mCorrectControl.GetAttributeValue("name");
                    String mClassName = mCorrectControl.GetAttributeValue("class");
                    if (mId != null && mId != "")
                    {
                        SearchType = "ID";
                        SearchValue = mId;
                    }
                    else if (mName != null && mName != "")
                    {
                        SearchType = "NAME";
                        SearchValue = mName;
                    }
                    else if (mClassName != null && mClassName != "")
                    {
                        SearchType = "CLASSNAME";
                        SearchValue = mClassName.Split(' ').First();
                    }
                    else
                    {
                        SearchType = "XPATH";
                        SearchValue = mCorrectControl.FindXPath();
                    }
                }
            }
            catch
            {

            }
        }


        [Keyword("Edit")]
        public void Edit()
        {
            try
            {
                Initialize();
                switch (mType)
                {
                    case RegClass:
                        RegEdit();
                        break;
                    case OneFourthClass:
                        OneFourthEdit();
                        break;
                    case HeaderClass:
                        HeaderEdit();
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Edit() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String ExpectedValue)
        {
            ExpectedValue = ExpectedValue.Trim();
            try
            {
                Initialize();
                switch (mType)
                {
                    case RegClass:
                        RegVerifyText(ExpectedValue);
                        break;
                    case OneFourthClass:
                        OneFourthVerifyText(ExpectedValue);
                        break;
                    case HeaderClass:
                        HeaderEditVerifyText(ExpectedValue);
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            Initialize(); 
            try
            {                               
                    String sValue = mElement.GetAttribute("class");
                    if ((sValue.Contains("locked")) || (!sValue.Contains("editable")))
                    {
                        sValue = "true";
                    }
                    else
                    {
                        sValue = "false";
                    }

                    DlkAssert.AssertEqual("VerifyReadOnly()", Convert.ToBoolean(TrueOrFalse), Convert.ToBoolean(sValue));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        private void RegEdit()
        {
            DlkLogger.LogInfo("RegEdit() started");
            //IWebElement mLabelElement = mElement.FindElement(By.XPath(mRegLabelXPath));
            try
            {
                DlkLabel label = new DlkLabel("Label", this, "XPATH", mRegLabelXPath);

                if (label.Exists(1))
                {
                    label.MouseOver();
                }
                else
                {
                    //IWebElement mContentElement = mElement.FindElement(By.CssSelector(mRegContentCSS));
                    DlkLabel lblContent = new DlkLabel("Content", this, "XPATH", mRegContentXPath);
                    DlkLogger.LogInfo("Start performing mouse over on content label.");
                    lblContent.MouseOver();
                    DlkLogger.LogInfo("No error during mouse over.");
                }

                //IWebElement editIconElement = mElement.FindElement(By.CssSelector(mRegEditIconCSS));
                DlkButton btnEdit = new DlkButton("Edit Icon", this, "XPATH", mRegEditIconXPath);
                btnEdit.Click();
            }
            catch
            {
                CustomEdit();
            }
        }

        private void CustomEdit()
        {
            DlkLogger.LogInfo("CustomEdit() started");
            DlkLabel label = new DlkLabel("Label", this, "XPATH", "./div[contains(@class, 'label')]");

            if (label.Exists(1))
            {
                label.MouseOver();
            }
            else
            {
                DlkLabel lblContent = new DlkLabel("Content", this, "XPATH", "./div[contains(@class, 'content')]");
                DlkLogger.LogInfo("Start performing mouse over on content label.");
                lblContent.MouseOver();
                DlkLogger.LogInfo("No error during mouse over.");
            }

            DlkButton btnEdit = new DlkButton("Edit Icon", this, "XPATH", ".//div[@class='icon-edit']");
            btnEdit.Click();


        }

        private void HeaderEdit()
        {
            DlkLogger.LogInfo("HeaderEdit() started");

            DlkLabel label = new DlkLabel("Label", this, "XPATH", mHeaderLabelXPath);
            if (label.Exists(1))
            {
                label.MouseOver();
            }
            else
            {
                DlkLogger.LogInfo("Label is not found");
            }

            DlkButton btnEdit = new DlkButton("Edit Icon", this, "XPATH", mRegEditIconXPath);
            btnEdit.Click();
        }


        private void OneFourthEdit()
        {
            try
            { 
            IWebElement mLabelElement = mElement.FindElement(By.XPath(mOneFourthLabelXPath));
            DlkLabel label = new DlkLabel("Label", mLabelElement);
            if (label.Exists(3))
            {
                label.MouseOver();
            }
            else
            {
                IWebElement mContentElement = mElement.FindElement(By.XPath(mOneFourthContentXPath));
                DlkLabel lblContent = new DlkLabel("Content", mContentElement);
                lblContent.MouseOver();
            }

            IWebElement editIconElement = mElement.FindElement(By.XPath(mOneFourthEditIconXPath));
            DlkButton btnEdit = new DlkButton("Edit Icon", editIconElement);
            btnEdit.Click();
                }
            catch
            {
                RegEdit();
            }
        }

        private void RegVerifyText(String ExpectedText)
        {
            IWebElement mContentElement = null;
            DlkLabel lblContent = null;
            String ActualResult = "";
            int Index = -1;
           

            try
            {
                mContentElement = this.mElement.FindElement(By.XPath(mRegContentXPath));
                lblContent = new DlkLabel("Content", mContentElement);
                lblContent.VerifyText(ExpectedText);
            }
            catch
            {
                Index = this.mElement.Text.ToString().IndexOf(":");
                if (Index != -1)
                {
                    ActualResult = this.mElement.Text.Substring(Index + 2);                   
                }
                else
                {
                    int Ind = this.mElement.Text.ToString().IndexOf("\r\n");
                    if (Ind != -1)
                    {
                        ActualResult = this.mElement.Text.Substring(Ind + 2);
                    }
                    else
                    {
                        ActualResult = this.mElement.Text.Substring(Ind + 1);
                    }
                    
                }
                
                if (ActualResult.Contains("\r\n"))
                {
                     ActualResult = ActualResult.Replace("\r\n", "<br>");
                }
                    DlkAssert.AssertEqual("VerifyText()", ExpectedText, ActualResult);
            }
        }

        private void HeaderEditVerifyText(String ExpectedText)
        {
            IWebElement mContentElement = null;
            DlkLabel lblContent = null;
            String ActualResult = "";
            int Index = -1;


            try
            {
                mContentElement = this.mElement.FindElement(By.XPath(mHeaderLabelXPath));
                lblContent = new DlkLabel("Content", mContentElement);
                lblContent.VerifyText(ExpectedText);
            }
            catch
            {
                Index = this.mElement.Text.ToString().IndexOf(":");
                if (Index != -1)
                {
                    ActualResult = this.mElement.Text.Substring(Index + 2);
                }
                else
                {
                    int Ind = this.mElement.Text.ToString().IndexOf("\r\n");
                    if (Ind != -1)
                    {
                        ActualResult = this.mElement.Text.Substring(Ind + 2);
                    }
                    else
                    {
                        ActualResult = this.mElement.Text.Substring(Ind + 1);
                    }

                }

                if (ActualResult.Contains("\r\n"))
                {
                    ActualResult = ActualResult.Replace("\r\n", "<br>");
                }
                DlkAssert.AssertEqual("VerifyText()", ExpectedText, ActualResult);
            }
        }


        private void OneFourthVerifyText(String ExpectedText)
        {
            IWebElement mContentElement = null;
            try
            { 
                mContentElement = this.mElement.FindElement(By.XPath(mOneFourthContentXPath)); 
            }
            catch
            {
                RegVerifyText(ExpectedText);
               // mContentElement = this.mElement.FindElement(By.XPath(mRegDivSpanXPath)); 
            }
        }
    }
}
