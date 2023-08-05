using EndProject.Application.DTOs.Tag;
using FluentValidation;

namespace EndProject.Application.Validators.TagValidator;

public class TagCreateDTOValidator:AbstractValidator<TagCreateDTO>
{
	public TagCreateDTOValidator()
	{
        RuleFor(x => x.Tag).NotEmpty().NotNull().MaximumLength(120);
    }
}
