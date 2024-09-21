using API.Core.Entities;
using API.Core.Repositories.Interfaces;
using API.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Data.Repositories.Concretes
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }
        public async Task<T> Get(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(c => c.Id == id);
        }
        public void Remove(T entity)
        {
            _context.Remove(entity);
        }
        public void Update(T entity)
        {
            _context.Update(entity);
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
