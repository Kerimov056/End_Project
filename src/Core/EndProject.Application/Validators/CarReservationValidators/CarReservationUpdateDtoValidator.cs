using EndProject.Application.DTOs.CarImage;
using EndProject.Application.DTOs.CarReservation;
using FluentValidation;

namespace EndProject.Application.Validators.CarReservationValidators;

public class CarReservationUpdateDtoValidator : AbstractValidator<CarReservationUpdateDTO>
{
    public CarReservationUpdateDtoValidator()
    {
        RuleFor(x => x.FullName).NotNull().NotEmpty().MaximumLength(140);
        RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty().MaximumLength(255);
        RuleFor(x => x.Number).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(x => x.ImagePath).NotNull().NotEmpty();
        RuleFor(x => x.Notes).NotNull().NotEmpty().MaximumLength(12000);
    }
}