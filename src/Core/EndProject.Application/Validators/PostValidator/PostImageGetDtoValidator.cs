using EndProject.Application.DTOs.Post;
using FluentValidation;

namespace EndProject.Application.Validators.PostValidator;

public class PostImageGetDtoValidator:AbstractValidator<PostImageGetDTO>
{
	public PostImageGetDtoValidator()
	{
		RuleFor(x => x.ImagePath).NotNull().NotEmpty().MaximumLength(756);
	}
}
