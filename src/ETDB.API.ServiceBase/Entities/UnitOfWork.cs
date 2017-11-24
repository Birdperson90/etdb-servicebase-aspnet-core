using System;
using System.Collections.Generic;
using System.Text;
using ETDB.API.ServiceBase.Domain.Abstractions.Base;
using ETDB.API.ServiceBase.Domain.Abstractions.Commands;
using Microsoft.EntityFrameworkCore;

namespace ETDB.API.ServiceBase.Entities
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public CommandResponse Commit()
        {
            var rowsAffected = this.context.SaveChanges();
            return new CommandResponse(rowsAffected > 0);
        }
    }
}
