using System;
using Etdb.ServiceBase.EventSourcing.Abstractions.Base;
using Etdb.ServiceBase.EventSourcing.Abstractions.Commands;
using Etdb.ServiceBase.Repositories.Abstractions.Base;

namespace Etdb.ServiceBase.EventSourcing.Base
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppContextBase context;

        public UnitOfWork(AppContextBase context)
        {
            this.context = context;
        }

        public SourcingCommandResponse Commit()
        {
            var rowsAffected = this.context.SaveChanges();
            return new SourcingCommandResponse(rowsAffected > 0);
        }

        public void Dispose()
        {
            this.context?.Dispose();
        }
    }
}
