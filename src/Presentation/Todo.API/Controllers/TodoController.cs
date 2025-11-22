using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Interfaces;
using Todo.Application.Services.Todo.Dtos;

namespace Todo.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            //throw new Exception("Something went wrong");

            var res = await _todoService.GetAsync(id);
            return Ok(res);
        }

        [HttpGet("completed")]
        public async Task<ActionResult<List<TodoResponseDto>>> GetCompletedTasks()
        {
            var res = await _todoService.GetAllCompletedItemsAsync();
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] TodoRequestDto req)
        {
            var res = await _todoService.AddAsync(req);
            return res ? Created() : BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateTodoRequestDto req)
        {
            var res = await _todoService.UpdateAsync(req);
            return res ? Ok() : NotFound();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _todoService.DeleteAsync(id);
            return deleted ? Ok() : NotFound();
        }
    }
}
