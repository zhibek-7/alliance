using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Text;

namespace Car.Models
{
    public class MessageServices
    {
        public bool Send(string smtpUserName, string smtpPassword, string smtpHost, 
            int smtpPort, string toEmail, string body) {
            try
            {
               
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Host = smtpHost;
                    smtpClient.Port = smtpPort;
                    smtpClient.UseDefaultCredentials = true;


                    smtpClient.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
                    
                    var msg = new MailMessage
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8,
                        From = new MailAddress(smtpUserName),
                        Body = body,
                        Priority = MailPriority.Normal

                    };
                    msg.To.Add(toEmail);
                    smtpClient.Send(msg);
                    return true;
                    
                }
            }
            catch { return false; }
          
        




        }

        /*


        public async static Task SendEmailAsync(string email, string subject, string message) {
            try
            {
                var _email = "jibek-7@mail.ru";
                var _epass = ConfigurationManager.AppSettings["EmailPassword"];
                var _dispName = "Devsone";
                MailMessage myMessage = new MailMessage();
                myMessage.To.Add(email);
                myMessage.From = new MailAddress(_email, _dispName);
                myMessage.Subject = subject;
                myMessage.Body = message;
                myMessage.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.live.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_email, _epass);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                    await smtp.SendMailAsync(myMessage);
                }
            }
            catch(Exception ex) {
                throw ex;
            }
        }*/

    }
}