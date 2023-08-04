using EndProject.Application.DTOs.Post;
using FluentValidation;

namespace EndProject.Application.Validators.PostValidator;

public class PostGetDtoValidator:AbstractValidator<PostGetDTO>
{
	public PostGetDtoValidator()
	{
		RuleFor(x => x.Message).MaximumLength(500);
	}
}
