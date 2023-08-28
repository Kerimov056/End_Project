namespace EndProject.Domain.Helpers.AccountSetting;

public class EmailSetting
{
    public string From { get; set; }
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
