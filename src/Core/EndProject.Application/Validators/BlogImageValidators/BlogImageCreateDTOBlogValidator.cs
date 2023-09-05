using EndProject.Application.DTOs.Blog;
using EndProject.Application.DTOs.BlogImage;
using FluentValidation;

namespace EndProject.Application.Validators.BlogImageValidators;

public class BlogImageCreateDTOBlogValidator : AbstractValidator<BlogImageCreateDTO>
{
    public BlogImageCreateDTOBlogValidator()
    {
        RuleFor(x => x.imagePath).NotNull().NotEmpty();
    }
}
