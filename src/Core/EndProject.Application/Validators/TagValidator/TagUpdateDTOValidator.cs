using EndProject.Application.DTOs.Tag;
using FluentValidation;

namespace EndProject.Application.Validators.TagValidator;

public class TagUpdateDTOValidator : AbstractValidator<TagUpdateDTO>
{
    public TagUpdateDTOValidator()
    {
        RuleFor(x => x.Tag).NotEmpty().NotNull().MaximumLength(120);
    }
}
