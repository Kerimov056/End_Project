using EndProject.Application.DTOs.Comments;
using FluentValidation;

namespace EndProject.Application.Validators.CommentValidators;

public class CommentCreateDTOValidator:AbstractValidator<CommentCreateDTO>
{
	public CommentCreateDTOValidator()
	{
        RuleFor(x => x.Comment).NotNull().NotEmpty().MaximumLength(800);
    }
}
