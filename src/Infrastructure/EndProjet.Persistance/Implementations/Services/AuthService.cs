using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs;

namespace EndProjet.Persistance.Implementations.Services;

public class AuthService : IAuthService
{
    public Task<TokenResponseDTO> Login(LoginDTO loginDTO)
    {
        throw new NotImplementedException();
    }

    public Task Register(RegisterDTO registerDTO)
    {
        throw new NotImplementedException();
    }

    public Task<TokenResponseDTO> ValidRefleshToken(string refreshToken)
    {
        throw new NotImplementedException();
    }
}
