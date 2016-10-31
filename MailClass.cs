using System;
using System.Net.Mail;

namespace SEGP
{
    class MailClass
    {

        public MailClass(String to, String sms)
        {
            MailMessage mail = new MailMessage("rahman.khan803@gmail.com", to, "Password Has Been Changed", sms);
            SmtpClient clinet = new SmtpClient("smtp.gmail.com");
            clinet.Port = 587;
            clinet.Credentials = new System.Net.NetworkCredential("rahman.khan803@gmail.com", "khushbot");
            clinet.EnableSsl = true;
            clinet.Send(mail);
        }

    }
}
