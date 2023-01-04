using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CommonLib.DlkControls;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using OpenQA.Selenium;

namespace CommonLib.DlkUtility
{
    public static class DlkInspect
    {
        #region PRIVATE MEMBERS
        private static Object node;
        private static string[] mQueryComponents = { "basicCountBttn", "basicSaveQ", "basicResetQ", "closeQ", "relationList",
                                                     "submitQ", "qryTabsDiv", "fieldList",  "AddBtn", "CombineOperator",
                                                     "valueEntered", "valueCBView", "valueEnteredCal", "matchCase", 
                                                     "advancedCountBttn", "advancedResetQ", "advancedSaveQ", "advancedCountBttn"};
        private static readonly string[] m_ValidClasses = { "bOk", "actBtn", "fCalBtn", "fCalBtn20", "fExpandoBtn", "fBrowseBtn", // button
                                                   "fLaunchBtn", "submitBtn", "submitBtnDisabled", // button
                                                   "submitBtnActive", "submitBtnHover", "goToLbl", // button
                                                   "tCCBF", "tCCB" , "tCCBV", "tCCBImgF", "tCCBImgT", "tCCBT",// combo
                                                   "rsTbBtn", "rsTbBtnNormal", "rsTbBtnDisabled", "rsTbBtnHover", "rsTbBtnActive", // form buttons
                                                   "busItem", "deptItem", "navItem", //nav menu
                                                   "fCB", //checkbox
                                                   "modalTabLbl", // tab
                                                   "sbtskLnk", "drillDownPathText", // link
                                                   "msgText", "msgTextOld", "wok", "eLnk", // message area
                                                   "wMnuHead", "wMnuPick", // menu
                                                   "tbBtn_Disabled", "tbBtn_Hover", "tbBtn_Active", // toolbar
                                                   "tbBtn", "tbBtnSplitLeft", "tbBtnSplitRight", "tlbrDDActionDiv",  // toolbar
                                                   "tbBtn_Normal", "tbBtnSplitLeft_Normal", "tbBtnSplitRight_Normal", // toolbar
                                                   "queryPopupBtn", "queryBasicFld", "imgAndTextBtn", "fDFNH", "fDFRONH", // query
                                                   "cllfrst", // table
                                                   "appFltr", "fDFRQ",  //textbox NOTE: Other txtbox classes should be listed here as well to skip valid ancestor search
                                                   "autoCItem", // SearchAppResultList
                                                   "fdfrq" , "fdf" , "fdfro", "fdfrqnum", "fdfronum", "fdfnum", "fdexpandosmall" , "inputfld",
                                                    "querybasicfld", "querydatafield", "query", "appfltr", "popupdatafield", "bok", "imgbtn",
                                                    "actbtn",  "fcalbtn", "fcalbtn20", "fexpandobtn", "fbrowsebtn",  "flaunchbtn", "submitbtn",
                                                    "submitbtnhover", "submitbtnactive",  "querypopupbtn", "imgandtextbtn", "showcriteriadiv",
                                                    "hidecriteriadiv","hidecriteriaimg","showcriteriaimg", "navareabtn","gotolbl", "lkpglyph",
                                                    "popupbtn",  "querycb" , "popupfcb", "fcb", "tccbf" , "tccb", "cust_tccbv", "sbtsklnk",
                                                    "wmnupick",  "wmnuhead", "navitem", "busitem", "deptitem" , "modaltablbl", "rstbbtn",
                                                    "rstbbtnactive", "rstbbtnnormal", "rstbbtnhover",  "rstbbtndisabled", "rsdragger", "fcba",
                                                    "tbbtn","tbbtnsplitleft","tbbtnsplitright","tbbtn_active","tbbtn_normal","tbbtn_disabled",
                                                    "tbbtn_hover","tbbtnsplitleft_normal","tbbtnsplitright_normal","tbbtnsplitleft_hover",
                                                    "tbbtnsplitright_hover", "fdfnh", "fdfronh","fdfrqnh", "tlbrddactiondiv","popupclose","wok",
                                                    "msgtext","msgtextold","elnk","tccbtb","tdf","tdfrq","tdfro","tcb","tdfrqnum","tdfronum",
                                                    "cllfrst","tdfnum", "autocitem", "popupcaltdybtn", "popupcaldate"
                                                 };
          #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Verify if DescriptorValue is in the criteria or is Table
        /// </summary>
        /// <param name="DescriptorValue">The value of the descriptor</param>
        private static bool IsProbablyLookUp(string DescriptorValue)
        {
            string[] criteria = { "qryBttn", "bOk", "bCan", "rsCls", "rsMax", "drillDownPathText" };
            return criteria.Contains(DescriptorValue) || DescriptorValue.Contains("Table");
        }
        
        /// <summary>
        /// Verify if test environment is at login
        /// </summary>
        private static bool IsLoginScreen()
        {
            // We need to determine if the user clicked on the 'Log In' text that is a child of the loginBtn
            return DlkEnvironment.AutoDriver.FindElements(By.Id("loginBtn")).Any();
        }

        private static bool IsPrintOptions()
        {
            return DlkEnvironment.AutoDriver.FindElements(By.Id("printSetupForm")).Count > 0 
                   && DlkEnvironment.AutoDriver.FindElements(By.Id("printSetupForm")).First().Displayed;
        }

        /// <summary>
        /// Verify if test environment is at lookup
        /// </summary>
        private static bool IsLookUp()
        {
            return DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[contains(@id,'LKP') or contains(@id,'LOOKUP')]/ancestor::form[1]")).Count > 0;
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Gets accessed control
        /// </summary>
        /// <param name="ActionType">Type of action</param>
        /// <param name="AlertText">Error message if exception is encountered</param>
        /// <param name="IsVerify">Checks if action is verified</param>
        /// <param name="IsHotkeyPressed">Checks if a hotkey is pressed</param>
        /// <returns>Returns control if found, throws an Alert Dialog otherwise</returns>
        public static DlkBaseControl GetAccessedControl(out string ActionType, out string AlertText, out DlkCapturedStep CurrentStep, bool IsVerify, bool IsHotkeyPressed)
        {
            DlkBaseControl ret = null;
            bool bPrevent = IsVerify || IsHotkeyPressed;
            AlertText = string.Empty;
            ActionType = string.Empty;
            /* Store the ff info in memory: BaseControl, value, actiontype, id, class, tag */
            CurrentStep = new DlkCapturedStep(null,"","", "" ,"", "", "", "", "");
            try
            {
                try
                {
                    if (DlkAlert.DoesAlertExist())
                    {
                        throw new UnhandledAlertException();
                    }
                }
                catch
                {
                    throw;
                }

                // for future editors: what you can do to further optimize the inspect/test capture/mapping feature is to find opportunities to remove redundant code 
                // (such as reloading ctrls/screens every control click, etc.).
                // also look for comments with the text "PERFORMANCE GAIN" -> these small comments contain tips on what you can do as a starting point to optimize the code

                node = (Object)((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteAsyncScript(
                            "try{" +
                            "var callback =  arguments[arguments.length - 1];" +

                            "function findAncestor(el, cls) {" +
                                "while ((el = el.parentElement) && el.className.indexOf(cls) < 0) ;" +
                                "return el;" +
                            "}" +

                            "function getTargetElementAndValue(event) {" + // handler for delayed firing of click events for certain controls
                            "try{ " +
                                "var parent1 = event.target.parentElement; "+
                                "var parent2 = event.target.parentElement.parentElement;" + 
                                "var parent3 = event.target.parentElement.parentElement.parentElement;" +
                                "var screen = document.querySelector(\".app\");" +
                                "var elem = parent3 != null ? parent3.querySelector(\"form div.popRef\") : null;" +
                                "var mainForm = findAncestor(event.target, \"mainForm\");" +
                                "elem = elem == null && mainForm != null ? mainForm.querySelector(\"div.popRef\") : elem;" +
                                "var indexIndicator = mainForm != null ? mainForm.parentElement : null;" +
                                "callback([event.target, event.target.value, event.target.getAttribute(\"actiontype\"), event.target.getAttribute(\"id\"), event.target.className, event.target.tagName," +
                                "parent1, parent1.getAttribute(\"id\"), parent1.className," +
                                "parent2, parent2.getAttribute(\"id\"), parent2.className," +
                                "parent3, parent3.getAttribute(\"id\"), parent3.className," +
                                "elem, elem != null ? elem.getAttribute(\"id\") : null, elem != null ? elem.className: null," +
                                "screen != null ? screen.getAttribute(\"id\") : null," +
                                "indexIndicator != null ? indexIndicator.getAttribute(\"id\") : null ]);}" +
                            "catch (err) {}" +
                            "}" +

                            // ACTION TYPE LISTENERS TO DETERMINE KEYWORD
                            "function nodeLostFocus(event) {" + // handler for multiple keystroke events
                                "try { " +
                                    "event.target.setAttribute(\"actiontype\", \"Keystroke\");" +
                                    "var timeout = 0;" +
                                    "event.target.onkeyup =" + 
                                    "function (e)" + 
                                    "{" + 
                                        "clearTimeout(timeout);" + 
                                        "timeout = setTimeout(function() {getTargetElementAndValue(e);},1300); };" +
                                    "}" +
                                "catch (err) {}" +
                            "}" +

                            "function nodeMouseDown(event) {" + // mouse down event handler
                                "try{ event.target.setAttribute(\"actiontype\", \"Click\"); getTargetElementAndValue(event);}" + 
                                "catch (err) {}" +
                            "}" +

                            "function nodeClicked(event) {" + // mouse click event handler
                                "try{" + 
                                    "if (" + bPrevent.ToString().ToLower() + "){" + // verify action
                                        "event.preventDefault();" +
                                        "event.stopPropagation();" +
                                        "document.removeEventListener(\"click\",nodeClicked,true);" +
                                        "getTargetElementAndValue(event);"+
                                    "}" +
                                "}" + 
                                "catch (err){}" + 
                            "}" +

                            "function delayedClick(event) {" + // handler for delayed firing of click events for certain controls
                                "try{ document.removeEventListener(\"click\",nodeClicked,true); event.target.dispatchEvent(event);}" + 
                                "catch (err){}" +
                            "}" +

                            // ASSIGN ACTIONTYPE TO DETERMINE KEYWORD
                            "document.addEventListener(\"changed\", nodeLostFocus, true);" +
                            "document.addEventListener(\"keydown\", nodeLostFocus, true);" +
                            "document.addEventListener(\"click\", nodeClicked, true);" +
                            "document.addEventListener(\"mousedown\", nodeMouseDown, true);" +

                         "}" + 
                         "catch (err){}");
                if (node != null)
                {
                    // abuse dynamic typing, resolve dynamic type to actual type during runtime. 
                    // we use dynamic here because javascript arrays are loosely typed, meaning an array can have multiple data types
                    var array = ((IEnumerable)node).Cast<dynamic>().Select(x => x).ToArray();
                    DlkCapturedElement capturedElement = new DlkCapturedElement(array);
                    var elementTxt = capturedElement.ElementText;
                    ActionType = IsVerify ? "Verify" : (capturedElement.ActionType ?? string.Empty);
                    try
                    {
                        // PERFORMANCE GAIN: REMOVE INFER OR INFER ONLY WHEN NEEDED.
                        // FOR NOW, INFER EVERYTIME BUT DO NOT USE THE INFERRED CONTROL IF NOT NEEDED

                        // ANOTHER PERFORMANCE GAIN OPPORTUNITY: CHECK IF YOU NEED TO INFER AGAIN. IF THE CURRENT INTERACTED ELEMENT IS THE SAME AS THE PREVIOUS INTERACTED ELEMENT,
                        // WE MAY CONSIDER TO NOT INFER ANYMORE BECAUSE IT WILL JUST BE THE SAME ELEMENT. 
                        // WE CAN ATTACH AN HTML ATTRIBUTE USING JAVASCRIPT TO KNOW IF THE ELEMENT IS THE SAME AS BEFORE TO AVOID INFERRING AGAIN
                        string newClass = capturedElement.ElementClass;
                        string newId = capturedElement.ElementId;

                        ret = new DlkBaseControl("TargetControl", InferTarget(capturedElement.WebElement, capturedElement.ElementClass, 
                            out newClass, ref newId,
                            capturedElement.Parent1, capturedElement.Parent1Id,  capturedElement.Parent1Class, 
                            capturedElement.Parent2, capturedElement.Parent2Id, capturedElement.Parent2Id, 
                            capturedElement.Parent3, capturedElement.Parent3Id, capturedElement.Parent3Class)); 
                        capturedElement.ElementId = newId;
                        capturedElement.ElementClass = newClass;
                        CurrentStep = new DlkCapturedStep(ret, elementTxt, ActionType, capturedElement.ElementId, 
                            capturedElement.ElementClass, capturedElement.ElementTagName, capturedElement.PopRefId, 
                            capturedElement.InferredScreen, capturedElement.AncestorFormIndex);
                    }
                    catch
                    {
                        // do nothing
                    }
                }
            }  
            catch (WebDriverTimeoutException)
            {
                // do nothing
            }
            catch (UnhandledAlertException)
            {
                AlertText = DlkEnvironment.AutoDriver.SwitchTo().Alert().Text;
                return ret;
            }
            catch // for real exceptions
            {
                ActionType = string.Empty;
            }
            return ret;
        }

        /// <summary>
        /// Infers the target element
        /// </summary>
        /// <param name="Source">The element interacted</param>
        /// <returns>Returns the target element</returns>
        public static IWebElement InferTarget(IWebElement Source, string sourceClassName,
            out string newClass, ref string newId,
            IWebElement parent1 = null, string parent1Id = "", string parent1Class ="",
            IWebElement parent2 = null, string parent2Id = "", string parent2Class = "",
            IWebElement parent3 = null, string parent3Id = "", string parent3Class = "")
        {
            IWebElement ret = Source;
            string classNameSource = sourceClassName;

            /* support for 701 tabs */
            classNameSource = classNameSource.Replace("Inactive", string.Empty).Replace("Active", string.Empty);
            newClass = classNameSource; // set the value to whatever the class of the control is. we need to out the new class since if we change the element.

            if (parent1Class.Contains("cll")) /* Special case for anthing related to tabl control, performance issues */
            {
                if (parent1Class == "cllfrst")
                {
                    ret = parent1;
                    newClass = parent1Class;
                }
                else if (sourceClassName.Contains("lookup"))
                {
                    ret = parent1;
                    newClass = "cust_lu_table";
                }
                newId = parent2Class.Replace("row", string.Empty);
                int zeroBasedIndex;
                if (int.TryParse(newId, out zeroBasedIndex))
                {
                    newId = (++zeroBasedIndex).ToString();
                }
                return ret;
            }
            else if (classNameSource.Contains("lookupIcon"))
            {
                try
                {
                    ret = DlkEnvironment.AutoDriver.FindElement(By.XPath("//*[contains(@style, 'box-shadow: rgb(2, 121, 168)')]"));
                    newClass = "cust_lu";
                    newId = ret.GetAttribute("id");
                }
                catch
                {
                    // ignore
                }
                return ret;
            }
            else if (m_ValidClasses.Any(item => item.ToLower().Equals(classNameSource.ToLower())))
            {
                // special case for combobox dropdownbutton
                try
                {
                    if (classNameSource == "tCCBImgF")
                    {
                        ret = Source.FindElement(By.XPath("./preceding-sibling::*[@class='tCCBF'][1]"));
                        newClass = "tCCBF"; // we need to out the new class since we changed the element.
                        newId = ret.GetAttribute("id");
                    }
                    else if (classNameSource == "tCCBImgT")
                    {
                        ret = Source.FindElement(By.XPath("./preceding-sibling::*[@class='tCCBTb'][1]"));
                        newClass = "tCCBImgT"; // we need to out the new class since we changed the element.
                        newId = ret.GetAttribute("id");
                    }
                    else if (classNameSource == "tCCBT") // special handling for combo box with diff XML (Submit Batch Job, Print Options, etc)
                    {
                        newClass = "tCCBImg";
                        newId = parent2Id + "__img";
                    }
                    else if (classNameSource == "popupBtn" && !string.IsNullOrEmpty(parent1Class) // Message Area close 
                        && parent1Class.ToLower().Contains("msg"))
                    {
                        newClass = "closeM";
                    }
                }
                catch
                {
                    // ignore error
                }
                return ret;
            }
            else
            {
                // 3 ancestor-level search algorithm
                if (m_ValidClasses.Contains(parent1Class))
                {
                    if (parent1Class == "tCCBV")
                    {
                        new DlkBaseControl("ComboItem", ret).SetAttribute("class", "cust_tCCBV");
                        newClass = "cust_tCCBV"; // we need to out the new class since we changed the element.
                    }
                    else
                    {
                        ret = parent1;
                        newClass = parent1Class;
                        newId = parent1Id;
                    }
                    return ret;
                }
                else if (m_ValidClasses.Contains(parent2Class))
                {
                    if (parent2Class == "tCCBV")
                    {
                        new DlkBaseControl("ComboItem", ret).SetAttribute("class", "cust_tCCBV");
                        newClass = "cust_tCCBV"; // we need to out the new class since we changed the element.
                    }
                    else
                    {
                        ret = parent2;
                        newClass = parent2Class;
                        newId = parent2Id;
                    }
                    return ret;
                }
                else if (m_ValidClasses.Contains(parent3Class))
                {
                    if (parent3Class == "tCCBV")
                    {
                        new DlkBaseControl("ComboItem", ret).SetAttribute("class", "cust_tCCBV");
                        newClass = "cust_tCCBV"; // we need to out the new class since we changed the element.
                    }
                    else
                    {
                        ret = parent3;
                        newClass = parent3Class;
                        newId = parent3Id;
                    }
                    return ret;
                }
                return ret;
            }
        }

        
        /// <summary>
        /// Gets the index of the form
        /// </summary>
        /// <param name="TargetControl">Name of the control which belongs to the form</param>
        /// <returns>Returns the index of the form</returns>
        public static int GetAncestorFormIndex(DlkBaseControl TargetControl, string className = "", string inferredIndex = "")
        {
            int ret = -1;
            string classTarget = string.IsNullOrEmpty(className) ? TargetControl.GetAttributeValue("class") : className;   // pass class as arg        

            try
            {
                switch (classTarget)
                {
                    case "rsDragger":
                        IWebElement div = TargetControl.mElement.FindElement(By.XPath("./.."));
                        ret = int.Parse(div.GetAttribute("id"));
                        break;
                    case "bOk":
                        IWebElement formPreceding = TargetControl.mElement.FindElement(By.XPath("./preceding::form[1]"));
                        ret = int.Parse(formPreceding.GetAttribute("id"));
                        break;
                    default:
                        if (!string.IsNullOrEmpty(inferredIndex))
                        {
                            ret = int.Parse(inferredIndex);
                            break;
                        }
                        IWebElement ancdiv = TargetControl.mElement.FindElements(By.XPath("./ancestor::form[1]/..")).FirstOrDefault();
                        ret = ancdiv == null ? -1 : int.Parse(ancdiv.GetAttribute("id"));
                        break;
                }
            }
            catch
            {
                // do nothing
            }
            return ret;
        }

        /// <summary>
        /// Gets target link based from input list of object store files
        /// </summary>
        /// <param name="Input">List of filtered object store files</param>
        /// <param name="LinkDescriptor">The descriptor of the link</param>
        /// <param name="LinkText">The name of the link</param>
        /// <returns>Name of the link if found, UNKNOWN if otherwise</returns>
        public static string GetTargetLink(List<DlkObjectStoreFileControlRecord> Input, string LinkDescriptor, string LinkText, string popRefId = "")
        {
            string ret = "UNKNOWN";
            int firstIndex = LinkDescriptor.IndexOf("_");
            int lastIndex = LinkDescriptor.LastIndexOf("_");
            string linkIndex = LinkDescriptor.Substring(firstIndex + 1, lastIndex - (firstIndex + 1));
            string formIndex = LinkDescriptor.Substring(lastIndex + 1, (LinkDescriptor.Length - 1) - lastIndex);

            string targetForm = GetTargetForm(Input.FindAll(y => y.mControlType == "Form"), int.Parse(formIndex), popRefId);
            DlkObjectStoreFileControlRecord obj = Input.Find(y => y.mKey == targetForm);
            string formSearchString = obj.mSearchParameters;

            /* special case */
            string[] tokens = DlkString.NormalizeNonBreakingSpace(LinkText).Split(' ');
            string specialCase = formSearchString + @"/following-sibling::div[1]/descendant::*[starts-with(text(),'" + tokens.First()
                + "') and substring(text(), string-length(text()) - string-length('" + tokens.Last() + "') +1) = '" + tokens.Last() + "']";

            DlkObjectStoreFileControlRecord sc = Input.Find(y => y.mSearchParameters == specialCase);

            if (sc != null)
            {
                ret = sc.mKey;
            }
            else
            {
                string searchstring = formSearchString + @"/following-sibling::div[1]/descendant::*[substring-after(@clickh,',')='" + linkIndex + "']";
                ret = Input.Find(y => y.mSearchParameters == searchstring).mKey;
            }
            return ret;
        }

        /// <summary>
        /// Gets target button based from input list of object store files
        /// </summary>
        /// <param name="Input">List of filtered object store files</param>
        /// <param name="ButtonDescriptor">The class-id tandem of the control</param>
        /// <param name="AncestorFormIndex">Index of form</param>
        /// <returns>Name of button if found, UNKNOWN otherwise</returns>
        public static string GetTargetButton(List<DlkObjectStoreFileControlRecord> Input, string ButtonDescriptor, int AncestorFormIndex, string popRefId = "")
        {
            string ret = "UNKNOWN";

            string relevantID = ButtonDescriptor.Split('~').First();
            string targetClass = ButtonDescriptor.Split('~').Last();

            switch (targetClass)
            {
                case "btnSpan": // the 'Log In' label in the log-in screen is not being handled
                    if (AncestorFormIndex == -1 && Input.Any(ctrl => ctrl.mSearchParameters.Equals("loginBtn")))
                    {
                        ret = Input.Find(y => y.mSearchParameters == "loginBtn").mKey;                        
                    }
                    break;
                case "bOk":
                case "bApply":
                    {
                        // xpath will look like this:
                        //div[translate(@id,'0123456789','')='pr__PDUM_ITEMUM_CTW_']/ancestor::form[1]/../descendant::*[@id='bOk']
                        int lastIndex = popRefId.LastIndexOf("_");
                        string TrimmedPopRefId = popRefId.Substring(0, lastIndex + 1);

                        /* this xpath format will be used for forms with numeric values at the 'inside' of popRef id */
                        var partialXpath = DlkString.HasNumericChar(TrimmedPopRefId)
                            ? "//div[starts-with(@id,'" + TrimmedPopRefId + "')]/ancestor::form[1]"
                            : "//div[translate(@id,'0123456789','')='" + TrimmedPopRefId + "']/ancestor::form[1]";

                        // assemble xpath
                        var searchstring = partialXpath + @"/following-sibling::div[1]/descendant::*[@id='" + targetClass + "']";
                        ret = Input.Find(y => y.mSearchParameters == searchstring).mKey;
                        break;
                    }
                case "actBtn":
                    {
                        string targetForm = GetTargetForm(Input.FindAll(y => y.mControlType == "Form"), AncestorFormIndex, popRefId);
                        DlkObjectStoreFileControlRecord obj = Input.Find(y => y.mKey == targetForm);
                        string formSearchString = obj.mSearchParameters;
                        relevantID = relevantID.Replace("___T", "");
                        string searchstring = formSearchString + "/descendant::*[contains(@id,'" + relevantID + "') and contains(@style,'visible')]";
                        ret = Input.Find(y => y.mSearchParameters == searchstring).mKey;
                        break;
                    }
                case "fCalBtn":
                case "fCalBtn20":
                    {
                        string targetForm = GetTargetForm(Input.FindAll(y => y.mControlType == "Form"), AncestorFormIndex, popRefId);
                        DlkObjectStoreFileControlRecord obj = Input.Find(y => y.mKey == targetForm);
                        string searchstring = obj.mSearchParameters + "/descendant::*[@id='" + relevantID
                            + "']/following-sibling::*[contains(@class,'fCalBtn')]";
                        ret = Input.Find(y => y.mSearchParameters == searchstring).mKey;
                        break;
                    }
                case "fBrowseBtn":
                case "fLaunchBtn":
                    {
                        string targetForm = GetTargetForm(Input.FindAll(y => y.mControlType == "Form"), AncestorFormIndex,popRefId);
                        DlkObjectStoreFileControlRecord obj = Input.Find(y => y.mKey == targetForm);
                        string searchstring = obj.mSearchParameters + "/descendant::*[@id='" + relevantID 
                            + "']/following-sibling::*[@class='" + targetClass + "']";
                        ret = Input.Find(y => y.mSearchParameters == searchstring).mKey;
                        break;
                    }
            }
            
            return ret;
        }

        /// <summary>
        /// Gets target radio button based from input list of object store files
        /// </summary>
        /// <param name="Input">List of filtered object store files</param>
        /// <param name="Descriptor">The ID-value tandem of the control</param>
        /// <param name="AncestorFormIndex">Index of form</param>
        /// <returns>Name of radio button if found, UNKNOWN otherwise</returns>
        public static string GetTargetRadioButton(List<DlkObjectStoreFileControlRecord> Input, string Descriptor, int AncestorFormIndex, string popRefId ="")
        {
            string ret = "UNKNOWN";

            try
            {
                string relevantId = Descriptor.Split('~').First();
                string targetVal = Descriptor.Split('~').Last();
                string searchstring = "//*[@id='" + relevantId + "' and @value='" + targetVal + "']";

                if (AncestorFormIndex >= 0)
                {
                    string targetForm = GetTargetForm(Input.FindAll(y => y.mControlType == "Form"), AncestorFormIndex, popRefId);
                    DlkObjectStoreFileControlRecord obj = Input.Find(y => y.mKey == targetForm);
                    string formSearchString = obj.mSearchParameters;
                    searchstring = formSearchString + "/descendant::*[@id='" + relevantId + "' and @value='" + targetVal + "']";
                }

                ret = Input.Find(y => y.mSearchParameters == searchstring).mKey;
            }
            catch
            {
                /* Do nothing */
            }

            return ret;
        }

        /// <summary>
        /// Gets target table based from input list of object store files
        /// </summary>
        /// <param name="Input">List of filtered object store files</param>
        /// <param name="AncestorFormIndex">Index of form</param>
        /// <returns>Name of table if found, UNKNOWN otherwise</returns>
        public static string GetTargetTable(List<DlkObjectStoreFileControlRecord> Input, int AncestorFormIndex, string popRefId="")
        {
            string ret = "UNKNOWN";

            try
            {
                string targetForm = GetTargetForm(Input.FindAll(y => y.mControlType == "Form"), AncestorFormIndex, popRefId);
                DlkObjectStoreFileControlRecord obj = Input.Find(y => y.mKey == targetForm);
                string searchstring = obj.mSearchParameters + "/descendant::*[@id='dTbl']";
                ret = Input.Find(y => y.mSearchParameters == searchstring).mKey;
            }
            catch
            {
                /* Do nothing */
            }

            return ret;
        }

        /// <summary>
        /// Gets target control based from input list of object store files
        /// </summary>
        /// <param name="Input">List of filtered object store file</param>
        /// <param name="TextBoxDescriptor">The ID name of the control</param>
        /// <param name="AncestorFormIndex">Index of form</param>
        /// <returns>Name of control if found, UNKNOWN otherwise</returns>
        public static string GetTargetControl(List<DlkObjectStoreFileControlRecord> Input, string TextBoxDescriptor, int AncestorFormIndex, string popRefId = "")
        {
            string ret = "UNKNOWN";

            try
            {
                string targetForm = GetTargetForm(Input.FindAll(y => y.mControlType == "Form"), AncestorFormIndex, popRefId);
                DlkObjectStoreFileControlRecord obj = Input.Find(y => y.mKey == targetForm);
                string searchstring = obj.mSearchParameters + "/descendant::*[@id='" + TextBoxDescriptor + "']";
                ret = Input.Find(y => y.mSearchParameters == searchstring).mKey;
            }
            catch
            {
                /* Do nothing */
            }

            return ret;
        }

        /// <summary>
        /// Get target form name based from input list of object store files
        /// </summary>
        /// <param name="Input">List of filtered object store files</param>
        /// <param name="AncestorFormIndex">Index of form</param>
        /// <returns>Name of form if found, UNKNOWN otherwise</returns>
        public static string GetTargetForm(List<DlkObjectStoreFileControlRecord> Input, int AncestorFormIndex, string popRefId = "")
        {
            string ret = "UNKNOWN";
            try
            {
                DlkObjectStoreFileControlRecord obj = null;

                /* Main form */
                if (AncestorFormIndex == 0)
                {
                    obj = Input.Find(y => y.mSearchParameters == "//div[@id='0']/form[1]");
                    ret = obj.mKey;
                }
                /* Sub Forms */
                else
                {
                    string subFormId = "";
                    if (string.IsNullOrWhiteSpace(popRefId))
                    {
                        // contol flow will go here for any control inside the subForm.
                        IWebElement frm = DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@id='" + AncestorFormIndex + "']/form[1]"));
                        IWebElement lbl = frm.FindElements(By.XPath("./descendant::div[@class='popRef']")).First();
                        DlkBaseControl ctl = new DlkBaseControl("Label", lbl); 
                        subFormId = ctl.GetAttributeValue("id");
                    }
                    else
                    {
                        subFormId = popRefId;
                    }
                    int lastIndex = subFormId.LastIndexOf("_");
                    string strPartialValue = subFormId.Substring(0, lastIndex + 1);

                    /* this xpath format will be used for forms with numeric values at the 'inside' of popRef id */
                    if (DlkString.HasNumericChar(strPartialValue))
                    {
                        obj = Input.Find(y => y.mSearchParameters == "//div[starts-with(@id,'" + strPartialValue + "')]/ancestor::form[1]");
                    }

                    if (obj == null)
                    {
                        /* Default format */
                        obj = Input.Find(y => y.mSearchParameters == "//div[translate(@id,'0123456789','')='" + strPartialValue + "']/ancestor::form[1]");
                    }

                    if (obj != null)
                    {
                        ret = obj.mKey;
                    }
                }
            }
            catch
            {
            }
            return ret;
        }

        /// <summary>
        /// Gets XPATH search string of form with input index based from pre-defined xpath format template
        /// </summary>
        /// <param name="AncestorFormIndex">Index of form</param>
        /// <returns>Constructed XPATH of form</returns>
        public static string GetTargetFormXPath(int AncestorFormIndex)
        {
            string ret = "";
            IWebElement frm = DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@id='" + AncestorFormIndex + "']/form[1]"));
            try
            {
                /* Main form */
                if (AncestorFormIndex == 0)
                {
                    ret = "//div[@id='0']/form[1]";
                }
                /* Sub Forms */
                else
                {
                    IWebElement lbl = frm.FindElements(By.XPath("./descendant::div[@class='popRef']")).First();
                    DlkBaseControl ctl = new DlkBaseControl("Label", lbl);
                    string strID = ctl.GetAttributeValue("id");
                    int lastIndex = strID.LastIndexOf("_");
                    string strPartialValue = strID.Substring(0, lastIndex + 1);

                    if (DlkString.HasNumericChar(strPartialValue))
                    {
                        ret = "//div[starts-with(@id,'" + strPartialValue + "')]/ancestor::form[1]";
                    }
                    else
                    {
                        ret = "//div[translate(@id,'0123456789','')='" + strPartialValue + "']/ancestor::form[1]";
                    }
                }
            }
            catch
            {
                // ignored
            }

            return ret;
        }

        // temp
        // plan is to get app screen from metadata
        /// <summary>
        /// Get app screen from metadata
        /// </summary>
        /// <param name="DescriptorValue">The value of the descriptor</param>
        /// <returns>Returns the name of the screen by which the control belongs to</returns>
        public static string GetScreen(string DescriptorValue)
        {
            // this is a stub
            string alias;

            if (IsLoginScreen() || DescriptorValue.Equals("loginBtn") || DescriptorValue.Equals("btnSpan"))
            {
                alias = "CP7Login";
            }
            else if (IsProbablyLookUp(DescriptorValue) && !DescriptorValue.ToLower().Contains("lookup") && IsLookUp())
            {
                alias = "Lookup";
            }
            else
            {
                try
                {
                    alias = DlkEnvironment.AutoDriver.FindElements(By.ClassName("app")).First().GetAttribute("Id");
                }
                catch
                {
                    alias = "UNKNOWN";
                }
            }
            return alias;
        }

        /// <summary>
        /// Gets the descriptor
        /// </summary>
        /// <param name="Source">The control where the descriptor must be extracted</param>
        /// <returns>Returns the descriptor of the control</returns>
        public static KeyValuePair<string, string> GetDescriptor(DlkBaseControl Source, string Id, string Class)
        {
            KeyValuePair<string, string> ret;

            string ctlID = string.IsNullOrEmpty(Id) ? string.Empty : Id;
            string ctlClass = string.IsNullOrEmpty(Class) ?  string.Empty : Class;

            /* support for 701 tabs */
            ctlClass = ctlClass.Replace("Inactive", string.Empty).Replace("Active", string.Empty);

            string ctlDescriptor = ctlID;

            switch (ctlClass)
            {
                case "fBrowseBtn":
                case "fLaunchBtn":
                case "fCalBtn":
                case "fCalBtn20":
                    if (!IsPrintOptions())
                    {
                    IWebElement parCtl = Source.mElement.FindElement(By.XPath("./preceding-sibling::input[1]"));
                    ctlDescriptor = Id.Contains("submit2Q") ? ctlDescriptor : parCtl.GetAttribute("Id") + "~" + ctlClass;
                    }
                    break;
                case "actBtn":
                    ctlDescriptor = ctlID + "~" + ctlClass;
                    break;
                case "popupfCB":
                case "fCB":
                    string type = Source.GetAttributeValue("type");
                    if (type == "radio")
                    {
                        string val = Source.GetAttributeValue("value");
                        ctlDescriptor = ctlID + "~" + val;
                    }
                    break;
                case "modalTabLbl":
                    IWebElement tabBar = Source.mElement.FindElement(By.XPath("./ancestor::*[@class='modalTabBar'][1]"));
                    ctlDescriptor = tabBar.GetAttribute("id");
                    break;
                case "msgTextOld":
                case "msgText":
                case "eLnk":
                case "tlbrDDActionDiv":
                case "cust_tCCBV":
                    ctlDescriptor = ctlClass;
                    break;
                case "popupClose":
                    if (!string.IsNullOrEmpty(Id))
                    {
                        ctlDescriptor = ctlID;
                    }
                    else
                    {
                        ctlDescriptor = ctlClass;
                    }
                    break;
                case "drillDownPathText":
                    ctlDescriptor = ctlClass;
                    break;
            }
            {
                ret = new KeyValuePair<string, string>("Id", ctlDescriptor);
            }
            return ret;
        }

        /// <summary>
        /// Gets the value of the control
        /// </summary>
        /// <param name="Target">The name of the control</param>
        /// <returns>Returns the value of the control</returns>
        public static string GetControlValue(DlkBaseControl Target)
        {
            string ret = "";
            try
            {
                ret = Target.GetValue();
            }
            catch
            {
                //ignored
            }
            return ret;
        }

        /// <summary>
        /// Gets the control via Xpath
        /// </summary>
        /// <param name="xpath">Xpath location of the control</param>
        /// <param name="relativeControl">Name of the control relative to the one being located</param>
        /// <returns>Returns the control found in the given Xpath</returns>
        public static DlkBaseControl GetControlViaXPath(string xpath, DlkBaseControl relativeControl = null)
        {
            DlkBaseControl ret = null;
            try
            {
                ret = relativeControl == null ? new DlkBaseControl("Control", DlkEnvironment.AutoDriver.FindElement(By.XPath(xpath))) 
                    : new DlkBaseControl("Control", relativeControl.mElement.FindElement(By.XPath(xpath)));
            }
            catch
            {
                // ignored
            }

            return ret;
        }

        /// <summary>
        /// Gets the attribute value of a specified attribute in an element
        /// </summary>
        /// <param name="SearchType">Type of element</param>
        /// <param name="SearchString">ID of the element</param>
        /// <param name="Attribute">Name of attribute where value must be extracted</param>
        /// <returns>Returns the attribute value</returns>
        public static string GetAttributeValue(string SearchType, string SearchString, string Attribute)
        {
            string ret = "";
            try
            {
                switch (SearchType.ToLower())
                {
                    case "id":
                        IWebElement elem = DlkEnvironment.AutoDriver.FindElement(By.Id(SearchString));
                        ret = elem.GetAttribute(Attribute);
                        break;
                }
            }
            catch
            {
                ret = "";
            }
            return ret;
        }

        /// <summary>
        /// Gets the date value of an element and formats it to short date
        /// </summary>
        /// <param name="ctl">The element containing a date value</param>
        /// <returns>Returns date value in short date format</returns>
        public static string GetCalendarDateValue(DlkBaseControl ctl)
        {
            string ret = string.Empty;
            IWebElement calendar = ctl.mElement;
            try
            {
                string month = new DlkBaseControl("month", calendar.FindElement(By.XPath("./ancestor::div[@id='calDiv']//*[@id='calMo']"))).GetValue();
                string year = new DlkBaseControl("year", calendar.FindElement(By.XPath("./ancestor::div[@id='calDiv']//*[@id='calYrEdit']"))).GetValue();
                string date = string.Empty;
                if (ctl.GetAttributeValue("id").Contains("calDate"))
                {
                    date = new DlkBaseControl("date", calendar).GetValue();
                }

                ret = DateTime.Parse(month + "/" + date + "/" + year).ToShortDateString();
            }
            catch
            {
                ret = string.Empty;
            }
            return ret;
        }

        /// <summary>
        /// Gets the header of the table column
        /// </summary>
        /// <param name="ctl">The table control</param>
        /// <param name="Header">The header of the table</param>
        /// <returns>Returns the header name of the table column</returns>
        public static bool GetTableColumnHeader(DlkBaseControl ctl, string className, out string Header)
        {
            bool ret = true;
            Header = string.Empty;
            try
            {
                IWebElement cell = ctl.mElement;
                string cls = string.IsNullOrEmpty(className) ? cell.GetAttribute("class") : className;
                if (!cls.Contains("cll"))
                {
                    cell = ctl.mElement.FindElement(By.XPath("./ancestor::*[contains(@class,'cll')]"));
                }
                IReadOnlyCollection<IWebElement> siblings = cell.FindElements(By.XPath("./preceding-sibling::*"));
                int numSiblings = siblings.Count(x => x.Displayed);
                IWebElement hdr = cell.FindElement(By.XPath("./ancestor::*[@id='dTbl']/div[@id='tHdr']/div["
                    + (numSiblings + 1) + "]"));
                Header = new DlkBaseControl("HDR", hdr).GetValue().Trim();
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
        
        /// <summary>
        /// Gets message from control
        /// </summary>
        /// <param name="msg">The control containing the message</param>
        /// <returns>Returns the string value of the message control</returns>
        public static string GetMessages(DlkBaseControl msg)
        {
            List<string> mlstMessages = new List<string>();

            DlkBaseControl ctl = new DlkBaseControl("MessageArea", msg.mElement.FindElements(By.XPath(
                "./ancestor::*[@class='msgText'][1]")).First());

            IList<IWebElement> msgElements = ctl.mElement.FindElements(By.XPath(
                "//span[contains(@class,'msgTextHdr')]/following-sibling::span[contains(@class,'msgText')]"));

            foreach (IWebElement msgElement in msgElements)
            {
                IList<IWebElement> lnkMsgElements = msgElement.FindElements(By.CssSelector("a.eLnk"));
                if (lnkMsgElements.Count == 0)
                {
                    DlkBaseControl msgControl = new DlkBaseControl("Message", msgElement);
                    DlkLogger.LogInfo(msgControl.GetValue().Trim());
                    mlstMessages.Add(msgControl.GetValue().Trim());
                }
                else
                {
                    foreach (IWebElement lnkMsgElement in lnkMsgElements)
                    {
                        DlkBaseControl msgControl = new DlkBaseControl("Message", lnkMsgElement);
                        DlkLogger.LogInfo(msgControl.GetValue().Trim());
                        mlstMessages.Add(msgControl.GetValue().Trim());
                    }
                }
            }

            String sMessage = "";
            foreach (String sText in mlstMessages)
            {
                sMessage = sMessage + sText + " ";
            }
            sMessage = sMessage.Trim();

            return sMessage;
        }

        /// <summary>
        /// Gets the table row number of the table control
        /// </summary>
        /// <param name="ctl">The table control</param>
        /// <param name="Row">The row number</param>
        /// <returns>Returns the table row number of the table control</returns>
        public static bool GetTableRowNumber(DlkBaseControl ctl, out string Row)
        {
            bool ret = true;
            Row = string.Empty;
            try
            {
                IWebElement cell = ctl.mElement;
                if (!ctl.GetAttributeValue("class").Contains("cll"))
                {
                    cell = ctl.mElement.FindElement(By.XPath("./ancestor::*[contains(@class,'cll')]"));
                }

                IWebElement row = cell.FindElement(By.XPath("./.."));
                string rowId = row.GetAttribute("id");
                int count = 0;
                foreach (IWebElement rw in cell.FindElements(By.XPath("./ancestor::*[@id='dTbl']/descendant::*[@class='dRw']")))
                {
                    count++;
                    if (rw.GetAttribute("id") == rowId)
                    {
                        break;
                    }
                }
                Row = count.ToString();
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Close the driver
        /// </summary>
        public static void KillDriver()
        {
            try
            {
                DlkEnvironment.CloseSession();
            }
            catch
            {
                // do nothing
            }
        }
        #endregion
    }
}
