namespace Todo.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task AddAsync(T item);
        void Update(T item);
        void Delete(T entity);
    }
}
