using EndProject.Application.Abstraction.Services;
using EndProject.Application.Abstraction.Services.Stroge;
using EndProject.Application.DTOs.Auth;
using EndProject.Application.DTOs.Auth.ResetPassword;
using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;
using EndProject.Domain.Enums.Role;
using EndProject.Domain.Helpers;
using EndProjet.Persistance.Context;
using EndProjet.Persistance.Exceptions;
using EndProjet.Persistance.ExtensionsMethods;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace EndProjet.Persistance.Implementations.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _siginManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private readonly IStorageFile _storageFile;

    //readonly HttpClient _httpClient;



    public AuthService(UserManager<AppUser> userManager,
                       SignInManager<AppUser> siginManager,
                       RoleManager<IdentityRole> roleManager,
                       ITokenHandler tokenHandler,
                       AppDbContext context,
                       //HttpClient httpClient,
                       IEmailService emailService,
                       IConfiguration configuration,
                       IStorageFile storageFile
        )
    {
        _userManager = userManager;
        _siginManager = siginManager;
        _roleManager = roleManager;
        _tokenHandler = tokenHandler;
        _context = context;
        _configuration = configuration;
        _emailService = emailService;
        _storageFile = storageFile;
        //_httpClient = httpClient;
    }

    public async Task AdminCreate(string superAdminId, string appUserId)
    {
        var bySuperAdmin = await _userManager.FindByIdAsync(superAdminId);
        if (bySuperAdmin is null) throw new NotFoundException("SuperAdmin Not found");
        if (bySuperAdmin == null || !await _userManager.IsInRoleAsync(bySuperAdmin, "SuperAdmin"))
            throw new UnauthorizedAccessException("You do not have permission to perform this action.");


        var targetUser = await _userManager.FindByIdAsync(appUserId);

        if (targetUser == null) throw new NotFoundException("User Not Found");

        await _userManager.RemoveFromRoleAsync(targetUser, "Member");
        await _userManager.AddToRoleAsync(targetUser, "Admin");
    }

    public async Task AdminDelete(string superAdminId, string appAdminId)
    {
        var bySuperAdmin = await _userManager.FindByIdAsync(superAdminId);
        if (bySuperAdmin is null) throw new NotFoundException("SuperAdmin Not found");
        if (bySuperAdmin == null || !await _userManager.IsInRoleAsync(bySuperAdmin, "SuperAdmin"))
            throw new UnauthorizedAccessException("You do not have permission to perform this action.");


        var targetAdmin = await _userManager.FindByIdAsync(appAdminId);

        if (targetAdmin == null) throw new NotFoundException("Admin Not Found");

        await _userManager.RemoveFromRoleAsync(targetAdmin, "Admin");
        await _userManager.AddToRoleAsync(targetAdmin, "Member");
    }

    public async Task<List<AppUser>> AllAdminUser(string? searchUser)
    {
        IQueryable<AppUser> AllUsers = _context.Users;

        if (!string.IsNullOrEmpty(searchUser))
        {
            AllUsers = AllUsers.Where(x => x.FullName.ToLower().Contains(searchUser.ToLower()) || x.Email.ToLower().Contains(searchUser.ToLower()));
        }

        var AdminList = new List<AppUser>();
        foreach (var item in await AllUsers.ToListAsync())
        {
            var userRoles = await _userManager.GetRolesAsync(item);
            if (userRoles.Contains("Admin"))
            {
                AdminList.Add(item);
            }
        }
        return AdminList;
    }

    public async Task<List<AppUser>> AllMemberUser(string? searchUser)
    {
        IQueryable<AppUser> AllUsers = _context.Users;

        if (!string.IsNullOrEmpty(searchUser))
        {
            AllUsers = AllUsers.Where(x => x.FullName.ToLower().Contains(searchUser.ToLower()) || x.Email.ToLower().Contains(searchUser.ToLower()));
        }

        var MemberList = new List<AppUser>();
        foreach (var item in await AllUsers.ToListAsync())
        {
            var userRoles = await _userManager.GetRolesAsync(item);
            if (userRoles.Contains("Member"))
            {
                MemberList.Add(item);
            }
        }
        return MemberList;
    }

    public async Task<AppUser> ByUser(string? userId)
    {
        var byUser = await _userManager.FindByIdAsync(userId);
        if (byUser is null) throw new NotFoundException("SuperAdmin Not found");
        return byUser;
    }

    private async Task<TokenResponseDTO> CreateUserExternalAsync(AppUser user, string email, string name, UserLoginInfo info)
    {
        bool result = user != null;
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = email,
                    UserName = email,
                    FullName = name,
                    IsActive = false,
                };
                var identityResult = await _userManager.CreateAsync(user);
                result = identityResult.Succeeded;
            }
        }

        if (result)
        {
            await _userManager.AddLoginAsync(user, info); //AspNetUserLogins

            TokenResponseDTO token = await _tokenHandler.CreateAccessToken(2, 3, user);
            user.RefreshToken = token.refreshToken;
            user.RefreshTokenExpration = token.refreshTokenExpration;
            await _userManager.UpdateAsync(user);
            return token;
        }
        throw new Exception("Invalid external authentication.");
    }

    public async Task<TokenResponseDTO> GoogleLoginAsync(string idToken)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { _configuration["ExternalLoginSettings:Google:Client_ID"] }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

        var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
        AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        return await CreateUserExternalAsync(user, payload.Email, payload.Name, info);
    }


    public async Task<TokenResponseDTO> Login(LoginDTO loginDTO)
    {
        AppUser appUser = await _userManager.FindByEmailAsync(loginDTO.UsernameOrEmail);
        if (appUser is null)
        {
            appUser = await _userManager.FindByNameAsync(loginDTO.UsernameOrEmail);
            if (appUser is null) throw new LogInFailerException("Invalid Login!");
        }


        SignInResult signResult = await _siginManager.CheckPasswordSignInAsync(appUser, loginDTO.password, true);
        if (!signResult.Succeeded)
        {
            throw new LogInFailerException("Invalid Login!");
        }
        //if (!appUser.IsActive)
        //{
        //    throw new UserBlockedException("User Blocked");
        //}


        var tokenResponse = await _tokenHandler.CreateAccessToken(2, 3, appUser);
        appUser.RefreshToken = tokenResponse.refreshToken;
        appUser.RefreshTokenExpration = tokenResponse.refreshTokenExpration;
        await _userManager.UpdateAsync(appUser);
        return tokenResponse;
    }

    public async Task<TokenResponseDTO> LoginAdmin(LoginDTO loginDTO)
    {
        AppUser appUser = await _userManager.FindByEmailAsync(loginDTO.UsernameOrEmail);
        if (appUser is null)
        {
            appUser = await _userManager.FindByNameAsync(loginDTO.UsernameOrEmail);
            if (appUser is null) throw new LogInFailerException("Invalid Login!");
        }

        SignInResult signResult = await _siginManager.CheckPasswordSignInAsync(appUser, loginDTO.password, true);
        if (!signResult.Succeeded) throw new LogInFailerException("Invalid Login!");

        var userRoles = await _userManager.GetRolesAsync(appUser);

        if (userRoles.Contains("Admin") || userRoles.Contains("SuperAdmin"))
        {
            var tokenResponse = await _tokenHandler.CreateAccessToken(2, 3, appUser);
            appUser.RefreshToken = tokenResponse.refreshToken;
            appUser.RefreshTokenExpration = tokenResponse.refreshTokenExpration;
            await _userManager.UpdateAsync(appUser);
            return tokenResponse;
        }
        else throw new LogInFailerException("You do not have permission to access this resource.");
    }

    public async Task<SignUpResponse> Register(RegisterDTO registerDTO)
    {
        AppUser appUser = new AppUser()
        {
            FullName = registerDTO.Fullname,
            UserName = registerDTO.Username,
            Email = registerDTO.Email,
            BirthDate = registerDTO.BirthDate,
            IsActive = false
        };

        IdentityResult identityResult = await _userManager.CreateAsync(appUser, registerDTO.password);
        if (!identityResult.Succeeded)
        {
            StringBuilder errorMessage = new();
            foreach (var error in identityResult.Errors)
            {
                errorMessage.AppendLine(error.Description);
            }
            throw new RegistrationException(errorMessage.ToString());
        }

        var result = await _userManager.AddToRoleAsync(appUser, Role.Member.ToString());
        if (!result.Succeeded)
        {
            return new SignUpResponse
            {
                StatusMessage = ExceptionResponseMessages.UserFailedMessage,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        return new SignUpResponse
        {
            Errors = null,
            StatusMessage = ExceptionResponseMessages.UserSuccesMessage,
            UserEmail = appUser.Email
        };
    }

    public async Task RemoveUser(string superAdminId, string userId)
    {
        var bySuperAdmin = await _userManager.FindByIdAsync(superAdminId);
        if (bySuperAdmin is null) throw new NotFoundException("SuperAdmin Not found");
        if (bySuperAdmin == null || !await _userManager.IsInRoleAsync(bySuperAdmin, "SuperAdmin"))
            throw new UnauthorizedAccessException("You do not have permission to perform this action.");

        var targetUser = await _userManager.FindByIdAsync(userId);
        if (targetUser == null) throw new NotFoundException("User Not Found");

        _context.Remove(targetUser);
        await _context.SaveChangesAsync();
    }

    public async Task<TokenResponseDTO> ValidRefleshToken(string refreshToken)
    {
        if (refreshToken is null)
        {
            throw new ArgumentNullException("Refresh token does not exist");
        }
        var ByUser = await _context.Users.Where(a => a.RefreshToken == refreshToken).FirstOrDefaultAsync();
        if (ByUser is null)
        {
            throw new NotFoundException("User does Not Exist");
        }
        if (ByUser.RefreshTokenExpration < DateTime.UtcNow)
        {
            throw new ArgumentNullException("Refresh token does not exist");
        }

        var tokenResponse = await _tokenHandler.CreateAccessToken(2, 3, ByUser);
        ByUser.RefreshToken = tokenResponse.refreshToken;
        ByUser.RefreshTokenExpration = tokenResponse.refreshTokenExpration;
        await _userManager.UpdateAsync(ByUser);
        return tokenResponse;
    }

    public async Task<TokenResponseDTO> FacebookLoginAsync(string authToken, int accessTokenLifeTime)
    {
        //string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternalLoginSettings:Facebook:Client_ID"]}&client_secret={_configuration["ExternalLoginSettings:Facebook:Client_Secret"]}&grant_type=client_credentials");

        //FacebookAccessTokenResponse? facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);

        //string userAccessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse?.AccessToken}");

        //FacebookUserAccessTokenValidation? validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);

        //if (validation?.Data.IsValid != null)
        //{
        //    string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");

        //    FacebookUserInfoResponse? userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

        //    var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
        //    AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        //    return await CreateUserExternalAsync(user, userInfo.Email, userInfo.Name, info, accessTokenLifeTime);
        //}
        throw new Exception("Invalid external authentication.");
    }

    public async Task PasswordResetAsnyc(string email)
    {
        AppUser user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            byte[] tokenBytes = Encoding.UTF8.GetBytes(resetToken);
            resetToken = WebEncoders.Base64UrlEncode(tokenBytes);

            await _emailService.SendPasswordResetMailAsync(user.Email, user.Id, resetToken);
        }
    }

    public async Task<bool> ResetPassword(ResetPassword resetPassword)
    {
        AppUser user = await _userManager.FindByIdAsync(resetPassword.UserId);
        if (user != null)
        {
            IdentityResult identityResult = await _userManager.RemovePasswordAsync(user);

            if (identityResult.Succeeded)
            {
                identityResult = await _userManager.AddPasswordAsync(user, resetPassword.Password);

                if (identityResult.Succeeded)
                {
                    return true;
                }
                else throw new Exception("Sifre deyisdirilmede bir xetta bas verdi.");
            }
            else throw new Exception("Sifre deyisdirilmedi!");
        }
        else { throw new Exception("Sifre deyisdirilmedi!!!"); }
    }

    public async Task PrfileImage(string? Email, IFormFile ImageFile)
    {
        if (Email is null)
        {
            Console.WriteLine("--------------------------------------------------------");
        }
        var user = await _userManager.FindByEmailAsync(Email);
        if (user != null)
        {
            if (ImageFile is not null)
            {
                user.ImagePath = await ImageFile.GetBytes();
            }

            await _context.SaveChangesAsync();
        }
        else throw new NotFoundException("User not Found");
    }

    public async Task<bool> ByAdmin(string email)
    {
        AppUser appUser = await _userManager.FindByEmailAsync(email);
        if (appUser is null) throw new NotFoundException("User is Null");
        var userRoles = await _userManager.GetRolesAsync(appUser);

        if (userRoles.Contains("SuperAdmin"))   return true;
        return false;
    }
}
