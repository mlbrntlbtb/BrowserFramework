using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFTLib.DlkSystem;
using SFTLib.DlkUtility;
using System.Threading;

namespace SFTLib.DlkControls
{
    [ControlType("TreeView")]
    public class DlkTreeView : DlkBaseControl
    {
        const int COUNTER = 0;
        const int SEARCH_MAX = 40;

        #region Constructors
        public DlkTreeView(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTreeView(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTreeView(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

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
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyFolderContentList", new String[] { "1|text|ParentFolder|Path1~Path2~Path3" })]
        public void VerifyFolderContentList(String parentFolder, String expectedContentList)
        {
            try
            {
                Initialize();
                VerifyTreeViewContent(parentFolder, expectedContentList);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyFolderContentList() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("Select", new String[] { "1|text|Path|Path1~Path2~Path3" })]
        public void Select(String path)
        {
            try
            {
                Initialize();
                SelectTreeViewItem(path);
            }
            catch (StaleElementReferenceException)
            {
                DlkLogger.LogInfo("Stale Element. Retrying to select.");
                if (COUNTER <= SEARCH_MAX)
                    Select(path);
                return;
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        #region Private Methods
        private void Initialize()
        {
            DlkSFTCommon.WaitForScreenToLoad();
            DlkSFTCommon.WaitForSpinner();
            FindElement();
            DlkEnvironment.mSwitchediFrame = true;
        }

        private void Terminate()
        {
            DlkEnvironment.mSwitchediFrame = false;
        }

        private IList<IWebElement> TreeViewItems()
        {
            return mElement.FindElements(By.XPath(".//*[@class='x-tree-node-text']")).ToList();
        }

        private void SelectTreeViewItem(string treeViewPath)
        {
            var arrPath = treeViewPath.Split('~').OfType<object>().Select((value, index) => new { value, index });
            var treeViewItems = TreeViewItems();
            //get initial number of items in treeview as reference
            int currentItemsOnView = treeViewItems.Count;

            foreach(var path in arrPath)
            {
                Thread.Sleep(500);
                var itemToSelect = treeViewItems.FirstOrDefault(x => x.Text == path.value.ToString());
                var tooltip = mElement.FindElements(By.XPath(".//ancestor::*[contains(@class, 'x-container')]//*[@id='ext-quicktips-tip']")).FirstOrDefault();

                if (tooltip != null)
                    tooltip.ExecJS("arguments[0].style.display = 'none';");

                if (itemToSelect != null)
                {
                    bool isLast = arrPath.Count() - 1 == path.index;
                    if (isLast)
                    {
                        IWebElement checkbox = itemToSelect.FindElements(By.XPath(".//preceding-sibling::input")).FirstOrDefault();
                        if (checkbox != null)
                            checkbox.Click();
                        else
                            new DlkBaseControl("Folder", itemToSelect).Click();//clicking once just highlights the last item in the view.
                    }
                    else
                    {
                        if (treeViewItems.Count == currentItemsOnView)
                            new DlkBaseControl("Folder", itemToSelect).DoubleClick();//expands the folder

                        Thread.Sleep(500);
                        DlkSFTCommon.WaitForScreenToLoad();
                        treeViewItems = TreeViewItems(); //instatiate new list to include the newly expanded items.
                    }
                }
                else
                    throw new Exception("Folder: [" + path.value + "] not found on treeview.");
            }

            Thread.Sleep(500);
        }
        
        private void VerifyTreeViewContent(string parentFolder, string expectedContent)
        {
            var treeViewItems = TreeViewItems();
            var expectedContentList = expectedContent.Split('~').ToList();
            List<string> actualContentList = new List<string>();

            if (treeViewItems.Count <= 0)
                throw new Exception("No items were found in the treeview.");

            //verify if expected items are shown on treeview
            if(treeViewItems.Any(path => expectedContentList.Any(text => text == path.Text)))
            {
                var index = treeViewItems.IndexOf(treeViewItems.FirstOrDefault(x => x.Text == parentFolder));

                foreach(var expectedValue in expectedContentList)
                {
                    index = index < (treeViewItems.Count - 1) ? index + 1 : index;

                    if(treeViewItems[index].Text == expectedValue)
                    {
                        actualContentList.Add(treeViewItems[index].Text);
                    }
                }

                string actualContent = string.Join("~", actualContentList);
                DlkAssert.AssertEqual("VerifyContent() : ", expectedContent, actualContent);
            }
            else
            {
                throw new Exception("Unable to find \"" + expectedContent + "\" under \"" + parentFolder + "\" in the treeview.");
            }
            
        }

        #region old implementation for Select/Verify treeview. do not delete
        //private void FolderExpandState(IWebElement element, bool toClose)
        //{
        //    if (toClose == element.FindElement(By.XPath(".//ancestor::tr")).GetAttribute("class").Contains("x-grid-tree-node-expanded"))
        //        new DlkBaseControl("Folder", element).DoubleClick();
        //}
        //private void SelectOrVerifyContentList(string parentPath, string expectedContentList = "")
        //{
        //    var arrPath = parentPath.Split('~').OfType<object>().Select((value, index) => new { value, index });
        //    foreach (var path in arrPath)
        //    {
        //        String tXPath = string.Format(mXPath, path.value);
        //        var element = mElement.FindElement(By.XPath(tXPath));
        //        var tooltip = mElement.FindElements(By.XPath(".//ancestor::*[contains(@class, 'x-container')]//*[@id='ext-quicktips-tip']")).FirstOrDefault();

        //        if (tooltip != null)
        //            tooltip.ExecJS("arguments[0].style.display = 'none';");

        //        if (element != null)
        //        {
        //            bool isLast = arrPath.Count() - 1 == path.index;
        //            if (isLast && !string.IsNullOrEmpty(expectedContentList))
        //            {
        //                //VerifyingContentList of the last item in the path
        //                VerifyContent(element, expectedContentList);
        //            }
        //            else if (isLast)
        //            {
        //                //Selecting the last item in the path
        //                IWebElement checkbox = element.FindElements(By.XPath(".//preceding-sibling::input")).FirstOrDefault();
        //                if (checkbox != null)
        //                    checkbox.Click();
        //                else
        //                    new DlkBaseControl("Folder", element).Click();
        //            }
        //            else
        //                FolderExpandState(element, false);
        //        }
        //        else
        //            throw new Exception("Folder: [" + path.value + "] could not be found");

        //        Thread.Sleep(1000);
        //    }
        //}

        //private void VerifyContent(IWebElement element, string expectedContentList)
        //{
        //    FolderExpandState(element, false);
        //    List<string> content = new List<string>();
        //    int currentTreeLevel = element.FindElements(By.XPath(".//parent::div//img")).Count();
        //    IWebElement fSibling = element.FindElements(By.XPath(".//ancestor::tr//following-sibling::tr")).FirstOrDefault();
        //    int fSiblingTreeLevel = fSibling.FindElements(By.XPath(".//img")).Count();
        //    while (fSiblingTreeLevel > currentTreeLevel)
        //    {
        //        if (fSiblingTreeLevel == currentTreeLevel + 1)
        //        {
        //            FolderExpandState(fSibling, true);
        //            content.Add(fSibling.Text);
        //        }
        //        fSibling = fSibling.FindElements(By.XPath(".//following-sibling::tr")).FirstOrDefault();
        //        fSiblingTreeLevel = fSibling != null ? fSibling.FindElements(By.XPath(".//img")).Count() : 0;
        //    }
        //    string actualContentList = string.Join("~", content);
        //    DlkAssert.AssertEqual("VerifyContent() : ", expectedContentList, actualContentList);
        //}
        #endregion
        #endregion
    }
}
