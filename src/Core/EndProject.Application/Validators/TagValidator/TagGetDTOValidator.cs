using EndProject.Application.DTOs.Tag;
using FluentValidation;

namespace EndProject.Application.Validators.TagValidator;

public class TagGetDTOValidator : AbstractValidator<TagGetDTO>
{
    public TagGetDTOValidator()
    {
        RuleFor(x => x.Tag).NotEmpty().NotNull().MaximumLength(120);
    }
}
