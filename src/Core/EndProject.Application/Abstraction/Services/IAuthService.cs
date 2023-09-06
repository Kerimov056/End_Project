using EndProject.Application.DTOs.Auth;
using EndProject.Domain.Entitys.Identity;
using EndProject.Domain.Helpers;

namespace EndProject.Application.Abstraction.Services;

public interface IAuthService
{
    Task<SignUpResponse> Register(RegisterDTO registerDTO);
    Task<TokenResponseDTO> Login(LoginDTO loginDTO);
    Task<TokenResponseDTO> LoginAdmin(LoginDTO loginDTO);
    Task<TokenResponseDTO> ValidRefleshToken(string refreshToken);
    Task<List<AppUser>> AllMemberUser();
    Task AdminCreate(string superAdminId, string appUserId);
    //Task<LoginDTO> ExternalLogin(ExternalLoginInfo info);
}
