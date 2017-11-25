using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ETDB.API.ServiceBase.ContextBase
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
