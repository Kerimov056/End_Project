using EndProject.Application.DTOs.Car;
using FluentValidation;

namespace EndProject.Application.Validators.CarValidators;

public class CarCreateDtoValidator : AbstractValidator<CarCreateDTO>
{
    public CarCreateDtoValidator()
    {
        RuleFor(x => x.Marka).NotNull().NotEmpty().MaximumLength(130);
        RuleFor(x => x.Model).NotNull().NotEmpty().MaximumLength(120);
        RuleFor(x => x.Price).NotNull().NotEmpty();
        RuleFor(x => x.Year).NotNull().NotEmpty();
        RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(1000);
    }
}