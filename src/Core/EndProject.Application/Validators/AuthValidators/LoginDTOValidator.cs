using EndProject.Application.DTOs;
using FluentValidation;

namespace EndProject.Application.Validators.AuthValidators;

public class LoginDTOValidator: AbstractValidator<LoginDTO>
{
	public LoginDTOValidator()
	{
		RuleFor(x => x.UsernameOrEmail).NotEmpty().NotNull().MaximumLength(255);
		RuleFor(x => x.password).NotEmpty().NotNull().MaximumLength(255);
	}
}
