using EndProject.Application.DTOs.Auth.Profil;
using EndProject.Domain.Entitys.Identity;
using Microsoft.AspNetCore.Http;

namespace EndProject.Application.Abstraction.Services.AdminCommands;

public interface IAdminCommands
{
    Task<List<AppUser>> AllMemberUser(string? searchUser);
    Task<List<AppUser>> AllAdminUser(string? searchUser);
    Task<AppUser> ByUser(string? userId);
    Task AdminCreate(string superAdminId, string appUserId);
    Task AdminDelete(string superAdminId, string appAdminId);
    Task RemoveUser(string superAdminId, string userId);
    Task PrfileImage(string? Email, IFormFile ImageFile);
}
