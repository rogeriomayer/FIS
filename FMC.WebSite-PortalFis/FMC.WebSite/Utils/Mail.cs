using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FMC.WebSite.FIS.Utils
{
    public class Mail
    {
        public static SmtpClient Smtp(string email, string password)
        {
            SmtpClient client = new SmtpClient();
            client.Port = AppSettings.PortHost;
            client.Host = AppSettings.Host;
            client.EnableSsl = false;
            //client.Timeout = 10000;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(email, password);
            return client;
        }

        public static MailMessage Message(string mailFrom, IList<string> mailTo, string subject, string message)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(mailFrom);
            foreach (string address in mailTo)
            {
                if (!string.IsNullOrEmpty(address))
                    mail.To.Add(address);
            }
            mail.Priority = MailPriority.High;
            //mail.To.Add(new MailAddress(mailTo));
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = message;
            mail.BodyEncoding = Encoding.UTF8;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            return mail;
        }
    }
}
