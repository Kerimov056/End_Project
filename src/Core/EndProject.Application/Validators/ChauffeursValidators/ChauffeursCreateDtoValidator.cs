using EndProject.Application.DTOs.Chauffeurs;
using FluentValidation;

namespace EndProject.Application.Validators.ChauffeursValidators;

public class ChauffeursCreateDtoValidator : AbstractValidator<ChauffeursCreateDTO>
{
    public ChauffeursCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(x => x.Number).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(x => x.Price).NotNull().NotEmpty();
        RuleFor(x => x.Image).NotNull().NotEmpty();
    }
}