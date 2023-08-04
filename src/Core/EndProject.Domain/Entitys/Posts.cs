using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;

namespace EndProject.Domain.Entitys;

public class Posts:BaseEntity
{
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public string? message { get; set; }
    public List<PostImage>? postImage { get; set; }
    public List<Comments> comments { get; set; }
}
