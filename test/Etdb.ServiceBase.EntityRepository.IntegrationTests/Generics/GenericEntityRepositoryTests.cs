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
        public async Task GenericEntityRepository_CreateReadUpdateDeleteMultipleAsync_ExpectSuccess()
        {
            var createLists = CreateRandom(5, 3);

            foreach (var createList in createLists)
            {
                await this.repository.AddAsync(createList);
                var added = await this.repository.EnsureChangesAsync();
                Assert.True(added);
            }

            var createIds = createLists.Select(todoList => todoList.Id).ToArray();

            foreach (var id in createIds)
            {
                var readList =
                    await this.repository.FindAsync(todoList => todoList.Id == id, todoList => todoList.Todos);

                Assert.NotNull(readList);
                Assert.True(readList.Todos.Count == 3);

                var newTitle = $"NewTitle_{id}";

                readList.Titel = newTitle;
                readList.Todos.First().Done = true;

                this.repository.Edit(readList);
                var updated = await this.repository.EnsureChangesAsync();
                Assert.True(updated);

                readList =
                    await this.repository.FindAsync(todoList => todoList.Id == id, todoList => todoList.Todos);

                Assert.Equal(newTitle, readList.Titel);
                Assert.True(readList.Todos.First().Done);
            }

            var allTodoLists = await this.repository.GetAllAsync();

            Assert.True(createIds.All(createId => allTodoLists.Select(todoList => todoList.Id).Contains(createId)));

            allTodoLists = await this.repository.FindAllAsync(todoList => todoList.Titel.StartsWith("NewTitle"),
                todoList => todoList.Todos);

            var todoListArray = allTodoLists as TodoListEntity[] ?? allTodoLists.ToArray();

            Assert.True(createLists.Select(todoList => todoList.Id).All(createId =>
                todoListArray.Select(todoList => todoList.Id).Contains(createId)));

            var intersectingIds = createLists.Select(todoList => todoList.Id)
                .Intersect(todoListArray.Select(todoList => todoList.Id));

            foreach (var id in intersectingIds)
            {
                var todoList = todoListArray.First(list => list.Id == id);

                Assert.True(todoList.Todos.Count == 3);
            }

            foreach (var id in createIds)
            {
                var readList = await this.repository.FindAsync(id);

                Assert.NotNull(readList);

                readList = await this.repository.FindAsync(list => list.Titel == $"NewTitle_{id}");

                Assert.NotNull(readList);

                this.repository.Delete(readList);

                var deleted = await this.repository.EnsureChangesAsync();

                Assert.True(deleted);
            }

            allTodoLists = await this.repository.GetAllAsync();

            todoListArray = allTodoLists as TodoListEntity[] ?? allTodoLists.ToArray();

            foreach (var allId in todoListArray.Select(todoList => todoList.Id))
            {
                Assert.DoesNotContain(createIds, id => id == allId);
            }
        }

        [Fact]
        public void GenericEntityRepository_CreateReadUpdateDeleteMultiple_ExpectSuccess()
        {
            var createLists = CreateRandom(5, 3);

            foreach (var createList in createLists)
            {
                this.repository.Add(createList);
                var added = this.repository.EnsureChanges();
                Assert.True(added);
            }

            var createIds = createLists.Select(todoList => todoList.Id).ToArray();

            foreach (var id in createIds)
            {
                var readList = this.repository.Find(todoList => todoList.Id == id, todoList => todoList.Todos);

                Assert.NotNull(readList);
                Assert.True(readList.Todos.Count == 3);

                var newTitle = $"NewTitle_{id}";

                readList.Titel = newTitle;
                readList.Todos.First().Done = true;

                this.repository.Edit(readList);
                var updated = this.repository.EnsureChanges();
                Assert.True(updated);

                readList = this.repository.Find(todoList => todoList.Id == id, todoList => todoList.Todos);

                Assert.Equal(newTitle, readList.Titel);
                Assert.True(readList.Todos.First().Done);
            }

            var allTodoLists = this.repository.GetAll();

            Assert.True(createLists.Select(todoList => todoList.Id).All(createId =>
                allTodoLists.Select(todoList => todoList.Id).Contains(createId)));

            allTodoLists = this.repository.FindAll(todoList => todoList.Titel.StartsWith("NewTitle"),
                todoList => todoList.Todos);

            var todoListArray = allTodoLists as TodoListEntity[] ?? allTodoLists.ToArray();

            Assert.True(createLists.Select(todoList => todoList.Id).All(createId =>
                todoListArray.Select(todoList => todoList.Id).Contains(createId)));

            var intersectingIds = createLists.Select(todoList => todoList.Id)
                .Intersect(todoListArray.Select(todoList => todoList.Id));

            foreach (var id in intersectingIds)
            {
                var todoList = todoListArray.First(list => list.Id == id);

                Assert.True(todoList.Todos.Count == 3);
            }

            foreach (var id in createIds)
            {
                var readList = this.repository.Find(id);

                Assert.NotNull(readList);

                readList = this.repository.Find(list => list.Titel == $"NewTitle_{id}");

                Assert.NotNull(readList);

                this.repository.Delete(readList);

                var deleted = this.repository.EnsureChanges();

                Assert.True(deleted);
            }

            allTodoLists = this.repository.GetAll();

            todoListArray = allTodoLists as TodoListEntity[] ?? allTodoLists.ToArray();

            foreach (var allId in todoListArray.Select(todoList => todoList.Id))
            {
                Assert.DoesNotContain(createIds, id => id == allId);
            }
        }

        [Fact]
        public async Task GenericEntityRepositoryTests_CreateCountMultipleAsync_ExpectSuccess()
        {
            var createList = CreateRandom(5);

            foreach (var todoList in createList)
            {
                await this.repository.AddAsync(todoList);
                var added = await this.repository.EnsureChangesAsync();
                Assert.True(added);
            }

            var count = await this.repository.CountAsync();

            Assert.True(5 <= count);
        }

        [Fact]
        public void GenericEntityRepositoryTests_CreateCountMultiple_ExpectSuccess()
        {
            var createList = CreateRandom(5);

            foreach (var todoList in createList)
            {
                this.repository.Add(todoList);
                var added = this.repository.EnsureChanges();
                Assert.True(added);
            }

            var count = this.repository.Count();

            Assert.True(5 <= count);
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
                    todoList.Todos.Add(CreateRandom(todoList.Id, j));
                }

                todoLists.Add(todoList);
            }

            return todoLists;
        }

        private static TodoEntity CreateRandom(Guid todoListId, int prio = 0)
        {
            return new TodoEntity
            {
                Id = Guid.NewGuid(),
                Prio = prio,
                Title = new Random().Next(1111, 9999999).ToString(),
                ListId = todoListId
            };
        }
    }
}