using EndProject.Application.DTOs.Category;
using FluentValidation;

namespace EndProject.Application.Validators.CarCategoryValidators;

public class CarCategoryCreateDtoValidator : AbstractValidator<CarCategoryCreateDTO>
{
    public CarCategoryCreateDtoValidator()
    {
        RuleFor(x => x.category).NotNull().NotEmpty().MaximumLength(100);
    }
}
