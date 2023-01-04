using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using SBCLib.DlkControls.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBCLib.DlkControls.Concrete.NavList
{
    public class DefaultNavList : INavList
    {
        private IWebElement mElement;

        private const String PARTS_XPATH = ".//div[@class='part_nav']";
        private const String ARTICLE_XPATH = ".//div[@class='article_nav']";
        private const String PARAGRAPH_XPATH = ".//ul//div//div//span";

        public DefaultNavList(IWebElement mElement)
        {
            this.mElement = mElement;
        }

        public void VerifyPartList(string ExpectedPartList)
        {
            var partList = mElement.FindElements(By.XPath(PARTS_XPATH))
                    .Select(part => {
                        var Part = new DlkBaseControl("Part", GetInnerTextContainer(part));
                        Part.ScrollIntoViewUsingJavaScript();   // Scroll & Click is needed to ensure we get the text of every element 
                        Part.Click();                           //
                        return Part.mElement.Text.Trim();
                    }).ToList();
            var ActualPartList = String.Join("~", partList);

            DlkAssert.AssertEqual("VerifyPartList()", ExpectedPartList.Trim(), ActualPartList.Trim());
            DlkLogger.LogInfo("VerifyPartList() passed");
        }
        public void VerifyArticleList(string Part, string ExpectedArticleList)
        {
            var articleList = GetPart(Part).FindElements(By.XPath(ARTICLE_XPATH)).Select(article => GetInnerTextContainer(article).Text.Trim()).ToList();
            var ActualArticleList = String.Join("~", articleList);

            DlkAssert.AssertEqual("VerifyArticleList()", ExpectedArticleList.Trim(), ActualArticleList.Trim());
            DlkLogger.LogInfo("VerifyArticleList() passed");
        }

        public void VerifyParagraphList(string Part, string Article, string ExpectedParagraphList)
        {
            var paragraphList = GetArticle(Part, Article).FindElements(By.XPath(PARAGRAPH_XPATH)).Select(paragraph => paragraph.GetAttribute("title")).ToList();
            var ActualParagraphList = String.Join("~", paragraphList);

            DlkAssert.AssertEqual("VerifyParagraphList()", ExpectedParagraphList.Trim(), ActualParagraphList.Trim());
            DlkLogger.LogInfo("VerifyParagraphList() passed");
        }
        public void SelectPartByIndexOrName(string Part)
        {
            GetPart(Part).FindElement(By.XPath(".//div[@class='navEle']")).Click();
            DlkLogger.LogInfo("SelectPartByIndexOrName() passed");
        }

        public void SelectArticleByIndexOrName(string Part, string Article)
        {
            GetArticle(Part, Article).Click();
            DlkLogger.LogInfo("SelectArticleByIndexOrName() passed");
        }

        public void SelectParagraphByIndexOrName(string Part, string Article, string Paragraph)
        {
            GetParagraph(Part, Article, Paragraph).Click();
            DlkLogger.LogInfo("SelectParagraphByIndexOrName() passed");
        }

        private IWebElement GetPart(String Part)
        {
            try
            {
                var part = GetElement(PARTS_XPATH, Part);
                if (part == null) throw new Exception($"Cannot find Part : {Part}");
                var partExpanded = part.FindElements(By.XPath(".//a[contains(@class, 'parArt')]")).FirstOrDefault();
                if (!partExpanded.GetAttribute("class").Contains("expanded") && IsSelected(part))
                    partExpanded.Click();
                else if (!partExpanded.GetAttribute("class").Contains("expanded"))
                    new DlkBaseControl("partExpanded", partExpanded).DoubleClick();

                return part;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        private IWebElement GetArticle(String Part, String Article)
        {
            try
            {
                var part = String.IsNullOrEmpty(Part) ? null : GetPart(Part);

                var article = GetElement(ARTICLE_XPATH, Article, part);
                if (article == null) throw new Exception($"Cannot find Article : {Article}");
                var articleExpanded = article.FindElements(By.XPath(".//a[contains(@class, 'artPara')]")).FirstOrDefault();
                if (!articleExpanded.GetAttribute("class").Contains("expanded") && IsSelected(article))
                    articleExpanded.Click();
                else if (!articleExpanded.GetAttribute("class").Contains("expanded"))
                    new DlkBaseControl("articleExpanded", articleExpanded).DoubleClick();

                return article;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        private IWebElement GetParagraph(String Part, String Article, String Paragraph)
        {
            try
            {
                var article = GetArticle(Part, Article);

                var paragraph = GetElement(PARAGRAPH_XPATH, Paragraph, article);
                if (paragraph == null) throw new Exception($"Cannot find Paragraph : {Paragraph}");
                return paragraph;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        private IWebElement GetElement(String Xpath, String IndexOrName, IWebElement parentElement = null)
        {
            try
            {
                var elements = parentElement == null
                    ? mElement.FindElements(By.XPath(Xpath)).ToList()
                    : parentElement.FindElements(By.XPath(Xpath)).ToList();
                var isIndex = Int32.TryParse(IndexOrName, out int index);
                if (isIndex)
                {
                    if (index > elements.Count() && !(index < 1)) throw new Exception($"Invalid Index : {index}");
                    return elements[index - 1];

                }
                return elements.Where(el => { var sample = GetInnerTextContainer(el).Text.Trim() == IndexOrName.Trim(); return sample; }).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        private IWebElement GetInnerTextContainer(IWebElement element)
        {
            if (element.TagName == "span") return element;
            var Elem = new DlkBaseControl("element", element);
            Elem.ScrollIntoViewUsingJavaScript();   // Scroll & Click is needed to ensure we get the text of every Part & Article element 
            Elem.Click();                           //
            var part = element.FindElement(By.XPath("./div[contains(@class,'navEle')]"));
            return element.FindElement(By.XPath("./div[contains(@class,'navEle')]"));
        }
        private bool IsSelected(IWebElement element)
        {
            var xpath = element.TagName == "span" ? "./../../div[contains(@class, 'left_navigation')]" : "./div[contains(@class, 'left_navigation')]";
            var elem = element.FindElements(By.XPath(xpath)).FirstOrDefault();
            return elem != null && elem.GetAttribute("class").Contains("selected");
        }
    }
}
