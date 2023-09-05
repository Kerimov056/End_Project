using EndProject.Application.DTOs.CarComment;
using FluentValidation;

namespace EndProject.Application.Validators.CarComment;

public class CarCommentGetDtoValidator : AbstractValidator<CarCommentGetDTO>
{
    public CarCommentGetDtoValidator()
    {
        RuleFor(x => x.Comment).NotNull().NotEmpty().MaximumLength(1000);
    }
}
