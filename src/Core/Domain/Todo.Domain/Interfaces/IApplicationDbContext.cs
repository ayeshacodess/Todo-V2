using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;

namespace Todo.Domain.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<T> Set<T>() where T : class;
        DbSet<TodoItem> TodoItems { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
