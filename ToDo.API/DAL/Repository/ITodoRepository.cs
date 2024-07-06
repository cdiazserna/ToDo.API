using ToDo.API.DAL.Entities;

namespace ToDo.API.DAL.Repository
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo> GetByIdAsync(int id);
        Task<Todo> CreateAsync(Todo todoItem);
        Task<bool> UpdateAsync(Todo todoItem, int id);
        Task<bool> DeleteAsync(int id);
    }
}
