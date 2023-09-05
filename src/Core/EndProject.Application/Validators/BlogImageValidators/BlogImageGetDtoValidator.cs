using EndProject.Application.DTOs.BlogImage;
using FluentValidation;

namespace EndProject.Application.Validators.BlogImageValidators;

public class BlogImageGetDtoValidator : AbstractValidator<BlogImageGetDTO>
{
    public BlogImageGetDtoValidator()
    {
        RuleFor(x => x.imagePath).NotNull().NotEmpty();
    }
}
