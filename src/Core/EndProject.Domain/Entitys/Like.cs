using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;

namespace EndProject.Domain.Entitys;

public class Like:BaseEntity
{
    public int likeSum { get; set; }
    public Guid CommentsId { get; set; }
    public Comments Comments { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}
