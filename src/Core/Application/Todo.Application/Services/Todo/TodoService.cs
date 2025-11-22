using Todo.Application.Interfaces;
using Todo.Application.Services.Todo.Dtos;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces;

namespace Todo.Application.Services.Todo
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        protected readonly IApplicationDbContext _context;

        public TodoService(ITodoRepository todoRepository, IApplicationDbContext context)
        {
            _todoRepository = todoRepository;
            _context = context;
        }

        public async Task<TodoResponseDto> GetAsync(int id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);

            var res = new TodoResponseDto();
            if (todo == null)
            {
                return res;
            }

            //TODO: Move this to Mapper
            res.Id = id;
            res.Title = todo.Title;
            res.AssignedTo = todo.AssignedTo;
            res.CreatedAt = todo.CreatedAt;
            res.Description = todo.Description;
            res.Status = todo.Status;

            return res;
        }

        public async Task<List<TodoResponseDto>> GetAllCompletedItemsAsync()
        {
            var todoItems = await _todoRepository.GetAllAsync();
            var completedItems = todoItems.Where(x => x.Status == 3 && x.AssignedTo == 1);

            var completedItemsList = completedItems.Select(x => new TodoResponseDto()
            {
                Id = x.Id,
                AssignedTo = x.AssignedTo,
                Title = x.Title,
                Description = x.Description,
                Status = x.Status
            }).ToList();

            return completedItemsList;
        }

        public async Task<bool> AddAsync(TodoRequestDto req)
        {
            //Todo:
            //Convert Dto to Entity obj
            var entityObj = new TodoItem
            {
                AssignedTo = req.AssignedTo,
                CreatedBy = 12,
                CreatedAt = DateTime.Now,
                CompletedDate = DateTime.Now,
                Status = req.Status,
                Title = req.Title,
                Description = req.Description

            };
            //send this to repo to save into DB
            await _todoRepository.AddAsync(entityObj);

            //Inject appdbcontext in this class
            var res = await _context.SaveChangesAsync();

            return res == 1;
        }

        public async Task<bool> UpdateAsync(UpdateTodoRequestDto req)
        {
            var item = await _todoRepository.GetByIdAsync(req.Id);
            if (item is null)
            {
                return false;
            }

            item.Title = req.Title;
            item.Description = req.Description;
            item.AssignedTo = req.AssignedTo;
            item.Status = req.Status;

            _todoRepository.Update(item);

            var res = await _context.SaveChangesAsync();

            return res > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _todoRepository.GetByIdAsync(id);
            if (item is null)
            {
                return false;
            }


            _todoRepository.Delete(item);
            var res = await _context.SaveChangesAsync();
            return res > 0;
        }

        public void DeleteAllAsync()
        {
            // DeleteAll();
        }

    }
}
