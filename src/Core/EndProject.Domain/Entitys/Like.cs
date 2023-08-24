using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EndProject.Domain.Entitys;

public class Like : BaseEntity
{
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public Guid CarCommentId { get; set; }
    public CarComment CarComment { get; set; }
}
