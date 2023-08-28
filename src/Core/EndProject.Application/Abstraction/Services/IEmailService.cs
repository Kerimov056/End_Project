namespace EndProject.Application.Abstraction.Services;

public interface IEmailService
{
    void Send(string to, string subject, string html, string from = null);
}
