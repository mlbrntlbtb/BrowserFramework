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
    public class DocumentContNavList: INavList
    {
        private IWebElement mElement;
        public DocumentContNavList(IWebElement mElement)
        {
            this.mElement = mElement;
        }

        public void VerifyPartList(string ExpectedPartList)
        {
            var partsInnerText = GetPartsInnerText();
            var ActualResult = String.Join("~", partsInnerText);

            DlkAssert.AssertEqual("VerifyPartList()", ActualResult, ExpectedPartList);
            DlkLogger.LogInfo("VerifyPartList() passed");
        }

        public void VerifyArticleList(string Part, string ExpectedArticleList)
        {
            var articlesInnerText = GetArticlesInnerText(Part);
            var ActualResult = String.Join("~", articlesInnerText);

            DlkAssert.AssertEqual("VerifyArticleList()", ActualResult, ExpectedArticleList);
            DlkLogger.LogInfo("VerifyArticleList() passed");
        }

        public void VerifyParagraphList(string Part, string Article, string ExpectedParagraphList)
        {
            var paragraphsInnerText = GetParagraphsInnerText(Part, Article);
            var ActualResult = String.Join("~", paragraphsInnerText);

            DlkAssert.AssertEqual("VerifyArticleList()", ActualResult, ExpectedParagraphList);
            DlkLogger.LogInfo("VerifyArticleList() passed");
        }

        public void SelectPartByIndexOrName(string Part)
        {
            var part = GetPartByIndexOrName(Part);
            part.Click();
            DlkLogger.LogInfo("SelectPartByIndexOrName() passed");
        }

        public void SelectArticleByIndexOrName(string Part, string Article)
        {
            var part = GetArticleByIndexOrName(Part, Article);
            part.Click();
            DlkLogger.LogInfo("SelectArticleByIndexOrName() passed");
        }

        public void SelectParagraphByIndexOrName(string Part, string Article, string Paragraph)
        {
            var part = GetParagraphByIndexOrName(Part, Article, Paragraph);
            part.Click();
            DlkLogger.LogInfo("SelectParagraphByIndexOrName() passed");
        }

        private List<string> GetPartsInnerText()
        {
            var parts = GetParts();
            var partsInnerText = parts.Select(p => p.Text.Replace(Environment.NewLine, " ")).ToList();
            return partsInnerText;
        }

        private List<IWebElement> GetParts()
        {
            var parts = mElement.FindElements(By.XPath("./button[contains(@class, 'part')]")).ToList();
            if (parts.Count < 1) throw new Exception("No parts found.");
            return parts;
        }

        private IWebElement GetPartByIndexOrName(string partIndexOrName)
        {
            var parts = GetParts();

            var isIndex = Int32.TryParse(partIndexOrName, out int index);
            IWebElement part;

            if (isIndex)
            {
                if (index < 1) throw new Exception($"Index [{index}] should be greater than 0.");
                part = parts.ElementAt(index-1);
            }
            else part = parts.FirstOrDefault(p => p.Text.Replace(Environment.NewLine, " ") == partIndexOrName);

            if (part == null) throw new Exception("No parts found.");

            return part;
        }
        private List<string> GetArticlesInnerText(string partIndexOrName)
        {
            var articles = GetArticles(partIndexOrName);
            var articlesInnerText = articles.Select(a => a.Text.Replace(Environment.NewLine, " ")).ToList();
            return articlesInnerText;
        }

        private List<IWebElement> GetArticles(string partIndexOrName)
        {
            var part = GetPartByIndexOrName(partIndexOrName);

            List<IWebElement> articles = new List<IWebElement>();

            var partFollowingSiblings = part.FindElements(By.XPath("./following-sibling::*[not(contains(@class, 'paragraph'))]")).ToList();
            if (partFollowingSiblings.Count < 1) throw new Exception("Part following siblings not found.");

            foreach (var pfs in partFollowingSiblings)
            {
                var pfsClass = pfs.GetAttribute("class").ToLower();
                if (pfsClass.Contains("article")) articles.Add(pfs);
                else if (pfsClass.Contains("part")) break;
            }

            if (articles.Count < 1) throw new Exception("No articles found.");

            return articles;
        }

        private IWebElement GetArticleByIndexOrName(string partIndexOrName, string articleIndexOrName)
        {
            var articles = GetArticles(partIndexOrName);
            var isIndex = Int32.TryParse(articleIndexOrName, out int index);
            IWebElement article;

            if (isIndex)
            {
                if (index < 1) throw new Exception($"Index [{index}] should be greater than 0.");
                article = articles.ElementAt(index - 1);
            }
            else article = articles.FirstOrDefault(a => a.Text.Replace(Environment.NewLine, " ") == articleIndexOrName);

            if (article == null) throw new Exception("No article found.");

            return article;
        }
        private List<string> GetParagraphsInnerText(string partIndexOrName, string articleIndexOrName)
        {
            var paragraphs = GetParagraphs(partIndexOrName, articleIndexOrName);
            var paragraphsInnerText = paragraphs.Select(a => a.Text.Replace(Environment.NewLine, " ")).ToList();
            return paragraphsInnerText;
        }
        
        private List<IWebElement> GetParagraphs(string partIndexOrName, string articleIndexOrName)
        {
            var article = GetArticleByIndexOrName(partIndexOrName, articleIndexOrName);

            List<IWebElement> paragraph = new List<IWebElement>();

            var articleFollowingSiblings = article.FindElements(By.XPath("./following-sibling::*")).ToList();
            if (articleFollowingSiblings.Count < 1) throw new Exception("Article following siblings not found.");

            foreach (var afs in articleFollowingSiblings)
            {
                var afsClass = afs.GetAttribute("class").ToLower();
                if (afsClass.Contains("paragraph")) paragraph.Add(afs);
                else if (afsClass.Contains("part") || afsClass.Contains("article")) break;
            }

            if (paragraph.Count < 1) throw new Exception("No paragraph found.");

            return paragraph;

        }

        private IWebElement GetParagraphByIndexOrName(string partIndexOrName, string articleIndexOrName, string paragraphIndexOrName)
        {
            var paragraphs = GetParagraphs(partIndexOrName, articleIndexOrName);
            var isIndex = Int32.TryParse(paragraphIndexOrName, out int index);
            IWebElement paragraph;

            if (isIndex)
            {
                if (index < 1) throw new Exception($"Index [{index}] should be greater than 0.");
                paragraph = paragraphs.ElementAt(index - 1);
            }
            else paragraph = paragraphs.FirstOrDefault(a => a.Text.Replace(Environment.NewLine, " ") == paragraphIndexOrName);

            if (paragraph == null) throw new Exception("No paragraph found.");

            return paragraph;
        }
    }
}
