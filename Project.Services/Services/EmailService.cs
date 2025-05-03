using MimeKit;
using MailKit.Net.Smtp;
using Project.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Project.Services.Services
{
    public class EmailService : IEmailService
    {

        private readonly string? FromName;
        private readonly string? FromEmail;
        private readonly string? SmtpServer;
        private readonly int SmtpPort;
        private readonly string? SmtpUser;
        private readonly string? SmtpPass;

        public EmailService(IConfiguration configuration)
        {
            FromName = configuration["EmailSettings:FromName"];
            FromEmail = configuration["EmailSettings:FromEmail"];
            SmtpServer = configuration["EmailSettings:SmtpServer"];
            SmtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
            SmtpUser = configuration["EmailSettings:SmtpUser"];
            SmtpPass = configuration["EmailSettings:SmtpPass"];
        }
        public async Task SendEmailAsync(string toName, string toEmail, string subject, string htmlBody)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(FromName, FromEmail));
            email.To.Add(new MailboxAddress(toName, toEmail));
            email.Subject = subject;
            var bodyBuilder = new BodyBuilder { HtmlBody = htmlBody };
            email.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(SmtpServer, SmtpPort, false);
                await client.AuthenticateAsync(SmtpUser, SmtpPass);
                await client.SendAsync(email);
                await client.DisconnectAsync(true);
            }
        }

        public async Task SendReportEmailAsync(string name, string address, string htmlBody, string deleteLink)
        {
            string subject = "התקבל דיווח על תוכן פוגעני";
            string body = $@"
        <div dir='rtl'>
            <p><strong>להלן התוכן של הפוסט המדווח:</strong><br/>{htmlBody}</p>
            <p><a href=""{deleteLink}"">למחיקה לחץ כאן</a></p>
        </div>";
            await SendEmailAsync(name, address, subject, body);
        }

        public async Task SendContactCounselorEmailAsync(string counselorName, string counselorEmail, string userName, string userEmail, string htmlBody)
        {
            string subject = "משתמש מהאתר מבקש ליצור איתך קשר";
            string body = $@"
                <div dir='rtl'>
                    <p>שלום וברכה {counselorName}!</p>
                    <p>משתמש בשם {userName} עם כתובת המייל {userEmail} מבקש ליצור אתך קשר.</p>
                    <p><strong>זה תוכן ההודעה:</strong><br> {htmlBody}</p>
                    <p>אנו מודים לך על תרומתך החשובה לקידום הפורום, ומאחלים לך הצלחה רבה בהמשך התהליך הייעוצי!</p>
                </div>";
            await SendEmailAsync(counselorName, counselorEmail, subject, body);
        }

        //Contact the site manager
        public async Task SendContactEmailAsync(string name, string address, string userName, string userEmail, string htmlBody)
        {
            string subject = "משתמש מהאתר מבקש ליצור איתך קשר";
            string body = $@"
                <div dir='rtl'>
                    <p>שלום וברכה {name}!</p>
                    <p>משתמש בשם {userName} עם כתובת המייל {userEmail} מבקש ליצור אתך קשר.</p>
                    <p><strong>זה תוכן ההודעה:</strong><br/> {htmlBody}</p>
                </div>";
            await SendEmailAsync(name, address, subject, body);
        }
    }

}

