using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EndProject.Domain.Entitys;

public class PostLike:BaseEntity
{
    public int likeSum { get; set; }
    public Guid PostsId { get; set; }
    [ForeignKey("PostsId")]
    public Posts Posts { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}
