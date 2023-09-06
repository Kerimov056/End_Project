using EndProject.Application.DTOs.CarType;
using FluentValidation;

namespace EndProject.Application.Validators.CarTypeValidators;

public class CarTypeGetDtoValidator : AbstractValidator<CarTypeGetDTO>
{
    public CarTypeGetDtoValidator()
    {
        RuleFor(x => x.Type).NotNull().NotEmpty().MaximumLength(100);
    }
}