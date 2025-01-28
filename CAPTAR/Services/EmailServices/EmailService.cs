using CAPTAR.DTO;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace CAPTAR.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
            public EmailService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public Task SendEmail (EmailDto email)
        {
            try
            {
                var mail = new MimeMessage();
                mail.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
                mail.To.Add(MailboxAddress.Parse(email.To));
                mail.Subject = email.Subject;
                mail.Body = new TextPart(TextFormat.Html) { Text = email.Body };
                using var smtp = new SmtpClient();
                smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("Password").Value);
                smtp.Send(mail);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
            ex.ToString());
            }

            //var mail = new MimeMessage();
            //mail.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
            //mail.To.Add(MailboxAddress.Parse(email.To));
            //mail.Subject = email.Subject;
            //mail.Body = new TextPart(TextFormat.Html) { Text = email.Body };

            //using var smtp = new SmtpClient();
            //smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            //smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("Password").Value);
            //smtp.Send(mail);
            //smtp.Disconnect(true);
            return Task.CompletedTask;
        }

    }
}
