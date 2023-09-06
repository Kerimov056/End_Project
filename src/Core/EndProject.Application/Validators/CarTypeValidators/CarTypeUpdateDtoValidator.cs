using EndProject.Application.DTOs.CarType;
using FluentValidation;

namespace EndProject.Application.Validators.CarTypeValidators;

public class CarTypeUpdateDtoValidator : AbstractValidator<CarTypeUpdateDTO>
{
    public CarTypeUpdateDtoValidator()
    {
        RuleFor(x => x.Type).NotNull().NotEmpty().MaximumLength(100);
    }
}