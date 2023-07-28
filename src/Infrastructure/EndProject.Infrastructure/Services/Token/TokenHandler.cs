using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs;
using EndProject.Domain.Entitys.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace EndProject.Infrastructure.Services.Token;

public class TokenHandler : ITokenHandler 
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    public TokenHandler(UserManager<AppUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration; 
    }

    public async Task<TokenResponseDTO> CreateAccessToken(int minutes, int refreshTokenMinutes, AppUser appUser)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.Id),
            new Claim(ClaimTypes.Email, appUser.Email),
            new Claim(ClaimTypes.Name,appUser.UserName)
        };

        var roles = await
    }
}
