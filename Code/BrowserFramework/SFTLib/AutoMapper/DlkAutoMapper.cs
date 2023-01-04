using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using SFTLib.DlkControls;
using SFTLib.DlkSystem;
using SFTLib.DlkUtility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace SFTLib.AutoMapper
{
    public class DlkAutoMapper
    {

        #region Declaration
        private List<String> TabPagesID = new List<string>();
        private IWebElement mElement;
        private string ObjectStorePath, XPathParent, ParentTabName, ScreenFileName;
        private Boolean IsInit, isMainForm, IsDialogBox, IsLookup;
        private int tabcount;
        #endregion

        #region Constructor
        public DlkAutoMapper(string ObjectStorePath, string ParentTabName, string ScreenFileName, string IsDialogBox, string IsLookup)
        {
            this.ObjectStorePath = ObjectStorePath;
            this.ParentTabName = ParentTabName;
            this.ScreenFileName = ScreenFileName;
            this.IsDialogBox = Boolean.Parse(IsDialogBox);
            this.IsLookup = Boolean.Parse(IsLookup);
            XPathParent = Boolean.Parse(IsDialogBox) && !Boolean.Parse(IsLookup) ? "//*[contains(@class, 'x-window-body')]" : 
                Boolean.Parse(IsLookup) ? "//*[contains(@id, 'header-innerCt')]//ancestor::*[contains(@class, 'x-window-body')]" : "//*[@id='body']";
            isMainForm = true;
            tabcount = 1;
        }
        #endregion

        #region Private Methods
        private void Initialize()
        {
            DlkEnvironment.mSwitchediFrame = false;

            DlkSFTCommon.WaitForScreenToLoad(3);
            DlkSFTCommon.WaitForSpinner();

            if (!IsInit ^ IsElementStale())
            {
                mElement = DlkEnvironment.AutoDriver.FindElement(By.XPath("//*[@id='mainMenuToolbar']//*[contains(@class,'x-unselectable')][position() =1]"));
                IsInit = !IsInit ? true : IsInit;
            }
        }
        public Boolean IsElementStale()
        {
            try
            {
                String sTagName = mElement.TagName;
                return false;
            }
            catch (StaleElementReferenceException)
            {
                return true;
            }
        }
        private List<IWebElement> GetMenuItemWithNoArrow()
        {
            return mElement.FindElements(By.XPath("//div[contains(@class,'x-panel x-layer') and not(contains(@style,'visibility: hidden'))][last()]//img[not(contains(@class,'x-menu-item-arrow'))]/ancestor::div[1]")).ToList();
        }
        private List<IWebElement> GetMenuItemWithArrow()
        {
            return mElement.FindElements(By.XPath("//div[contains(@class,'x-panel x-layer') and not(contains(@style,'visibility: hidden'))][last()]//img[contains(@class,'x-menu-item-arrow')]/ancestor::div[1]")).ToList();
        }
        private string GetCurrentMenuTitle()
        {
            return mElement.FindElement(By.XPath("//*[@id='mainMenuToolbar']//*[contains(@class,'x-btn x-unselectable')][last()]")).Text;
        }
        private string GetOSScreenName()
        {
            var list = mElement.FindElements(By.XPath("//*[@id='mainMenuToolbar']//*[contains(@class,'x-btn x-unselectable')]"))
                   .Where(x => !String.IsNullOrEmpty(x.Text.Trim()))
                   .Select(x => x.Text.Trim()).ToList();
            var osName = "OS_" + String.Join("_", list).Replace(" ", "") + ".xml";
            return osName;
        }
        private string GetParentTabName(IWebElement x)
        {
            var val = x.GetAttribute("id").Split('-')[0]
                .Replace("_toolbar", "")
                .Replace("_filter", "")
                .Replace("_grid", "");
            var xpath = IsDialogBox ? $"(//*[contains(@id, '{val}') and contains(@id, '_tab-btnWrap')])[{tabcount}]" : $"//*[contains(@id, '{val}_tab-btnWrap')]";
            var parentElement = DlkEnvironment.AutoDriver.FindElements(By.XPath(xpath)).FirstOrDefault();
            if(String.IsNullOrEmpty(parentElement.Text)) parentElement = DlkEnvironment.AutoDriver.FindElements(By.XPath($"//*[contains(@id, '{val}') and contains(@id, 'detail_panel_header-innerCt')]")).FirstOrDefault();
            return parentElement.Text.Trim().Replace(" ", "") + "_";
        }
        private Boolean IsParentTabBody(IWebElement x)
        {
            Boolean sample = x.FindElements(By.XPath(".//ancestor::*[contains(@class, 'tab-body')]")).FirstOrDefault() != null;
            return sample;
        }
        private List<FormControl> GetFormControls(out string formTitle, string xPathParentForControls = "", string parentTab = "")
        {
            List<FormControl> FormControlList = new List<FormControl>();

            try
            {
                formTitle = GetCurrentMenuTitle();
            }
            catch
            {
                formTitle = "";
            }
            string formTitleString = formTitle;
            DlkBaseControl formTitleAndFormSubtitle = GetFormTitleAndFormSubTitle();

            string parentName = (!String.IsNullOrEmpty(parentTab) ? parentTab + "_" : "");

            if (!IsDialogBox && formTitleAndFormSubtitle.mElms != null)
            {
                FormControlList.AddRange(formTitleAndFormSubtitle.mElms
                        .Where(x => !String.IsNullOrEmpty(x.Text.Trim()))
                        .Select(x => new FormControl()
                        {
                            IsFormTitle = (formTitleString == x.Text.Trim() ? true : false),
                            XPath = $"contentFrame_//*[@id='{x.GetAttribute("id")}']",
                            ControlType = "Label",
                            ControlName = (formTitleString == x.Text.Trim() ? "FormTitle" : "SubFormTitle"),
                            SearchMethod = "IFRAME_XPATH"
                        }).ToList());
            }
            else
            {
                DlkBaseControl dialogBoxTitle = GetFormControl(ControlType.DialogBoxTitle, xPathParentForControls, true);

                if (dialogBoxTitle != null && dialogBoxTitle.mElms != null)
                {
                    FormControlList.AddRange(dialogBoxTitle.mElms
                            .Select(x => new FormControl()
                            {
                                XPath = $"contentFrame_//*[@id='{x.GetAttribute("id")}']",
                                ControlType = "Label",
                                ControlName = "DialogBoxTitle",
                                SearchMethod = "IFRAME_XPATH"
                            }).ToList());
                }

            }

            DlkBaseControl formTabPages = GetFormControl(ControlType.TabPage, xPathParentForControls);

            if (formTabPages != null && formTabPages.mElms != null)
            {
                if (isMainForm) TabPagesID.AddRange(formTabPages.mElms.Select(x => x.GetAttribute("id").ToString()).ToList());
                isMainForm = false;
                FormControlList.AddRange(formTabPages.mElms
                        .Select(x => new FormControl()
                        {
                            IsFormTitle = (formTitleString == x.Text.Trim() ? true : false),
                            XPath = $"contentFrame_//*[@id='{x.GetAttribute("id")}']{(x.Text.Trim() == "Filter" ? "/parent::*" : "")}",
                            ControlType = "TabPage",
                            ControlName = x.Text.Trim().Replace(" ", "") + "_TabPage",
                            SearchMethod = "IFRAME_XPATH"
                        }).ToList());
            }

            DlkBaseControl formButtons = GetFormControl(ControlType.Button, xPathParentForControls, true);

            if (formButtons != null && formButtons.mElms != null)
            {
                
                FormControlList.AddRange(formButtons.mElms
                    .Where(x => !x.GetAttribute("id").Any(char.IsDigit))
                        .Select(x => new FormControl()
                        {
                            IsFormTitle = (formTitleString == x.Text.Trim() ? true : false),
                            XPath = $"contentFrame_//*[@id='{x.GetAttribute("id")}']{(x.Text.Trim() == "Filter" ? "/parent::*" : "")}",
                            ControlType = (x.Text.Trim() == "Filter" ? "Toggle" : "Button"),
                            ControlName = (IsParentTabBody(x) ? GetParentTabName(x) : parentName) + (x.GetAttribute("id").ToLower().Contains("quicklink-btnwrap") ? $"{GetFormControlLabel(x.GetAttribute("id"))}_Lookup" : x.Text.Trim().Replace(" ", "")),
                            SearchMethod = "IFRAME_XPATH"
                        }).ToList());
            }

            DlkBaseControl formCombBoxes = GetFormControl(ControlType.ComboBox, xPathParentForControls, true);

            if (formCombBoxes != null && formCombBoxes.mElms != null)
            {
                FormControlList.AddRange(formCombBoxes.mElms
                    .Where(x => !x.GetAttribute("id").Any(char.IsDigit))
                        .Select(x => new FormControl()
                        {
                            XPath = $"contentFrame_//*[@id='{x.GetAttribute("id")}']",
                            ControlType = GetFormControlLabel(x.GetAttribute("id")).Contains("Date") ? "Calendar" : "ComboBox",
                            ControlName = (IsParentTabBody(x) ? GetParentTabName(x) : parentName) + GetFormControlLabel(x.GetAttribute("id")),
                            SearchMethod = "IFRAME_XPATH"
                        }).ToList());
            }
            DlkBaseControl formLabel = GetFormControl(ControlType.Label, xPathParentForControls, true);

            if (formLabel != null && formLabel.mElms != null)
            {
                FormControlList.AddRange(formLabel.mElms
                    .Where(x => !x.GetAttribute("id").Any(char.IsDigit))
                        .Select(x => new FormControl()
                        {
                            XPath = $"contentFrame_//*[@id='{x.GetAttribute("id")}']",
                            ControlType = "Label",
                            ControlName = (IsParentTabBody(x) ? GetParentTabName(x) : parentName) + GetFormControlLabel(x.GetAttribute("id")),
                            SearchMethod = "IFRAME_XPATH"
                        }).ToList());
            }

            DlkBaseControl formTextBoxes = GetFormControl(ControlType.TextBox, xPathParentForControls, true);

            if (formTextBoxes != null && formTextBoxes.mElms != null)
            {
                FormControlList.AddRange(formTextBoxes.mElms
                    .Where(x => !x.GetAttribute("id").Any(char.IsDigit))
                        .Select(x => new FormControl()
                        {
                            XPath = $"contentFrame_//*[@id='{x.GetAttribute("id")}']",
                            ControlType = "TextBox",
                            ControlName = (IsParentTabBody(x) ? GetParentTabName(x) : parentName) + GetFormControlLabel(x.GetAttribute("id")),
                            SearchMethod = "IFRAME_XPATH"
                        }).ToList());
            }

            DlkBaseControl formCheckBoxes = GetFormControl(ControlType.CheckBox, xPathParentForControls, true);

            if (formCheckBoxes != null && formCheckBoxes.mElms != null)
            {
                FormControlList.AddRange(formCheckBoxes.mElms
                    .Where(x => !x.GetAttribute("id").Any(char.IsDigit))
                        .Select(x => new FormControl()
                        {
                            XPath = $"contentFrame_//*[@id='{x.GetAttribute("id")}']",
                            ControlType = "CheckBox",
                            ControlName = (IsParentTabBody(x) ? GetParentTabName(x) : parentName) + GetFormControlLabel(x.GetAttribute("id")),
                            SearchMethod = "IFRAME_XPATH"
                        }).ToList());
            }

            DlkBaseControl formNumerics = GetFormControl(ControlType.Numeric, xPathParentForControls, true);

            if (formNumerics != null && formNumerics.mElms != null)
            {
                FormControlList.AddRange(formNumerics.mElms
                    .Where(x => !x.GetAttribute("id").Any(char.IsDigit))
                        .Select(x => new FormControl()
                        {
                            XPath = $"contentFrame_//*[@id='{x.GetAttribute("id")}']",
                            ControlType = "TextBox",
                            ControlName = (IsParentTabBody(x) ? GetParentTabName(x) : parentName) + GetFormControlLabel(x.GetAttribute("id")),
                            SearchMethod = "IFRAME_XPATH"
                        }).ToList());
            }

            DlkBaseControl formGrids = GetFormControl(ControlType.Grid, xPathParentForControls);

            if (formGrids != null && formGrids.mElms != null)
            {
                FormControlList.AddRange(formGrids.mElms
                    .Where(x => !x.GetAttribute("id").Any(char.IsDigit))
                        .Select(x => new FormControl()
                        {
                            XPath = $"contentFrame_//*[@id='{x.GetAttribute("id")}']",
                            ControlType = "Grid",
                            ControlName = (IsParentTabBody(x) ? GetParentTabName(x) : parentName) + "Grid",
                            SearchMethod = "IFRAME_XPATH"
                        }).ToList());
            }



            return FormControlList;
        }
        private DlkBaseControl GetFormControl(ControlType controlType, string parentXPATH, bool acceptEmptyString = false)
        {
            string controlBaseXPath = GetControlBaseXPath(controlType);
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkLabel label = new DlkLabel("Control", "IFRAME_XPATH_MULTIPLE_RETURN", "contentFrame_" + parentXPATH + controlBaseXPath);
            try { label.FindElement(3); } catch { }
            if (label != null && label.mElms != null)
            {
                label.mElms = label.mElms.Where(row => {
                    return (acceptEmptyString ? true : !String.IsNullOrEmpty(row.Text.Trim())) && row.Displayed;
                }).ToList();
            }
            return label;
        }
        private string GetFormControlLabel(string controlId)
        {
            try
            {
                var cID = IsDialogBox && !IsLookup
                        ? controlId
                            .Replace("inputEl", "label-inputEl")
                            .Replace("Quicklink-btnWrap", "-label-inputEl")
                            .Replace("triggerWrap", "label-inputEl")
                        : controlId
                            .Replace("inputEl", "labelEl")
                            .Replace("Quicklink-btnWrap", "Container-labelEl")
                            .Replace("triggerWrap", "labelEl");

                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                DlkLabel label = new DlkLabel("Control", "IFRAME_XPATH", $"contentFrame_//*[contains(@id, '{cID}')]");
                try
                {
                    label.FindElement(3);
                    if (String.IsNullOrEmpty(label.mElement.Text))
                    {
                        cID = controlId.Replace("-inputEl", "Container-labelEl").Replace("-triggerWrap", "Container-labelEl");
                        DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                        label = new DlkLabel("Control", "IFRAME_XPATH", $"contentFrame_//*[contains(@id, '{cID}')]");
                        label.FindElement(3);
                    }
                }
                catch
                {
                    return "";
                }

                if (label != null && label.mElement != null)
                {
                    return label.mElement.Text.Replace(" ", "");
                }
                return "";
            }
            catch(Exception e)
            {
                throw new Exception("Automapper.GetFormControlLabel() failed : " + e.Message);
            }
        }
        private void GenerateDocument(out string formTitle, string xpath, XDocument document, XElement root, string ParentName = "")
        {
            var formControls = GetFormControls(out formTitle, xpath, ParentName);
            foreach (var formControl in formControls)
            {
                if (!document.ToString().Contains($"key=\"{formControl.ControlName}\""))
                {
                    DlkLogger.LogInfo($"Generating {formControl.ControlName}");
                    var control = new XElement("control", new XAttribute("key", formControl.ControlName));
                    var controlType = new XElement("controltype");
                    controlType.Value = formControl.ControlType;
                    var searchmethod = new XElement("searchmethod");
                    searchmethod.Value = formControl.SearchMethod;
                    var searchparameters = new XElement("searchparameters");
                    searchparameters.Value = formControl.XPath;
                    control.Add(controlType);
                    control.Add(searchmethod);
                    control.Add(searchparameters);
                    root.Add(control);
                }
            }
        }
        private string GetControlBaseXPath(ControlType control)
        {
            switch (control)
            {
                case ControlType.Button:
                    return "//*[contains(@id,'btnWrap')]";
                case ControlType.ComboBox:
                    return "//input/parent::*/following-sibling::td[contains(@class,'x-trigger-cell x-unselectable')]/ancestor::table[1]";
                case ControlType.TextBox:
                    return "//input/ancestor::table[1][not(contains(@class,'x-form-trigger-wrap'))]//input[not(contains(@class,'checkbox'))]";
                case ControlType.CheckBox:
                    return "//input/ancestor::table[1][not(contains(@class,'x-form-trigger-wrap'))]//input[contains(@class,'checkbox') and not(contains(@disabled,'true'))]";
                case ControlType.Numeric:
                    return "//input/parent::*/following-sibling::td[@class='x-trigger-cell']/ancestor::table[1]//input";
                case ControlType.TabPage:
                    return "//div[contains(@class,'x-tab-wrap')]";
                case ControlType.Label:
                    return "//*[contains(@class, 'x-form-display-field') and not(contains(@id, 'label-inputEl'))]";
                case ControlType.Grid:
                    return "//*[substring(@id,string-length(@id) -string-length('_grid') +1) = '_grid']";
                case ControlType.DialogBoxTitle:
                    return "//*[@class='x-header-text x-window-header-text']";
                case ControlType.ListBoxSelection:
                    return "//*[contains(@class, 'x-list-plain')]//ancestor::*[contains(@id, 'containerEl')][2]";
                default:
                    return "";
            }
        }
        private static DlkBaseControl GetFormTitleAndFormSubTitle()
        {
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkLabel label = new DlkLabel("Control", "IFRAME_XPATH_MULTIPLE_RETURN", "contentFrame_//*[contains(@class,'x-panel-header-text-container')]");
            try { label.Initialize(); } catch { }

            return label;
        }

        private class Menuitem
        {
            public string ParentMenu { get; set; }
            public string Menu { get; set; }
            public bool IsParent { get; set; }
            public bool IsExplored { get; set; }
        }
        public class FormControl
        {
            public bool IsFormTitle { get; set; }
            public string XPath { get; set; }
            public string ControlType { get; set; }
            public string ControlName { get; set; }
            public string SearchMethod { get; set; }
        }
        private enum ControlType
        {
            TextBox,
            ComboBox,
            Button,
            CheckBox,
            Numeric,
            TabPage,
            Label,
            Grid,
            DialogBoxTitle,
            ListBoxSelection
        }
        #endregion

        #region Public Methods
        public void Start()
        {
            try
            {
                XDocument document;
                XElement root;
                string formTitle;
                string osName = "";
                IsInit = false;
                ScreenFileName += ".xml";

                if (String.IsNullOrEmpty(ScreenFileName))
                {
                    DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                    Initialize();

                    osName = GetOSScreenName();
                }

                string filePath = $@"{ObjectStorePath}\OS_{((!String.IsNullOrEmpty(ScreenFileName)) ? ScreenFileName : osName)}";


                if (File.Exists(filePath))
                {
                    document = XDocument.Load(filePath);
                    root = document.Element("objectstore");
                }
                else
                {
                    root = new XElement("root", new XAttribute("screen", ((!String.IsNullOrEmpty(ScreenFileName)) ? ScreenFileName : osName).Replace("OS_", "").Replace(".xml", "")));
                    root.Name = "objectstore";
                    root.Descendants("control");
                    document = new XDocument(root);
                }

                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                GenerateDocument(out formTitle, XPathParent, document, root, ParentTabName);

                if (TabPagesID.Count() > 0)
                {
                    foreach (var tabPageID in TabPagesID)
                    {
                        var tabpage = DlkEnvironment.AutoDriver.FindElement(By.XPath($"//*[@id='{tabPageID}']"));
                        tabpage.ClickUsingJS();
                        Thread.Sleep(1000);
                        var tabPageBody = IsDialogBox 
                            ? $"(//*[contains(@id, 'kaba-form') and contains(@id, 'innerCt')])[{tabcount}]"
                            : $"//*[contains(@class, 'x-box-inner') and contains(@id, '{tabPageID.Split('_')[tabPageID.Split('_').Count() - 2]}-innerCt') and @role='presentation']";
                        GenerateDocument(out formTitle, tabPageBody, document, root);
                        if (IsDialogBox) tabcount++;
                    }
                }

                Thread.Sleep(1000);
                document.Save(filePath);
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo(filePath);
                process.Start();
            }
            catch (Exception e)
            {
                throw new Exception("Automapper.Start() failed : " + e.Message);
            }
        }
        #endregion

    }
}
