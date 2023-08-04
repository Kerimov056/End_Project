using EndProject.Application.DTOs.Auth;
using FluentValidation;

namespace EndProject.Application.Validators.AuthValidators;

public class RegisterDTOValidator:AbstractValidator<RegisterDTO>
{
	public RegisterDTOValidator()
	{
		RuleFor(x => x.Fullname).MaximumLength(255);
		RuleFor(x => x.Username).NotEmpty().NotNull().MaximumLength(60);
		RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull().MaximumLength(80);
		RuleFor(x => x.password).NotEmpty().NotNull().MaximumLength(155);
	}
}
