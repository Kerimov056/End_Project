using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class Communication : BaseEntity
{
    public string Name { get; set; }
    public string SurName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Note { get; set; }
}
