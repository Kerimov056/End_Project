using EndProject.Application.Abstraction.Services;
using MediatR;

namespace EndProject.Application.DTOs.Auth.GoogleLogin;

public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
{
    readonly IAuthService _authService;
    public GoogleLoginCommandHandler(IAuthService authService) =>  _authService = authService;

    public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var tokenResponseDTO = await _authService.GoogleLoginAsync(request.IdToken, 900);
        return new()
        {
            TokenResponseDTO = tokenResponseDTO,
        };
    }
}