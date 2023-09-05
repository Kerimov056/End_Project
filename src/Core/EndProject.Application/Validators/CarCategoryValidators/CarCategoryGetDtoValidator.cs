using EndProject.Application.DTOs.Category;
using FluentValidation;

namespace EndProject.Application.Validators.CarCategoryValidators;

public class CarCategoryGetDtoValidator : AbstractValidator<CarCategoryGetDTO>
{
    public CarCategoryGetDtoValidator()
    {
        RuleFor(x => x.Category).NotNull().NotEmpty().MaximumLength(100);
    }
}
