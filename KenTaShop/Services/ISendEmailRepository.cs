using KenTaShop.ViewModel;
using System.Net.Mail;
using System.Net;

namespace KenTaShop.Services
{
    public interface ISendEmailRepository
    {
        bool SendEmail(EmailModel email);
    }

    public class SendEmailServices : ISendEmailRepository
    {
        private IConfiguration _confEmail;
        public SendEmailServices(IConfiguration confEmail)
        {
            _confEmail = confEmail;
        }
        public bool SendEmail(EmailModel email) 
        {
            try
            {
                var username = _confEmail["Gmail:Username"];
                var password = _confEmail["Gmail:Password"];

                var smtpClient = new SmtpClient
                {
                    Host = _confEmail["Gmail:Host"],
                    Port = int.Parse(_confEmail["Gmail:Port"]),
                    EnableSsl = bool.Parse(_confEmail["Gmail:SMTP:starttls:enable"]),
                    Credentials = new NetworkCredential(username, password)
                };

                var mailMessage = new MailMessage(username, email.ToEmail);
                mailMessage.Subject = email.Subject;
                mailMessage.Body = email.Body;
                mailMessage.IsBodyHtml = true;


                smtpClient.Send(mailMessage);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
