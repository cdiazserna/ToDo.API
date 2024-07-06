using AutoMapper;
using Moq;
using ToDo.API.DAL.Repository;
using ToDo.API.Domain.Services;
using ToDo.API.Models;

namespace Todo.API.Tests.ServiceTest
{
    public class TodoServiceTests
    {
        private readonly Mock<ITodoRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly TodoService _todoService;

        public TodoServiceTests()
        {
            _mockRepo = new Mock<ITodoRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TodoItemDTO, ToDo.API.DAL.Entities.Todo>().ReverseMap();
                cfg.CreateMap<ToDo.API.DAL.Entities.Todo, TodoItemResponse>().ReverseMap();
            });
            _mapper = config.CreateMapper();

            _todoService = new TodoService(_mockRepo.Object, _mapper);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllTodos()
        {
            // Arrange
            var todos = new List<ToDo.API.DAL.Entities.Todo>
            {
                new ToDo.API.DAL.Entities.Todo { Id = 1, Name = "Test 1", IsComplete = false },
                new ToDo.API.DAL.Entities.Todo { Id = 2, Name = "Test 2", IsComplete = true }
            };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(todos);

            // Act
            var result = await _todoService.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsTodo()
        {
            // Arrange
            var todo = new ToDo.API.DAL.Entities.Todo { Id = 1, Name = "Test", IsComplete = false };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(todo);

            // Act
            var result = await _todoService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result.Name);
        }

        [Fact]
        public async Task CreateAsync_AddsTodo()
        {
            // Arrange
            var todoDto = new TodoItemDTO { Name = "New Todo", IsComplete = false };
            var todo = new ToDo.API.DAL.Entities.Todo { Id = 1, Name = "New Todo", IsComplete = false };
            _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<ToDo.API.DAL.Entities.Todo>())).ReturnsAsync(todo);

            // Act
            var result = await _todoService.CreateAsync(todoDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Todo", result.Name);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesTodo()
        {
            // Arrange
            var todoDto = new TodoItemDTO { Name = "Updated Todo", IsComplete = true };
            var todo = new ToDo.API.DAL.Entities.Todo { Id = 1, Name = "Updated Todo", IsComplete = true };
            _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<ToDo.API.DAL.Entities.Todo>(), 1)).ReturnsAsync(true);

            // Act
            var result = await _todoService.UpdateAsync(todoDto, 1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_DeletesTodo()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _todoService.DeleteAsync(1);

            // Assert
            Assert.True(result);
        }
    }
}
