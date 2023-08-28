using EndProject.Application.Abstraction.Services;
using Microsoft.Extensions.Options;

namespace EndProject.Infrastructure.Services.Email;

public class EmailService : IEmailService
{
    //private readonly EmailSetting _emailSetting;
    //public EmailService(IOptions<EmailSetting> emailSetting)
    //{
    //    _emailSetting = emailSetting.Value;
    //}
    public void Send(string to, string subject, string html, string from = null)
    {
        throw new NotImplementedException();
    }
}
