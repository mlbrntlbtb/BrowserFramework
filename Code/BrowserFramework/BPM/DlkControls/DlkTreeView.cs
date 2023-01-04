using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BPMLib.DlkControls
{
    [ControlType("TreeView")]
    public class DlkTreeView : DlkBaseControl
    {
        private readonly String mXPath = ".//span[contains(text(),'{0}')]/ancestor::*[@aria-level='{1}']";

        public DlkTreeView(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTreeView(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTreeView(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private void Initialize()
        {
            FindElement();
            DlkEnvironment.mSwitchediFrame = true;
        }

        private void Terminate()
        {
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkEnvironment.mSwitchediFrame = false;
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
            finally
            {
                Terminate();
            }
        }

        [Keyword("Select", new String[] { "1|text|Path|Path1~Path2~Path3" })]
        public void Select(String Path)
        {
            try
            {
                Initialize();
                String[] arrPath = Path.Split('~');
                int retryCount = 0;

                for (int i = 0; i < arrPath.Count(); i++)
                {
                    string tXPath = string.Format(mXPath, arrPath[i], i);

                    IWebElement element = GetTreeViewElement(i.ToString(), arrPath[i]);

                    while (element==null) //Checking for parent node, slow loading in IE
                    {
                        if (retryCount == 20)
                        {
                            throw new ArgumentException($"Treeview Control: {arrPath[i]} cannot be found.");
                        }
                        else
                        {
                            DlkLogger.LogInfo($"Treeview Control: Click {arrPath[i]} with retry count {retryCount}.");
                        }

                        Thread.Sleep(1000);
                        element = GetTreeViewElement(i.ToString(), arrPath[i]);
                        retryCount++;
                    }

                    int nextNode = i + 1;

                    if (nextNode <= (arrPath.Count() - 1))
                    {
                        var expectedNode = ClickNode(tXPath, nextNode.ToString(), arrPath[nextNode]);

                        while ((expectedNode?.Text ?? "").Contains(arrPath[nextNode]) == false)
                        {
                            expectedNode = ClickNode(tXPath, nextNode.ToString(), arrPath[nextNode], true);
                            Thread.Sleep(1000);
                        }
                    }
                    else
                    {
                        ClickNode(tXPath);
                    }
                }
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

        /// <summary>
        /// Clicknode for treeview
        /// </summary>
        /// <param name="TxPath">XPATH for selected node</param>
        /// <param name="TxNextIndex">Index for next expected node</param>
        /// <param name="TxNextCaption">Caption for next expected node</param>
        /// <param name="IsForceDoubleClick">Used for slow loading treeview</param>
        /// <returns></returns>
        private IWebElement ClickNode(string TxPath,string TxNextIndex = null,string TxNextCaption = null,bool IsForceDoubleClick = true)
        {
            DlkBaseControl currentNode = new DlkBaseControl("CurrentNode", "xpath", TxPath);

            if (!IsForceDoubleClick && currentNode.GetAttributeValue("aria-expanded") == "true")
            {
                currentNode.Click();
            }
            else
            {
                currentNode.DoubleClick();
            }

            Thread.Sleep(1000);

            return TxNextIndex == null ? null : GetTreeViewElement(TxNextIndex, TxNextCaption);
        }

        /// <summary>
        /// Get element from treeview
        /// </summary>
        /// <param name="AriaLevel">Treeview Aria Level</param>
        /// <param name="Caption">Treeview Caption</param>
        /// <returns></returns>
        private IWebElement GetTreeViewElement(string AriaLevel,string Caption)
        {
            try
            {
                IWebElement element = mElement.FindElements(By.TagName("a")).Where(w => w.GetAttribute("aria-level") == AriaLevel)
                    .FirstOrDefault(f => f.Text.Contains(Caption));

                return element;
            }
            catch
            {
                //set to null since retry clicking is set on main function
                return null;
            }
        }
    }
}
