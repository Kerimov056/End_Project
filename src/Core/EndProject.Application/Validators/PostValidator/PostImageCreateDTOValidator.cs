using EndProject.Application.DTOs.Post;
using FluentValidation;

namespace EndProject.Application.Validators.PostValidator;

public class PostImageCreateDTOValidator:AbstractValidator<PostImageCreateDTO>
{
	public PostImageCreateDTOValidator()
	{
        RuleFor(x => x.ImagePath).NotNull().NotEmpty();
    }
}
