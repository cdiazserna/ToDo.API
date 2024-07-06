using ToDo.API.Models;

namespace ToDo.API.Domain.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItemResponse>> GetAllAsync();
        Task<TodoItemDTO> GetByIdAsync(int id);
        Task<TodoItemDTO> CreateAsync(TodoItemDTO todoItem);
        Task<bool> UpdateAsync(TodoItemDTO todoItem, int id);
        Task<bool> DeleteAsync(int id);
    }
}
