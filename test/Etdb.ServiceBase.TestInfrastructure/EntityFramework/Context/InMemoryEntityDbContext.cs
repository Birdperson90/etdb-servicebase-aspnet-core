using Etdb.ServiceBase.EntityRepository.Abstractions.Context;
using Etdb.ServiceBase.TestInfrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace Etdb.ServiceBase.TestInfrastructure.EntityFramework.Context
{
    public class InMemoryEntityDbContext : EntityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("InMemoryTestDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            {
                var builder = modelBuilder.Entity<TodoListEntity>();
                
                builder.HasKey(tl => tl.Id);

                builder.HasMany(tl => tl.Todos)
                    .WithOne(td => td.List)
                    .HasForeignKey(td => td.ListId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasIndex(tl => tl.Titel)
                    .IsUnique(true);

                builder.Property(tl => tl.Titel)
                    .IsRequired();
            }

            {
                var builder = modelBuilder.Entity<TodoEntity>();

                builder.HasKey(td => td.Id);

                builder.HasIndex(td => new
                {
                    td.ListId,
                    td.Title
                });
            }
        }
    }
}