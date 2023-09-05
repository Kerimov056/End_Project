using EndProject.Application.DTOs.Advantage;
using FluentValidation;

namespace EndProject.Application.Validators.AdvantagesValidators;

public class AdvantagesUpdateDtoValidator : AbstractValidator<AdvantageUpdateDTO>
{
    public AdvantagesUpdateDtoValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(x => x.Descrption).NotNull().NotEmpty().MaximumLength(1000);
    }
}
