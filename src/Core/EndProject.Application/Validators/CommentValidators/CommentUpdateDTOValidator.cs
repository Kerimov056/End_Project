using EndProject.Application.DTOs.Comments;
using FluentValidation;

namespace EndProject.Application.Validators.CommentValidators;

public class CommentUpdateDTOValidator : AbstractValidator<CommentUpdateDTO>
{
    public CommentUpdateDTOValidator()
    {
        RuleFor(x => x.Comment).NotNull().NotEmpty().MaximumLength(800);
    }
}