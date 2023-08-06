using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;

namespace EndProject.Domain.Entitys;

public class Comments:BaseEntity
{
    public string message { get; set; }
    public Guid? PostsId { get; set; }
    public Posts? Posts { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public List<Like> Likes { get; set; }

}

