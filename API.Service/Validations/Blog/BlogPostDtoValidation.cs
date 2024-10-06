using API.Service.Dtos.Blog;
using API.Service.Extensions;
using FluentValidation;

namespace API.Service.Validations.Blog
{
    public class BlogPostDtoValidation : AbstractValidator<BlogPostDto>
    {
        public BlogPostDtoValidation()
        {
            RuleFor(b => b.Title)
                .NotEmpty()
                .NotNull()
                .MaximumLength(30);
            RuleFor(b => b.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(10000);
            RuleFor(b => b)
                .Custom((b, content) =>
                {
                    if (!b.File.IsImage())
                    {
                        content.AddFailure("File", "Only following extensions are allowed: png, jpg, jpeg, webp");
                    }
                    if (!b.File.IsSizeOk(5))
                    {
                        content.AddFailure("File", "File size cannot exceed 5mb");
                    }
                });
            RuleFor(b => b.CategoryId)
                .NotEmpty()
                .NotNull();
        }
    }
}
