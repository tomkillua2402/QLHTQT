using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace QLHTQT_Aplication.Common
{
    public static class EmailHelper
    {
        private static string FromEmailAddress = System.Configuration.ConfigurationManager.AppSettings["Email"].ToString();
        private static string FromEmailPassword = System.Configuration.ConfigurationManager.AppSettings["Password"].ToString();
        public static bool isValidEmail(string emailAddress)
        {
            try
            {
                var addr = new MailAddress(emailAddress);
                return addr.Address == emailAddress;
            }
            catch
            {
                return false;
            }
        }
        public static void SendEmail(string Subject, string Body, string ToEmailAdress)
        {
            var list = new List<String>();
            list.Add(ToEmailAdress);
            SendEmail(Subject, Body, list);
        }
        public static void SendEmail(string Subject, string Body, List<String> ToEmailAdress)
        {
            var fromAddress = new MailAddress(FromEmailAddress, System.Configuration.ConfigurationManager.AppSettings["DisplayNameEmail"].ToString());
            var smtp = new SmtpClient
            {

                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, FromEmailPassword)
            };
            var message = new MailMessage();
            var toAddress = new List<MailAddress>();
            message.From = fromAddress;
            foreach (var item in ToEmailAdress)
            {
                if (isValidEmail(item))
                {
                    message.To.Add(item);
                }
            }
            message.Subject = Subject;
            message.Body = Body;
            smtp.Send(message);
        }
    }
}