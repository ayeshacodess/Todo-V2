using Microsoft.EntityFrameworkCore;
using Todo.Domain.Interfaces;

namespace Todo.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IApplicationDbContext _context;
        protected readonly DbSet<T> _db;

        public Repository(IApplicationDbContext context)
        {
            _context = context;
            _db = context.Set<T>();
        }
        async Task IRepository<T>.AddAsync(T entity)
        {
            await _db.AddAsync(entity);
        }

        void IRepository<T>.Delete(T entity)
        {
            _db.Remove(entity);
        }

        async Task<List<T>> IRepository<T>.GetAllAsync()
        {
            return await _db.ToListAsync();
        }

        async Task<T?> IRepository<T>.GetByIdAsync(int id)
        {
            var todoItem = await _db.FindAsync(id);
            return todoItem;
        }

        void IRepository<T>.Update(T entity)
        {
            _db.Update(entity);
        }
    }
}
