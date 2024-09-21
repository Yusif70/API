using API.Core.Entities;
using API.Core.Repositories.Interfaces;
using API.Data.Context;

namespace API.Data.Repositories.Concretes
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
