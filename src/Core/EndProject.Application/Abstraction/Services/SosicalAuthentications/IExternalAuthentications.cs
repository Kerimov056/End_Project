using EndProject.Application.DTOs.Auth;

namespace EndProject.Application.Abstraction.Services.SosicalAuthentications;

public interface IExternalAuthentications
{
    Task<TokenResponseDTO> GoogleLoginAsync(string idToken);
    Task<TokenResponseDTO> FacebookLoginAsync(string authToken, int accessTokenLifeTime);
}
