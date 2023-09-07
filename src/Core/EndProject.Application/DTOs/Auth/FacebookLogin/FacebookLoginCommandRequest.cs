using MediatR;

namespace EndProject.Application.DTOs.Auth.FacebookLogin;

public class FacebookLoginCommandRequest : IRequest<FacebookLoginCommandResponse>
{
    public string AuthToken { get; set; }
}