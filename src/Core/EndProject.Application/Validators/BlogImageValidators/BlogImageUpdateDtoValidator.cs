using EndProject.Application.DTOs.BlogImage;
using FluentValidation;

namespace EndProject.Application.Validators.BlogImageValidators;

public class BlogImageUpdateDtoValidator : AbstractValidator<BlogImageUpdateDTO>
{
    public BlogImageUpdateDtoValidator()
    {
        RuleFor(x => x.imagePath).NotNull().NotEmpty();
    }
}
