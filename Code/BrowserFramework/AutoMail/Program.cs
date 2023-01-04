using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace AutoMail
{
    class Program
    {
        #region CONSTANTS
        private const string STR_VERSION = "1.3"; // change this when upgrading
        private const string STR_LAST_MODIFIED_INITIALS = "NFS"; // change this when deploying
        private const string STR_FROM_ADDRESS = "AutomationBatch@deltek.com";
        private const string STR_DEFAULT_SUBJECT = "Batch Execution Notification";
        private const string STR_SMTP_HOST = "smtp.dummyserver.com";
        private const string STR_SMTP_USER = "dummy@dummyserver.com";
        private const string STR_SMTP_PASS = "dummypassword";
        private const int INT_SMTP_PORT = 25;


        private const string STR_HTML_STYLE_TITLE = "border:1px dotted black;border-collapse:collapse; font-style: bold; display: inline";
        private const string STR_HTML_STYLE_TABLE = "border:1px dotted black;border-collapse:collapse; font-size:13";
        private const string STR_HTML_STYLE_SUMMARY_HEADER = "border:1px dotted black; text-align:left";
        private const string STR_HTML_STYLE_SUMMARY_CELL = "border:1px dotted black; text-align:right";
        private const string STR_HTML_STYLE_DETAILS_HEADER = "border:1px dotted black";
        private const string STR_HTML_STYLE_DETAILS_STATUS_PASS = "border:1px dotted black; text-align:center; color:green; padding:5px";
        private const string STR_HTML_STYLE_DETAILS_STATUS_FAIL = "border:1px dotted black; text-align:center; color:red; padding:5px";
        private const string STR_HTML_STYLE_DETAILS_LEFT = "border:1px dotted black; text-align:left; padding:5px";
        private const string STR_HTML_STYLE_DETAILS_RIGHT = "border:1px dotted black; text-align:right; padding:5px";
        private const string STR_HTML_STYLE_DETAILS_CENTER = "border:1px dotted black; text-align:center; padding:5px";

        private const string STR_HTML_TITLE = "Browser Framework Test Execution Report";
        private const string STR_HTML_TOTAL_TEST_SCRIPTS = "Total Test Scripts";
        private const string STR_HTML_PASSED_TEST_SCRIPTS = "Passed Test Scripts";
        private const string STR_HTML_FAILED_TEST_SCRIPTS = "Failed Test Scripts";
        private const string STR_HTML_SUCCESS_RATE = "Success Rate";
        private const string STR_HTML_EXECUTION_TIME_FORMAT = "Execution Time (hh:mm:ss.ms)";

        private const string STR_HTML_TEST_RESULT = "Result";
        private const string STR_HTML_FILE_PATH = "Filename";
        private const string STR_HTML_TEST = "Test";
        private const string STR_HTML_INSTANCE = "Instance";
        private const string STR_HTML_EXECUTED_STEPS = "Executed steps";
        private const string STR_HTML_ERROR_MESSAGE = "Error Message";
        private const string STR_HTML_EXECUTION_TIME = "Execution Time";

        #endregion

        private enum Option
        {
            None,
            To,
            From,
            Bcc,
            Cc,
            Subject,
            Attachment,
            HTML,
            File,
            SMTPHost,
            SMTPPort,
            SMTPUser,
            SMTPPass
        }

        private enum RecepientType
        {
            To,
            Cc,
            Bcc
        }

        private static bool HasRecepients = false;
        private static bool IncludeAttachments = false;

        static void Main(string[] args)
        {
            Option opt = Option.None;
            MailMessage msg = new MailMessage();
            msg.Subject = STR_DEFAULT_SUBJECT;
            msg.From = new MailAddress(STR_FROM_ADDRESS);
            string inputFilePath = string.Empty;
            List<string> attachments = new List<string>();

            string SMTPHost = STR_SMTP_HOST, SMTPUser = STR_SMTP_USER, SMTPPass = STR_SMTP_PASS;
            int SMTPPort = INT_SMTP_PORT;

            try
            {
                // temporary body
                msg.Body = "Batch run completed processing in " + System.Environment.MachineName;

                if (args.Length < 1)
                {
                    DisplayHelp();
                    return;
                }

                for (int idx = 0; idx < args.Length; idx++)
                {
                    if (args[idx].StartsWith(@"/")) // option identifier
                    {
                        if (args[idx].Length != 2)
                        {
                            DisplayHelp();
                            return;
                        }
                        else
                        {
                            switch (args[idx].Substring(1).ToLower())
                            {
                                case "o":
                                    opt = Option.From;
                                    break;
                                case "t":
                                    opt = Option.To;
                                    break;
                                case "s":
                                    opt = Option.Subject;
                                    break;
                                case "a":
                                    opt = Option.Attachment;
                                    break;
                                case "b":
                                    opt = Option.Bcc;
                                    break;
                                case "c":
                                    opt = Option.Cc;
                                    break;
                                case "f":
                                    opt = Option.File;
                                    break;
                                case "h":
                                    opt = Option.HTML;
                                    break;
                                case "e":
                                    opt = Option.SMTPHost;
                                    break;
                                case "p":
                                    opt = Option.SMTPPort;
                                    break;
                                case "u":
                                    opt = Option.SMTPUser;
                                    break;
                                case "w":
                                    opt = Option.SMTPPass;
                                    break;
                                default:
                                    DisplayHelp();
                                    return;
                            }
                        }
                    }
                    else // option value
                    {
                        switch (opt)
                        {
                            case Option.Subject:
                                msg.Subject = args[idx];
                                break;
                            case Option.From:                                
                                if (!AddSender(msg, args[idx]))
                                {
                                    DisplayError("Invalid email address entered", true);
                                    return;
                                }
                                break;
                            case Option.To:
                                if (!AddRecepient(msg, args[idx], RecepientType.To))
                                {
                                    DisplayError("Invalid email address entered", true);
                                    return;
                                }
                                break;
                            case Option.HTML:
                                AddHTMLBody(msg, args[idx]);
                                break;
                            case Option.Attachment:
                                attachments.Add(args[idx]);
                                if (!AddAttachment(msg, args[idx]))
                                {
                                    DisplayError("Invalid attachment entered", true);
                                    return;
                                }
                                break;
                            case Option.Bcc:
                                if (!AddRecepient(msg, args[idx], RecepientType.Bcc))
                                {
                                    DisplayError("Invalid email address entered", true);
                                    return;
                                }
                                break;
                            case Option.Cc:
                                if (!AddRecepient(msg, args[idx], RecepientType.Cc))
                                {
                                    DisplayError("Invalid email address entered", true);
                                    return;
                                }
                                break;
                            case Option.File:
                                if (!ProcessFile(msg, args[idx]))
                                {
                                    DisplayError("Error was encountered when processing input file", true);
                                    return;
                                }
                                break;
                            case Option.None:
                                break;
                            case Option.SMTPHost:
                                SMTPHost = args[idx];
                                break;
                            case Option.SMTPPort:
                                int defaultPort = INT_SMTP_PORT;
                                Int32.TryParse(args[idx], out defaultPort);
                                SMTPPort = defaultPort;
                                break;
                            case Option.SMTPUser:
                                SMTPUser = args[idx];
                                break;
                            case Option.SMTPPass:
                                SMTPPass = args[idx];
                                break;
                            default: // un-reacheable
                                break;
                        }
                    }
                }


                // create smtp client at mail server location
                SmtpClient client = new SmtpClient();
                client.Port = SMTPPort;
                client.Host = SMTPHost;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                // add credentials
                client.Credentials = new NetworkCredential(SMTPUser, SMTPPass);

                // send message
                if (!HasRecepients)
                {
                    client.Dispose();
                }
                else
                {
                    client.Send(msg);
                }
            }
            catch (Exception e)
            {
                DisplayError(e.Message, true);
            }
        }

        /// <summary>
        /// Checks if input string is valid email address
        /// </summary>
        /// <param name="inputEmail">Input email string</param>
        /// <returns>True if valid, otherwise False</returns>
        private static bool IsEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }


        /// <summary>
        /// Display error message
        /// </summary>
        /// <param name="Message">Message to display</param>
        private static void DisplayError(string Message, bool LogToFile)
        {
            Console.WriteLine(Message);
            if (LogToFile)
            {
                string logPath = Path.Combine(Directory.GetCurrentDirectory() + @"\AutoMailLog\");
                if(!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
                
                StreamWriter sw = new StreamWriter(logPath + "AutoMail_" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Year 
                    + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + ".log");
                using (sw)
                {
                    sw.WriteLine(Message);
                }
            }
        }

        /// <summary>
        /// Display help menu
        /// </summary>
        private static void DisplayHelp()
        {
            Console.WriteLine("Email notification tool by MakatiAutomation");
            Console.WriteLine("Version: " + STR_VERSION);
            Console.WriteLine("Last Modified by: " + STR_LAST_MODIFIED_INITIALS);
            Console.WriteLine();
            Console.WriteLine("AUTOMAIL [/A] [/B] [/C] [/E] [/F] [/O] [/P] [/S] [/T] [/U] [/W]");
            Console.WriteLine("   /A          Attachment to the email mesage. Enclose in \"\" when space character is used.");
            Console.WriteLine("   /B          Recepient of email message blind carbon copy.");
            Console.WriteLine("   /C          Recepient of email message carbon copy.");
            Console.WriteLine("   /E          SMTP server address.");
            Console.WriteLine("   /F          Input file containing Email information. Enclose in \"\" when space character is used.");
            Console.WriteLine("   /O          Sender of email message.");
            Console.WriteLine("   /P          SMTP server port number.");
            Console.WriteLine("   /S          Subject of email message. Enclose in \"\" when space character is used.");
            Console.WriteLine("   /T          Recepient of email message.");
            Console.WriteLine("   /U          SMTP username.");
            Console.WriteLine("   /W          SMTP Password.");
            Console.WriteLine();
            Console.WriteLine("EXAMPLES");
            Console.WriteLine("   AutoMail /s \"Sample Subject\" /t juanvalle@deltek.com jancuevas@deltek.com /a \"C:\\sample.txt\"");
            Console.WriteLine("   AutoMail /f \"C:\\Data.xml\"");
        }

        /// <summary>
        /// Add attachment to mail message
        /// </summary>
        /// <param name="Message">Mail message</param>
        /// <param name="Path">Path of attachment</param>
        /// <returns>TRUE if attachment is valid file, FALSE otherwise</returns>
        private static bool AddAttachment(MailMessage Message, string Path)
        {
            
            if (File.Exists(Path))
            {
                Attachment attch = new Attachment(Path);
                Message.Attachments.Add(attch);
                return true;
            }
            return false;
        }

        private static void AddHTMLBody(MailMessage Message, string HtmlFile)
        {
            Message.Body = "";
            Message.IsBodyHtml = true;
            XDocument html = XDocument.Load(HtmlFile);
            Message.Body = html.ToString();
        }

        /// <summary>
        /// Add recepient to mail mail message
        /// </summary>
        /// <param name="Message">Mail message</param>
        /// <param name="rcpt">Recepient to add</param>
        /// <param name="Type">Type of recepient to add</param>
        /// <returns>TRUE if recipient is valid email address, FALSE otherwise</returns>
        private static bool AddRecepient(MailMessage Message, string rcpt, RecepientType Type)
        {
            if (IsEmail(rcpt))
            {
                switch (Type)
                {
                    case RecepientType.To:
                        Message.To.Add(new MailAddress(rcpt));
                        break;
                    case RecepientType.Cc:
                        Message.CC.Add(new MailAddress(rcpt));
                        break;
                    case RecepientType.Bcc:
                        Message.Bcc.Add(new MailAddress(rcpt));
                        break;
                }
                HasRecepients = true;
                return true;
            }
            return false;
        }

        private static bool AddSender(MailMessage Message, string sender)
        {
            if (IsEmail(sender))
            {
                Message.From = new MailAddress(sender);
                return true;
            }
            return false;
        }

        //private static string ProcessInputFile(string path)
        //{
        //    XmlDocument doc = new XmlDocument();
        //    string[] arrInputName = new string[] { "CHANGE THIS" };
        //    string ret = null;
        //    try
        //    {
        //        doc.Load(path);
        //        for (int i 
        //        ret = doc.GetElementsByTagName(DataName).Item(0).InnerText;
        //    }
        //    catch
        //    {
        //        // do nothing
        //    }
        //    return ret;
        //}

        private static List<string> GetNodeValues(string filePath, string nodeName)
        {
            
            List<string> lstRet = new List<string>();
            XmlDocument doc = new XmlDocument();
            
            try
            {
                doc.Load(filePath); // if exception thrown, simply ignore, and return empty list
                foreach (XmlNode node in doc.GetElementsByTagName(nodeName)) // no possible exception
                {
                    //string content = isXml ? node.InnerXml : node.InnerText;
                    lstRet.Add(node.InnerText);
                }
            }
            catch
            {
                // return empty list if exception encountered
                lstRet.Clear();
            }
            return lstRet;
        }

        //private  string GetNodeValue(string filePath, string nodeName)
        //{
        //        XmlDocument doc = new XmlDocument()
        //        string[] arrInputName = new string[] { "CHANGE THIS" };
        //        string ret = null;
        //        try
        //        {
        //            foreach (XmlNode node in doc.GetElementsByTagName(nodeName))
        //            {

        //            }
        //            doc.Load(path);
        //            for (int i 
        //            ret = doc.GetElementsByTagName(DataName).Item(0).InnerText;
        //        }
        //        catch
        //        {
        //            // do nothing
        //        }
        //}

        private static bool ProcessFile(MailMessage Message, string filePath)
        {
            bool ret = true;
            try
            {
                // get To's
                foreach (string to in GetNodeValues(filePath, "To"))
                {
                    AddRecepient(Message, to, RecepientType.To);
                }

                // get Cc's
                foreach (string to in GetNodeValues(filePath, "Cc"))
                {
                    AddRecepient(Message, to, RecepientType.Cc);
                }

                // get Bcc's
                foreach (string to in GetNodeValues(filePath, "Bcc"))
                {
                    AddRecepient(Message, to, RecepientType.Bcc);
                }

                // get From
                foreach (string from in GetNodeValues(filePath, "From"))
                {
                    AddSender(Message, from);
                }

                // get subject
                foreach (string subj in GetNodeValues(filePath, "Subject"))
                {
                    if (!string.IsNullOrEmpty(subj.Trim()))
                    {
                        Message.Subject = subj.Trim();
                    }
                }

                // get attachment flag
                foreach (string attach in GetNodeValues(filePath, "AttachLogs"))
                {
                    IncludeAttachments = Convert.ToBoolean(attach);
                }

                // get attachments
                foreach (string attachment in GetNodeValues(filePath, "Attachment"))
                {
                    if (IncludeAttachments)
                    {
                        AddAttachment(Message, attachment);
                    }
                }
            }
            catch
            {
                ret = false;
            }

            return ret;

        }

        //private static void ProcessAttachments(MailMessage Message, List<string> AttachmentPaths, bool IncludeAttachments)
        //{
        //    foreach (string attachment in AttachmentPaths)
        //    {
        //        if (Path.GetExtension(attachment).ToLower().Contains("html"))
        //        {
        //            Message.Body = "";
        //            AttachHTMLBody(Message, attachment);
        //        }
        //        if (IncludeAttachments)
        //        {
        //            AddAttachment(Message, attachment);
        //        }
        //    }
        //}

        //private static void AttachHTMLBody(MailMessage Message, string filePath)
        //{
        //    bool skip = false;
        //    string linkDesc = string.Empty;
        //    string linkLog = string.Empty;
        //    bool isLinkExist = false;

        //    foreach (string line in File.ReadLines(filePath))
        //    {
        //        if (line.Contains("<h3>Test Results</h3>")) // hard code this for now for test batch execution
        //        {
        //            skip = true;
        //            linkDesc = "Test Execution";
        //            linkLog = "Execution Log";
        //            continue;
        //        }
        //        else if (line.Contains("<h3>Warnings</h3>") ||
        //            line.Contains("<h3>Modified files/folders</h3>") || line.Contains("<h3>Errors</h3>")) // hard code this for now for build
        //        {
        //            skip = true;
        //            linkDesc = "Build";
        //            linkLog = "Build Log";
        //            continue;
        //        }
        //        else if (skip && line.Contains("</table>") && !isLinkExist)
        //        {
        //            skip = false;
        //            Message.Body += "\n" + FormatAttachmentLinkToHtml("Full " + linkDesc + " Report", filePath);
        //            Message.Body += "\n" + FormatAttachmentLinkToHtml(linkLog, filePath.Replace(".html", ".txt"));
        //            isLinkExist = true;
        //        }
        //        else if (skip)
        //        {
        //            // do nothing
        //        }
        //        else
        //        {
        //            Message.Body += line;
        //        }
        //    }
        //    Message.Body = Message.Body.Replace(@"C:\TFS",  @"\\" + System.Environment.MachineName);
        //}

        //private static string FormatAttachmentLinkToHtml(string LinkLabel, string LinkPath)
        //{
        //    string ret = "<h3><a href=\"" + LinkPath + "\" target=\"\">" + LinkLabel + "</a></h3>";
        //    return ret;
        //}

        //private static bool CreateReportBody(string inputXML, MailMessage Message)
        //{
        //    try
        //    {
        //        XDocument input = XDocument.Load(inputXML);
        //        XElement title = new XElement("h3", new XAttribute("style", STR_HTML_STYLE_TITLE), STR_HTML_TITLE);
        //        XElement suitename = GetSuiteName(inputXML);
        //        XElement summaryTable = GetSummaryTable(inputXML);
        //        XElement blankHeader = new XElement("br");
        //        XElement detailsTable = GetDetailsTable(inputXML);
        //        XElement body = new XElement("body", title, suitename, summaryTable, blankHeader, detailsTable);

        //        // create html node
        //        XElement root = new XElement("html", body);
        //        Message.IsBodyHtml = true;
        //        Message.Body = root.ToString();
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        //private static XElement GetSummaryTable(string inputXML)
        //{
        //    XDocument input = XDocument.Load(inputXML);

        //    var summary = from itm in input.Descendants("summary")
        //                  select new
        //                  {
        //                      total = itm.Attribute("total").Value,
        //                      passed = itm.Attribute("passed").Value,
        //                      failed = itm.Attribute("failed").Value,
        //                      elapsed = itm.Attribute("elapsed").Value
        //                  };

        //    // total test scripts
        //    XElement totalTestScripts = new XElement("tr", 
        //        new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_TOTAL_TEST_SCRIPTS),
        //        new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), summary.First().total));

        //    // passed test scripts
        //    XElement passedTestScripts = new XElement("tr",
        //        new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_PASSED_TEST_SCRIPTS),
        //        new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), summary.First().passed));

        //    // failed test scripts
        //    XElement failedTestScripts = new XElement("tr",
        //        new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_FAILED_TEST_SCRIPTS),
        //        new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), summary.First().failed));
            
        //    // success rate
        //    double rate = int.Parse(summary.First().passed) * 100 / int.Parse(summary.First().total);
        //    XElement successRate = new XElement("tr",
        //        new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_SUCCESS_RATE),
        //        new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), rate.ToString() + "%"));

        //    // execution time
        //    XElement executionTime = new XElement("tr",
        //        new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_EXECUTION_TIME_FORMAT),
        //        new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), summary.First().elapsed));

        //    // table
        //    XElement table = new XElement("table", new XAttribute("style", STR_HTML_STYLE_TABLE),
        //        totalTestScripts, passedTestScripts, failedTestScripts, successRate, executionTime);

        //    return table;
        //}

        //private static XElement GetSuiteName(string inputXML)
        //{
        //    XDocument input = XDocument.Load(inputXML);

        //    var title = from itm in input.Descendants("summary")
        //                  select new
        //                  {
        //                      name = itm.Attribute("name").Value
        //                  };
        //    return new XElement("h4", title.First().name);
        //}

        //private static XElement GetDetailsTable(string inputXML)
        //{
        //    // create table header row
        //    XElement header = new XElement("tr",
        //        new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), "#"),
        //        //new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_FILE_PATH),
        //        new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_TEST),
        //        new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_INSTANCE),
        //        new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_EXECUTED_STEPS),
        //        new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_EXECUTION_TIME),
        //        new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_TEST_RESULT),
        //        new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_ERROR_MESSAGE));

        //    // create table rows
        //    XDocument input = XDocument.Load(inputXML);
        //    var test = from itm in input.Descendants("test")
        //               select new
        //               {
        //                   file = itm.Element("filepath").Value,
        //                   name = itm.Element("name").Value,
        //                   instance = itm.Element("instance").Value,
        //                   elapsed = itm.Element("elapsed").Value,
        //                   executed = itm.Element("executedsteps").Value + "/" + itm.Element("totalsteps").Value,
        //                   errormessage = string.IsNullOrEmpty(itm.Element("errormessage").Value) ? "-" : itm.Element("errormessage").Value,
        //                   status = itm.Element("status").Value
        //               };

        //    List<XElement> rows = new List<XElement>();
        //    int count = 0;

        //    foreach (var data in test)
        //    {
        //        XElement row = new XElement("tr",
        //            new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_CENTER), ++count),
        //            //new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_LEFT), Path.GetFileName(data.file)),
        //            new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_LEFT), data.name),
        //            new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_CENTER), data.instance),
        //            new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_CENTER), data.executed),
        //            new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_RIGHT), data.elapsed),
        //            new XElement("td",
        //                new XAttribute("style", data.status.ToLower() == "passed" ? STR_HTML_STYLE_DETAILS_STATUS_PASS : STR_HTML_STYLE_DETAILS_STATUS_FAIL),
        //                data.status),
        //            new XElement("td",
        //                new XAttribute("style", data.errormessage == "-" ? STR_HTML_STYLE_DETAILS_CENTER : STR_HTML_STYLE_DETAILS_LEFT),
        //                data.errormessage));
        //        rows.Add(row);
        //    }

        //    XElement table = new XElement("table", new XAttribute("style", STR_HTML_STYLE_TABLE), header, rows);
        //    return table;
        //}
    }
}
