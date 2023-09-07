using EndProject.Application.Abstraction.Services.AdminCommands;
using EndProject.Application.Abstraction.Services.SosicalAuthentications;
using EndProject.Application.DTOs.Auth;
using EndProject.Application.DTOs.Auth.ResetPassword;
using EndProject.Domain.Entitys.Identity;
using EndProject.Domain.Helpers;

namespace EndProject.Application.Abstraction.Services;

public interface IAuthService : IExternalAuthentications, IAdminCommands
{
    Task<SignUpResponse> Register(RegisterDTO registerDTO);
    Task<TokenResponseDTO> Login(LoginDTO loginDTO);
    Task<TokenResponseDTO> LoginAdmin(LoginDTO loginDTO);
    Task<TokenResponseDTO> ValidRefleshToken(string refreshToken);
    Task PasswordResetAsnyc(string email);
    Task<bool> ResetPassword(ResetPassword resetPassword);

}
