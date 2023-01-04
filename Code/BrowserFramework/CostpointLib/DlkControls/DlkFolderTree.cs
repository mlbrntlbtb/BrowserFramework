using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace CostpointLib.DlkControls
{
    [ControlType("FolderTree")]
    public class DlkFolderTree : DlkBaseControl
    {
        private Boolean IsInit = false;
        private List<String> mlstTreeNodes;
        private String mstrTreeNodesXPath = "//div[@class='drillDownPathLine']";

        public DlkFolderTree(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkFolderTree(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkFolderTree(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }

        }

        private void GetTreeNodes()
        {
            mlstTreeNodes = new List<String>();
            IList<IWebElement> mTreeNodes;

            mTreeNodes = mElement.FindElements(By.XPath(mstrTreeNodesXPath));

            if (mTreeNodes.Count == 0)
            {
                DlkLogger.LogInfo("Folder tree contains no nodes for selection.");
            }

            foreach (IWebElement mNode in mTreeNodes)
            {
                DlkBaseControl nodeControl = new DlkBaseControl("Message", mNode);
                DlkLogger.LogInfo(nodeControl.GetValue().Trim());
                mlstTreeNodes.Add(nodeControl.GetValue().Trim());
            }
        }


        [Keyword("VerifyBranchExists", new String[] { "1|text|Branch Text|Automation Charge", 
                                                        "2|text|Expected Branch Exists|True or False"})]
        public void VerifyBranchExists(String BranchText, String ExpectedExists)
        {
            Initialize();
            GetTreeNodes();

            Boolean bContains = mlstTreeNodes.Contains(BranchText);

            if (bContains == Convert.ToBoolean(ExpectedExists))
            {
                DlkLogger.LogInfo("VerifyBranchExists() passed : Actual = " + Convert.ToString(bContains) + " : Expected = " + ExpectedExists);
            }
            else
            {
                throw new Exception("VerifyBranchExists() failed : Actual = " + Convert.ToString(bContains) + " : Expected = " + ExpectedExists);
            }
        }

        /// <summary>
        /// Click node in folder tree view
        /// </summary>
        /// <param name="BranchText">Exact text of node</param>
        [Keyword("ClickFolderTreeBranch", new String[] { "1|text|Branch Text|Automation Charge"})]
        public void ClickFolderTreeBranch(string BranchText)
        {
            try
            {
                Initialize();
                IList<IWebElement> mTreeNodes = mElement.FindElements(By.XPath(mstrTreeNodesXPath));
                IWebElement element = mTreeNodes.FirstOrDefault(branch => branch.Text == BranchText);

                if (element != null)
                {
                    element.Click();
                    DlkLogger.LogInfo($"ClickFolderTreeBranch() passed : Clicked folder tree branch {BranchText}");
                }
                else
                {
                    throw new ArgumentException($"ClickFolderTreeBranch() failed : Node \"{BranchText}\" cannot be found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"ClickFolderTreeBranch() failed : {e.Message}", e);
            }
        }
    }
}
