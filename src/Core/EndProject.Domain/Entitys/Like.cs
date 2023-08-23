using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;

namespace EndProject.Domain.Entitys;

public class Like : BaseEntity
{
    public int LikeSum { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public Guid CarCommentId { get; set; }
    public CarComment CarComment { get; set; }
}
