using EndProject.Application.DTOs.Category;
using EndProject.Application.DTOs.Faq;
using FluentValidation;

namespace EndProject.Application.Validators.CarCategoryValidators;

public class CarCategoryUpdateDtoValidator : AbstractValidator<CarCategoryUpdateDTO>
{
    public CarCategoryUpdateDtoValidator()
    {
        RuleFor(x => x.category).NotNull().NotEmpty().MaximumLength(100);
    }
}
