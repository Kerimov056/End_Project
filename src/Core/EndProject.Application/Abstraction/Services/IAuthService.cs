using EndProject.Application.DTOs.Auth;
using EndProject.Domain.Entitys.Identity;
using EndProject.Domain.Helpers;

namespace EndProject.Application.Abstraction.Services;

public interface IAuthService
{
    Task<SignUpResponse> Register(RegisterDTO registerDTO);
    Task<TokenResponseDTO> Login(LoginDTO loginDTO);
    Task<TokenResponseDTO> LoginAdmin(LoginDTO loginDTO);
    Task<TokenResponseDTO> GoogleLoginAsync(string idToken, int accessTokenLifeTime);
    Task<TokenResponseDTO> ValidRefleshToken(string refreshToken);
    Task<List<AppUser>> AllMemberUser(string? searchUser);
    Task<List<AppUser>> AllAdminUser(string? searchUser);
    Task<AppUser> ByUser(string? userId);
    Task AdminCreate(string superAdminId, string appUserId);
    Task AdminDelete(string superAdminId, string appAdminId);
    Task RemoveUser(string superAdminId, string userId);
}
