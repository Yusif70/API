using API.Service.Dtos.Category;
using FluentValidation;

namespace API.Service.Validations.Category
{
    public class CategoryPostDtoValidation : AbstractValidator<CategoryPostDto>
    {
        public CategoryPostDtoValidation()
        {
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
