using Microsoft.AspNetCore.Identity.UI.Services;

namespace VektorelProje.Utility
{
    public class EMailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}
