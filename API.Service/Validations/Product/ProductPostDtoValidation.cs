using API.Service.Dtos.Product;
using API.Service.Extensions;
using FluentValidation;

namespace API.Service.Validations.Product
{
    public class ProductPostDtoValidation : AbstractValidator<ProductPostDto>
    {
        public ProductPostDtoValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(30);
            RuleFor(p => p.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(5000);
            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);
            RuleFor(p => p.CategoryId)
                .NotEmpty()
                .NotNull();
            RuleFor(p => p.File)
                .Custom((file, context) =>
                {
                    if (!file.IsImage())
                    {
                        context.AddFailure("File", "Only files with following extensions are allowed: png, jpeg, jpg");
                    }
                    if (!file.IsSizeOk(5))
                    {
                        context.AddFailure("File", "File size cannot be greater than 5mb");
                    }
                });
        }
    }
}
