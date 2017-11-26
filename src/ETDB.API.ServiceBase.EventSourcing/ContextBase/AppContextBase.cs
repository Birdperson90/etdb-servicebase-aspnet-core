using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ETDB.API.ServiceBase.EventSourcing.ContextBase
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
