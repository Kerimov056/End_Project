using EndProject.Application.DTOs.Auth;
using EndProject.Domain.Entitys.Identity;

namespace EndProject.Application.Abstraction.Services;

public interface ITokenHandler
{
    public Task<TokenResponseDTO> CreateAccessToken(int minutes, int refreshTokenMinutes, AppUser appUser);
}
