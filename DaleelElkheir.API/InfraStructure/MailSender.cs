using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace DaleelElkheir.API.Infrastructure
{
    public class MailSender
    {
        //Satrt Send Email Function
        public bool SendMail(string to, string subject, string body, bool isBodyHtml = true, string cc = "")
        {

            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            string msg = string.Empty;
            try
            {
                MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["SendMail.From"]);
                string toaddress = to;
                message.From = fromAddress;
                message.To.Add(to);
                message.Subject = subject;
                if (!string.IsNullOrEmpty(cc))
                {
                    message.CC.Add(cc);
                }
               
                message.IsBodyHtml = true;
                message.Body = body;
                string fromPassword = ConfigurationManager.AppSettings["SendMail.Password"];
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = ConfigurationManager.AppSettings["SendMail.Host"];
                    smtp.Port = int.Parse(ConfigurationManager.AppSettings["SendMail.Port"]);
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SendMail.From"], fromPassword);
                    smtp.Timeout = 20000;

                }
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                //ExceptionLog.Log(ex, new { to, subject, body, isBodyHtml });
                //var id = Log_ExceptionRepository.Log(ex);
                return true;
                //throw ex;
            }
            return true;
        }
        //End Send Email Function
    }

    public class MailSubscripeSender
    {
        //Satrt Send Email Function
        public bool SendMail(string to, string subject, string body, bool isBodyHtml = true, string cc = "")
        {

            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            string msg = string.Empty;
            try
            {
                MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["SendSubscripeMail.From"]);
                string toaddress = to;
                message.From = fromAddress;
                message.To.Add(to);
                message.Subject = subject;
                if (!string.IsNullOrEmpty(cc))
                {
                    message.CC.Add(cc);
                }

                message.IsBodyHtml = true;
                message.Body = body;
                string fromPassword = ConfigurationManager.AppSettings["SendSubscripeMail.Password"];
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = ConfigurationManager.AppSettings["SendSubscripeMail.Host"];
                    smtp.Port = int.Parse(ConfigurationManager.AppSettings["SendSubscripeMail.Port"]);
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SendSubscripeMail.From"], fromPassword);
                    smtp.Timeout = 20000;

                }
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                //ExceptionLog.Log(ex, new { to, subject, body, isBodyHtml });
                //var id = Log_ExceptionRepository.Log(ex);
                return true;
                //throw ex;
            }
            return true;
        }
        //End Send Email Function
    }
}