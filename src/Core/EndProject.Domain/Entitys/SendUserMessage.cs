using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class SendUserMessage:BaseEntity
{
    public string Email { get; set; }
    public string Message { get; set; }
}
