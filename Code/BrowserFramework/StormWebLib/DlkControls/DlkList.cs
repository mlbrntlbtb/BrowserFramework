using System;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Collections.Generic;
using StormWebLib.System;
using System.Linq;
using CommonLib.DlkUtility;

namespace StormWebLib.DlkControls
{
    [ControlType("List")]
    public class DlkList : DlkBaseControl
    {
        public DlkList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
   
        public void Initialize()
        {
            DlkStormWebFunctionHandler.WaitScreenGetsReady();

            FindElement();
            this.ScrollIntoViewUsingJavaScript();            

            listSetupConfigurationTextType = mElement.FindElements(By.XPath(SETUP_CONFIGURATION_TEXT_XPATH));
            listInputType = mElement.FindElements(By.XPath(INPUT_XPATH));
            listGenericType = mElement.FindElements(By.XPath(GENERIC_XPATH));
            listProjectCardType = mElement.FindElements(By.XPath(PROJECT_CARD_XPATH));
            listDashpartType = mElement.FindElements(By.XPath(DASHPART_XPATH));

        }

        #region FIELDS
        //XPATHS for the different classes of the <li> tags of the list control type
        private const string SETUP_CONFIGURATION_TEXT_XPATH = "./descendant::li/div[contains(@class,'setup-configuration-text')]";
        private const string INPUT_XPATH = "./descendant::input[not(contains(@type,'hidden'))]";
        private const string GENERIC_XPATH = "./descendant::li";
        private const string PROJECT_CARD_XPATH = "./div[contains(@class,'project-card')]";
        private const string DASHPART_XPATH = ".//div[contains(@class,'dashpart-container')]";


        IReadOnlyCollection<IWebElement> listSetupConfigurationTextType;
        IReadOnlyCollection<IWebElement> listInputType;
        IReadOnlyCollection<IWebElement> listGenericType;
        IReadOnlyCollection<IWebElement> listProjectCardType;
        IReadOnlyCollection<IWebElement> listDashpartType;

        #endregion
        [Keyword("AddItem", new String[] { "1|text|Item|NewItemToAdd" })]
        public void AddItem(String Item)
        {
            try
            {
                Initialize();

                IWebElement plusSign = mElement.FindElement(By.XPath("./descendant::span[text()='+']"));
                // Click plus sign
                new DlkBaseControl("AddButton", plusSign).Click();
                Thread.Sleep(3000); // 3 sec delay in between click and setting of value
                IWebElement selectedField = mElement.FindElement(By.XPath("./descendant::input[@class='org-level-item-name']"));
                selectedField.SendKeys(Item);
                selectedField.SendKeys(Keys.Tab);
                DlkLogger.LogInfo("AddItem() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("AddItem() failed : " + e.Message, e);
            }
        }

        [Keyword("AssignValueToVariable", new String[] { "1|text|Index|1",
                                            "2|text|VariableName|Sample"})]
        public void AssignValueToVariable(String Index, String VariableName)
        {
            try
            {
                Initialize();
                string xpath_input = ".//li[" + Index + "]/input[1]";
                string xpath_div = ".//li[" + (Convert.ToInt32(Index) + 1).ToString() +"]/*[1]";
                IWebElement target;
                if (mElement.FindElements(By.XPath(xpath_input)).Count > 0)
                {
                    target = mElement.FindElement(By.XPath(xpath_input));
                }
                else
                {
                    target = mElement.FindElement(By.XPath(xpath_div));
                }
                    
                DlkFunctionHandler.AssignToVariable(VariableName, new DlkBaseControl("Target", target).GetValue());
                DlkLogger.LogInfo("AssignValueToVariable() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("AssignValueToVariable() failed : " + e.Message, e);
            }
        }

        [Keyword("AssignIndexToVariable", new String[] { "1|text|Value|ItenValue",
                                                         "2|text|VariableName|Sample"})]
        public void AssignIndexToVariable(String Value, String VariableName)
        {
            try
            {
                Initialize();
                for (int idx = 0; idx < mElement.FindElements(By.XPath(INPUT_XPATH)).Count; idx++)
                {
                    DlkBaseControl ctl = new DlkBaseControl("element", mElement.FindElements(By.XPath(INPUT_XPATH))[idx]);
                    if (ctl.GetValue() == Value)
                    {
                        DlkFunctionHandler.AssignToVariable(VariableName, idx.ToString());
                        DlkLogger.LogInfo("AssignIndexToVariable() successfully executed.");
                        return;
                    }
                }
                throw new Exception("Item not found in list.");
            }
            catch (Exception e)
            {
                throw new Exception("AssignIndexToVariable() failed : " + e.Message, e);
            }
        }

        [Keyword("EditItem", new String[] { "1|text|OldItemValue|OldValueOfItem",
                                            "2|text|NewItemValue|NewValueofItem"})]
        public void EditItem(String OldItemValue, String NewItemValue)
        {
            try
            {
                Initialize();

                foreach (IWebElement elm in mElement.FindElements(By.XPath(INPUT_XPATH)))
                {
                    DlkBaseControl ctl = new DlkBaseControl("element", elm);
                    if (ctl.GetValue() == OldItemValue)
                    {
                        ctl.ScrollIntoViewUsingJavaScript();
                        elm.Click();
                        elm.Clear();
                        elm.SendKeys(NewItemValue);
                        elm.SendKeys(Keys.Tab);
                        DlkLogger.LogInfo("EditItem() successfully executed.");
                        return;
                    }
                }
                //if that didn't work, try this
                foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::span")))
                {
                    DlkBaseControl ctl = new DlkBaseControl("element", elm);
                    if (ctl.GetValue() == OldItemValue)
                    {
                        ctl.ScrollIntoViewUsingJavaScript();
                        elm.Click();
                        IWebElement input = mElement.FindElement(By.XPath(INPUT_XPATH));
                        input.Clear();
                        input.SendKeys(NewItemValue);
                        input.SendKeys(Keys.Tab);
                        DlkLogger.LogInfo("EditItem() successfully executed.");
                        return;
                    }
                }                
                throw new Exception("Item not found in list.");
            }
            catch (Exception e)
            {
                throw new Exception("EditItem() failed : " + e.Message, e);
            }
        }

        [Keyword("SetItem", new String[] { "1|text|OldItemValue|OldValueOfItem"})]
        public void SetItem(String NewItemValue)
        {
            try
            {
                Initialize();
                foreach (IWebElement elm in mElement.FindElements(By.TagName("li")))
                {
                    if (elm.GetAttribute("data-row-disposition") == "new")
                    {
                        IWebElement mInput = elm.FindElement(By.XPath(INPUT_XPATH));
                        mInput.SendKeys(NewItemValue);
                        mInput.SendKeys(Keys.Enter);                     
                        DlkLogger.LogInfo("SetItem() successfully executed.");
                        return;
                     }                       
                    }
                }                          
               
            catch (Exception e)
            {
                throw new Exception("SetItem() failed : " + e.Message, e);
            }
        }

        [Keyword("EditItemAtIndex", new String[] { "1|text|Index|1",
                                                   "2|text|NewValue|NewValueofItem"})]
        public void EditItemAtIndex(String Index, String NewValue)
        {
            try
            {
                Initialize();
                IWebElement target = mElement.FindElement(By.XPath("./ul[1]/li[" + Index + "]/input[1]"));
                new DlkBaseControl("TargetItem", target).ScrollIntoViewUsingJavaScript();
                target.Click(); // not clicking causes validation error Is this a bug in product?
                target.Clear();
                target.SendKeys(NewValue);
                target.SendKeys(Keys.Tab);
                DlkLogger.LogInfo("EditItem() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("EditItemAtIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("DeleteItem", new String[] { "1|text|Value|ItemToDelete" })]
        public void DeleteItem(String Value)
        {
            try
            {
                Initialize();
                // needed to loop since value is not visible in DOM for newly added items
                foreach (IWebElement elm in mElement.FindElements(By.XPath(INPUT_XPATH)))
                {
                    DlkBaseControl ctl = new DlkBaseControl("element", elm);
                    if (ctl.GetValue() == Value)
                    {
                        ctl.Click();
                        IWebElement target = ctl.mElement.FindElement(By.XPath("./following-sibling::div[1]"));
                        target.Click();
                        DlkLogger.LogInfo("DeleteItem() successfully executed.");
                        return;
                    }
                }
                //for deleting items in dashpart list                
             foreach (IWebElement elm in mElement.FindElements(By.XPath(DASHPART_XPATH)))
                {
                    DlkBaseControl ctl = new DlkBaseControl("element", elm);
                    if (DlkString.RemoveCarriageReturn(ctl.GetValue().Trim()) == Value)
                    {
                        ctl.MouseOver();
                        IWebElement target = ctl.mElement.FindElement(By.XPath(".//div[@class='delete-button']"));
                        target.Click();
                        DlkLogger.LogInfo("DeleteItem() successfully executed.");
                        return;
                    }
                }
                //if that didn't work, try this
              //  foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::span")))
                    foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::div")))
                {
                    DlkBaseControl ctl = new DlkBaseControl("element", elm);
                    if (ctl.GetValue() == Value)
                    {
                        ctl.Click();
                        IWebElement target = ctl.mElement.FindElement(By.XPath("./following-sibling::span[2]"));
                        target.Click();
                        DlkLogger.LogInfo("DeleteItem() successfully executed.");
                        return;
                    }
                }
                    //if that didn't work, try this                   
                    foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::div")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("element", elm);
                        if (ctl.GetValue() == Value)
                        {
                            ctl.MouseOver();
                            IWebElement target = ctl.mElement.FindElement(By.XPath("./following-sibling::span[2]"));
                            target.Click();
                            DlkLogger.LogInfo("DeleteItem() successfully executed.");
                            return;
                        }
                    }
                throw new Exception("Item not found in list.");
            }
            catch (Exception e)
            {
                throw new Exception("DeleteItem() failed : " + e.Message, e);
            }
        }

        [Keyword("DeleteItemAtIndex", new String[] { "1|text|Index|IndexOfItemToDelete" })]
        public void DeleteItemAtIndex(String Index)
        {
            try
            {
                Initialize();
                IWebElement inputField = mElement.FindElement(By.XPath("./ul[1]/li[" + Index + "]"));
                new DlkBaseControl("TargetItem", inputField).Click();
                IWebElement target = mElement.FindElement(By.XPath("./ul[1]/li[" + Index + "]/div[1]"));
                target.Click();
                DlkLogger.LogInfo("DeleteItemAtIndex() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("DeleteItemAtIndex() failed : " + e.Message, e);
            }
        }

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

        [Keyword("VerifyAvailableInList", new String[] { "1|text|Item|ItemToFind",
                                                         "2|text|ExpectedValue|TRUE"})]
        public void VerifyAvailableInList(String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                // needed to loop since value is not visible in DOM for newly added items
                bool actual = false;
                // allows reusability of code between if else's

                Item = DlkString.RemoveCarriageReturn(Item.Trim());
                Action<IReadOnlyCollection<IWebElement>> Verify = (collection) =>
                {
                    foreach (IWebElement elm in collection)
                    {
                        DlkBaseControl ctl = new DlkBaseControl("element", elm);
                        string val = DlkString.RemoveCarriageReturn(ctl.GetValue().Trim());
                        if (val.Trim() == Item.Trim())
                        {
                            actual = true;
                            break;
                        }
                    }
                };
                if (listSetupConfigurationTextType.Count > 0)
                {
                    Verify(listSetupConfigurationTextType);
                }
                else if (listInputType.Count > 0)
                {
                    Verify(listInputType);
                }
                else if (listGenericType.Count > 0)
                {
                    Verify(listGenericType);
                }
                else if (listProjectCardType.Count > 0)
                {
                    Verify(listProjectCardType);
                }
                else if (listDashpartType.Count > 0)
                {
                    Verify(listDashpartType);
                }
                DlkAssert.AssertEqual("VerifyAvailableInList()", bool.Parse(TrueOrFalse), actual);
                DlkLogger.LogInfo("VerifyAvailableInList() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAvailableInList() failed : " + e.Message, e);
            }


        }

        [Keyword("VerifyList", new String[] { "1|text|Items|Item1~Item2~Item3" })]
        public void VerifyList(String Items)
        {
            try
            {
                Initialize();
                string actual = "";


                //to allow reuse of code between if else's
                Action<IReadOnlyCollection<IWebElement>> Verify = (collection) =>
                {
                    foreach (IWebElement elm in collection)
                    {
                        DlkBaseControl ctl = new DlkBaseControl("element", elm);
                        string val = DlkString.RemoveCarriageReturn(ctl.GetValue().Trim());
                        if (!String.IsNullOrWhiteSpace(val.Trim()))
                        {
                            actual += val + "~";
                        }
                    }
                };
         
                // check if the type of the list because there are lists with different classes or tags
                if (listSetupConfigurationTextType.Count > 0)
                {
                    Verify(listSetupConfigurationTextType);
                }
                else if (listDashpartType.Count > 0)
                {
                    Verify(listDashpartType);
                }
                else if (listInputType.Count > 0)
                {
                    Verify(listInputType);
                }
                else if (listGenericType.Count > 0)
                {
                    Verify(listGenericType);
                }
                else if (listProjectCardType.Count > 0)
                {
                    Verify(listProjectCardType);
                }
                
                DlkAssert.AssertEqual("VerifyList()", Items, actual.Trim('~'));
                DlkLogger.LogInfo("VerifyList() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyValueAtIndex", new String[] { "1|text|Items|Item1~Item2~Item3" })]
        public void VerifyValueAtIndex(String Index, String Value)
        {
            try
            {
                Initialize();
                int index = 0;
                if (!Int32.TryParse(Index, out index))
                    throw new FormatException("VerifyValueAtIndex(): Invalid index entered [" + Index + "]");

                IWebElement inputField;
                if (mElement.FindElements(By.XPath("./ul[1]/li[" + Index + "]/input[1]")).Count > 0)
                    inputField = mElement.FindElement(By.XPath("./ul[1]/li[" + Index + "]/input[1]"));
                else if (listDashpartType.Count > 0)
                    inputField = listDashpartType.ElementAt(index - 1);
                else
                    inputField = listProjectCardType.ElementAt(index - 1);


                DlkAssert.AssertEqual("VerifyValueAtIndex()", DlkString.RemoveCarriageReturn(Value), DlkString.RemoveCarriageReturn(new DlkBaseControl("TargetItem", inputField).GetValue()));
                DlkLogger.LogInfo("VerifyValueAtIndex() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValueAtIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemCount", new String[] { "1|text|Count|10" })]
        public void VerifyItemCount(String Count)
        {
            try
            {
                Initialize();
                int actualCount = mElement.FindElements(By.XPath("./ul[1]/li")).Count - 1;
                if(actualCount < 0)
                {
                    actualCount = 0;
                }
                DlkAssert.AssertEqual("VerifyItemCount()", int.Parse(Count), actualCount);
                DlkLogger.LogInfo("VerifyItemCount() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyListIcon", new String[] { "1|text|Item|ItemToCheck",
                                                  "2|text|Icon|ExpectedIcon"})]
        public void VerifyListIcon(String Item, String Icon)
        {
            try
            {
                Initialize();

                VerifyAvailableInList(Item, "TRUE");

                IWebElement mIcon = null;
                foreach (IWebElement listItem in listGenericType)
                {
                    if (Item == CommonLib.DlkUtility.DlkString.NormalizeNonBreakingSpace(listItem.FindElement(By.XPath("./div")).Text))
                    {
                        mIcon = listItem.FindElement(By.XPath("./div/following-sibling::div/span"));
                        break;
                    }
                }

                String actualIconClass = mIcon.GetAttribute("class");
                String actualIcon = string.Empty;

                if (actualIconClass.Contains("default"))
                {
                    actualIcon = "default";
                }
                else if (actualIconClass.Contains("check"))
                {
                    actualIcon = "checked";
                }
                else
                {
                    actualIcon = "none";
                }

                DlkAssert.AssertEqual("VerifyListIcon()", Icon.ToLower(), actualIcon.ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyListIcon() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickInfoButton", new String[] { "1|text|ItemName|Value" })]
        public void ClickInfoButton(String ItemName)
        {
            try
            {
                Initialize();
                bool bFound = false;
                DlkBaseControl infoButton = null;
                foreach (IWebElement listItem in listProjectCardType)
                {
                    if (ItemName == CommonLib.DlkUtility.DlkString.NormalizeNonBreakingSpace(listItem.FindElement(By.XPath("./div[@class='project-name']")).Text))
                    {
                        bFound = true;
                        if (listItem.FindElements(By.XPath(".//span[contains(@class,'icon-Infobutton')]")).Count > 0)
                        {
                            infoButton = new DlkBaseControl("infoButton", listItem.FindElement(By.XPath(".//span[contains(@class,'icon-Infobutton')]")));

                            infoButton.MouseOver();
                            infoButton.Click();

                            DlkLogger.LogInfo("Successfully executed ClickInfoButton()");
                        }
                        else
                        {
                            throw new Exception("ClickInfoButton() failed. No InfoButton found at [" + ItemName + "].");
                        }
                    }
                }

                //for dashpart list
                foreach (IWebElement listItem in listDashpartType)
                {
                    if (ItemName == DlkString.RemoveCarriageReturn(new DlkBaseControl("element", listItem).GetValue().Trim()))
                    {
                        bFound = true;
                        if (listItem.FindElements(By.XPath(".//div[@class='dashpart-info-icon']")).Count > 0)
                        {
                            infoButton = new DlkBaseControl("infoButton", listItem.FindElement(By.XPath(".//div[@class='dashpart-info-icon']")));

                            infoButton.MouseOver();
                            infoButton.Click();

                            DlkLogger.LogInfo("Successfully executed ClickInfoButton()");
                        }
                        else
                        {
                            throw new Exception("ClickInfoButton() failed. No InfoButton found at [" + ItemName + "].");
                        }
                    }
                }

                if(!bFound)
                {
                    throw new Exception("ClickInfoButton() failed. [" + ItemName + "] was not found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickInfoButton() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyInfoButtonExistsAtIndex", new String[] { "1|text|Item|ItemToFind",
                                                         "2|text|ExpectedValue|TRUE"})]
        public void VerifyInfoButtonExistsAtIndex(String Index, String TrueOrFalse)
        {
            try
            {
                int index;
                bool actual = false, expected = false;

                if (!int.TryParse(Index, out index)) throw new Exception("Index must be a valid number.");
                if (!Boolean.TryParse(TrueOrFalse, out expected)) throw new Exception("Invalid value for TrueOrFalse");

                Initialize();

                //Having this ready in case other list types would need the keyword
                Action<IReadOnlyCollection<IWebElement>> VerifyInfoButton = (collection) =>
                {
                    IWebElement elem = collection.ElementAt(index - 1);
                    actual = elem.FindElements(By.XPath(".//span[contains(@class, 'Infobutton')]")).Where(x => x.Displayed).Count() > 0;
                };
                
                if (listProjectCardType.Count > 0)
                {
                    VerifyInfoButton(listProjectCardType);
                }
                else
                {
                    throw new Exception("List type is not yet supported.");
                }

                DlkAssert.AssertEqual("VerifyInfoButtonExists()", expected, actual);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyInfoButtonExistsAtIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemImage", new String[] { "1|text|Item|ItemToFind",
                                                   "2|text|ExpectedImage|Table"})]
        public void VerifyItemImage(String Index, String ExpectedImage)
        {
            try
            {
                int index;
                string actualImage = string.Empty;
                if (!int.TryParse(Index, out index)) throw new Exception("Index must be a valid number.");

                Initialize();
                
                switch(ExpectedImage)
                {
                    case "System":
                        ExpectedImage = "system-dashpart-icon.png";
                        break;
                    case "Table":
                        ExpectedImage = "dashpartTable.png";
                        break;
                    default:
                        DlkLogger.LogInfo("Unregistered image. Using the given ExpectedImage as title/source.");
                        break;
                }

                //Having this ready in case other list types would need the keyword
                Action<IReadOnlyCollection<IWebElement>> GetImage = (collection) =>
                {
                    IWebElement elem = collection.ElementAt(index - 1);
                    if(elem.FindElements(By.XPath(".//img[contains(@class, 'icon')]")).Where(x => x.Displayed).Count() > 0)
                    {
                        IWebElement mIcon = elem.FindElement(By.XPath(".//img[contains(@class, 'icon')]"));
                        actualImage = !String.IsNullOrEmpty(mIcon.GetAttribute("title")) ? mIcon.GetAttribute("title") : mIcon.GetAttribute("src");
                    }
                };

                if (listDashpartType.Count > 0)
                {
                    GetImage(listDashpartType);
                }
                else
                {
                    throw new Exception("List type is not yet supported.");
                }

                DlkAssert.AssertEqual("VerifyItemImage()", ExpectedImage, actualImage, true);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemImage() failed : " + e.Message, e);
            }
        }
    }
}
