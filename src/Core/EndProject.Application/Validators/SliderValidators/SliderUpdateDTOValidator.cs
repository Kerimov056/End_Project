using EndProject.Application.DTOs.Slider;
using FluentValidation;

namespace EndProject.Application.Validators.SliderValidators
{
    public class SliderUpdateDTOValidator:AbstractValidator<SliderUpdateDTO>
    {
        public SliderUpdateDTOValidator()
        {
            RuleFor(x => x.image).NotNull().NotEmpty();

        }
    }
}
