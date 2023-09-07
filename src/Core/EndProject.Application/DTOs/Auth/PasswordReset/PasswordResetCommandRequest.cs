using MediatR;

namespace EndProject.Application.DTOs.Auth.PasswordReset;

public class PasswordResetCommandRequest : IRequest<PasswordResetCommandResponse>
{
    public string Email { get; set; }
}