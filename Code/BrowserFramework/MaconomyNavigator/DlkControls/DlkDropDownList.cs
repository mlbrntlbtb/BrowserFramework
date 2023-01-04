using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium.Support.UI;

namespace MaconomyNavigatorLib.DlkControls
{
    [ControlType("DropDownList")]
    public class DlkDropDownList : DlkBaseControl
    {
        private IWebElement mDropDownButton;

        public DlkDropDownList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDropDownList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        //public DlkTextBox(String ControlName, DlkControl ParentControl, String SearchType, String SearchValue)
        //    : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkDropDownList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {

            FindElement();
            //mDropDownButton = mElement.FindElement(By.XPath("./descendant::button[1]"));
        }

        [Keyword("Select", new String[] { "1|text|Value|SampleValue" })]
        public void Select(String Item)
        {
            try
            {
                Initialize();
                if (mElement.GetAttribute("class").Contains("form-control"))
                {
                    DlkLogger.LogInfo("Opening dropdown");
                    mDropDownButton = mElement.FindElement(By.XPath("./parent::*[1]//a[1]"));
                    //mDropDownButton = mElement.FindElement(By.XPath("./preceding::a[1]"));
                    mDropDownButton.Click();
                    Thread.Sleep(1000);

                    if (mElement.GetAttribute("class").Contains("enum"))
                    {
                        IWebElement target = mElement.FindElement(By.XPath("./parent::div[1]/parent::div[1]/descendant::div[@class='result-wrapper']/descendant::a/div[contains(translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'),'"
                        + Item.ToLower() + "')]"));
                        target.Click();
                    }
                    else
                    {
                        //old one used for form-control only
                        IWebElement target = mElement.FindElement(By.XPath("./parent::div[1]/parent::div[1]/descendant::div[@class='result-wrapper']/descendant::a/descendant::p[translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')='"
                        + Item.ToLower() + "']"));
                        target.Click();
                    }
                }
                else if (mElement.FindElements(By.XPath("./button[contains(@class,'dropdown-toggle')]")).Count == 1)
                {
                    //T#616101
                    DlkLogger.LogInfo("Opening dropdown");
                    mElement.FindElement(By.XPath("./button[contains(@class,'dropdown-toggle')]")).Click();
                    DlkLogger.LogInfo("Finding " + Item);
                    mElement.FindElement(By.XPath("./div[contains(@class,'dropdown-menu')]//*[text()='"+Item+"']")).Click();
                }
                else if (mElement.GetAttribute("class").Contains("group-btn"))
                {
                    mDropDownButton = mElement.FindElement(By.XPath("./button[1]"));
                    DlkLogger.LogInfo("Opening dropdown");
                    mDropDownButton.Click();
                    Thread.Sleep(1000);

                    IWebElement target = mElement.FindElement(By.XPath(".//li[1]//a[translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')='"
                        + Item.ToLower() + "']"));
                    target.Click();
                }
                else
                {
                    if (!mElement.FindElement(By.XPath("./following-sibling::div[1]")).Displayed)
                    {
                        DlkLogger.LogInfo("Opening dropdown");
                        mElement.Click();
                    }

                    IWebElement target = mElement.FindElement(By.XPath("./following-sibling::div[1]/descendant::a[translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')='"
                        + Item.ToLower() + "']"));
                    target.Click();
                }
                DlkLogger.LogInfo("Successfully executed Select()");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectContains", new String[] { "1|text|Value|SampleValue" })]
        public void SelectContains(String SearchItem)
        {
            try
            {
                Initialize();
                if (mElement.GetAttribute("class").Contains("form-control"))
                {
                    DlkLogger.LogInfo("Opening dropdown");
                    mDropDownButton = mElement.FindElement(By.XPath("./parent::*[1]//a[1]"));
                    //mDropDownButton = mElement.FindElement(By.XPath("./preceding::a[1]"));
                    mDropDownButton.Click();
                    Thread.Sleep(1000);

                    if (mElement.GetAttribute("class").Contains("enum"))
                    {
                        IWebElement target = mElement.FindElement(By.XPath("./parent::div[1]/parent::div[1]/descendant::div[@class='result-wrapper']/descendant::a/div[contains(.,'" + SearchItem + "')]"));
                        target.Click();
                    }
                    else
                    {
                        //old one used for form-control only
                        IWebElement target = mElement.FindElement(By.XPath("./parent::div[1]/parent::div[1]/descendant::div[@class='result-wrapper']/descendant::a/descendant::p[contains(.,'"+ SearchItem + "')]"));
                        target.Click();
                    }
                }
                else if (mElement.FindElements(By.XPath("./button[contains(@class,'dropdown-toggle')]")).Count == 1)
                {
                    DlkLogger.LogInfo("Opening dropdown");
                    mElement.FindElement(By.XPath("./button[contains(@class,'dropdown-toggle')]")).Click();
                    DlkLogger.LogInfo("Finding " + SearchItem);
                    mElement.FindElement(By.XPath("./div[contains(@class,'dropdown-menu')]//*[text()[contains(.,'" + SearchItem + "')]]")).Click();
                }
                else if (mElement.GetAttribute("class").Contains("group-btn"))
                {
                    mDropDownButton = mElement.FindElement(By.XPath("./button[1]"));
                    DlkLogger.LogInfo("Opening dropdown");
                    mDropDownButton.Click();
                    Thread.Sleep(1000);

                    IWebElement target = mElement.FindElement(By.XPath(".//li[1]//a[contains(.,'"+ SearchItem + "']"));
                    target.Click();
                }
                else
                {
                    if (!mElement.FindElement(By.XPath("./following-sibling::div[1]")).Displayed)
                    {
                        DlkLogger.LogInfo("Opening dropdown");
                        mElement.Click();
                    }

                    IWebElement target = mElement.FindElement(By.XPath("./following-sibling::div[1]/descendant::a/span[contains(.,'" + SearchItem + "')]"));
                    target.Click();
                }
                DlkLogger.LogInfo("Successfully executed SelectContains()");
            }
            catch (Exception e)
            {
                throw new Exception("SelectContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyList(string Items)
        {
            try
            {
                Initialize();
                bool bIsOpenedHere = false;
                string actual = "";
                string expected = "";

                // process expected
                foreach (string expItm in Items.Split('~'))
                {
                    expected += DlkString.ReplaceCarriageReturn(expItm, "") + "~";
                }
                expected = expected.Trim('~');

                if (mElement.GetAttribute("class").Contains("form-control"))
                {
                    mDropDownButton = mElement.FindElement(By.XPath("./parent::*[1]//a[1]"));
                    DlkLogger.LogInfo("Opening dropdown");
                    mDropDownButton.Click();
                    bIsOpenedHere = true;
                    Thread.Sleep(1000);

                    foreach (IWebElement elm in mElement.FindElements(By.XPath("./parent::div[1]/parent::div[1]/descendant::div[@class='result-wrapper']/descendant::a/div[text()]")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                        actual += DlkString.ReplaceCarriageReturn(ctl.GetValue(), "").Trim() + "~";
                    }
                    actual = actual.Trim('~');
                    DlkAssert.AssertEqual("VerifyList()", expected, actual);

                    if (bIsOpenedHere)
                    {
                        DlkLogger.LogInfo("Closing dropdown");
                        mDropDownButton.Click();
                    }
                }
                else if (mElement.GetAttribute("class").Contains("group-btn"))
                {
                    mDropDownButton = mElement.FindElement(By.XPath("./button[1]"));
                    DlkLogger.LogInfo("Opening dropdown");
                    mDropDownButton.Click();
                    bIsOpenedHere = true;
                    Thread.Sleep(1000);

                    foreach (IWebElement elm in mElement.FindElements(By.XPath(".//li[1]//a")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                        actual += DlkString.ReplaceCarriageReturn(ctl.GetValue(), "") + "~";
                    }
                    actual = actual.Trim('~');
                    DlkAssert.AssertEqual("VerifyList()", expected, actual);

                    if (bIsOpenedHere)
                    {
                        DlkLogger.LogInfo("Closing dropdown");
                        mDropDownButton.Click();
                    }
                }
                else if (mElement.FindElements(By.XPath("./following-sibling::div[1]")).Count == 1)
                {
                    //T#622152, different xpath in items dropdown and only the arrow can be clicked, hence the if..
                    if (mElement.FindElements(By.XPath("./preceding-sibling::a/*[@class='caret']")).Count == 1)
                    {
                        var caret = mElement.FindElement(By.XPath("./preceding-sibling::a/*[@class='caret']"));
                        DlkLogger.LogInfo("Opening dropdown");
                        new DlkBaseControl("ddArrow", caret).Click();
                        bIsOpenedHere = true;
                        foreach (IWebElement elm in mElement.FindElements(By.XPath("./following-sibling::div[1]/descendant::div[contains(@class,'ng-binding')]")))
                        {
                            DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                            actual += DlkString.ReplaceCarriageReturn(ctl.GetValue().Trim(), "") + "~";
                        }
                        actual = actual.Trim('~');

                        DlkAssert.AssertEqual("VerifyList()", expected, actual);
                        if (bIsOpenedHere)
                        {
                            DlkLogger.LogInfo("Closing dropdown");
                            mElement.Click();
                        }
                    }
                    else
                    {
                        //T#615803
                        DlkLogger.LogInfo("Opening dropdown");
                        mElement.Click();
                        bIsOpenedHere = true;
                        foreach (IWebElement elm in mElement.FindElements(By.XPath("./following-sibling::div[1]/descendant::li")))
                        {
                            DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                            actual += DlkString.ReplaceCarriageReturn(ctl.GetValue(), "") + "~";
                        }
                        actual = actual.Trim('~');

                        DlkAssert.AssertEqual("VerifyList()", expected, actual);
                        if (bIsOpenedHere)
                        {
                            DlkLogger.LogInfo("Closing dropdown");
                            mElement.Click();
                        }
                    }
                }
                else if (mElement.FindElements(By.XPath("./button[contains(@class,'dropdown-toggle')]")).Count == 1)
                {
                    var items = mElement.FindElements(By.XPath("./div[contains(@class,'dropdown-menu')]//a"));
                    foreach (var item in items)
                    {
                        DlkBaseControl ctl = new DlkBaseControl("Item", item);
                        actual += DlkString.ReplaceCarriageReturn(ctl.GetValue(), "") + "~";
                    }
                    actual = actual.Trim('~');
                    DlkAssert.AssertEqual("VerifyList()", expected, actual);
                }
                else
                {
                    if (!mElement.FindElement(By.XPath("./following-sibling::div[1]")).Displayed)
                    {
                        DlkLogger.LogInfo("Opening dropdown");
                        mElement.Click();
                        bIsOpenedHere = true;
                    }

                    mDropDownButton = mElement.FindElement(By.XPath("./parent::*[1]//a[1]"));
                    mDropDownButton.Click();

                    //click the dropdown, then iterate over the elements.
                    foreach (IWebElement elm in mDropDownButton.FindElements(By.XPath("./following-sibling::div[1]/descendant::a/div")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                        actual += DlkString.ReplaceCarriageReturn(ctl.GetValue(), "") + "~";
                    }
                    actual = actual.Trim('~');
                    DlkAssert.AssertEqual("VerifyList()", expected, actual);
                    if (bIsOpenedHere)
                    {
                        DlkLogger.LogInfo("Closing dropdown");
                        mElement.Click();
                    }
                }
                DlkLogger.LogInfo("VerifyList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyItemInList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemInList(string Item, string TrueOrFalse)
        {
            try
            {
                Initialize();
                bool bIsOpenedHere = false;
                bool actual = false;
                bool expected = bool.Parse(TrueOrFalse);

                if (mElement.GetAttribute("class").Contains("form-control"))
                {
                    mDropDownButton = mElement.FindElement(By.XPath("./parent::*[1]//a[1]"));
                    DlkLogger.LogInfo("Opening dropdown");
                    mDropDownButton.Click();
                    bIsOpenedHere = true;
                    Thread.Sleep(1000);

                    foreach (IWebElement elm in mElement.FindElements(By.XPath("./parent::div[1]/parent::div[1]/descendant::div[@class='result-wrapper']/descendant::a/div[text()]")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                        if (DlkString.ReplaceCarriageReturn(ctl.GetValue(), "") == DlkString.ReplaceCarriageReturn(Item, ""))
                        {
                            actual = true;
                            break;
                        }
                    }
                    DlkAssert.AssertEqual("VerifyItemInList()", expected, actual);
                    if (bIsOpenedHere)
                    {
                        DlkLogger.LogInfo("Closing dropdown");
                        mDropDownButton.Click();
                    }
                }
                else if (mElement.GetAttribute("class").Contains("group-btn"))
                {
                    mDropDownButton = mElement.FindElement(By.XPath("./button[1]"));
                    DlkLogger.LogInfo("Opening dropdown");
                    mDropDownButton.Click();
                    bIsOpenedHere = true;
                    Thread.Sleep(1000);

                    foreach (IWebElement elm in mElement.FindElements(By.XPath(".//li[1]//a")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                        if (DlkString.ReplaceCarriageReturn(ctl.GetValue(), "") == DlkString.ReplaceCarriageReturn(Item, ""))
                        {
                            actual = true;
                            break;
                        }
                    }
                    DlkAssert.AssertEqual("VerifyItemInList()", expected, actual);
                    if (bIsOpenedHere)
                    {
                        DlkLogger.LogInfo("Closing dropdown");
                        mDropDownButton.Click();
                    }
                }
                else if (mElement.FindElements(By.XPath("./button[contains(@class,'dropdown-toggle')]")).Count == 1)
                {
                    var items = mElement.FindElements(By.XPath("./div[contains(@class,'dropdown-menu')]//a"));
                    actual = items.Any(x => DlkString.ReplaceCarriageReturn(new DlkBaseControl("Item", x).GetValue(), "") == DlkString.ReplaceCarriageReturn(Item, ""));
                    DlkAssert.AssertEqual("VerifyList()", expected, actual);
                }
                else
                {
                    if (!mElement.FindElement(By.XPath("./following-sibling::div[1]")).Displayed)
                    {
                        DlkLogger.LogInfo("Opening dropdown");
                        mElement.Click();
                        bIsOpenedHere = true;
                    }

                    foreach (IWebElement elm in mElement.FindElements(By.XPath("./following-sibling::div[1]/descendant::a/span[1]")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                        if (DlkString.ReplaceCarriageReturn(ctl.GetValue(), "") == DlkString.ReplaceCarriageReturn(Item, ""))
                        {
                            actual = true;
                            break;
                        }
                    }
                    DlkAssert.AssertEqual("VerifyItemInList()", expected, actual);
                    if (bIsOpenedHere)
                    {
                        DlkLogger.LogInfo("Closing dropdown");
                        mElement.Click();
                    }
                }
                DlkLogger.LogInfo("VerifyItemInList() passed");

            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCount(string Count)
        {
            try
            {
                Initialize();
                int actual = 0;
                bool bIsOpenedHere = false;
                if (mElement.GetAttribute("class").Contains("form-control"))
                {
                    //click dropdown
                    mDropDownButton = mElement.FindElement(By.XPath("./parent::*[1]//a[1]"));
                    DlkLogger.LogInfo("Opening dropdown");
                    mDropDownButton.Click();
                    bIsOpenedHere = true;
                    Thread.Sleep(1000);
                    actual = mElement.FindElements(By.XPath("./parent::div[1]/parent::div[1]/descendant::div[@class='result-wrapper']/descendant::a/div[text()]")).Count;

                    if (bIsOpenedHere)
                    {
                        DlkLogger.LogInfo("Closing dropdown");
                        mDropDownButton.Click();
                    }
                }
                else if(mElement.GetAttribute("class").Contains("group"))
                {
                    //click dropdown
                    mDropDownButton = mElement.FindElement(By.XPath("./button[1]"));
                    DlkLogger.LogInfo("Opening dropdown");
                    mDropDownButton.Click();
                    bIsOpenedHere = true;
                    Thread.Sleep(1000);

                    actual = mElement.FindElements(By.XPath(".//li//a")).Count;

                    if (bIsOpenedHere)
                    {
                        DlkLogger.LogInfo("Closing dropdown");
                        mDropDownButton.Click();
                    }
                }
                else
                {
                    if (!mElement.FindElement(By.XPath("./following-sibling::div[1]")).Displayed)
                    {
                        //click dropdowm
                        DlkLogger.LogInfo("Opening dropdown");
                        mElement.Click();
                        bIsOpenedHere = true;
                    }
                    actual = mElement.FindElements(By.XPath("./following-sibling::div[1]/descendant::a/span[1]")).Count;

                    if (bIsOpenedHere)
                    {
                        DlkLogger.LogInfo("Closing dropdown");
                        mElement.Click();
                    }
                }
                int expected = int.Parse(Count);

                DlkAssert.AssertEqual("VerifyCount()", expected, actual);
                DlkLogger.LogInfo("VerifyCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
            DlkLogger.LogInfo("VerifyExists() passed");
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActValue = "";
                if (mElement.GetAttribute("class").Equals("form-control"))
                {
                    ActValue = new DlkBaseControl("Text", mElement.FindElement(By.XPath("./descendant::p[1]"))).GetValue();
                }
                else if (mElement.GetAttribute("class").Equals("form-control enum"))
                {
                    ActValue = mElement.GetAttribute("value");
                }
                else if (mElement.GetAttribute("class").Contains("group-btn"))
                {
                    ActValue = new DlkBaseControl("Text", mElement.FindElement(By.XPath("./button[1]"))).GetValue();
                }
                else
                {
                    ActValue = new DlkBaseControl("Text", mElement.FindElement(By.XPath(".//span[1]"))).GetValue();
                }
                DlkAssert.AssertEqual("VerifyText()", ExpectedValue, ActValue);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// This keyword will get the value of the placeholder attribute of a TextBox control.
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyPlaceholder", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyPlaceholder(String ExpectedValue)
        {
            //i considered this but it might have unintended results
            //if (ActValue=="")
            //{
            //    ActValue = base.GetValue();
            //}
            String ActValue = GetAttributeValue("placeholder");
            DlkAssert.AssertEqual("VerifyPlaceholder()", ExpectedValue, ActValue);
        }

    }
}
