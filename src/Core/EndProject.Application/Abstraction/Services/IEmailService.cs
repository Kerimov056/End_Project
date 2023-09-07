namespace EndProject.Application.Abstraction.Services;

public interface IEmailService
{
    void Send(string to, string subject, string html, string from = null);
    Task SendPasswordResetMailAsync(string to, string userId, string resetToken);
}
