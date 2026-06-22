using System.Net;
using System.Net.Mail;

namespace CustomerCommLib
{
    /// <summary>
    /// Concrete implementation that talks to a real SMTP server.
    /// This class CANNOT be unit tested directly because it requires
    /// an actual SMTP connection — that is why we use the IMailSender
    /// interface and Moq to replace it during tests.
    /// </summary>
    public class MailSender : IMailSender
    {
        public bool SendMail(string toAddress, string message)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("your_email_address@gmail.com");
            mail.To.Add(toAddress);
            mail.Subject = "Test Mail";
            mail.Body = message;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new NetworkCredential("username", "password");
            SmtpServer.EnableSsl = true;

            // In production this actually connects to Gmail.
            // In tests we NEVER reach this line — the mock intercepts the call.
            SmtpServer.Send(mail);

            return true;
        }
    }
}
