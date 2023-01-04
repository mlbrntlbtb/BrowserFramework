using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBCLib.DlkControls.Contract
{
    interface INavList
    {
        void VerifyPartList(String ExpectedPartList);
        void VerifyArticleList(String Part, String ExpectedArticleList);
        void VerifyParagraphList(String Part, String Article, String ExpectedParagraphList);
        void SelectPartByIndexOrName(String Part);
        void SelectArticleByIndexOrName(String Part, String Article);
        void SelectParagraphByIndexOrName(String Part, String Article, String Paragraph);
    }
}
