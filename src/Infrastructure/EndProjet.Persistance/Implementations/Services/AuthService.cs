using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs;
using EndProject.Domain.Entitys.Identity;
using EndProject.Domain.Enums.Role;
using EndProjet.Persistance.Context;
using EndProjet.Persistance.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace EndProjet.Persistance.Implementations.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _siginManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AppDbContext _appDbContext;
    private readonly IConfiguration _configuration;
    private readonly ITokenHandler _tokenHandler;

    public AuthService(UserManager<AppUser> userManager,
                      SignInManager<AppUser> siginManager,
                      RoleManager<IdentityRole> roleManager,
                      AppDbContext appDbContext,
                      IConfiguration configuration,
                      ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _siginManager = siginManager;
        _roleManager = roleManager;
        _appDbContext = appDbContext;
        _configuration = configuration;
        _tokenHandler = tokenHandler;
    }
    public Task<TokenResponseDTO> Login(LoginDTO loginDTO)
    {
        throw new Exception();   
    }

    public async Task Register(RegisterDTO registerDTO)
    {
        AppUser appUser = new AppUser()
        {
            FullName = registerDTO.Fullname,
            UserName = registerDTO.Username,
            Email = registerDTO.Email,
            IsActive = false
        };

        IdentityResult identityResult = await _userManager.CreateAsync(appUser,registerDTO.password);
        if (!identityResult.Succeeded)
        {
            StringBuilder errorMessage= new();
            foreach (var error in identityResult.Errors)
            {
                errorMessage.AppendLine(error.Description);
            }
            throw new RegistrationException(errorMessage.ToString());
        }

        var result = await _userManager.AddToRoleAsync(appUser, Role.Member.ToString());
        if (!result.Succeeded)
        {
            StringBuilder errorMessage = new();
            foreach (var error in result.Errors)
            {
                errorMessage.AppendLine(error.Description);
            }
            throw new RegistrationException(errorMessage.ToString());
        }
    }

    public Task<TokenResponseDTO> ValidRefleshToken(string refreshToken)
    {
        throw new NotImplementedException();
    }
}
