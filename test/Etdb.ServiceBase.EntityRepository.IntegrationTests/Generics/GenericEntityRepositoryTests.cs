using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Etdb.ServiceBase.TestInfrastructure.EntityFramework.Context;
using Etdb.ServiceBase.TestInfrastructure.EntityFramework.Entities;
using Etdb.ServiceBase.TestInfrastructure.EntityFramework.Repositories;
using Xunit;

namespace Etdb.ServiceBase.EntityRepository.IntegrationTests.Generics
{
    public class GenericEntityRepositoryTests
    {
        private readonly ITodoListEntityRepository repository;
        
        public GenericEntityRepositoryTests()
        {
            this.repository = new TodoListEntityRepository(new InMemoryEntityDbContext());
        }

        [Fact]
        public async Task GenericEntityRepository_CreateMultipleReadUpdateDeleteAsync_ExpectSuccess()
        {
            var createLists = CreateRandom(5, 3);
            var addedToIdsByListId = new Dictionary<Guid, List<Guid>>();

            foreach (var createList in createLists)
            {
                await this.repository.AddAsync(createList);
                var added = await this.repository.EnsureChangesAsync();
                Assert.True(added);
                addedToIdsByListId.Add(createList.Id, createList.Todos.Select(todo => todo.Id).ToList());
            }

            foreach (var key in addedToIdsByListId.Keys)
            {
                var readList =
                    await this.repository.FindAsync(todoList => todoList.Id == key, todoList => todoList.Todos);
                
                Assert.NotNull(readList);
                Assert.True(readList.Todos.Count == 3);

                var newTitle = $"NewTitel_{key}";

                readList.Titel = newTitle;
                readList.Todos.First().Done = true;

                this.repository.Edit(readList);
                var updated = await this.repository.EnsureChangesAsync();
                Assert.True(updated);
                
                readList =
                    await this.repository.FindAsync(todoList => todoList.Id == key, todoList => todoList.Todos);
                
                Assert.Equal(newTitle, readList.Titel);
                Assert.True(readList.Todos.First().Done);
            }

            var allTodoLists = await this.repository.GetAllAsync();
            
            Assert.True(createLists.Select(todoList => todoList.Id).All(createId => allTodoLists.Select(todoList => todoList.Id).Contains(createId)));
        }

        private static ICollection<TodoListEntity> CreateRandom(int lists = 1, int children = 0)
        {
            var random = new Random();
            
            var todoLists = new List<TodoListEntity>();

            for (var i = 0; i < lists; i++)
            {
                var todoList = new TodoListEntity
                {
                    Id = Guid.NewGuid(),
                    Titel = random.Next(1111, 9999999).ToString()
                };
                
                for (var j = 0; j < children; j++)
                {
                    todoList.Todos.Add(new TodoEntity
                    {
                        Id = Guid.NewGuid(),
                        Prio = j,
                        Title = random.Next(1111, 9999999).ToString(),
                        ListId = todoList.Id
                    });
                }
                
                todoLists.Add(todoList);
            }

            return todoLists;
        }
    }
}