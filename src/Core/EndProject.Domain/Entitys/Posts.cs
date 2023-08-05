﻿using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;
using Microsoft.AspNetCore.Identity;

namespace EndProject.Domain.Entitys;

public class Posts:BaseEntity
{
    public string? message { get; set; }
    public List<PostImage>? postImage { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public List<Comments> comments { get; set; }
    public List<PostLike> postLikes { get; set; }
}








//var userId = GetUserId();
//private string GetUserId()
//{
//    var user = _contextAccessor.HttpContext.User;
//    string userId = _userManager.GetUserId(user);
//    return userId;
//}
