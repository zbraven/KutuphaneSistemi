using Microsoft.AspNetCore.Identity.UI.Services;

namespace ZeroToHeroZBS.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //Email gönderme işlemlerini buradan yapabilirsin.

            return Task.CompletedTask;
        }
    }
}
