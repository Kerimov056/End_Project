using EndProject.Application.DTOs.Faq;
using FluentValidation;

namespace EndProject.Application.Validators.FaqValidators;

public class FaqCreateDtoValidator : AbstractValidator<FaqCreateDTO>
{
    public FaqCreateDtoValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(x => x.Descrption).NotNull().NotEmpty().MaximumLength(1000);
    }
}
