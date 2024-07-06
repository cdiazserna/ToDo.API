using Microsoft.EntityFrameworkCore;
using ToDo.API.DAL.Entities;

namespace ToDo.API.DAL.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DataBaseContext _context;

        public TodoRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<Todo> GetByIdAsync(int id)
        {
            return await _context.Todos.FindAsync(id);
        }

        public async Task<Todo> CreateAsync(Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<bool> UpdateAsync(Todo todo, int id)
        {
            var existingItem = await _context.Todos.FindAsync(id);
            if (existingItem == null)
                return false;

            existingItem.Name = todo.Name;
            existingItem.IsComplete = todo.IsComplete;
            existingItem.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Todos.FindAsync(id);
            if (item == null)
                return false;

            _context.Todos.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
