using Todo.Domain.Entities;
using Todo.Domain.Interfaces;
using Todo.Infrastructure.Persistence;

namespace Todo.Infrastructure.Repositories
{
    public class TodoRepository : Repository<TodoItem>, ITodoRepository
    {
        public TodoRepository(ApplicationDbContext context) : base(context) { }
    }
}
