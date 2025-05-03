namespace Project.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toName, string toEmail, string subject, string htmlBody);
        Task SendReportEmailAsync(string name, string address, string htmlBody, string deleteLink);
        Task SendContactCounselorEmailAsync(string counselorName, string counselorEmail, string userName, string userEmail, string htmlBody);
        Task SendContactEmailAsync(string name, string address, string userName, string userEmail, string htmlBody);
    }
}
