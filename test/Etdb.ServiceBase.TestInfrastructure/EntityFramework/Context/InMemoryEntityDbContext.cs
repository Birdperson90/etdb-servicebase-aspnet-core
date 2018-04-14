using Etdb.ServiceBase.EntityRepository.Abstractions.Context;
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
            base.OnModelCreating(modelBuilder);
        }
    }
}