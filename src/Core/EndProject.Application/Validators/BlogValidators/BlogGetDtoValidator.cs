using EndProject.Application.DTOs.Blog;
using FluentValidation;

namespace EndProject.Application.Validators.BlogValidators;

public class BlogGetDtoValidator : AbstractValidator<BlogGetDTO>
{
    public BlogGetDtoValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(15000);
    }
}
