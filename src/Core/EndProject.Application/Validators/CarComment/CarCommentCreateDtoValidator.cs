using EndProject.Application.DTOs.CarComment;
using FluentValidation;

namespace EndProject.Application.Validators.CarComment;

public class CarCommentCreateDtoValidator : AbstractValidator<CarCommentCreateDTO>
{
    public CarCommentCreateDtoValidator()
    {
        RuleFor(x => x.comment).NotNull().NotEmpty().MaximumLength(1000);
    }
}
