using EndProject.Application.DTOs.Slider;
using FluentValidation;

namespace EndProject.Application.Validators.SliderValidators;

public class SliderGetDtoValidator:AbstractValidator<SliderGetDTO>
{
    public SliderGetDtoValidator()
    {
        RuleFor(x => x.imagePath).NotNull().NotEmpty().MaximumLength(12000);
    }
}
