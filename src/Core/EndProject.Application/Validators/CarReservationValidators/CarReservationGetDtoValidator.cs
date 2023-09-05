using EndProject.Application.DTOs.CarReservation;
using FluentValidation;

namespace EndProject.Application.Validators.CarReservationValidators;

public class CarReservationGetDtoValidator : AbstractValidator<CarReservationGetDTO>
{
    public CarReservationGetDtoValidator()
    {
        RuleFor(x => x.FullName).NotNull().NotEmpty().MaximumLength(140);
        RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty().MaximumLength(255);
        RuleFor(x => x.Number).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(x => x.ImagePath).NotNull().NotEmpty().MaximumLength(12000);
        RuleFor(x => x.Notes).NotNull().NotEmpty().MaximumLength(12000);
    }
}