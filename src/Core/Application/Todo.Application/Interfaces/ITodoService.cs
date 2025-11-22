using Todo.Application.Services.Todo.Dtos;

namespace Todo.Application.Interfaces
{
    public interface ITodoService
    {
        Task<TodoResponseDto> GetAsync(int id);

        Task<List<TodoResponseDto>> GetAllCompletedItemsAsync();

        Task<bool> AddAsync(TodoRequestDto req);

        Task<bool> UpdateAsync(UpdateTodoRequestDto req);

        Task<bool> DeleteAsync(int id);

        void DeleteAllAsync();
    }
}
