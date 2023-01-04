using System;
using System.Threading;
using System.Windows.Automation;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using ControlType = System.Windows.Automation.ControlType;

namespace AjeraLib.DlkFunctions
{
    [Component("CommonDialog")]
    public static class DlkDialog
    {
        #region KEYWORDS
        
        [Keyword("FileDownload", new String[] {"C:\\test.txt", "30"})]
        public static void FileDownload(String Filename, String WaitTime)
        {
            //DlkWinApi.FileDownload(Filename, Convert.ToInt32(WaitTime) );
            String sBrowserTitle = DlkEnvironment.AutoDriver.Title;
            DlkMSUIAutomationHelper.FileDownload(sBrowserTitle, Filename, Convert.ToInt32(WaitTime));
        }

        [Keyword("ClickPrint")]
        public static void ClickPrint()
        {
            FilePrint("", "Print");
        }

        [Keyword("CancelPrint")]
        public static void CancelPrint()
        {
            FilePrint("", "Cancel");
        }

        [Keyword("SelectPrinter", new String[] { "Cavite-16Printer" })]
        public static void SelectPrinter(String PrinterName)
        {
            FilePrint(PrinterName);
        }
        
        #endregion

        #region METHODS



        public static void FilePrint(String printerName, String action = "Print")
        {
            if (printerName != "")
            {
                switch (DlkEnvironment.mBrowser.ToLower())
                {

                    case "ie":
                        SelectPrinter(printerName, "Internet Explorer");
                        break;
                    case "firefox":
                        SelectPrinter(printerName, "Mozilla Firefox");
                        break;
                    case "chrome":
                        //Print(printerName, "Google Chrome", action);
                        throw new Exception("SelectPrinter() failed. Chrome print dialog not supported.");
                }
            }
            else
            {
                switch (DlkEnvironment.mBrowser.ToLower())
                {

                    case "ie":
                        Print("Internet Explorer", action);
                        break;
                    case "firefox":
                        Print("Mozilla Firefox", action);
                        break;
                    case "chrome":
                        //Print(printerName, "Google Chrome", action);
                        throw new Exception("FilePrint() failed. Chrome print dialog not supported.");
                }
            }
            

        }

        private static void Print(String browser, String action)
        {

            AutomationElement aeDesktop = AutomationElement.RootElement;
            Condition controlCondition = Automation.ControlViewCondition;
            TreeWalker controlWalker = new TreeWalker(controlCondition);


            AutomationElement ajeraWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, "Ajera Report");

            if (ajeraWindow == null)
            {
                ajeraWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, browser);
            }

            Thread.Sleep(1000);

            AutomationElement printDialog = DlkMSUIAutomationHelper.FindWindow(ajeraWindow, controlWalker, "Print");
            if (action == "Print")
            {
                var buttonName = "OK";
                if (browser == "Internet Explorer")
                {
                    buttonName = "Print";
                }
                AutomationElement printButton = printDialog.FindFirst(TreeScope.Children,
                    new PropertyCondition(AutomationElement.NameProperty, buttonName));
                Thread.Sleep(300);
                ((InvokePattern)printButton.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
            }
            else
            {
                AutomationElement CancelButton = printDialog.FindFirst(TreeScope.Children,
                    new PropertyCondition(AutomationElement.NameProperty, "Cancel"));
                Thread.Sleep(300);
                ((InvokePattern)CancelButton.GetCurrentPattern(InvokePattern.Pattern)).Invoke();

            }

            DlkLogger.LogInfo("Successfully executed Print(). ");

        }

        private static void SelectPrinter(String printerName, String browser)
        {

            AutomationElement aeDesktop = AutomationElement.RootElement;
            Condition controlCondition = Automation.ControlViewCondition;
            TreeWalker controlWalker = new TreeWalker(controlCondition);


            AutomationElement ajeraWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, "Ajera Report");

            if (ajeraWindow == null)
            {
                ajeraWindow = DlkMSUIAutomationHelper.FindWindow(aeDesktop, controlWalker, browser);
            }

            Thread.Sleep(1000);

            AutomationElement printDialog = DlkMSUIAutomationHelper.FindWindow(ajeraWindow, controlWalker, "Print");

            if (browser == "Mozilla Firefox")
            {
                AutomationElement printerNameSelector = printDialog.FindFirst(TreeScope.Descendants,
                    new AndCondition(new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ComboBox),
                        new PropertyCondition(AutomationElement.NameProperty, "Name:")));

                ExpandCollapsePattern expandCollapsePattern =
                    printerNameSelector.GetCurrentPattern(ExpandCollapsePatternIdentifiers.Pattern) as
                        ExpandCollapsePattern;

                expandCollapsePattern.Expand();
                expandCollapsePattern.Collapse();

                //Find Printer Name
                AutomationElement listItem = printerNameSelector.FindFirst(TreeScope.Subtree,
                    new PropertyCondition(AutomationElement.NameProperty, printerName));

                if (listItem != null)
                {
                    //Select Printer Name
                    SelectionItemPattern selectionItemPattern =
                        listItem.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
                    selectionItemPattern.Select();
                    DlkLogger.LogInfo("Successfully executed SelectPrinter(): " + printerName);
                }
                else
                {
                    throw new Exception(printerName + "not found in the Printer List.");
                }
            }
            else
            {
                AutomationElement printerNameSelector = printDialog.FindFirst(TreeScope.Descendants,
                    new AndCondition(new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.List),
                        new PropertyCondition(AutomationElement.NameProperty, "Select Printer")));

                //Find Printer Name
                AutomationElement listItem = printerNameSelector.FindFirst(TreeScope.Subtree,
                    new PropertyCondition(AutomationElement.NameProperty, printerName));

                if (listItem != null)
                {
                    //Select Printer Name
                    SelectionItemPattern selectionItemPattern =
                        listItem.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;
                    selectionItemPattern.Select();
                    DlkLogger.LogInfo("Successfully executed SelectPrinter(): " + printerName);
                }
                else
                {
                    throw new Exception(printerName + "not found in the Printer List.");
                }

            }
        }

        #endregion

    }
}
