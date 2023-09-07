//using EndProject.Application.Abstraction.Services;
//using MediatR;

//namespace EndProject.Application.DTOs.Auth.PasswordReset;

//public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommandRequest, PasswordResetCommandResponse>
//{
//    readonly IAuthService _authService;

//    public PasswordResetCommandHandler(IAuthService authService)
//    {
//        _authService = authService;
//    }

//    public async Task<PasswordResetCommandResponse> Handle(PasswordResetCommandRequest request, CancellationToken cancellationToken)
//    {
//        await _authService.PasswordResetAsnyc(request.Email);
//        return new();
//    }
//}