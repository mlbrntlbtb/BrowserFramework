using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HTMLBuilder
{
    public class HtmlBuilder
    {
        private List<object> _body;
        private List<object> _data;
        private List<XElement> _scripts;
        private List<XElement> _headItems;
        public HtmlBuilder()
        {
            _body = new List<object>();
            _data = new List<object>();
            _scripts = new List<XElement>();
            _headItems = new List<XElement>();

            _headItems.Add(HTML.Tag("link",
                    HTML.Attr("href", "https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/css/bootstrap.min.css"),
                    HTML.Attr("rel", "stylesheet"),
                    HTML.Attr("integrity", "sha384-giJF6kkoqNQ00vy+HMDP7azOuL0xtbfIcaT9wjKHr8RbDVddVHyTfAAsrekwKmP1"),
                    HTML.Attr("crossorigin", "anonymous")
                ));

            _headItems.Add(HTML.Tag("link",
                    HTML.Attr("href", "https://fonts.gstatic.com"),
                    HTML.Attr("rel", "preconnect")
                ));

            _headItems.Add(HTML.Tag("link",
                    HTML.Attr("href", "https://fonts.googleapis.com/css2?family=Roboto:wght@400;500;700&display=swap"),
                    HTML.Attr("rel", "stylesheet")
                ));
            _headItems.Add(HTML.Tag("link",
                    HTML.Attr("href", "https://fonts.googleapis.com/css2?family=Roboto:wght@400;500;700&display=swap"),
                    HTML.Attr("rel", "stylesheet")
                ));

            _headItems.Add(HTML.Tag("link",
                    HTML.Attr("href", "https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.2/css/all.min.css"),
                    HTML.Attr("rel", "stylesheet")
                ));

            _scripts.Add(HTML.Tag("script",
                    HTML.Attr("src", "https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta1/dist/js/bootstrap.bundle.min.js"),
                    HTML.Attr("integrity", "sha384-ygbV9kiqUc6oa4msXn9868pTtWMgiQaeYH7/t7LECLbyPA2x65Kgf80OJFdroafW"),
                    HTML.Attr("crossorigin", "anonymous"),
                    new XElement("span")
                ));

            AddStyle(@"
                body {
                    font-family: 'Roboto', sans-serif !important;
                  }
                ::-webkit-scrollbar {
                width: 10px;
                height: 5px;
                }
                ::-webkit-scrollbar-track {
                background: #f1f1f1;
                }
                ::-webkit-scrollbar-thumb {
                background: #888;
                }
                ::-webkit-scrollbar-thumb:hover {
                background: #555;
                }
            ");

            AddScript(@"
                        document.addEventListener(
                            'click',
                            function (event) {
                                if (!event.target.matches('.copy')) return;
                                event.preventDefault();
                                getCopy(event.target.innerText);
                            },
                            false
                        );

                        Array.from(document.getElementsByClassName('copy-btn')).forEach(
                            function(element) {
                                element.addEventListener(
                                'click',
                                function () {
                                    const path = document.getElementById(this.getAttribute('target'))
                                    .innerText;
                                    getCopy(path);
                                },
                                false
                                );
                            }
                        );

                        function getCopy(path) {
                            var temp = document.createElement('textarea');
                            document.body.appendChild(temp);
                            temp.value = path;
                            temp.select();
                            document.execCommand('copy');
                            alert('Successfully copied path to clipboard.');
                            document.body.removeChild(temp);
                        }
                    ");
        }

        public void Body(params object[] content) => _body.AddRange(content);
        public void Data(params object[] content) => _body.AddRange(content);

        public void AddScript(string script) => _scripts.Add(HTML.Tag("script", script));
        public void AddScript(XElement script) => _scripts.Add(script);
        public void AddStyle(string styles) => _headItems.Add(HTML.Tag("style", styles));
        public void AddLink(string href, string rel) =>
            _headItems.Add(HTML.Tag("link",
                HTML.Attr("href", href),
                HTML.Attr("rel", rel)
            ));
        public void AddLink(XElement link) => _headItems.Add(link);

        /// <summary>
        /// Will save and open Html report
        /// </summary>
        /// <param name="fileOutput"></param>
        /// <exception cref="DirectoryNotFoundException"><exception>
        /// <exception cref="UnauthorizedAccessException"><exception>
        public void Save(string fileOutput, bool autoLaunch = true)
        {
            var html =
               HTML.Tag("html",
                   HTML.Tag("head", _headItems),
                   HTML.Tag("body", _body, _scripts),
                   _data.Count > 0 ? _data : null
               );

            if (!fileOutput.EndsWith(".html")) fileOutput += ".html";

            File.WriteAllText(fileOutput, html.ToString());
            if(autoLaunch) Process.Start(fileOutput);
        }

        public string ConvertToString()
        {
            var html =
              HTML.Tag("html",
                  HTML.Tag("head", _headItems),
                  HTML.Tag("body", _body, _scripts),
                  _data.Count > 0 ? _data : null
              );

            return html.ToString();
        }

        public string ToEmailMessageBody(string content)
        {
            content = content.Replace("class=\"Table align-middle table-bordered\"", string.Format("style=\"{0}\"", UC.STR_TABLE_STYLE));
            content = content.Replace("class=\"table-dark\"", string.Format("style=\"{0}\"", UC.STR_TABLE_HEADER));
            content = content.Replace("class=\"text-center\"", string.Format("style=\"{0}\"", UC.STR_TABLE_HEADER));
            content = content.Replace("class=\"table-active\"", string.Format("style=\"{0}\"", UC.STR_TABLE_ROW_SETUP));
            content = content.Replace("class=\"table-white\"", string.Format("style=\"{0}\"", UC.STR_TABLE_ROW_NOTRUN));
            content = content.Replace("class=\"table-success\"", string.Format("style=\"{0}\"", UC.STR_TABLE_ROW_SUCCESS));
            content = content.Replace("class=\"table-danger\"", string.Format("style=\"{0}\"", UC.STR_TABLE_ROW_FAILED));
            content = content.Replace("<th>", string.Format("<th style=\"{0}\">", UC.STR_TABLE_STYLE));
            content = content.Replace("<td>", string.Format("<td style=\"{0}\">", UC.STR_TABLE_STYLE));
            content = content.Replace("<td style=\"white-space:pre-wrap; word-wrap: break-word; max-width: 25rem !important;\">", string.Format("<td style=\"{0}\">", UC.STR_TABLE_STYLE_WITHWRAP));
            return content;
        }
    }
}
