using EndProject.Application.DTOs.Advantage;
using FluentValidation;

namespace EndProject.Application.Validators.AdvantagesValidators;

public class AdvantagesGetDtoValidator : AbstractValidator<AdvantageGetDTO>
{
    public AdvantagesGetDtoValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(x => x.Descrption).NotNull().NotEmpty().MaximumLength(1000);
    }
}
