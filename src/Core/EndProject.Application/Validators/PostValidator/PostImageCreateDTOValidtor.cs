using EndProject.Application.DTOs.Post;
using FluentValidation;

namespace EndProject.Application.Validators.PostValidator;

public class PostImageCreateDTOValidtor:AbstractValidator<PostImageCreateDTO>
{
    public PostImageCreateDTOValidtor()
    {
        RuleFor(x => x.ImagePath).NotNull().NotEmpty().MaximumLength(756);
    }
}
