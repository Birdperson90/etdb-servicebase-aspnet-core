using Microsoft.EntityFrameworkCore;

namespace ETDB.API.ServiceBase.TestBase.Mocks
{
    public class DbContextMock : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EntityMock>();
        }
    }
}
