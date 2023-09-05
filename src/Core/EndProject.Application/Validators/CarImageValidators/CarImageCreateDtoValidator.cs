using EndProject.Application.DTOs.CarImage;
using FluentValidation;

namespace EndProject.Application.Validators.CarImageValidators;

public class CarImageCreateDtoValidator : AbstractValidator<CarImageCreateDTO>
{
    public CarImageCreateDtoValidator()
    {
        RuleFor(x => x.image).NotNull().NotEmpty();
    }
}