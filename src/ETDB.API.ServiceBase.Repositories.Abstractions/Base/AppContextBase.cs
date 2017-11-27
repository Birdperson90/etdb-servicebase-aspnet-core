using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ETDB.API.ServiceBase.Repositories.Abstractions.Base
{
    public class AppContextBase : DbContext
    {
        protected void DisableCascadeDelete(ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes().SelectMany(entity => entity.GetForeignKeys()))
            {
                entity.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
