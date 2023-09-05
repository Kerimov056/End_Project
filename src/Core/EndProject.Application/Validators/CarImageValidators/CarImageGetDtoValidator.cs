using EndProject.Application.DTOs.CarImage;
using FluentValidation;

namespace EndProject.Application.Validators.CarImageValidators;

public class CarImageGetDtoValidator : AbstractValidator<CarImageGetDTO>
{
    public CarImageGetDtoValidator()
    {
        RuleFor(x => x.ImagePath).NotNull().NotEmpty().MaximumLength(12200);
    }
}