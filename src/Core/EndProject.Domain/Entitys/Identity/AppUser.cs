using Microsoft.AspNetCore.Identity;

namespace EndProject.Domain.Entitys.Identity;

public class AppUser:IdentityUser
{
    public bool IsActive { get; set; }
    public string? FullName { get; set; }
    public DateTime RefreshTokenExpration { get; set; }
    public string? RefreshToken { get; set; }
    public List<Group_User> group_Users { get; set; }
    public List<GroupMessage> groupMessages { get; set; }

    public List<Posts> posts { get; set; }
}
