using API.Core.Entities;
using API.Core.Repositories.Interfaces;
using API.Data.Context;

namespace API.Data.Repositories.Concretes
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        public BlogRepository(AppDbContext context) : base(context) { }
    }
}
