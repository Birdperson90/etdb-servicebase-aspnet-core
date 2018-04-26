using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Etdb.ServiceBase.DocumentRepository.Abstractions.Context;
using Etdb.ServiceBase.TestInfrastructure.MongoDb.Context;
using Etdb.ServiceBase.TestInfrastructure.MongoDb.Documents;
using Etdb.ServiceBase.TestInfrastructure.MongoDb.Repositories;
using Microsoft.Extensions.Options;
using Xunit;

namespace Etdb.ServiceBase.DocumentRepository.IntegrationTests.Generics
{
    public class GenericDocumentRepositoryTests
    {
        private readonly TodoListDocumentRepository repository;

        public GenericDocumentRepositoryTests()
        {
            this.repository = new TodoListDocumentRepository(new TestDocumentDbContext(Options.Create(new DocumentDbContextOptions
            {
                ConnectionString = "mongodb://admin:admin@localhost:27017",
                DatabaseName = "Etdb_ServiceBase_Tests",
                UseCamelCaseConvention = true
            })));
        }
        
        [Fact]
        public async Task GenericDocumentRepository_CreateReadUpdateDeleteMultipleAsync_ExpectSuccess()
        {
            var createLists = CreateRandom(5, 3);

            foreach (var createList in createLists)
            {
                await this.repository.AddAsync(createList);
            }
            
            var createIds = createLists.Select(todoList => todoList.Id).ToArray();

            foreach (var id in createIds)
            {
                var readList =
                    await this.repository.FindAsync(todoList => todoList.Id == id);
                
                Assert.NotNull(readList);
                Assert.True(readList.Todos.Count == 3);

                var newTitle = $"NewTitle_{id}";

                readList.Titel = newTitle;
                readList.Todos.First().Done = true;

                var updated = await this.repository.EditAsync(readList);
                Assert.True(updated);
                
                readList =
                    await this.repository.FindAsync(todoList => todoList.Id == id);
                
                Assert.Equal(newTitle, readList.Titel);
                Assert.True(readList.Todos.First().Done);
            }
            
            var allTodoLists = await this.repository.GetAllAsync();
            
            Assert.True(createIds.All(createId => allTodoLists.Select(todoList => todoList.Id).Contains(createId)));
            
            allTodoLists = await this.repository.FindAllAsync(todoList => todoList.Titel.StartsWith("NewTitle"));
            
            var todoListArray = allTodoLists as TodoListDocument[] ?? allTodoLists.ToArray();
            
            Assert.True(createLists.Select(todoList => todoList.Id).All(createId => todoListArray.Select(todoList => todoList.Id).Contains(createId)));

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
                
                readList = await this.repository.FindAsync(list => list.Titel == $"NewTitle_{ id }");
                
                Assert.NotNull(readList);

                var deleted = await this.repository.DeleteAsync(readList.Id);

                Assert.True(deleted);
            }
            
            allTodoLists = await this.repository.GetAllAsync();

            todoListArray = allTodoLists as TodoListDocument[] ?? allTodoLists.ToArray();
            
            foreach (var allId in todoListArray.Select(todoList => todoList.Id))
            {
                Assert.DoesNotContain(createIds, id => id == allId);
            }
        }
        
        [Fact]
        public void GenericDocumentRepository_CreateReadUpdateDeleteMultiple_ExpectSuccess()
        {
            var createLists = CreateRandom(5, 3);

            foreach (var createList in createLists)
            {
                this.repository.Add(createList);
            }

            var createIds = createLists.Select(todoList => todoList.Id).ToArray();

            foreach (var id in createIds)
            {
                var readList = this.repository.Find(todoList => todoList.Id == id);
                
                Assert.NotNull(readList);
                Assert.True(readList.Todos.Count == 3);

                var newTitle = $"NewTitle_{id}";

                readList.Titel = newTitle;
                readList.Todos.First().Done = true;

                var updated = this.repository.Edit(readList);
                Assert.True(updated);
                
                readList = this.repository.Find(todoList => todoList.Id == id);
                
                Assert.Equal(newTitle, readList.Titel);
                Assert.True(readList.Todos.First().Done);
            }
            
            var allTodoLists = this.repository.GetAll();
            
            Assert.True(createLists.Select(todoList => todoList.Id).All(createId => allTodoLists.Select(todoList => todoList.Id).Contains(createId)));

            allTodoLists = this.repository.FindAll(todoList => todoList.Titel.StartsWith("NewTitle"));

            var todoListArray = allTodoLists as TodoListDocument[] ?? allTodoLists.ToArray();
            
            Assert.True(createLists.Select(todoList => todoList.Id).All(createId => todoListArray.Select(todoList => todoList.Id).Contains(createId)));

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
                
                readList = this.repository.Find(list => list.Titel == $"NewTitle_{ id }");
                
                Assert.NotNull(readList);
                
                var deleted = this.repository.Delete(readList.Id);

                Assert.True(deleted);
            }
            
            allTodoLists = this.repository.GetAll();
            
            todoListArray = allTodoLists as TodoListDocument[] ?? allTodoLists.ToArray();
            
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
            }

            var count = this.repository.Count();
            
            Assert.True(5 <= count);
        }

        private static ICollection<TodoListDocument> CreateRandom(int lists = 1, int children = 0)
        {
            var random = new Random();
            
            var todoLists = new List<TodoListDocument>();

            for (var i = 0; i < lists; i++)
            {
                var todoList = new TodoListDocument
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

        private static TodoDocument CreateRandom(Guid todoListId, int prio = 0)
        {
            return new TodoDocument
            {
                Id = Guid.NewGuid(),
                Prio = prio,
                Title = new Random().Next(1111, 9999999).ToString(),
                ListId = todoListId
            };
        }
    }
}