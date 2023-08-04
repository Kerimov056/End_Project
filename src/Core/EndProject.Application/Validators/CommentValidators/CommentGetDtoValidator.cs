using EndProject.Application.DTOs.Comments;
using FluentValidation;

namespace EndProject.Application.Validators.CommentValidators;

public class CommentGetDtoValidator:AbstractValidator<CommentGetDTO>
{
	public CommentGetDtoValidator()
	{
		RuleFor(x => x.Comment).NotNull().NotEmpty().MaximumLength(800);
	}
}
