using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs;
using EndProject.Domain.Entitys.Identity;

namespace EndProject.Infrastructure.Services.Token;

public class TokenHandler : ITokenHandler
{
    public Task<TokenResponseDTO> CreateAccessToken(int minutes, int refreshTokenMinutes, AppUser appUser)
    {
        throw new NotImplementedException();
    }
}
