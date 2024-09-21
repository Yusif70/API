using API.Core.Entities;
using System.Linq.Expressions;

namespace API.Core.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task AddAsync(T entity);
        public IQueryable<T> GetAll();
        public Task<T> Get(Expression<Func<T, bool>> expression);
        public Task<T> GetByIdAsync(Guid id);
        public void Remove(T entity);
        public void Update(T entity);
        public Task<int> SaveAsync();
    }


}

