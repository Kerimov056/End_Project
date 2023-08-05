using EndProject.Application.DTOs.Post;
using FluentValidation;

namespace EndProject.Application.Validators.PostValidator;

public class PostUpdateDTOValidator:AbstractValidator<PostUpdateDTO>
{
	public PostUpdateDTOValidator()
	{
        RuleFor(x => x.Message).MaximumLength(500);
    }
}
