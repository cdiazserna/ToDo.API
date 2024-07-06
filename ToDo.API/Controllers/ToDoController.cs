using Microsoft.AspNetCore.Mvc;
using ToDo.API.Domain.Interfaces;
using ToDo.API.Models;

namespace ToDo.API.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public ToDoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        [Route("get")]
        public async Task<ActionResult<IEnumerable<TodoItemResponse>>> GetAllAsync()
        {
            var items = await _todoService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetByIdAsync(int id)
        {
            var item = await _todoService.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<TodoItemDTO>> CreateAsync(TodoItemDTO todoItem)
        {
            var createdItem = await _todoService.CreateAsync(todoItem);
            return Ok(createdItem);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<ActionResult> UpdateAsync(TodoItemDTO todoItem, int id)
        {
            var updated = await _todoService.UpdateAsync(todoItem, id);
            if (!updated)
                return NotFound();

            return Ok("Todo Item updated");
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var deleted = await _todoService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return Ok("Todo Item deleted");
        }
    }
}
