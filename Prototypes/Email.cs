using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;

namespace Prototype_Library
{
    public class Email
    {
        public void customerEmail(string e)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("realityglitchtest@gmail.com");
            mail.To.Add(e);
            mail.Subject = "CONGRATIONS";
            mail.Body = "THANK YOU FOR SIGNING UP TO BE A CUSTOMER FOR ME";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("realityglitchtest@gmail.com", "Ql$p3%*qXN^wX5sQ");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}

