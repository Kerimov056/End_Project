using EndProject.Application.DTOs.Auth;
using EndProject.Domain.Helpers;
using Microsoft.AspNetCore.Identity;

namespace EndProject.Application.Abstraction.Services;

public interface IAuthService
{
    Task<SignUpResponse> Register(RegisterDTO registerDTO);
    Task<TokenResponseDTO> Login(LoginDTO loginDTO);
    Task<TokenResponseDTO> ValidRefleshToken(string refreshToken);
    Task<LoginDTO> ExternalLogin(ExternalLoginInfo info);
}
