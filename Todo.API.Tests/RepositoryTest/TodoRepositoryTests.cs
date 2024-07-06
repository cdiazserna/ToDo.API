using Microsoft.EntityFrameworkCore;
using ToDo.API.DAL;
using ToDo.API.DAL.Repository;

namespace ToDo.API.Tests.DAL.Repository
{
    public class TodoRepositoryTests
    {
        private readonly DbContextOptions<DataBaseContext> _options;

        public TodoRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "TodoDB_Test")
                .Options;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllTodos()
        {
            // Arrange
            using (var context = new DataBaseContext(_options))
            {
                context.Todos.Add(new ToDo.API.DAL.Entities.Todo { Name = "Test 1", IsComplete = false });
                context.Todos.Add(new ToDo.API.DAL.Entities.Todo { Name = "Test 2", IsComplete = true });
                await context.SaveChangesAsync();
            }

            using (var context = new DataBaseContext(_options))
            {
                var repository = new TodoRepository(context);

                // Act
                var todos = await repository.GetAllAsync();

                // Assert
                Assert.Equal(2, todos.Count());
            }
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsTodo()
        {
            // Arrange
            int id;
            using (var context = new DataBaseContext(_options))
            {
                var todo = new ToDo.API.DAL.Entities.Todo { Name = "Test", IsComplete = false };
                context.Todos.Add(todo);
                await context.SaveChangesAsync();
                id = todo.Id;
            }

            using (var context = new DataBaseContext(_options))
            {
                var repository = new TodoRepository(context);

                // Act
                var todo = await repository.GetByIdAsync(id);

                // Assert
                Assert.NotNull(todo);
                Assert.Equal("Test", todo.Name);
            }
        }

        [Fact]
        public async Task CreateAsync_AddsTodo()
        {
            // Arrange
            using (var context = new DataBaseContext(_options))
            {
                var repository = new TodoRepository(context);
                var todo = new ToDo.API.DAL.Entities.Todo { Name = "New Todo", IsComplete = false };

                // Act
                var createdTodo = await repository.CreateAsync(todo);

                // Assert
                Assert.NotNull(createdTodo);
                Assert.Equal("New Todo", createdTodo.Name);
                Assert.False(createdTodo.IsComplete);
            }
        }

        [Fact]
        public async Task UpdateAsync_UpdatesTodo()
        {
            // Arrange
            int id;
            using (var context = new DataBaseContext(_options))
            {
                var todo = new ToDo.API.DAL.Entities.Todo { Name = "Old Todo", IsComplete = false };
                context.Todos.Add(todo);
                await context.SaveChangesAsync();
                id = todo.Id;
            }

            using (var context = new DataBaseContext(_options))
            {
                var repository = new TodoRepository(context);
                var todo = new ToDo.API.DAL.Entities.Todo { Name = "Updated Todo", IsComplete = true };

                // Act
                var result = await repository.UpdateAsync(todo, id);

                // Assert
                Assert.True(result);
                var updatedTodo = await context.Todos.FindAsync(id);
                Assert.Equal("Updated Todo", updatedTodo.Name);
                Assert.True(updatedTodo.IsComplete);
            }
        }

        [Fact]
        public async Task DeleteAsync_DeletesTodo()
        {
            // Arrange
            int id;
            using (var context = new DataBaseContext(_options))
            {
                var todo = new ToDo.API.DAL.Entities.Todo { Name = "Test", IsComplete = false };
                context.Todos.Add(todo);
                await context.SaveChangesAsync();
                id = todo.Id;
            }

            using (var context = new DataBaseContext(_options))
            {
                var repository = new TodoRepository(context);

                // Act
                var result = await repository.DeleteAsync(id);

                // Assert
                Assert.True(result);
                var deletedTodo = await context.Todos.FindAsync(id);
                Assert.Null(deletedTodo);
            }
        }
    }
}