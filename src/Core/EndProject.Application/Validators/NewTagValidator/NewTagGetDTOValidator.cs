using EndProject.Application.DTOs.NewTag;
using FluentValidation;

namespace EndProject.Application.Validators.NewTagValidator;

public class NewTagGetDTOValidator:AbstractValidator<NewTagGetDTO>
{
	public NewTagGetDTOValidator()
	{
		RuleFor(x => x.Tag).MaximumLength(120);
	}
}
