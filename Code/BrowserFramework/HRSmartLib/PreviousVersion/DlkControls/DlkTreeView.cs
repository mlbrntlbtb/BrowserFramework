using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.PreviousVersion.DlkControls
{
    [ControlType("TreeView")]
    public class DlkTreeView : DlkBaseControl
    {
        #region Declarations

        //CSS Selector for List - li
        private const string OPEN = "jstree-open";
        private const string CLOSED = "jstree-closed";
        private const string UNCOLLAPSEABLE = "jstree-leaf jstree-closed";
        private const string UNCOLLAPSEABLELASTROW = "jstree-leaf jstree-closed jstree-last";

        private const string EXPANDNODE = "./ins[@class='jstree-icon']";
        private const string SELECTNODE = "./a";

        #endregion

        #region Constructors

        public DlkTreeView(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
            initialize();
        }

        #endregion

        #region Properties
        #endregion

        #region Keywords

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

        [Keyword("SelectByTreeRow")]
        public void SelectByTreeRow(string Row)
        {
            try
            {
                string[] rows = Row.Split('~');

                IWebElement elementRow = mElement;
                int lastRow = rows.Length - 1;
                for (int i = 0; i < rows.Length; i++)
                {
                    string row = rows[i];
                    elementRow = getChildByRow(elementRow, Convert.ToInt32(row) - 1);
                    string elementRowClassAttr = elementRow.GetAttribute("class") == null ? string.Empty : elementRow.GetAttribute("class");
                    string rowAction = string.Empty;
                    IWebElement actionableElement = null;
                    if (lastRow.Equals(i))
                    {
                        //Select the treeview row.
                        actionableElement = elementRow.FindElement(By.XPath(SELECTNODE));
                        rowAction = string.Concat(" Selected : ", elementRow.Text);
                    }
                    else if (elementRowClassAttr.Equals(CLOSED))
                    {
                        //Expand the treeview row.
                        actionableElement = elementRow.FindElement(By.XPath(EXPANDNODE));
                        rowAction = string.Concat(" Expanded : ", elementRow.Text);
                    }

                    if (actionableElement == null)
                    {
                        switch (elementRowClassAttr)
                        {
                            case OPEN :
                                DlkLogger.LogInfo("Row " + row + " already expanded.");
                                break;
                            case UNCOLLAPSEABLE :
                            case UNCOLLAPSEABLELASTROW :
                                throw new Exception("Row " + row + " is uncollapseable.");
                        }
                    }
                    else
                    {
                        DlkBaseControl element = new DlkBaseControl("Treview Row Item", actionableElement);
                        element.Click();
                        DlkLogger.LogInfo("Row " + row + rowAction);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("SelectByTreeRow( ) execution failed. : " + ex.Message, ex);
            }
        }

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }

        private IWebElement getChildByRow(IWebElement parentElement, int row)
        {
            IList<IWebElement> children = parentElement.FindElements(By.XPath("./ul[1]/li"));
            if (row < children.Count)
            {
                return children[row];
            }
            else
            {
                throw new Exception("child row doesn't exists.");
            }
        }

        #endregion
    }
}
