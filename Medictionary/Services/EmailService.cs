using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Medictionary.Services.Interfaces;
using Medictionary.Settings;
using Microsoft.Extensions.Options;

namespace Medictionary.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
                {
                    Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
                Console.WriteLine("✅ Email sent successfully to " + email);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Failed to send email: " + ex.Message);
                throw;
            }
        }
    }
}