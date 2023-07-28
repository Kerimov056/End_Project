using EndProject.Application.DTOs;

namespace EndProject.Application.Abstraction.Services;

public interface IAuthService
{
    Task Register(RegisterDTO registerDTO);
    Task<TokenResponseDTO> Login(LoginDTO loginDTO);
    Task<TokenResponseDTO> ValidRefleshToken(string refreshToken);
}
