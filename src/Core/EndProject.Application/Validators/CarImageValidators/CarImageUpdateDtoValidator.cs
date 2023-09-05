using EndProject.Application.DTOs.CarImage;
using FluentValidation;

namespace EndProject.Application.Validators.CarImageValidators;

public class CarImageUpdateDtoValidator : AbstractValidator<CarImageUpdateDTO>
{
    public CarImageUpdateDtoValidator()
    {
        RuleFor(x => x.image).NotNull().NotEmpty();
    }
}