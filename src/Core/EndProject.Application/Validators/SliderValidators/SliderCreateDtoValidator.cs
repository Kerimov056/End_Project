using EndProject.Application.DTOs.Slider;
using FluentValidation;

namespace EndProject.Application.Validators.SliderValidators;

public class SliderCreateDtoValidator:AbstractValidator<SliderCreateDTO>
{
    public SliderCreateDtoValidator()
    {
        RuleFor(x => x.image).NotEmpty().NotNull();
    }
}


