using API.Core.Entities;
using API.Core.Repositories.Interfaces;
using API.Data.Context;

namespace API.Data.Repositories.Concretes
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
