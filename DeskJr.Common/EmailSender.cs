using DeskJr.Common;
using Microsoft.Extensions.Options;
using MimeKit;

namespace DeskJr.Service.Concrete
{
    public class EmailSender
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailSender() { }

        public EmailSender(IOptions<SmtpSettings> smtpSetting)
        {
            _smtpSettings = smtpSetting.Value;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string template, Dictionary<string, string> variables)
        {
            foreach (var variable in variables)
            {
                template = template.Replace($"{{{variable.Key}}}", variable.Value);
            }

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Desk JR", _smtpSettings.User));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = template };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_smtpSettings.User, _smtpSettings.Pass);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
