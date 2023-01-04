using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkUtility;

namespace CostpointLib.DlkControls
{
    [ControlType("AppMenuView")]
    public class DlkAppMenuView : DlkBaseControl
    {
        #region Declaration
        private const int NODE_LEFT_OFFSET = 10;
        private readonly string mnuViewPath = "//div[@class='myMnuViewport']";
        private readonly string mnuViewClass = "wWindowApp,myMnuViewport";
        private readonly string mnuItemsClass = "wWindowPick,mnuItem";
        private bool IsInit = false;
        private IDictionary<IWebElement, IList<IWebElement>> mstrAppNodes;
        #endregion

        #region Constructor
        public DlkAppMenuView(string ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public DlkAppMenuView(string ControlName, string SearchType, string SearchValue)
            : base(ControlName, SearchType, SearchValue) { }

        public DlkAppMenuView(string ControlName, string SearchType, string[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public DlkAppMenuView(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector)
            : base(ControlName, ExistingParentWebElement, CSSSelector) { }

        public DlkAppMenuView(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        #endregion

        #region Initialize
        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }
        }
        #endregion

        #region Keyword
        [Keyword("VerifyExists", new string[] { "1|text|App Menu Path|Manage Parts~Manage Parts~Project Requirements",
                                                        "2|text|Expected Menu Exists|True or False" })]
        public void VerifyExists(string AppMenuPath, string ExpectedExists)
        {
            bool actualResult = false;
            try
            {
                Initialize();
                if (AppMenuExist())
                {
                    GetAppMenuNodes();
                    IWebElement element = GetExactElement(AppMenuPath);
                    actualResult = element.Text == AppMenuPath.Split('~').Last();
                }
                DlkAssert.AssertEqual("VerifyExists() : ", bool.Parse(ExpectedExists), actualResult);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Method
        /// <summary>
        /// Get exact element in node.
        /// Call GetAppMenuNodes before calling this method 
        /// </summary>
        /// <param name="Path">Manage Parts~Manage Parts</param>
        /// <returns></returns>
        private IWebElement GetExactElement(string Path)
        {
            int leftBefore = 0;
            string leftAfter;
            string[] arrPath = Path.Split('~');
            string splitGetFirst(string keyText) => keyText.Split(new string[] { "\r\n" }, StringSplitOptions.None)[0];
            KeyValuePair<IWebElement, IList<IWebElement>> exactNode = mstrAppNodes.SingleOrDefault(node => splitGetFirst(node.Key.Text) == arrPath[0]);

            if (exactNode.Key != null)
            {
                IList<IWebElement> matchedNodes = arrPath.Skip(1).Join(exactNode.Value, path => (path), val => (val.Text), (path, val) => new { val })
                                    .Select(element => element.val).ToList();

                if (exactNode.Value.Count() > 0 && matchedNodes.Count() > 0)
                {
                    foreach (IWebElement item in matchedNodes) //remove open duplicate app name
                    {
                        leftAfter = item.FindElements(By.TagName("span"))?.FirstOrDefault(f => !string.IsNullOrEmpty(f.GetAttribute("style")))?.GetAttribute("style").Replace("margin-left:", "").Replace("px;", "");
                        if (int.TryParse(leftAfter, out int aft))
                        {
                            if (leftBefore == 0 || aft - leftBefore == NODE_LEFT_OFFSET)
                                leftBefore = aft;
                            else
                                matchedNodes.Remove(item);
                        }
                        else if (leftAfter == null) //For My Menu 1 child olnly
                            return matchedNodes.First();
                        else
                            matchedNodes.Remove(item);
                    }

                    if (string.Join("~", arrPath.Skip(1)) == string.Join("~", matchedNodes.Take(arrPath.Length - 1).Select(e => e.Text)))
                        return matchedNodes.Last();
                }
                else
                {
                    if (Path == splitGetFirst(exactNode.Key.Text))
                        return exactNode.Key.FindElements(By.XPath($"/descendant::span[contains(text(),'{Path}')]")).FirstOrDefault();
                }
            }
            return null;
        }

        /// <summary>
        /// Get all nodes with key and store to mstrAppNodes
        /// </summary>
        /// <returns></returns>
        private void GetAppMenuNodes()
        {
            mstrAppNodes = new Dictionary<IWebElement, IList<IWebElement>>();
            string[] appMnuClass = mnuViewClass.Split(',');

            foreach (string menu in appMnuClass)
            {
                ICollection<IWebElement> listMenu = mElement.FindElements(By.ClassName(menu)).ToList();

                if (mElement != null && menu == "myMnuViewport")
                    listMenu = mElement.FindElements(By.XPath(mnuViewPath));

                if (listMenu.Count() > 0)
                {
                    foreach (IWebElement key in listMenu)
                    {
                        foreach (string mnuItem in mnuItemsClass.Split(','))
                        {
                            key.FindElements(By.ClassName("mnuGrPlusS")).FirstOrDefault()?.Click();
                            IList<IWebElement> items = key.FindElements(By.ClassName(mnuItem));
                            if (items.Count() > 0)
                            {
                                mstrAppNodes.Add(key, items);
                                break;
                            }
                        }
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Check if AppMenuView control exist
        /// </summary>
        /// <returns></returns>
        private bool AppMenuExist()
        {
            return mElement.FindElement(By.XPath(mnuViewPath)) != null;
        }
        #endregion
    }
}
