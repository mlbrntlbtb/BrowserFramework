using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows.Automation;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Collections;
using CommonLib.DlkSystem;


namespace CommonLib.DlkUtility
{
    //tilde delimited path
    public class DlkMSUIAPath : IEnumerator<string>
    {
        List<string> _paths;
        int _depth;
        int _index;

        public int Index
        {
            get { return this._index; }
        }

        public int Depth
        {
            get { return this._depth; }
        }

        public IList<string> Paths
        {
            get { return this._paths; }
        }

        public DlkMSUIAPath(string path)
        {
            var list = path.Split(new char[] { '~' }).ToList();
            _paths = new List<string>();
            _paths.AddRange(list);
            _depth = list.Count;
            _index = -1;
        }

        public void Dispose()
        {
            _paths = null;
        }

        public bool MoveNext()
        {
            _index++;
            if (_index > _paths.Count - 1)
                return false;
            else
                return true;
        }

        public void Reset()
        {
            _index = -1;
        }


        string IEnumerator<string>.Current
        {
            get { return _paths[_index]; }
        }

        object IEnumerator.Current
        {
            get { return (object)_paths[_index]; }
        }
    }

    public static class DlkMSUIAutomationHelper
    {
        //Left Button - Mouse Down 
        private const int WM_LBUTTONDOWN = 0x0201;
        //Left Button - Mouse Up 
        private const int WM_LBUTTONUP = 0x0202;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        [Flags]
        public enum MouseEventFlags : uint
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }



        public const string GovWinIQKeyword = "GovWin IQ";

        public static AutomationElement TraverseWindow(AutomationElement node,
                        TreeWalker treeWalker,
                        DlkMSUIAPath keyword)
        {
            AutomationElement element = node;
            while (keyword.MoveNext())
            {
                element = FindWindow(node, treeWalker, keyword.Paths[keyword.Index]);
                if (element != null)
                    element = TraverseWindow(element, treeWalker, keyword);
                else
                    break;
            }

            return element;
        }

        public static AutomationElement FindWindow(AutomationElement root,
                                TreeWalker treeWalker,
                                DlkMSUIAPath searchPath)
        {
            searchPath.Reset();
            return TraverseWindow(root, treeWalker, searchPath);
        }

        public static AutomationElement FindWindow(AutomationElement root,
                                        TreeWalker treeWalker,
                                        string keywordSearch,
                                        string controlType = "")
        {
            Thread.Sleep(300);
            AutomationElement window = null;


            //create content first
            bool bypassOtherTypes = false;
            List<AutomationElement> children = new List<AutomationElement>();
            AutomationElement child = treeWalker.GetFirstChild(root);

            while (child != null)
            {
                children.Add(child);
                child = treeWalker.GetNextSibling(child);
            }

            if (controlType != string.Empty)
            {
                bypassOtherTypes = true;
            }

            foreach (AutomationElement e in children)
            {
                if (bypassOtherTypes && e.Current.LocalizedControlType != controlType)
                {
                    continue;
                }

                if (e.Current.Name.Trim() != "" && e.Current.Name != null && e.Current.Name.Contains(keywordSearch))
                {
                    if (keywordSearch == DlkMSUIAutomationHelper.GovWinIQKeyword)
                    {
                        if (VerifyIfRunByWebDriver(e))
                        {
                            window = e;
                            break;
                        }
                    }
                    else
                    {
                        window = e;
                        break;
                    }
                }
            }

            if (window != null)
            {
                DlkLogger.LogInfo(string.Format("{0} found. Handle: 0x{1}.", window.Current.Name, window.Current.NativeWindowHandle.ToString("X")));
            }
            else
            {
                DlkLogger.LogInfo(string.Format(" Can't find window that contains {0}.", keywordSearch));
            }


            return window;
        }

        public static List<AutomationElement> FindAllWindow(AutomationElement root,
                                TreeWalker treeWalker,
                                string keywordSearch)
        {
            Thread.Sleep(300);
            List<AutomationElement> windowsFound = new List<AutomationElement>();


            //create content first
            List<AutomationElement> children = new List<AutomationElement>();
            AutomationElement child = treeWalker.GetFirstChild(root);

            while (child != null)
            {
                children.Add(child);
                child = treeWalker.GetNextSibling(child);
            }


            foreach (AutomationElement e in children)
            {
                if (e.Current.Name != null && e.Current.Name.Contains(keywordSearch))
                {
                    if (keywordSearch == DlkMSUIAutomationHelper.GovWinIQKeyword)
                    {
                        if (VerifyIfRunByWebDriver(e))
                        {
                            windowsFound.Add(e);
                        }
                    }
                    else
                    {
                        windowsFound.Add(e);
                    }
                }
            }

            if (windowsFound.Count > 0)
            {
                foreach (var window in windowsFound)
                {
                    DlkLogger.LogInfo(string.Format("{0} found. Handle: 0x{1}.", window.Current.Name, window.Current.NativeWindowHandle.ToString("X")));
                }
            }
            else
            {
                DlkLogger.LogInfo(string.Format(" Can't find window/s that contains {1} in {0}.", root.Current.Name, keywordSearch));
            }

            return windowsFound;
        }

        public static bool VerifyIfRunByWebDriver(AutomationElement window)
        {
            bool ret = false;

            TreeWalker walker = new TreeWalker(Automation.ContentViewCondition);

            //toolbar properties            
            AutomationElement toolbar = window.FindFirst(TreeScope.Descendants, new AndCondition(new Condition[]{
                                                                        new PropertyCondition(  AutomationElement.ControlTypeProperty, 
                                                                                                System.Windows.Automation.ControlType.ToolBar),
                                                                        new PropertyCondition( AutomationElement.NameProperty, "Add-on Bar")}));
            if (toolbar == null)
                return false;

            AutomationElement webDriverText = toolbar.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "WebDriver"));

            if (webDriverText != null)
                ret = true;

            return ret;
        }

        public static void FileDownload(String BrowserTitle, String Filename, int iWaitTimeInSecs, String action = "Save")
        {
            switch (DlkEnvironment.mBrowser.ToLower())
            {

                case "ie":
                    throw new Exception("FileDownload() failed. IE file download currently not supported.");
                case "firefox":
                    FirefoxDownload(Filename, iWaitTimeInSecs, action);
                    break;
                default:
                    // do nothing
                    // chrome will fall through here
                    break;
            }

        }

        public static void SaveFile()
        {
            AutomationElement aeDesktop = AutomationElement.RootElement;


            Condition contentCondition = Automation.ContentViewCondition;
            Condition controlCondition = Automation.ControlViewCondition;

            TreeWalker controlWalker = new TreeWalker(controlCondition);

            AutomationElement browserWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, "Mozilla Firefox");
            AutomationElement openingDialog = DlkMSUIAutomationHelper.FindWindow(browserWindow, controlWalker, "Opening");

            AutomationElementCollection aeAllRadioButton = openingDialog.FindAll(TreeScope.Descendants,
                                            new PropertyCondition(AutomationElement.ControlTypeProperty,
                                                System.Windows.Automation.ControlType.RadioButton));
            foreach (AutomationElement radio in aeAllRadioButton)
            {
                if (radio.Current.Name == "Save File")
                {
                    //get pattern here
                    ((InvokePattern)radio.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
                    Thread.Sleep(1000);
                    break;
                }
            }

            AutomationElement OKButton = openingDialog.FindFirst(TreeScope.Children,
                    new PropertyCondition(AutomationElement.NameProperty, "OK"));
            ((InvokePattern)OKButton.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
            Thread.Sleep(5000);
            List<AutomationElement> allDialog = DlkMSUIAutomationHelper.FindAllWindow(browserWindow, controlWalker, "Enter name");
            int dialogRetryLimit = 3;
            for (int num = 0; num <= dialogRetryLimit; num++)
            {
                if (!allDialog.Any())
                {
                    DlkLogger.LogInfo("Dialog not found. Retrying...");
                    Thread.Sleep(2000);
                    allDialog = DlkMSUIAutomationHelper.FindAllWindow(browserWindow, controlWalker, "Enter name");
                }
                else
                {
                    DlkLogger.LogInfo("Save dialog found. Proceeding");
                    break;
                }
            }
            AutomationElement filenameDialog = DlkMSUIAutomationHelper.FindWindow(browserWindow, controlWalker, "Enter name");
            AutomationElement fNameEditBoxElement = filenameDialog.FindFirst(TreeScope.Descendants,
                        new AndCondition(new PropertyCondition(AutomationElement.ControlTypeProperty,
                                                System.Windows.Automation.ControlType.Edit),
                                                new PropertyCondition(AutomationElement.NameProperty, "File name:")));

            AutomationElement fNameSaveButton = filenameDialog.FindFirst(TreeScope.Children,
                            new PropertyCondition(AutomationElement.NameProperty, "Save"));
            Thread.Sleep(300);
            ((InvokePattern)fNameSaveButton.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
            allDialog = DlkMSUIAutomationHelper.FindAllWindow(browserWindow, controlWalker, "Enter name");
            if (!allDialog.Any())
            {
                DlkLogger.LogInfo("Save dialog not found. Proceeding");
                return;
            }
            AutomationElement confirmDialog = DlkMSUIAutomationHelper.FindWindow(filenameDialog, controlWalker, "Confirm");
            if (confirmDialog != null)
            {
                confirmDialog.SetFocus();
                AutomationElement confirmYesButton = confirmDialog.FindFirst(TreeScope.Descendants,
                                    new PropertyCondition(AutomationElement.NameProperty, "Yes"));

                confirmYesButton.SetFocus();
                Thread.Sleep(300);
                ((InvokePattern)confirmYesButton.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
            }
        }

        public static AutomationElement GetFirstWindow(string windowTitle, string controlType, string value)
        {

            AutomationElement handle = null;
            AutomationElement aeDesktop = AutomationElement.RootElement;


            Condition contentCondition = Automation.ContentViewCondition;
            Condition controlCondition = Automation.ControlViewCondition;

            TreeWalker controlWalker = new TreeWalker(controlCondition);

            List<AutomationElement> windows = DlkMSUIAutomationHelper.FindAllWindow(aeDesktop, controlWalker, windowTitle);

            if (controlType != "")
            {
                foreach (var window in windows)
                {
                    AutomationElementCollection controls = window.FindAll(TreeScope.Descendants,
                                    new PropertyCondition(AutomationElement.LocalizedControlTypeProperty,
                                    controlType.ToLower()));

                    //search for the value
                    foreach (AutomationElement control in controls)
                    {
                        object pattern;
                        control.TryGetCurrentPattern(ValuePattern.Pattern, out pattern);
                        if (pattern != null)
                        {
                            //the control has value
                            ValuePattern valPattern = (ValuePattern)pattern;
                            if (valPattern.Current
                                .Value.ToString().ToLower()
                                .Contains(value.ToLower()))
                            {
                                handle = control;
                                break;
                            }
                        }
                        else
                        {
                            //the control does not have value, you can get the name property
                            if (control.Current.Name
                                .ToString().ToLower()
                                .Contains(value.ToLower()))
                            {
                                handle = control;
                                break;
                            }
                        }
                    }
                }

            }
            else
            {
                if (windows.Count > 0)
                    handle = windows.First();
            }


            return handle;
        }

        private static void FirefoxDownload(String Filename, int iWaitTimeInSecs, String action = "Save")
        {

            AutomationElement aeDesktop = AutomationElement.RootElement;


            Condition contentCondition = Automation.ContentViewCondition;
            Condition controlCondition = Automation.ControlViewCondition;

            TreeWalker controlWalker = new TreeWalker(controlCondition);


            AutomationElement govWinWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, DlkMSUIAutomationHelper.GovWinIQKeyword);

            if (govWinWindow == null)
            {
                govWinWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, "Mozilla Firefox");
            }

            Thread.Sleep(1000);

            AutomationElement openingDialog = DlkMSUIAutomationHelper.FindWindow(govWinWindow, controlWalker, "Opening");

            AutomationElementCollection aeAllRadioButton = openingDialog.FindAll(TreeScope.Descendants,
                                            new PropertyCondition(AutomationElement.ControlTypeProperty,
                                                System.Windows.Automation.ControlType.RadioButton));

            if (action == "Open")
            {
                AutomationElement OpenButton = openingDialog.FindFirst(TreeScope.Descendants,
                                new PropertyCondition(AutomationElement.NameProperty, "Open with"));
                ((SelectionItemPattern)OpenButton.GetCurrentPattern(SelectionItemPattern.Pattern)).Select();
            }
            else
            {
                AutomationElement SaveButton = openingDialog.FindFirst(TreeScope.Descendants,
                                    new PropertyCondition(AutomationElement.NameProperty, "Save File"));
                ((SelectionItemPattern)SaveButton.GetCurrentPattern(SelectionItemPattern.Pattern)).Select();
            }



            AutomationElement OKButton = openingDialog.FindFirst(TreeScope.Children,
                    new PropertyCondition(AutomationElement.NameProperty, "OK"));

            Thread.Sleep(300);
            ((InvokePattern)OKButton.GetCurrentPattern(InvokePattern.Pattern)).Invoke();

            //refresh from the top as previous govWInWindow could have changed
            govWinWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, DlkMSUIAutomationHelper.GovWinIQKeyword);

            if (action == "Open")
            {
                {
                    govWinWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, "Microsoft Excel");
                }

                AutomationElement xlsDialog = DlkMSUIAutomationHelper.FindWindow(govWinWindow, controlWalker, "Microsoft Excel");

                AutomationElement confirmYesButton = xlsDialog.FindFirst(TreeScope.Descendants,
                                        new PropertyCondition(AutomationElement.NameProperty, "Yes"));

                confirmYesButton.SetFocus();
                Thread.Sleep(300);
                ((InvokePattern)confirmYesButton.GetCurrentPattern(InvokePattern.Pattern)).Invoke();

                DlkLogger.LogInfo("Successfully executed FileOpen(): " + Filename);
            }
            else
            {
                if (govWinWindow == null)
                {
                    govWinWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, "Mozilla Firefox");
                }
                AutomationElement filenameDialog = DlkMSUIAutomationHelper.FindWindow(govWinWindow, controlWalker, "Enter name");

                AutomationElement fNameEditBoxElement = filenameDialog.FindFirst(TreeScope.Descendants,
                            new AndCondition(new PropertyCondition(AutomationElement.ControlTypeProperty,
                                                    System.Windows.Automation.ControlType.Edit),
                                                    new PropertyCondition(AutomationElement.NameProperty, "File name:")));

                ValuePattern fNameEditBox = (ValuePattern)fNameEditBoxElement.GetCurrentPattern(ValuePattern.Pattern);

                fNameEditBox.SetValue(Filename);

                AutomationElement fNameSaveButton = filenameDialog.FindFirst(TreeScope.Children,
                                new PropertyCondition(AutomationElement.NameProperty, action));


                Thread.Sleep(300);
                ((InvokePattern)fNameSaveButton.GetCurrentPattern(InvokePattern.Pattern)).Invoke();

                AutomationElement confirmDialog = DlkMSUIAutomationHelper.FindWindow(filenameDialog, controlWalker, "Confirm");
                if (confirmDialog != null)
                {
                    confirmDialog.SetFocus();
                    AutomationElement confirmYesButton = confirmDialog.FindFirst(TreeScope.Descendants,
                                        new PropertyCondition(AutomationElement.NameProperty, "Yes"));

                    confirmYesButton.SetFocus();
                    Thread.Sleep(300);
                    ((InvokePattern)confirmYesButton.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
                }

                DlkLogger.LogInfo("Successfully executed FileDownload(): " + Filename);
            }

        }

        public static void VerifySupportedFileTypes(String BrowserTitle, String ExpectedFileTypes)
        {
            int iDialogWaitTimeInSecs = 30;
            AutomationElement aeDesktop = AutomationElement.RootElement;
            AutomationElement browserWindow = null;
            AutomationElement fileUploadDialog = null;
            List<AutomationElement> uploadDialogList = new List<AutomationElement>();
            Condition controlCondition = Automation.ControlViewCondition;
            TreeWalker controlWalker = new TreeWalker(controlCondition);
            for (int i = 0; i < iDialogWaitTimeInSecs; i++)
            {
                browserWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, BrowserTitle);
                if (browserWindow == null)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    uploadDialogList = GetDialogInfo(browserWindow, controlWalker, "Dialog");
                    if (uploadDialogList.Count(x => x != null) > 0)
                    {
                        fileUploadDialog = uploadDialogList.Find(x => x != null);
                        break;
                    }

                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
            AutomationElement fComboBoxElement = fileUploadDialog.FindFirst(TreeScope.Descendants,
                            new AndCondition(new PropertyCondition(AutomationElement.ControlTypeProperty,
                            System.Windows.Automation.ControlType.ComboBox),
                            new PropertyCondition(AutomationElement.NameProperty, "Files of type:")));
            AutomationElement listItem = fComboBoxElement.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.ControlTypeProperty, System.Windows.Automation.ControlType.ListItem));
            string nameProperty = listItem.Current.Name;
            DlkAssert.AssertEqual("VerifySupportedFileTypes", ExpectedFileTypes, nameProperty);
            DlkLogger.LogInfo("Successfully executed VerifySupportedFileTypes()");
        }

        public static void VerifyFileName(String BrowserTitle, String ExpectedFileName)
        {
            int iDialogWaitTimeInSecs = 30;
            AutomationElement aeDesktop = AutomationElement.RootElement;
            AutomationElement browserWindow = null;
            AutomationElement fileUploadDialog = null;
            List<AutomationElement> uploadDialogList = new List<AutomationElement>();
            Condition controlCondition = Automation.ControlViewCondition;
            TreeWalker controlWalker = new TreeWalker(controlCondition);

            for (int i = 0; i < iDialogWaitTimeInSecs; i++)
            {
                browserWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, BrowserTitle);
                if (browserWindow == null)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    uploadDialogList = GetDialogInfo(browserWindow, controlWalker, "Dialog");
                    if (uploadDialogList.Count(x => x != null) > 0)
                    {
                        fileUploadDialog = uploadDialogList.Find(x => x != null);
                        break;
                    }

                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
            AutomationElement fNameEditBoxElement = fileUploadDialog.FindFirst(TreeScope.Descendants,
                new AndCondition(new PropertyCondition(AutomationElement.ControlTypeProperty,
                    System.Windows.Automation.ControlType.Edit), new PropertyCondition(AutomationElement.NameProperty, "File name:")));
            ValuePattern fNameEditBox = (ValuePattern)fNameEditBoxElement.GetCurrentPattern(ValuePattern.Pattern);
            string nameProperty = fNameEditBox.Current.Value;
            DlkAssert.AssertEqual("VerifyFileName", ExpectedFileName, nameProperty);
            DlkLogger.LogInfo("Successfully executed VerifyFileName()");
        }

        public static void FileUpload(String BrowserTitle, String Filename, int iWaitTimeInSecs, bool IsControlType = false)
        {

            AutomationElement aeDesktop = AutomationElement.RootElement;
            AutomationElement browserWindow = null;
            AutomationElement fileUploadDialog = null;
            List<AutomationElement> uploadDialogList = new List<AutomationElement>();
            Condition contentCondition = Automation.ContentViewCondition;
            Condition controlCondition = Automation.ControlViewCondition;

            TreeWalker controlWalker = new TreeWalker(controlCondition);

            for (int i = 0; i < iWaitTimeInSecs; i++)
            {
                browserWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, BrowserTitle);
                if (browserWindow == null)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    uploadDialogList = GetDialogInfo(browserWindow, controlWalker, "Dialog");
                    if (uploadDialogList.Count(x => x != null) > 0)
                    {
                        fileUploadDialog = uploadDialogList.Find(x => x != null);
                        break;
                    }

                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
            }

            AutomationElement fNameEditBoxElement = fileUploadDialog.FindFirst(TreeScope.Descendants,
                new AndCondition(new PropertyCondition(AutomationElement.ControlTypeProperty,
                    System.Windows.Automation.ControlType.Edit), new PropertyCondition(AutomationElement.NameProperty, "File name:")));

            ValuePattern fNameEditBox = (ValuePattern)fNameEditBoxElement.GetCurrentPattern(ValuePattern.Pattern);
            DlkLogger.LogInfo("Filename: " + Filename);

            /* Allow for both (a) absolute path and (b) filename only, look for file in User data folder */
            fNameEditBox.SetValue(Path.IsPathRooted(Filename) ? Filename : DlkEnvironment.mDirUserData + Filename);
            bool bClicked = false;
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    //fileUploadDialog.SetFocus();
                    AutomationElement OpenButton = fileUploadDialog.FindFirst(TreeScope.Children,
                            new AndCondition(new PropertyCondition(AutomationElement.NameProperty, "Open"), new PropertyCondition(AutomationElement.ControlTypeProperty, System.Windows.Automation.ControlType.SplitButton)));
                    if (IsControlType && OpenButton == null)
                    {
                        OpenButton = fileUploadDialog.FindFirst(TreeScope.Children,
                            new AndCondition(new PropertyCondition(AutomationElement.NameProperty, "Open"),
                            new PropertyCondition(AutomationElement.ControlTypeProperty, System.Windows.Automation.ControlType.Button)));
                    }
                    System.Threading.Thread.Sleep(2000);
                    //OpenButton.SetFocus();
                    InvokePattern clickOpenButton = (InvokePattern)OpenButton.GetCurrentPattern(InvokePattern.Pattern);
                    clickOpenButton.Invoke();
                    bClicked = true;
                    break;
                }
                catch
                {
                    try
                    {
                        // assuming that the focus is still on the textbox after setting the text in the try block above.
                        SendKeys.SendWait("{ENTER}");
                        break;
                    }
                    catch
                    {
                        // retry
                    }
                }

            }
            if (!bClicked)
            {
                DlkLogger.LogInfo("FileUpload() failed. Unable to click 'Open', will now try to use keyboard event.");
                System.Threading.Thread.Sleep(1800); // wait, then check if any upload dialog still exists
                uploadDialogList.Clear();
                uploadDialogList = GetDialogInfo(browserWindow, controlWalker, "Dialog");
                if (uploadDialogList.Count(x => x != null) > 0)
                {
                    SendKeys.SendWait("{ENTER}");
                }
                System.Threading.Thread.Sleep(DlkEnvironment.mLongWaitMs);
                //check if upload still exists
                uploadDialogList.Clear();
                uploadDialogList = GetDialogInfo(browserWindow, controlWalker, "Dialog");
                if (uploadDialogList.Count(x => x != null) > 0)
                {
                    throw new Exception("FileUpload() failed. Even keyboard event doesn't work.");
                }
            }
            else
            {
                DlkLogger.LogInfo("FileUpload() : 'Open' clicked.");
            }
            //OpenButton.SetFocus();

            //SendKeys.SendWait("{ENTER}");
            System.Threading.Thread.Sleep(DlkEnvironment.mMediumWaitMs);


            DlkLogger.LogInfo("Successfully executed FileUpload(): " + Filename);

        }

        public static void FileUploadWithDelay(String BrowserTitle, String Filename, int iWaitTimeInSecs)
        {
            int afterUploadDelay = 15000;
            AutomationElement aeDesktop = AutomationElement.RootElement;
            AutomationElement browserWindow = null;
            AutomationElement fileUploadDialog = null;
            List<AutomationElement> uploadDialogList = new List<AutomationElement>();
            Condition contentCondition = Automation.ContentViewCondition;
            Condition controlCondition = Automation.ControlViewCondition;

            TreeWalker controlWalker = new TreeWalker(controlCondition);

            for (int i = 0; i < iWaitTimeInSecs; i++)
            {
                browserWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, BrowserTitle);
                if (browserWindow == null)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    uploadDialogList = GetDialogInfo(browserWindow, controlWalker, "Dialog");
                    if (uploadDialogList.Count(x => x != null) > 0)
                    {
                        fileUploadDialog = uploadDialogList.Find(x => x != null);
                        break;
                    }

                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
            }

            AutomationElement fNameEditBoxElement = fileUploadDialog.FindFirst(TreeScope.Descendants,
                new AndCondition(new PropertyCondition(AutomationElement.ControlTypeProperty,
                    System.Windows.Automation.ControlType.Edit), new PropertyCondition(AutomationElement.NameProperty, "File name:")));

            ValuePattern fNameEditBox = (ValuePattern)fNameEditBoxElement.GetCurrentPattern(ValuePattern.Pattern);
            DlkLogger.LogInfo("Filename: " + Filename);

            /* Allow for both (a) absolute path and (b) filename only, look for file in User data folder */
            fNameEditBox.SetValue(Path.IsPathRooted(Filename) ? Filename : DlkEnvironment.mDirUserData + Filename);
            bool bClicked = false;
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    //fileUploadDialog.SetFocus();
                    AutomationElement OpenButton = fileUploadDialog.FindFirst(TreeScope.Children,
                            new AndCondition(new PropertyCondition(AutomationElement.NameProperty, "Open"), new PropertyCondition(AutomationElement.ControlTypeProperty, System.Windows.Automation.ControlType.SplitButton)));
                    System.Threading.Thread.Sleep(2000);
                    //OpenButton.SetFocus();
                    InvokePattern clickOpenButton = (InvokePattern)OpenButton.GetCurrentPattern(InvokePattern.Pattern);
                    clickOpenButton.Invoke();
                    bClicked = true;
                    break;
                }
                catch
                {
                    try
                    {
                        // assuming that the focus is still on the textbox after setting the text in the try block above.
                        SendKeys.SendWait("{ENTER}");
                        break;
                    }
                    catch
                    {
                        // retry
                    }
                }

            }
            if (!bClicked)
            {
                DlkLogger.LogInfo("FileUpload() failed. Unable to click 'Open', will now try to use keyboard event.");
                System.Threading.Thread.Sleep(1000); // wait, then check if any upload dialog still exists
                uploadDialogList.Clear();
                uploadDialogList = GetDialogInfo(browserWindow, controlWalker, "Dialog");
                if (uploadDialogList.Count(x => x != null) > 0)
                {
                    SendKeys.SendWait("{ENTER}");
                }
                System.Threading.Thread.Sleep(afterUploadDelay);
                //check if upload still exists
                uploadDialogList.Clear();
                uploadDialogList = GetDialogInfo(browserWindow, controlWalker, "Dialog");
                if (uploadDialogList.Count(x => x != null) > 0)
                {
                    throw new Exception("FileUpload() failed. Even keyboard event doesn't work.");
                }
            }
            else
            {
                DlkLogger.LogInfo("FileUpload() : 'Open' clicked.");
            }
            //OpenButton.SetFocus();

            //SendKeys.SendWait("{ENTER}");
            System.Threading.Thread.Sleep(DlkEnvironment.mMediumWaitMs);


            DlkLogger.LogInfo("Successfully executed FileUpload(): " + Filename);

        }

        public static void MultipleFileUpload(String BrowserTitle, String FileList, int iWaitTimeInSecs)
        {
            try
            {
                AutomationElement aeDesktop = AutomationElement.RootElement;
                AutomationElement browserWindow = null;
                List<AutomationElement> uploadDialogList = new List<AutomationElement>();
                AutomationElement fileUploadDialog = null;

                Condition contentCondition = Automation.ContentViewCondition;
                Condition controlCondition = Automation.ControlViewCondition;

                TreeWalker controlWalker = new TreeWalker(controlCondition);

                for (int i = 0; i < iWaitTimeInSecs; i++)
                {
                    browserWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, BrowserTitle);
                    if (browserWindow == null)
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        uploadDialogList = GetDialogInfo(browserWindow, controlWalker);
                        if (uploadDialogList.Count(x => x != null) > 0) 
                        {
                            fileUploadDialog = uploadDialogList.Find(x => x != null);
                            break;
                        }
                        
                        else
                        {
                            Thread.Sleep(1000);
                        }
                    }
                }

                AutomationElement fNameEditBoxElement = fileUploadDialog.FindFirst(TreeScope.Descendants,
                    new AndCondition(new PropertyCondition(AutomationElement.ControlTypeProperty,
                        System.Windows.Automation.ControlType.Edit), new PropertyCondition(AutomationElement.NameProperty, "File name:")));

                ValuePattern fNameEditBox = (ValuePattern)fNameEditBoxElement.GetCurrentPattern(ValuePattern.Pattern);
                List<string> listedFiles = FileList.Split('~').ToList();
                listedFiles.Sort(); //multiple files entered in Upload/Open field don't work for some reason if not alphabetical
                String constructedList = String.Empty;
                foreach (string file in listedFiles)
                {
                    constructedList += "\"" + file + "\" ";
                }
                DlkLogger.LogInfo("File names: " + constructedList);
                fNameEditBox.SetValue(DlkEnvironment.mDirUserData); //since no absolute path for multiupload, go to user directory first
                SendKeys.SendWait("{ENTER}");
                System.Threading.Thread.Sleep(2000);
                fNameEditBox.SetValue(constructedList);
                bool bClicked = false;
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        AutomationElement OpenButton = fileUploadDialog.FindFirst(TreeScope.Children,
                                new AndCondition(new PropertyCondition(AutomationElement.NameProperty, "Open"), new PropertyCondition(AutomationElement.ControlTypeProperty, System.Windows.Automation.ControlType.SplitButton)));
                        System.Threading.Thread.Sleep(2000);
                        InvokePattern clickOpenButton = (InvokePattern)OpenButton.GetCurrentPattern(InvokePattern.Pattern);
                        clickOpenButton.Invoke();
                        bClicked = true;
                        break;
                    }
                    catch
                    {
                        try
                        {
                            // assuming that the focus is still on the textbox after setting the text in the try block above.
                            SendKeys.SendWait("{ENTER}");
                            break;
                        }
                        catch
                        {
                            // retry
                        }
                    }

                }
                if (!bClicked)
                {
                    DlkLogger.LogInfo("MultipleFileUpload() failed. Unable to click 'Open', will now try to use keyboard event.");
                    System.Threading.Thread.Sleep(1000); // wait, then check if any upload dialog still exists
                    uploadDialogList.Clear();
                    uploadDialogList = GetDialogInfo(browserWindow, controlWalker);
                    if (uploadDialogList.Count(x => x != null) > 0)
                    {
                        SendKeys.SendWait("{ENTER}");
                    }
                    System.Threading.Thread.Sleep(DlkEnvironment.mMediumWaitMs);
                    //check if upload still exists
                    uploadDialogList.Clear();
                    uploadDialogList = GetDialogInfo(browserWindow, controlWalker);
                    if (uploadDialogList.Count(x => x != null) > 0)
                    {
                        throw new Exception("MultipleFileUpload() failed. Even keyboard event doesn't work.");
                    }
                }
                else
                {
                    DlkLogger.LogInfo("MultipleFileUpload() : 'Open' clicked.");
                }
                System.Threading.Thread.Sleep(DlkEnvironment.mMediumWaitMs);


                DlkLogger.LogInfo("Successfully executed MultipleFileUpload(): " + FileList);
            }
            catch (Exception ex)
            {
                throw new Exception("MultipleFileUpload() failed: " + ex.Message);
            }
        }

        //private static void IEDownload(String BrowserTitle, String Filename, int iWaitTimeInSecs)
        //{
        //    try
        //    {
        //        if(File.Exists(Filename))
        //        {
        //            File.Delete(Filename);
        //        }

        //        AutomationElement aeDesktop = AutomationElement.RootElement;
        //        AutomationElement browserWindow = null;
        //        AutomationElement fileDownloadDialog = null;

        //        Condition contentCondition = Automation.ContentViewCondition;
        //        Condition controlCondition = Automation.ControlViewCondition;

        //        TreeWalker controlWalker = new TreeWalker(controlCondition);

        //        for (int i = 0; i < iWaitTimeInSecs; i++)
        //        {
        //            browserWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, BrowserTitle);
        //            if (browserWindow == null)
        //            {
        //                Thread.Sleep(1000);
        //            }
        //            else
        //            {
        //                fileDownloadDialog = DlkMSUIAutomationHelper.FindWindow(browserWindow, controlWalker, "File Download");
        //                if (fileDownloadDialog == null)
        //                {
        //                    Thread.Sleep(1000);
        //                }
        //                else
        //                {
        //                    break;
        //                }
        //            }
        //        }

        //        AutomationElement SaveButton = fileDownloadDialog.FindFirst(TreeScope.Children,
        //                new PropertyCondition(AutomationElement.NameProperty, "Save"));

        //        fileDownloadDialog.SetFocus();
        //        Thread.Sleep(300);
        //        System.Windows.Rect rect = SaveButton.Current.BoundingRectangle;
        //        Cursor.Position = new System.Drawing.Point(Convert.ToInt32(rect.TopLeft.X + 20), Convert.ToInt32(rect.TopLeft.Y + 20));
        //        mouse_event((uint) (MouseEventFlags.LEFTDOWN | MouseEventFlags.LEFTUP | MouseEventFlags.ABSOLUTE),
        //            (uint)(rect.TopLeft.X + 20), (uint)(rect.TopLeft.Y+20), 0, UIntPtr.Zero);
        //        Thread.Sleep(1000);

        //        //refresh from the top as previous govWInWindow could have changed
        //        for (int i = 0; i < iWaitTimeInSecs; i++)
        //        {
        //            browserWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, BrowserTitle);
        //            AutomationElement saveAsDialog = DlkMSUIAutomationHelper.FindWindow(browserWindow, controlWalker, "Save As");
        //            if (saveAsDialog == null)
        //            {
        //                Thread.Sleep(1000);
        //            }
        //            else
        //            {
        //                saveAsDialog.SetFocus();
        //                SendKeys.SendWait(Filename);
        //                System.Threading.Thread.Sleep(DlkEnvironment.MediumWaitMs);
        //                SendKeys.SendWait("{ENTER}");
        //                System.Threading.Thread.Sleep(DlkEnvironment.MediumWaitMs);

        //                for (int j = 0; j < 2; j++)
        //                {
        //                    AutomationElement replaceDialog = DlkMSUIAutomationHelper.FindWindow(saveAsDialog, controlWalker, "Confirm Save As");
        //                    if (replaceDialog == null)
        //                    {
        //                        Thread.Sleep(1000);
        //                    }
        //                    else
        //                    {
        //                        AutomationElement YesButton = replaceDialog.FindFirst(TreeScope.Children,
        //                            new PropertyCondition(AutomationElement.NameProperty, "Yes"));
        //                        Thread.Sleep(300);
        //                        System.Windows.Rect YesRect = YesButton.Current.BoundingRectangle;
        //                        Cursor.Position = new System.Drawing.Point(Convert.ToInt32(YesRect.TopLeft.X + 20), Convert.ToInt32(YesRect.TopLeft.Y + 20));
        //                        mouse_event((uint)(MouseEventFlags.LEFTDOWN | MouseEventFlags.LEFTUP | MouseEventFlags.ABSOLUTE),
        //                            (uint)(YesRect.TopLeft.X + 20), (uint)(YesRect.TopLeft.Y + 20), 0, UIntPtr.Zero);
        //                        Thread.Sleep(1000);
        //                        break;    
        //                    }
        //                }
        //                break;
        //            }
        //        }

        //        for (int i = 0; i < iWaitTimeInSecs; i++)
        //        {
        //            AutomationElement downloadCompleteDialog = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, "Download complete");
        //            if (downloadCompleteDialog == null)
        //            {
        //                Thread.Sleep(1000);
        //            }
        //            else
        //            {
        //                AutomationElement closeButton = downloadCompleteDialog.FindFirst(TreeScope.Children,
        //                                    new PropertyCondition(AutomationElement.NameProperty, "Close"));

        //                closeButton.SetFocus();
        //                Thread.Sleep(300);
        //                ((InvokePattern)closeButton.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
        //                break;
        //            }
        //        }
        //        DlkLogger.LogInfo("Successfully executed FileDownload(): " + Filename);
        //    }
        //    catch (Exception e)
        //    {
        //        DlkLogger.LogException("Failed to download file.", e);
        //    }        
        //}

        private static void IE9Download(String BrowserTitle, String Filename, int iWaitTimeInSecs)
        {
            //try
            //{
            //    bool completed = false;
            //    int count = iWaitTimeInSecs;
            //    while (!completed && count > 0)
            //    {
            //        AutomationElement aeDesktop = AutomationElement.RootElement;

            //        Condition controlCondition = Automation.ControlViewCondition;

            //        TreeWalker controlWalker = new TreeWalker(controlCondition);

            //        AutomationElement mainBrowser = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, BrowserTitle);

            //        if (mainBrowser != null)
            //        {
            //            Condition notifyName = new PropertyCondition(AutomationElement.NameProperty, "Notification bar", PropertyConditionFlags.IgnoreCase);

            //            Condition notifyControlType = new PropertyCondition(AutomationElement.ControlTypeProperty,
            //                                                    System.Windows.Automation.ControlType.ToolBar);
            //            Condition notifyEnabled = new PropertyCondition(AutomationElement.IsEnabledProperty, true);
            //            Condition notifyVisible = new PropertyCondition(AutomationElement.IsOffscreenProperty, false);

            //            Condition notifyToolbarCondition = new AndCondition(notifyName, notifyControlType, notifyEnabled, notifyVisible);

            //            AutomationElement notifyToolBar = mainBrowser.FindFirst(TreeScope.Descendants, notifyToolbarCondition);

            //            //find components
            //            if (notifyToolBar != null)
            //            {
            //                //get text
            //                Condition textCondition = new PropertyCondition(AutomationElement.ControlTypeProperty, System.Windows.Automation.ControlType.Text);
            //                AutomationElement textField = notifyToolBar.FindFirst(TreeScope.Children, textCondition);

            //                //get close button
            //                Condition closeNameCondition = new PropertyCondition(AutomationElement.NameProperty, "Close", PropertyConditionFlags.IgnoreCase);
            //                AutomationElement closeButton = notifyToolBar.FindFirst(TreeScope.Children, closeNameCondition);

            //                //get open button
            //                Condition openNameCondition = new PropertyCondition(AutomationElement.NameProperty, "Open", PropertyConditionFlags.IgnoreCase);
            //                AutomationElement openButton = notifyToolBar.FindFirst(TreeScope.Children, openNameCondition);

            //                //get cancel button
            //                Condition cancelNameCondition = new PropertyCondition(AutomationElement.NameProperty, "Cancel", PropertyConditionFlags.IgnoreCase);
            //                AutomationElement cancelButton = notifyToolBar.FindFirst(TreeScope.Children, cancelNameCondition);

            //                //get Save button
            //                Condition saveControlTypeCondition = new PropertyCondition(AutomationElement.ControlTypeProperty, System.Windows.Automation.ControlType.SplitButton);
            //                Condition saveNameCondition = new PropertyCondition(AutomationElement.NameProperty, "Save", PropertyConditionFlags.IgnoreCase);
            //                AutomationElement saveButton = notifyToolBar.FindFirst(TreeScope.Children, new AndCondition(saveNameCondition, saveControlTypeCondition));

            //                //get Save button
            //                Condition saveSplitButtonCondition = new PropertyCondition(AutomationElement.ControlTypeProperty, System.Windows.Automation.ControlType.SplitButton);
            //                AutomationElement saveSplitButton = notifyToolBar.FindFirst(TreeScope.Descendants, saveSplitButtonCondition);


            //                if (textField != null && closeButton != null && openButton != null && cancelButton != null && saveButton != null && saveSplitButton != null)
            //                {
            //                    ((InvokePattern)saveButton.GetCurrentPattern(InvokePattern.Pattern)).Invoke();

            //                    //check if download is completed
            //                    bool successFulDownload = false;
            //                    int waitSuccess = iWaitTimeInSecs;

            //                    while (!successFulDownload)
            //                    {
            //                        aeDesktop = AutomationElement.RootElement;
            //                        controlCondition = Automation.ControlViewCondition;
            //                        controlWalker = new TreeWalker(controlCondition);

            //                        mainBrowser = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, BrowserTitle);

            //                        notifyName = new PropertyCondition(AutomationElement.NameProperty, "Notification bar", PropertyConditionFlags.IgnoreCase);
            //                        notifyControlType = new PropertyCondition(AutomationElement.ControlTypeProperty,
            //                                                    System.Windows.Automation.ControlType.ToolBar);
            //                        notifyEnabled = new PropertyCondition(AutomationElement.IsEnabledProperty, true);
            //                        notifyVisible = new PropertyCondition(AutomationElement.IsOffscreenProperty, false);

            //                        notifyToolbarCondition = new AndCondition(notifyName, notifyControlType, notifyEnabled, notifyVisible);

            //                        if (mainBrowser != null)
            //                        {
            //                            //wait for successful dialog to appear
            //                            notifyToolBar = mainBrowser.FindFirst(TreeScope.Descendants, notifyToolbarCondition);

            //                            if (notifyToolBar != null)
            //                            {
            //                                //get text field
            //                                textField = notifyToolBar.FindFirst(TreeScope.Children, textCondition);

            //                                if (textField != null)
            //                                {
            //                                    string textValue = ((ValuePattern)textField.GetCurrentPattern(ValuePattern.Pattern)).Current.Value;

            //                                    if (textValue.ToLower().Contains("download has completed"))
            //                                    {
            //                                        successFulDownload = true;

            //                                        notifyToolBar = mainBrowser.FindFirst(TreeScope.Descendants, notifyToolbarCondition);
            //                                        //get close button
            //                                        closeButton = notifyToolBar.FindFirst(TreeScope.Children, closeNameCondition);

            //                                        if (closeButton != null)
            //                                        {
            //                                            ((InvokePattern)closeButton.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
            //                                        }

            //                                        completed = true;
            //                                        break;
            //                                    }
            //                                    else
            //                                    {
            //                                        notifyToolBar = mainBrowser.FindFirst(TreeScope.Descendants, notifyToolbarCondition);
            //                                        //get close button
            //                                        closeButton = notifyToolBar.FindFirst(TreeScope.Children, closeNameCondition);

            //                                        if (closeButton != null)
            //                                        {
            //                                            ((InvokePattern)closeButton.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
            //                                        }
            //                                    }
            //                                }
            //                            }

            //                            DlkLogger.LogInfo("Waiting for download verify message...");
            //                            Thread.Sleep(1000);                                        
            //                            waitSuccess--;
            //                        }
            //                    }

            //                    if (waitSuccess == 0)
            //                    {
            //                        DlkLogger.LogException("Timeout occured while trying to download in IE9.");
            //                    }

            //                    if (successFulDownload)
            //                    {
            //                        completed = true;
            //                        DlkLogger.LogInfo("Download completed.");
            //                        break;
            //                    }
            //                    else
            //                    {
            //                        DlkLogger.LogException("Unsuccessful download.");
            //                    }
            //                }
            //            }
            //        }

            //        DlkLogger.LogInfo("Retrying download...");
            //        Thread.Sleep(1000);
            //        count--;
            //    }

            //    if (count == 0)
            //    {
            //        DlkLogger.LogException("Timeout occured while downloading in IE9.");
            //    }
            //}
            //catch (Exception e)
            //{
            //    DlkLogger.LogException(e.ToString());
            //}


        }

        private static List<AutomationElement> GetDialogInfo(AutomationElement window, TreeWalker walker, string controlType = "")
        {
            List<AutomationElement> dialogList = new List<AutomationElement>();
            dialogList.Add(DlkMSUIAutomationHelper.FindWindow(window, walker, "Upload", controlType));
            dialogList.Add(DlkMSUIAutomationHelper.FindWindow(window, walker, "Open", controlType));
            dialogList.Add(DlkMSUIAutomationHelper.FindWindow(window, walker, "Choose File to Upload", controlType));
            return dialogList;
        }

        public static String GetBrowserStatusBar(String BrowserTitle, String iWaitTimeInSecs)
        {

            AutomationElement aeDesktop = AutomationElement.RootElement;
            AutomationElement browserWindow = null;
            AutomationElement frameTabPane = null;
            AutomationElement browserPane = null;
            AutomationElement statusBar = null;

            string statusBarTextProp = "";

            Condition contentCondition = Automation.ContentViewCondition;
            Condition controlCondition = Automation.ControlViewCondition;

            TreeWalker controlWalker = new TreeWalker(controlCondition);

            int sec = 0;

            if (int.TryParse(iWaitTimeInSecs, out sec))
            {
                for (int i = 0; i < sec; i++)
                {
                    browserWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, BrowserTitle);
                    if (browserWindow == null)
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        frameTabPane = browserWindow.FindFirst(TreeScope.Descendants,
                            new PropertyCondition(AutomationElement.ClassNameProperty, "Frame Tab"));

                        browserPane = frameTabPane.FindFirst(TreeScope.Descendants,
                            new PropertyCondition(AutomationElement.ClassNameProperty, "TabWindowClass"));

                        statusBar = browserPane.FindFirst(TreeScope.Descendants,
                            new PropertyCondition(AutomationElement.ClassNameProperty, "msctls_statusbar32"));

                        statusBarTextProp = statusBar.GetCurrentPropertyValue(AutomationElement.NameProperty) as string;
                        break;
                    }
                }
            }

            DlkLogger.LogInfo("Successfully executed GetBrowserStatusBar().");
            return statusBarTextProp;

        }

        public static bool? IsClickDialogOkButtonClicked(System.Windows.Point ClickPoints)
        {
            //AutomationElement aeDesktop = AutomationElement.RootElement;
            //Condition controlCondition = Automation.ControlViewCondition;
            //TreeWalker controlWalker = new TreeWalker(controlCondition);
            //AutomationElement dialogWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, "The page at");

            ////AutomationElement x = browserWindow.
            bool? ret = null;
            //AutomationElement ok = browserWindow.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "OK"));

            Thread.Sleep(2000);
            AutomationElement ok = AutomationElement.FromPoint(ClickPoints);
            var name = ok.GetCurrentPropertyValue(AutomationElement.NameProperty) ?? string.Empty;
            if (name.ToString() == "OK")
            {
                ret = true;
            }
            else if (name.ToString() == "Cancel")
            {
                ret = false;
            }
            else
            {
                // do nothing
            }
            //AutomationElement ok = browserWindow.FindFirst(TreeScope.Descendants, new PropertAutomationElement.FromPoint(ClickPoints));
            //ok.GetCurrentPropertyValue(AutomationElement.point)
            return ret;
            ////return dialog != null;
            //return false;
        }
    }
}
