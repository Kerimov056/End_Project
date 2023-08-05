using EndProject.Application.DTOs.NewTag;
using FluentValidation;

namespace EndProject.Application.Validators.NewTagValidator;

public class NewTagCreateDTOValidator : AbstractValidator<NewTagCreateDTO>
{
    public NewTagCreateDTOValidator()
    {
        RuleFor(x => x.Tag).MaximumLength(120);
    }
}
