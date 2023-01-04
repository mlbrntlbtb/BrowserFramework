using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace METestHarness.Sys
{
    public class Mailer
    {
        private string mServer = string.Empty;
        private string mUserName = string.Empty;
        private string mPassword = string.Empty;
        private int mPort = 25;

        public Mailer(string Server, int Port, string UserName, string Password)
        {
            mServer = Server;
            mUserName = UserName;
            mPassword = Password;
        }

        public bool SendMail(string Subject, string Sender, string[] Recipients, string Body, bool IsHtml, out string ErrorMessage, string[] Attachments = null)
        {
            bool ret = true;
            ErrorMessage = string.Empty;
            SmtpClient client = new SmtpClient();
            MailMessage msg = new MailMessage();

            try
            {
                /* Assemble SMTP client */
                client.Host = mServer;
                client.Port = mPort;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(mUserName, mPassword);

                /* Assemble Message */
                msg.Subject = Subject ?? "Mailer Subject";
                if (!IsEmail(Sender))
                {
                    throw new Exception("Invalid sender address");
                }
                msg.From = new MailAddress(Sender);

                if (Recipients == null || Recipients.Length == 0)
                {
                    throw new Exception("No valid recipient address found");
                }
                else
                {
                    foreach (string rcpt in Recipients)
                    {
                        if (IsEmail(rcpt))
                        {
                            msg.To.Add(new MailAddress(rcpt));
                        }
                    }
                    if (!msg.To.Any())
                    {
                        throw new Exception("No valid recipient address found");
                    }
                }
                msg.Body = string.Empty;
                msg.IsBodyHtml = IsHtml;
                if (IsHtml)
                {
                    XDocument xdoc = XDocument.Load(Body);
                    msg.Body = xdoc.ToString();
                }
                else
                {
                    msg.Body = Body ?? string.Empty;
                }

                if (Attachments != null && Attachments.Any())
                {

                }
                client.Send(msg);
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                ret = false;
            }
            finally
            {
                client.Dispose();
            }
            return ret;
        }

        /// <summary>
        /// Checks if input string is valid email address
        /// </summary>
        /// <param name="inputEmail">Input email string</param>
        /// <returns>True if valid, otherwise False</returns>
        private bool IsEmail(string inputEmail)
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
    }
}
