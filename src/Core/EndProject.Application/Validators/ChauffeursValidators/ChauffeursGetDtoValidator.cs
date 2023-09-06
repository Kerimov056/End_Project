using EndProject.Application.DTOs.Chauffeurs;
using FluentValidation;

namespace EndProject.Application.Validators.ChauffeursValidators;

public class ChauffeursGetDtoValidator : AbstractValidator<ChauffeursGetDTO>
{
    public ChauffeursGetDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(x => x.Number).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(x => x.Price).NotNull().NotEmpty();
        RuleFor(x => x.ImagePath).NotNull().NotEmpty().MaximumLength(15000);
    }
}