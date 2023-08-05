using EndProject.Application.DTOs.Post;
using FluentValidation;

namespace EndProject.Application.Validators.PostValidator;

public class PostImageUpdateDTOValidator : AbstractValidator<PostImageCreateDTO>
{
    public PostImageUpdateDTOValidator()
    {
        RuleFor(x => x.ImagePath).NotNull().NotEmpty().MaximumLength(756);
    }
}
