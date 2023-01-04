using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HTMLBuilder
{
    public class HTML
    {
        #region ELEMENTS


        public static XElement Tag(string tagName, params object[] content) => new XElement(tagName, content);
        public static XElement Div(params object[] content) => new XElement("div", content);
        public static XElement H1(params object[] content) => new XElement("h1", content);
        public static XElement H2(params object[] content) => new XElement("h2", content);
        public static XElement H3(params object[] content) => new XElement("h3", content);
        public static XElement H4(params object[] content) => new XElement("h4", content);
        public static XElement H5(params object[] content) => new XElement("h5", content);
        public static XElement Span(params object[] content) => new XElement("span", content);
        public static XElement P(params object[] content) => new XElement("p", content);
        public static XElement Ul(params object[] content) => new XElement("ul", content);
        public static XElement Li(params object[] content) => new XElement("li", content);
        public static XElement A(string href, params object[] content) =>
            new XElement("a",
                Attr("href", href),
                content);
        public static XElement Btn(params object[] content) =>
            new XElement("button",
                Attr("type", "button"),
                content);

        #endregion

        #region Components

        public static XElement Modal(string id, params object[] content) =>
            Div(
                Class("modal fade"),
                ID(id),
                Attr("tabindex", "-1"),
                Attr("aria-labelledby", id + "Label"),
                Attr("aria-hidden", "true"),
                Div(
                    Class("modal-dialog modal-dialog-centered modal-dialog-scrollable"),
                    Div(
                        Class("modal-content"),
                        removeAttrClasses(content)
                    )
                )
            );

        public static XElement ModalHeader(params object[] content) =>
           Div(
                Class($"modal-header {getAttrClasses(content)}"),
                removeAttrClasses(content),
                Btn(
                    Class(UC.BTN_CLOSE),
                    Attr("data-bs-dismiss", "modal"),
                    Attr("aria-label", "Close")
                )
            );

        public static XElement ModalBody(params object[] content) => Div(Class("modal-body"), content);

        public static XElement ModalFooter() => ModalFooter(null);
        public static XElement ModalFooter(params object[] content) =>
            Div(
                Class($"modal-footer {getAttrClasses(content)}"),
                Btn(
                    Class(UC.BTN_SECONDARY),
                    Attr("data-bs-dismiss", "modal"),
                    "Close"
                ),
                removeAttrClasses(content)
            );

        public static XElement Modal(string title, string titleColor, string id, params object[] content) =>
            Modal(
                id,
                ModalHeader(H4(Class(titleColor), title)),
                ModalBody(content),
                ModalFooter()
            );
        public static XElement Modal(string title, string itemNumer, string titleColor, string id, params object[] content) =>
            Modal(
                id,
                ModalHeader(
                    H4($"#{itemNumer} ", Span(Class(titleColor), title))
                ),
                ModalBody(content),
                ModalFooter()
            );

        public static XElement CustomCard(string title, params object[] content) =>
            Div(
                Class("card", UC.SHADOW, UC.MY_5),
                Div(
                    Class("card-header", UC.SHADOW, UC.TEXT_WHITE),
                    Style("background-color: #0B5D99;"),
                    H3(Class(UC.MY_2), title)
                ),
                Div(Class("card-body", UC.PX_5, UC.PY_4), removeAttrClasses(content))
            );
        public static XElement InfoCard(string title, string content, string bgColor) =>
            Div(
                Class("card", UC.SHADOW, UC.BG_GRADIENT, bgColor),
                Div(
                    Class("card-body", UC.D_FLEX, UC.FLEX_COLUMN),
                    H1(Class(UC.TEXT_WHITE), content),
                    H5(Class(UC.MT_AUTO, UC.TEXT_WHITE), title)
                )
            );

        public static XElement Info(string title, string content, string id = null) =>
            id == null
            ? Div(
                Span(Class(UC.TEXT_SECONDARY, UC.MB_2), title),
                H5(Class(UC.MB_3), content)
            )

            : Div(
                Span(Class(UC.TEXT_SECONDARY, UC.D_BLOCK, UC.MB_2), title),
                HTML.Div(HTML.Class(UC.D_FLEX, UC.MB_4),
                    HTML.Div(HTML.Class(UC.M_0),
                         Btn(
                            Class("copy-btn", UC.BTN_PRIMARY, UC.MR_2),
                            Attr("target", id),
                            Style("margin-bottom: 8px"),
                            TooltipAttr("Copy to clipboard", false),
                            Tag("i", Class("far fa-copy"), Tag("span"))
                        )
                    ),
                    HTML.Div(HTML.Class(UC.M_0), HTML.Style("overflow-x: scroll"),
                         H4(
                            TooltipAttr("Copy to clipboard"),
                            ID(id),
                            Class("copy", UC.D_INLINE_BLOCK),
                            content
                        )
                    )
                )
            );

        public static XElement Table(params object[] content) =>
            Tag("Table", Class($"Table {getAttrClasses(content)}"), removeAttrClasses(content));

        public static XElement THead(params object[] content) => Tag("thead", content);
        public static XElement TBody(params object[] content) => Tag("tbody", content);
        public static XElement TH(params object[] content) => Tag("th", content);
        public static XElement TR(params object[] content) => Tag("tr", content);
        public static XElement TD(params object[] content) => Tag("td", content);

        public static XElement Container(params object[] content) => Div(Class($"container {getAttrClasses(content)}"), removeAttrClasses(content));
        public static XElement Row(params object[] content) => Div(Class($"row {getAttrClasses(content)}"), removeAttrClasses(content));
        public static XElement Col(string size, params object[] content) => Div(Class($"col-{size} {getAttrClasses(content)}"), removeAttrClasses(content));


        public static XElement TabLilnkContainer(params object[] content) =>
            Div(
                Class("nav", UC.D_FLEX, UC.FLEX_COLUMN, UC.FLEX_MD_ROW, getAttrClasses(content)),
                removeAttrClasses(content)
            );
        public static XElement TabLink(string id, bool active, params object[] content) =>
            A($"#{id}",
                Class("text-decoration-none", UC.FLEX_FILL, UC.MB_5, getAttrClasses(content)),
                ID($"{id}-tab"),
                Attr("data-bs-toggle", "pill"),
                Attr("role", "tab"),
                Attr("aria-controls", id),
                Attr("aria-selected", active.ToString()),
                removeAttrClasses(content)
            );

        public static XElement TabContent(params object[] content) =>
            Div(
                Class($"tab-content {getAttrClasses(content)}"),
                removeAttrClasses(content)
            );

        public static XElement Tab(string id, bool active, params object[] content)
        {
            var isActive = active ? "show active" : string.Empty;
            return Div(
                Class($"tab-pane fade {isActive} {getAttrClasses(content)}"),
                ID(id),
                Attr("role", "tabpanel"),
                Attr("aria-labelledby", $"{id}-tab"),
                removeAttrClasses(content)
            );
        }

        #endregion

        #region ATTRIBUTES

        public static List<XAttribute> ModalAttr(string id)
        {
            var attrs = new List<XAttribute>();
            attrs.Add(Attr("data-bs-toggle", "modal"));
            attrs.Add(Attr("data-bs-target", $"#{id}"));
            return attrs;
        }
        public static List<XAttribute> TooltipAttr(string message, bool cursor = true)
        {
            var attrs = new List<XAttribute>();
            if (cursor) attrs.Add(Style("cursor: pointer;"));
            attrs.Add(Attr("data-bs-toggle", "tooltip"));
            attrs.Add(Attr("data-bs-placement", "top"));
            attrs.Add(Attr("title", message));
            return attrs;
        }
        public static XAttribute Attr(string attrName, string attrValue) => new XAttribute(attrName, attrValue);
        public static XAttribute ID(string id) => new XAttribute("id", id);
        public static XAttribute Class(string className) => new XAttribute("class", className);
        public static XAttribute Class(params string[] classNames) => new XAttribute("class", string.Join(" ", classNames));
        public static XAttribute Style(params string[] styles) => new XAttribute("style", string.Join(" ", styles));

        #endregion

        #region PRIVATE METHODS

        private static string getAttrClasses(object[] contents)
        {
            List<string> classes = new List<string>();

            if (contents == null) return string.Empty;

            contents.ToList().ForEach(c =>
            {
                if (c.GetType() == typeof(XAttribute))
                {
                    var attr = c as XAttribute;
                    if (attr.Name == "class") classes.Add(attr.Value);
                }

            });

            return string.Join(" ", classes);
        }

        private static List<object> removeAttrClasses(object[] contents)
        {
            List<object> attrs = new List<object>();

            if (contents == null) return null;

            contents.ToList().ForEach(c =>
            {
                if (c.GetType() == typeof(XAttribute) && (c as XAttribute).Name == "class") return;

                attrs.Add(c);
            });

            return attrs;
        }

        #endregion
    }
}
