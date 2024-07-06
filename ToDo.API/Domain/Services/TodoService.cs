using AutoMapper;
using ToDo.API.DAL.Entities;
using ToDo.API.DAL.Repository;
using ToDo.API.Domain.Interfaces;
using ToDo.API.Models;

namespace ToDo.API.Domain.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public TodoService(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoItemResponse>> GetAllAsync()
        {
            var items = await _todoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TodoItemResponse>>(items);
        }

        public async Task<TodoItemDTO> GetByIdAsync(int id)
        {
            var item = await _todoRepository.GetByIdAsync(id);
            return _mapper.Map<TodoItemDTO>(item);
        }

        public async Task<TodoItemDTO> CreateAsync(TodoItemDTO todoItem)
        {
            var todo = _mapper.Map<Todo>(todoItem);
            var createdItem = await _todoRepository.CreateAsync(todo);
            return _mapper.Map<TodoItemDTO>(createdItem);
        }

        public async Task<bool> UpdateAsync(TodoItemDTO todoItem, int id)
        {
            var todo = _mapper.Map<Todo>(todoItem);
            return await _todoRepository.UpdateAsync(todo, id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _todoRepository.DeleteAsync(id);
        }
    }
}
