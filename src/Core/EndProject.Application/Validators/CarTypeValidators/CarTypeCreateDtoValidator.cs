using EndProject.Application.DTOs.CarType;
using FluentValidation;

namespace EndProject.Application.Validators.CarTypeValidators;

public class CarTypeCreateDtoValidator : AbstractValidator<CarTypeCreateDTO>
{
    public CarTypeCreateDtoValidator()
    {
        RuleFor(x => x.type).NotNull().NotEmpty().MaximumLength(100);
    }
}