using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("DataTree")]
    public class DlkDataTree : DlkBaseControl
    {
        #region Declarations

        private const string CHECKBOX_FEATURES = @"./ancestor::h3/label[@class='node_checkbox']/input[contains(@id,'checkbox_features')] | ./preceding-sibling::input[@type='checkbox']";
        private const string GLOBAL_ICON = @"./ancestor::h3/span[contains(@class,'node_global')]/i[contains(@class,'globe')]";
        private const string MORE_OPTION_ICON = @"./ancestor::h3/span[@class='node_blank']//i[contains(@class,'toggle-down')]";
        private const string CHILDREN_FEATURES = @"./ancestor::div[@data-id]/div[@role='tabpanel']/div[@id and contains(@class,'nodetree')]/child::div[@data-id]/div[not(contains(@class,'nodetree'))]";
        
        #endregion

        #region Constructors

        public DlkDataTree(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
        }

        public DlkDataTree(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement)
        {
        }

        #endregion

        #region Keywords

        [Keyword("ClickByTitle")]
        public void ClickByTitle(string Title)
        {
            try
            {
                if (Title.Equals(DlkCommon.DlkCommonFunction.SKIP_TEXTBOX_SET))
                {
                    DlkLogger.LogInfo("Skipping step since the data parameter is blank.");
                }
                else
                {
                    initialize();
                    DlkBaseControl selectedControl = getControlByTitle(Title);

                    if (mControlName == "Privileges") // special case on Privileges data-tree
                    {
                        selectedControl.ClickUsingJavaScript(false);
                    }
                    else
                    {
                        selectedControl.ClickUsingJavaScript();
                    }

                    DlkLogger.LogInfo("ClickByTitle() successfully executed.");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("ClickByTitle() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("SetCheckBoxByTitle")]
        public void SetCheckBoxByTitle(string Title, string TrueOrFalse)
        {
            try
            {
                initialize();
                DlkBaseControl selectedControl = getControlByTitle(Title);
                IWebElement checkBoxFeatureElement = selectedControl.mElement.FindElement(By.XPath(CHECKBOX_FEATURES));
                DlkCheckBox checkBoxFeatureControl = new DlkCheckBox(Title, checkBoxFeatureElement);
                checkBoxFeatureControl.Set(TrueOrFalse);
                DlkLogger.LogInfo("SetCheckBoxByTitle() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("SetCheckBoxByTitle() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("SetCheckBoxByTitleAndIndex")]
        public void SetCheckBoxByTitleAndIndex(string Title, string Index, string TrueOrFalse)
        {
            try
            {
                if (Title.Equals(DlkCommon.DlkCommonFunction.SKIP_TEXTBOX_SET))
                {
                    DlkLogger.LogInfo("Skipping step since the data parameter is blank.");
                }
                else
                {
                    initialize();
                    DlkBaseControl selectedControl = getControlByTitle(Title, Convert.ToInt32(Index));
                    IWebElement checkBoxFeatureElement = selectedControl.mElement.FindElement(By.XPath(CHECKBOX_FEATURES));
                    DlkCheckBox checkBoxFeatureControl = new DlkCheckBox(Title, checkBoxFeatureElement);
                    checkBoxFeatureControl.Set(TrueOrFalse);
                    DlkLogger.LogInfo("SetCheckBoxByTitleAndIndex() successfully executed.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SetCheckBoxByTitleAndIndex() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyCheckBoxByTitleAndIndex")]
        public void VerifyCheckBoxByTitleAndIndex(string Title, string Index, string TrueOrFalse)
        {
            try
            {
                initialize();
                DlkBaseControl selectedControl = getControlByTitle(Title, Convert.ToInt32(Index));
                IWebElement checkBoxFeatureElement = selectedControl.mElement.FindElement(By.XPath(CHECKBOX_FEATURES));
                DlkCheckBox checkBoxFeatureControl = new DlkCheckBox(Title, checkBoxFeatureElement);
                checkBoxFeatureControl.VerifyValue(TrueOrFalse);
                DlkLogger.LogInfo("VerifyCheckBoxByTitleAndIndex() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyCheckBoxByTitleAndIndex() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyGlobalFeatureByTitleAndIndex")]
        public void VerifyGlobalFeatureByTitleAndIndex(string Title, string Index, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool actualResult = false;
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                DlkBaseControl selectedControl = getControlByTitle(Title, Convert.ToInt32(Index));
                IList<IWebElement> globalIconElement = selectedControl.mElement.FindElements(By.XPath(GLOBAL_ICON));

                if (globalIconElement.Count > 0)
                {
                    actualResult = true;
                }

                DlkAssert.AssertEqual("Global_Icon_Exist", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyGlobalFeatureByTitleAndIndex() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyGlobalFeatureByTitleAndIndex() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyMoreOptionByTitleAndIndex")]
        public void VerifyMoreOptionByTitleAndIndex(string Title, string Index, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool actualResult = false;
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);

                IList<IWebElement> globalIconElement = new List<IWebElement>();
                DlkBaseControl selectedControl = getControlByTitle(Title, Convert.ToInt32(Index));
                if (selectedControl.mElement != null)
                {
                    globalIconElement = selectedControl.mElement.FindElements(By.XPath(MORE_OPTION_ICON));
                }

                if (globalIconElement.Count > 0)
                {
                    actualResult = true;
                }

                DlkAssert.AssertEqual("More_Option_Icon_Exist", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyMoreOptionByTitleAndIndex() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyMoreOptionByTitleAndIndex() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyItemExists")]
        public void VerifyItemExists(string Title, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool actualResult = false;
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                
                DlkBaseControl selectedControl = getControlByTitle(Title);
                if (selectedControl.mElement != null)
                {
                    actualResult = true;
                }

                DlkAssert.AssertEqual("VerifyItemExists", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyItemExists() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyItemExists() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifySortOrder")]
        public void VerifySortOrder(string ParentRoot, string SortOrder)
        {
            try
            {
                initialize();
                DlkBaseControl parent = getControlByTitle(ParentRoot);

                if (parent.mElement == null)
                {
                    throw new Exception("Unable to find Parent Root.");
                }

                var children = parent.mElement.FindElements(By.XPath(CHILDREN_FEATURES));
                List<string> sortedList = new List<string> { };
                if (SortOrder.ToLower().Contains("enabled") ||
                    SortOrder.ToLower().Contains("disabled"))
                {
                    sortedList = children.ToList<IWebElement>().ConvertAll<string>(child => 
                    {
                        string classAttr = child.GetAttribute("class");
                        if (classAttr.Contains("enabled"))
                        {
                            return "enabled";
                        }
                        else if (classAttr.Contains("disabled"))
                        {
                            return "disabled";
                        }
                        else
                        {
                            return classAttr;
                        }
                    });
                }
                else
                {
                    sortedList = children.ToList<IWebElement>().ConvertAll<string>(child => child.Text.Replace("Enable All\r\nDisable All\r\n",string.Empty));
                }

                var unsortedList = from name in sortedList
                                   orderby name ascending
                                   select name; 

                switch (SortOrder.ToLower())
                {
                    case "enabled":
                    case "descending" :
                    {
                            unsortedList = from name in sortedList
                                           orderby name descending
                                           select name;
                            break;
                    }
                    default: break;
                }

                if (!unsortedList.ToList<string>().SequenceEqual(sortedList))
                {
                    throw new Exception("Sorted list not equal");
                }

                List<string> newSortedList = new List<string>(sortedList);
                DlkLogger.LogInfo("VerifySortOrder() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifySortOrder() execution failed. : " + ex.Message, ex);
            }
        }

        #endregion

        #region Methods

        private DlkBaseControl getControlByTitle(string title, int index = 1)
        {
            IWebElement selectedElement = null;
            IList<IWebElement> elements = DlkCommon.DlkCommonFunction.GetElementWithText(title, mElement, true);
            int counter = 0;
            foreach (IWebElement element in elements)
            {
                DlkBaseControl control = new DlkBaseControl("control", element);
                string elementText = control.GetValue().Trim();
                if (element.Displayed &&
                    elementText.Equals(title))
                {
                    counter++;
                    if (counter == index)
                    {
                        selectedElement = element;
                        break;
                    }
                }
            }

            DlkBaseControl selectedControl = new DlkBaseControl(title, selectedElement);

            return selectedControl;
        }

        private void initialize()
        {
            FindElement();
        }

        #endregion
    }
}
