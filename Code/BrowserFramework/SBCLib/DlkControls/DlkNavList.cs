using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using System.Linq;
using CommonLib.DlkUtility;
using OpenQA.Selenium.Interactions;
using System.Threading;
using SBCLib.DlkControls.Contract;
using SBCLib.DlkControls.Concrete.NavList;

namespace SBCLib.DlkControls
{
    [ControlType("NavList")]
    class DlkNavList : DlkBaseControl
    {
        #region Constructors
        public DlkNavList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkNavList(String ControlName, String SearchType, String[] SearchValues)
        : base(ControlName, SearchType, SearchValues) { }
        public DlkNavList(String ControlName, IWebElement ExistingWebElement)
        : base(ControlName, ExistingWebElement) { }
        #endregion

        private INavList navlist;

        public void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
            navlist = NavListTypeFactory();
        }

        /// <summary>
        /// Will determine what concrete class to use based on the class attribute of the mElement.
        /// </summary>
        /// <returns></returns>
        private INavList NavListTypeFactory()
        {
            var elementClass = mElement.GetAttribute("class").ToLower();
            var tagName = mElement.TagName.ToLower();

            if (tagName.Equals("ul") && !elementClass.Contains("document-cont"))
                return new DefaultNavList(mElement);
            else if (elementClass.Contains("document-cont"))
                return new DocumentContNavList(mElement);
            else
                throw new Exception("Grid type is not supported.");
        }

        /// <summary>
        /// Will verify if the element exist on the DOM.
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        #region Keywords
        [Keyword("VerifyExists")]
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

        /// <summary>
        /// Will verify all visible Part.
        /// </summary>
        /// <param name="ExpectedPartList">List of visible Part seperated by tilde (~).</param>
        [Keyword("VerifyPartList")]
        public void VerifyPartList(String ExpectedPartList)
        {
            try
            {
                Initialize();
                navlist.VerifyPartList(ExpectedPartList);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPartList() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Will verify all visible Article.
        /// </summary>
        /// <param name="Part">Index or Name of parent Part.</param>
        /// <param name="ExpectedArticleList">List of visible Article seperated by tilde (~).</param>
        [Keyword("VerifyArticleList")]
        public void VerifyArticleList(String Part, String ExpectedArticleList)
        {
            try
            {
                Initialize();
                navlist.VerifyArticleList(Part, ExpectedArticleList);           
            }
            catch (Exception e)
            {
                throw new Exception("VerifyArticleList() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Will verify all visible Paragraph.
        /// </summary>
        /// <param name="Part">Index or Name of parent Part.</param>
        /// <param name="Article">Index or Name of parent Article.</param>
        /// <param name="ExpectedParagraphList">List of visible Paragraph seperated by tilde (~).</param>
        [Keyword("VerifyParagraphList")]
        public void VerifyParagraphList(String Part, String Article, String ExpectedParagraphList)
        {
            try
            {
                Initialize();
                navlist.VerifyParagraphList(Part, Article, ExpectedParagraphList);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyParagraphList() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Will select specified Part.
        /// </summary>
        /// <param name="Part">Index or Name of the Part to be selected.</param>
        [Keyword("SelectPartByIndexOrName")]
        public void SelectPartByIndexOrName(String Part)
        {
            try
            {
                Initialize();
                navlist.SelectPartByIndexOrName(Part);
            }
            catch (Exception e)
            {
                throw new Exception("SelectPartByIndexOrName() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Will select specified Article.
        /// </summary>
        /// <param name="Part">Index or Name of the parent Part.</param>
        /// <param name="Article">Index or Name of the Article to be selected.</param>
        [Keyword("SelectArticleByIndexOrName")]
        public void SelectArticleByIndexOrName(String Part, String Article)
        {
            try
            {
                Initialize();
                navlist.SelectArticleByIndexOrName(Part, Article);
            }
            catch (Exception e)
            {
                throw new Exception("SelectArticleByIndexOrName() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Will select specified Paragraph.
        /// </summary>
        /// <param name="Part">Index or Name of the parent Part.</param>
        /// <param name="Article">Index or Name of the parent Article.</param>
        /// <param name="Paragraph">Index or Name of the Paragraph to be selected.</param>
        [Keyword("SelectParagraphByIndexOrName")]
        public void SelectParagraphByIndexOrName(String Part, String Article, String Paragraph)
        {
            try
            {
                Initialize();
                navlist.SelectParagraphByIndexOrName(Part, Article, Paragraph);
                
            }
            catch (Exception e)
            {
                throw new Exception("SelectParagraphByIndexOrName() failed : " + e.Message, e);
            }
        }
        #endregion
    }
}
