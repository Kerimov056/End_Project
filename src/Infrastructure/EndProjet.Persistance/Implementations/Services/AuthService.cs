﻿using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Auth;
using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;
using EndProject.Domain.Enums.Role;
using EndProject.Domain.Helpers;
using EndProjet.Persistance.Context;
using EndProjet.Persistance.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EndProjet.Persistance.Implementations.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _siginManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly AppDbContext _context;

    public AuthService(UserManager<AppUser> userManager,
                       SignInManager<AppUser> siginManager,
                       RoleManager<IdentityRole> roleManager,
                       ITokenHandler tokenHandler,
                       AppDbContext context)
    {
        _userManager = userManager;
        _siginManager = siginManager;
        _roleManager = roleManager;
        _tokenHandler = tokenHandler;
        _context = context;
    }

    public async Task AdminCreate(string superAdminId, string appUserId)
    {
        var bySuperAdmin = await _userManager.FindByIdAsync(superAdminId);
        if (bySuperAdmin is null) throw new NotFoundException("SuperAdmin Not found");
        if (bySuperAdmin == null || !await _userManager.IsInRoleAsync(bySuperAdmin, "SuperAdmin"))
            throw new UnauthorizedAccessException("You do not have permission to perform this action.");
       
        
        var targetUser = await _userManager.FindByIdAsync(appUserId);

        if (targetUser == null)   throw new NotFoundException("User Not Found");

        await _userManager.RemoveFromRoleAsync(targetUser, "Member");
        await _userManager.AddToRoleAsync(targetUser, "Admin");
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


    //public async Task<LoginDTO> ExternalLogin(ExternalLoginInfo info)
    //{
    //    //LoginDTO loginResult = null;

    //    //if (info==null)
    //    //{
    //    //    return null;
    //    //}

    //    //var signinResult = await _siginManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
    //    //var email = info.Principal.FindFirstValue(ClaimTypes.Email);
    //    //var user = await _userManager.FindByEmailAsync(email);
    //    ////var claims = await GetUserClaims(user);


    //    //if (signinResult.Succeeded)
    //    //{
    //    //    var jwtResult = await _tokenHandler.CreateAccessToken(2, 3, user);


    //    //    await _userManager.SetAuthenticationTokenAsync(
    //    //        user,
    //    //        TokenOptions.DefaultProvider,
    //    //        jwtResult.refreshToken,
    //    //        jwtResult.refreshToken);

    //    //    loginResult = new LoginDTO()
    //    //    {
    //    //        user = new UserViewModel()
    //    //        {
    //    //            Email = email,
    //    //            AccessToken = jwtResult.AccessToken,
    //    //            RefreshToken = jwtResult.RefreshToken,
    //    //            FirstName = user.FirstName,
    //    //            LastName = user.LastName,
    //    //            Phone = user.PhoneNumber,
    //    //            UserId = user.Id
    //    //        }
    //    //    };             //https://mahdi-karimipour.medium.com/react-google-auth-asp-net-5-0-api-azure-and-asp-net-identity-4dfe1ced369e

    //    //    return loginResult;
    //    //}
    //    throw new NotImplementedException();    
    //}

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
}
