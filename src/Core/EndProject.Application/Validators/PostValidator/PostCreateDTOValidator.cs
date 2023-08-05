using EndProject.Application.DTOs.Post;
using FluentValidation;

namespace EndProject.Application.Validators.PostValidator;

public class PostCreateDTOValidator : AbstractValidator<PostUpdateDTO>
{
    public PostCreateDTOValidator()
    {
        RuleFor(x => x.Message).MaximumLength(500);
    }
}
