using EndProject.Application.DTOs.CarComment;
using FluentValidation;

namespace EndProject.Application.Validators.CarComment;

public class CarCommentUpdateDtoValidator : AbstractValidator<CarCommentUpdateDTO>
{
    public CarCommentUpdateDtoValidator()
    {
        RuleFor(x => x.Comment).NotNull().NotEmpty().MaximumLength(1000);
    }
}
