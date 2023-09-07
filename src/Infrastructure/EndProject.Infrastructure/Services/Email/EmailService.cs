using EndProject.Application.Abstraction.Services;
using EndProject.Domain.Helpers.AccountSetting;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System.Text;

namespace EndProject.Infrastructure.Services.Email;

public class EmailService : IEmailService
{
    private readonly EmailSetting _emailSetting;
    public EmailService(IOptions<EmailSetting> emailSetting)
     =>  _emailSetting = emailSetting.Value;
    public void Send(string to, string subject, string html, string from = null)
    {
        // create email message
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(from ?? _emailSetting.From));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = html };

        // send email
        using var smtp = new SmtpClient();
        smtp.Connect(_emailSetting.SmtpServer, _emailSetting.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(_emailSetting.Username, _emailSetting.Password);
        smtp.Send(email);
        smtp.Disconnect(true);
    }

    public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
    {
        StringBuilder mail = new();
        mail.AppendLine("Merhaba <br/>  Eger yeni sifre talebinde bulunduysaniz asagdaki link'e click ederek kecid yapa bilirsiniz." +
            "<br/><strong><a target=\"_blank\" href=\"............./");
        mail.AppendLine(userId);
        mail.AppendLine("/");
        mail.AppendLine(resetToken);
        mail.AppendLine("\">Yeni şifre talebi için tıklayınız...</a></strong><br><br><span style=\"font-size:12px;\">NOT : Eğer ki bu talep tarafınızca gerçekleştirilmemişse lütfen bu maili ciddiye almayınız.</span><br>Saygılarımızla...<br><br><br>LD - LuxeDrive");

        Send(to, "Şifre Yenileme Talebi", mail.ToString());
    }
}
