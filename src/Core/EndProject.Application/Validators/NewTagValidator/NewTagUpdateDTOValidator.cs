using EndProject.Application.DTOs.NewTag;
using FluentValidation;

namespace EndProject.Application.Validators.NewTagValidator;

public class NewTagUpdateDTOValidator : AbstractValidator<NewTagUpdateDTO>
{
    public NewTagUpdateDTOValidator()
    {
        RuleFor(x => x.Tag).MaximumLength(120);
    }
}
